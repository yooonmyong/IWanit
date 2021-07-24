module.exports = function(sequelize, DataTypes) {
  return sequelize.define('baby_taste', {
    ID: {
      type: DataTypes.STRING(25),
      allowNull: false,
      primaryKey: true,
      references: {
        model: 'user',
        key: 'ID'
      }
    },
    Food: {
      type: DataTypes.JSON,
      allowNull: false
    },
    Playing: {
      type: DataTypes.JSON,
      allowNull: false
    },
    Color: {
      type: DataTypes.JSON,
      allowNull: false
    }
  }, {
    sequelize,
    tableName: 'baby_taste',
    timestamps: false,
    indexes: [
      {
        name: "PRIMARY",
        unique: true,
        using: "BTREE",
        fields: [
          { name: "ID" },
        ]
      },
    ]
  });
};
