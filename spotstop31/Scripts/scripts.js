
$(document).ready(function(){/* google maps -----------------------------------------------------*/
google.maps.event.addDomListener(window, 'load', initialize);

$("#submit").click(function(){
    console.log("clicked");
    var latLong = $("#latLong").val();
    var arraylatLong = latLong.split(" ");
    changeMap(arraylatLong[0],arraylatLong[1]);
    placeMarkers([[30.288815, -97.747512],[30.292897, -97.726403]]);
    getRandomPoints();
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
  
  map = new google.maps.Map(document.getElementById("map-canvas"), mapOptions);
  marker.setMap(map);
  marker.setIcon('http://maps.google.com/mapfiles/ms/icons/green-dot.png');

};
var map;

function getRandomPoints() {
$.ajax({
  url: "http://spotstop31.azurewebsites.net/home/newsearch?myLat=0&myLong=0&radius=1",
  
})
  .done(function( data ) {
    if ( console && console.log ) {
      console.log("hi");
      console.log(data);
    }
    placeMarkers(data);
  });
}

function placeMarkers(points) {
  for (var i = 0;i<points.length;i++){
    var lat = points[i].latitude;
    var lng = points[i].longitude;
    var latlng = new google.maps.LatLng(lat,lng);
    var marker2 = new google.maps.Marker({
      position: latlng,
      url: '/',
      animation: google.maps.Animation.DROP
    });
    marker2.setMap(map);
  }
};
/* end google maps -----------------------------------------------------*/
});