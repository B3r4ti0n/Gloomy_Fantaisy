const Stats = require('../../models/stats');

exports.getById = async (req, res, next) => {
    const { id } = req.body;

    try {
        let stats = await Stats.findById(id);

        if (stats) {
            return res.status(200).json(stats);
        }

        return res.status(404).json('user_not_found');
    } catch (error) {
        return res.status(501).json(error);
    }
}

exports.add = async (req, res, next) => {
    const temp = {};

    ({ 
        race : temp.race,
        level : temp.level,
        exp : temp.exp,
        gold : temp.gold,
        gold_premium : temp.gold_premium,
        health_point : temp.health_point,
        offensive_value : temp.offensive_value,
        defensive_value : temp.defensive_value,
        intelligence_value : temp.intelligence_value,
        speed_value : temp.speed_value,
        mana_value : temp.mana_value,
    } = req.body);

    Object.keys(temp).forEach((key) => (temp[key] == null) && delete temp[key]);

    try {
        let stats = await Stats.create(temp);

        return res.status(201).json(stats);
    } catch (error) {
        return res.status(501).json(error);
    }
}

exports.update = async (req, res, next) => {
    const temp = {};

    ({ 
        _id : temp.id,
        race : temp.race,
        level : temp.level,
        exp : temp.exp,
        gold : temp.gold,
        gold_premium : temp.gold_premium,
        health_point : temp.health_point,
        offensive_value : temp.offensive_value,
        defensive_value : temp.defensive_value,
        intelligence_value : temp.intelligence_value,
        speed_value : temp.speed_value,
        mana_value : temp.mana_value,
    } = req.body);

    Object.keys(temp).forEach((key) => (temp[key] == null) && delete temp[key]);

    try {
        console.log(temp);
        let stats = await Stats.findOne({ _id: temp.id });
        console.log(stats);
        if (stats) {       
            Object.keys(temp).forEach((key) => {
                if (!!temp[key]) {
                    stats[key] = temp[key];
                }
            });
            
            await stats.save();
            return res.status(201).json(stats);
        }

        return res.status(404).json('stats_not_found');
    } catch (error) {
        return res.status(501).json(error);
    }
}

exports.delete = async (req, res, next) => {
    const { id } = req.body;

    try {
        await Stats.deleteOne({ _id: id });

        return res.status(204).json('delete_ok');
    } catch (error) {
        return res.status(501).json(error);
    }
}