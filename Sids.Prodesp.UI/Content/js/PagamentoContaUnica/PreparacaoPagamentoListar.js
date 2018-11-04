var modalImprimir;
var obj = "PreparacaoDePagamento";
var entity;
$(document).on('ready', function () {

    modalImprimir = $("#modalImprimir");

    $("#modalImprimir").remove();

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
        url: "/PagamentoContaUnica/PreparacaoDePagamento/ConsultaImpressaoOpApoio",
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

                $("#CodAssResponsavel").change(function BuscarAssinatura() {
                    var valor = $("div >#CodAssResponsavel").val();
                    tipo = 3;
                    LimparCombo(tipo);
                    if (valor.length != 0 & valor.length == 5 & valor != "") {
                        ConsultarAssinatura(valor, tipo);
                    }
                });

                entity = dados.objModel;
                $("#DataTransmitidoProdesp").val(ConvertDateCSharp(entity.DataTransmitidoProdesp));
                AtualizarFormulario(dados.objModel);
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


function Imprimir() {

    if (navigator.onLine != true) {
        AbrirModal("Erro de conexão");
        return false;
    }


    entity.CodigoConta = $("#CodigoConta").val();
    entity.DataTransmitidoProdesp = $("#DataTransmitidoProdesp").val();
    entity.NumeroBanco = $("#NumeroBanco").val();
    entity.NumeroAgencia = $("#NumeroAgencia").val();
    entity.NumeroConta = $("#NumeroConta").val();

    entity.CodigoAutorizadoAssinatura = $("#CodAssAutorizado").val();
    entity.CodigoAutorizadoGrupo = $("#txtAutorizadoGrupo").val();
    entity.CodigoAutorizadoOrgao = $("#txtAutorizadoOrgao").val();
    entity.NomeAutorizadoAssinatura = $("#txtAutorizadoNome").val();
    entity.DescricaoAutorizadoCargo = $("#txtAutorizadoCargo").val();
    entity.CodigoExaminadoAssinatura = $("#CodAssExaminado").val();
    entity.CodigoExaminadoGrupo = $("#txtExaminadoGrupo").val();
    entity.CodigoExaminadoOrgao = $("#txtExaminadoOrgao").val();
    entity.NomeExaminadoAssinatura = $("#txtExaminadoNome").val();
    entity.DescricaoExaminadoCargo = $("#txtExaminadoCargo").val();

    $.ajax({
        datatype: 'JSON',
        type: 'POST',
        url: "/PagamentoContaUnica/PreparacaoDePagamento/Imprimir",
        data: JSON.stringify({ preparacaoPagamento: entity }),
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