var obj = "ReclassificacaoRetencao";
$(document).on('ready', function () {

    $('#NormalEstorno').change(function () {
        cbxNormalEstorno();
    })

    function cbxNormalEstorno() {
      
        if ($('#NormalEstorno').val() === "2") {
            $('#ReclassificacaoRetencaoTipoId').empty("<option></option>");
            $('#ReclassificacaoRetencaoTipoId').append("<option  value=''>Selecione</option>");
            $('#ReclassificacaoRetencaoTipoId').append("<option  value='3'>Pagamento de Obras Sem OB</option>");
            $('#ReclassificacaoRetencaoTipoId').append("<option  value='5'>Retenção de INSS – SIAFNLRetInss</option>");
            $('#ReclassificacaoRetencaoTipoId').append("<option  value='4'>Retenção de ISS Prefeituras - SIAFNLIssReten</option>");
        }
        else {
            $('#ReclassificacaoRetencaoTipoId').empty("<option></option>");
            $('#ReclassificacaoRetencaoTipoId').append("<option  value=''>Selecione</option>");
            $('#ReclassificacaoRetencaoTipoId').append("<option  value='2'>Nota de Lançamento (Retenção / Reclassificação) - SIAFNL001</option>");
            $('#ReclassificacaoRetencaoTipoId').append("<option  value='3'>Pagamento de Obras Sem OB</option>");
            $('#ReclassificacaoRetencaoTipoId').append("<option  value='5'>Retenção de INSS – SIAFNLRetInss</option>");
            $('#ReclassificacaoRetencaoTipoId').append("<option  value='4'>Retenção de ISS Prefeituras - SIAFNLIssReten</option>");
        }
    }


});