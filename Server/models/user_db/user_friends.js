module.exports = function(sequelize, DataTypes) {
  return sequelize.define('user_friends', {
    ID: {
      type: DataTypes.STRING(25),
      allowNull: false,
      primaryKey: true,
      references: {
        model: 'user',
        key: 'ID'
      }
    },
    friend_ID: {
      type: DataTypes.STRING(25),
      allowNull: false,
      primaryKey: true,
      references: {
        model: 'user',
        key: 'ID'
      }
    },
    relationship: {
      type: DataTypes.BLOB,
      allowNull: true
    },
    isBlocked: {
      type: DataTypes.TINYINT,
      allowNull: true
    }
  }, {
    sequelize,
    tableName: 'user_friends',
    timestamps: false,
    indexes: [
      {
        name: "PRIMARY",
        unique: true,
        using: "BTREE",
        fields: [
          { name: "ID" },
          { name: "friend_ID" },
        ]
      },
      {
        name: "friend_ID_idx",
        using: "BTREE",
        fields: [
          { name: "friend_ID" },
        ]
      },
    ]
  });
};
