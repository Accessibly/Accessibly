// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function setMarkers(map, locations, markers) {
    var newMarkers = {};
    var ids = locations.map(loc => loc.id);
    for (var id in markers) {
        if (id in ids) {
            newMarkers[id] = markers[id];
        }
        else {
            markers[id].setMap(null);
        }
    }
    
    locations.forEach(function (marker) {
        if (!(marker.id in newMarkers)) {
            newMarkers[marker.id] = new google.maps.Marker({
                position: { lat: marker.coordinate.lat, lng: marker.coordinate.lng },
                map: map,
                icon: `https://maps.google.com/mapfiles/ms/icons/${marker.colour}-dot.png`,
                title: marker.name
            });
        }
    });

    //var cluster = new MarkerClusterer(map, newlocations)

    return newMarkers;
}

function toggleAdd() {
    var add = document.getElementById("addPane");
    $(add).toggle(600);
}