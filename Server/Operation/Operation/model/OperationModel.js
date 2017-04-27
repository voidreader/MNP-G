var request = require('request');
var serverList = require('./ServerList');
var log = require('../utils/LogUtil');
var queryUtil = require('../utils/QueryUtil');
var dbAgent = require('./DBAgent');

exports.PushMailSchedule = PushMailSchedule;
function PushMailSchedule() {

    var strQuery = queryUtil.QueryGen('sp_select_op_mail_schedule');
    dbAgent.Query(dbAgent.OP, strQuery, function (err, rows) {
        if (err) {
            log.write(1, err, err.message);
        }
        else {
            log.write(99, 'PushMailSchedule completed', '');
        }
    });
}


