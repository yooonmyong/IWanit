// Entry point (In here, call middleware you need)
const express = require('express'); 
const bodyParser = require('body-parser');
const session = require('express-session');
const app = express();

app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: true }));

app.use('/User', require('./api/User/app'));

module.exports = app;