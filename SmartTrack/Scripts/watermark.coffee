class Watermark
	
	blurSpan: (span) -> 
		if (span.siblings('input').val().trim().length == 0)
			span.show().css('z-index', 100);

	focusSpan: (span) -> 
		span.hide();
		span.siblings('input').focus();
	
	blurInput: (input) -> 
		if (input.val().trim().length == 0)
			input.siblings('span').show().css('z-index', 100);

	focusInput: (input) -> 
		input.siblings('span').hide();

window.Watermark = new Watermark()

$ -> $('.watermark').blur()