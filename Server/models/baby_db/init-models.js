var DataTypes = require("sequelize").DataTypes;
var _baby = require("./baby");
var _baby_taste = require("./baby_taste");
var _baby_language = require("./baby_language");

function initModels(sequelize) {
  var baby = _baby(sequelize, DataTypes);
  var baby_taste = _baby_taste(sequelize, DataTypes);
  var baby_language = _baby_language(sequelize, DataTypes);

  return {
    baby,
    baby_taste,
    baby_language,
  };
}

module.exports = initModels;
module.exports.initModels = initModels;
module.exports.default = initModels;
