var area = "LiquidacaoDespesa";

$(document).ready(function () {
    
    $('#btnReTransmitir').click(function () {
        tans = true;
        ObterId("Subempenho");
    });

    $('#idSelecionar').click(function () {
        MarcaTodos();
    });
});


