const DataTypes = require("sequelize").DataTypes;
const _food = require("./food");
const _playing = require("./playing");
const _style = require("./style");

function initModels(sequelize) {
  const food = _food(sequelize, DataTypes);
  const playing = _playing(sequelize, DataTypes);
  const style = _style(sequelize, DataTypes);

  return {
    food,
    playing,
    style,
  };
}

module.exports = initModels;
module.exports.initModels = initModels;
module.exports.default = initModels;
