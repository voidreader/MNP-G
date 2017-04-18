

var fs = require('fs')
    , Log = require('log')
    , log = new Log('debug', null);
    //, log = new Log('debug', fs.createWriteStream('./log/my.log', {flags:'a'}));
/*
exports.EMERGENCY = 0;
exports.ALERT = 1;
exports.CRITICAL = 2;
exports.ERROR = 3;
exports.WARNING = 4;
exports.NOTICE = 5;
exports.INFO = 6;
exports.DEBUG = 7;

log.emergency('emergency');
log.alert('alert');
log.critical('critical');
log.error('error');
log.warning('warning');
log.notice('notice');
log.info('info');
log.debug('debug');
*/

// ansi code
// http://pueblo.sourceforge.net/doc/manual/ansi_color_codes.html

function write(errlv, titletext, jsontext) {
    var curTime = new Date();
    var errLevel;
    var color;

    if ( 99 !== errlv && log.level - 1 < errlv ) {
        return;
    }

    switch(errlv) {
        case 1 : {
            color = 31;     // red
            errLevel = 'Critical';
            log.critical(titletext + ' - %s', JSON.stringify(jsontext));
        } break;
        case 2 : {
            color = 33;     // yellow
            errLevel = 'Error';
            log.error(titletext + ' - %s', JSON.stringify(jsontext));
        } break;
        case 3 : {
            color = 32;     // green
            errLevel = 'Warning';
            log.warning('%s - %s', titletext, JSON.stringify(jsontext));
        } break;
        case 4 : {
            color = 36;     // cyan
            errLevel = 'Note';
            log.notice('%s - %s', titletext, JSON.stringify(jsontext));
        } break;
        case 5 : {
            color = 34;     // cyan
            errLevel = 'Test';
            log.debug('%s - %s', titletext, JSON.stringify(jsontext));
        } break;
        case 6 : {
            color = 34;     // cyan
            errLevel = 'Debug';
            log.debug('%s - %s', titletext, JSON.stringify(jsontext));
        } break;
        default : {
            color = 35;     // magenta (purple)
            errLevel = 'Info';
            log.info('%s - %s', titletext, JSON.stringify(jsontext));
        } break;
    } // end of switch

    console.log( curTime.toLocaleString() + ': \x1b[' + color + 'm[' + errLevel + ']\x1b[0m ' + titletext + ' - ' + JSON.stringify(jsontext) );
}
exports.write = write;