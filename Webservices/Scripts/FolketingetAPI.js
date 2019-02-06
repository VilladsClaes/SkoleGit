$(document).ready(function () {


    var wsurl = "https://oda.ft.dk/api/Sag?$expand=Sagskategori,Sagstrin";

    KaldWebservice()
    alert("hej")
    function KaldWebservice() {

        $.ajax({
            type: "GET",
            dataType: "jsonp",
            contentType: "text/plain",
            crossDomain: true,
            url: wsurl,
            success: function (data) {
                udskrivData(data);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                alert("FEJL: " + jqXHR + textStatus + errorThrown)

            }
        })
    }

    function udskrivData(data) {



        console.log(data)

    };




});



