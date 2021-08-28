var DataTypes = require("sequelize").DataTypes;
var _baby = require("./baby");

function initModels(sequelize) {
  var baby = _baby(sequelize, DataTypes);

  baby.belongsTo(user, { as: "ID_user", foreignKey: "ID"});
  user.hasMany(baby, { as: "babies", foreignKey: "ID"});

  return {
    baby,
  };
}
module.exports = initModels;
module.exports.initModels = initModels;
module.exports.default = initModels;
