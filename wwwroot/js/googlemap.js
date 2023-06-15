var map = new google.maps.Map(document.getElementById('map'), {
    center: { lat: 42.46154751282092, lng: 21.466844433347905 },
    zoom: 8
});

var marker; // Reference to the marker

// Add a click event listener to the map
map.addListener('click', function (event) {
    var latitudeInput = document.getElementById('lat');
    var longitudeInput = document.getElementById('long');
    var addressInput = document.getElementById('Address')

    const clickedLatLng = event.latLng;

    const geocoder = new google.maps.Geocoder();
    geocoder.geocode({ location: clickedLatLng }, (results, status) => {
        if (status === 'OK') {
            if (results[0]) {
                // Retrieve the formatted address
                const address = results[0].formatted_address;

                // Update the address input field with the retrieved address
                addressInput.value = address;
            } else {
                console.log('No results found');
            }
        } else {
            console.log('Geocoder failed due to: ' + status);
        }
    });

    // Update the latitude and longitude input fields with the clicked location
    latitudeInput.value = event.latLng.lat().toFixed(6);
    longitudeInput.value = event.latLng.lng().toFixed(6);

    // Remove the previous marker if it exists
    if (marker) {
        marker.setMap(null);
    }

    // Create a new marker at the clicked location
    marker = new google.maps.Marker({
        position: clickedLatLng,
        map: map,
    });

    // Center the map on the clicked location
    map.setCenter(clickedLatLng);
});
