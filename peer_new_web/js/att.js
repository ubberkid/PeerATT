var station_id = "552494308";

var appKey = "ME_DB2E01318780D65E_1";

var authToken = null;
var requestToken = null;
var subscriptionId = null;
var gateway = null;

var attLock = null;
var attLock_current = false;

var attLight = null;
var attLight_current = false;

var attPlug = null;
var attPlug_current = false;

function getDLToken() {
    $.ajax({
        url: 'https://systest.digitallife.att.com:443/penguin/api/authtokens?userId='+station_id+'&password=NO-PASSWD&domain=DL&appKey='+appKey+'&rememberMe=false',
        type: 'post',
        success: function (data) {

            authToken = data.content.authToken;
            requestToken = data.content.requestToken;
            subscriptionId = data.content.subscriptionId;

            gateway = data.content.gateways[0].id
            console.log("ATT DL Token Success");
            pollDevices();
        }
    });
};

function pollDevices() {
    $.ajax({
        url: 'https://systest.digitallife.att.com:443/penguin/api/'+gateway+'/devices',
        type: 'get',
        headers: {
            appKey: appKey,
            authToken: authToken,
            requestToken: requestToken,
        },
        success: function (data) {

            for(var obj in data.content) {
                //console.log("" + data.content[obj].deviceType + ": "+ data.content[obj].deviceGuid);

                if(data.content[obj].deviceType == "door-lock") attLock = data.content[obj].deviceGuid;
                if(data.content[obj].deviceType == "light-control") attLight = data.content[obj].deviceGuid;
                if(data.content[obj].deviceType == "smart-plug") attPlug = data.content[obj].deviceGuid;
            }
            console.log("ATT Device Success");
           // console.info(data);
        }
    });
};

function switchAttLock() {
    var lockstatus = attLock_current ? 'lock' : 'unlock';
    console.log(lockstatus);
    attLock_current = !attLock_current;
    $.ajax({
        url: 'https://systest.digitallife.att.com:443/penguin/api/'+gateway+'/devices/'+attLock+'/lock/' + lockstatus,
        type: 'post',
        headers: {
            appKey: appKey,
            authToken: authToken,
            requestToken: requestToken,
        },
        success: function (data) {
            console.info(data);
        }
    });
};

function switchAttLight() {
    var lightstatus = attLight_current ? 'on' : 'off';
    console.log(lightstatus);
    attLight_current = !attLight_current;
    $.ajax({
        url: 'https://systest.digitallife.att.com:443/penguin/api/'+gateway+'/devices/'+attLight+'/switch/' + lightstatus,
        type: 'post',
        headers: {
            appKey: appKey,
            authToken: authToken,
            requestToken: requestToken,
        },
        success: function (data) {
            console.info(data);
        }
    });
};

function switchAttPlug() {
    var plugstatus = attPlug_current ? 'on' : 'off';
    console.log(plugstatus);
    attPlug_current = !attPlug_current;
    $.ajax({
        url: 'https://systest.digitallife.att.com:443/penguin/api/'+gateway+'/devices/'+attPlug+'/switch/' + plugstatus,
        type: 'post',
        headers: {
            appKey: appKey,
            authToken: authToken,
            requestToken: requestToken,
        },
        success: function (data) {
            console.info(data);
        }
    });
};

getDLToken();