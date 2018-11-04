var obj = "ArquivoRemessa";
var modalCancelarArquivo;
var modalImprimir;
var entity;
//var modalBloqueioPagamento;

$(document).on('ready', function () {

    modalCancelarArquivo = $("#modalCancelarArquivo");
    //modalBloqueioPagamento = $("#modalBloqueioPagamento");
    modalImprimir = $("#modalImprimirOP");
    $("#RegionalId").attr("ReadOnly", usuario.RegionalId != 1);
    $('#RegionalId > option:not(:selected)').attr('disabled', usuario.RegionalId != 1);

    $("#modalCancelarArquivo").remove();
    $("#modalImprimirOP").remove();
    //$("#modalBloqueioPagamento").remove();

    $('#btnReTransmitir').click(function () {
        tans = true;
        ObterId(controller);
    });

    $('#idSelecionar').click(function () {
        MarcaTodos();
    });



});

function ImprimirOp(id) {

    if (navigator.onLine != true) {
        AbrirModal("Erro de conexão");
        return false;
    }

    $.ajax({
        datatype: 'JSON',
        type: 'POST',
        url: "/PagamentoContaDer/ArquivoRemessa/ConsultaImpressaoOpApoio",
        data: JSON.stringify({ id: id }),
        contentType: "application/json; charset=utf-8",
        beforeSend: function () {
            waitingDialog.show('Consultando');
        },
        success: function (dados) {
            if (dados.Status === 'Sucesso') {
                modalImprimir.remove();
                $("body").append(modalImprimir);
                modalImprimir.modal('show');


                $("#CodAssAutorizado").change(function BuscarAssinatura() {
                    var valor = $("div >#CodAssAutorizado").val();

                    tipo = 1;
                    LimparCombo(tipo);

                    if (valor.length != 0 & valor.length == 5 & valor != "") {
                        ConsultarAssinatura(valor, tipo);
                    }
                });

                $("#CodAssExaminado").change(function BuscarAssinatura() {
                    var valor = $("div >#CodAssExaminado").val();
                    tipo = 2;
                    LimparCombo(tipo);
                    if (valor.length != 0 & valor.length == 5 & valor != "") {
                        ConsultarAssinatura(valor, tipo);
                    }
                });



                entity = dados.objModel;
                $("#DataPreparacao").val(ConvertDateCSharp(entity.DataTrasmitido));

                $("#CodAssAutorizado").val(entity.CodigoAssinatura);

                $("#txtAutorizadoGrupo").val(entity.CodigoGrupoAssinatura);
                $("#txtAutorizadoOrgao").val(entity.CodigoOrgaoAssinatura);
                $("#txtAutorizadoNome").val(entity.NomeAssinatura);
                $("#txtAutorizadoCargo").val(entity.DesCargo);

                $("#CodAssExaminado").val(entity.CodigoContraAssinatura);

                $("#txtExaminadoGrupo").val(entity.CodigoContraGrupoAssinatura);
                $("#txtExaminadoOrgao").val(entity.CodigoContraOrgaoAssinatura);
                $("#txtExaminadoNome").val(entity.NomeContraAssinatura);
                $("#txtExaminadoCargo").val(entity.DesContraCargo);

                $("#CodConta").val(entity.CodigoConta);
                $("#NumeroConta").val(entity.Banco + " " + entity.Agencia + " " + entity.NumeroConta);
                $("#NumGeracao").val(entity.NumeroGeracao);
                $("#QtOpArquivo").val(entity.QtOpArquivo);
                $("#ValorTotal").val(entity.ValorTotal);
                $("#ValorTotal").maskMoney({ prefix: "R$ ", allowNegative: false, thousands: ".", decimal: ",", affixesStay: true });
                $("#ValorTotal").maskMoney('mask');
                //AtualizarFormulario(dados.objModel);
            }
            else {
                AbrirModal(dados.Msg);
            }
        },
        error: function (dados) {
            waitingDialog.hide();
            AbrirModal(dados);
        },
        complete: function () {
            waitingDialog.hide();
        }
    });
}


function ImprimirOP() {

    if (navigator.onLine != true) {
        AbrirModal("Erro de conexão");
        return false;
    }


    entity.DataTrasmitido = $("#DataPreparacao").val();

    entity.CodigoAssinatura= $("#CodAssAutorizado").val();

    entity.CodigoGrupoAssinatura= $("#txtAutorizadoGrupo").val();
    entity.CodigoOrgaoAssinatura = $("#txtAutorizadoOrgao").val();
    entity.NomeAssinatura= $("#txtAutorizadoNome").val();
    entity.DesCargo=$("#txtAutorizadoCargo").val();

    entity.CodigoContraAssinatura=$("#CodAssExaminado").val();

    entity.CodigoContraGrupoAssinatura=$("#txtExaminadoGrupo").val();
    entity.CodigoContraOrgaoAssinatura=$("#txtExaminadoOrgao").val();
    entity.NomeContraAssinatura=$("#txtExaminadoNome").val();
    entity.DesContraCargo=$("#txtExaminadoCargo").val();

    entity.CodigoConta =$("#CodConta").val();
    entity.NumeroConta =$("#NumeroConta").val();
    entity.NumeroGeracao = $("#NumGeracao").val();
    entity.QtOpArquivo= $("#QtOpArquivo").val();
    entity.ValorTotal = $("#ValorTotal").val();
    entity.SelArquivo = $("#selArquivo").val();



    $.ajax({
        datatype: 'JSON',
        type: 'POST',
        url: "/PagamentoContaDer/ArquivoRemessa/ImprimirOP",
        data: JSON.stringify({ arquivoRemessa: entity }),
        contentType: "application/json; charset=utf-8",
        beforeSend: function () {
            waitingDialog.show('Imprimindo');
        },
        success: function (dados) {
            if (dados.Status === 'Sucesso') {
                $.confirm({
                    text: "Operação Concluída",
                    title: "Confirmação",
                    cancel: function () {
                        window.location.reload();
                    },
                    cancelButton: "Ok",
                    confirmButton: "",
                    post: true,
                    cancelButtonClass: "btn-default",
                    modalOptionsBackdrop: true
                });
            }
            else {
                modalImprimir.modal('hide');
                AbrirModal(dados.Msg);
            }
        },
        error: function (dados) {
            waitingDialog.hide();
            modalImprimir.modal('hide');
            AbrirModal(dados);
        },
        complete: function () {
            waitingDialog.hide();
        }
    });
}


