function myFunction() {
    //Oculto lo de este id
    $('#btnAdmin').hide();

    //oculto muestro los cursos segun la selección
    //var gender = document.querySelector('input[name = "gender"]:checked').value;
    //alert("You entered " + gender + " for your gender<br>");

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