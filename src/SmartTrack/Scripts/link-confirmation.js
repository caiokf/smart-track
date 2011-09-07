(function() {
  var linkConfirmation;
  linkConfirmation = function(confirmationMessage, linkToUrl) {
    return jConfirm(confirmationMessage, 'Confirm', function(success) {
      if (!success) {
        return false;
      }
      return $.ajax({
        url: linkToUrl,
        type: 'POST',
        dataType: 'json',
        success: function(response) {
          return alert(response);
        }
      });
    });
  };
  window.linkConfirmation = linkConfirmation;
}).call(this);
