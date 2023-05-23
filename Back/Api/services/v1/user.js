const User = require('../../models/user');
const bcrypt   = require('bcryptjs');

exports.getById = async (req, res, next) => {
    const { id } = req.params;

    try {
        let user = await User.findById(id);

        if (user) {
            return res.status(200).json(user);
        }

        return res.status(404).json('user_not_found');
    } catch (error) {
        return res.status(501).json(error);
    }
}

exports.getByNameWithPassword = async (req, res, next) => {
    const temp = {};

    ({ 
        name     : temp.name,
        password : temp.password,
    } = req.body);

    try{
        let user = await User.findOne({ name : temp.name});
        
        if (user) {
            bcrypt.compare(temp.password, user.password, (err, result) => {
                if (err) {
                  return err;
                }
              
                if (result) {
                    return res.status(201).json(user);
                } else {
                    return res.status(404).json('Invalide password');
                }
            });
        }else{
            return res.status(404).json('Invalide user');
        }
    }catch(error){
        return res.status(501).json(error);
    }

    
}

exports.add = async (req, res, next) => {
    const temp = {};

    ({ 
        name     : temp.name,
        email    : temp.email,
        password : temp.password,
        ID_Stats : temp.ID_Stats
    } = req.body);

    Object.keys(temp).forEach((key) => (temp[key] == null) && delete temp[key]);

    try {
        let user = await User.create(temp);

        return res.status(201).json(user);
    } catch (error) {
        return res.status(501).json(error);
    }
}

exports.update = async (req, res, next) => {
    const temp = {};

    ({ 
        name     : temp.name,
        email    : temp.email,
        ID_Stats : temp.ID_Stats,
    } = req.body);

    try {
        let user = await User.findOne({ email: temp.email });

        if (user) {       
            Object.keys(temp).forEach((key) => {
                if (!!temp[key]) {
                    user[key] = temp[key];
                }
            });
            
            await user.save();
            return res.status(201).json(user);
        }

        return res.status(404).json('user_not_found');
    } catch (error) {
        return res.status(501).json(error);
    }
}

exports.delete = async (req, res, next) => {
    const { id } = req.body;

    try {
        await User.deleteOne({ _id: id });

        return res.status(204).json('delete_ok');
    } catch (error) {
        return res.status(501).json(error);
    }
}