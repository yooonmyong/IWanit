const app = require('./app');
const config = require('./config/config.json')['development'];

app.listen(config.server.port, () => {
    console.log('Server started at ' + config.server.port);
});