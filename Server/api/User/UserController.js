const User = require('../../models')['user'];
const User_friends = require('../../models')['user_friends'];
const UserService = require('../../services/UserService');

module.exports = {
    SignUp: async (req, res) => {
        const userID = req.body.userID;
        const userEmail = req.body.userEmail;
        var result = await UserService.SignUp(userID, userEmail, req.body.userPWD, req.body.repeatedUserPWD);

        if (typeof result.message === 'string') {
            return res.status(422).json(result);
        }

        await User
        .create({ ID: userID, email: userEmail, PWD: result.message.hashedPWD })
        .then(() => {
            console.log("Success to sign up");
            return res.sendStatus(200);
        })
        .catch((err) => {
            if (err.message === 'Validation error') {
                result.message = 'Duplicated ID';
            }
            else {
                result.message = err;
                console.log(err);
            }

            return res.status(500).json(result);
        });
    },
    SignIn: function(req, res, next) {
    },
    SignOut: function(req, res, next) {
    },
    UpdateInfo: function(req, res, next) {
    },
    FindID: function(req, res, next) {
    },
    FindPWD: function(req, res, next) {
    },
    DeleteUser: function(req, res, next) {
    }
}