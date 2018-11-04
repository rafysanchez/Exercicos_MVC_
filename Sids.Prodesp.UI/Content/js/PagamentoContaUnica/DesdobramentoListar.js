
var obj = "Desdobramento";
$(document).on('ready', function () {
    $('#btnReTransmitir').click(function () {
        tans = true;
        ObterId(controller);
    });

    $('#idSelecionar').click(function () {
        MarcaTodos();
    });

    $('#DocumentoTipoId').change(function () {
        filterHandler();
    });

    function filterHandler() {
        $('#NumeroDocumento').removeClass("Subempenho");
        $('#NumeroDocumento').removeClass("RapRequisicao");

        if ($("#DocumentoTipoId").val() == 11) {
            $('#NumeroDocumento').addClass("RapRequisicao");
        } else if ($("#DocumentoTipoId").val() == 5) {
            $('#NumeroDocumento').addClass("Subempenho");
        }
    }
});