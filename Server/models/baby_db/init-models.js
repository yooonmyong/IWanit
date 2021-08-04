const DataTypes = require("sequelize").DataTypes;
const _baby = require("./baby");
const _baby_taste = require("./baby_taste");
const _baby_language = require("./baby_language");

function initModels(sequelize) {
  const baby = _baby(sequelize, DataTypes);
  const baby_taste = _baby_taste(sequelize, DataTypes);
  const baby_language = _baby_language(sequelize, DataTypes);

  return {
    baby,
    baby_taste,
    baby_language,
  };
}

module.exports = initModels;
module.exports.initModels = initModels;
module.exports.default = initModels;
