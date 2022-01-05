const controller = require('./ParentingController');
const express = require('express');
const router = express.Router();

router.get('/LoadFoodInfo', controller.LoadFoodInfo);
router.get('/LoadPlayingInfo', controller.LoadPlayingInfo);

module.exports = router;