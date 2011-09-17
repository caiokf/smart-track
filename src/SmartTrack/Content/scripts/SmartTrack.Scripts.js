// SmartTrack.Scripts.js
(function($){
Type.registerNamespace('SmartTrack.Scripts');SmartTrack.Scripts.AjaxFormValidation=function(){}
SmartTrack.Scripts.Watermark=function(){}
SmartTrack.Scripts.Watermark.spanBlur=function(span){if(!span.siblings('input').val().trim().length){span.show().css('z-index','100');}}
SmartTrack.Scripts.Watermark.spanFocus=function(span){span.hide();span.siblings('input').focus();}
SmartTrack.Scripts.Watermark.inputBlur=function(input){if(!input.val().trim().length){input.siblings('span').show().css('z-index','100');}}
SmartTrack.Scripts.Watermark.inputFocus=function(input){input.siblings('span').hide();}
SmartTrack.Scripts.AjaxFormValidation.registerClass('SmartTrack.Scripts.AjaxFormValidation');SmartTrack.Scripts.Watermark.registerClass('SmartTrack.Scripts.Watermark');})(jQuery);// This script was generated using Script# v0.7.3.0
