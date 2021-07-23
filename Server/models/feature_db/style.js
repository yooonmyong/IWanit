module.exports = function(sequelize, DataTypes) {
  return sequelize.define('style', {
    Name: {
      type: DataTypes.STRING(100),
      allowNull: false,
      primaryKey: true
    },
    Kind: {
      type: DataTypes.STRING(100),
      allowNull: false,
      primaryKey: true
    },
    Color: {
      type: DataTypes.STRING(100),
      allowNull: false
    }
  }, {
    sequelize,
    tableName: 'style',
    timestamps: false,
    indexes: [
      {
        name: "PRIMARY",
        unique: true,
        using: "BTREE",
        fields: [
          { name: "Name" },
          { name: "Kind" },
        ]
      },
    ]
  });
};
