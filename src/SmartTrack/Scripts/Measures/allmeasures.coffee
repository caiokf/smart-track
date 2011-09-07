$ -> 
	$('#save-all-measures').click ->
		measurements = []
		measuresTable = $('#measures-table tr')
		measuresTable.each((m) -> 
			measurement = {}
			measurement.Name = $(m).find('.measure-table-col-name').text()
			measurement.Value = $(m).find('.measure-table-col-value input').text()
			measurements.push(measurement)
		)
		inputModel = {}
		inputModel.Measurements = measurements
		jqxhr = $.post(saveMeasuresUrl, inputModel, () -> alert("success"))
			.success(() -> alert("second success") )
			.error(() -> alert("error") )
			.complete(() -> alert("complete") )
	