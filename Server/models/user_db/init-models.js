var DataTypes = require("sequelize").DataTypes;
var _user = require("./user");
var _user_friends = require("./user_friends");

function initModels(sequelize) {
  var user = _user(sequelize, DataTypes);
  var user_friends = _user_friends(sequelize, DataTypes);

  return {
    user,
    user_friends,
  };
}

module.exports = initModels;
module.exports.initModels = initModels;
module.exports.default = initModels;
