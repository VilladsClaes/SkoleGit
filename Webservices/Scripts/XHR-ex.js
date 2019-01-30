window.onload = kaldWebService;

function kaldWebService() {

    var wsurl = "https://swapi.co/api/planets/1/";

    var request = new XMLHttpRequest;

    request.onreadystatechange = function () {

        if (this.readyState == 4 && this.status == 200) {
            console.log(this.responseText);
            udskrivData(JSON.parse(this.responseText));
        }

    };

    request.open("GET", wsurl, true);

    request.send();

    function udskrivData(data) {
        document.getElementById("resultat").innerHTML = data.name;
    };

    console.log(JSON.parse(XMLHttpRequest.responseText).name);

};