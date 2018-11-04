var dadosReserva;
var contrato;
var table;
var dadosEmpenho;
var numOc = "";
var tipo = "";
var cfp = "";
var CED = "";
var numCt = "";
var origemConsulta = "";
var dadosDatabase;
var tipoContrato = "0";
var EntitySubempenho;
var subempenhoSelec = "";

$(document).ready(function () {

    // Consultar Especificação de despesas
    $("#EspecDespesa").change(function BuscarEsp() {
        var valor = $("div >#EspecDespesa").val();
        if ((valor != '000') && (valor != '') && valor.length === 3) { //caso campo ja vier preenchido terá o valor 000 e não será realizada a busca
            EspecificacaoDespesa(valor);
        }
    });

    $("#CodAssAutorizado").change(function BuscarAssinatura() {
        var valor = $("div >#CodAssAutorizado").val();

        tipo = 1;
        LimparCombo(tipo);

        if (valor.length != 0 & valor.length === 5 & valor != "") {
            ConsultarAssinatura(valor, tipo);
        }
    });

    $("#CodAssExaminado").change(function BuscarAssinatura() {
        var valor = $("div >#CodAssExaminado").val();
        tipo = 2;
        LimparCombo(tipo);
        if (valor.length != 0 & valor.length === 5 & valor != "") {
            ConsultarAssinatura(valor, tipo);
        }
    });

    $("#CodAssResponsavel").change(function BuscarAssinatura() {
        var valor = $("div >#CodAssResponsavel").val();
        tipo = 3;
        LimparCombo(tipo);
        if (valor.length != 0 & valor.length === 5 & valor != "") {
            ConsultarAssinatura(valor, tipo);
        }
    });

});

function ConsultarContrato() {

    origemConsulta = "contrato";
    var online = navigator.onLine;

    if (online === false) {
        AbrirModal("Erro de conexão");
        return false;
    }

    var num = $("#Contrato").val().trim() === "" ? $("#NumeroContrato").val() : $("#Contrato").val();

    var numero = num.replace(/[\s*\.-]/g, "");
    var numcontrato = contrato == null ? "" : contrato.OutContrato.replace(/\s/g, "").replace(/[\.-]/g, "");

    if (numero.length === 0) {
        AbrirModal("Favor informar número do contrato!");
        return false;
    } else if (numero == numcontrato) {

        waitingDialog.show('Consultando');
        waitingDialog.hide();
        $('#modalConsultaContrato').modal("toggle");
        $('input#Contrato').val(contrato.OutContrato.replace(/\s/g, "").replace(/[\.-]/g, "").replace(/(\d{2})(\d{2})(\d{5})(\d{1})/g, "\$1.\$2.\$3\-\$4"));
        $('input#txtCNPJ').val(contrato.OutCpfcnpj);
        $('input#txtObra').val(contrato.OutCodObra);
        $('input#txtContratada').val(contrato.OutContratada);
        $('input#txtObjeto').val(contrato.OutObjeto);
        $('input#ProcessoSiafem').val(contrato.OutProcesSiafem);
        $('input#txtPragmatica').val(contrato.OutPrograma);
        $('input#txtCED').val(contrato.OutCED);

        GerarTabela(table);
    } else {

        table = null;


        var contratoItem = {
            NumContrato: numero,
            Type: tipoContrato
        }

        waitingDialog.show('Consultando');
        $.ajax({
            datatype: 'json',
            type: 'Post',
            url: "/ConsutasBase/ConsultarContrato",
            cache: false,
            async: true,
            data: JSON.stringify({ contrato: contratoItem }),
            contentType: "application/json; charset=utf-8",
            success: function (dados) {
                if (dados.Status == "Sucesso") {

                    waitingDialog.hide();

                    contrato = dados.Contrato;
                    table = dados.Contrato.ListConsultaContrato;

                    $('#modalConsultaContrato').modal();

                    $('input#Contrato').val(contrato.OutContrato.replace(/\s/g, "").replace(/[\.-]/g, "").replace(/(\d{2})(\d{2})(\d{5})(\d{1})/g, "\$1.\$2.\$3\-\$4"));
                    $('input#txtCNPJ').val(contrato.OutCpfcnpj);
                    $('input#txtObra').val(contrato.OutCodObra);
                    $('input#txtContratada').val(contrato.OutContratada);
                    $('input#txtObjeto').val(contrato.OutObjeto);
                    $('input#ProcessoSiafem').val(contrato.OutProcesSiafem);
                    $('input#txtPragmatica').val(contrato.OutPrograma);
                    $('input#txtCED').val(contrato.OutCED);

                    GerarTabela(table);

                } else {
                    var msg = dados.Msg;
                    if (dados.Msg == null)
                        msg = "Erro na comunicação com servidor, Tente novamente.";

                    waitingDialog.hide();
                    AbrirModal(msg);
                }
            },
            error: function (dados, exception) {
                waitingDialog.hide();
                ErrorAjax(dados, exception);
            }
        });
    }
}


function ConsultarPorEstruturaMacroInfo() {
    var result;
    estruturaInfo.forEach(function (estrutura) {
        if (estrutura.Programa == $("#Programa").val() && estrutura.Natureza == $("#NatDespesa").val() && estrutura.Fonte == $("#Fonte").val().substring(1,3)) {
        //if (estrutura.id_programa == programa && estrutura.cd_natureza == despesa && estrutura.id_fonte == fonte) {
            result = estrutura.Macro;
        }
    });
    return result;
}


function ConsultarPorEstruturaProgramasInfo() {
    var result;
    programasInfo.forEach(function (programa) {
        if (programa.Codigo == $("#Programa").val() && programa.Ano == $("#AnoExercicio").val()) {
            result = programa.Cfp;
        }
    });
    return result;
}

function ConsultarPorEstruturaEstruturaInfo() {
    var result;
    estruturaInfo.forEach(function (natureza) {
        if (natureza.Codigo == $("#Natureza").val()) {
            result = natureza.Natureza;
        }
    });
    return result;
}

function ConsultarPorEstruturaFonteInfo() {
    var result;
    fonteInfo.forEach(function (origem) {
        if (origem.Id == $("#Fonte").val()) {
            result = origem.Codigo;
        }
    });
    return result;
}

function ConsultaPorEstruturaRegionalInfo() {
    var result;
    regionais.forEach(function (regional) {
        if (regional.Id == $("#Regional").val()) {
            result = regional.Descricao;
        }
    });
    return result;
}

function ConsultarPorEstrutura() {

    origemConsulta = "estrutura";
    var online = navigator.onLine;

    if (online != true) {
        AbrirModal("Erro de conexão");
        return false;
    }


    var valorCfp = ConsultarPorEstruturaProgramasInfo();
    var valorNatureza = ConsultarPorEstruturaEstruturaInfo();
    var valorFonte = ConsultarPorEstruturaFonteInfo();
    var valorOrgao = ConsultaPorEstruturaRegionalInfo();

    if (valorOrgao == null) {
        AbrirModal("Favor informar número do orgão!");
        return false;
    }
    else if ($("#AnoExercicio").val() == "") {
        AbrirModal("Favor informar o Ano!");
        return false;
    }
    else if (valorCfp == null) {
        AbrirModal("Favor informar o CFP!");
        return false;
    }
    else if (valorFonte == null) {
        AbrirModal("Favor informar a Origem do Recurso!");
        return false;
    }

    var estruturaFiltrosDto = {
        AnoExercicio: $("#AnoExercicio").val(),
        RegionalId: $("#Regional").val(),
        Cfp: valorCfp,
        Natureza: valorNatureza,
        Programa: $("#Ptres").val(),
        OrigemRecurso: valorFonte,
        Processo: $("#Processo").val(),
        Tipo: tipoEstrutura
    };


    $.ajax({
        datatype: 'json',
        type: 'Post',
        url: "/ConsutasBase/ConsultarPorEstrutura",
        cache: false,
        data: JSON.stringify({ estrutura: estruturaFiltrosDto }),
        contentType: "application/json; charset=utf-8",
        beforeSend: function () {
            waitingDialog.show('Consultando');
        },
        success: function (dados) {
            if (dados.Status == "Sucesso") {
                $('#modalConsultaPorEstrutura').modal("toggle");
                $('input#txtAno').val($("#AnoExercicio").val());
                $('input#txtOrgao').val(valorOrgao);
                $('input#txtEstrutura').val(valorCfp);

                GerarTabelaEstrutura(dados.Estrutura.ListConsultaEstrutura); //propriedade List da classe : "ConsultaReservaEstrutura"
            }
            else {
                var msg = dados.Msg;
                if (dados.Msg == null)
                    msg = "Erro na comunicação com servidor, Tente novamente.";

                waitingDialog.hide();
                AbrirModal(msg);
            }
        },
        error: function (dados, exception) {
            waitingDialog.hide();
            ErrorAjax(dados, exception);
        },
        complete: function () {
            waitingDialog.hide();
        }
    });
}

function GerarTabelaEstrutura(values) {

    $('#tblListaEstrutura > thead').remove();
    $('#tblListaEstrutura').append("<thead></thead>");

    if (tipoEstrutura == "Reserva") {
        $('#tblListaEstrutura > thead').append("<tr>" +
            "    <th class='text-center'>Nº Res.</th>" +
            "    <th class='text-center'>CED</th>" +
            "    <th class='text-right'>Cod. Apl.</th>" +
            "    <th class='text-center'>Dt. Inicial</th>" +
            "    <th class='text-right'>Valor Atual</th>" +
            "    <th class='text-right'>Valor Empenhado</th>" +
            "    <th class='text-right'>Disp. A Empenhar</th>" +
            "    <th class='text-center'></th>" +
            "</tr>");

        if (!$("#tblListaEstrutura").hasClass("_tbDataTables")) {
            $('#tblListaEstrutura').addClass("_tbDataTables");
            BuildDataTables.ById('#tblListaEstrutura');
        }

        $('#tblListaEstrutura').DataTable().clear().draw(true);
        $.each(values,
            function (index, x) {
                $('#tblListaEstrutura')
                    .DataTable()
                    .row.add([
                        x.OutNrReserva,
                        x.OutCED,
                        x.OutCodAplicacao,
                        x.OutDataInic,
                        x.OutValorAtual,
                        x.OutValorEmpenhado,
                        x.OutDispEmpenhar,
                        "<button class='btn btn-xs btn-primary' title='Visualizar' onclick='DadosReserva(" +
                        x.OutNrReserva +
                        ",0)'><i class='fa fa-search'></i></button>"
                    ])
                    .draw(true);
            });
    } else {
        $('#tblListaEstrutura > thead').append("<tr>" +
            "    <th class='text-center'>Nº Empenho.</th>" +
            "    <th class='text-right'>Cod. Apl.</th>" +
            "    <th class='text-right'>Valor Atual</th>" +
            "    <th class='text-right'>Valor Sub-Empenhado</th>" +
            "    <th class='text-right'>Disp. A Empenhar</th>" +
            "    <th class='text-center'></th>" +
            "</tr>");

        if (!$("#tblListaEstrutura").hasClass("_tbDataTables")) {
            $('#tblListaEstrutura').addClass("_tbDataTables");
            BuildDataTables.ById('#tblListaEstrutura');
        }

        $('#tblListaEstrutura').DataTable().clear().draw(true);
        $.each(values,
            function (index, x) {
                $('#tblListaEstrutura')
                    .DataTable()
                    .row.add([
                        x.OutNrEmpenho,
                        x.OutCodAplicacao,
                        x.OutValorAtual,
                        x.OutValorSubEmpenhado,
                        x.OutDispSubEmpenhar,
                        "<button class='btn btn-xs btn-primary' title='Visualizar' onclick='DadosEmpenho(" +
                        x.OutNrEmpenho +
                        ",0)'><i class='fa fa-search'></i></button>"
                    ])
                    .draw(true);
            });
    }
}

function GerarTabela(values) {
    $('#tblListaContrato').DataTable().clear().draw(true);
    $.each(values,
        function (index, x) {
            var button = serviceConsultaDetalheProvider([x]);
            $('#tblListaContrato').DataTable().row.add([
                x.OutEvento,
                x.OutNumero,
                x.OutData,
                x.OutValor,
              button
            ]).draw(true);
        });
}

function buttonConsultaProvider(action, value, value2) {

    value2 = ((value2.toString().indexOf(",") >= 0) || (value2.toString().indexOf(".") >= 0)) ? value2.toString().replace(/[\.,]/g, "") : value2.toString();

    if (tipoContrato === "0304") {

        //$('#btnConfirmarContratoTodos').hide();
        action = "ConfirmarDesdobramento";
        //return "<button class='btn btn-warning'".concat(action.length > 0
        //    ? `onclick='${action}(${value},${value2}, event)'` : ''
        //, "><i>Confirmar</i></button>");

        return "<button class='btn btn-warning'".concat(action.length > 0 ? "onclick='" + action + "(" + value + "," + value2 + ", event)'" : '', "><i>Confirmar</i></button>");

    }
    if (tipoContrato === 4) {
        return "<button class='btn btn-xs btn-warning'".concat(action.length > 0 ? "onclick='" + action + "(" + value + "," + value2 + ", event)'" : '', "><i>Confirmar</i></button>");
    }
    else {
        //return "<button class='btn btn-xs btn-primary'".concat(action.length > 0
        //    ? `onclick='${action}(${value},${value2}, event)'` : ''
        //, "><i class='fa fa-search'></i></button>");

        return "<button class='btn btn-xs btn-primary'".concat(action.length > 0
            ? "onclick='" + action + "(" + value + "," + value2 + ", event)'" : ''
        , "><i class='fa fa-search'></i></button>");

    }
}

function serviceConsultaDetalheProvider(entity) {
    switch (entity[0].OutEvento) {
        case 'RESERVA': //1
            return buttonConsultaProvider('DadosReserva', entity[0].OutNumero, 1);
        case 'EMPENHO': //2
            return buttonConsultaProvider('DadosEmpenho', entity[0].OutNumero, 1);
        case 'SUBEMPENHO': //3
            return buttonConsultaProvider('DadosSubempenho', entity[0].OutNumero, 1);
        case 'REQUISICAO DE RAP': //4
            return buttonConsultaProvider('ConfirmarRap', entity[0].OutNumero, String(entity[0].OutValor));
        case 'DESP.EXTRA ORCAMENTARIA': //5
            return buttonConsultaProvider('DadosSubempenho', entity[0].OutNumero, 1);
        case 'ORDEM DE PAGAMENTO': //6
        case 'CAUCAO': //7
        case 'RECEITA': //8
        default:
            return buttonConsultaProvider('', '');
    }
}

