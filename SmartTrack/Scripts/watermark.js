(function() {
  var Watermark;
  Watermark = (function() {
    function Watermark() {}
    Watermark.prototype.blurSpan = function(span) {
      if (span.siblings('input').val().trim().length === 0) {
        return span.show().css('z-index', 100);
      }
    };
    Watermark.prototype.focusSpan = function(span) {
      span.hide();
      return span.siblings('input').focus();
    };
    Watermark.prototype.blurInput = function(input) {
      if (input.val().trim().length === 0) {
        return input.siblings('span').show().css('z-index', 100);
      }
    };
    Watermark.prototype.focusInput = function(input) {
      return input.siblings('span').hide();
    };
    return Watermark;
  })();
  window.Watermark = new Watermark();
  $(function() {
    return $('.watermark').blur();
  });
}).call(this);
