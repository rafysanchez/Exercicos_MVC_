var obj = "ReclassificacaoRetencao";
$(document).on('ready', function () {
    $('#btnReTransmitir').click(function () {
        tans = true;
        ObterId(controller);
    });

    $('#idSelecionar').click(function () {
        MarcaTodos();
    });

    $('#ReclassificacaoRetencaoTipo').change(function () {
        cbxNormalEstorno();
    })

    function cbxNormalEstorno() {
      
        if ($('#ReclassificacaoRetencaoTipo').val() === "2") {
            $('#NormalEstorno').empty("<option></option>");
            $('#NormalEstorno').append("<option  value=''>Selecione</option>");
            $('#NormalEstorno').append("<option  value='2'>Normal</option>");
        }
        else {
            $('#NormalEstorno').empty("<option></option>");
            $('#NormalEstorno').append("<option  value=''>Selecione</option>");
            $('#NormalEstorno').append("<option  value='1'>Estorno</option>");
            $('#NormalEstorno').append("<option  value='2'>Normal</option>");
        }
    }
});