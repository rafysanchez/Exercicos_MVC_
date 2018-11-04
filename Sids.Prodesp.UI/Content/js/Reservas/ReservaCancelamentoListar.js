
var area = "Reserva";
$(document).ready(function () {


    $("#Regional").attr("ReadOnly", usuario.RegionalId != 1);
    $('#Regional > option:not(:selected)').attr('disabled', usuario.RegionalId != 1);

    $("#AnoExercicio").change(function () {
        GerarComboPtres();
        GerarComboCfp();
        GerarComboNatureza();
    });

    $("#Ptres").change(function () {
        SelecioarComboCfp();
        GerarComboNatureza();
    });

    $("#Programa").change(function () {
        SelecionarComboPtres();
        GerarComboNatureza();
    });

    $("#Natureza").change(function () {
        GerarAplicacao("Obra");
    });

    $('#btnReTransmitir').click(function () {    //retransmite somente os checkados
        tans = true;
        ObterId("ReservaCancelamento");
    });

    $('#idSelecionar').click(function () {   // marca e desmarca typeBox do grid para o usuario poder visualizar o evento
        MarcaTodos();
    });
});