const express = require('express');
const router = express.Router();
const controller = require('./UserController');

router.post('/SignUp', controller.SignUp);
router.get('/SignIn', controller.SignIn);
router.post('/SignOut', controller.SignOut);
router.post('/UpdateInfo', controller.UpdateInfo);
router.post('/FindID', controller.FindID);
router.post('/FindPWD', controller.FindPWD);
router.post('/DeleteUser', controller.DeleteUser);

module.exports = router;