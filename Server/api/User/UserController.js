const config = require('../../config/config.json');
const nodemailer = require('./nodemailer');
const User = require('../../models')['user'];
const UserService = require('../../services/UserService');

module.exports = {
    SignUp: async (req, res) => {
        const userID = req.body.userID;
        const userEmail = req.body.userEmail;
        const userPWD = req.body.userPWD;
        const repeatedUserPWD = req.body.repeatedUserPWD;

        try {
            const result = await UserService.CheckSignUp(userID, userEmail, userPWD, repeatedUserPWD);

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
        }
        catch (error) {
            return res.status(422).json(error);
        }
    },
    FailedSignIn: async (req, res) => {
        return res.status(422).send({ "message": "Incorrect password or Invalid userID" });
    },
    SignOut: async (req, res) => {
        req.logout();
        await req.session.save(() => {
            console.log('Success to sign out');
            return res.redirect('/Main');
        });
    },
    UpdatePWD: async (req, res) => {
        const originalUserPWD = req.body.originalUserPWD;
        const changedUserPWD = req.body.changedUserPWD;
        const repeatedChangedUserPWD = req.body.repeatedChangedUserPWD;

        await User
            .findOne({
                where: {
                    ID: req.user.ID
                }
            })
            .then(async (user) => {
                const compareResult = await new Promise(async (resolve, reject) => {
                    var result = await UserService.ComparePWD(originalUserPWD, user.PWD);

                    if (result === null) {
                        reject(result);
                    }
                    else {
                        resolve(result);
                    }
                });
                if (!compareResult) {
                    return res.status(422).send({ "message": "Incorrect password" });
                }
            })
            .catch((sequelizeError) => {
                console.log(sequelizeError);
                return res.status(500).send({ "Sequelize module occured error": sequelizeError });
            });

        const checkResult = await UserService.CheckPWD(changedUserPWD, repeatedChangedUserPWD);

        if (checkResult.message === 'Possible password') {
            await new Promise(async (resolve, reject) => {
                const result = await UserService.HashPWD(changedUserPWD);

                if (result.hashedPWD) {
                    User
                        .update({ PWD: result.hashedPWD }, {
                            where: {
                                ID: req.user.ID
                            }
                        })
                        .then(() => {
                            return res.redirect('/User/SignOut');
                        })
                        .catch((sequelizeError) => {
                            console.log(sequelizeError);
                            return res.status(500).send({ "message": "Sequelize module occured error: " + sequelizeError });
                        });
                    resolve();
                }
                else {
                    reject(res.status(500).send({ "message": result.message }));
                }
            });
        }
        else {
            return res.status(422).send({ "message": checkResult.message });
        }
    },
    FindID: async (req, res) => {
        const userEmail = req.body.userEmail;
        const userPWD = req.body.userPWD;

        await User
            .findOne({
                where: {
                    email: userEmail
                }
            })
            .then(async (user) => {
                if (!user) {
                    console.log('Not exist user');
                    return res.status(404).send({ "message": "Not exist user" });
                }

                const compareResult = await new Promise(async (resolve, reject) => {
                    var result = await UserService.ComparePWD(userPWD, user.PWD);

                    if (result === null) {
                        reject(result);
                    }
                    else {
                        resolve(result);
                    }
                });
                if (compareResult) {
                    console.log('Send userID to valid email');
                    req.session.sender = config.development.nodemailer.senderID;
                    req.session.receiver = userEmail;
                    req.session.subject = '[IWanit] 회원님의 아이디 입니다'
                    req.session.content = user.ID + ' 입니다.';

                    return res.redirect("/User/SendEmail");
                }
                else {
                    return res.status(422).send({ "message": "Incorrect password" });
                }
            })
            .catch((sequelizeError) => {
                console.log(sequelizeError);
                return res.status(500).send({ "message": "Sequelize module occured error: " + sequelizeError });
            });
    },
    FindPWD: async (req, res) => {
        const userID = req.body.userID;
        const userEmail = req.body.userEmail;
        const temporaryPWD = Math.random().toString(36).slice(2);

        sequelize['user_db'].transaction(async (tx) => {
            const user = await User
                .findOne({
                    where: {
                        ID: userID,
                        email: userEmail
                    }
                }, { transaction: tx });

            if (!user) {
                return res.status(404).send({ "message": "Not exist user" });
            }

            await new Promise(async (resolve, reject) => {
                var result = await UserService.HashPWD(temporaryPWD);

                if (result.hashedPWD) {
                    User
                        .update({ PWD: result.hashedPWD }, {
                            where: {
                                ID: user.ID,
                                email: user.email
                            }
                        }, { transaction: tx })
                        .then(() => {
                            console.log('Send temporary password to valid email');
                            req.session.sender = config.development.nodemailer.senderID;
                            req.session.receiver = userEmail;
                            req.session.subject = '[IWanit] 회원님의 임시 비밀번호 입니다'
                            req.session.content = temporaryPWD + ' 입니다. 로그인 후 꼭 비밀번호를 변경해주시기 바랍니다.';

                            return res.redirect("/User/SendEmail");
                        })
                        .catch((sequelizeError) => {
                            console.log(sequelizeError);
                            return res.status(500).send({ "Sequelize module occured error": sequelizeError });
                        });
                    resolve();
                }
                else {
                    reject(res.status(500).send({ "message": result.message }));
                }
            });
        });
    },
    SendEmail: async (req, res) => {
        var mailParameters = {
            sender: req.session.sender,
            receiver: req.session.receiver,
            subject: req.session.subject,
            content: req.session.content
        };

        nodemailer.sendMail(mailParameters);
        req.session.sender = null;
        req.session.receiver = null;
        req.session.subject = null;
        req.session.content = null;

        return res.sendStatus(200);
    },
    DeleteUser: async (req, res) => {
        await User
            .destroy({
                where: {
                    ID: req.user.ID
                }
            })
            .then(() => {
                console.log('Success to delete account');
                return res.redirect('/User/SignOut');
            })
            .catch((sequelizeError) => {
                console.log(sequelizeError);
                return res.status(500).send({ "Sequelize module occured error": sequelizeError });
            });
    }
}