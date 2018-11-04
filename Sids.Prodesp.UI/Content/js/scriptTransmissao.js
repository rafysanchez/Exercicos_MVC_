function Transmissao(object, ctrl, msgCadastrarOutro) {
    var isCadastrarOutro = msgCadastrarOutro !== undefined && msgCadastrarOutro !== null && msgCadastrarOutro.length > 0;

    if (!((ModelItem.TransmitidoSiafem === false && ModelItem.TransmitirSiafem === true) || (ModelItem.TransmitidoSiafisico === false && ModelItem.TransmitirSiafisico === true) || (ModelItem.TransmitidoProdesp === false && ModelItem.TransmitirProdesp === true))) {
        AbrirModal("Por favor selecione um sistema que ainda não tenha sido transmitido!");
        return false;
    }


    $.ajax({
        datatype: 'json',
        type: 'Post',
        url: "/" + area + "/" + ctrl + "/Transmitir",
        cache: false,
        //async: false,
        data: object,
        contentType: "application/json; charset=utf-8",
        beforeSend: function () {
            waitingDialog.show("Transmitindo");
        },
        complete: function () {
            waitingDialog.hide();
        },
        success: function (dados) {
            if (dados.Status == "Sucesso") {
                if (dados.Codigo !== 0) {
                    ModelItem = dados.objModel;

                    $("#Codigo").val(dados.Codigo);
                    $.confirm({
                        text: isCadastrarOutro ? msgCadastrarOutro : "Transmissão realizada com sucesso",
                        title: "Transmitir",
                        cancel: function () {
                            tans = "I";
                            BoquearTransmitido();
                            if ((ctrl.toLowerCase() !== "movimentacao" && (ModelItem.TransmitidoSiafem == true || ModelItem.TransmitidoSiafisico == true)) || (ctrl === "Desdobramento" && ModelItem.TransmitidoProdesp === true)) {
                                Imprimir(dados.Codigo, ctrl, $("#frmPainelCadastrar"));
                            }
                            else {
                                ShowLoading();
                                window.location.href = '/' + area + '/' + ctrl + '/Edit/' + dados.Codigo + '?tipo=a';
                            }
                        },
                        confirm: function () {
                            ShowLoading();
                            window.location.href = '/' + area + '/' + ctrl + '/CreateThis/' + dados.Codigo;
                        },
                        cancelButton: isCadastrarOutro ? "Não" : "Fechar",
                        confirmButton: isCadastrarOutro ? "Sim" : ""
                    });
                }
            } else if (dados.Status == "Falha") {
                $("#Codigo").val(dados.Codigo);
                AbrirModal(dados.Msg, function editar() { ShowLoading(); window.location.href = '/' + area + '/' + ctrl + '/Edit/' + dados.Codigo + '?tipo=a'; });
            } else if (dados.Status == "Falha Doc") {

                ModelItem = dados.objModel;

                $.confirm({
                    text: "Inserir Documento" + dados.Msg,
                    title: "Transmitir",
                    cancel: function () {
                        tans = "I";
                        BoquearTransmitido();
                        if (ModelItem.TransmitidoSiafem == true || ModelItem.TransmitidoSiafisico == true)
                            Imprimir(dados.Codigo, ctrl, $("#frmPainelCadastrar"));
                        else {
                            ShowLoading();
                            window.location.href = '/' + area + '/' + ctrl + '/Edit/' + dados.Codigo + '?tipo=a';
                        }
                    },
                    cancelButton: "Fechar",
                    confirmButton: ""
                });
            } else if (dados.Status == "Falha Prodesp") {
                $("#NumSiafemSiafisico").val(dados.objModel.NumSiafemSiafisico);
                $("#Codigo").val(dados.Codigo);

                ModelItem = dados.objModel;

                $.confirm({
                    text: dados.Msg,
                    title: "Transmitir",
                    cancel: function () {
                        tans = "I";
                        BoquearTransmitido();
                        if (ModelItem.TransmitidoSiafem == true || ModelItem.TransmitidoSiafisico == true)
                            Imprimir(dados.Codigo, ctrl, $("#frmPainelCadastrar"));
                        else {
                            ShowLoading();
                            window.location.href = '/' + area + '/' + ctrl + '/Edit/' + dados.Codigo + '?tipo=a';
                        }
                    },
                    cancelButton: "Fechar",
                    confirmButton: ""
                });
            } else {
                $("#Codigo").val(dados.Codigo);
                ModelItem = dados.objModel;
                $.confirm({
                    text: "Transmitido Siafem/Siafisico | " + dados.Msg,
                    title: "Transmitir",
                    cancel: function () {
                        tans = "I";
                        if (ModelItem.TransmitidoSiafem == true || ModelItem.TransmitidoSiafisico == true)
                            Imprimir(dados.Codigo, ctrl, $("#frmPainelCadastrar"));
                        else {
                            ShowLoading();
                            window.location.href = '/' + area + '/' + ctrl + '/Edit/' + dados.Codigo + '?tipo=a';
                        }
                    },
                    cancelButton: "Fechar",
                    confirmButton: ""
                });
            }
            //$('.close').css('display', 'none'); // TODO verificar
        },
        error: function (dados) {
            AbrirModal(dados);
        }
    });
}

function Retransmissao(object, ctrl) {
    waitingDialog.show('Retransmitindo');
    $.ajax({
        datatype: 'json',
        type: 'Post',
        url: "/" + area + "/" + ctrl + "/Retransmitir",
        cache: false,
        async: true,
        data: ids = object,
        contentType: "application/json; charset=utf-8",
        success: function (dados) {
            if (dados.Status == "Sucesso") {
                waitingDialog.hide();
                $.confirm({
                    text: "Retransmissão realizada com sucesso",
                    title: "Retransmitir",
                    cancel: function () {
                        $("#form_filtro").submit();
                    },
                    cancelButton: "Fechar",
                    confirmButton: ""
                });

            } else {
                waitingDialog.hide();
                $.confirm({
                    text: dados.Msg.replace(/[\;]/g, "\n"),
                    title: "Retransmitir",
                    cancel: function () {
                        $("#form_filtro").submit();
                    },
                    cancelButton: "Fechar",
                    confirmButton: ""
                });
            }
        },
        error: function (dados) {
            waitingDialog.hide();
            AbrirModal(dados);
        }
    });
}

function MarcaTodos() {   // marca e desmarca typeBox do grid para o usuario poder visualizar o evento
    if ($('#idSelecionar').is(':checked')) {
        $(".idRetrasmitir").prop("checked", true);
    }
    else {
        $(".idRetrasmitir").prop("checked", false);
    }
}

function ObterId(ctrl) {
    var ids = [];
    $.each($("td >.idRetrasmitir"), function (id, val) {
        if ($(val).is(":checked"))
            ids[ids.length] = $(val).val(); //percorre o vetor
    });

    if (ids.length <= 0) {
        AbrirModal("Selecione ao menos um item da lista para Transmistir");
        return false;
    }

    var object = JSON.stringify(ids);
    Retransmissao(object, ctrl);
}

function BoquearTransmitido() {
    if (ModelItem.TransmitidoProdesp == true)
        $(".prodesp").attr("disabled", "disabled");

    if (ModelItem.TransmitidoSiafem == true)
        $(".siafem").attr("disabled", "disabled");

    if (ModelItem.TransmitidoSiafisico == true)
        $(".siafisico").attr("disabled", "disabled");
}

