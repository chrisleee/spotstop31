
$(document).ready(function(){/* google maps -----------------------------------------------------*/
google.maps.event.addDomListener(window, 'load', initialize);
//$("#datetimePicker").datetimepicker();
$("#range").on("input", function () {
    updateRangeOutput();
});
$("#submit").click(function(){
   /* var latLong = $("#latLong").val();
    var arraylatLong = latLong.split(" ");
    changeMap(arraylatLong[0],arraylatLong[1]);
    getRandomPoints(arraylatLong[0],arraylatLong[1],$("#range").val()); */
    console.log("click");
    var address = $("#latLong").val();
    addressToLanLat(address);
  });


function initialize() {

  /* position Austin */
    var latlng = new google.maps.LatLng(30.2671, -97.7430);

  var mapOptions = {
    center: latlng,
    scrollWheel: false,
    zoom: 12
  };
  
  

  //var marker = new google.maps.Marker({
  //  position: latlng,
  //  url: '/',
  //  animation: google.maps.Animation.DROP,
    
  //});
  
  var map = new google.maps.Map(document.getElementById("blah"), mapOptions);
  //marker.setMap(map);

};
function changeMap(lat,lng) {

  var latlng = new google.maps.LatLng(lat, lng);

  var mapOptions = {
    center: latlng,
    scrollWheel: false,
    zoom: 16
  };
  
  
  var marker = new google.maps.Marker({
    position: latlng,
    url: '/',
    animation: google.maps.Animation.DROP,
 
  });
  

  map = new google.maps.Map(document.getElementById("blah"), mapOptions);
  marker.setMap(map);
  centerPoint = marker;
  listOfMarkers[0] = centerPoint;

};
var map;
var centerPoint;
function getRandomPoints(lat, lng, radius, rate) {
$.ajax({
   url: "http://spotstop31.azurewebsites.net/home/newsearch?myLat="+lat+"&myLong="+lng+"&radius="+radius + "&rate="+rate,
 
})
  .done(function( data ) {
      console.log(data);
      if (data.length == 0) {
          var div = $("<div>").addClass("panel panel-default our-panel").text("Oops! We couldn't find any nearby spots. Consider changing your price range.");
          $("#ParentList").append(div);
      }
      else {
          placeMarkers(data);
          $("#ParentList").empty();
          data = mergeSortDistance(data);
          addElements(data);
      }
   

  });
}

var listOfMarkers = [];
function placeMarkers(points) {
  
    var geo = new google.maps.Geocoder;
    
    for (var i = 0; i < points.length; i++) {
    var lat = points[i].latitude;
    var lng = points[i].longitude;
    var latlng = new google.maps.LatLng(lat,lng);
    var image = {
        url: "../Content/parkicon.png",

    }
    
    marker2 = new google.maps.Marker({
      position: latlng,
      url: '/',
      animation: google.maps.Animation.DROP,
      icon: image
    });
    
    marker2.setMap(map);
    var infowindow = new google.maps.InfoWindow({
        content: "holding..."
    });
    marker2.addListener('click', function () {
        var geo = new google.maps.Geocoder;
        geo.geocode({ 'location': this.position }, function (results, status) {
            if (status === google.maps.GeocoderStatus.OK) {

                infowindow.setContent(results[0].formatted_address);
               
            }
            
        })
        infowindow.open(map, this);
    });
    listOfMarkers[i+1] = marker2;
    
   
  }
  
    var bounds = new google.maps.LatLngBounds();
    for(i=0;i<listOfMarkers.length;i++) {
        bounds.extend(listOfMarkers[i].getPosition());
    }

    map.fitBounds(bounds);
  
};

    //Merge
function mergeDistance(left, right){
    var result  = [],
        il      = 0,
        ir      = 0;

    while (il < left.length && ir < right.length){
        if (left[il].distance < right[ir].distance){
            result.push(left[il++]);
        } else {
            result.push(right[ir++]);
        }
    }

    return result.concat(left.slice(il)).concat(right.slice(ir));
}

    //Sort
function mergeSortDistance(items){

    // Terminal case: 0 or 1 item arrays don't need sorting
    if (items.length < 2) {
        return items;
    }

    var middle = Math.floor(items.length / 2),
        left    = items.slice(0, middle),
        right   = items.slice(middle);

    return mergeDistance(mergeSortDistance(left), mergeSortDistance(right));
}

    //Add Elements to Side List
function addElements(data) {
    console.log("hi");
    
    for (var i = 0; i < data.length; i++) {
        var lat = data[i].latitude;
        var lng = data[i].longitude;
        var spots = data[i].amountOfSpots;
        var latLng = new google.maps.LatLng(lat, lng);
        var div = $("<div>").addClass("panel panel-default our-panel").attr("id", i);
        var thereString;
        var spotsString;
        if (spots === 1) {
            thereString = "There is ";
            spotsString = " spot ";
        }
        else {
            thereString = "There are ";
            spotsString = " spots ";
        }
        div.html("<h3> $" + data[i].rate.toFixed(2) + ":  Located " + data[i].distance + " miles away</h5> <p> " + thereString + spots + spotsString + "available at this location. </p>");
        $("#ParentList").append(div);
        div.data("lat", lat);
        div.data("lng", lng);
        div.click(function () {
            var latLng = ($(this).data(lat), $(this).data(lng));
            var lat = latLng.lat;
            map.setCenter(latLng);
            google.maps.event.trigger(latLng, 'click');
            map.setZoom(17);
        })

            }
    }
    

function addressToLanLat(address){
    var geo = new google.maps.Geocoder;
     geo.geocode({'address': address}, function(results, status) {
         if (status === google.maps.GeocoderStatus.OK) {
             var latLong = results[0].geometry.location;
             changeMap(latLong.lat(), latLong.lng());
             getRandomPoints(latLong.lat(), latLong.lng(), 5, $("#rate").val());
         }
            });
};
/* end google maps -----------------------------------------------------*/
});

function updateRangeOutput() {
  $("#rangeOutput").text($("#range").val());
}