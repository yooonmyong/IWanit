module.exports = function(sequelize, DataTypes) {
  return sequelize.define('baby', {
    ID: {
      type: DataTypes.STRING(25),
      allowNull: false,
      primaryKey: true,
      references: {
        model: 'user',
        key: 'ID'
      }
    },
    Name: {
      type: DataTypes.STRING(100),
      allowNull: false
    },
    Level: {
      type: DataTypes.BOOLEAN,
      allowNull: false
    },
    Weight: {
      type: DataTypes.DOUBLE,
      allowNull: false
    },
    Appearance: {
      type: DataTypes.JSON,
      allowNull: false
    },
    Temperament: {
      type: DataTypes.JSON,
      allowNull: false
    }
  }, {
    sequelize,
    tableName: 'baby',
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
