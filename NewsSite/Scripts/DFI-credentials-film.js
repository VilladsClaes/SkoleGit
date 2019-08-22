
$(document).ready(function () {


    var wsurl = "https://api.dfi.dk/v1/film/74245";
    var titeloverskrift = $("#titel");
    var beskrivelsetekst = $("#beskrivelse");
    var medvirkendelist = $("#medvirkende");

    KaldWebservice();

    

    //Søgefunktion
    //$("#soeg").keyup(Soeg);
    $("#soeg").keyup(Soeg);


    function Soeg() {


        var soegeord = $("#soeg").val();
        wsurl = "https://api.dfi.dk/v1/film?SortBy=title&Title=" + soegeord;
        alert("du har søgt på " + soegeord)

        KaldWebservice();




    };




    function KaldWebservice() {

        $.ajax({
            type: "GET",
            dataType: "json",
            url: wsurl,
            headers: {
                'Authorization': 'Basic ' + btoa('F005936:JRbTlfWVMH0bm3n')
            },
            success: function (data) {
                console.log(data)
                udskrivData(data);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                alert("FEJL: " + jqXHR + textStatus + errorThrown)

            }
        })
    }

   

    function udskrivData(data) {

       

        if (data.ResultCount > 1) {
            $("#antalresultater").html("der blev fundet " + data.ResultCount + " film");
            titeloverskrift.html(""); //fjerner Jagten-startfilmen
            beskrivelsetekst.html("");
            medvirkendelist.html("");
            $(".card-title").html("");
            $(".card-subtitle").html("");
        }

        for (var x in data.FilmList) {

            $(".card").append("<h5 class='card-title'>" + data.FilmList[x].Title + " <h6 class='card - subtitle mb - 2 text - muted'>" + data.FilmList[x].ReleaseYear  );
           
            var Kategori = "";
            for (var y in data.FilmList[x].PersonCredits) {

                if (Kategori != data.FilmList[x].PersonCredits[y].Type) { //tjek om det er en anderledes Type

                    medvirkendelist.append("<ol start='" + x + "'><li style='list-style-type:none;'><h2>" + data.FilmList[x].PersonCredits[y].Type)

                    Kategori = data.FilmList[x].PersonCredits[y].Type;

                }

                medvirkendelist.append("<li>" + data.FilmList[x].PersonCredits[y].Name + " </li>") //skriv Name

            };


        }
        



        titeloverskrift.append(data.Title);
        beskrivelsetekst.append(data.Description);
        var Kategori = "";
        for (var x in data.PersonCredits) {

            if (Kategori != data.PersonCredits[x].Type) { //tjek om det er en anderledes Type

                medvirkendelist.append("<ol start='" + x + "'><li style='list-style-type:none;'><h2>" + data.PersonCredits[x].Type)

                Kategori = data.PersonCredits[x].Type;

            }

            medvirkendelist.append("<li>" + data.PersonCredits[x].Name + " </li>") //skriv Name

        };

        
        


    };




});



