const Food = require('../../models')['food'];
const Playing = require('../../models')['playing'];

module.exports = {
    LoadFoodInfo: async (req, res) => {
        const foods = 
            await Food
                .findAll({
                    attributes: ['Name', 'Calorie', 'KoreanName', 'ProperMonths'],
                    raw: true
                })
                .catch((sequelizeError) => {
                    console.log(sequelizeError);
                    return res.status(500).send({ "message": sequelizeError });
                });
        return res.status(200).json(foods);
    },
    LoadPlayingInfo: async (req, res) => {
        const playings =
            await Playing
                .findAll({
                    attributes: ['Name', 'Calorie', 'KoreanName', 'ProperMonths'],
                    raw: true
                })
                .catch((sequelizeError) => {
                    console.log(sequelizeError);
                    return res.status(500).send({ "message": sequelizeError });
                });
        return res.status(200).json(playings);
    }
}