function DadosReserva(reserva, type) {
    if (navigator.onLine != true) {
        AbrirModal("Erro de conexão");
        return false;
    }

    if (reserva == undefined || reserva == null || reserva == 0) {
        AbrirModal("Favor informar o número da Reserva!");
        return false;
    }

    $.ajax({
        datatype: 'json',
        type: 'Post',
        url: "/ConsutasBase/ConsultarReserva",
        cache: false,
        data: JSON.stringify({ reserva: reserva }),
        contentType: "application/json; charset=utf-8",
        beforeSend: function () {
            waitingDialog.show('Consultando');
        },
        success: function (dados) {
            if (dados.Status == "Sucesso") {
                dadosReserva = dados.Reserva;
                dadosDatabase = dados.ReservaFromDatabase;

                CED = dadosReserva.OutCed1 + "." +
                    dadosReserva.OutCed2 + "." +
                    dadosReserva.OutCed3 + "." +
                    dadosReserva.OutCed4 + "." +
                    dadosReserva.OutCed5;
                if (type == 0) {
                    origemConsulta = "reserva";
                } else if (type == 1) {
                    $('#modalConsultaContrato').modal("toggle");
                }

                cfp = dadosReserva.OutCfp1 + "." +
                    dadosReserva.OutCfp2 + "." +
                    dadosReserva.OutCfp3 + "." +
                    dadosReserva.OutCfp4 + "." +
                    dadosReserva.OutCfp5;

                $('#modalDadosReserva').modal();

                $('input#txtNumReserva').val(dadosReserva.OutNumReserva);
                $('input#txtNumProcesso').val(dadosReserva.OutNumProcesso);
                $('input#txtCfp').val(cfp + "/" + CED);
                $('input#txtNumAplicacao').val(dadosReserva.OutCodAplicacao);
                $('input#txtDataEmissao').val(dadosReserva.OutDataEmissao);
                $('input#txtPrevisaoInicio').val(dadosReserva.OutPrevInic);
                $('input#txtDestino').val(dadosReserva.OutDestRecurso);
                $('input#txtCodObra').val(dadosReserva.OutCodObra);
                $('input#txtValor').val(dadosReserva.OutValorReserva);
                $('input#txtNumContrato').val(dadosReserva.OutIdentContrato);
                $('input#txtValorReforco').val(dadosReserva.OutValorReforco);
                $('input#txtValorAnulado').val(dadosReserva.OutValorAnulado);
                $('input#txtValorEmprenhado').val(dadosReserva.OutValorEmpenhado);
                $('input#txtSeqTam').val(dadosReserva.OutSeqTam);
                $('input#ValorReserva').val(dadosReserva.OutSaldoReserva);
                $('input#txtQuota1').val(dadosReserva.OutSaldoQ1);
                $('input#txtQuota2').val(dadosReserva.OutSaldoQ2);
                $('input#txtQuota3').val(dadosReserva.OutSaldoQ3);
                $('input#txtQuota4').val(dadosReserva.OutSaldoQ4);
                $('textarea#txtEspecificacao').text(dadosReserva.OutEspecDespesa1 + "\n" +
                    dadosReserva.OutEspecDespesa2 + "\n" +
                    dadosReserva.OutEspecDespesa3 + "\n" +
                    dadosReserva.OutEspecDespesa4 + "\n" +
                    dadosReserva.OutEspecDespesa5 + "\n" +
                    dadosReserva.OutEspecDespesa5 + "\n" +
                    dadosReserva.OutEspecDespesa7 + "\n" +
                    dadosReserva.OutEspecDespesa8 + "\n" +
                    dadosReserva.OutEspecDespesa9);
            } else {
                var msg = dados.Msg;
                if (dados.Msg == null)
                    msg = "Erro na comunicação com servidor, Tente novamente.";

                waitingDialog.hide();
                AbrirModal(msg);
            }
        },
        error: function (dados, exception) {
            waitingDialog.hide();
            ErrorAjax(dados, exception);
        },
        complete: function () {
            waitingDialog.hide();
        }
    });
}

function ConfirmarReserva() {
    var msg = "";
    if (navigator.onLine != true) {
        AbrirModal("Erro de conexão");
        return false;
    }

    $('input#Reserva').val(dadosReserva.OutNumReserva);
    $('input#Processo').val(dadosReserva.OutNumProcesso);

    var obra = dadosReserva.OutCodObra == ""
        ? dadosReserva.OutCodAplicacao.replace(/[\.-]/g, "")
        : dadosReserva.OutCodObra.replace(/[\.-]/g, "");

    $('input#Obra').val(obra.replace(/(\d)(\d{1})$/, "$1-$2"));

    $('input#Obra').addClass("isLocked");
    RefazerCombo("#Fontes");

    $('#Fontes option[value="' + dadosReserva.OutDestRecurso + '"]').attr("selected", true);


    if (dadosReserva.OutIdentContrato.length > 0) {
        $('input#Contrato').val(dadosReserva.OutIdentContrato.replace(/\s/g, "").replace(/[\.-]/g, "").replace(/(\d{2})(\d{2})(\d{5})(\d{1})/g, "\$1.\$2.\$3\-\$4"));
        $('input#NumeroContrato').val(dadosReserva.OutIdentContrato.replace(/\s/g, "").replace(/[\.-]/g, "").replace(/(\d{2})(\d{2})(\d{5})(\d{1})/g, "\$1.\$2.\$3\-\$4"));
    }

    if (dadosDatabase != null) {
        //obtido no Sids
        RefazerCombo("#Fonte");
        $('#Fonte option[value="' + dadosDatabase.Fonte + '"]').attr("selected", true);
        $('input#Oc').val(dadosDatabase.Oc);
        $('input#Ugo').val(dadosDatabase.Ugo);
        $('input#Uo').val(dadosDatabase.Uo);

        if (dadosDatabase.Contrato != null) {
            $('input#Contrato').val(dadosDatabase.Contrato.replace(/\s/g, "").replace(/[\.-]/g, "").replace(/(\d{2})(\d{2})(\d{5})(\d{1})/g, "\$1.\$2.\$3\-\$4"));
            $('input#NumeroContrato').val(dadosDatabase.Contrato.replace(/\s/g, "").replace(/[\.-]/g, "").replace(/(\d{2})(\d{2})(\d{5})(\d{1})/g, "\$1.\$2.\$3\-\$4"));
        }

    }

    var prog = cfp.replace(/[\.-]/g, "");
    GerarComboCfp();
    var cont = 0;

    programasInfo.forEach(function (programa) {
        if (programa.Cfp == prog.substr(0, 13) && programa.Ano == ($("#AnoExercicio").length <= 0 ? year : $("#AnoExercicio").val())) {
            $('#Programa option[value="' + programa.Codigo + '"]').attr("selected", true);
            cont += 1;
        }
    });

    SelecionarComboPtres();

    var natureza = CED.replace(/[\.-]/g, "");

    if (cont == 0)
        msg = "Programa " + prog.substr(0, 13) + " não Cadastrado no SIDS";

    GerarComboNatureza();
    var contN = 0;
    estruturaInfo.forEach(function (estrutura) {
        if (estrutura.Natureza == natureza && estrutura.Fonte == prog.substr(13, 2)) {
            $('#Natureza option[value="' + estrutura.Codigo + '"]').attr("selected", true);
            contN += 1;
        }
    });

    if (contN == 0)
        msg = (msg.length === 0 ? "\n" : "") + "CED " + natureza + '-' + prog.substr(13, 2) + " não Cadastrada no SIDS";

    if (msg.length > 0)
        AbrirModal(msg);


    $("#DescEspecificacaoDespesa > .Proximo")[0].value = removerAcentos(dadosReserva.OutEspecDespesa1);
    $("#DescEspecificacaoDespesa > .Proximo")[1].value = removerAcentos(dadosReserva.OutEspecDespesa2);
    $("#DescEspecificacaoDespesa > .Proximo")[2].value = removerAcentos(dadosReserva.OutEspecDespesa3);
    $("#DescEspecificacaoDespesa > .Proximo")[3].value = removerAcentos(dadosReserva.OutEspecDespesa4);
    $("#DescEspecificacaoDespesa > .Proximo")[4].value = removerAcentos(dadosReserva.OutEspecDespesa5);
    $("#DescEspecificacaoDespesa > .Proximo")[5].value = removerAcentos(dadosReserva.OutEspecDespesa6);
    $("#DescEspecificacaoDespesa > .Proximo")[6].value = removerAcentos(dadosReserva.OutEspecDespesa7);
    $("#DescEspecificacaoDespesa > .Proximo")[7].value = removerAcentos(dadosReserva.OutEspecDespesa8);
    $("#DescEspecificacaoDespesa > .Proximo")[8].value = removerAcentos(dadosReserva.OutEspecDespesa9);

    $("#EspecDespesa").val("000");

    if ($("#modalConsultaPorEstrutura").is(":visible")) {
        $('#modalConsultaPorEstrutura').modal("toggle");
    }
    $("#resultadoConsulta").show();

    ValicarCampos("campo");


    if (Left(natureza, 1) == "4" || (Left(natureza, 1) == "3" && natureza.substr(4, 2) == "39")) {
        $("#dadosObra").show();
    }
    else {
        $("#dadosObra").hide();
    }

}

function DadosEmpenho(empenho, type, callback) {

    var online = navigator.onLine;

    if (online === true) {
    } else {
        AbrirModal("Erro de conexão");
        return false;
    }

    waitingDialog.show('Consultando');

    $.ajax({
        datatype: 'json',
        type: 'Post',
        url: "/ConsutasBase/ConsultarEmpenho",
        cache: false,
        async: true,
        data: JSON.stringify({ empenho: empenho }),
        contentType: "application/json; charset=utf-8",
        success: function (dados) {

            if (dados.Status == "Sucesso") {
                dadosEmpenho = dados.Empenho;
                dadosDatabase = dados.EmpenhoFromDatabase;

                if (type == 0) {

                    origemConsulta = "reserva";
                } else if (type == 1) {

                    $('#modalConsultaContrato').modal("toggle");
                }
                if (type != 3) {

                    $('#modalDadosEmpenho').modal();

                    CED = dadosEmpenho.OutCed1 +
                        "." +
                        dadosEmpenho.OutCed2 +
                        "." +
                        dadosEmpenho.OutCed3 +
                        "." +
                        dadosEmpenho.OutCed4 +
                        "." +
                        dadosEmpenho.OutCed5;
                    cfp = dadosEmpenho.OutCfp1 +
                        "." +
                        dadosEmpenho.OutCfp2 +
                        "." +
                        dadosEmpenho.OutCfp3 +
                        "." +
                        dadosEmpenho.OutCfp4 +
                        "." +
                        dadosEmpenho.OutCfp5;

                    $('input#txtNumEmpenho').val(dadosEmpenho.NumEmpenho);
                    $('input#txtNeSiafem').val(dadosEmpenho.OutNeSiafem);
                    $('input#txtFonteSiafem').val(dadosEmpenho.OutFonteSiafem);
                    $('input#txtNumProcesso').val(dadosEmpenho.OutNumProcesso);
                    $('input#txtCfp').val(cfp + " / " + CED);
                    $('input#txtNumAplicacao').val(dadosEmpenho.OutCodAplicacao);
                    $('input#txtDataEmissao').val(dadosEmpenho.OutDataEmissao);
                    $('input#txtPrevisaoInicio').val(dadosEmpenho.OutPrevInic);
                    $('input#txtOrigem').val(dadosEmpenho.OutOrigemRecurso);
                    $('input#txtDestino').val(dadosEmpenho.OutDestRecurso);
                    $('input#txtCodObra').val(dadosEmpenho.OutCodObra);
                    $('input#txtNumContrato').val(dadosEmpenho.OutNumContrato);
                    $('input#txtValor').val(dadosEmpenho.OutValorEmpenho);
                    $('input#txtValorReforco').val(dadosEmpenho.OutValorReforco);
                    $('input#txtValorAnulado').val(dadosEmpenho.OutValorAnulado);
                    $('input#txtValorSubmpenhado').val(dadosEmpenho.OutValorSubEmpenhado);
                    $('input#txtSaldoEmpenho').val(dadosEmpenho.OutSaldoEmpenho);
                    $('input#txtSeqTam').val(dadosEmpenho.OutSeqTam);
                    $('input#txtQuota1').val(dadosEmpenho.OutSaldoQ1);
                    $('input#txtQuota2').val(dadosEmpenho.OutSaldoQ2);
                    $('input#txtQuota3').val(dadosEmpenho.OutSaldoQ3);
                    $('input#txtQuota4').val(dadosEmpenho.OutSaldoQ4);
                    $('textarea#txtEspecificacao')
                        .text(dadosEmpenho.OutEspecDespesa1 +
                            "\n" +
                            dadosEmpenho.OutEspecDespesa2 +
                            "\n" +
                            dadosEmpenho.OutEspecDespesa3 +
                            "\n" +
                            dadosEmpenho.OutEspecDespesa4 +
                            "\n" +
                            dadosEmpenho.OutEspecDespesa5 +
                            "\n" +
                            dadosEmpenho.OutEspecDespesa5 +
                            "\n" +
                            dadosEmpenho.OutEspecDespesa7 +
                            "\n" +
                            dadosEmpenho.OutEspecDespesa8 +
                            "\n" +
                            dadosEmpenho.OutEspecDespesa9);
                    $('input#txtCNPJ').val(dadosEmpenho.OutCnpjCpf);
                    $('input#txtCredor').val(dadosEmpenho.OutCredor);
                    $('input#txtEndereco').val(dadosEmpenho.OutEndereco);
                    $('input#txtBairro').val(dadosEmpenho.OutBairro);
                    $('input#txtMunicipio').val(dadosEmpenho.OutMunicipio);
                    $('input#txtEstado').val(dadosEmpenho.OutEstado);
                    $('input#txtCEP').val(dadosEmpenho.OutCep);
                } else {

                    //if (window.controller === 'Subempenho') {
                    $('#InscricaoEvento').val(dadosEmpenho.OutNeSiafem);
                    //}
                    ConfirmarEmpenho();
                }
                waitingDialog.hide();

                if (callback) {
                    callback();
                }
            } else {

                var msg = dados.Msg;
                if (dados.Msg == null)
                    msg = "Erro na comunicação com servidor, Tente novamente.";

                waitingDialog.hide();
                AbrirModal(msg);
            }
        },
        error: function (dados, exception) {
            waitingDialog.hide();
            ErrorAjax(dados, exception);
        }
    });

}

