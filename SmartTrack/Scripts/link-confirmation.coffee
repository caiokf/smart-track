linkConfirmation = (confirmationMessage, linkToUrl) ->
	jConfirm(confirmationMessage, 'Confirm', (success) ->
		if (!success) 
			return false;
		$.ajax({
			url: linkToUrl,
			type: 'POST',
			dataType: 'json',
			success: (response) -> alert(response)
		})
	);

window.linkConfirmation = linkConfirmation