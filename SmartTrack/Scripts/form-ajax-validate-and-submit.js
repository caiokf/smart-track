$(function () {
    $('.form-ajax-validate-and-submit').validate({
        errorElement: 'span',
        errorPlacement: function (error, element) {
            error.prependTo(element.parent());
        },
        submitHandler: function (form) {
            $(form).ajaxSubmit({
                url: $(form).attr('action'),
                type: 'POST',
                dataType: 'json',
                success: function (response) {
                    if (response.Success) {
                        form.submit();
                    }
                    else {
                        $(form).find('.ui-error').fadeIn('slow', function () {
                            var list = $(form).find('.ui-state-error > ul');
                            list.html('');
                            for (var i = 0; i < response.Errors.length; i++) {
                                // TODO -- use the field/message info
                                list.append('<li>' + response.Errors[i].message + '</li>');
                            }
                        });
                    }
                },
                error: function (responseObj, textError, errorCode) {
                    form.submit();
                }
            });
            return true;
        }
    });
});