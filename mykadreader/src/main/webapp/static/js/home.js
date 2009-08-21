function read() {
}
function init_dialog() {
	$('#progress-dialog').dialog( {
		"modal" : true,
		"autoOpen" : false
	});

}

function readUsingService() {
	var activeXObject;
	var activeXId = "Tabuk.MyKad.JpnReaderService";
	try {
		$('#progress-status').text("Unable to create ActiveX object");
		activeXObject = new ActiveXObject(activeXId);
		$('#progress-status').text("ActiveX object created...");
		var result;
		result = activeXObject.init();
		if (result != 0) {
			$('#progress-status').text("Reader initiliazation problem...");
			activeXObject.cleanUp();
			return false;
		}
		$('#progress-status').text("Reading basic info...");
		result = activeXObject.readTextInfo();
		if (result != true) {
			$('#progress-status').text("Reading basic info problem...");
			activeXObject.cleanUp();
			return false;
		}
		$('#id').val(activeXObject.icno);
		$('#name').val(activeXObject.originalName);
		$('#religion').val(activeXObject.religion);
		activeXObject.cleanUp();
		return true;
	} catch (e) {
		alert(activeXId + ":" + e);
		$('a.ui-dialog-titlebar-close').show();
	}
	return false;
}

function hook_up_read() {
	$('#button-read').click(function() {
		$('a.ui-dialog-titlebar-close').hide();
		$("#progress-bar").progressbar( {
			value : 0
		});
		$('#progress-dialog').dialog('open');
		$('#progress-status').text("Reading text info...");
		if (readUsingService()) {
			$('#progress-dialog').dialog('close');
		}

	});
}
function init_page() {
	init_dialog();
	hook_up_read();
}
jQuery(document).ready(init_page);
