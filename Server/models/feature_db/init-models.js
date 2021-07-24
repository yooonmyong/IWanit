var DataTypes = require("sequelize").DataTypes;
var _food = require("./food");
var _playing = require("./playing");
var _style = require("./style");

function initModels(sequelize) {
  var food = _food(sequelize, DataTypes);
  var playing = _playing(sequelize, DataTypes);
  var style = _style(sequelize, DataTypes);

  return {
    food,
    playing,
    style,
  };
}

module.exports = initModels;
module.exports.initModels = initModels;
module.exports.default = initModels;
