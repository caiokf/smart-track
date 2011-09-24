//! SmartTrack.Scripts.debug.js
//

(function($) {

Type.registerNamespace('SmartTrack.Scripts');

////////////////////////////////////////////////////////////////////////////////
// JsonResponse

window.$create_JsonResponse = function JsonResponse() { return {}; }


////////////////////////////////////////////////////////////////////////////////
// SmartTrack.Scripts.SetupAjaxValidation

SmartTrack.Scripts.SetupAjaxValidation = function SmartTrack_Scripts_SetupAjaxValidation() {
}


////////////////////////////////////////////////////////////////////////////////
// SmartTrack.Scripts.AjaxFormValidation

SmartTrack.Scripts.AjaxFormValidation = function SmartTrack_Scripts_AjaxFormValidation(f) {
    /// <param name="f" type="Object" domElement="true">
    /// </param>
    /// <field name="_form" type="jQueryObject">
    /// </field>
    this._form = $(f);
}
SmartTrack.Scripts.AjaxFormValidation.prototype = {
    _form: null,
    
    setupValidation: function SmartTrack_Scripts_AjaxFormValidation$setupValidation() {
        var options = {};
        options.errorElement = 'span';
        options.errorPlacement = function(error, element) {
            error.prependTo(element.parent());
        };
        options.submitHandler = ss.Delegate.create(this, this._submit);
        this._form.validate(options);
    },
    
    _submit: function SmartTrack_Scripts_AjaxFormValidation$_submit(formElement) {
        /// <param name="formElement" type="Object" domElement="true">
        /// </param>
        /// <returns type="Boolean"></returns>
        var ajaxSubmitOptions = {};
        ajaxSubmitOptions.url = this._form.attr('action');
        ajaxSubmitOptions.type = 'POST';
        ajaxSubmitOptions.dataType = 'json';
        ajaxSubmitOptions.success = ss.Delegate.create(this, this._showValidationErrors);
        ajaxSubmitOptions.error = ss.Delegate.create(this, function() {
            this._form.submit();
        });
        this._form.ajaxSubmit(ajaxSubmitOptions);
        return true;
    },
    
    _showValidationErrors: function SmartTrack_Scripts_AjaxFormValidation$_showValidationErrors(response) {
        /// <param name="response" type="JsonResponse">
        /// </param>
        if (response.Success) {
            this._form.submit();
        }
        else {
            this._showValidationSummary(response);
            this._showValidationFields(response);
        }
    },
    
    _showValidationSummary: function SmartTrack_Scripts_AjaxFormValidation$_showValidationSummary(response) {
        /// <param name="response" type="JsonResponse">
        /// </param>
        this._form.find('.ui-error-summary').fadeIn('slow', ss.Delegate.create(this, function() {
            var list = this._form.find('.ui-state-error > ul').html('');
            $.each(response.Errors, function(index, value) {
                list.append('<li>' + value.message + '</li>');
            });
        }));
    },
    
    _showValidationFields: function SmartTrack_Scripts_AjaxFormValidation$_showValidationFields(response) {
        /// <param name="response" type="JsonResponse">
        /// </param>
        this._form.find('.ui-error-field').each(function(i, e) {
            var field = $(e);
            field.fadeIn('slow', function() {
                var fieldToValidate = $('#' + field.attr('id').replaceAll('validation-for-', ''));
                var list = field.find('ul').html('');
                var errors = _linq.from(response.Errors).where(function(x) {
                    return (x).field === fieldToValidate.attr('name');
                }).toArray();
                $.each(errors, function(index, value) {
                    list.append('<li>' + (value).message + '</li>');
                });
            });
        });
    }
}


////////////////////////////////////////////////////////////////////////////////
// SmartTrack.Scripts.Watermark

SmartTrack.Scripts.Watermark = function SmartTrack_Scripts_Watermark() {
}
SmartTrack.Scripts.Watermark.spanBlur = function SmartTrack_Scripts_Watermark$spanBlur(span) {
    /// <param name="span" type="jQueryObject">
    /// </param>
    if (!span.siblings('input').val().trim().length) {
        span.show().css('z-index', '100');
    }
}
SmartTrack.Scripts.Watermark.spanFocus = function SmartTrack_Scripts_Watermark$spanFocus(span) {
    /// <param name="span" type="jQueryObject">
    /// </param>
    span.hide();
    span.siblings('input').focus();
}
SmartTrack.Scripts.Watermark.inputBlur = function SmartTrack_Scripts_Watermark$inputBlur(input) {
    /// <param name="input" type="jQueryObject">
    /// </param>
    if (!input.val().trim().length) {
        input.siblings('span').show().css('z-index', '100');
    }
}
SmartTrack.Scripts.Watermark.inputFocus = function SmartTrack_Scripts_Watermark$inputFocus(input) {
    /// <param name="input" type="jQueryObject">
    /// </param>
    input.siblings('span').hide();
}


Type.registerNamespace('SmartTrack.Scripts.Libraries');

////////////////////////////////////////////////////////////////////////////////
// _linq

_linq = function _linq() {
}
_linq.from = function _linq$from(list) {
    /// <param name="list" type="Object">
    /// </param>
    /// <returns type="_linqObject"></returns>
    var value = new _linqObject(list);
    return value;
}


////////////////////////////////////////////////////////////////////////////////
// _linqObject

_linqObject = function _linqObject(list) {
    /// <param name="list" type="Object">
    /// </param>
    /// <field name="_list" type="Array">
    /// </field>
    /// <field name="_select" type="Function">
    /// </field>
    /// <field name="_where" type="Function">
    /// </field>
    /// <field name="_orderby" type="Function">
    /// </field>
    this._list = Array.toArray(list);
}
_linqObject.prototype = {
    _list: null,
    _select: null,
    _where: null,
    _orderby: null,
    
    where: function _linqObject$where(where) {
        /// <param name="where" type="Function">
        /// </param>
        /// <returns type="_linqObject"></returns>
        this._where = where;
        return this;
    },
    
    orderBy: function _linqObject$orderBy(orderby) {
        /// <param name="orderby" type="Function">
        /// </param>
        /// <returns type="_linqObject"></returns>
        this._orderby = orderby;
        return this;
    },
    
    select: function _linqObject$select(select) {
        /// <param name="select" type="Function">
        /// </param>
        /// <returns type="_linqObject"></returns>
        if (select != null) {
            this._select = select;
        }
        return this;
    },
    
    count: function _linqObject$count() {
        /// <returns type="Number" integer="true"></returns>
        return this.toArray().length;
    },
    
    firstOrDefault: function _linqObject$firstOrDefault() {
        /// <returns type="Object"></returns>
        var tempList = [];
        var $enum1 = ss.IEnumerator.getEnumerator(this._list);
        while ($enum1.moveNext()) {
            var item = $enum1.current;
            if (this._where == null || this._where(item)) {
                tempList.add(item);
                if (this._orderby == null) {
                    break;
                }
            }
        }
        if (this._orderby != null) {
            tempList.sort(this._orderby);
        }
        if (this._select != null) {
            for (var i = 0; i < tempList.length; i++) {
                tempList[i] = this._select(tempList[i]);
            }
        }
        return (tempList.length > 0) ? tempList[0] : null;
    },
    
    toArray: function _linqObject$toArray() {
        /// <returns type="Array"></returns>
        return this.toList();
    },
    
    toList: function _linqObject$toList() {
        /// <returns type="Array"></returns>
        var value = [];
        var $enum1 = ss.IEnumerator.getEnumerator(this._list);
        while ($enum1.moveNext()) {
            var item = $enum1.current;
            if (this._where == null || this._where(item)) {
                value.add(item);
            }
        }
        if (this._orderby != null) {
            value.sort(this._orderby);
        }
        if (this._select != null) {
            for (var i = 0; i < value.length; i++) {
                value[i] = this._select(value[i]);
            }
        }
        return value;
    }
}


SmartTrack.Scripts.SetupAjaxValidation.registerClass('SmartTrack.Scripts.SetupAjaxValidation');
SmartTrack.Scripts.AjaxFormValidation.registerClass('SmartTrack.Scripts.AjaxFormValidation');
SmartTrack.Scripts.Watermark.registerClass('SmartTrack.Scripts.Watermark');
_linq.registerClass('_linq');
_linqObject.registerClass('_linqObject');
(function () {
    $(function() {
        $('.form-ajax-validate-and-submit').each(function(index, e) {
            new SmartTrack.Scripts.AjaxFormValidation(e).setupValidation();
        });
    });
})();
})(jQuery);

//! This script was generated using Script# v0.7.3.0
