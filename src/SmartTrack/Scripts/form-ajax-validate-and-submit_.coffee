class ValidatioErrorHandler	

	addErrorsToUi: (container, errors) -> {
		this.addErrorsToSummary(container, errors)
		this.addErrorsToFields(container, errors)
	}

	addErrorsToSummary: (container, errors) -> 
		$(container).find('.ui-error-summary').fadeIn('slow', () -> {
            list = $(container).find('.ui-state-error > ul').html('');
            errors.forEach((x) -> list.append('<li>' + x.message + '</li>'));
        });
	
	addErrorsToFields: (container, errors) -> 
		$(container).find('.ui-error-field').fadeIn('slow', () -> {
            validating = $('#' + $(this).attr('id').replace('validation-for-', ''));
            list = $(container).find('ul').html('');
            errors
				.filter((x) -> x.field === validating.attr('name'))
				.forEach((x) -> list.append('<li>' + x.message + '</li>'));
        }


window.ValidatioErrorHandler = new ValidatioErrorHandler()

$ ->
	$('.form-ajax-validate-and-submit').validate({
        errorElement: 'span'
        errorPlacement: (error, element) -> error.prependTo(element.parent())
        submitHandler: (form) -> 
            $(form).ajaxSubmit({
				url: $(form).attr('action')
				type: 'POST'
				dataType: 'json'
				success: (response) -> {
					if response.Success
						form.submit()
					else
						ValidatioErrorHandler.addErrorsToUi(response.Errors)
				}
				error: () -> form.submit();
			};
            return true
    });
    
