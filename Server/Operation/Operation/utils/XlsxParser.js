var xlsx = require('node-xlsx');


function LoadData1(filePath, object, table) {
    LoadData0(filePath, object, table, '');
}

function LoadData0(filePath, object, table, sheetName) {
    var xlsxData = xlsx.parse(filePath);

    for( var sheetID in xlsxData ) {
        if( '' != sheetName && xlsxData[sheetID].name != sheetName ) {
            continue;
        }
        var sheetData = xlsxData[sheetID].data;
        var maxRowCount = sheetData.length;

        if( 0 >= maxRowCount ) {
            console.log('Table load fail. file : ' + filePath);
            process.exit(1);
        }

        // 엑셀 테이블 상위 2개 행은 컬럼 타입(int,string, 등) 과 컬럼명이 들어 있다.
        // 0-base 라 2번째 row부터 사용
        for( var i = 2; i < maxRowCount; ++i ) {
            var rowData = sheetData[i];
            var dataTable = new object();
            dataTable.Parse( rowData );
            table[dataTable.tid] = dataTable;
        }
    }
}

function LoadData(filePath, object, table) {
    var count = arguments.length;
    switch( count ) {
        case 3:
            return LoadData1(filePath, object, table);
        case 4:
            return LoadData0(filePath, object, table, arguments[3]);
    }
}

exports.LoadData = LoadData;

function xlsxDateToDate(date) {
    if(typeof date !== 'string') {
        return new Date(0);
    }

    var yyyy = date.substr(0, 4);
    var mm = date.substr(4, 2);
    var dd = date.substr(6, 2);
    var hh = date.substr(8, 2);
    var MM = date.substr(10, 2);

    return new Date(yyyy, mm, dd, hh, MM);
}
exports.xlsxDateToDate = xlsxDateToDate;