const bcrypt = require('bcrypt-nodejs');
const config = require('../config/config.json');

exports.SignUp = async (userID, userEmail, userPWD, repeatedUserPWD) => {
    const regularExpressionforID = /^[A-Za-z]{1}[A-Za-z0-9_-]{3,19}$/;
    const regularExpressionforPWD = /(?=.*\d)(?=.*[a-zA-ZS]).{8,}/;
    const saltRounds = config.bcrypt.saltRounds;

    if (!regularExpressionforID.test(userID)) {
        console.log("Inappropriate expression for userID");
        return { "message": "Inappropriate ID" };
    }
    else if (!regularExpressionforPWD.test(userPWD)) {
        console.log("Inappropriate expression for userPWD");
        return { "message": "Inappropriate PWD" };
    }
    else if (userPWD !== repeatedUserPWD) {
        console.log("PWDs are not matched");        
        return { "message": "Not matched PWD" };
    }
    // 이메일 인증 추가 자리

    const hashingResult = await new Promise((resolve, reject) => {
        bcrypt.genSalt(saltRounds, (bcryptError, salt) => {
            if (bcryptError) {
                console.log('Bcrypt generating salt error occured: ' + bcryptError);
                reject(bcryptError);
            }
            else {
                bcrypt.hash(userPWD, salt, null, (err, hash) => {
                    resolve({ "hashedPWD": hash });
                });
            }
        });
    });

    return { "message": hashingResult };
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