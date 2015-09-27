
$(document).ready(function () {
    function addressToLanLat(address) {
        var geo = new google.maps.Geocoder;
        geo.geocode({ 'address': address }, function (results, status) {
            if (status === google.maps.GeocoderStatus.OK) {
                var latLong = results[0].geometry.location;
                console.log(latLong);
               sendData($("#name".val()),latLong.lat(), latLong.lng(), $("#range").val(), $("#rate").val());
            }
        });
    }
    function sendData(name,lat, lng, numSpots, rate) {
        $.ajax({
            url: "http://spotstop31.azurewebsites.net/home/NewPosting?Name="+name+"&mylat="+lat+"&myLong="+lng+ "&rate="+rate,

        }).done(function() {
  
            confirm('Success! Your spot has been posted.');
        })
        }
   
    $("#send").click(function(){
       
       
        var address = $("#address").val();
        addressToLanLat(address);
    });
});