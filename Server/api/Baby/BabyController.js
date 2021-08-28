const Baby = require('../../models')['baby'];
const BabyTaste = require('../../models')['baby_taste'];
const BabyLanguage = require('../../models')['baby_language'];
const BabyService = require('../../services/BabyService');
const config = require('../../config/settingBaby.json');
const { v4: uuidv4 } = require('uuid');

module.exports = {
    SetInitialBaby: async (req, res) => {
        if (!req.user) {
            return res.status(401).send({ "message": "No session info" });
        }

        const babyName = req.body.babyName;
        const babyLevel = ;
        const babyWeight = ;
        const babyAppearance = ;
        const babyTemperament = ;

        Baby
            .create({
                ID: req.user.ID,
                UUID: uuidv4(),
                Name: babyName,
                Months: 0,
                Level: babyLevel,
                Weight: babyWeight,
                Appearance: babyAppearance,
                Temperament: babyTemperament
            })
            .then(() => {
                console.log('Success to create new baby');
                return res.sendStatus(201);
            })
            .catch((sequelizeError) => {
                console.log(sequelizeError);
                return res.status(500).send({ "message": sequelizeError });
            });
    },
    LoadBabyInfo: async (req, res) => {
        if (!req.user) {
            return res.status(401).send({ "message": "No session info" });
        }

        await Baby
            .findOne({
                where: {
                    ID: req.user.ID
                }
            })
            .then((userBaby) => {
                if (userBaby) {
                    console.log('Success to load baby info');
                    return res.status(200).json(userBaby);
                }
                else {
                    console.log('There\'s no baby info');
                    return res.status(404).send({ "message": "No baby info" });
                }
            })
            .catch((sequelizeError) => {
                console.log(sequelizeError);
                return res.status(500).send({ "message": sequelizeError });
            });
    }
}