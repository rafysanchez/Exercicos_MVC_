function DesdobramentoApoio() {

    var entity = {
        DesdobramentoTipoId: $("#DesdobramentoTipoId").val(),
        DocumentoTipoId: $("#DocumentoTipoId").val(),
        NumeroDocumento: $("#NumeroDocumento").val(),
        CodigoServico: $("#CodigoServico").val(),
        ValorDistribuido: $("#ValorDistribuido").val().replace(/[R$ \.-]/g, "")
    }

    if (entity.DocumentoTipoId == "") {
        AbrirModal("Campo Tipo Documento deve ser selecionado");
        return false;
    }

    if (entity.NumeroDocumento == "") {
        AbrirModal("Campo N° Documento deve ser preenchido");
        return false;
    }


    if (entity.ValorDistribuido == "" && entity.DesdobrametoTipoId == 2) {
        AbrirModal("Campo Valor Dcto. Original deve ser selecionado");
        return false;
    }

    if (entity.ValorDistribuido == "" && entity.DesdobrametoTipoId == 1) {
        AbrirModal("Campo Valor Distribuição Original deve ser selecionado");
        return false;
    }


    if (entity.DocumentoTipoId == "" && entity.DesdobrametoTipoId == 1) {
        AbrirModal("Campo CodigoServico deve ser selecionado");
        return false;
    }


    $.ajax({
        datatype: 'JSON',
        type: 'POST',
        url: "/PagamentoContaUnica/Desdobramento/ConsultarDesdobramentoApoio",
        data: JSON.stringify({ entity: entity }),
        contentType: "application/json; charset=utf-8",
        beforeSend: function () {
            waitingDialog.show('Consultando');
        },
        success: function (dados) {
            if (dados.Status === 'Sucesso') {
                if (entity.DesdobramentoTipoId == 1) {
                    $("#DescricaoServico").val(dados.Desdobramento.outDescricaoServico);
                    $("#DescricaoCredor").val(dados.Desdobramento.outCredor);
                    $("#NomeReduzidoCredor").val(dados.Desdobramento.outCredorReduz);

                } else {
                    $("#DescricaoCredor").val(dados.Desdobramento.outCredor01);
                    $("#NomeReduzidoCredor").val(dados.Desdobramento.outCredorReduz);
                    $("#TipoDespesa").val(dados.Desdobramento.outTipoDespesa);
                    $("#ValorDistribuidoOutros").val(dados.Desdobramento.outValorOriginal);

                    $("#ValorDistribuidoOutros").maskMoney({ prefix: "R$ ", allowNegative: false, thousands: ".", decimal: ",", affixesStay: true });
                    $("#ValorDistribuidoOutros").maskMoney('mask');
                    $("#ValorDistribuidoOutros").maskMoney('destroy');
                }

                if (dados.NumeroContrato !== "" && dados.NumeroContrato !== null) {
                    $("#NumeroContrato").val(dados.NumeroContrato.replace(/(\d{2})(\d{2})(\d{5})(\d{1})/g, "\$1.\$2.\$3\-\$4"));
                }
                $("#CodigoAplicacaoObra").val(dados.CodigoAplicacaoObra.replace(/[\.-]/g, "").replace(/(\d)(\d{1})$/, "$1-$2"));
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

function ConsultaDocumento() {

    var entity = {
        DesdobramentoTipoId: $("#DesdobramentoTipoId").val(),
        DocumentoTipoId: $("#DocumentoTipoId").val(),
        NumeroDocumento: $("#NumeroDocumento").val(),
        CodigoServico: $("#CodigoServico").val(),
        ValorDistribuido: $("#ValorDistribuido").val().replace(/[R$ \.-]/g, "")
    }

    if (entity.DocumentoTipoId == "") {
        return false;
    }

    if (entity.NumeroDocumento == "") {
        return false;
    }
    

    $.ajax({
        datatype: 'JSON',
        type: 'POST',
        url: "/PagamentoContaUnica/Desdobramento/ConsultarDocumento",
        data: JSON.stringify({ entity: entity }),
        contentType: "application/json; charset=utf-8",
        beforeSend: function () {
            waitingDialog.show('Consultando');
        },
        success: function (dados) {
            if (dados.Status === 'Sucesso') {
                if (entity.DesdobramentoTipoId == 1) {
                    $("#ValorDistribuido").maskMoney('mask', dados.Valor);

                } else {
                    $("#ValorDistribuidoOutros").maskMoney('mask', dados.Valor);
                }

                if(dados.NumeroContrato !== "" && dados.NumeroContrato !== null){
                    $("#NumeroContrato").val(dados.NumeroContrato.replace(/(\d{2})(\d{2})(\d{5})(\d{1})/g, "\$1.\$2.\$3\-\$4"));
                }
                if(dados.CodigoAplicacaoObra !==""){
                    $("#CodigoAplicacaoObra").val(dados.CodigoAplicacaoObra.replace(/[\.-]/g, "").replace(/(\d)(\d{1})$/, "$1-$2"));
                }
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