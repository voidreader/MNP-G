
var log = require('./utils/LogUtil');
log.write(99, '                         ', '');
log.write(99, '--- Operation Server start ---', '');

log.write(99, '--- Standard Require loading ---', '');
var http = require('http');
var bodyparser = require('body-parser');
var express = require('express');
var app = express();
var op = require('./routes/OperationRoutes');
var opModel = require('./model/OperationModel');

var dbAgent = require('./model/DBAgent');
log.write(99, '--- DB data loading ---', '');
dbAgent.LoadData();


app.set('port', 7120);
app.use(bodyparser.json()); // for parsing application/json
app.use(bodyparser.urlencoded({ extended: true })); // for parsing application/x-www-form-urlencoded

app.post('/op', op.parse);


http.createServer(app).listen(app.get('port'), function () {
    log.write(99, '$$$ Express Operation server listening on port', app.get('port'));
});




var schedule3min = require('node-schedule');
var cronStyle3min = '*/3 * * * *'; // 3분 마다 실행
var job3min = schedule3min.scheduleJob(cronStyle3min, function () {
    //log.write(99, 'Cron every Minute', '');
    //gameData.LoadHotTime();

    opModel.PushMailSchedule();

});