<!DOCTYPE html>
<html>
	<head>
		<link rel="stylesheet" href="css/calendar.css" media="screen">
	</head>
	<body>	
		<div id="calendar"></div>

		<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js"></script>
		<script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
		<script>
			$('#calendar').datepicker({
				inline: true,
				firstDay: 1,
				showOtherMonths: true,
				dayNamesMin: ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat']
			});
		</script>
	</body>
</html>