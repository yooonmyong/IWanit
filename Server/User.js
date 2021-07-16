const mysql = require('mysql');
const bodyParser = require('body-parser');
const router = require('express').Router();
const urlencodedParser = bodyParser.urlencoded({ extended: false });

var pool = mysql.createPool(
  {
    host: '',
    user: '',
    port: '',
    password: '',
    database: '',
    connectionLimit: 1000,
    connectTimeout: 20000,
    acquireTimeout: 20000,
    timeout: 20000,
    waitForConnections: false
  }
);

router.route('/sign-up').post(urlencodedParser, (req, res, next) => {

});

router.route('/sign-in').post(urlencodedParser, (req, res, next) => {

});

router.route('/update-info').post(urlencodedParser, (req, res, next) => {

});

router.route('/find-id').post(urlencodedParser, (req, res, next) => {

});

router.route('find-pwd').post(urlencodedParser, (req, res, next) => {

});

router.route('/delete-account').post(urlencodedParser, (req, res, next) => {

});

module.exports = router;