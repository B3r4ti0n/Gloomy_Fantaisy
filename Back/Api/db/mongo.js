const dotenv = require('dotenv');
const mongoose = require('mongoose');

const clientOptions = {
    useNewUrlParser   : true,
    dbName            : 'gloomy_fantasy_dev'
};

exports.initClientDbConnection = async () => {
    try {
        await mongoose.connect("mongodb://root:prod@localhost/", clientOptions)
        console.log('Connected');
    } catch (error) {
        console.log(error);
        throw error;
    }
}