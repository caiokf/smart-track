(function() {
  var ValidatioErrorHandler;
  ValidatioErrorHandler = (function() {
    function ValidatioErrorHandler() {}
    ValidatioErrorHandler.prototype.addErrorsToUi = function(container, errors) {
      this.addErrorsToSummary(container, errors);
      return this.addErrorsToFields(container, errors);
    };
    ValidatioErrorHandler.prototype.addErrorsToSummary = function(container, errors) {
      return $(container).find('.ui-error-summary').fadeIn('slow', function() {
        var list;
        list = $(container).find('.ui-state-error > ul').html('');
        return errors.forEach(function(x) {
          return list.append('<li>' + x.message + '</li>');
        });
      });
    };
    ValidatioErrorHandler.prototype.addErrorsToFields = function(container, errors) {
      return $(container).find('.ui-error-field').fadeIn('slow', function() {
        var list, validating;
        validating = $('#' + $(this).attr('id').replace('validation-for-', ''));
        list = $(this).find('ul').html('');
        return errors.filter(function(x) {
          return x.field === validating.attr('name');
        }).forEach(function(x) {
          return list.append('<li>' + x.message + '</li>');
        });
      });
    };
    return ValidatioErrorHandler;
  })();
  window.ValidatioErrorHandler = new ValidatioErrorHandler();
  $(function() {
    return $('.form-ajax-validate-and-submit').validate({
      errorElement: 'span',
      errorPlacement: function(error, element) {
        return error.prependTo(element.parent());
      },
      submitHandler: function(form) {
        $(form).ajaxSubmit({
          url: $(form).attr('action'),
          type: 'POST',
          dataType: 'json',
          success: function(response) {
            if (response.Success) {
              return form.submit();
            } else {
              return new ValidatioErrorHandler().addErrorsToUi(form, response.Errors);
            }
          },
          error: function() {
            return form.submit();
          }
        });
        return true;
      }
    });
  });
}).call(this);
