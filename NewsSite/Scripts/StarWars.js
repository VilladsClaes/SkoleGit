$(document).ready(function () {


    var wsurl = "https://swapi.co/api/people/";


    $.ajax({
        type: "GET",
        dataType: "json",
        url: wsurl,
        success: function (data) {
            console.log(wsurl); udskrivData(data);
        }
    })


    function udskrivData(data) {

        console.log("udskrivData køres");

        console.log(data);


        //EN METODE

        //$("#Starwarsresultat").append("<h1>Den første metode</h1>");

        for (var x in data.results) {

            $("#Starwarsresultat").append("<div>Skabningen hedder " + data.results[x].name + " </div>")


        };

        ////ANDEN METODE
        //$("#Starwarsresultat").append("<h1>Den anden metode</h1>");

        //var skabninger = "";


        //for (var x in data.results) {

        //    skabninger += "<div class='well'>" + "<h2>" + data.results[x].name + "</h2>" + "<p>" + data.results[x].eye_color + "</p>" + "</div>"

        //};

        //$("#Starwarsresultat").append(skabninger);

        ////Tredje metode (stringtemplate)
        //$("#Starwarsresultat").append("<h1>Den tredje metode</h1>");

        //var skabninger1


        //for (var x in data.results) {

        //    skabninger1 += `<h2>${data.results[x].name}</h2>`;

        //};




    };



    //Søge
    $("#soeg").keyup(Soeg);


    function Soeg() {

        var soegeord = $("#soeg").val();

        $("#Starwarsresultat").html("Søgeresultater");

        var soegeurl = wsurl + "?search=" + soegeord;


        if (soegeord.length > 0) {



            $.ajax({ type: "GET", dataType: "json", url: soegeurl, success: function (data) { console.log(soegeurl); FundneData(data); } })


            function FundneData(data) {

                for (var x in data.results) {

                    $("#Starwarsresultat").append("<div>Skabningen hedder " + data.results[x].name + " </div>")



                };

                if (data.count == 0) {
                    $("#Starwarsresultat").append("<div>Ingen resultater</div>");
                }

            };

        };




    };


})


