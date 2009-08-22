function read() {
}
function init_dialog() {
	jQuery('#progress-dialog').dialog( {
		"modal" : true,
		"autoOpen" : false
	});

}
function activeX_transfer_basic_properties(activeXObject) {
	jQuery('#id').val(activeXObject.icno);
	jQuery('#name').val(activeXObject.originalName);
	jQuery('#religion').val(activeXObject.religion);
	jQuery('#race').val(activeXObject.race);
	jQuery('#dob').val(activeXObject.birthdate);
	jQuery('#gender').val(activeXObject.gender);
	jQuery('#nationality').val(activeXObject.citizenship);
	var address = '';
	address = address + activeXObject.address1;
	address = address + '\n' + activeXObject.address2;
	address = address + '\n' + activeXObject.address3;
	address = address + '\n' + activeXObject.postcode + ' '
			+ activeXObject.city;
	address = address + '\n' + activeXObject.state;
	jQuery('#address').val(address);

};

function activeX_read_image(activeXObject) {
	jQuery('#imageId').val(jQuery('#id').val());
	var i = 0;
	var elementSelector = 'imageBlock' + i;
	while (jQuery('#' + elementSelector).size() == 1) {
		var statusText = "Reading image segment [" + i + "]";
		jQuery('#progress-status').text(statusText);
		var content = activeXObject.getImageContent(i);
		jQuery('#' + elementSelector).val(content);
		i++;
		elementSelector = 'imageBlock' + i;
	}
	alert(' not found ' + elementSelector);
	return;
};

function show_image(responseText, statusText) {
	var getAction = jQuery('form#pullImageAction').attr('action');
	var imageLocation = getAction + '?' + 'id=' + jQuery('#imageId').val();
	jQuery('#photo').attr('src', imageLocation);
	alert('ok');
};

function activeX_image_submit() {
	var options = {
		"cache" : false,
		"success" : show_image
	};
	alert('before submit2');
	jQuery('form#pushImageAction').ajaxSubmit(options);
};

function readUsingService() {
	var activeXId = "Tabuk.MyKad.JpnReaderService";
	try {
		jQuery('#progress-status').text("Unable to create ActiveX object");
		activeXObject = new ActiveXObject(activeXId);
		jQuery('#progress-status').text("ActiveX object created...");
		var result;
		result = activeXObject.init();
		if (result != 0) {
			jQuery('#progress-status').text("Reader initiliazation problem...");
			activeXObject.cleanUp();
			return false;
		}
		jQuery('#progress-status').text("Reading basic info...");
		result = activeXObject.readTextInfo();
		if (result != true) {
			jQuery('#progress-status').text("Reading basic info problem...");
			activeXObject.cleanUp();
			return false;
		}
		activeX_transfer_basic_properties(activeXObject);
		activeX_read_image(activeXObject);
		jQuery('#progress-status').text("Done reading image...");
		activeX_image_submit();
		activeXObject.cleanUp();
		return true;
	} catch (e) {
		alert(activeXId + ":" + e);
		jQuery('a.ui-dialog-titlebar-close').show();
	}
	return false;
}

function hook_up_read() {
	jQuery('#button-read').click(function() {
		jQuery('a.ui-dialog-titlebar-close').hide();
		jQuery("#progress-bar").progressbar( {
			value : 0
		});
		jQuery('#progress-dialog').dialog('open');
		jQuery('#progress-status').text("Reading text info...");
		if (readUsingService()) {
			// jQuery('#progress-dialog').dialog('close');
		}

	});
}
function init_page() {
	init_dialog();
	hook_up_read();
}
jQuery(document).ready(init_page);
