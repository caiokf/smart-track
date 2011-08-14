(function() {
  var Watermark;
  Watermark = (function() {
    function Watermark() {}
    Watermark.prototype.blur = function(inputbox, watermarkText) {
      if (inputbox.val().trim().length === 0 || inputbox.val().trim() === watermarkText) {
        return inputbox.val(' ' + watermarkText + ' ').addClass("watermark").removeAttr('type').prop('type', 'text');
      }
    };
    Watermark.prototype.focus = function(inputbox, watermarkText) {
      if (inputbox.val() === ' ' + watermarkText + ' ') {
        return inputbox.val('').removeClass("watermark").removeAttr('type').prop('type', 'password');
      }
    };
    return Watermark;
  })();
  window.Watermark = new Watermark();
  $(function() {
    return $('.watermark').blur();
  });
}).call(this);
