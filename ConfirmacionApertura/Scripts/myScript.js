function myFunction() {
    //Oculto lo de este id
    $('#btnAdmin').hide();

    //oculto muestro los cursos segun la selección
    //var gender = document.querySelector('input[name = "gender"]:checked').value;
    //alert("You entered " + gender + " for your gender<br>");

}

function formatearContenido() {
    var str = document.getElementById("contenido").innerHTML;
    var res = str.replace(/\*/g, "</br>* ");
    document.getElementById("contenido").innerHTML = res;

}

$(document).keydown(function (e) {
    if (e.keyCode === 19) {
        $('#btnAdmin').show();
    }
});

$(document).keyup(function (e) {
    if (e.keyCode === 19) {
        $('#btnAdmin').hide();
    }
});

