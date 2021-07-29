const User = require('../../models')['user'];
const UserService = require('../../services/UserService');

module.exports = {
    SignUp: async (req, res) => {
        const userID = req.body.userID;
        const userEmail = req.body.userEmail;
        var result = await UserService.SignUp(userID, userEmail, req.body.userPWD, req.body.repeatedUserPWD);

        if (typeof result.message === 'string') {
            return res.status(422).json(result);    // Error occured.
        }

        await User
            .create({ ID: userID, email: userEmail, PWD: result.message.hashedPWD })
            .then(() => {
                console.log('Success to sign up');
                return res.sendStatus(200);
            })
            .catch((sequelizeError) => {
                if (sequelizeError.message === 'Validation error') {
                    result.message = 'Duplicated ID';
                }
                else {
                    result.message = 'Sequelize occured error';
                    console.log(sequelizeError);
                }

                return res.status(500).json(result);
            });
    },
    FailedSignIn: async (req, res) => {
        return res.status(403).send({ "message": "Incorrect password or Invalid userID" });
    },
    SignOut: (req, res, next) => {
    },
    UpdateInfo: (req, res, next) => {
    },
    FindID: (req, res, next) => {
    },
    FindPWD: (req, res, next) => {
    },
    DeleteUser: (req, res, next) => {
    }
}