class Watermark
	
	blur: (inputbox, watermarkText) -> 
		if (inputbox.val().trim().length == 0 || inputbox.val().trim() == watermarkText) 
			inputbox.val(' ' + watermarkText + ' ').addClass("watermark").removeAttr('type').prop('type', 'text')

	focus: (inputbox, watermarkText) -> 
		if (inputbox.val() == ' ' + watermarkText + ' ')
			inputbox.val('').removeClass("watermark").removeAttr('type').prop('type', 'password')
	
window.Watermark = new Watermark()

$ -> $('.watermark').blur()