


function luomutila() {

    //this first part fills first div to show where location is but also fills second div for directions
    //end location ( koskenkorvan tila )
    var center1 =  new google.maps.LatLng(61.227446, 21.757453);
    var mapCanvas1 = document.getElementById("googleMaps");
    var mapOptions1 = { center: center1, zoom: 9 };
    var map1 = new google.maps.Map(mapCanvas1, mapOptions1);
    var marker1 = new google.maps.Marker({ position: center1 });
        
    marker1.setMap(map1);


    /*
        First part calculates your location (might ask you to enable location from browser)
        after calculation is done it will take it as starting point and first part "center1" as endpoint
    */
    var mapCanvas2 = document.getElementById("googleLocation");
    var mapOptions2 = { center: center2, zoom: 9 };
    var map2 = new google.maps.Map(mapCanvas2, mapOptions2);

    var center2;

    infoWindow = new google.maps.InfoWindow;

    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {
            center2 = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);

            infoWindow.setPosition(center2);
            infoWindow.setContent('Paikannettu');
            map2.setCenter(center2);
        }, function () {
            handleLocationError(true, infoWindow, map.getCenter());
        });
    } else {
        handleLocationError(false, infoWindow, map.getCenter());
    }
    
    //service for routes
    var directionService = new google.maps.DirectionsService;
    var directionDisplay = new google.maps.DirectionsRenderer;

    directionDisplay.setMap(map2);

    window.onload = function () {
        calculateAndDisplayRoute(directionService, directionDisplay)
    };

    //calculate and create route
    function calculateAndDisplayRoute(directionService, directionDisplay) {
        directionService.route({
            origin: center2,
            destination: center1,
            travelMode: 'DRIVING'
        }, function (response, status) {
            if (status === 'OK') {
                directionDisplay.setDirections(response);
            } else {
                windows.alert('Ei onnistunut ' + response);
            }

        });
    }

}