
$(document).ready(function(){/* google maps -----------------------------------------------------*/
google.maps.event.addDomListener(window, 'load', initialize);

$("#submit").click(function(){
    console.log("clicked");
    var latLong = $("#latLong").val();
    var arraylatLong = latLong.split(" ");
    changeMap(arraylatLong[0],arraylatLong[1])
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