// Entry point (In here, call middleware you need)
const express = require('express'); 
const app = express();
const bodyParser = require('body-parser');
const config = require('./config/config.json');
const passport = require('passport');
const session = require('express-session');

app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: true }));
app.use(session({
    secret: config.development.session.secret,
    resave: false,
    saveUninitialized: true
}));
app.use(passport.initialize());
app.use(passport.session());

app.use('/User', require('./api/User/app'));

app.get('/Main', (req, res) => {
    return res.sendStatus(200);
});

module.exports = app;