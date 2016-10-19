<!DOCTYPE html>
<html>
  <head>

    <style>
      #map {
		min-width: 200px;
		min-height: 200px;
		height: 400px;
		width: 100%;
      }
    </style>
  </head>
  <body>
	<div id="map"></div>
    <script>
		function initMap() {
		  var myLatlng = {lat: 49.354180, lng: 9.150733};

		  var map = new google.maps.Map(document.getElementById('map'), {
			zoom: 18,
			center: myLatlng
		  });

		  var marker = new google.maps.Marker({
			position: myLatlng,
			map: map,
			title: 'Click to zoom'
		  });

		  map.addListener('center_changed', function() {
			// 3 seconds after the center of the map has changed, pan back to the
			// marker.
			window.setTimeout(function() {
			  map.panTo(marker.getPosition());
			}, 3000);
		  });

		  marker.addListener('click', function() {
			map.setZoom(8);
			map.setCenter(marker.getPosition());
		  });
		}
    </script>
    <script async defer
		src="https://maps.googleapis.com/maps/api/js?key=AIzaSyB4DopIB-rH3QOK5lclz-sZj86Eis1YZWI&callback=initMap">
    </script>
  </body>
</html>