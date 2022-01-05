module.exports = function(sequelize, DataTypes) {
  return sequelize.define('food', {
    Name: {
      type: DataTypes.STRING(100),
      allowNull: false,
      primaryKey: true
    },
    Calorie: {
      type: DataTypes.DOUBLE,
      allowNull: false
    },
    KoreanName: {
      type: DataTypes.STRING(100),
      allowNull: false
    },
    ProperMonths: {
      type: DataTypes.INTEGER.UNSIGNED,
      allowNull: false
    }
  }, {
    sequelize,
    tableName: 'food',
    timestamps: false,
    indexes: [
      {
        name: "PRIMARY",
        unique: true,
        using: "BTREE",
        fields: [
          { name: "Name" },
        ]
      },
    ]
  });
};