//function Imprimir(id, controller, form) {
//    waitingDialog.show('Gerando impressão');
//    $.ajax({
//        datatype: 'json',
//        type: 'Post',
//        url: "/" + area + "/" + controller + "/Imprimir",
//        cache: false,
//        async: true,
//        data: JSON.stringify({ id: id }),
//        contentType: "application/json;",
//        success: function (dados) {
//            if (dados.Status == "Sucesso") {
//                //window.location = "/" + area + "/" + controller + "/DownloadFile";
//                $(form).submit();
//                waitingDialog.hide();

//                $.confirm({
//                    text: "Operação Concluída",
//                    title: "Confirmação",
//                    cancel: function () {
//                        window.location.reload();
//                    },
//                    cancelButton: "Ok",
//                    confirmButton: "",
//                    post: true,
//                    cancelButtonClass: "btn-default",
//                    modalOptionsBackdrop: true
//                });

//            } else {
//                waitingDialog.hide();
//                AbrirModal(dados.Msg);
//            }
//        }
//    });
//}



function CancelarOpApoio( id) {

    var entity = {
        Id: id
    }

    $.ajax({
        datatype: 'JSON',
        type: 'POST',
        url: "/PagamentoContaDer/ArquivoRemessa/ConsultarCancelamentoArquivo",
        data: JSON.stringify({ ArquivoRemessa: entity }),
        contentType: "application/json; charset=utf-8",
        beforeSend: function () {
            waitingDialog.show('Consultando');
        },
        success: function (dados) {
            if (dados.Status === 'Sucesso') {
                //modalBloqueioPagamento.remove();
                modalCancelarArquivo.remove();
                $("body").append(modalCancelarArquivo);
                modalCancelarArquivo.modal('show');

                //AtualizarFormularioById(dados.objModel);
                $("#CodigoC").val(dados.objModel.CodigoConta);
                $("#NumGeracaoC").val(dados.objModel.NumeroGeracao);

                var date = dados.objModel.DataTrasmitido;
                var nowDate = new Date(parseInt(date.substr(6)));
                var teste = nowDate.toLocaleDateString("pt-BR")

                $("#DataPreparacaoC").val(teste);

    




                $("#QtOpArquivoC").val(dados.objModel.QtOpArquivo);
                $("#ValorTotalC").val(dados.objModel.ValorTotal);
                $("#ValorTotalC").maskMoney({ prefix: "R$ ", allowNegative: false, thousands: ".", decimal: ",", affixesStay: true });
                $("#ValorTotalC").maskMoney('mask');


                $("#CanOPId").val(dados.objModel.Id);
                //$("#CanOPTipoId").val(tipo);
                $("#transmitirCancelamento").click(CancelarOp);

            }
            else {
                AbrirModal(dados.Msg);
            }
        },
        error: function (dados) {
            waitingDialog.hide();
            AbrirModal(dados);
        },
        complete: function () {
            waitingDialog.hide();
        }
    });
}

function CancelarOp() {



    var entity = {
        Id: $("#CanOPId").val()
    }

    $.ajax({
        datatype: 'JSON',
        type: 'POST',
        url: "/PagamentoContaDer/ArquivoRemessa/TransmitirCancelamentoOp",
        data: JSON.stringify({ arquivoremessa: entity }),
        contentType: "application/json; charset=utf-8",
        beforeSend: function () {
            waitingDialog.show('Consultando');
        },
        success: function (dados) {
            if (dados.Status === 'Sucesso') {
                $.confirm({
                    text: "Operação realizada com sucesso",
                    title: "Confirmação",
                    cancel: function () {
                        ShowLoading();
                        $("#form_filtro").submit();
                    },
                    cancelButton: "Ok",
                    confirmButton: "",
                    post: true,
                    cancelButtonClass: "btn-default",
                    modalOptionsBackdrop: true
                });
            }
            else {
                AbrirModal(dados.Msg);
            }
        },
        error: function (dados) {
            AbrirModal(dados);
        },
        complete: function () {
            FecharModal("#modalCancelarArquivo");
            modalCancelarArquivo.remove();
            waitingDialog.hide();
        }
    });
}


