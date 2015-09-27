
$(document).ready(function(){/* google maps -----------------------------------------------------*/
google.maps.event.addDomListener(window, 'load', initialize);

$("#submit").click(function(){
    console.log("clicked");
    var latLong = $("#latLong").val();
    var arraylatLong = latLong.split(" ");
<<<<<<< HEAD
    changeMap(arraylatLong[0],arraylatLong[1]);
    /*placeMarkers(makeArray()); */
=======
    changeMap(arraylatLong[0],arraylatLong[1])
>>>>>>> parent of 16928e7... place markers
  });

function initialize() {

  /* position Amsterdam */
  var latlng = new google.maps.LatLng(30.2671, -97.7430);

  var mapOptions = {
    center: latlng,
    scrollWheel: false,
    zoom: 12
  };
  
  var marker = new google.maps.Marker({
    position: latlng,
    url: '/',
    animation: google.maps.Animation.DROP
  });
  
  var map = new google.maps.Map(document.getElementById("map-canvas"), mapOptions);
  marker.setMap(map);

};
function changeMap(lat,lng) {

  /* position Amsterdam */
  var latlng = new google.maps.LatLng(lat, lng);

  var mapOptions = {
    center: latlng,
    scrollWheel: false,
    zoom: 12
  };
  
  var marker = new google.maps.Marker({
    position: latlng,
    url: '/',
    animation: google.maps.Animation.DROP
  });
  
  var map = new google.maps.Map(document.getElementById("map-canvas"), mapOptions);
  marker.setMap(map);

};
/* end google maps -----------------------------------------------------*/
});
<<<<<<< HEAD
/*
function makeArray() {
  var points = [[30.2881, -97.7473]];
  return points;
}

function placeMarkers(points) {
  for (var i = 0;i<points.length;i++){
    var lat = points[i][0];
    var lng = points[i][1];
    var latlng = new google.maps.LatLng(lat, lng);
    var marker2 = new google.maps.Marker({
    position: latlng,
    url: '/',
    animation: google.maps.Animation.DROP;
    
  });
    marker2.setMap(map);
  }
}
=======
>>>>>>> parent of 16928e7... place markers
