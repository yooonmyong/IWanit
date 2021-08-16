module.exports = function(sequelize, DataTypes) {
  return sequelize.define('playing', {
    Name: {
      type: DataTypes.STRING(100),
      allowNull: false,
      primaryKey: true
    },
    Description: {
      type: DataTypes.TEXT,
      allowNull: false
    },
    Calorie: {
      type: DataTypes.DOUBLE,
      allowNull: false
    }
  }, {
    sequelize,
    tableName: 'playing',
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
