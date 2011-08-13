(function() {
  $(function() {
    return $('#save-all-measures').click(function() {
      var inputModel, jqxhr, measurements, measuresTable;
      measurements = [];
      measuresTable = $('#measures-table tr');
      measuresTable.each(function(m) {
        var measurement;
        measurement = {};
        measurement.Name = $(m).find('.measure-table-col-name').text();
        measurement.Value = $(m).find('.measure-table-col-value input').text();
        return measurements.push(measurement);
      });
      inputModel = {};
      inputModel.Measurements = measurements;
      return jqxhr = $.post(saveMeasuresUrl, inputModel, function() {
        return alert("success");
      }).success(function() {
        return alert("second success");
      }).error(function() {
        return alert("error");
      }).complete(function() {
        return alert("complete");
      });
    });
  });
}).call(this);