function dadosContrato(modal) {

    FecharModal('#' + modal);

    if (origemConsulta == "contrato") {

        $('#modalConsultaContrato').modal("toggle");

        $('input#Contrato').val(contrato.OutContrato);
        $('input#txtCNPJ').val(contrato.OutCpfcnpj);
        $('input#Obra').val(contrato.OutCodObra);
        $('input#txtContratada').val(contrato.OutContratada);
        $('input#txtObjeto').val(contrato.OutObjeto);
        $('input#ProcessoSiafem').val(contrato.OutProcesSiafem);
        $('input#txtPragmatica').val(contrato.OutPrograma);
        $('input#txtCED').val(contrato.OutCED);
        GerarTabela(table);
    } else if (origemConsulta == "estrutura") {
        $('#modalConsultaPorEstrutura').modal("toggle");
    } else if (origemConsulta == "empenho") {
        $('#modalConsultaEmpenhos').modal("toggle");
    }

}

function ConfirmarContrato() {
    var msg = "";
    var online = navigator.onLine;

    if (online === true) {
    } else {
        AbrirModal("Erro de conexão");
        return false;
    }

    var obra = contrato.OutCodObra.replace(/[\.-]/g, "");

    $('input#Obra').val(obra.replace(/(\d)(\d{1})$/, "$1-$2"));

    $('input#Obra').addClass("isLocked");

    $('input#Processo').val(contrato.OutProcesSiafem);


    var obraSelecionada = $('#txtObra', '#modalConsultaContrato ').val();
    $('#CodigoAplicacaoObra', '#divPainelCadastrar').val(obraSelecionada);

    if (programasInfo) {
        GerarComboCfp();
        var prog = contrato.OutPrograma.replace(/[\.-]/g, "");
        var cont = 0;
        var ano = $("#AnoExercicio").length === 0 || $("#AnoExercicio").val() === "" ? year : $("#AnoExercicio").val();
        programasInfo.forEach(function (programa) {

            if (programa.Cfp == prog.substr(0, 13) && programa.Ano == ano) {

                $('#Programa option[value="' + programa.Codigo + '"]').attr("selected", true);
                cont += 1;

            }
        });

        var natureza = contrato.OutCED.replace(/[\.-]/g, "");

        if (cont == 0)
            msg = "Programa " + prog.substr(0, 13) + " não Cadastrado no SIDS";

        if (estruturaInfo) {
            GerarComboNatureza();

            if (typeof estrutura !== "undefined") {
                var contN = 0;
                estruturaInfo.forEach(function (estrutura) {

                    if (estrutura.Natureza == natureza && estrutura.Fonte == prog.substr(13, 2)) {
                        $('#Natureza option[value="' + estrutura.Codigo + '"]').attr({ selected: "selected" });
                        contN += 1;
                    }
                });

                if (contN == 0)
                    msg = (msg.length === 0 ? "\n" : "") + "CED " + natureza + '-' + prog.substr(13, 2) + " não Cadastrada no SIDS";

                if (msg.length > 0)
                    AbrirModal(msg);
            }
        }

        SelecionarComboPtres();
    }

    $("#CodigoCredorOrganizacaoId").val(contrato.OutTipo);
    $("#NumeroCNPJCPFFornecedorId").val(contrato.OutCpfcnpj);
    $("#NumeroOriginalProdesp").val('');


    ValicarCampos("campo");
}

function ConfirmarEmpenho() {
    var online = navigator.onLine;

    if (online != true) {
        AbrirModal("Erro de conexão");
        return false;
    }

    if (controller.indexOf("Empenho") < 0 && controller.indexOf("Reserva") < 0) {
        ConfirmarEmpenhoSubEmpenhoCtrl();
    } else {

        $('input#CodigoEmpenho').val(dadosEmpenho.NumEmpenho);
        $('input#Processo').val(dadosEmpenho.OutNumProcesso);
        $('input#CodigoEmpenhoOriginal').val(dadosEmpenho.OutNeSiafem);

        RefazerCombo("#CodigoFonteSiafisico");
        $('#CodigoFonteSiafisico option[value="' + dadosEmpenho.OutFonteSiafem + '"]').attr('selected', true);

        $('input#Obra').val(dadosEmpenho.OutCodAplicacao);

        //$('input#DataEntregaMaterialSiafisico').val(dadosEmpenho.OutPrevInic);

        RefazerCombo("#DestinoId");
        $('#DestinoId option[value="' + dadosEmpenho.OutDestRecurso + '"]').attr('selected', true);

        $('input#Obra').val(dadosEmpenho.OutCodObra);

        $('input#fornecedor').val(dadosEmpenho.OutCnpjCpf.split(" ")[0]);

        if (dadosEmpenho.OutCnpjCpf.split(" ")[0].length > 11) {
            $('input#credor').val(dadosEmpenho.OutCnpjCpf.split(" ")[0]);
            $('input#NumeroCNPJCPFFornecedor').val(dadosEmpenho.OutCnpjCpf.split(" ")[0]);
        } else if (dadosEmpenho.OutCnpjCpf.split(" ")[0].length <= 11) {
            if (TestaCPF(dadosEmpenho.OutCnpjCpf.split(" ")[0]) == true) {
                $('input#credor').val(dadosEmpenho.OutCnpjCpf.split(" ")[0]);
                $('input#NumeroCNPJCPFFornecedor').val(dadosEmpenho.OutCnpjCpf.split(" ")[0]);
            } else {
                $('input#CodigoUnidadeGestoraFornecedora').val(Left(dadosEmpenho.OutCnpjCpf.split(" ")[0], 6));
                $('input#NumeroCNPJCPFFornecedor').val(Left(dadosEmpenho.OutCnpjCpf.split(" ")[0], 6));
                $('input#CodigoUnidadeGestoraFornecedora').val(Right(dadosEmpenho.OutCnpjCpf.split(" ")[0], 5));
                $('input#CodigoGestaoCredor').val(Right(dadosEmpenho.OutCnpjCpf.split(" ")[0], 5));

            }

        }

        if (dadosEmpenho.OutNumContrato.length > 0) {
            $('input#Contrato').val(dadosEmpenho.OutNumContrato.replace(/\s/g, "").replace(/[\.-]/g, "").replace(/(\d{2})(\d{2})(\d{5})(\d{1})/g, "\$1.\$2.\$3\-\$4"));
            $('input#NumeroContrato').val(dadosEmpenho.OutNumContrato.replace(/\s/g, "").replace(/[\.-]/g, "").replace(/(\d{2})(\d{2})(\d{5})(\d{1})/g, "\$1.\$2.\$3\-\$4"));
        }



        if (dadosDatabase != null) {
            //Correção: trazia o status do banco, modificado para permanecer o status que esta selecionado na tela.

            dadosDatabase.TransmitirProdesp = $("#transmitirProdesp").is(":checked")
            dadosDatabase.TransmitirSiafem = $("#transmitirSIAFEM").is(":checked")
            dadosDatabase.TransmitirSiafisico = $("#transmitirSIAFISICO").is(":checked")

            AtualizarEmpenhoDataBase(dadosDatabase);
        }


        $('input#DescricaoLogradouroEntrega').val(dadosEmpenho.OutEndereco);
        $('input#DescricaoBairroEntrega').val(dadosEmpenho.OutBairro);
        $('input#CodigoMunicipio').val(dadosEmpenho.OutMunicipio.length === 3 ? "0" + dadosEmpenho.OutMunicipio : dadosEmpenho.OutMunicipio);

        $('input#NumeroCEPEntrega').val(dadosEmpenho.OutCep);

        var obra = dadosEmpenho.OutCodObra == "" ? dadosEmpenho.OutCodAplicacao.replace(/[\.-]/g, "") : dadosEmpenho.OutCodObra.replace(/[\.-]/g, "");

        $('input#Obra').val(obra.replace(/(\d)(\d{1})$/, "$1-$2"));
        $('input#Obra').addClass("isLocked");

        RefazerCombo("#Fontes");
        $('#Fontes option[value="' + dadosEmpenho.OutDestRecurso + '"]').attr('selected', true);

        $('#Fontes option[value="' + dadosEmpenho.OutDestRecurso + '"]').attr({ selected: "selected" });

        RefazerCombo("#Fonte");
        var fontId = $("#Fonte option:contains('" + dadosEmpenho.OutFonteSiafem + "')").val();
        $("#Fonte option[value='" + fontId + "']").attr('selected', "selected");

        var prog = dadosEmpenho.OutCfp1 + dadosEmpenho.OutCfp2 + dadosEmpenho.OutCfp3 + dadosEmpenho.OutCfp4;
        SelecioarComboCfp();
        var cont = 0;
        var ano = $("#AnoExercicio").length === 0 || $("#AnoExercicio").val() === "" ? year : $("#AnoExercicio").val();

        programasInfo.forEach(function (programa) {

            if (programa.Cfp == prog && programa.Ano == ano) {

                $('#Programa option[value="' + programa.Codigo + '"]').attr('selected', true);
                cont += 1;
            }
        });

        if ($('#Programa').length > 0)
            if (cont == 0)
                msg = "Cfp " + prog + " não Cadastrado no SIDS";

        GerarComboNatureza();
        var natureza = CED.replace(/[\.-]/g, "");

        var msg = "";

        var contN = 0;
        estruturaInfo.forEach(function (estrutura) {

            if (estrutura.Natureza == natureza && estrutura.Fonte == dadosEmpenho.OutFonteSiafem.substr(1, 2)) {
                $('#Natureza option[value="' + estrutura.Codigo + '"]').attr({ selected: "selected" });
                contN += 1;
            }
        });


        if ($('#Natureza').length > 0)
            if (contN == 0)
                msg = (msg.length === 0 ? "\n" : "") + "CED " + natureza + '-' + dadosEmpenho.OutFonteSiafem.substr(1, 2) + " não Cadastrada no SIDS";

        if (msg.length > 0)
            AbrirModal(msg);
        SelecionarComboPtres();



        $("#DescEspecificacaoDespesa > .Proximo")[0].value = removerAcentos(dadosEmpenho.OutEspecDespesa1);
        $("#DescEspecificacaoDespesa > .Proximo")[1].value = removerAcentos(dadosEmpenho.OutEspecDespesa2);
        $("#DescEspecificacaoDespesa > .Proximo")[2].value = removerAcentos(dadosEmpenho.OutEspecDespesa3);
        $("#DescEspecificacaoDespesa > .Proximo")[3].value = removerAcentos(dadosEmpenho.OutEspecDespesa4);
        $("#DescEspecificacaoDespesa > .Proximo")[4].value = removerAcentos(dadosEmpenho.OutEspecDespesa5);
        $("#DescEspecificacaoDespesa > .Proximo")[5].value = removerAcentos(dadosEmpenho.OutEspecDespesa6);
        $("#DescEspecificacaoDespesa > .Proximo")[6].value = removerAcentos(dadosEmpenho.OutEspecDespesa7);
        $("#DescEspecificacaoDespesa > .Proximo")[7].value = removerAcentos(dadosEmpenho.OutEspecDespesa8);
        $("#DescEspecificacaoDespesa > .Proximo")[8].value = removerAcentos(dadosEmpenho.OutEspecDespesa9);

        $("#EspecDespesa").val("000");
    }
    if ($("#modalConsultaEmpenhoCredor").is(":visible")) {
        $('#modalConsultaEmpenhoCredor').modal("toggle");
    }
    if ($("#modalConsultaPorEstrutura").is(":visible")) {
        $('#modalConsultaPorEstrutura').modal("toggle");
    }
    if ($("#modalConsultaEmpenhoSaldo").is(":visible")) {
        $('#modalConsultaEmpenhoSaldo').modal("toggle");
    }
    if ($("#modalConsultaContrato").is(":visible")) {
        $('#modalConsultaContrato').modal("toggle");
    }

    ValicarCampos("campo");
}

