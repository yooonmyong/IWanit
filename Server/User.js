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
  pool.getConnection((err, connection) => {
    if (!err) {
      connection.beginTransaction((err) => {
        if (err) {
          console.log("Mysql transaction error occured: " + err);
          connection.rollback(function () {
            connection.release();
          });
        }
        else {
          const regularExpressionforID = /^[A-Za-z]{1}[A-Za-z0-9_-]{3,19}$/;
          const regularExpressionforPWD = /(?=.*\d)(?=.*[a-zA-ZS]).{8,}/;
          const saltRounds = 10;
          var userID = req.body.userID;
          var userEmail = req.body.userEmail;
          var userPWD = req.body.userPWD;
          var repeatedUserPWD = req.body.repeatedUserPWD;

          if (!regularExpressionforID.test(userID)) {
            console.log("Inappropriate expression for userID");
            return res.status(422).send({ message: 'Inappropriate ID' });
          }
          else if (!regularExpressionforPWD.test(userPWD)) {
            console.log("Inappropriate expression for userPWD");
            return res.status(422).send({ message: 'Inappropriate PWD' });
          }
          else if (!(userPWD === repeatedUserPWD)) {
            console.log("PWDs are not matched");
            return res.status(422).send({ message: 'Not matched PWD' });
          }
          else {
            bcrypt.genSalt(saltRounds, (err, salt) => {
              console.log("Bcrypt generated salt: " + salt);
              if (err) {
                console.log("Bcrypt generating salt error occured: " + err);
              }
              else {
                var sql = `insert into user_db.user(ID, PWD, email) values(?, ?, ?);`;
                bcrypt.hash(userPWD, salt, null, (err, hash) => {
                  var params = [userID, hash, userEmail];
                  connection.query(sql, params, (err, results) => {
                    if (err) {
                      console.log("Mysql query error occured: " + err);
                      connection.rollback(() => {
                        connection.release();
                      });
                      if (err.code === 'ER_DUP_ENTRY') {
                        return res.status(500).send({ message: 'Duplicated ID' });
                      }
                    }
                    else {
                      connection.commit((err) => {
                        if (err) {
                          console.log("Mysql commit error occured: " + err);
                          connection.rollback(() => {
                            connection.release();
                          });
                        }
                        else {
                          connection.release();
                          console.log("Success to insert data: " + results);
                          res.send(results);
                        }
                      });
                    }
                  });
                });
              }
            });
          }
        }
      });
    }
    else {
      console.log("MySql connection error occured: " + err);
    }
  });
});

router.route('/sign-in').post(urlencodedParser, (req, res, next) => {
  pool.getConnection((err, connection) => {
    if (!err) {
      connection.beginTransaction((err) => {
        if (err) {
          console.log("Mysql transaction error occured: " + err);
          connection.rollback(function () {
            connection.release();
          });
        }
        else {
          var userID = req.body.userID;
          var userPWD = req.body.userPWD;
          var sql = `select * from user_db.user where user.ID = '${userID}';`;
          connection.query(sql, (err, results) => {
            if (err) {
              console.log("Mysql query error occured: " + err);
              connection.release();
            }
            else {
              if (results.length <= 0) {
                console.log("Not exist user");
                return res.status(403).send({ error: 'Not exist user' });
              }
              bcrypt.compare(userPWD, results[0].PWD, (err, result) => {
                if (result) {
                  res.send({ isCorrectPWD : true });
                }
                else {
                  console.log("Incorrect password");
                  res.status(403).send({ error: 'Incorrect password' });
                }
                connection.release();
              });
            }
          });
        }
      });
    }
    else {
      console.log("MySql connection error occured: " + err);
    }
  });
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