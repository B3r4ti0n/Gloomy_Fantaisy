const express = require('express');
const router = express.Router();

const service = require('../../services/v1/level');

router.put('/get', service.getByLevel);

router.put('/add', service.add);

router.delete('/delete', service.delete);

module.exports = router;