function ConfirmarEmpenhoSubEmpenhoCtrl() {

    var msg = "";

    $('input#NumeroEmpenhoProdesp').val(dadosEmpenho.NumEmpenho);
    $('input#NumeroOriginalProdesp').val(dadosEmpenho.NumEmpenho);

    $('input#NumeroProcesso').val(dadosEmpenho.OutNumProcesso);
    $('input#NumeroOriginalSiafemSiafisico').val(dadosEmpenho.OutNeSiafem);

    $('input#CodigoAplicacaoObra').val(dadosEmpenho.OutCodAplicacao);


    if (!$("#transmitirSIAFEM").is(":checked")) {
        $('input#ValorRealizado').val(dadosEmpenho.OutSaldoEmpenho);
    }

    var isSiafem = $("#transmitirSIAFEM").is(":checked");
    var isProdesp = $("#transmitirProdesp").is(":checked");
    var cenarioNlNlObrasSiafem = liquidacao.verificarCenarioNlNlObrasSiafem();
    if (isSiafem && isProdesp && cenarioNlNlObrasSiafem) {
        console.log("der/sids#35");
    }
    else {
        $('input#Valor').val(dadosEmpenho.OutSaldoEmpenho);
    }

    $("input#Valor").maskMoney('mask');



    $('input#CodigoAplicacaoObra').val(dadosEmpenho.OutCodObra);

    $('input#NumeroCNPJCPFFornecedorId').val(dadosEmpenho.OutCnpjCpf.split(" ")[0]);


    if (dadosEmpenho.OutCnpjCpf.split(" ")[0].length > 11) {
        if (dadosEmpenho.OutCnpjCpf.split(" ")[0].length > 14) {
            $('input#NumeroCNPJCPFCredor').val(dadosEmpenho.OutCnpjCpf.slice(1, 15))
        } else {
            $('input#NumeroCNPJCPFCredor').val(dadosEmpenho.OutCnpjCpf.split(" ")[0]);
        }
        $('input#NumeroCNPJCPFFornecedorId').val(dadosEmpenho.OutCnpjCpf.split(" ")[0]);
    } else if (dadosEmpenho.OutCnpjCpf.split(" ")[0].length <= 11) {
        if (TestaCPF(dadosEmpenho.OutCnpjCpf.split(" ")[0]) == true) {
            $('input#NumeroCNPJCPFCredor').val(dadosEmpenho.OutCnpjCpf.split(" ")[0]);
            $('input#NumeroCNPJCPFFornecedorId').val(dadosEmpenho.OutCnpjCpf.split(" ")[0]);
        } else {
            $('input#NumeroCNPJCPFFornecedorId').val(Left(dadosEmpenho.OutCnpjCpf.split(" ")[0], 6));

        }

    }

    if (dadosEmpenho.OutNumContrato.length > 0)
        $('input#Contrato').val(dadosEmpenho.OutNumContrato.replace(/\s/g, "").replace(/[\.-]/g, "").replace(/(\d{2})(\d{2})(\d{5})(\d{1})/g, "\$1.\$2.\$3\-\$4"));

    if (dadosDatabase != null) {
        $('input#CodigoUnidadeGestora').val(dadosDatabase.CodigoUnidadeGestora);
        $('input#CodigoGestao').val(dadosDatabase.CodigoGestao);

        RefazerCombo("#FonteId");
        $("#FonteId option[value='" + dadosDatabase.FonteId + "']").attr('selected', true);

        if (controller === "RapInscricao") {
            $("#Programa").val(dadosDatabase.ProgramaId);
            $("AnoExercicio").val(dadosDatabase.NumeroAnoExercicio)
            $("#CodigoCredorOrganizacao").val(dadosEmpenho.OutCnpjCpfTipo);
        } else {
            $("#CodigoCredorOrganizacaoId").val(dadosEmpenho.OutCnpjCpfTipo);
        }

        if (dadosDatabase.TransmitirSiafem == true)
            if ($("#transmitirSIAFEM").is(":checked") == false) {
                $("#transmitirSIAFISICO").removeAttr("checked");
                $("#transmitirSIAFEM").prop("checked", true);
                $("#transmitirSIAFEM").trigger("change");
            }

        if (dadosDatabase.TransmitirSiafisico == true)
            if ($("#transmitirSIAFISICO").is(":checked") == false) {
                $("#transmitirSIAFEM").removeAttr("checked");
                $("#transmitirSIAFISICO").prop("checked", true);
                $("#transmitirSIAFISICO").trigger("change");
            }


        $("#CodigoCredorOrganizacaoId").val(dadosDatabase.CodigoCredorOrganizacao);
        $("#NumeroCNPJCPFFornecedor").val(dadosDatabase.NumeroCNPJCPFFornecedor);
        $("#NumeroCNPJCPFCredor").val(dadosDatabase.NumeroCNPJCPFCredor);
        $("#NumeroCNPJCPFUGCredor").val(dadosDatabase.NumeroCNPJCPFUGCredor);
        $("#NumeroCNPJCPFCredor").val(dadosDatabase.NumeroCNPJCPFUGCredor);

        var empNum = dadosDatabase.NumeroEmpenhoSiafem == null ? dadosDatabase.NumeroEmpenhoSiafisico : dadosDatabase.NumeroEmpenhoSiafem;

        $('input#NumeroOriginalSiafemSiafisico').val(empNum);

        if (dadosDatabase.NumeroContrato != null)
            $('input#Contrato').val(dadosDatabase.NumeroContrato.replace(/\s/g, "").replace(/[\.-]/g, "").replace(/(\d{2})(\d{2})(\d{5})(\d{1})/g, "\$1.\$2.\$3\-\$4"));
    }
    else {
        $("#CodigoCredorOrganizacaoId").val(dadosEmpenho.OutCnpjCpfTipo);
    }

    var obra = dadosEmpenho.OutCodObra == "" ? dadosEmpenho.OutCodAplicacao.replace(/[\.-]/g, "") : dadosEmpenho.OutCodObra.replace(/[\.-]/g, "");

    $('input#CodigoAplicacaoObra').val(obra.replace(/(\d)(\d{1})$/, "$1-$2"));
    $('input#CodigoAplicacaoObra').addClass("isLocked");

    var prog = dadosEmpenho.OutCfp1 + dadosEmpenho.OutCfp2 + dadosEmpenho.OutCfp3 + dadosEmpenho.OutCfp4;

    if (controller === "RapInscricao") {

        SelecioarComboCfp();
        var cont = 0;
        var ano = $("#AnoExercicio").length === 0 || $("#AnoExercicio").val() === "" ? year : $("#AnoExercicio").val();

        programasInfo.forEach(function (programa) {
            if (programa.Cfp == prog && programa.Ano == ano) {
                $('#Programa option[value="' + programa.Codigo + '"]').attr('selected', true);
                cont += 1;
            }
        });

        if ($('#Programa').length > 0)
            if (cont == 0)
                msg = "Cfp " + prog + " não Cadastrado no SIDS";
        SelecionarComboPtres();

        $("#NumeroCNPJCPFFornecedor").val(dadosEmpenho.OutCnpjCpf);

    } else {

        if ($('#ProgramaId').length > 0 && $('#ProgramaId').val().length > 0) {
            SelecioarComboCfp();
            var cont = 0;
            var ano = $("#AnoExercicio").length === 0 || $("#AnoExercicio").val() === "" ? year : $("#AnoExercicio").val();

            programasInfo.forEach(function (programa) {
                if (programa.Cfp == prog && programa.Ano == ano) {
                    $('#ProgramaId option[value="' + programa.Codigo + '"]').attr('selected', true);
                    cont += 1;
                }
            });


            if (cont == 0)
                msg = "Cfp " + prog + " não Cadastrado no SIDS";
            SelecionarComboPtres();

        }

    }

    var contN = 0;

    GerarComboNatureza();
    var natureza = dadosEmpenho.OutCed1 + dadosEmpenho.OutCed2 + dadosEmpenho.OutCed3 + dadosEmpenho.OutCed4 + dadosEmpenho.OutCed5;


    estruturaInfo.forEach(function (estrutura) {

        if (estrutura.Natureza == natureza && estrutura.Fonte == dadosEmpenho.OutFonteSiafem.substr(1, 2)) {
            $('#Natureza option[value="' + estrutura.Codigo + '"]').attr({ selected: "selected" });
            contN += 1;
        }
    });


    if (contN == 0)
        msg = (msg.length === 0 ? "\n" : "") +
            "CED " +
            natureza +
            '-' +
            dadosEmpenho.OutFonteSiafem.substr(1, 2) +
            " não Cadastrada no SIDS";


    if (msg.length > 0)
        AbrirModal(msg);

    //$('input#CodigoEspecificacaoDespesa').val("000");// na view id = #EspecDespesa
    $("#EspecDespesa").val("000");

    $("#DescricaoEspecificacaoDespesa1").val(removerAcentos(dadosEmpenho.OutEspecDespesa1));
    $("#DescricaoEspecificacaoDespesa2").val(removerAcentos(dadosEmpenho.OutEspecDespesa2));
    $("#DescricaoEspecificacaoDespesa3").val(removerAcentos(dadosEmpenho.OutEspecDespesa3));
    $("#DescricaoEspecificacaoDespesa4").val(removerAcentos(dadosEmpenho.OutEspecDespesa4));
    $("#DescricaoEspecificacaoDespesa5").val(removerAcentos(dadosEmpenho.OutEspecDespesa5));
    $("#DescricaoEspecificacaoDespesa6").val(removerAcentos(dadosEmpenho.OutEspecDespesa6));
    $("#DescricaoEspecificacaoDespesa7").val(removerAcentos(dadosEmpenho.OutEspecDespesa7));
    $("#DescricaoEspecificacaoDespesa8").val(removerAcentos(dadosEmpenho.OutEspecDespesa8));

    $('input#NumeroOriginalSiafemSiafisico').trigger('blur');

    var veioCtDoDb = dadosDatabase !== undefined && dadosDatabase !== null && dadosDatabase.NumeroCT !== undefined && dadosDatabase.NumeroCT !== null;
    var semNE = dadosEmpenho === undefined || dadosEmpenho === null || dadosEmpenho.OutNeSiafem === undefined || dadosEmpenho.OutNeSiafem === null;
    if (veioCtDoDb && semNE) {
        $("#NumeroCT").val(dadosDatabase.NumeroCT);
        $('input#NumeroCT').trigger('blur');
    }
}

function ConfirmarRap(num, valor) {
    ConfirmarContrato();

    $('#modalConsultaContrato').modal("toggle");
    $("#ValorAnulado").val(valor);
    $("#Valor").val(valor);

    $("#ValorAnulado").attr("disabled", true);
    $("#Valor").attr("disabled", true);

    $(".real").maskMoney('mask');
    var rapNum = String(num).replace(/(\d{9})(\d{3})(\d{3})/g, "\$1\/\$2\/\$3");
    $("#NumeroRequisicaoRap").val(rapNum);
    $("#NumeroDocumento").val(rapNum);
    DadosRap(rapNum);
}

function ConfirmarDesdobramento(num, valor, e) {

    e.preventDefault();

    var online = navigator.onLine;

    if (online != true) {
        AbrirModal("Erro de conexão");
        return false;
    }

    $('input#CodigoAplicacaoObra').val(contrato.OutCodObra);
    $('input#NumeroDocumento').val(num);



    RefazerCombo("#DocumentoTipoId");
    if (String(num).length === 15) {
        $('#DocumentoTipoId option[value="' + 11 + '"]').attr("selected", true);
        $('#DocumentoTipoId').trigger("change");
    } else {
        $('#DocumentoTipoId option[value="' + 5 + '"]').attr("selected", true);
        $('#DocumentoTipoId').trigger("change");
    }

    table.forEach(function (table) {

        if (parseInt(table.OutNumero) === num) {
            $('input#ValorDistribuido').val(table.OutValor);
        }

    });

    if ($("#modalConsultaContrato").is(":visible")) {
        $('#modalConsultaContrato').modal("toggle");
    }

    $('input#CodigoAplicacaoObra').val(contrato.OutCodObra);
    $('input#NumeroDocumento').val(num);
    e.preventDefault();

}


function AtualizarEmpenhoDataBase(dadosDatabase) {

    //obtido no Sids
    $('input#Oc').val(dadosDatabase.Oc);
    $('input#CodigoUnidadeGestora').val(dadosDatabase.CodigoUnidadeGestora);
    $('input#CodigoGestao').val(dadosDatabase.CodigoGestao);


    RefazerCombo("#ModalidadeId");
    $("#ModalidadeId option[value='" + dadosDatabase.ModalidadeId + "']").attr('selected', true);

    RefazerCombo("#LicitacaoId");
    $("#LicitacaoId option[value='" + dadosDatabase.LicitacaoId + "']").attr('selected', true);

    RefazerCombo("#Fonte");
    $("#Fonte option[value='" + dadosDatabase.FonteId + "']").attr('selected', true);

    $("#CodigoMunicipio").val(dadosDatabase.CodigoMunicipio);
    //$("#CodigoEvento").val(dadosDatabase.CodigoEvento);
    $("#CodigoUO").val(dadosDatabase.CodigoUO);
    $("#DescricaoAcordo").val(dadosDatabase.DescricaoAcordo);
    $("#DescricaoLocalEntregaSiafem").val(dadosDatabase.DescricaoLocalEntregaSiafem);
    $("#CodigoCredorOrganizacao").val(dadosDatabase.CodigoCredorOrganizacao);
    $("#CodigoNaturezaItem").val(dadosDatabase.CodigoNaturezaItem);
    $("#NumeroOriginalCT").val(dadosDatabase.NumeroCT);

    if (dadosDatabase.TransmitirSiafem == true)
        if ($("#transmitirSIAFEM").is(":checked") == false) {
            $("#transmitirSIAFISICO").removeAttr("checked");
            $("#transmitirSIAFEM").prop("checked", true);
            $("#transmitirSIAFEM").trigger("change");
        }

    if (dadosDatabase.TransmitirSiafisico == true)
        if ($("#transmitirSIAFISICO").is(":checked") == false) {
            $("#transmitirSIAFEM").removeAttr("checked");
            $("#transmitirSIAFISICO").prop("checked", true);
            $("#transmitirSIAFISICO").trigger("change");
        }

    $("#NumeroCNPJCPFFornecedor").val(dadosDatabase.NumeroCNPJCPFFornecedor);
    $("#NumeroCNPJCPFCredor").val(dadosDatabase.NumeroCNPJCPFCredor);
    $("#NumeroCNPJCPFUGCredor").val(dadosDatabase.NumeroCNPJCPFUGCredor);


    var empNum = dadosDatabase.NumeroEmpenhoSiafem == null ? dadosDatabase.NumeroEmpenhoSiafisico : dadosDatabase.NumeroEmpenhoSiafem;

    $('input#CodigoEmpenhoOriginal').val(empNum);

    if (dadosDatabase.NumeroContrato != null)
        $('input#Contrato').val(dadosDatabase.NumeroContrato.replace(/\s/g, "").replace(/[\.-]/g, "").replace(/(\d{2})(\d{2})(\d{5})(\d{1})/g, "\$1.\$2.\$3\-\$4"));


}

function EspecificacaoDespesa(valor) {
    var online = navigator.onLine;

    if (online === true) {
    } else {
        AbrirModal("Erro de conexão");
        return false;
    }

    waitingDialog.show('Consultando');
    $.ajax({
        datatype: 'json',
        type: 'Post',
        url: "/ConsutasBase/ConsultarEspecificacao",
        cache: false,
        async: true,
        data: JSON.stringify({ codigo: valor }),
        contentType: "application/json; charset=utf-8",
        success: function (dados) {
            if (dados.Status == "Sucesso") {
                var dadosEspecificacao = dados.Especificacao;

                waitingDialog.hide();

                $("#DescEspecificacaoDespesa > .Proximo")[0].value = removerAcentos(dadosEspecificacao.outEspecDespesa);
                $("#DescEspecificacaoDespesa > .Proximo")[1].value = removerAcentos(dadosEspecificacao.outEspecDespesa_02);
                $("#DescEspecificacaoDespesa > .Proximo")[2].value = removerAcentos(dadosEspecificacao.outEspecDespesa_03);
                $("#DescEspecificacaoDespesa > .Proximo")[3].value = removerAcentos(dadosEspecificacao.outEspecDespesa_04);
                $("#DescEspecificacaoDespesa > .Proximo")[4].value = removerAcentos(dadosEspecificacao.outEspecDespesa_05);
                $("#DescEspecificacaoDespesa > .Proximo")[5].value = removerAcentos(dadosEspecificacao.outEspecDespesa_06);
                $("#DescEspecificacaoDespesa > .Proximo")[6].value = removerAcentos(dadosEspecificacao.outEspecDespesa_07);
                $("#DescEspecificacaoDespesa > .Proximo")[7].value = removerAcentos(dadosEspecificacao.outEspecDespesa_08);
                $("#DescEspecificacaoDespesa > .Proximo")[8].value = removerAcentos(dadosEspecificacao.outEspecDespesa_09);
                $("#DescEspecificacaoDespesa > .Proximo").trigger("input");
            }
            else {
                var msg = dados.Msg;
                if (dados.Msg == null)
                    msg = "Erro na comunicação com servidor, Tente novamente.";

                waitingDialog.hide();
                AbrirModal(msg);
            }
        },
        error: function (dados, exception) {
            waitingDialog.hide();
            ErrorAjax(dados, exception);
        }
    });


    ValicarCampos("campo");
}

