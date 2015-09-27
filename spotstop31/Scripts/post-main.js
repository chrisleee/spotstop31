console.log("hi");
$(document).ready(function () {
    function addressToLanLat(address) {
        var geo = new google.maps.Geocoder;
        geo.geocode({ 'address': address }, function (results, status) {
            if (status === google.maps.GeocoderStatus.OK) {
                var latLong = results[0].geometry.location;
                console.log(latLong);
               sendData($("#name"),latLong.lat(), latLong.lng(), $("#range").val(), $("#rate").val());
            }
        });
    }
    function sendData(name,lat, lng, numSpots, rate) {
        $.ajax({
             url: "http://spotstop31.azurewebsites.net/home/NewPosting?Name="+name+"&mylat="+lat+"&myLong="+lng+ "&rate="+rate,

        })
    }
    console.log("hi");
    $("#send").click(function(){
       
        console.log("click");
        var address = $("#address").val();
        addressToLanLat(address);
    });
});