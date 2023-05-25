const Level = require('../../models/level');

exports.getByLevel = async (req, res, next) => {
    const temp = {};

    ({ 
        level     : temp.level,
    } = req.body);

    try {
        let level = await Level.findOne(temp);

        if (level) {
            return res.status(200).json(level);
        }

        return res.status(404).json('level_not_found');
    } catch (error) {
        return res.status(501).json(error);
    }
}

exports.add = async (req, res, next) => {
    const temp = {};

    ({ 
        level           : temp.level,
        level_point     : temp.level_point,
        exp             : temp.exp
    } = req.body);

    Object.keys(temp).forEach((key) => (temp[key] == null) && delete temp[key]);

    try {
        let level = await Level.create(temp);

        return res.status(201).json(level);
    } catch (error) {
        return res.status(501).json(error);
    }
}

exports.delete = async (req, res, next) => {
    const { level } = req.body;

    try {
        await User.deleteOne({ level: level });

        return res.status(204).json('delete_ok');
    } catch (error) {
        return res.status(501).json(error);
    }
}