function ConsultarAssinatura(valor, tipo) {

    var online = navigator.onLine;

    if (online === true) {
    } else {
        AbrirModal("Erro de conexão");
        return false;
    }


    $.ajax({
        datatype: 'json',
        type: 'Post',
        url: "/Reserva/Reserva/ConsultarAssinatura",
        cache: false,
        async: true,
        data: JSON.stringify({ codigo: valor, tipo: tipo }),
        contentType: "application/json; charset=utf-8",
        success: function (dados) {
            if ((dados.Status == "Sucesso") & (tipo == 1)) {

                document.getElementById('txtAutorizadoGrupo').value = dados.Assinatura.outGrupoAutorizador;
                document.getElementById('txtAutorizadoOrgao').value = dados.Assinatura.outOrgaoAutorizador;
                document.getElementById('txtAutorizadoNome').value = dados.Assinatura.outNomeAutorizador;
                document.getElementById('txtAutorizadoCargo').value = dados.Assinatura.outCargoAutorizador;
                $("#CodAssAutorizado").trigger('input');
            }

            else if ((dados.Status == "Sucesso") & (tipo == 2)) {

                document.getElementById('txtExaminadoGrupo').value = dados.Assinatura.outGrupoExaminador;
                document.getElementById('txtExaminadoOrgao').value = dados.Assinatura.outOrgaoExaminador;
                document.getElementById('txtExaminadoNome').value = dados.Assinatura.outNomeExaminador;
                document.getElementById('txtExaminadoCargo').value = dados.Assinatura.outCargoExaminador;
                $("#CodAssExaminado").trigger('input');

            }

            else if ((dados.Status == "Sucesso") & (tipo == 3)) {

                document.getElementById('txtResponsavelGrupo').value = dados.Assinatura.outGrupoResponsavel;
                document.getElementById('txtResponsavelOrgao').value = dados.Assinatura.outOrgaoResponsavel;
                document.getElementById('txtResponsavelNome').value = dados.Assinatura.outNomeResponsavel;
                document.getElementById('txtResponsavelCargo').value = dados.Assinatura.outCargoResponsavel;
                $("#CodAssResponsavel").trigger('input');


            }

            else {
                if (tipo == 1)
                    $("div >#CodAssAutorizado").val("");
                else if (tipo == 2)
                    $("div >#CodAssExaminado").val("");
                else if (tipo == 3)
                    $("div >#CodAssResponsavel").val("");

                AbrirModal(dados.Msg);
            }
        },
        beforeSend: function () {
            waitingDialog.show('Consultando');
        },
        complete: function () {
            waitingDialog.hide();
        },
        error: function (dados, exception) {
            ErrorAjax(dados, exception);
        }
    });
}

function ConsultaOc() {
    var online = navigator.onLine;

    if (online === true) {
    } else {
        AbrirModal("Erro de conexão");
        return false;
    }

    if ($("#Oc").val().length === 0 || $("#Oc").val().length < 11 || $("#Oc").val() == "" || numOc == $("#Oc").val())
        return false;


    var mydate = new Date();
    var year = mydate.getYear();
    var month = mydate.getMonth();
    if (year < 1000)
        year += 1900;


    //if ($("#Oc").val().substr(0, 4) != year)
    //    $("#Oc").val($("#Oc").val().replace($("#Oc").val().substr(0, 4), year));

    numOc = $("#Oc").val();

    waitingDialog.show('Consultando');

    var dtoOc = {
        Oc: $("#Oc").val(),
        Ugo: $("#Ugo").val(),
        Ug: $("#Uo").val()
    };
    $.ajax({
        datatype: 'json',
        type: 'Post',
        url: "/Reserva/Reserva/ConsultarOc",
        cache: false,
        async: true,
        data: JSON.stringify({ dtoOc: dtoOc }),
        contentType: "application/json; charset=utf-8",
        success: function (dados) {
            if (dados.Status == "Sucesso") {
                DadosOc(dados.Oc);
                waitingDialog.hide();

            }
            else {
                var msg = dados.Msg;
                if (dados.Msg == null)
                    msg = "Erro na comunicação com servidor, Tente novamente.";

                waitingDialog.hide();
                AbrirModal(msg);
            }
        },
        error: function (dados, exception) {
            waitingDialog.hide();
            ErrorAjax(dados, exception);
        }
    });
}

function DadosOc(oc) {

    $('input#Processo').val(oc.Processo);
    SelecioarComboCfp();

    $('#Fonte option[value="' + oc.Fonte + '"]').attr("selected", true);
    var cont = 0;
    programasInfo.forEach(function (programa) {

        if (programa.Ptres == oc.Ptres && programa.Ano == $("#AnoExercicio").val()) {

            $('#Programa option[value="' + programa.Codigo + '"]').attr("selected", true);
            cont += 1;
        }

    });
    var fonte = $("#Fonte :selected").text();
    SelecionarComboPtres();

    var msg = "";

    if (cont == 0)
        msg = "Ptres " + oc.Ptres + " não Cadastrado no SIDS";
    GerarComboNatureza();
    var contN = 0;
    var natureza = oc.Natureza;
    estruturaInfo.forEach(function (estrutura) {

        if (estrutura.Natureza == natureza && estrutura.Fonte == fonte.substr(1, 2)) {
            $('#Natureza option[value="' + estrutura.Codigo + '"]').attr({ selected: "selected" });
            contN += 1;
        }
    });


    if (contN == 0)
        msg = (msg.length === 0 ? "\n" : "") + "CED " + natureza + '-' + fonte.substr(1, 2) + " não Cadastrada no SIDS";

    if (msg.length > 0)
        AbrirModal(msg);

}

function LimparCombo(tipo) {

    if ((tipo == 1)) {

        document.getElementById('txtAutorizadoGrupo').value = "";
        document.getElementById('txtAutorizadoOrgao').value = "";
        document.getElementById('txtAutorizadoNome').value = "";
        document.getElementById('txtAutorizadoCargo').value = "";

    }

    else if ((tipo == 2)) {
        document.getElementById('txtExaminadoGrupo').value = "";
        document.getElementById('txtExaminadoOrgao').value = "";
        document.getElementById('txtExaminadoNome').value = "";
        document.getElementById('txtExaminadoCargo').value = "";

    }

    else if ((tipo == 3)) {
        document.getElementById('txtResponsavelGrupo').value = "";
        document.getElementById('txtResponsavelOrgao').value = "";
        document.getElementById('txtResponsavelCargo').value = "";
        document.getElementById('txtResponsavelNome').value = "";

    }
}

function ConsultaCt(campoCt, origem, callbackSucesso) {
    if ($('#tblPesquisaItem').length > 0) {
        $('#tblPesquisaItem').dataTable().fnDeleteRow();//limpa grid
    }
    origemConsulta = "ct";
    var online = navigator.onLine;

    if (online === false) {
        AbrirModal("Erro de conexão");
        return false;
    }

    if ($(campoCt).val().length === 0 || $(campoCt).val().length < 11 || $(campoCt).val() == "")
        return false;


    var mydate = new Date();
    var year = mydate.getYear();
    var month = mydate.getMonth();
    if (year < 1000)
        year += 1900;


    numCt = $(campoCt).val();

    waitingDialog.show('Consultando');

    $.ajax({
        datatype: 'json',
        type: 'Post',
        url: "/ConsutasBase/ConsultarCt",
        cache: false,
        async: true,
        data: JSON.stringify({ NumCt: numCt, origem: origem }),
        contentType: "application/json; charset=utf-8",
        success: function (dados) {
            if (dados.Status == "Sucesso") {
                waitingDialog.hide();

                $('.clearFields').val('');
                DadosCt(dados);

                if (callbackSucesso) {
                    callbackSucesso(dados);
                }
            }
            else {
                waitingDialog.hide();
                AbrirModal(dados.Msg);
            }
        },
        error: function (dados) {
            waitingDialog.hide();
            AbrirModal(dados);
        }
    });
}

function verificarOrigem() {
    if (liquidacao.isInclusao()) {
        return 'Subempenho';
    }
    else if (liquidacao.isAnulacao()) {
        return 'SubempenhoAnulacao';
    }
}


function ConsultarCtPorCt(seletorTextBox, callbackSucesso) {
    var txtBox = $(seletorTextBox);
    var valor = txtBox.val();

    if (valor.length === 0 || valor.length < 11 || valor == "")
        return false;

    var origem = verificarOrigem();
    var url = "/ConsutasBase/ConsultarCt";
    var data = { NumCt: valor, origem: origem };

    return ConsultarGeral(url, data, callbackSucesso);
}

function ConsultarCtPorNe(seletorTextBox, callbackSucesso) {
    var txtBox = $(seletorTextBox);
    var valor = txtBox.val();

    if (valor.length === 0 || valor.length < 11 || valor == "")
        return false;

    var origem = verificarOrigem();
    var url = "/ConsutasBase/ConsultarCtPorNe";
    var data = { numeroNe: valor, origem: origem };

    return ConsultarGeral(url, data, callbackSucesso);
}


function ConsultarNl(seletorTextBox, callbackSucesso) {
    var txtBox = $(seletorTextBox);
    var valor = txtBox.val();

    if (valor.length === 0 || valor.length < 11 || valor == "")
        return false;

    var origem = verificarOrigem();
    var url = "/ConsutasBase/ConsultarNl";
    var data = { numeroNl: valor, origem: origem };

    return ConsultarGeral(url, data, callbackSucesso);
}

