window.onload = kaldWebService;

function kaldWebService() {

    var wsurl = "https://swapi.co/api/planets/1/";

    fetch(wsurl, { method: "get" }).then(function (response) { return response.json(); }).then(function () { data }).catch(function (error) { alert("det gik ikke") })

  
    function udskrivData(data) {
        document.getElementById("resultat").innerHTML = data.name;
    }


};