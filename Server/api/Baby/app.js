const controller = require('./BabyController');
const express = require('express');
const router = express.Router();

router.post('/SetInitialBaby', controller.SetInitialBaby);
router.get('/LoadBabyInfo', controller.LoadBabyInfo);

module.exports = router;