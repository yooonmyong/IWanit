const path = require('path');
const Sequelize = require('sequelize');
const basename = path.basename(module.filename);
const fs = require('fs');

const env = process.env.NODE_ENV || 'development';
const config = require(path.join(__dirname, '..', 'config', 'config.json'))[env];
const db = {};

const databases = Object.keys(config.databases);

for (var i = 0; i < databases.length; i++) {
    var database = databases[i];
    var dbPath = config.databases[database];

    db[database] = new Sequelize(
        dbPath.database,
        dbPath.username,
        dbPath.password,
        {
            host: dbPath.host,
            port: dbPath.port,
            dialect: 'mysql',
            define: {
                charset: 'utf8mb4',
                collate: 'utf8mb4_0900_ai_ci'
            }
        }
    );

    fs
    .readdirSync(__dirname + '/' + database)
    .filter(file =>
        (file.indexOf('.') !== 0) &&
        (file !== basename) &&
        (!file.startsWith('init')) &&
        (file.slice(-3) === '.js'))
    .forEach(file => {
        const model = require(path.join(__dirname + '/' + database, file))(db[database], Sequelize.DataTypes);
        db[model.name] = model;
    });
}

Object.keys(db).forEach(modelName => {
    if (db[modelName].associate) {
        db[modelName].associate(db);
    }
});

db.Sequelize = Sequelize;

module.exports = db;