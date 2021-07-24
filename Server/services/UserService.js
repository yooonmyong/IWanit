const bcrypt = require('bcrypt-nodejs');

exports.SignUp = async (userID, userEmail, userPWD, repeatedUserPWD) => {
    const regularExpressionforID = /^[A-Za-z]{1}[A-Za-z0-9_-]{3,19}$/;
    const regularExpressionforPWD = /(?=.*\d)(?=.*[a-zA-ZS]).{8,}/;
    const saltRounds = 10;

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
        bcrypt.genSalt(saltRounds, (err, salt) => {
            if (err) {
                console.log("Bcrypt generating salt error occured: " + err)                
                reject(err);
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