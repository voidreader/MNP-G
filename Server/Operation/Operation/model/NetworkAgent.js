var crypto = require( 'crypto' );
var request = require('request');

const SUCCESS = 0;
const FAIL = 1;

exports.SUCCESS = SUCCESS;
exports.FAIL = FAIL;

function Response( res, result, error, data, token ) {

	var ret = {
		result: result,
		error: error,
		data: data
	};

	var output;
	if( token != '' ) {
		// stringify : java object 를 JSON 으로.
		// parse : JSON을 java object로
		var json = JSON.stringify( ret );
		var cipher = crypto.createCipher( 'aes192', token );
		output = cipher.update( json, 'utf8', 'base64' );

		output += cipher.final( 'base64' );
	}

	if( res != undefined ) {

		res.send( ret );  // 암호화 안한 패킷
		//res.send(output);  // 암호화한 패킷

	}
}
exports.Response = Response;

function Post(URLEncodedForms, callback) {
	request.post( URLEncodedForms, function(err, res, body) {
		callback(err, res, body);
	});
}
exports.Post = Post;

function Get(URL, callback) {
	request.get( URL, function(req, message, res) {
		callback( req, message, res );
	});
}
exports.Get = Get;