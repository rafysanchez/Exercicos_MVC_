function Imprimir(id, controller, form) {
    waitingDialog.show('Gerando impressão');
    $.ajax({
        datatype: 'json',
        type: 'Post',
        url: "/" + area + "/" + controller + "/Imprimir",
        cache: false,
        async: true,
        data: JSON.stringify({ id: id }),
        contentType: "application/json;",
        success: function (dados) {
            if (dados.Status == "Sucesso") {
                //window.location = "/" + area + "/" + controller + "/DownloadFile";
                $(form).submit();
                waitingDialog.hide();
            } else {
                waitingDialog.hide();
                AbrirModal(dados.Msg);
            }
        }
    });
}