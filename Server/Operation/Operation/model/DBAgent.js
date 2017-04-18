var fs = require( 'fs' );
var xmlTojs = require( 'xml2js' );
var mysql = require( 'mysql' );
var queryUtil = require('../utils/QueryUtil');
var log = require('../utils/LogUtil');
/*
const SUCCESS = 0;
const FAIL = 1;

exports.SUCCESS = SUCCESS;
exports.FAIL = FAIL;
*/
const AccountDB = 0;
const GameDB = 1;
const GameLog = 2;
const EnvDB = 3;
const OP = 4;

exports.AccountDB = AccountDB;
exports.GameDB = GameDB;
exports.GameLog = GameLog;
exports.EnvDB = EnvDB;
exports.OP = OP;

// ----------------------------------------------------------
// DB agent util
// ----------------------------------------------------------
//var DBConnection = null;
var DBConnectionPool = null;

function Callback_ProcessDBConnection( err, result ) {
	var dbInfo = result.DBConnection;

	DBConnectionPool = new Array();
	DBConnectionPool[AccountDB] = mysql.createPool( dbInfo.AccountDB );
	DBConnectionPool[GameDB] = mysql.createPool( dbInfo.GameDB );
	DBConnectionPool[GameLog] = mysql.createPool( dbInfo.GameLog );
    DBConnectionPool[EnvDB] = mysql.createPool(dbInfo.EnvDB);
    DBConnectionPool[OP] = mysql.createPool(dbInfo.OP);

	var ret = {
		connectionLimit : dbInfo.GameDB.connectionLimit
		, host : dbInfo.GameDB.host
		, port : dbInfo.GameDB.port
		, database : dbInfo.GameDB.database
	};
	log.write(99, 'GameDB information', ret);

	
}

function LoadData() {
	fs.readFile( __dirname + '/DB/DB_Connection.xml', 'utf8', function ( err, data ) {
		if ( err ) {
			log.write(1, 'Load Fail DB_Connection', '');
			process.exit( 1 );
			return;
		}

		parser = new xmlTojs.Parser( { mergeAttrs: true, explicitArray: false } );
		parser.parseString( data, Callback_ProcessDBConnection );
	});
}
exports.LoadData = LoadData;


// ----------------------------------------------------------
// DB Query
// ----------------------------------------------------------
// Multiple statement queries
function DBQuery_Multiple(dbindex, query, callback) {
	Query( dbindex, query, function ( err, rows ) {
		callback(err, rows);
	});
}
exports.DBQuery_Multiple = DBQuery_Multiple;

function Query( dbIndex, query, callback ) {
	if ( dbIndex < 0 || dbIndex >= DBConnectionPool.length ) {
		log.write(2, 'Invalid DBIndex', dbIndex);
		return;
	}

	DBConnectionPool[dbIndex].getConnection( function(err,connection) {
		if(err) {
			log.write(1, err, err.message);
			return;
		}

		connection.beginTransaction(function() {
			connection.query( query, function ( err, result ) {
				//console.log(err, result);
				if(err) {
					return connection.rollback(function() {
						var ret = {
							err : err
							, message : err.message
						};
						log.write(1, query, ret);
						//throw err;
						// db에러가 나더라도 서버를 죽이지 않도록 한다. 로그는 필수 확인
						return;
					});
				}

				connection.commit(function(err) {
					if (err) {
						return connection.rollback(function() {
							log.write(1, err, err.message);
							//throw err;
							// db에러가 나더라도 서버를 죽이지 않도록 한다. 로그는 필수 확인
							return;
						});
					}

					callback(err, result);
				});

				connection.release();
			});
		});
	});
}
exports.Query = Query;


