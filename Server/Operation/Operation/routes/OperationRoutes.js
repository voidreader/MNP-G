var opModel = require('../model/OperationModel');
var network = require('../model/NetworkAgent');


var commandFunction = {};
function setupCommandFunction() {
    /*
    commandFunction['request_transfer'] = managerModel.RequestTransfer;
    commandFunction['request_ranklist'] = managerModel.RequestRankList;
    commandFunction['request_lastweekranklist'] = managerModel.RequestLastWeekRankList;
    commandFunction['request_lastweekrankreward'] = managerModel.RequestLastWeekRankReward;
    commandFunction['request_googleverify'] = managerModel.RequestGoogleVerify;
    commandFunction['request_appleverify'] = managerModel.RequestAppleVerify;
    */
}

setupCommandFunction();


exports.parse = function (req, res) {

    ///TODO Decrpyto Parameter
    var command = req.body.cmd;
    var data = req.body.data;
    ///
    //console.log(command);
    var func = commandFunction[command];
    if (func !== null) {
        func(res, data);
    }
    else {
        console.log('Can not found func : ' + command);
        network.Response(res, network.FAIL, 'func not found.', command, '');
    }
};