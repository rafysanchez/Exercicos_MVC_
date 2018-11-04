function ConsultarOrgaoDataVenc2() {

    var entity = {
        AnoExercicio: $("#AnoExercicio").val(),
        CodigoConta: $("#CodigoConta").val(),
        DataPreparacao: $("#DataPreparacao").val(),
        DataPagamento: $("#DataPagamento").val(),
        CodigoAssinatura: $("#CodAssAutorizado").val(),
        CodigoGrupoAssinatura: $("#txtAutorizadoGrupo").val(),
        CodigoOrgaoAssinatura: $("#txtAutorizadoOrgao").val(),
        CodigoContraAssinatura: $("#CodAssExaminado").val(),
        CodigoContraGrupoAssinatura: $("#txtExaminadoGrupo").val(),
        CodigoContraOrgaoAssinatura: $("#txtExaminadoOrgao").val(),
    }

    //validação de campos para consulta
    var campos = 0;
    var nomeCampo = "";


    if (entity.CodigoConta == "") {

        campos = campos + 1;
        nomeCampo = nomeCampo + "- Cod. Conta";
    }

    if (entity.DataPreparacao == "") {

        campos = campos + 1;
        nomeCampo = nomeCampo + "- Data Preparação";
    }

    if (entity.DataPagamento == "") {

        campos = campos + 1;
        nomeCampo = nomeCampo + "- Data Pagamento";
    }

    if (entity.CodigoAssinatura == "") {

        campos = campos + 1;
        nomeCampo = nomeCampo + "- Cod Assintaura";
    }

    if (entity.CodigoContraAssinatura == "") {

        campos = campos + 1;
        nomeCampo = nomeCampo + "- Cod Ex. Assinatura";
    }

    if (entity.CodigoContraGrupoAssinatura == "") {

        campos = campos + 1;
        nomeCampo = nomeCampo + "- Ex. Grupo";
    }

    if (entity.CodigoContraOrgaoAssinatura == "") {

        campos = campos + 1;
        nomeCampo = nomeCampo + "- Ex. Orgão";
    }

    if (campos >= 2) {
        AbrirModal("Os campos " + nomeCampo + " devem ser preenchidos")
        return false;
    }

    //if (entity.DataPreparacao > entity.DataPagamento) {
    //    AbrirModal("A data da Preparação da OP deve ser menor ou igual à data do pagamento.")
    //    return false;
    //}

    else if (campos != 0) {
        AbrirModal("O campo " + nomeCampo + " deve ser preenchido");
        return false;
    }


    $.ajax({
        datatype: 'JSON',
        type: 'POST',
        url: "/PagamentoContaDer/ArquivoRemessa/ConsultarArquivoTipoDataVenc2",
        data: JSON.stringify({ entity: entity }),
        contentType: "application/json; charset=utf-8",
        beforeSend: function () {
            waitingDialog.show('Consultando');
        },
        success: function (dados) {
            if (dados.Status === 'Sucesso') {
                //var limiteConta = dados.ArquivoRemessa.outContaPagtoNUMERO.indexOf(" - ");
                //$("#NumeroBancoPagto").val(dados.ArquivoRemessa.outContaPagtoBANCO.substring(0, 3)),
                //$("#NumeroAgenciaPagto").val(dados.ArquivoRemessa.outContaPagtoAGENCIA.substring(0, 5)),
                //$("#NumeroContaPagto").val(dados.ArquivoRemessa.outContaPagtoNUMERO.substring(0, limiteConta).trim())

                $("#NumeroBancoPagto").val(dados.ArquivoRemessa.outNumBanco),
                $("#NumeroAgenciaPagto").val(dados.ArquivoRemessa.outNumAgencia),
                $("#NumeroContaPagto").val(dados.ArquivoRemessa.outNumConta)

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
