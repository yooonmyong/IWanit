// Entry point (In here, call middleware you need)
const express = require('express'); 
const bodyParser = require('body-parser');
const session = require('express-session');
const passport = require('passport');
const config = require('./config/config.json');
const app = express();

app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: true }));
app.use(session({
    secret: config.session.secret,
    resave: false,
    saveUninitialized: true
}));
app.use(passport.initialize());
app.use(passport.session());

app.use('/User', require('./api/User/app'));

module.exports = app;