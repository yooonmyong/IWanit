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
            const result =
                await UserService.CheckSignUp(
                    userID, userEmail, userPWD, repeatedUserPWD
                );

            await User
                .create(
                    {
                        ID: userID,
                        email: userEmail,
                        PWD: result.message.hashedPWD
                    }
                )
                .then(() => {
                    console.log('Success to sign up');
                    return res.sendStatus(200);
                })
                .catch((error) => {
                    if (error.message === 'Validation error') {
                        result.message = 'Duplicated ID';
                    } else {
                        result.message = error;
                        console.log(error);
                    }

                    return res.status(500).json(result);
                });
        } catch (error) {
            console.log('Failed to sign up');
            return res.status(422).json(error);
        }
    },
    SignOut: async (req, res) => {
        const result = { "message": "Success to sign out" };

        if (req.session.isDelete) {
            result.message = "Success to delete user";
            req.session.isDelete = null;
        }

        req.logout();
        await req.session.save(() => {
            console.log(result.message);
            return res.status(204).json(result);
        });
    },
    UpdatePWD: async (req, res) => {
        const originalUserPWD = req.body.originalUserPWD;
        const changedUserPWD = req.body.changedUserPWD;
        const repeatedChangedUserPWD = req.body.repeatedChangedUserPWD;

        try {
            const isSamePWD =
                await UserService.ComparePWD(originalUserPWD, req.user.PWD);
            if (!isSamePWD) {
                return res.status(422).send({ 
                    "message": "Incorrect password" 
                });
            }
        }
        catch (error) {
            console.log('Failed to compare passwords');
            return res.status(500).json(error);
        }
        const checkResult =
            await UserService.CheckPWD(changedUserPWD, repeatedChangedUserPWD);

        if (checkResult.message === 'Possible password') {
            try {
                const result = await UserService.HashPWD(changedUserPWD);

                User
                    .update({ PWD: result.hashedPWD }, {
                        where: {
                            ID: req.user.ID
                        }
                    })
                    .then(() => {
                        console.log('Success to update password');
                        return res.sendStatus(201);
                    })
                    .catch((sequelizeError) => {
                        console.log(sequelizeError);
                        return res.status(500).send({
                            "message": sequelizeError
                        });
                    });
            } catch (error) {
                console.log('Failed to hash password');
                return res.status(500).json(error);
            }
        } else {
            console.log('Failed to update password');
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
                    return res.status(404).send({ 
                        "message": "Not exist user" 
                    });
                }

                try {
                    const isSamePWD =
                        await UserService.ComparePWD(userPWD, user.PWD);
                    if (!isSamePWD) {
                        return res.status(422).send({ 
                            "message": "Incorrect password" 
                        });
                    }

                    console.log('Send userID to valid email');
                    req.session.sender = config.development.nodemailer.senderID;
                    req.session.receiver = userEmail;
                    req.session.subject = '[IWanit] 회원님의 아이디 입니다'
                    req.session.content = user.ID + ' 입니다.';

                    return res.redirect("/User/SendEmail");
                } catch (error) {
                    console.log('Failed to compare passwords');
                    return res.status(500).json(error);
                }
            })
            .catch((sequelizeError) => {
                console.log(sequelizeError);
                return res.status(500).send({ "message": sequelizeError });
            });
    },
    FindPWD: async (req, res) => {
        const userID = req.body.userID;
        const userEmail = req.body.userEmail;
        const temporaryPWD = Math.random().toString(36).slice(2);

        try {
            const result = await UserService.HashPWD(temporaryPWD);

            User
                .update({ PWD: result.hashedPWD }, {
                    where: {
                        ID: userID,
                        email: userEmail
                    }
                })
                .then((result) => {
                    if (result.includes(0)) {
                        console.log('Not exist user');
                        return res.status(404).send({ 
                            "message": "Not exist user" 
                        });
                    }

                    console.log('Send temporary password to valid email');
                    req.session.sender =
                        config.development.nodemailer.senderID;
                    req.session.receiver = userEmail;
                    req.session.subject =
                        '[IWanit] 회원님의 임시 비밀번호 입니다'
                    req.session.content =
                        temporaryPWD
                        + ' 입니다. 로그인 후 꼭 비밀번호를 변경하시길 바랍니다.';

                    return res.redirect("/User/SendEmail");
                })
                .catch((sequelizeError) => {
                    console.log(sequelizeError);
                    return res.status(500).send({
                        "message": sequelizeError
                    });
                });
        } catch (error) {
            console.log('Failed to hash password');
            return res.status(500).json(error);
        }
    },
    SendEmail: async (req, res) => {
        const mailParameters = {
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
        const repeatedUserPWD = req.body.repeatedUserPWD;

        try {
            const isSamePWD =
                await UserService.ComparePWD(repeatedUserPWD, req.user.PWD);
            if (!isSamePWD) {
                return res.status(422).send({ 
                    "message": "Incorrect password" 
                });
            }
        } catch (error) {
            console.log('Failed to compare passwords');
            return res.status(500).json(error);
        }
        User
            .destroy({
                where: {
                    ID: req.user.ID
                }
            })
            .then(() => {
                console.log('Success to delete account');
                req.session.isDelete = true;
                return res.redirect('/User/SignOut');
            })
            .catch((sequelizeError) => {
                console.log(sequelizeError);
                return res.status(500).send({ "message": sequelizeError });
            });
    }
}