$(document).ready(function () {

    alert("Jquery indlæst for at teste ajax");

    var wsurl = "https://swapi.co/api/planets/1/";

    var request = new XMLHttpRequest;

    $.ajax(
        {
            type: "GET",
            dataType: "json",
            url: wsurl,
            success: function (data) {
                udskrivData(data);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                alert("FEJL: " + jqXHR + textStatus + errorThrown)
            }
        })

    function udskrivData(data) {

        $("#ajaxresultat").html("Hilsen fra ajax" + data.name)


    };

    

})


