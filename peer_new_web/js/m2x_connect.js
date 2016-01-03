var m2x = new M2X("9fc7996ea7f03fccc6ef3978f2a4d012");
var device = "8e8402aaa6c97bc7e2f9d5bd6a454fa2";

var M2XData = {
	positionTimer: null,
	actionTimer: null,

	frequency: 5000,

	setPosition: function(pos) {
		clearTimeout(M2XData.positionTimer);
		console.log("Clearing timeout, setting position");
		m2x.devices.setStreamValue(device, "position", {value: pos}, function(result) { console.log(result); M2XData.getPosition();}, function(error) { console.log(error); });
	},

	getPosition: function() {
		M2XData.positionTimer = setTimeout(function() {
			if(CamCon.currentPosition != -1) {
				m2x.devices.streamValues(device, "position", function(values) { M2XData.gotPosition(values); }, function(error) { console.log(error); });
			} else {
				M2XData.gotPosition({values: [{value: 0}]});
			}
		},M2XData.frequency);
	},

	gotPosition: function(values) {
		var latest = values.values[0].value;
		console.log("latest position: " + latest);

		M2XData.getPosition();

		if(latest != 0 && latest != CamCon.currentPosition && CamCon.currentPosition != -1) {
			CamCon.ptz_preset_command(latest, false);
		}
	},


	getAction: function() {
		M2XData.actionTimer = setTimeout(function() {
			if(CamCon.currentPosition != -1) {
				m2x.devices.streamValues(device, "action", function(values) { M2XData.gotAction(values); }, function(error) { console.log(error); });
			} else {
				M2XData.gotAction({values: [{value: 0}]});
			}
		},M2XData.frequency);

	},

	gotAction: function(values) {

		var latest = values.values[0].value;
		console.log("latest action: " + latest);

		if(latest == 1) {
			CamCon.sayYes();
		}

		if(latest == 2) {
			CamCon.sayNo();
		}

		if(latest == 3) {
			M2XData.setPosition(16);
		}

		if(latest == 4) {
			M2XData.setPosition(8);
		}

		if(latest != 0) {
			M2XData.resetAction();
		} else {
			M2XData.getAction();
		}

	},

	setAction: function(action) {
		console.log("Setting action: " + action);
		m2x.devices.setStreamValue(device, "action", {value: action}, function(result) { console.log(result); }, function(error) { console.log(error); });
	},

	resetAction: function() {
		console.log("Resetting action");
		m2x.devices.setStreamValue(device, "action", {value: 0}, function(result) { console.log(result); M2XData.getAction(); }, function(error) { console.log(error); });
	},

};

M2XData.getPosition();
M2XData.getAction();