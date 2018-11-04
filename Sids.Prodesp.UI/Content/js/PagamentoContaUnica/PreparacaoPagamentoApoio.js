function consultarDocumentoGerador() {

    var entity = {
        AnoExercicio: $("#AnoExercicio").val(),
        CodigoAutorizadoAssinatura: $("#CodAssAutorizado").val(),
        CodigoAutorizadoGrupo: $("#txtAutorizadoGrupo").val(),
        CodigoAutorizadoOrgao: $("#txtAutorizadoOrgao").val(),
        CodigoExaminadoAssinatura: $("#CodAssExaminado").val(),
        CodigoExaminadoGrupo: $("#txtExaminadoGrupo").val(),
        CodigoExaminadoOrgao: $("#txtExaminadoOrgao").val(),
        CodigoConta: $("#CodigoConta").val(),
        NumeroAgencia: $("#NumeroAgencia").val(),
        NumeroConta: $("#NumeroConta").val(),
        NumeroBanco: $("#NumeroBanco").val(),

        Referencia: $("#Referencia").val(),
        DocumentoTipoId: $("#DocumentoTipoId").val(),
        NumeroDocumento: $("#NumeroDocumento").val(),
        ValorDocumento: $("#ValorDocumento").length > 0 ? $("#ValorDocumento").val().replace(/[\.,R$ ]/g, "") : ""
    }

    var campos = 0;
    var campo = "";

    if (entity.AnoExercicio == "") {
        campos = campos + 1;
        campo = "- Ano Exercicio";
    }

    if (entity.CodigoAutorizadoAssinatura == "") {

        campos = campos + 1;
        campo = campo + "- Cod Assintaura";
    }

    if (entity.CodigoExaminadoAssinatura == "") {

        campos = campos + 1;
        campo = campo + "- Cod Ex. Assinatura";
    }

    if (entity.CodigoExaminadoGrupo == "") {

        campos = campos + 1;
        campo = campo + "- Ex. Grupo";
    }

    if (entity.CodigoExaminadoOrgao == "") {

        campos = campos + 1;
        campo = campo + "- Ex. Orgão";
    }

    if (entity.CodigoConta == "") {

        campos = campos + 1;
        campo = campo + "- Cod. Conta";
    }

    if (entity.DocumentoTipoId == "") {

        campos = campos + 1;
        campo = campo + "- Tipo Documento";
    }
    
    if (entity.NumeroDocumento == "") {

        campos = campos + 1;
        campo = campo + "- N° Documento";
    }

    if (entity.ValorDocumento == "") {
        campos = campos + 1;
        campo = campo + "- Valor documento ";
    }

    if (campos >= 2) {
        AbrirModal("Os campos " + campo + " devem ser preenchidos")
        return false;
    }

    else if (campos != 0) {
        AbrirModal("O campo " + campo + " deve ser preenchido");
        return false;
    }


    $.ajax({
        datatype: 'JSON',
        type: 'POST',
        url: "/PagamentoContaUnica/PreparacaoDePagamento/ConsultarPreparacaoPgtoDocGerador",
        data: JSON.stringify({ entity: entity }),
        contentType: "application/json; charset=utf-8",
        beforeSend: function () {
            waitingDialog.show('Consultando');
        },
        success: function (dados) {
            if (dados.Status === 'Sucesso') {
                $("#TipoDespesaCredor").val(dados.PreparacaoPagamento.outTipoDespesa.substring(0,2)),
                $("#DataVencimento").val(dados.PreparacaoPagamento.outVencimento),
                $("#Referencia").val(dados.PreparacaoPagamento.outReferencia),
                $("#NumeroContrato").val(dados.PreparacaoPagamento.outContrato),
                $("#CodigoCredorOrganizacaoId").val(dados.PreparacaoPagamento.outOrganiz),
                $("#NumeroCNPJCPFCredorId").val(dados.PreparacaoPagamento.outCPFCGC),
                $("#Credor1").val(dados.PreparacaoPagamento.outCredor1),
                $("#Credor2").val(dados.PreparacaoPagamento.outCredor2),
                $("#DescricaoLogradouroEntrega").val(dados.PreparacaoPagamento.outEndereco),
                //dados.entity.outBairro
                //dados.entity.outCidade
                //dados.entity.outEstado
                $("#NumeroCEPEntrega").val(dados.PreparacaoPagamento.outCEP),

                $("#NumeroConta").val(dados.PreparacaoPagamento.outContaPagto.substring(35, 44)),
                $("#NumeroBanco").val(dados.PreparacaoPagamento.outContaPagto.substring(5, 8)),
                $("#NumeroAgencia").val(dados.PreparacaoPagamento.outContaPagto.substring(18, 23)),

                $("#NumeroContaCredor").val(dados.PreparacaoPagamento.outContaCredor.substring(35, 44)),
                $("#NumeroBancoCredor").val(dados.PreparacaoPagamento.outContaCredor.substring(5, 8)),
                $("#NumeroAgenciaCredor").val(dados.PreparacaoPagamento.outContaCredor.substring(18, 23))

                $("#NumeroContaPagto").val(dados.PreparacaoPagamento.outContaPagto.substring(35, 44)),
                $("#NumeroBancoPagto").val(dados.PreparacaoPagamento.outContaPagto.substring(5, 8)),
                $("#NumeroAgenciaPagto").val(dados.PreparacaoPagamento.outContaPagto.substring(18, 23))

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


function ConsultarOrgaoDataVenc() {
   
    var entity = {
        AnoExercicio: $("#AnoExercicio").val(),
        CodigoConta: $("#CodigoConta").val(),
        CodigoAutorizadoAssinatura: $("#CodAssAutorizado").val(),
        CodigoAutorizadoGrupo: $("#txtAutorizadoGrupo").val(),
        CodigoAutorizadoOrgao: $("#txtAutorizadoOrgao").val(),
        CodigoExaminadoAssinatura: $("#CodAssExaminado").val(),
        CodigoExaminadoGrupo: $("#txtExaminadoGrupo").val(),
        CodigoExaminadoOrgao: $("#txtExaminadoOrgao").val(),
    }

    //validação de campos para consulta
    var campos =  0;
    var nomeCampo = "";

    if (entity.AnoExercicio == "") {
        campos = campos + 1;
        nomeCampo = nomeCampo + "- Ano Exercicio";
    }

    if (entity.CodigoConta == "") {

        campos = campos + 1;
        nomeCampo = nomeCampo + "- Cod. Conta";
    }
    
    if (entity.CodigoAutorizadoAssinatura == "") {

        campos = campos + 1;
        nomeCampo = nomeCampo + "- Cod Assintaura";
    }

    if (entity.CodigoExaminadoAssinatura == "") {

        campos = campos + 1;
        nomeCampo =nomeCampo + "- Cod Ex. Assinatura";
    }

    if (entity.CodigoExaminadoGrupo == "") {

        campos = campos + 1;
        nomeCampo =nomeCampo + "- Ex. Grupo";
    }

    if (entity.CodigoExaminadoOrgao == "") {

        campos = campos + 1;
        nomeCampo = nomeCampo + "- Ex. Orgão";
    }

    if (campos >= 2) {
        AbrirModal("Os campos " + nomeCampo + " devem ser preenchidos")
        return false;
    }

    else if (campos != 0) {
        AbrirModal("O campo " + nomeCampo + " deve ser preenchido");
        return false;
    }
    

    $.ajax({
        datatype: 'JSON',
        type: 'POST',
        url: "/PagamentoContaUnica/PreparacaoDePagamento/ConsultarPreparacaoPgtoTipoDespesaDataVenc",
        data: JSON.stringify({ entity: entity }),
        contentType: "application/json; charset=utf-8",
        beforeSend: function () {
            waitingDialog.show('Consultando');
        },
        success: function (dados) {
            if (dados.Status === 'Sucesso') {
                var limiteConta = dados.PreparacaoPagamento.outContaPagtoNUMERO.indexOf(" - ");
                $("#NumeroBanco").val(dados.PreparacaoPagamento.outContaPagtoBANCO.substring(0, 3)),
                $("#NumeroAgencia").val(dados.PreparacaoPagamento.outContaPagtoAGENCIA.substring(0, 5)),
                $("#NumeroConta").val(dados.PreparacaoPagamento.outContaPagtoNUMERO.substring(0, limiteConta).trim())

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