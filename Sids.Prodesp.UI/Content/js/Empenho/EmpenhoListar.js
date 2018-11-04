
var area = "Empenho";
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

    $('#btnReTransmitir').click(function () {
        tans = true;
        ObterId(controller);
    });

    $('#idSelecionar').click(function () {
        MarcaTodos();
    });
});


function ValidaFormularioEmpenho() {
    var online = navigator.onLine;

    if (online != true) {
        AbrirModal("Erro de conexão");
        return false;
    }
    ShowLoading();
    var cont = 0;
    $("#form_filtro input").each(function () {
        if ($(this).val() != "") {
            cont++;
        }
    });

    $("#form_filtro select").each(function () {
        if ($(this).val() != "") {
            cont++;
        }
    });

    if (cont == 0) {
        HideLoading();
        AbrirModal("Informe ao menos um campo para filtro!");
        return false;
    } else {
        var msg = "";
        var data1;
        var data2;

        var dtInicial = $("#DataCadastramentoDe").val();
        var dtFinal = $("#DataCadastramentoAte").val();

        if ($("#DataCadastramentoDe").length > 0) {
            var dia;
            var mes;
            var ano;
            if (dtInicial.length > 0 && isValidDate(dtInicial) == false) {
                msg = "A data inicial é invalida!";
            } else {

                dia = dtInicial.substr(0, 2);
                mes = dtInicial.substr(3, 2);
                ano = dtInicial.substr(6, 4);

                data1 = new Date(mes + "/" + dia + "/" + ano);
            }

            if (dtFinal.length > 0 && isValidDate(dtFinal) == false) {
                msg = "A data final é invalida!";
            } else {
                dia = dtFinal.substr(0, 2);
                mes = dtFinal.substr(3, 2);
                ano = dtFinal.substr(6, 4);

                data2 = new Date(mes + "/" + dia + "/" + ano);
            }

            if (data1 && data2 && isValidDate(dtFinal) && isValidDate(dtInicial))
                if (data1.getTime() > data2.getTime() && dtFinal.length > 0) {
                    msg = "A data De deve ser menor ou igual a data Até!";
                }
        }

        if ($("#DataCadastramentoAte").length > 0) {
            if (dtFinal.length > 0 && isValidDate(dtFinal) == false) {
                msg = "A data final é invalida!";
            }
        }

        if (msg != "") {
            HideLoading();
            AbrirModal(msg);
            return false;
        }
    }

    $("#form_filtro").submit();
}


