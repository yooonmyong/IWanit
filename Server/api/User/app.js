const controller = require('./UserController');
const express = require('express');
const passport = require('./passport');
const router = express.Router();

router.post('/SignUp', controller.SignUp);
router.post('/SignIn', (req, res) => {
    passport.authenticate('local', (err, user, info) => {
        if (err) {
            return res.status(500).send({ "message": err });
        }

        if (!user) {
            return res.status(422).send({ "message": "Trying to access using invalid userID" });
        }

        return req.login(user, err => {
            if (err) {
                return res.status(500).send({ "message": err });
            }
            const fillteredUser = { ...user.dataValues };
            console.dir(fillteredUser);
            delete fillteredUser.password;
            return res.json(fillteredUser);
        });
    })(req, res);
});
router.delete('/SignOut', controller.SignOut);
router.patch('/UpdatePWD', controller.UpdatePWD);
router.post('/FindID', controller.FindID);
router.post('/FindPWD', controller.FindPWD);
router.get('/SendEmail', controller.SendEmail);
router.delete('/DeleteUser', controller.DeleteUser);

module.exports = router;