﻿$(document).ready(function () {
    var base_url = window.location.origin;
    console.log("BASE URI: " + base_url + "/AsientoContable/Sincronizar");
    $('#btnSincronizar').click(function () {
        
        $.post(base_url + "/AsientoContable/Sincronizar",
            {
                id: $('#id').val()
            },
            function (data) {
                var myObj = JSON.parse(data);
                console.log(myObj);
                if (myObj.resultado == "Ok") {
                    $('#mensaje').show();
                    mensajeCambiosGuardadosTimeOut = setTimeout(ocultarMensaje, 3500);

                } else {
                    $('#mensajeError').show();
                    mensajeCambiosGuardadosTimeOut = setTimeout(ocultarMensajeError, 3500);
                }

            }),
            function (error) {
                console.log(error);
            };

    });
});


function ocultarMensaje() {
    $('#mensaje').slideUp('fast');
    clearTimeout(mensajeCambiosGuardadosTimeOut);
}


function ocultarMensajeError() {
    $('#mensajeError').slideUp('fast');
    clearTimeout(mensajeCambiosGuardadosTimeOut);
}