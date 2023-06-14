var map = new google.maps.Map(document.getElementById('map'), {
    center: { lat: 42.46154751282092, lng: 21.466844433347905 },
    zoom: 8
});

// Add a click event listener to the map
map.addListener('click', function (event) {
    var latitudeInput = document.getElementById('lat');
    var longitudeInput = document.getElementById('long');

    // Update the latitude and longitude input fields with the clicked location
    latitudeInput.value = event.latLng.lat().toFixed(6);
    longitudeInput.value = event.latLng.lng().toFixed(6);
});