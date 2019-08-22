$(document).ready(function () {

    

    var wsurl = "http://api.openweathermap.org/data/2.5/weather?q=London,uk&APPID=bc3c5b8507f2b50691ecd5b4f5cf9ae8&units=metric";
   

    $.ajax({
        type: "GET",
        dataType: "json",
        url: wsurl,
        success: function (data) {
            console.log(data)
            udskrivData(data);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert("FEJL: " + jqXHR + textStatus + errorThrown)

        }
    })

    

        
    function udskrivData(data) {
        
        $("#byvejr").append("<div>Vejret i " + data.name +"</div>")
        $("#byvejrspecific").append("<p>Temperaturen er: " + data.main.temp + "</p>")
        $("#vejrikon").attr("src","http://openweathermap.org/img/w/" + data.weather[0].icon + ".png")
         
    };

    $("#soegknap").on("click", ChooseCity(by))


    function ChooseCity(by) {
        $("#soeg").val(this);
        wsurl = "'http://api.openweathermap.org/data/2.5/weather?q=' + this+',dk&APPID=bc3c5b8507f2b50691ecd5b4f5cf9ae8&units=metric'";
        alert(wsurl)
    };

    

})
