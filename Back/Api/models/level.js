const mongoose = require('mongoose');
const Schema   = mongoose.Schema;

const Level = new Schema({
    level: {
        type      : Number,
        trim      : true,
    },
    level_point: {
        type     : Number,
        trim     : true,
    },
    exp: {
        type     : Number,
        trim     : true,
    },
}, {
    timestamps: true // ajoute 2 champs au document createdAt et updatedAt
});

// hook executé avant la sauvegarde d'un document. Hash le mot de passe quand il est modifié
Level.pre('save', function(next) {

    next();
});

module.exports = mongoose.model('Level', Level);