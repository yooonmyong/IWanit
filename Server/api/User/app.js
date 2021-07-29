const express = require('express');
const router = express.Router();
const controller = require('./UserController');
const passport = require('./passport');

router.post('/SignUp', controller.SignUp);
router.post('/SignIn',
    passport.authenticate(
        'local', 
        { 
            successRedirect: '/Baby/LoadBabyInfo',
            failureRedirect: '/User/FailedSignIn',
            failureFlash: false
        }
    )
);
router.get('/FailedSignIn', controller.FailedSignIn);
router.post('/SignOut', controller.SignOut);
router.post('/UpdateInfo', controller.UpdateInfo);
router.post('/FindID', controller.FindID);
router.post('/FindPWD', controller.FindPWD);
router.post('/DeleteUser', controller.DeleteUser);

module.exports = router;