

function GerarCombo() {
    $("#Programa").empty(); // remove all options bar first
    $("#Programa").append("<option value='' >Selecione</option>");

    programasInfo.forEach(function (programa) {
        var ano = (programa.Ano == $("#AnoExercicio").val() || $("#AnoExercicio").val() == "" || $("#AnoExercicio").length == 0);

        if (ano) {
            var cfp = programa.Cfp.substring(0, 2) + "." +
                      programa.Cfp.substring(2, 5) + "." +
                      programa.Cfp.substring(5, 9) + "." +
                      programa.Cfp.substring(9, 13) + " ";

            $("#Programa").append("<option value='" + programa.Codigo + "' >" + cfp + programa.Descricao + "</option>");
            $('#Programa option[value="' + selecionado + '"]').attr({ selected: "selected" });
        }
    });

    ValicarCampos($("#Programa"));
}

function SelecionarComboPtres() {
    selecionado = 0;

    $("#Ptres").empty(); // remove all options bar first
    $("#Ptres").append("<option value='' >Selecione</option>");
    var ptres;

    programasInfo = programasInfo.sort(dynamicSort("Ptres"));
    programasInfo.forEach(function (programa) {
        var ano = $("#AnoExercicio").val() != "" && $("#AnoExercicio").length > 0
           ? programa.Ano == $("#AnoExercicio").val()
           : programa.Ano == new Date().getFullYear() || (programa.Ano == new Date().getFullYear() - 1);

        if (ano) {
            ptres = programa.Ptres;
            $("#Ptres").append("<option value='" + programa.Ptres + "' >" + programa.Ptres + "</option>");
            selecionado = programa.Codigo == $("#Programa").val() ? programa.Ptres : 0;
            $('#Ptres option[value="' + selecionado + '"]').attr({ selected: "selected" });

        }
    });

    ValicarCampos($("#Ptres"));
}

function SelecioarComboCfp() {
    selecionado = 0;
    $("#Programa").empty();
    $("#Programa").append("<option value='' >Selecione</option>");

    programasInfo = programasInfo.sort(dynamicSort("Cfp"));
    programasInfo.forEach(function (programa) {
        var ano = $("#AnoExercicio").val() != "" && $("#AnoExercicio").length > 0
            ? programa.Ano == $("#AnoExercicio").val()
            : programa.Ano == new Date().getFullYear() || (programa.Ano == new Date().getFullYear() - 1);

        if (ano) {
            var cfp = programa.Cfp.substring(0, 2) + "." +
                      programa.Cfp.substring(2, 5) + "." +
                      programa.Cfp.substring(5, 9) + "." +
                      programa.Cfp.substring(9, 13) + " ";

            selecionado = programa.Ptres == $("#Ptres").val() ? programa.Codigo : 0;

            $("#Programa").append("<option value='" + programa.Codigo + "' >" + cfp + programa.Descricao + "</option>");
            $('#Programa option[value="' + selecionado + '"]').attr('selected', true);
        }
    });

    ValicarCampos($("#Programa"));
}

function GerarComboPtres() {
    selecionado = 0;

    $("#Ptres").empty(); // remove all options bar first
    $("#Ptres").append("<option value='' >Selecione</option>");

    programasInfo = programasInfo.sort(dynamicSort("Ptres"));
    programasInfo.forEach(function (programa) {
        var ano = $("#AnoExercicio").val() != "" && $("#AnoExercicio").length > 0
           ? programa.Ano == $("#AnoExercicio").val()
           : programa.Ano == new Date().getFullYear() || (programa.Ano == new Date().getFullYear() - 1);

        if (ano) {
            selecionado = programa.Codigo == $("#Programa").val() ? programa.Ptres : 0;

            $("#Ptres").append("<option value='" + programa.Ptres + "' >" + programa.Ptres + "</option>");
            //$('#Ptres option[value="' + selecionado + '"]').attr({ selected: "selected" });
        }
    });

    ValicarCampos($("#Ptres"));
}

function GerarComboCfp() {
    selecionado = 0;
    $("#Programa").empty();
    $("#Programa").append("<option value='' >Selecione</option>");

    programasInfo = programasInfo.sort(dynamicSort("Cfp"));
    programasInfo.forEach(function (programa) {
        var ano = $("#AnoExercicio").val() != "" && $("#AnoExercicio").length > 0
            ? programa.Ano == $("#AnoExercicio").val()
            : programa.Ano == new Date().getFullYear() || (programa.Ano == new Date().getFullYear() - 1);

        if (ano) {
            var cfp = programa.Cfp.substring(0, 2) + "." +
                      programa.Cfp.substring(2, 5) + "." +
                      programa.Cfp.substring(5, 9) + "." +
                      programa.Cfp.substring(9, 13) + " ";

            selecionado = programa.Ptres == $("#Ptres").val() ? programa.Codigo : 0;

            $("#Programa").append("<option value='" + programa.Codigo + "' >" + cfp + programa.Descricao + "</option>");
        }
    });

    ValicarCampos($("#Programa"));
}

function GerarComboNatureza() {
    $("#Natureza").empty(); // remove all options bar first
    $("#Natureza").append("<option value='' >Selecione</option>");

    estruturaInfo = estruturaInfo.sort(dynamicSort("Natureza"));

    var anoProg;
    estruturaInfo.forEach(function (estrutura) {

        var programa = programasInfo.filter(function (value) {
            if (value.Codigo == estrutura.Programa)
                return value;
        })[0];

        anoProg = programa.Ano;


        var ano = (anoProg == $("#AnoExercicio").val() || $("#AnoExercicio").val() == "" || $("#AnoExercicio").length == 0);
        var cfp = (estrutura.Programa == $("#Programa").val() || $("#Programa").val() == "");

        if (cfp && ano) {
            selecionado = estrutura.Codigo;
            $("#Natureza").append("<option value='" + estrutura.Codigo + "' >" + estrutura.Natureza.replace(/(\d{1})(\d{1})(\d{2})(\d{2})$/, "$1.$2.$3.$4") + " - " + estrutura.Fonte + " - " + estrutura.Nomenclatura + "</option>");
        }
    });

    ValicarCampos($("#Natureza"));
}


function GerarAplicacao(id) {

    if ($('#Contrato').length > 0)
        if ($('#Contrato').val().length == 0) {
            estruturaInfo.forEach(function(estrutura) {
                if (estrutura.Codigo == $("#Natureza").val()) {
                    $("#" + id).val(estrutura.Aplicacao.replace(/(\d)(\d{1})$/, "$1-$2"));
                }
            });
        
            ValicarCampos($("#Obra"));
        }
}

function GerarUGO() {
    regionais.forEach(function (regional) {
        if (regional.Id == $("#Regional").val()) {
            $("#Ugo").val(regional.Uge);
        }
    });

    ValicarCampos($("#Ugo"));
}
