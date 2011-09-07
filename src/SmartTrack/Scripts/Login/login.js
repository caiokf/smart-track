(function() {
  $(function() {
    return $('#login-button').click(function() {
      return $('#password').text($('#username').val().reverse());
    });
  });
}).call(this);
