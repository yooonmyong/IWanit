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
    UUID: {
      type: DataTypes.CHAR(36),
      allowNull: false,
      primaryKey: true
    },
    Name: {
      type: DataTypes.STRING(100),
      allowNull: false
    },
    Age: {
      type: DataTypes.SMALLINT.UNSIGNED,
      allowNull: false
    },
    Level: {
      type: DataTypes.JSON,
      allowNull: false
    },
    Weight: {
      type: DataTypes.DECIMAL(4,2),
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
          { name: "UUID" },
        ]
      },
    ]
  });
};
