$ ->
	$('.form-ajax-validate-and-submit').validate({
        errorElement: 'span',
        errorPlacement: (error, element) -> error.prependTo(element.parent()),
        submitHandler: (form) -> 
            $(form).ajaxSubmit({
                url: $(form).attr('action'),
                type: 'POST',
                dataType: 'json',
                success: (response) -> 
                    if (response.Success) 
                        form.submit()
                    else
                        listErrorsOn(form)                 
                ,
                error: () -> form.submit()
            });
            return false;
    });

listErrorsOn = (form) ->
	$(form).find('.ui-error').fadeIn('slow', () -> 
		list = $(form).find('.ui-state-error > ul');
        for (i = 0; i < response.Errors.length; i++) {
            // TODO -- use the field/message info
            list.append('<li>' + response.Errors[i].message + '</li>');
        }
	);