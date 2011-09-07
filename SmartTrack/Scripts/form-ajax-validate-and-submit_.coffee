$ ->
	ajaxOptions = 
        url: $(form).attr('action')
        type: 'POST'
        dataType: 'json'
        success: (response) -> {
            if (response.Success) {
                form.submit();
            }
            else {
                $(form).find('.ui-error-summary').fadeIn('slow', () -> {
                    list = $(form).find('.ui-state-error > ul').html('');
                    response.Errors.forEach((x) -> list.append('<li>' + x.message + '</li>'));
                });
                $(form).find('.ui-error-field').fadeIn('slow', () -> {
                    validating = $('#' + $(this).attr('id').replace('validation-for-', ''));
                    list = $(form).find('ul').html('');
                    response.Errors
						.filter((x) -> x.field === validating.attr('name'))
						.forEach((x) -> list.append('<li>' + x.message + '</li>'));
                }
            }
        }
        error: () -> form.submit();

    $('.form-ajax-validate-and-submit').validate({
        errorElement: 'span'
        errorPlacement: (error, element) -> error.prependTo(element.parent())
        submitHandler: (form) -> 
            $(form).ajaxSubmit(ajaxOptions)
            return true
    });
