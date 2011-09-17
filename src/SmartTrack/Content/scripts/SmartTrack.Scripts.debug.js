//! SmartTrack.Scripts.debug.js
//

(function($) {

Type.registerNamespace('SmartTrack.Scripts');

////////////////////////////////////////////////////////////////////////////////
// SmartTrack.Scripts.AjaxFormValidation

SmartTrack.Scripts.AjaxFormValidation = function SmartTrack_Scripts_AjaxFormValidation() {
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


SmartTrack.Scripts.AjaxFormValidation.registerClass('SmartTrack.Scripts.AjaxFormValidation');
SmartTrack.Scripts.Watermark.registerClass('SmartTrack.Scripts.Watermark');
})(jQuery);

//! This script was generated using Script# v0.7.3.0
