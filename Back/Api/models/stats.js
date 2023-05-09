const mongoose = require('mongoose');
const Schema   = mongoose.Schema;

const Stats = new Schema({
    race: {
        type    : String,
        trim    : true,
        required: [true, 'Race must be require']
    },
    exp: {
        type    : Number,
        trim    : true,
    },
    gold: {
        type    : Number,
        trim    : true,
    },
    gold_premium: {
        type    : Number,
        trim    : true,
    },
    healt_point: {
        type    : Number,
        trim    : true,
    },
    offensive_value: {
        type    : Number,
        trim    : true,
    },
    defensive_value: {
        type    : Number,
        trim    : true,
    },
    intelligence_value: {
        type    : Number,
        trim    : true,
    },
    speed_value: {
        type    : Number,
        trim    : true,
    },
    mana_value: {
        type    : Number,
        trim    : true,
    },
}, {
    timestamps: true // add 2 attributs in documents createdAt and updatedAt
});

// execute hook below the document's save
Stats.pre('save', function(next) {
    const categories = ['healt_point', 'offensive_value', 'defensive_value', 'intelligence_value', 'speed_value', 'mana_value'];
    let points = 600;
    const result = {};
    const limits = race_choise(this.race);
    
    for (let i = 0; i < categories.length; i++) {
        const limit = limits[categories[i]];
        const randomPoints = Math.floor(Math.random() * (Math.min(points, limit.max) - limit.min + 1) + limit.min);
        result[categories[i]] = randomPoints;
        points -= randomPoints;
    }
    
    this.healt_point = result['healt_point'];
    this.offensive_value = result['offensive_value'];
    this.defensive_value = result['defensive_value'];
    this.intelligence_value = result['intelligence_value'];
    this.speed_value = result['speed_value'];
    this.mana_value = result['mana_value'];

    next();
});

module.exports = mongoose.model('Stats', Stats);

function race_choise(race) {
    switch (race) {
        case "Orc":
            return limits = {
                'healt_point': { min: 100, max: 200 },
                'offensive_value': { min: 50, max: 150 },
                'defensive_value': { min: 10, max: 30 },
                'intelligence_value': { min: 40, max: 80 },
                'speed_value': { min: 80, max: 120 },
                'mana_value': { min: 70, max: 130 },
            };
    
        default:
            break;
    }
}