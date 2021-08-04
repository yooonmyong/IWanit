const LocalStrategy = require('passport-local').Strategy;
const passport = require('passport');
const User = require('../../models')['user'];
const UserService = require('../../services/UserService');

passport.serializeUser((user, done) => {
    console.log('Serialize user');
    done(null, user.ID);
});

passport.deserializeUser((userID, done) => {
    User.findByPk(userID).then(user => {
        if (user !== undefined) {
            console.log('Deserialize user: ' + userID);
            done(null, user);
        }
    })
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

            const compareResult = await new Promise(async (resolve, reject) => {
                var result = await UserService.ComparePWD(password, user.PWD);

                if (result === true) {
                    resolve(result);
                }
                else {
                    reject(result);                    
                }
            });
            if (compareResult) {
                console.log('Success to sign in');
                return done(null, user);
            }
            else {
                return done(null, false);
            }
        })
        .catch((sequelizeError) => {
            console.log(sequelizeError);
            return done({ "message": "Sequelize module occured error: " + sequelizeError });
        });
}));

module.exports = passport;