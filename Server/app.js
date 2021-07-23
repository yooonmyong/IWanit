const express = require('express'); 
const userRouter = require('./User');
const app = express(); 

app.use('/User', userRouter);

