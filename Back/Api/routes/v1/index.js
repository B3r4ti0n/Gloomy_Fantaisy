var express = require('express');
var router = express.Router();

const userRoute = require('./users');
const statsRoute = require('./stats');
const levelRoute = require('./levels')

router.get('/', async (req, res) => {
    res.status(200).json({
        name   : 'API', 
        version: '1.0', 
        status : 200, 
        message: 'Bienvenue sur l\'API !'
    });
});

router.use('/users', userRoute);
router.use('/stats', statsRoute);
router.use('/levels', levelRoute);

module.exports = router;