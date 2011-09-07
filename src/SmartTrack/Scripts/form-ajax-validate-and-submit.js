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
                        $(form).find('.ui-error-summary').fadeIn('slow', function () {
                            var list = $(form).find('.ui-state-error > ul').html('');
                            $(response.Errors).each(function () {
                                list.append('<li>' + this.message + '</li>');
                            });
                        });
                        $(form).find('.ui-error-field').fadeIn('slow', function () {
                            var validating = $('#' + $(this).attr('id').replace('validation-for-', ''));
                            var list = $(form).find('ul').html('');
                            $(response.Errors.filter(function (x) { return x.field === validating.attr('name'); })).each(function () {
                                list.append('<li>' + this.message + '</li>');
                            });
                        });
                    }
                },
                error: function () { form.submit(); }
            });
            return true;
        }
    });
});