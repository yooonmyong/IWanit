const nodemailer = require('nodemailer');
const config = require('../../config/config.json');

const nodeMailer = {
    sendMail: (mailParameters) => {
        var transporter = nodemailer.createTransport({
            service: 'gmail',
            host: 'smtp.gmail.com',
            port: 587,
            secure: false,
            auth: {
              user: config.development.nodemailer.senderID,
              pass: config.development.nodemailer.senderPWD
            },
        });
        
        var mailOptions = {
            from: mailParameters.sender,
            to: mailParameters.receiver,
            subject: mailParameters.subject,
            text: mailParameters.content
        };
        
        transporter.sendMail(mailOptions, (nodemailerError, info) => {
            if (nodemailerError) {
                console.log('nodemailer occured error: ' + nodemailerError);
            }
            else {
                console.log('nodemailer sent: ' + info.response);
            }
        });        
    }
};

module.exports = nodeMailer;