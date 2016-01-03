var CamCon = {

	active: false,

	imageWidth: 150,
	imageHeight: 150,

	currentPosition: -1,
	currentAction: 0,

	controlImage: null,
	moving: false,
	curious: false,
	freeControl: true,

	commands: {'stop':1, 'up':2, 'stop_up':3, 'down':0, 'stop_down':1, 'left':4, 'stop_left':5, 'right':6, 'stop_right':7, 
				'up_left':92, 'stop_up_left':1, 'up_right':93, 'stop_up_right':1,
				'down_left':90, 'stop_down_left':1, 'down_right':91, 'stop_down_right':1, 'center':25 },

	//Bounds are bounding box: [top left X, top left Y, bottom right X, bottom right Y]
	bounds: {'up':[49,0,101,50], 'down':[49,100,101,150], 'left':[0,49,50,101], 'right':[100,49,150,101],
				'up_left':[0,0,49,49], 'up_right':[101,0,150,49], 'down_left':[0,101,49,150], 'down_right':[101,101,150,150]},
	currentImage: null,

	init: function() {
	    CamCon.elm = document.getElementById('controller');
	    CamCon.ctx = CamCon.elm.getContext('2d');

	    CamCon.elm.width = CamCon.imageWidth;
	    CamCon.elm.height = CamCon.imageHeight;

	    CamCon.controlImage = new Image();
	    CamCon.controlImage.src = "images/idle.png";

	    requestAnimationFrame = window.requestAnimationFrame || 
	                        window.mozRequestAnimationFrame || 
	                        window.webkitRequestAnimationFrame || 
	                        window.msRequestAnimationFrame;

	    CamCon.bind();
	    CamCon.redraw();

	    //Flip our camera
	    CamCon.camera_command(5,3);
	},

	bind: function() {

        CamCon.elm.addEventListener('mousedown', function(e) {
            e.preventDefault();
			CamCon.checkMove();
        }, false );

        CamCon.elm.addEventListener('mouseup', function(e) {
            e.preventDefault();
			CamCon.stopMoving();
        }, false );

       	CamCon.elm.addEventListener('mousemove', function(e) {
            e.preventDefault();

            var tx = e.offsetX==undefined?e.layerX:e.offsetX;
            var yx = e.offsetY==undefined?e.layerY:e.offsetY;
			
			//console.log(tx,yx);
            CamCon.checkBounds(tx,yx);

        }, false );

       	CamCon.elm.addEventListener('mouseout', function(e) {
       		CamCon.stopMoving();
  			CamCon.currentImage = "idle";
			CamCon.controlImage.src="images/idle.png";
        }, false );
	},

	redraw: function() {
        CamCon.ctx.clearRect(0,0,150,150);
        CamCon.ctx.drawImage(CamCon.controlImage,0,0,150,150);

        requestAnimationFrame(CamCon.redraw);
	},

	checkBounds: function(tx,yx) {
		var nohit = true;
		for(var coords in CamCon.bounds) {
			if(tx>=CamCon.bounds[coords][0] && tx<=CamCon.bounds[coords][2] && yx>=CamCon.bounds[coords][1] && yx<=CamCon.bounds[coords][3]) {
				if(CamCon.currentImage != coords) {
					CamCon.currentImage = coords;
					CamCon.controlImage.src="images/"+coords+".png";
				}
			nohit = false;
			}
		}
		if(nohit == true && CamCon.currentImage != "idle") {
			CamCon.currentImage = "idle";
			CamCon.controlImage.src="images/idle.png";	
		}
	},

	checkMove: function() {
		if(CamCon.bounds.hasOwnProperty(CamCon.currentImage)) {
			CamCon.moving = true;
			//CamCon.ptz_command(CamCon.commands["stop"]);
			CamCon.ptz_command(CamCon.commands[CamCon.currentImage]);
		}
		if(CamCon.currentImage == "idle") CamCon.switchControlStyle();
	},
	stopMoving: function() {
		if(CamCon.moving == true) {
			CamCon.ptz_command(CamCon.commands["stop_"+CamCon.currentImage]);
			CamCon.moving = false;
		}
	},

	switchControlStyle: function() {
		if(CamCon.freeControl) {
			CamCon.currentPosition = 0;
			$('#floaterNav').hide();
			$('#presetNav').show();
		} else {
			CamCon.currentPosition = -1;
			$('#floaterNav').show();
			$('#presetNav').hide();
		}

		CamCon.freeControl = !CamCon.freeControl;
	},

	ptz_command: function(cmd) {
		var url = "http://"+server_ip+":81/decoder_control.cgi?loginuse=admin&loginpas=password";
		url += "&command="+cmd;
		url += "&onestep=0";
		url += "&" + Math.random();
		$.getScript(url);
	},

	ptz_preset: function(cmd, silent) {
		M2XData.setPosition(cmd);
	},

	ptz_preset_command: function(cmd,silent) {
		if(!silent) CamCon.currentPosition = cmd;
		var realcmd = 29 + (cmd * 2);
		CamCon.ptz_command(realcmd);
		$(".preset_button").removeClass('active');
		$("#prebut" + cmd).addClass('active');
	},

	camera_command: function(param,value) {
		var url = "http://"+server_ip+":81/camera_control.cgi?loginuse=admin&loginpas=password";
		url += "&param="+param+"&value="+value;
		url += "&" + Math.random();
		$.getScript(url);
	},

	sayYes: function() {
		setTimeout(function() { CamCon.ptz_preset_command(8,true); }, 100);
		setTimeout(function() { CamCon.ptz_preset_command(3,true); }, 500);
		setTimeout(function() { CamCon.ptz_preset_command(13,true); }, 1000);
		setTimeout(function() { CamCon.ptz_preset_command(3,true); }, 1500);
		setTimeout(function() { CamCon.ptz_preset_command(13,true); }, 2000);
		setTimeout(function() { CamCon.ptz_preset_command(3,true); }, 2500);
		setTimeout(function() { CamCon.ptz_preset_command(8,true); }, 3000);
		setTimeout(function() { CamCon.ptz_preset_command(CamCon.currentPosition,true); }, 3500);
	},

	sayNo: function() {
		setTimeout(function() { CamCon.ptz_preset_command(8,true); }, 100);
		setTimeout(function() { CamCon.ptz_preset_command(7,true); }, 500);
		setTimeout(function() { CamCon.ptz_preset_command(9,true); }, 1000);
		setTimeout(function() { CamCon.ptz_preset_command(7,true); }, 1500);
		setTimeout(function() { CamCon.ptz_preset_command(9,true); }, 2000);
		setTimeout(function() { CamCon.ptz_preset_command(7,true); }, 2500);
		setTimeout(function() { CamCon.ptz_preset_command(8,true); }, 3000);
		setTimeout(function() { CamCon.ptz_preset_command(CamCon.currentPosition,true); }, 3500);
	},

	startCuriosity: function() {
		CamCon.curious = true;
		CamCon.curiosity();
	},

	stopCuriosity: function() {
		CamCon.curious = false;
	},

	curiosity: function() {
		if(CamCon.curious) {
			var newMove = Math.ceil(Math.random() * 15);
			CamCon.ptz_preset(newMove);
			setTimeout(function() { CamCon.curiosity(); }, parseInt(Math.random() * 5000) + 500)
		} else {
		}
	},

}