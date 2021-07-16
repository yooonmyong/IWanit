const express = require('express'); 
const userRouter = require('./User');
const app = express(); 

app.use('/User', userRouter);

var port = ;
app.listen(port, () => {
    console.log('Server started at ' + port);
});