const DataTypes = require("sequelize").DataTypes;
const _user = require("./user");
const _user_friends = require("./user_friends");

function initModels(sequelize) {
  const user = _user(sequelize, DataTypes);
  const user_friends = _user_friends(sequelize, DataTypes);

  return {
    user,
    user_friends,
  };
}

module.exports = initModels;
module.exports.initModels = initModels;
module.exports.default = initModels;
