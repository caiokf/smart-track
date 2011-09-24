// SmartTrack.Scripts.js
(function($){
Type.registerNamespace('SmartTrack.Scripts');window.$create_JsonResponse=function(){return {};}
SmartTrack.Scripts.SetupAjaxValidation=function(){}
SmartTrack.Scripts.AjaxFormValidation=function(f){this.$0=$(f);}
SmartTrack.Scripts.AjaxFormValidation.prototype={$0:null,setupValidation:function(){var $0={};$0.errorElement='span';$0.errorPlacement=function($p1_0,$p1_1){
$p1_0.prependTo($p1_1.parent());};$0.submitHandler=ss.Delegate.create(this,this.$1);this.$0.validate($0);},$1:function($p0){var $0={};$0.url=this.$0.attr('action');$0.type='POST';$0.dataType='json';$0.success=ss.Delegate.create(this,this.$2);$0.error=ss.Delegate.create(this,function(){
this.$0.submit();});this.$0.ajaxSubmit($0);return true;},$2:function($p0){if($p0.Success){this.$0.submit();}else{this.$3($p0);this.$4($p0);}},$3:function($p0){this.$0.find('.ui-error-summary').fadeIn('slow',ss.Delegate.create(this,function(){
var $1_0=this.$0.find('.ui-state-error > ul').html('');$.each($p0.Errors,function($p2_0,$p2_1){
$1_0.append('<li>'+$p2_1.message+'</li>');});}));},$4:function($p0){this.$0.find('.ui-error-field').each(function($p1_0,$p1_1){
var $1_0=$($p1_1);$1_0.fadeIn('slow',function(){
var $2_0=$('#'+$1_0.attr('id').replaceAll('validation-for-',''));var $2_1=$1_0.find('ul').html('');var $2_2=_Linq.$0($p0.Errors).$4(function($p3_0){
return ($p3_0).field===$2_0.attr('name');}).$9();$.each($2_2,function($p3_0,$p3_1){
$2_1.append('<li>'+($p3_1).message+'</li>');});});});}}
SmartTrack.Scripts.Watermark=function(){}
SmartTrack.Scripts.Watermark.spanBlur=function(span){if(!span.siblings('input').val().trim().length){span.show().css('z-index','100');}}
SmartTrack.Scripts.Watermark.spanFocus=function(span){span.hide();span.siblings('input').focus();}
SmartTrack.Scripts.Watermark.inputBlur=function(input){if(!input.val().trim().length){input.siblings('span').show().css('z-index','100');}}
SmartTrack.Scripts.Watermark.inputFocus=function(input){input.siblings('span').hide();}
Type.registerNamespace('SmartTrack.Scripts.Libraries');_Linq=function(){}
_Linq.$0=function($p0){var $0=new _LinqObject($p0);return $0;}
_LinqObject=function(list){this.$0=Array.toArray(list);}
_LinqObject.prototype={$0:null,$1:null,$2:null,$3:null,$4:function($p0){this.$2=$p0;return this;},$5:function($p0){this.$3=$p0;return this;},$6:function($p0){if($p0!=null){this.$1=$p0;}return this;},$7:function(){return this.$9().length;},$8:function(){var $0=[];var $enum1=ss.IEnumerator.getEnumerator(this.$0);while($enum1.moveNext()){var $1=$enum1.current;if(this.$2==null||this.$2($1)){$0.add($1);if(this.$3==null){break;}}}if(this.$3!=null){$0.sort(this.$3);}if(this.$1!=null){for(var $2=0;$2<$0.length;$2++){$0[$2]=this.$1($0[$2]);}}return ($0.length>0)?$0[0]:null;},$9:function(){return this.$A();},$A:function(){var $0=[];var $enum1=ss.IEnumerator.getEnumerator(this.$0);while($enum1.moveNext()){var $1=$enum1.current;if(this.$2==null||this.$2($1)){$0.add($1);}}if(this.$3!=null){$0.sort(this.$3);}if(this.$1!=null){for(var $2=0;$2<$0.length;$2++){$0[$2]=this.$1($0[$2]);}}return $0;}}
SmartTrack.Scripts.SetupAjaxValidation.registerClass('SmartTrack.Scripts.SetupAjaxValidation');SmartTrack.Scripts.AjaxFormValidation.registerClass('SmartTrack.Scripts.AjaxFormValidation');SmartTrack.Scripts.Watermark.registerClass('SmartTrack.Scripts.Watermark');_Linq.registerClass('_Linq');_LinqObject.registerClass('_LinqObject');(function(){$(function(){
$('.form-ajax-validate-and-submit').each(function($p2_0,$p2_1){
new SmartTrack.Scripts.AjaxFormValidation($p2_1).setupValidation();});});})();
})(jQuery);// This script was generated using Script# v0.7.3.0