function ConsultarGeral(url, data, callbackSucesso) {
    if ($('#tblPesquisaItem').length > 0) {
        $('#tblPesquisaItem').dataTable().fnDeleteRow();//limpa grid
    }
    var online = navigator.onLine;

    if (online === false) {
        AbrirModal("Erro de conexão");
        return false;
    }

    //waitingDialog.show('Consultando');

    $.ajax({
        type: "POST",
        url: url,
        data: data,
        dataType: "json",
        success: function (response) {
            if (response.Status == "Sucesso") {
                //waitingDialog.hide();
                if (callbackSucesso) {
                    callbackSucesso(response);
                }
            }
            else {
                //waitingDialog.hide();
                AbrirModal(response.Msg);
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

function DadosCt(dados) {

    var ct = dados.Ct;

    $('input#Processo').val(ct.Processo);

    if (String(ct.CGCCpf).length > 11) {
        $('input#credor').val(ct.CGCCpf);
        $('input#NumeroCNPJCPFUGCredor').val(ct.CGCCpf);
    } else if (String(ct.CGCCpf).length <= 11) {
        if (TestaCPF(String(ct.CGCCpf)) == true) {
            $('input#credor').val(Stringct.CGCCpf);
            $('input#NumeroCNPJCPFUGCredor').val(ct.CGCCpf);
        } else {
            $('input#CodigoUnidadeGestoraFornecedora').val(Left(ct.CGCCpf, 6));
            $('input#NumeroCNPJCPFUGCredor').val(Left(ct.CGCCpf, 6));
            $('input#CodigoUnidadeGestoraFornecedora').val(Right(ct.CGCCpf, 5));
            $('input#CodigoGestaoCredor').val(Right(ct.CGCCpf, 5));

        }

    }

    //$('input#NumeroCNPJCPFUGCredor').val(ct.CGCCpf);
    $('input#DataEntregaMaterialSiafisico').val(ConverterData(ct.DataEntregaPrevista));
    $('input#DataEmissao').val(ConverterData(ct.DataEmissao));

    $('input#CodigoEvento').val(Left(ct.Evento, 6));
    $('#CodigoEvento option[value="' + Left(ct.Evento, 6) + '"]').attr("selected", true);

    $('input#Uo').val(ct.Gestao);
    $('input#CodigoGestao').val(ct.Gestao);
    $('input#DescricaoLogradouroEntrega').val(ct.LocalEntrega);
    $('input#DescricaoBairroEntrega').val(ct.Bairro);
    $('input#DescricaoCidadeEntrega').val(ct.Cidade);
    $('input#NumeroCEPEntrega').val(ct.CEP.replace(/(\d{5})(\d{3})/g, "\$1-\$2")); // maskara adicionada no retorno da consultaCt


    var max = $($("#DescricaoInformacoesAdicionaisEntrega > .Proximo")[0]).attr("maxlength");

    var resultado = ct.InformacoesAdicionais.substr(0, max) + ";" + ct.InformacoesAdicionais.substr(max, max) + ";" + ct.InformacoesAdicionais.substr(max * 2, max) + ";";

    CarregarTextArea("DescricaoInformacoesAdicionaisEntrega", 3, resultado);


    $('input#NumeroContratoFornecedor').val(ct.NumeroContratoForn);
    $('input#NumeroEdital').val(ct.NumeroEdital);

    RefazerCombo("#OrigemMaterialId");
    RefazerCombo("#TipoAquisicaoId");
    RefazerCombo("#LicitacaoId");
    RefazerCombo("#ModalidadeId");
    RefazerCombo("#CodigoFonteSiafisico");

    $('#OrigemMaterialId option[value="' + Left(ct.OrigemMaterial, 1) + '"]').attr("selected", true);
    $('#TipoAquisicaoId option[value="' + Left(ct.ServicoMaterial, 1) + '"]').attr("selected", true);
    $('#LicitacaoId option[value="' + Left(ct.TipoCompraLicitacao, 1) + '"]').attr("selected", true);
    $('#ModalidadeId option[value="' + Left(ct.ModalidadeEmpenho, 1) + '"]').attr("selected", true);
    $('#CodigoFonteSiafisico option[value="' + ct.Fonte + '"]').attr("selected", true);


    if (obj == "/Empenho/Empenho") {
        $('[name=Mes' + ct.Mes1 + ']').val(MaskMonetario(ct.Valor1));
        $('[name=Mes' + ct.Mes2 + ']').val(MaskMonetario(ct.Valor2));
        $('[name=Mes' + ct.Mes3 + ']').val(MaskMonetario(ct.Valor3));
        $('[name=Mes' + ct.Mes4 + ']').val(MaskMonetario(ct.Valor4));
        $('[name=Mes' + ct.Mes5 + ']').val(MaskMonetario(ct.Valor5));
        $('[name=Mes' + ct.Mes6 + ']').val(MaskMonetario(ct.Valor6));
        $('[name=Mes' + ct.Mes7 + ']').val(MaskMonetario(ct.Valor7));
        $('[name=Mes' + ct.Mes8 + ']').val(MaskMonetario(ct.Valor8));
        $('[name=Mes' + ct.Mes9 + ']').val(MaskMonetario(ct.Valor9));
        $('[name=Mes' + ct.Mes10 + ']').val(MaskMonetario(ct.Valor10));
        $('[name=Mes' + ct.Mes11 + ']').val(MaskMonetario(ct.Valor11));
        $('[name=Mes' + ct.Mes12 + ']').val(MaskMonetario(ct.Valor12));
    }

    $(".real").maskMoney({ prefix: "R$ ", allowNegative: false, thousands: ".", decimal: ",", affixesStay: true });
    $(".real").maskMoney('mask');
    SomarTotal();


    $('input#DescricaoReferenciaLegal').val(ct.ReferenciaLegal);


    $('input#CodigoUnidadeGestora').val(ct.UG);
    $('input#Ugo').val(ct.UGR);
    $('input#CodigoNaturezaItem').val(Right(ct.NaturezaDespesa, 2));


    SelecioarComboCfp();

    RefazerCombo("#Fonte");
    $('#Fonte option[value="' + ct.OrigemRecurso + '"]').attr("selected", true);

    //var fonte = $('#CodigoFonteSiafisico option[value="' + ct.Fonte + '"]').text();
    var fonte = ct.Fonte;

    var cont = 0;
    programasInfo.forEach(function (programa) {

        if (programa.Ptres == ct.PTRES && programa.Ano == $("#AnoExercicio").val()) {

            $('#Programa option[value="' + programa.Codigo + '"]').attr("selected", true);
            cont += 1;
        }

    });

    SelecionarComboPtres();

    var msg = "";
    var telaPossuiPrograma = $('#Programa').length > 0;

    if (cont === 0 && telaPossuiPrograma)
        msg = "Ptres " + ct.PTRES + " não Cadastrado no SIDS";

    GerarComboNatureza();
    var contN = 0;
    var natureza = Left(ct.NaturezaDespesa, 6);
    estruturaInfo.forEach(function (estrutura) {

        if (estrutura.Natureza == natureza && estrutura.Fonte == fonte.substr(1, 2)) {
            $('#Natureza option[value="' + estrutura.Codigo + '"]').attr("selected", true);
            contN += 1;
        }
    });


    if (dados.baseItem !== null) {

        CarregarGridItens(dados.baseItem);

    }

    if (contN == 0)
        msg = (msg.length === 0 ? "\n" : "") + "CED " + natureza + " não Cadastrada no SIDS";

    if (msg.length > 0)
        AbrirModal(msg);
}

function ConsultarNotaEmpenho() {

    origemConsulta = "empenho";
    var online = navigator.onLine;

    if (online != true) {
        AbrirModal("Erro de conexão");
        return false;
    }


    var unidadeGestora = $("#CodigoUnidadeGestora").val();
    var gestao = $("#CodigoGestao").val();
    var codigoEmpenho = $("#CodigoEmpenhoOriginal").val();
    var data = $("#DataEmissao").val();
    var cnpjCpf = $("#NumeroCNPJCPFUGCredor").val();
    var gestaoCredor = $("#CodigoGestaoCredor").val();
    var natureza = $("#CodigoNaturezaNe").val();
    var modalidade = $("#ModalidadeId").val();
    var licitacao = $("#LicitacaoId").val();
    var fonte = $("#CodigoFonteSiafisico").val() > 0 ? $("#CodigoFonteSiafisico :selected").text() : null;
    var processo = $("#NumeroProcessoNE").val();

    if (gestao == "") {
        AbrirModal("Favor informar Gestão!");
        return false;
    }
    else if (unidadeGestora == "") {
        AbrirModal("Favor informar o Unidade Gestora!");
        return false;
    }
    else if (codigoEmpenho == "") {
        AbrirModal("Favor informar o Nº do Empenho!");
        return false;
    }



    var dtoEmpenhos =
        {
            CgcCpf: cnpjCpf,
            //Data: data,
            Fonte: fonte,
            GestaoCredor: gestaoCredor,
            Licitacao: licitacao,
            Modalidade: modalidade,
            Natureza: natureza,
            NumEmpenho: codigoEmpenho,
            Processo: processo,
            Gestao: gestao,
            UnidadeGestora: unidadeGestora
        };
    var descNatureza = $("#CodigoNaturezaNe").val().length > 0 ? $("#CodigoNaturezaNe :selected").text() : "Todos";
    var descModalidade = $("#ModalidadeId").val().length > 0 ? $("#ModalidadeId :selected").text() : "Todos";
    var descLicitacao = $("#LicitacaoId").val().length > 0 ? $("#LicitacaoId :selected").text() : "Todos";
    var descFonte = $("#CodigoFonteSiafisico").val().length > 0 ? $("#CodigoFonteSiafisico :selected").text() : "Todos";

    $.ajax({
        datatype: 'json',
        type: 'Post',
        url: "/ConsutasBase/ConsultarEmpenhos",
        cache: false,
        data: JSON.stringify({ dtoEmpenhos: dtoEmpenhos }),
        contentType: "application/json; charset=utf-8",
        beforeSend: function () {
            waitingDialog.show('Consultando');
        },
        success: function (dados) {
            if (dados.Status == "Sucesso") {
                $('#modalConsultaEmpenhos').modal("toggle");
                $('input#txtUnidadeGestora').val(unidadeGestora);
                $('input#txtGestao').val(gestao);
                $('input#txtLicitacao').val(descLicitacao);
                $('input#txtFonte').val(descFonte);
                $('input#txtModalidade').val(descModalidade);
                $('input#txtNatureza').val(descNatureza);

                GerarTabelaEmpenhos(dados.Empenhos); //propriedade List da classe : "ConsultaReservaEstrutura"
            }
            else {
                AbrirModal(dados.Msg);
            }
        },
        error: function (dados) {
            AbrirModal(dados);
        },
        complete: function () {
            waitingDialog.hide();
        }
    });
}

function GerarTabelaEmpenhos(values) {

    if (!$("#tblListaEmpenhos").hasClass("_tbDataTables")) {
        $('#tblListaEmpenhos').addClass("_tbDataTables");
        BuildDataTables.ById('#tblListaEmpenhos');
    }

    $('#tblListaEmpenhos').DataTable().clear().draw(true);
    $.each(values,
        function (index, x) {
            $('#tblListaEmpenhos')
                .DataTable()
                .row.add([
                    x.numerone,
                    x.evento,
                    x.natureza,
                    x.credor,
                    x.valor,
                    "<button class='btn btn-xs btn-primary' title='Visualizar' onclick='dadosEmpenhoSIAFEM(\"" + x.numerone + "\")'><i class='fa fa-search'></i></button>"
                ])
                .draw(true);
        });
}

function dadosEmpenhoSIAFEM(empenho) {
    var online = navigator.onLine;

    if (online === true) {
    } else {
        AbrirModal("Erro de conexão");
        return false;
    }

    waitingDialog.show('Consultando');
    $.ajax({
        datatype: 'json',
        type: 'Post',
        url: "/ConsutasBase/ConsultarNe",
        cache: false,
        async: true,
        data: JSON.stringify({ numNe: Left($("#CodigoEmpenhoOriginal").val(), 4) + empenho }),
        contentType: "application/json; charset=utf-8",
        success: function (dados) {
            if (dados.Status == "Sucesso") {
                dadosEmpenho = dados.Empenho;
                dadosDatabase = dados.dadosDatabase;

                $('#modalConsultaEmpenhos').modal("toggle");

                $('#modalDadosEmpenhoSIAFEM').modal();

                $('input#txtCnpjCpfCredor').val(dadosEmpenho.CgcCpf);
                $('input#txtDataEmissao').val(dadosEmpenho.DataEmissao);
                $('input#txtDataELancamento').val(dadosEmpenho.DataLancamento);
                $('input#txtNaturezaDespesa').val(dadosEmpenho.Despesa);
                $('input#txtEmpenhoOriginal').val(dadosEmpenho.EmpenhoOriginal);
                $('input#txtEvento').val(dadosEmpenho.Evento);
                $('input#txtFonte').val(dadosEmpenho.Fonte);
                $('input#txtGestao').val(dadosEmpenho.Gestao);
                $('input#txtGestaoCredor').val(dadosEmpenho.GestaoCredor);
                $('input#txtObra').val(dadosEmpenho.IdentificadorObra);
                $('input#txtLicitacao').val(dadosEmpenho.Licitacao);
                $('input#txtLocalEntrega').val(dadosEmpenho.Local);
                $('input#txtModalidade').val(dadosEmpenho.Modalidade);
                $('input#txtContrato').val(dadosEmpenho.NumeroContrato);
                $('input#txtNumEmpenho').val(dadosEmpenho.NumeroNe);
                $('input#txtProcesso').val(dadosEmpenho.NumeroProcesso);
                $('input#txtOc').val(dadosEmpenho.Oc);
                $('input#txtOrigemMarterial').val(dadosEmpenho.OrigemMaterial);
                $('input#txtPlanoInterno').val(dadosEmpenho.PlanoInterno);
                $('input#txtPrograma').val(dadosEmpenho.Pt);
                $('input#txtPtres').val(dadosEmpenho.Ptres);
                $('input#txtRefLegal').val(dadosEmpenho.ReferenciaLegal);
                $('input#txtTipo').val(dadosEmpenho.TipoEmpenho);
                $('input#txtUgo').val(dadosEmpenho.Ugo);
                $('input#txtUnidadeGestora').val(dadosEmpenho.UnidadeGestora);
                $('input#txtUo').val(dadosEmpenho.Uo);
                $('input#txtValor').val(dadosEmpenho.Valor);

                waitingDialog.hide();
            } else {
                waitingDialog.hide();
                AbrirModal(dados.Msg);
            }
        },
        error: function (dados) {
            waitingDialog.hide();
            AbrirModal(dados);
        }
    });

}

function RefazerCombo(id) {
    $(id + ' option[selected="selected"]').removeAttr("selected");
    ToArrey(id);
}

function ToArrey(id) {

    if (id != "#CenarioSiafemSiafisico") {
        var dados = [];
        $(id + ' option')
            .each(function (i, select) {
                dados.push({
                    Name: select.innerHTML,
                    ModelID: select.value
                });
            });

        $(id).empty();

        dados.forEach(function (val) {
            $(id).append("<option value='" + val.ModelID + "' >" + val.Name + "</option>");
        });
    }
}

function carregaDadosEmpenhoSIAFEM() {

    $("#NumeroCNPJCPFUGCredor").val(dadosEmpenho.CgcCpf.split(" ")[0]);
    //$("#DataEmissao").val(ConverterData(dadosEmpenho.DataEmissao));
    //$("#CodigoEvento").val(Left(dadosEmpenho.Evento, 6));


    $("#CodigoEmpenhoOriginal").val(dadosEmpenho.NumeroNe);
    $("#CodigoGestaoCredor").val(Left(dadosEmpenho.GestaoCredor, 5));


    RefazerCombo("#OrigemMaterialId");
    $('#OrigemMaterialId option[value="' + Left(dadosEmpenho.OrigemMaterial, 1) + '"]').attr("selected", "selected");

    RefazerCombo("#TipoAquisicaoId");
    $('#TipoAquisicaoId option[value="' + Left(dadosEmpenho.ServicoouMaterial, 1) + '"]').attr("selected", "selected");

    RefazerCombo("#LicitacaoId");
    $('#LicitacaoId option[value="' + Left(dadosEmpenho.Licitacao, 1) + '"]').attr("selected", "selected");


    RefazerCombo("#ModalidadeId");
    $('#ModalidadeId option[value="' + Left(dadosEmpenho.Modalidade, 1) + '"]').attr("selected", "selected");

    RefazerCombo("#CodigoFonteSiafisico");
    $("#CodigoFonteSiafisico option[value='" + dadosEmpenho.Fonte + "']").attr('selected', "selected");

    RefazerCombo("#Fonte");
    var fontId = $("#Fonte option:contains('" + dadosEmpenho.Fonte + "')").val();
    $("#Fonte option[value='" + fontId + "']").attr('selected', "selected");

    RefazerCombo("#CodigoNaturezaNe");
    $("#CodigoNaturezaNe option[value='" + Left(dadosEmpenho.Despesa, 6) + "']").attr('selected', "selected");


    if (dadosDatabase != null) {
        //obtido no Sids
        $('input#Oc').val(dadosDatabase.Oc);
        $('input#CodigoUnidadeGestora').val(dadosDatabase.CodigoUnidadeGestora);
        $('input#CodigoGestao').val(dadosDatabase.CodigoGestao);
        $('input#NumeroCNPJCPFUGCredor').val(dadosDatabase.NumeroCNPJCPFUGCredor);
        $('input#CodigoGestaoCredor').val(Left(dadosDatabase.CodigoGestaoCredor, 5));

        RefazerCombo("#ModalidadeId");
        $("#ModalidadeId option[value='" + dadosDatabase.ModalidadeId + "']").attr('selected', true);

        RefazerCombo("#LicitacaoId");
        $("#LicitacaoId option[value='" + dadosDatabase.LicitacaoId + "']").attr('selected', true);

        RefazerCombo("#Fonte");
        $("#Fonte option[value='" + dadosDatabase.FonteId + "']").attr('selected', true);

        $("#CodigoMunicipio").val(dadosDatabase.CodigoMunicipio);
        //$("#CodigoEvento").val(dadosDatabase.CodigoEvento);
        $("#CodigoUO").val(dadosDatabase.CodigoUO);
        $("#DescricaoAcordo").val(dadosDatabase.DescricaoAcordo);
        $("#DescricaoLocalEntregaSiafem").val(dadosDatabase.DescricaoLocalEntregaSiafem);
        $("#CodigoCredorOrganizacao").val(dadosDatabase.CodigoCredorOrganizacao);
        $("#CodigoNaturezaItem").val(dadosDatabase.CodigoNaturezaItem);

        if (dadosDatabase.TransmitirSiafem == true)
            if ($("#transmitirSIAFEM").is(":checked") == false) {
                $("#transmitirSIAFISICO").removeAttr("checked");
                $("#transmitirSIAFEM").prop("checked", true);
                $("#transmitirSIAFEM").trigger("change");
            }

        if (dadosDatabase.TransmitirSiafisico == true)
            if ($("#transmitirSIAFISICO").is(":checked") == false) {
                $("#transmitirSIAFEM").removeAttr("checked");
                $("#transmitirSIAFISICO").prop("checked", true);
                $("#transmitirSIAFISICO").trigger("change");
            }


        $("#NumeroCNPJCPFFornecedor").val(dadosDatabase.NumeroCNPJCPFFornecedor);
        $("#NumeroCNPJCPFCredor").val(dadosDatabase.NumeroCNPJCPFCredor);

        $('input#CodigoEmpenhoOriginal').val(dadosEmpenho.NumeroNe);
        $('input#CodigoEmpenho').val(dadosDatabase.NumeroEmpenhoProdesp);
        $('input#Reserva').val(dadosDatabase.CodigoReserva);

        if (dadosDatabase.NumeroContrato != null)
            $('input#Contrato').val(dadosDatabase.NumeroContrato.replace(/\s/g, "").replace(/[\.-]/g, "").replace(/(\d{2})(\d{2})(\d{5})(\d{1})/g, "\$1.\$2.\$3\-\$4"));

    }




    $("#DescricaoLocalEntregaSiafem").val(dadosEmpenho.Local);

    $("#NumeroProcessoNE").val(dadosEmpenho.NumeroProcesso);
    $("#Processo").val(dadosEmpenho.NumeroProcesso);

    $("#CodigoNaturezaItem").val(Right(dadosEmpenho.Despesa, 2));

    $("#CodigoUO").val(Left(dadosEmpenho.Uo, 5));

    GerarComboPtres();

    var cont = 0;
    programasInfo.forEach(function (programa) {

        if (programa.Ptres == dadosEmpenho.Ptres && programa.Ano == $("#AnoExercicio").val()) {

            $('#Ptres option[value="' + programa.Ptres + '"]').attr("selected", true);
            cont += 1;
        }

    });

    SelecioarComboCfp();

    var msg = "";

    if (cont == 0)
        msg = "Ptres " + dadosEmpenho.Ptres + " não Cadastrado no SIDS";

    GerarComboNatureza();
    var contN = 0;
    var natureza = Left(dadosEmpenho.Despesa, 6);
    estruturaInfo.forEach(function (estrutura) {

        if (estrutura.Natureza == natureza && estrutura.Fonte == dadosEmpenho.Fonte.substr(1, 2)) {
            $('#Natureza option[value="' + estrutura.Codigo + '"]').attr("selected", true);
            contN += 1;
        }
    });


    if (contN == 0)
        msg = (msg.length === 0 ? "\n" : "") + "CED " + natureza + " não Cadastrada no SIDS";

    if (msg.length > 0)
        AbrirModal(msg);

}


//function consultarEmpenhoPorCredor() {
//    var online = navigator.onLine;

//    if (online != true) {
//        AbrirModal("Erro de conexão");
//        return false;
//    }
//   
//    DadosEmpenho(subempenho.NumeroOriginalProdesp, 3);

//    $.ajax({
//        datatype: 'JSON',
//        type: 'POST',
//        url: '/ConsutasBase/ConsultarSubempenhoApoio',
//        data: JSON.stringify({ subempenho: subempenho }),
//        contentType: 'application/json; charset=utf-8',
//        async: true,
//        beforeSend: function () {
//            waitingDialog.show('Consultando');
//        },
//        success: function (dados) {
//            if (dados.Status === 'Sucesso') {


//                $('#CodigoAplicacaoObra').val(dados.SubempenhoApoio.outAplicObra.replace(/(\d)(\d{1})$/, "$1-$2"));

//                if (dados.SubempenhoApoio.outAplicObra == null)
//                    $('#CodigoAplicacaoObra').removeAttr("disabled");

//                $('#NaturezaId').val(dados.SubempenhoApoio.outCED);
//                $("#FonteId option").text(dados.SubempenhoApoio.outOrigemRecurso);
//                $('#NumeroCNPJCPFFornecedorId').val(dados.SubempenhoApoio.outCGC);
//                $('#ProgramaId').val(dados.SubempenhoApoio.outCFP);
//                $('#CodigoCredorOrganizacaoId').val(dados.SubempenhoApoio.outOrganiz);
//                $('#RegionalId').val(dados.SubempenhoApoio.outOrgao);
//                $('#CenarioProdesp').val(dados.SubempenhoApoio.outInfoTransacao.replace("Cotrato", "Contrato"));
//                $('#NaturezaSubempenhoId').val(dados.SubempenhoApoio.outNatureza);
//                $('#NumeroMedicao').val(dados.SubempenhoApoio.outNumMedicao);
//                $('#Contrato').val(dados.SubempenhoApoio.outContrato.replace(" ", ".").replace(" ", "."));

//                if (dados.SubempenhoApoio.outCGC.length === 11) {  // valida se é cpf para preencher no campo cnpj/cpf/ug credor sifem / siafisico
//                    $('input#NumeroCNPJCPFCredor').val(dados.SubempenhoApoio.outCGC)
//                }

//                if (dados.SubempenhoApoio.outInfoTransacao === "SubEmpenhoContrato" ||
//                    dados.SubempenhoApoio.outInfoTransacao === "SubEmpenhoRecibo") {
//                    $("#DescricaoEspecificacaoDespesa1").val("PARA ATENDER AS DESPESAS DE SERVICO DA MEDICAO " + dados.SubempenhoApoio.outNumMedicao);
//                    $("#DescricaoEspecificacaoDespesa2").val("DO CONTRATO " + dados.SubempenhoApoio.outContrato);
//                    $("#DescricaoEspecificacaoDespesa3").val("NOTA FISCAL:");
//                }


//                liquidacao.displayHandlerButton();
//            }
//            else {
//                AbrirModal(dados.Msg);
//            }
//        },
//        error: function (dados) {
//            AbrirModal(dados);
//        },
//        complete: function () {
//            waitingDialog.hide();
//        }
//    });
//}

function consultarEmpenhoPorCredor() {
    var online = navigator.onLine;

    if (online != true) {
        AbrirModal("Erro de conexão");
        return false;
    }

    var num = $('#NumeroCNPJCPFFornecedorId').val().trim();
    for (var i; num.length < 15 && num !== ""; i++) {
        num = 0 + num;
    }

    var empenhoCredor = {
        NomeCredor: $('#NomeCredor').val().trim(),
        CodigoCredorOrganizacao: $('#CodigoCredorOrganizacaoId').val().trim(),
        NumeroCNPJCPFFornecedor: num,
        CredorCnpj: $('#CodigoCredorOrganizacaoId').val().trim() + $('#NumeroCNPJCPFFornecedorId').val().trim(),
        NumeroAnoExercicio: $('#AnoExercicio').val().trim()
    }

    var msg = "O campo Nome ou Credor Organização(Prodesp) e CNPJ/CPF(Prodesp) deve ser preenchido";

    if (empenhoCredor.NomeCredor === "" && empenhoCredor.CredorCnpj === "") {
        AbrirModal(msg); return false;
    } else if (empenhoCredor.NomeCredor !== "" && empenhoCredor.CredorCnpj !== "") {
        AbrirModal(msg); return false;
    } else if (empenhoCredor.NomeCredor !== "" && empenhoCredor.CodigoCredorOrganizacao !== "") {
        AbrirModal(msg); return false;
    } else if (empenhoCredor.NomeCredor !== "" && (empenhoCredor.NumeroCNPJCPFFornecedor !== "" && empenhoCredor.NumeroCNPJCPFFornecedor)) {
        AbrirModal(msg); return false;
    } else if (empenhoCredor.NumeroCNPJCPFFornecedor === "" && empenhoCredor.CodigoCredorOrganizacao !== "") {
        AbrirModal(msg); return false;
    } else if (empenhoCredor.NumeroCNPJCPFFornecedor !== "" && empenhoCredor.CodigoCredorOrganizacao === "") {
        AbrirModal(msg); return false;
    }



    waitingDialog.show('Consultando');
    $.ajax({
        datatype: 'json',
        type: 'Post',
        url: '/ConsutasBase/ConsultarEmpenhoPorCredor',
        data: JSON.stringify({ subempenho: empenhoCredor }),
        contentType: 'application/json; charset=utf-8',
        beforeSend: function () {
            waitingDialog.show('Consultando');
        },
        complete: function () {
            waitingDialog.hide();
        },
        success: function (dados) {
            if (dados.Status == "Sucesso") {

                table = dados.Subempenho.ListConsultarEmpenhoCredor;

                $('#modalConsultaEmpenhoCredor').modal();

                $('input#NomeCredorId').val(dados.Subempenho.outCredorReduzido);
                $('input#CodigoCredorOrganizacaoId').val(dados.Subempenho.outOrganiz);
                $('input#NumeroCNPJCPFFornecedorId').val(dados.Subempenho.outCGC);
                $('input#NomeCredor').val(dados.Subempenho.outCredorReduzido);

                GerarTabelaEmpenhoCredor(table);

            } else {
                waitingDialog.hide();
                AbrirModal(dados.Msg);
            }
        },
        error: function (dados) {
            waitingDialog.hide();
            AbrirModal(dados);
        }
    });

    //$.ajax({
    //    datatype: 'JSON',
    //    type: 'POST',
    //    url: '/ConsutasBase/ConsultarSubempenhoApoio',
    //    data: JSON.stringify({ subempenho: subempenho }),
    //    contentType: 'application/json; charset=utf-8',
    //    async: true,
    //    beforeSend: function () {
    //        waitingDialog.show('Consultando');
    //    },
    //    success: function (dados) {
    //        if (dados.Status === 'Sucesso') {


    //            $('#CodigoAplicacaoObra').val(dados.SubempenhoApoio.outAplicObra.replace(/(\d)(\d{1})$/, "$1-$2"));

    //            if (dados.SubempenhoApoio.outAplicObra == null)
    //                $('#CodigoAplicacaoObra').removeAttr("disabled");

    //            $('#NaturezaId').val(dados.SubempenhoApoio.outCED);
    //            $("#FonteId option").text(dados.SubempenhoApoio.outOrigemRecurso);
    //            $('#NumeroCNPJCPFFornecedorId').val(dados.SubempenhoApoio.outCGC);
    //            $('#ProgramaId').val(dados.SubempenhoApoio.outCFP);
    //            $('#CodigoCredorOrganizacaoId').val(dados.SubempenhoApoio.outOrganiz);
    //            $('#RegionalId').val(dados.SubempenhoApoio.outOrgao);
    //            $('#CenarioProdesp').val(dados.SubempenhoApoio.outInfoTransacao.replace("Cotrato", "Contrato"));
    //            $('#NaturezaSubempenhoId').val(dados.SubempenhoApoio.outNatureza);
    //            $('#NumeroMedicao').val(dados.SubempenhoApoio.outNumMedicao);
    //            $('#Contrato').val(dados.SubempenhoApoio.outContrato.replace(" ", ".").replace(" ", "."));

    //            if (dados.SubempenhoApoio.outCGC.length === 11) {  // valida se é cpf para preencher no campo cnpj/cpf/ug credor sifem / siafisico
    //                $('input#NumeroCNPJCPFCredor').val(dados.SubempenhoApoio.outCGC)
    //            }

    //            if (dados.SubempenhoApoio.outInfoTransacao === "SubEmpenhoContrato" ||
    //                dados.SubempenhoApoio.outInfoTransacao === "SubEmpenhoRecibo") {
    //                $("#DescricaoEspecificacaoDespesa1").val("PARA ATENDER AS DESPESAS DE SERVICO DA MEDICAO " + dados.SubempenhoApoio.outNumMedicao);
    //                $("#DescricaoEspecificacaoDespesa2").val("DO CONTRATO " + dados.SubempenhoApoio.outContrato);
    //                $("#DescricaoEspecificacaoDespesa3").val("NOTA FISCAL:");
    //            }


    //            liquidacao.displayHandlerButton();
    //        }
    //        else {
    //            AbrirModal(dados.Msg);
    //        }
    //    },
    //    error: function (dados) {
    //        AbrirModal(dados);
    //    },
    //    complete: function () {
    //        waitingDialog.hide();
    //    }
    //});

    // verificar se está requisição foi substituida pela funcao acima: ConsutasBase/ConsultarSubempenhoApoio

}

function provider() {
    body
        .on('click', '#btnConsultarContratoTodos', resumoEventHandler)
        .on('click', '#btnConfirmarContratoTodos', detalheEventHandler);
}

function GerarTabelaEmpenhoCredor(values) {


    $('#tblListaEmpenhoCredor > thead').remove();
    $('#tblListaEmpenhoCredor').append("<thead></thead>");

    $('#tblListaEmpenhoCredor > thead').append("<tr>" +
        "    <th>Nº do Empenho</th>" +
        "    <th>Contrato</th>" +
        "    <th>Liquido Empenhado</th>" +
        "    <th>Liq. Subempenhado</th>" +
        "    <th>Disponível a Subempenhar</th>" +
        "    <th></th>" +
        "</tr>");

    if (!$("#tblListaEmpenhoCredor").hasClass("_tbDataTables")) {
        $('#tblListaEmpenhoCredor').addClass("_tbDataTables");
        BuildDataTables.ById('#tblListaEmpenhoCredor');
    }

    $('#tblListaEmpenhoCredor').DataTable().clear().draw(true);

    $.each(values,
        function (index, x) {
            $('#tblListaEmpenhoCredor').DataTable().row.add([
                     x.outNrEmpenho,
                     x.outContrato,
                     x.outLiqEmpenhado,
                     x.outLiqSubEmpenhado,
                     x.outDisponivelSubEmpenhar,
                    "<button class='btn btn-xs btn-primary' onclick='DadosEmpenho(\"" + x.outNrEmpenho.replace(/[.,-]/g, "") + "\",0)'><i class='fa fa-search'></i></button>"
            ])
                .draw(true);
        });
}

function validateEntity(entity, message) {
    if (entity === undefined || entity === null || entity === 0) {
        AbrirModal(message);
        return false;
    }
}

function DadosSubempenho(id, type) {
    var online = navigator.onLine;

    if (online != true) {
        AbrirModal("Erro de conexão");
        return false;
    }

    validateEntity(id, 'Favor informar o número do Subempenho!');

    subempenhoSelec = id;
    $.ajax({
        datatype: 'JSON',
        type: 'POST',
        url: '/ConsutasBase/ConsultarSubempenho',
        data: JSON.stringify({ subempenho: id }),
        contentType: 'application/json; charset=utf-8',
        beforeSend: function () {
            //waitingDialog.show('Consultando');
        },
        success: function (dados) {
            if (dados.Status === 'Sucesso') {
                if (dados.SubempenhoFromDatabase !== null) {
                    if ($('#CenarioSiafemSiafisico').val() != undefined)
                        dados.SubempenhoFromDatabase.CenarioSiafemSiafisico = parseInt($('#CenarioSiafemSiafisico').val());

                    var ctr = dados.SubempenhoFromDatabase.NumeroContrato;
                    //dados.SubempenhoFromDatabase.NumeroSiafemSiafisico = '';

                    if (($('#Contrato').val() != undefined) && (ctr != null))
                        $('#Contrato').val(ctr.substr(0, 2) + "." + ctr.substr(2, 2) + "." + ctr.substr(4, 5) + "-" + ctr.substr(9, 1));

                    if (liquidacao.nlReferencia.length > 0) {
                        liquidacao.nlReferencia.val(dados.SubempenhoFromDatabase.NumeroSiafemSiafisico);
                        $('input#' + liquidacao.nlReferencia.prop('id')).trigger('blur');
                    }
                }
                EntitySubempenho = dados;
                carregarDadosConsulta(dados.Subempenho, type, id);


                if (dados.EmpenhoFromDatabase !== 'undefined' && dados.EmpenhoFromDatabase !== null) {
                    var empNum = dados.EmpenhoFromDatabase.NumeroEmpenhoSiafem == null ? dados.EmpenhoFromDatabase.NumeroEmpenhoSiafisico : dados.EmpenhoFromDatabase.NumeroEmpenhoSiafem;
                    $('#NumeroOriginalSiafemSiafisico').val(empNum);
                }
                else {
                    $('#NumeroOriginalSiafemSiafisico').val('');
                }

                if (dados.Subempenho !== 'undefined' && dados.Subempenho !== null) {
                    var contrato = formatarContratoProdesp(dados.Subempenho.outNumContrato);
                    $('#Contrato').val(contrato);
                }
                else {
                    $('#Contrato').val('');
                }
            }
            else {
                AbrirModal(dados.Msg);
            }
            if ($('#ValorRealizado').val() !== undefined) {
                $('#ValorRealizado').maskMoney('mask');
            }
            if ($('#Percentual').val() !== undefined) {
                $('#Percentual').mask("###.##000,00", { reverse: true, placeholder: "" });
            }

        },
        error: function (dados) {
            waitingDialog.hide();
            AbrirModal(dados);
        },
        complete: function () {
            waitingDialog.hide();
            //$('input#' + liquidacao.nlReferencia.prop('id')).trigger('blur');
        }
    });
}

function formatarContratoProdesp(ctr) {
    if (ctr.length === 0) {
        $(".contrato").mask("00.00.00000-0", { placeholder: "__.__._____-_" });
    } else {
        ctr = ctr.replace(" ", "").replace(" ", "");
        return ctr.substr(0, 2) + "." + ctr.substr(2, 2) + "." + ctr.substr(4, 5) + "-" + ctr.substr(9, 1)
    }
}

function carregarDadosConsulta(entity, type, id) {
    if (type === 1) {
        $('#modalConsultaContrato').modal('toggle');
        $('#modalDadosSubempenho').modal();
    }
    var subempenhoSelec = id;
    if (type === 0) {
        ConfirmarSubEmpenho();
    } else {

        $('#txtNumSubempenho').val(subempenhoSelec);
        $('#txtNumSubempenhoProcesso').val(entity.outNumProcesso);
        $('#txtNumSubempenhoContrato').val(entity.outNumContrato);
        $('#txtSubempenhoOrgao').val(entity.outOrgao);
        $('#txtSubempenhoCfp').val(entity.outCFP + "/" + entity.outCED);
        $('#txtNumSubempenhoAplicacao').val(entity.outCodAplicacao);
        $('#txtSubempenhoValorOriginal').val(entity.outValorOriginal);
        $('#txtSubempenhoValorReal').val(entity.outValorReal);
        $('#txtSubempenhoValorAnulado').val(entity.outValorAnulado);
        $('#txtSubempenhoSaldo').val(entity.outSaldo);
        $('#txtSubempenhoTarefa').val(entity.outTarefa);
        $('#txtSubempenhoDataReal').val(entity.outDataRealizacao);
        $('#txtSubempenhoDataReg').val(entity.outDataReg);
        $('#txtSubempenhoDataVenc').val(entity.outDataVencimento);
        $('#txtSubempenhoCredor').text(entity.outCredor1 + "\n" + entity.outCredor2);
        $('#txtSubempenhoAutorizadoNome').val(entity.outAutorizador);
        $('#txtSubempenhoAutorizadoCargo').val(entity.outCargoAutorizador);
        $('#txtSubempenhoExaminadoNome').val(entity.outExaminador);
        $('#txtSubempenhoExaminadoCargo').val(entity.outCargoExaminador);
        $('#txtSubempenhoResponsavelNome').val(entity.outResponsavel);
        $('#txtSubempenhoResponsavelCargo').val(entity.outCargoResponsavel);
    }
}

function ConfirmarSubEmpenho() {
    var entity = EntitySubempenho.Subempenho;
    var dataBaseEntity = EntitySubempenho.SubempenhoFromDatabase;

    if ($('#modalDadosSubempenho').is(":visible")) {
        $('#modalDadosSubempenho').modal();
    }

    $("#NumeroDocumento").val(subempenhoSelec);
    $('#NumeroSubempenhoProdesp').val(subempenhoSelec);
    $('#NumeroProcesso').val(entity.outNumProcesso);

    if (controller === "RapRequisicao") {
        if ($('#DescricaoEspecificacaoDespesa1').val() === '') {
            $('#DescricaoEspecificacaoDespesa1').val(entity.outDespesa_1);
            $('#DescricaoEspecificacaoDespesa2').val(entity.outDespesa_2);
            $('#DescricaoEspecificacaoDespesa3').val(entity.outDespesa_3);
            $('#DescricaoEspecificacaoDespesa4').val(entity.outDespesa_4);
            $('#DescricaoEspecificacaoDespesa5').val(entity.outDespesa_5);
            $('#DescricaoEspecificacaoDespesa6').val(entity.outDespesa_6);
            $('#DescricaoEspecificacaoDespesa7').val(entity.outDespesa_7);
            $('#DescricaoEspecificacaoDespesa8').val(entity.outDespesa_8);
        }
    } else {
        $('#DescricaoEspecificacaoDespesa1').val(entity.outDespesa_1);
        $('#DescricaoEspecificacaoDespesa2').val(entity.outDespesa_2);
        $('#DescricaoEspecificacaoDespesa3').val(entity.outDespesa_3);
        $('#DescricaoEspecificacaoDespesa4').val(entity.outDespesa_4);
        $('#DescricaoEspecificacaoDespesa5').val(entity.outDespesa_5);
        $('#DescricaoEspecificacaoDespesa6').val(entity.outDespesa_6);
        $('#DescricaoEspecificacaoDespesa7').val(entity.outDespesa_7);
        $('#DescricaoEspecificacaoDespesa8').val(entity.outDespesa_8);
    }
    $('#CodigoEspecificacaoDespesa').val("000");

    if (dataBaseEntity != null) {
        if (controller === "SubempenhoCancelamento") {
            dataBaseEntity.NumeroSubempenhoProdesp = dataBaseEntity.NumeroProdesp;
            dataBaseEntity.NumeroProdesp = "";
            dataBaseEntity.NumeroSiafemSiafisico = "";
        }

        if (controller === "SubempenhoCancelamento") {
            AtualizarFormulario(dataBaseEntity);
            if (dataBaseEntity.NumeroContrato !== null && dataBaseEntity.NumeroContrato !== undefined) {
                dataBaseEntity.NumeroContrato = dataBaseEntity.NumeroContrato.replace(/\s/g, "").replace(/[\.-]/g, "").replace(/(\d{2})(\d{2})(\d{5})(\d{1})/g, "\$1.\$2.\$3\-\$4");
            }
            $('#Contrato').val(dataBaseEntity.NumeroContrato);
        }


    }

}

function formatarNatureza(entity) {
    var text = "";
    return text.concat(StringIsNull(entity.OutCed1, ""), '.', StringIsNull(entity.OutCed2, ""), '.', StringIsNull(entity.OutCed3, ""), '.', StringIsNull(entity.OutCed4, ""), '.', StringIsNull(entity.OutCed5, ""));
}

function formatarPrograma(entity) {
    var text = "";
    return text.concat(StringIsNull(entity.OutCfp1, ""), '.', StringIsNull(entity.OutCfp2, ""), '.', StringIsNull(entity.OutCfp3, ""), '.', StringIsNull(entity.OutCfp4, ""), '.', StringIsNull(entity.OutCfp5, ""));
}

function formatarEspecificacaoDespesa(entity) {
    var text = "";
    return text.concat(StringIsNull(entity.OutEspecDespesa1, ""), '\n', StringIsNull(entity.OutEspecDespesa2, ""), '\n', StringIsNull(entity.OutEspecDespesa3, ""), '\n',
        StringIsNull(entity.OutEspecDespesa4, ""), '\n', StringIsNull(entity.OutEspecDespesa5, ""), '\n', StringIsNull(entity.OutEspecDespesa5, ""), '\n', StringIsNull(entity.OutEspecDespesa7, ""), '\n', StringIsNull(entity.OutEspecDespesa8, ""), '\n', StringIsNull(entity.OutEspecDespesa9, ""));
}

function formatarCfp(entity) {
    return formatarPrograma(entity).concat(' / ', formatarNatureza(entity));
}

function DadosRap(id) {
    var online = navigator.onLine;

    if (online != true) {
        AbrirModal("Erro de conexão");
        return false;
    }

    //$("#Valor").val('0,00');
    //$("#ValorAnulado").val('0,00');
    //$('#Classificacao').val('');
    //$('#NumeroProcesso').val('');
    //$('#DescricaoAutorizadoSupraFolha').val('')

    var anulacao = {
        NumeroOriginalProdesp: $('#NumeroSubempenhoProdesp').length > 0 ? $('#NumeroSubempenhoProdesp').val().replace("/", "") : $('#NumeroSubempenhoProdesp').val(),
        //Valor: $('#ValorAnular').val().replace("R$", "").replace(/[.,]/g, "")
        Valor: $('#ValorAnular').length > 0 ? $('#ValorAnular').val().replace("R$", "").replace("0,00", "").replace(",", "").trim() : $('#ValorAnular').val()
    }

    if (anulacao.NumeroOriginalProdesp !== undefined) {
        if (anulacao.NumeroOriginalProdesp.length === 0 || anulacao.Valor.length === 0) {
            AbrirModal('Os Campos Nº do Subempenho e o Valor a Anular devem ser preenchidos.');
            return false;
        }
    }

    validateEntity(id, 'Favor informar o número do Subempenho!');

    $.ajax({
        datatype: 'JSON',
        type: 'POST',
        url: '/ConsutasBase/ConsultarRap',
        data: JSON.stringify({ rap: id }),
        contentType: 'application/json; charset=utf-8',
        beforeSend: function () {
            waitingDialog.show('Consultando');
        },
        success: function (dados) {
            if (dados.Status === 'Sucesso') {
                if (dados.dataBaseEntity != null) {
                    var valor = dados.dataBaseEntity;

                    if ($('#CenarioProdesp').val === "ComContrato") {
                        $('#CodigoTarefa').val(valor.CodigoTarefa);
                        $('#Classificacao').val(valor.Classificacao);
                        $('#NumeroProcesso').val(valor.NumeroProcesso);
                        $('#DescricaoAutorizadoSupraFolha').val(valor.DescricaoAutorizadoSupraFolha)
                    }

                    $("#ValorAnulado").val(valor.Valor);
                    $("#Valor").val(valor.Valor);

                    $("#ValorAnulado").attr("disabled", true);
                    $("#Valor").attr("disabled", true);

                    $(".real").maskMoney('mask');

                    liquidacao.SaldoAnulacao();
                }
            }
            else {
                AbrirModal(dados.Msg);
            }
        },
        error: function (dados) {
            AbrirModal(dados);
        },
        complete: function () {
            waitingDialog.hide();
        }
    });
}

function PoopUpDesdobrmento() {

    var entity = {
        DocumentoTipoId: $("#DocumentoTipoId").val(),
        NumeroDocumento: $("#NumeroDocumento").val()
    }

    if (entity.DocumentoTipoId == "") {
        AbrirModal("Campo Tipo Documento deve ser selecionado");
        return false;
    }

    if (entity.NumeroDocumento == "") {
        AbrirModal("Campo N° Documento deve ser preenchido");
        return false;
    }
    window.open("/ConsutasBase/ConsultaDesdobramento", 'popup', 'STATUS=NO, TOOLBAR=NO, LOCATION=NO, DIRECTORIES=NO, RESISABLE=NO, SCROLLBARS=YES,TOP=10, LEFT=10,  WIDTH=992, HEIGHT=590');
}


function ConsultaCredorCNPJ(cnpjCredor) {
    $("#NumeroCNPJCPFFornecedorId").val(cnpjCredor);
    $("#NumeroCnpjcpfFavorecido").val(cnpjCredor);
}




function ConsultaCredorPorNome(nomeCredor, value, type) {
    var credorUrl;

    if (type == 1)
        credorUrl = "/PagamentoContaUnica/Desdobramento/ConsultarNomeReduzido";
    else
        credorUrl = "/PagamentoContaUnica/Desdobramento/ConsultarNomePrefeirura";

    $.ajax({
        datatype: 'JSON',
        type: 'POST',
        url: credorUrl,
        data: JSON.stringify({ nome: nomeCredor }),
        contentType: "application/json; charset=utf-8",
        beforeSend: function () {
            waitingDialog.show('Consultando');
        },
        success: function (dados) {
            if (dados.Status === 'Sucesso') {
                if (dados.Credor != null) {
                    $("#NumeroCNPJCPFFornecedorId").val(dados.Credor.CpfCnpjUgCredor.replace(/[\/.-]/g, ""));
                    $("#NumeroCnpjcpfFavorecido").val(dados.Credor.CpfCnpjUgCredor.replace(/[\/.-]/g, ""));
                }
                $("#ValorUnitario").val(value);
                $("#Valor").val(value);
                $("ValorUnitario").maskMoney('mask');
                $("Valor").maskMoney('mask');
            }
            else {
                //AbrirModal(dados.Msg);
                $("#NumeroCNPJCPFFornecedorId").val("");
                $("#NumeroCnpjcpfFavorecido").val("");
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