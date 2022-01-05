const Baby = require('../../models')['baby'];
const BabyService = require('../../services/BabyService');
const config = require('../../config/settingBaby.json');
const { v4: uuidv4 } = require('uuid');

module.exports = {
    SetInitialBaby: async (req, res) => {
        if (!req.user) {
            return res.status(401).send({ "message": "No session info" });
        }

        const babyName = req.body.babyName;
        const babyLevel = {
            "speaking": 1,
            "walking": 1,
            "toilet": 1
        }
        const babyWeight =
            (
                Math.random() * config.weight[0].average + config.weight[0].min
            )
                .toFixed(2);
        const babyAppearance = {
            "changeable": {
                "hairStyle":
                    config.appearance.changeable.hairStyle
                    [
                    Math.floor(Math.random()
                        * config.appearance.changeable.hairStyle.length + 0)
                    ],
                "clothes":
                    config.appearance.changeable.clothes
                    [
                    Math.floor(Math.random()
                        * config.appearance.changeable.clothes.length + 0)
                    ],
                "body": config.appearance.changeable.body[1]
            },
            "unchangeable": {
                "hairColor":
                    config.appearance.unchangeable.hairColor[
                    Math.floor(Math.random()
                        * config.appearance.unchangeable.hairColor.length + 0)
                    ],
                "eyebrow":
                    config.appearance.unchangeable.eyebrow[
                    Math.floor(Math.random()
                        * config.appearance.unchangeable.eyebrow.length + 0)
                    ],
                "eye":
                    config.appearance.unchangeable.eye[
                    Math.floor(Math.random()
                        * config.appearance.unchangeable.eye.length + 0)
                    ],
                "nose":
                    config.appearance.unchangeable.nose[
                    Math.floor(Math.random()
                        * config.appearance.unchangeable.nose.length + 0)
                    ],
                "lip":
                    config.appearance.unchangeable.lip[
                    Math.floor(Math.random()
                        * config.appearance.unchangeable.lip.length + 0)
                    ],
                "ear":
                    config.appearance.unchangeable.ear[
                    Math.floor(Math.random()
                        * config.appearance.unchangeable.ear.length + 0)
                    ],
                "skin":
                    config.appearance.unchangeable.skin[
                    Math.floor(Math.random()
                        * config.appearance.unchangeable.skin.length + 0)
                    ],
            }
        };
        babyAppearance.changeable.hairStyle = 
            babyAppearance.unchangeable.hairColor 
            + "_" 
            + babyAppearance.changeable.hairStyle;
        babyAppearance.changeable.clothes = 
            babyAppearance.unchangeable.skin 
            + "_" 
            + babyAppearance.changeable.body
            + "_"
            + babyAppearance.changeable.clothes;
        const babyTemperament = {
            "activity": Math.random().toFixed(2),
            "regularity": Math.random().toFixed(2),
            "adaptability": Math.random().toFixed(2),
            "intensity": Math.random().toFixed(2),
            "attentionPersistence": Math.random().toFixed(2),
        };

        Baby
            .create({
                ID: req.user.ID,
                UUID: uuidv4(),
                Name: babyName,
                Months: 6,
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
    },
    SaveBabyInfo: async (req, res) => {
        await Baby
            .update({
                Months: req.body.months,
                Level: JSON.parse(req.body.level),
                Weight: req.body.weight
            }, {
                where: {
                    ID: req.user.ID
                }
            });
    }
}