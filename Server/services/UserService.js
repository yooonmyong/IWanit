const bcrypt = require('bcrypt-nodejs');
const config = require('../config/config.json');

exports.CheckSignUp = async (userID, userEmail, userPWD, repeatedUserPWD) => {
    const checkResult = await new Promise(async (resolve, reject) => {
        var result = await this.CheckID(userID);

        if (result.message !== 'Possible ID') {
            reject({ "message": result.message });
        }
        else {
            result = await this.CheckPWD(userPWD, repeatedUserPWD);
            if (result.message !== 'Possible password') {
                reject({ "message": result.message });
            }
        }

        resolve({ "message": await this.HashPWD(userPWD) });
    });

    return checkResult;
}

exports.CheckID = async (id) => {
    const regularExpressionforID = /^[A-Za-z0-9]{1}[A-Za-z0-9_-]{3,19}$/;

    if (!regularExpressionforID.test(id)) {
        console.log('Inappropriate expression for userID');
        return { "message": "Inappropriate ID" };
    }

    return { "message": "Possible ID" };
}

exports.CheckPWD = async (password, repeatedPassword) => {
    const regularExpressionforPWD = /(?=.*\d)(?=.*[a-zA-ZS]).{8,}/;

    if (!regularExpressionforPWD.test(password)) {
        console.log('Inappropriate expression for userPWD');
        return { "message": "Inappropriate PWD" };
    }
    else if (password !== repeatedPassword) {
        console.log('PWDs are not matched');
        return { "message": "Not matched PWD" };
    }

    return { "message": "Possible password" };
}

exports.HashPWD = async (rawPassword) => {
    const saltRounds = config.development.bcrypt.saltRounds;

    const hashingResult = await new Promise((resolve, reject) => {
        bcrypt.genSalt(saltRounds, (bcryptError, salt) => {
            if (bcryptError) {
                console.log('Bcrypt generating salt error occured: ' + bcryptError);
                reject({ "message": bcryptError });
            }
            else {
                bcrypt.hash(rawPassword, salt, null, (err, hash) => {
                    console.log("hashed PWD: " + hash);
                    resolve({ "hashedPWD": hash });
                });
            }
        });
    });

    return hashingResult;
}

exports.CheckEmail = (email) => {
}

exports.CheckPWD = async (inputPWD, userPWD) => {
    if (inputPWD === undefined || userPWD === undefined) {
        console.log('Invalid password');
        return false;
    }

    const compareResult = await new Promise((resolve, reject) => {
        console.log(inputPWD + ", " + userPWD);
        bcrypt.compare(inputPWD, userPWD, (err, result) => {
            if (err) {
                reject({ "message": "Invalid PWD" });
            }
            if (result) {
                resolve(true);
            }
            else {
                console.log('Incorrect password');
                resolve(false);
            }
        });
    });
    return compareResult;
}