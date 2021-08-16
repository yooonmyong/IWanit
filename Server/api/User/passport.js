const LocalStrategy = require('passport-local').Strategy;
const passport = require('passport');
const User = require('../../models')['user'];
const UserService = require('../../services/UserService');

passport.serializeUser((user, done) => {
    console.log('Serialize user');
    done(null, user.ID);
});

passport.deserializeUser((userID, done) => {
    User
        .findByPk(userID)
        .then((user) => {
            if (!user) {
                console.log('Failed to deserialize user');
                done({ "message": "Failed to deserialize user" });
            }

            console.log('Deserialize user');
            done(null, user);
        });
});

passport.use(new LocalStrategy({
    usernameField: 'userID',
    passwordField: 'userPWD',
    session: true,
    passReqToCallback: true
}, (req, id, password, done) => {
    User
        .findOne({
            where: {
                ID: id
            }
        })
        .then(async (user) => {
            if (!user) {
                console.log('Not exist user');
                return done(null, false);
            }

            try {
                const isSamePWD =
                    await UserService.ComparePWD(password, user.PWD);

                if (!isSamePWD) {
                    return done(null, false);
                }

                console.log('Success to sign in');
                return done(null, user);
            } catch (error) {
                console.log('Failed to compare passwords');
                return done(error);
            }
        })
        .catch((sequelizeError) => {
            console.log(sequelizeError);
            return done({ "message": sequelizeError });
        });
}));

module.exports = passport;