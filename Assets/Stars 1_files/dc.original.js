var JQUERY_UI     = '/dc_includes/jquery-ui/';
var JQUERY_UI_URL = JQUERY_UI+'';
var JQUERY_UI_EXT = '.min.js';
var WEBPATH_JQUERY_UI = '/dc_includes/jquery-ui/jquery-ui.min.js';
var WEBPATH_IMAGES    = '/dc_images/';

function dcLoadJQueryUI(plugins, callback){

	if ( $.isArray(plugins) ){
	
		// Loop over each value in the array.
		$.each(plugins,
			function( index, plugin ){
		
				plugins[index] = dcJQueryUIPath(plugin);
			}
		);		
	}else{
	
		plugins = dcJQueryUIPath(plugins);		
	}
	
	$.requireScript(plugins,callback);
}

function dcJQueryUIPath(plugin){

	if ( plugin == 'ui' ){
	
		plugin = JQUERY_UI+'jquery-ui'+JQUERY_UI_EXT;
	}else{
	
		plugin = JQUERY_UI+plugin+JQUERY_UI_EXT;
	}
	
	return plugin;
}

// Place the user's cursor in a particular form field
//
// AK24122008
function dcFirstFormField(field_name) { 

	var field = document.getElementsByName(field_name)[0]; 
	field.focus(); 
}

// Open popup window in centre of page
//
// AK24122008
function dcPopup(windowUrl, return_field,windowHeight, windowWidth, windowName) {

    if ( windowHeight == null ){

    	windowHeight = 300; 
    }

    if ( windowWidth == null ){

    	windowWidth = 600; 
    }

    if ( windowName == null ){

    	windowName = 'popup';
    }

    var ifield = document.getElementById(return_field);

    var centerWidth = (window.screen.width - windowWidth) / 2; var centerHeight = (window.screen.height - windowHeight) / 2;

    newWindow = window.open(windowUrl, windowName, 'resizable=0,width=' + windowWidth + ',height=' + windowHeight + ',left=' + centerWidth + ',top=' + centerHeight);

    newWindow.focus();

    newWindow.ifield = ifield; window.ifield = ifield;

    return newWindow.name;
}

// Show an alert to the user
function snUserNotification(message,options){
	
	if ( typeof options == 'undefined' ){

		options = {};
	}

	// Default onClose function
	if ( typeof options.onClose == 'undefined' ){

		options.onClose = function(){ return true; };
	}
	
	// Image URL
	if ( typeof options.image_url !== 'undefined' ){
		
		message = "<img src='"+options.image_url+"' style='max-width: 50px; margin-right: 6px; float: left;' />"+message;
	}
	
	message = "<div class='dc-user-notification'>"+message+"<div style='clear:both;'></div></div>";
	
	// Wrap in A tag
	if ( typeof options.url !== 'undefined' ){
		
		message = "<a href='"+options.url+"'>"+message+"</a>";
	}
	
	var noty_options = {text: message,
	   layout: "bottomRight",
	   theme: "goe",
	   type: "success",
	   closeWith: ['click', 'button']
	   };

	return new Noty(noty_options).on('onClose', options.onClose).show();
}

// 
function snDialog(windowUrl,windowTitle,options,callback){
	
	if ( windowUrl == 'UserPicker' ){
		
		if (typeof options == 'undefined' ){			
			
			alert('Expecting either return_id or return_function in options');
		}
		
		windowUrl = "/admin/Users/_pick/?mode=picker";
		
		if ( typeof options.return_id !== 'undefined' ){
			
			windowUrl = windowUrl.concat('&return_element_id='+options.return_id);
			
		} else if ( typeof options.return_function !== 'undefined' ){
			
			windowUrl = windowUrl.concat('&return_function='+options.return_function);
			
		} else {
			
			alert('Expecting either return_id or return_function in options');
		}
		
		if  ( typeof options.filter_groups !== 'undefined' ){
		
			windowUrl = windowUrl.concat('&filter_groups='+encodeURI(options.filter_groups));
		}
		
		// User Picker Options
		options.width = 600;
		options.height = 500;
		options.modal = true;
	}
	
	$.requireScript(WEBPATH_JQUERY_UI,function(){ 
	
		var d = dcDialog(windowUrl,windowTitle,null,null,options);
		
		if(typeof callback == 'function'){
			
			callback(d);
		}
	});
}

// Launch a JqueryUI dialog popup
function dcDialog(windowUrl, windowTitle, windowHeight, windowWidth, options) {

		

		if(typeof $().dialog != 'function'){

			alert('Please install jquery_ui::dialog before calling dcDialog');
			return false;
		}
		
		// Dialog ID
		var dialog_id = 'dialog';
		if (typeof options !== 'undefined' ){
			
			if ( typeof options.dialog_id !== 'undefined') {
		  
				dialog_id = options.dialog_id;
			}
		}
		
		var settings = {resizable: true,
		     
		     hide: 'slow',
			 autoOpen: false,
			 title: windowTitle || 'Dialog',
			 closeOnEscape: true,
		     /* bgiframe: true, */
			 close: function() { 
				
				if ( typeof $(this).dialog == 'function' ){
			
					$(this).dialog("destroy").remove();
				}
			 }
		};
		
		// Options
		if (typeof options !== 'undefined' ){

			if ( typeof options.resize !== 'undefined') {
		  
				settings.resize = options.resize;
				settings.position = { my: 'top', at: 'top+80' };	
			}
			
			if ( typeof options.modal !== 'undefined') {
		  
				settings.modal = options.modal;
			}
			
			if ( typeof options.width !== 'undefined') {
		  
				settings.width = options.width;
			}
			
			if ( typeof options.height !== 'undefined') {
		  
				settings.height = options.height;
			}
		}
		
		// Window Width
		if ( windowWidth != null ){
		
			settings.width = windowWidth;
		}
		
		// Window Height
		if ( windowHeight != null ){
		
			settings.height = windowHeight;
		}
	
		

		var dialog = $("<div id='"+dialog_id+"'>");
	
		
		//dialog.parent().css('z-index',400000);
		
		 // $(".ui-front").css("z-index",999999);
		
		dialog.dialog(settings);

		dialog.html("<div align='center'><blink>Loading...</blink></div>");

		dialog.load(windowUrl,function(){
			
			dialog.dialog('open');
			
			dialog.dialog("option","position","center");
			
			// $(".ui-front").css("z-index",999999);
			
			// dialog.parent().css('z-index',700000);
		});

		return dialog;
	//});
}

// Turn on wait cursor and disable buttons
function dcWaitOn(){

	// Turn on Waiting Cursor
	$('body').css('cursor','wait');

	// Disable button
	$('.submit_button').attr("disabled", "disabled");   
}

// Turn off wait cursor and enable buttons
function dcWaitOff(){

	// Turn off Waiting Cursor
	setTimeout(function() {
		$('body').css('cursor', 'default');
	}, 0); 

	// Enable button
	$('.submit_button').removeAttr("disabled");	   
}     
