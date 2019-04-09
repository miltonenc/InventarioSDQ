$(document).ready(function () {
    $('#btnSincronizar').click(function () {
        
        $.post("AsientoContable/Sincronizar",
            {
                id: $('#item_id').val()
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