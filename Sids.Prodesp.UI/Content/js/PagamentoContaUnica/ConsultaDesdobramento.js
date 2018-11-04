var tipoDesdrobramento;
var table;
var obj = 'ReclassificacaoRetencao';
var desdobramento;

$(document).ready(function () {
    ConsultaDesdobramento();

});


//function AbrirModal(msg, func) {
//    $("html").css("overflow-y", "hidden");
//    $("#modalMessage").modal();
//    $("#modalMessage #Message").html("Erro!");
//    $("#modalMessage #value").html(msg);
//    $("#modalMessage .close").click(func);
//}

//function FecharModal(id) {
//    $("html").css("overflow-y", "scroll");
//    $(id).modal("toggle");
//}

function FecharPaginaModal() {
    window.close();
}

function ConsultaDesdobramento() {

    var entity = {
        DocumentoTipoId: window.opener.$("#DocumentoTipoId").val(),
        NumeroDocumento: window.opener.$("#NumeroDocumento").val()
    }

    var result;


    $.ajax({
        datatype: 'JSON',
        type: 'POST',
        url: "ConsultarDesdobramento",
        data: JSON.stringify({ entity: entity }),
        contentType: "application/json; charset=utf-8",
        beforeSend: function () {
            waitingDialog.show('Consultando');
        },
        success: function (dados) {
            if (dados.Status === 'Sucesso') {

                desdobramento = dados.Desdobramento[0];
                table = dados.Desdobramento;

                tipoDesdrobramento = desdobramento.outTotalISSQN == "" ? "Outros" : "ISSQN";

                //$('#modalConsultaDesdobramento').modal();

                var idTipo = window.opener.$("#DocumentoTipoId").val();
                var descTipo = window.opener.$("#DocumentoTipoId option[value='" + idTipo + "']").text();

                $('input#modalTipoDocumento').val(descTipo);
                $('input#modalNumeroDocumento').val(window.opener.$("#NumeroDocumento").val());

                if (tipoDesdrobramento == 'ISSQN') {
                    $('input#modalServico').val(desdobramento.outServico);
                    $('#divServico').show();
                    $('#divTipoBloqueio').hide();
                    GerarTabelaDesdobramentoIssqn(table);
                } else {
                    $('input#modalTipoBloqueio').val(desdobramento.outTipoBloqueioDoc);
                    $('#divTipoBloqueio').show();
                    $('#divServico').hide();
                    GerarTabelaDesdobramentoOutros(table);
                }

                $('input#modalCredor').val(desdobramento.outCredorDocumento);
                $('input#modalTipoDesdobramento').val(tipoDesdrobramento);
                $('input#modalValor').val(desdobramento.outValorDocOriginal);
                $('input#modalCodCredor').val(desdobramento.outCodCredor);

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
    return result;
}

function GerarTabelaDesdobramentoIssqn(values) {

    if ($("#tbllistaConsultaDesdobramento").hasClass("_tbDataTables")) {
        $('#tbllistaConsultaDesdobramento').removeClass("_tbDataTables");
        $('#tbllistaConsultaDesdobramento').DataTable().destroy();
    }

    $('#tbllistaConsultaDesdobramento > thead').remove();
    $('#tbllistaConsultaDesdobramento').append("<thead></thead>");

    $('#tbllistaConsultaDesdobramento > thead').append("<tr>" +
        "    <th>Credor</th>" +
        "    <th>Valor Distribuição</th>" +
        "    <th>Base Calc.</th>" +
        "    <th>Valor Base Cálculo</th>" +
        "    <th>Aliq.</th>" +
        "    <th>Valor</th>" +
        "    <th></th>" +
        "</tr>");

    if (!$("#tbllistaConsultaDesdobramento").hasClass("_tbDataTables")) {
        $('#tbllistaConsultaDesdobramento').addClass("_tbDataTables");
        BuildDataTables.ById('#tbllistaConsultaDesdobramento');
    }

    $('#tbllistaConsultaDesdobramento').DataTable().clear().draw(true);

    $.each(values,
        function (index, x) {
            $('#tbllistaConsultaDesdobramento').DataTable().row.add([
                     x.outCredor,
                     x.outValorDistribuicao,
                     x.outBaseCalc,
                     x.outValorBaseCalc,
                     x.outAliq,
                     x.outValor,
                    "<button class='btn btn-warning' onclick='ConsultaNomeCredorLista(this)'><i>Confirmar</i></button>"
            ])
                .draw(true);
        });
}

function GerarTabelaDesdobramentoOutros(values) {

    if ($("#tbllistaConsultaDesdobramento").hasClass("_tbDataTables")) {
        $('#tbllistaConsultaDesdobramento').removeClass("_tbDataTables");
        $('#tbllistaConsultaDesdobramento').DataTable().destroy();
    }

    $('#tbllistaConsultaDesdobramento > thead').remove();
    $('#tbllistaConsultaDesdobramento').append("<thead></thead>");

    $('#tbllistaConsultaDesdobramento > thead').append("<tr>" +
        "    <th>Desdobramento</th>" +
        "    <th>Nome Credor Reduzido</th>" +
        "    <th>Valor</th>" +
        "    <th>tipo Bloqueio</th>" +
        "    <th></th>" +
        "</tr>");

    if (!$("#tbllistaConsultaDesdobramento").hasClass("_tbDataTables")) {
        $('#tbllistaConsultaDesdobramento').addClass("_tbDataTables");
        BuildDataTables.ById('#tbllistaConsultaDesdobramento');
    }

    $('#tbllistaConsultaDesdobramento').DataTable().clear().draw(true);

    $.each(values,
        function (index, x) {
            $('#tbllistaConsultaDesdobramento').DataTable().row.add([
                     x.outDesdob,
                     x.outNomeReduzCred,
                     x.outValor,
                     x.outTipoBloqueio,
                    "<button class='btn btn-warning' onclick='ConsultaNomeCredorLista(this)'><i>Confirmar</i></button>"
            ])
                .draw(true);
        });
}


function ConsultaCNPJCredor() {
    window.opener.ConsultaCredorCNPJ($("#modalCodCredor").val());
    window.close();
}


function ConsultaNomeCredor() {
    window.opener.ConsultaCredorPorNome($("#modalCredor").val(), $("#modalValor").val(),2);
    window.close();
}
function ConsultaNomeCredorLista(nRow) {

    var selectedRow = $(nRow).parent();
    var aData = $("#tbllistaConsultaDesdobramento").dataTable().fnGetData(selectedRow);

    if(tipoDesdrobramento == "Outros")
        window.opener.ConsultaCredorPorNome(aData[1], aData[2],1);
    else {
        window.opener.ConsultaCredorPorNome(aData[0], aData[1],1);
    }
    window.close();
}