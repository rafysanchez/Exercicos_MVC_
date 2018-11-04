var obj = "EmpenhoCancelamento";
var area = "Empenho";
var quebras = 0;
var tans = "";
var isLoad;
var meses = [];
var itens = [];
var selectedRow = "";
var tipoEstrutura = "Empenho";
var tipoContrato = "2";
var StatusSiafisicoItemGrid = "";

$(document).ready(function () {
    isLoad = true;

    if (ModelItem.TransmitidoProdesp == true)
        $(".lockProdesp").attr("disabled", "disabled");

    if (ModelItem.TransmitidoSiafem == true)
        $(".lockSiafem").attr("disabled", "disabled");

    if (ModelItem.TransmitidoSiafisico == true)
        $(".lockSiafisico").attr("disabled", "disabled");

    $('#divNew').show();
    $('#divSave').hide();


    var prodespCheckbox = $('#transmitirPRODESP');
    var siafemCheckbox = $('#transmitirSIAFEM');
    var siafisicoCheckbox = $('#transmitirSIAFISICO');

    var mydate = new Date();
    var year = mydate.getYear();
    var month = mydate.getMonth();

    if (year < 1000)
        year += 1900;

    $('#new').click(function (e) {
        e.preventDefault();
        addRow();
    });

    $('#save').click(function (e) {
        e.preventDefault();
        saveRow();
    });

    if (ModelItem.Id > 0) {
        MakMoeda();
        $("#Obra").val($("#Obra").val().replace(/(\d)(\d{1})$/, "$1-$2"));
    }

    $("#Regional").attr("ReadOnly", usuario.RegionalId != 1);
    $('#Regional > option:not(:selected)').attr('disabled', usuario.RegionalId != 1);

    prodespCheckbox.change(function () {
        if (prodespCheckbox.is(":checked")) {
            $(".prodesp").show();
            hideSIAFEM();
            hideSIAFISICO();
        }
    });

    siafemCheckbox.change(function () {
        if (siafemCheckbox.is(":checked")) {
            siafisicoCheckbox.removeAttr("checked");
            $("#ValorTotal").maskMoney({ prefix: "R$ ", allowNegative: false, thousands: ".", decimal: ",", affixesStay: true });
            $("#ValorTotal").maskMoney('mask');
            $('#ValorTotal').removeAttr("readonly");
        }
        else {
            $("#ValorTotal").maskMoney('destroy');
            siafisicoCheckbox.prop("checked", "checked");
            $('#ValorTotal').attr("readonly", "true");
        }

        hideSIAFISICO();
        hideSIAFEM();
    });

    siafisicoCheckbox.change(function () {
        if (siafisicoCheckbox.is(":checked")) {
            $("#ValorTotal").maskMoney('destroy');
            siafemCheckbox.removeAttr("checked");
            $('#ValorTotal').attr("readonly", "true");
        }
        else {
            siafemCheckbox.prop("checked", "checked");
            $("#ValorTotal").maskMoney({ prefix: "R$ ", allowNegative: false, thousands: ".", decimal: ",", affixesStay: true });
            $("#ValorTotal").maskMoney('mask');
            $('#ValorTotal').removeAttr("readonly");
        }

        hideSIAFEM();
        hideSIAFISICO();
    });

    if (siafemCheckbox.is(":checked")) {
        siafisicoCheckbox.removeAttr("checked");
        $("#ValorTotal").maskMoney({ prefix: "R$ ", allowNegative: false, thousands: ".", decimal: ",", affixesStay: true });
        $("#ValorTotal").maskMoney('mask');
        $('#ValorTotal').removeAttr("readonly");
    }
    else {
        $("#ValorTotal").maskMoney('destroy');
        siafisicoCheckbox.attr("checked", "checked");
        $('#ValorTotal').attr("readonly", "true");
    }


    if (ModelItem.TransmitidoProdesp == 'true')
        $(".prodesp").attr("disabled", "disabled");

    if (ModelItem.TransmitidoSiafem == 'true')
        $(".siafem").attr("disabled", "disabled");

    if (ModelItem.TransmitidoSiafisico == 'true')
        $(".siafisico").attr("disabled", "disabled");


    if (month < 3) {
        $("#AnoExercicio").removeAttr("ReadOnly");
    } else {
        $("#AnoExercicio").attr("ReadOnly", "True");
    }


    $(".real").maskMoney({ prefix: "R$ ", allowNegative: false, thousands: ".", decimal: ",", affixesStay: true });
    $(".real").maskMoney('mask');
    SomarTotal();

    $("#ValorTotal").maskMoney('mask');

    $("#ValorUnitario").maskMoney({ prefix: "R$ ", allowNegative: false, thousands: ".", decimal: ",", affixesStay: true });
    $("#ValorUnitario").maskMoney('mask');
    MultValorTotal();

    $('#QuantidadeMaterialServico').change(function () {
        MultValorTotal();
    });

    $('#ValorUnitario').change(function () {
        MultValorTotal();
    });

    $("#btnSalvar").click(function () {
        tans = "S";
        $("#frmPainelCadastrar").data("bootstrapValidator").resetForm();
        $("#frmPainelCadastrar").submit();
    });

    $(".NoEspecial").blur("input", function (button) {
        button.target.value = button.target.value.replace(/[^a-zA-Z0-9 /]+/i, "");
    }).keyup("input", function (button) {
        button.target.value = button.target.value.replace(/[^a-zA-Z0-9 /]+/i, "");
    });


    $("#AnoExercicio").change(function () {
        selecionado = "0";
        GerarComboPtres();
        GerarComboCfp();
        GerarComboNatureza();
    });

    if (!isLoad) {
        GerarComboPtres();
        GerarComboCfp();
        GerarComboNatureza();
    }

    $("#btnTransmitir").click(function () {
        tans = "T";
        $("#frmPainelCadastrar").data("bootstrapValidator").resetForm();
        $("#frmPainelCadastrar").submit();
    });

    $("#Ptres").change(function () {

        if (!isLoad || ModelItem.Id == 0) {
            selecionado = "0";
            SelecioarComboCfp();
            GerarComboNatureza();
        }
    });

    $("#Programa").change(function () {
        if (!isLoad) {
            SelecionarComboPtres();
            GerarComboNatureza();
        }
    });

    $("#Natureza").change(function () {
        if (!isLoad) {
            GerarAplicacao("Obra");
        }
    });


    GerarUGO();
    $("#Regional").change(function () {
        GerarUGO();
    });


    CarregarTextArea("DescEspecificacaoDespesa", 9, ModelItem.DescricaoEspecificacaoDespesa);

    CarregarTextArea("DescricaoItemSiafem", 10, ModelItemEmpenho.DescricaoItemSiafem);

    CarregarTextArea("DescricaoJustificativaPreco", 2, ModelItemEmpenho.DescricaoItemSiafem);

    CarregarTextArea("DescricaoInformacoesAdicionaisEntrega", 3, ModelItem.DescricaoInformacoesAdicionaisEntrega);


    $("div > #Valor").change(function () { SomarTotal(); });

    $("#CodigoUnidadeGestoraFornecedora").change(function () {
        if ($("#CodigoUnidadeGestoraFornecedora").val().length > 0 && $("#CodigoGestaoFornecedora").val().length > 0) {
            $("#credor").val("");
            $("#credor").attr("ReadOnly", true);
        }
        else
            $("#credor").removeAttr("ReadOnly");
    });


    $("#CodigoGestaoFornecedora").change(function () {
        if ($("#CodigoUnidadeGestoraFornecedora").val().length > 0 && $("#CodigoGestaoFornecedora").val().length > 0) {
            $("#credor").val("");
            $("#credor").attr("ReadOnly", true);
        }
        else
            $("#credor").removeAttr("ReadOnly");
    });


    var optarray = $("#EmpenhoCancelamentoTipoId").children('option').map(function () {
        return {
            "value": this.value,
            "option": "<option value='" + this.value + "'>" + this.text + "</option>"
        };
    });

    optarray.sort(function (a, b) {
        var arel = parseInt($(a).attr('value')) || 0;
        var brel = parseInt($(b).attr('value')) || 0;
        return arel == brel ? 0 : arel < brel ? -1 : 1;
    });

    siafemCheckbox.change(function () {
        $("#EmpenhoCancelamentoTipoId").children('option').remove();
        var addoptarr = [];
        if (siafemCheckbox.is(":checked")) {
            for (i = 0; i < optarray.length; i++) {
                if (i < 8) { addoptarr.push(optarray[i].option); }
            }
            $.each(addoptarr, function (i, item) {
                $('#EmpenhoCancelamentoTipoId').append(item);
            });
            $('#EmpenhoCancelamentoTipoId option:eq(0)').prop('selected', true);
        }
    });
    siafisicoCheckbox.change(function () {
        $("#EmpenhoCancelamentoTipoId").children('option').remove();
        var addoptarr = [];
        if (siafisicoCheckbox.is(":checked")) {
            for (i = 0; i < optarray.length; i++) {
                if (i > 7 || i == 0) { addoptarr.push(optarray[i].option); }
            }
            $.each(addoptarr, function (i, item) {
                $('#EmpenhoCancelamentoTipoId').append(item);
            });
            $('#EmpenhoCancelamentoTipoId option:eq(0)').prop('selected', true);
        }
    });
    IniciarCreateEdit("Empenho");

    isLoad = false;

    hideSIAFEM();
    hideSIAFISICO();
    exibeSiAFEMLoad();
    hidenCamposEspecificos()


    if (getParameterByName("tipo") == "a")
        $("#titulo").html($("#titulo").html().replace("Visualizar", "Alterar"));


    $("#EmpenhoCancelamentoTipoId").change(function () {
        var value = $(this).val();
        var evento = "";
        switch (value) {
            case "1":
                evento = "400093";
                break;
            case "2":
                evento = "400097";
                break;
            case "3":
                evento = "400096";
                break;
            case "4":
                evento = "400101";
                break;
            case "5":
                evento = "400100";
                break;
            case "6":
                evento = "400093";
                break;
            case "7":
                evento = "400093";
                break;
        }
        $("#CodigoEvento").val(evento);
    });

});


function hideSIAFISICO() {
    $("div > .siafisico").hide();
    $(".siafisico").hide();

    if ($("#transmitirSIAFISICO").is(":checked")) {
        $("div > .siafisico").show();
        $(".siafisico").show();
    }
}

function hideSIAFEM() {
    $("div > .siafem").hide();
    $(".siafem").hide();

    if ($("#transmitirSIAFEM").is(":checked")) {
        $("div > .siafem").show();
        $(".siafem").show();
    }
}

function exibeSiAFEMLoad() {
    if (($("#transmitirSIAFEM").is(":checked")) && ($("#transmitirSIAFISICO").not(":checked"))) {
        $("div > .siafem").show();
        $(".siafem").show();
    }
}

function hidenCamposEspecificos() {
    $("div > .divEscondeCampo").hide();
    $(".divEscondeCampo").hide();
}

function Salvar() {
    if (navigator.onLine != true) {
        AbrirModal("Erro de conexão");
        return false;
    }

    CreateInstanceModel();
    CreateInstanceModelMes();
    CreateInstanceModelItem();

    var modelSalvar = { Model: ModelItem, Itens: itens, Meses: meses };

    $.ajax({
        datatype: "json",
        type: "Post",
        url: "/Empenho/EmpenhoCancelamento/Save",
        cache: false,
        data: JSON.stringify(modelSalvar),
        contentType: "application/json; charset=utf-8",
        beforeSend: function () {
            waitingDialog.show("Salvando");
        },
        success: function (dados) {
            if (dados.Status == "Sucesso") {
                $("#Id").val(dados.Id);
                $.confirm({
                    text: "Cancelamento " + AcaoRealizada(tipoAcao, "EmpenhoCancelamento") + " com sucesso!",
                    title: "Confirmação",
                    cancel: function () {
                        window.location.href = "/Empenho/EmpenhoCancelamento/Edit/" + dados.Id + "?tipo=a";
                    },
                    cancelButton: "Fechar",
                    confirmButton: false
                });
            } else {
                AbrirModal(dados.Msg);
            }
        },
        error: function (dados) {
            AbrirModal(dados);
        },
        complete: function () {
            $("#frmPainelCadastrar").data("bootstrapValidator").resetForm();
            waitingDialog.hide();
        }
    });
}

function Transmitir() {
    if (navigator.onLine != true) {
        AbrirModal("Erro de conexão");
        return false;
    }

    CreateInstanceModel();
    CreateInstanceModelMes();
    CreateInstanceModelItem();

    if ($("#transmitirSIAFISICO").is(":checked")) {
        validaItemsSiafisico();
    }

    var modelSalvar = { Model: ModelItem, Itens: itens, Meses: meses };
    var objetos = JSON.stringify(modelSalvar);
    
    Transmissao(objetos, "EmpenhoCancelamento");
}

function CreateInstanceModelMes() {
    meses = [];

    $.each($("div > #Valor"), function (index, value) {
        if (value.value != "") {
            var valor = value.value.replace(/[\.,R$ ]/g, "");

            var mes = {
                Codigo: 0,
                Descricao: value.name.replace("Mes", ""),
                ValorMes: parseFloat(valor),
                Id: ModelItem.Codigo
            };

            meses[meses.length] = mes;
        }
    });
}

function CreateInstanceModelItem() {
    itens = [];

    if ($("#transmitirSIAFEM").is(":checked")) {
        if ($("#ValorTotal").val() != "") {
            var valor = $("#ValorTotal").val().replace(/[\.,R$ ]/g, "");

            itens[itens.length] = {
                Id: 0,
                EmpenhoId: ModelItem.Id,

                DescricaoUnidadeMedida: $("#DescricaoUnidadeMedida").val(),
                DescricaoItemSiafem: Concatenar('DescricaoItemSiafem'),
                ValorTotal: valor,
                ValorUnitario: valor,
                QuantidadeMaterialServico: 1
            };
        }
    }
    else {
        itens = ModelItemList;
    }
}

function CreateInstanceModel() {
    //ModelItem.CodigoCancelamento = $("#Id").val();
    ModelItem.CodigoAplicacaoObra = $("#Obra").val().replace(/[\.-]/g, "");
    ModelItem.RegionalId = $("#Regional").val();
    ModelItem.FonteId = $("#Fonte").val();
    ModelItem.CodigoFonteSiafisico = $("#CodigoFonteSiafisico").val();
    ModelItem.NumeroEmpenhoProdesp = $("#NumeroEmpenhoProdesp").val();
    ModelItem.NumeroEmpenhoSiafem = $("#NumeroEmpenhoSiafem").val();
    ModelItem.NumeroEmpenhoSiafisico = $("#NumeroEmpenhoSiafisico").val();
    ModelItem.CodigoEmpenho = $("#CodigoEmpenho").val();
    ModelItem.CodigoEmpenhoOriginal = $("#CodigoEmpenhoOriginal").val();
    ModelItem.NumeroContrato = Number($("#Contrato").val().replace(/[\.-]/g, ""));
    ModelItem.NumeroCT = $("#NumeroCT").val();
    ModelItem.NumeroOriginalCT = $("#NumeroOriginalCT").val();
    ModelItem.CodigoReserva = $("#Reserva").val();
    ModelItem.NumeroAnoExercicio = $("#AnoExercicio").val();
    ModelItem.ProgramaId = $("#Programa").val();
    ModelItem.NumeroProcesso = $("#Processo").val();
    ModelItem.CodigoUnidadeGestora = $("#CodigoUnidadeGestora").val();
    ModelItem.CodigoGestao = $("#CodigoGestao").val();
    ModelItem.CodigoEvento = $("#CodigoEvento").val();
    ModelItem.CodigoNaturezaItem = $("#CodigoNaturezaItem").val();
    ModelItem.NaturezaId = $("#Natureza").val();
    ModelItem.NumeroCNPJCPFFornecedor = $("#fornecedor").val();
    ModelItem.CodigoNaturezaItem = $("#CodigoNaturezaItem").val();
    ModelItem.CodigoMunicipio = $("#CodigoMunicipio").val();
    ModelItem.DescricaoAcordo = $("#DescricaoAcordo").val();
    ModelItem.DescricaoLocalEntregaSiafem = $("#DescricaoLocalEntregaSiafem").val();
    ModelItem.CodigoUnidadeFornecimento = $("#CodigoUnidadeFornecimento").val();
    ModelItem.EmpenhoCancelamentoTipoId = $("#EmpenhoCancelamentoTipoId").val();

    ModelItem.DataEmissao = $('#DataEmissao').val();
    ModelItem.DataCadastramento = $("#DataCadastramentoDe").val();
    ModelItem.NumeroOC = $("#NumeroOC").val();
    ModelItem.CodigoUnidadeGestoraFornecedora = $("#CodigoUnidadeGestoraFornecedora").val();
    ModelItem.CodigoGestaoFornecedora = $("#CodigoGestaoFornecedora").val();
    ModelItem.NumeroCNPJCPFUGCredor = $("#NumeroCNPJCPFUGCredor").val();
    ModelItem.CodigoGestaoCredor = $("#CodigoGestaoCredor").val();
    ModelItem.CodigoCredorOrganizacao = $("#CodigoCredorOrganizacao").val();
    ModelItem.CodigoUO = $("#CodigoUO").val();
    ModelItem.CodigoUGO = $("#Ugo").val();
    ModelItem.ModalidadeId = $("#ModalidadeId").val();
    ModelItem.LicitacaoId = $("#LicitacaoId").val();
    ModelItem.DescricaoReferenciaLegal = $("#DescricaoReferenciaLegal").val();
    ModelItem.TipoAquisicaoId = $("#TipoAquisicaoId").val();
    ModelItem.NumeroContratoFornecedor = $("#NumeroContratoFornecedor").val();
    ModelItem.NumeroEdital = $("#NumeroEdital").val();
    ModelItem.DestinoId = $("#DestinoId").val();
    ModelItem.DescricaoLocalEntregaSiafem = $("#DescricaoLocalEntregaSiafem").val();
    ModelItem.DataEntregaMaterial = $("#DataEntregaMaterial").val();
    ModelItem.DescricaoLogradouroEntrega = $("#DescricaoLogradouroEntrega").val();
    ModelItem.DescricaoBairroEntrega = $("#DescricaoBairroEntrega").val();
    ModelItem.DescricaoCidadeEntrega = $("#DescricaoCidadeEntrega").val();
    ModelItem.NumeroCEPEntrega = $("#NumeroCEPEntrega").val();
    ModelItem.DescricaoInformacoesAdicionaisEntrega = $("#DescricaoInformacoesAdicionaisEntrega").val();
    ModelItem.CodigoUGObra = $("#CodigoUGObra").val();
    ModelItem.TipoObraId = $("#TipoObraId").val();
    ModelItem.NumeroAnoContrato = $("#NumeroAnoContrato").val();
    ModelItem.NumeroMesContrato = $("#NumeroMesContrato").val();
    ModelItem.CodigoAplicacaoObra = $("#Obra").val().replace(/[\.-]/g, "");

    //ModelItem.TransmitidoProdesp = $("#TransmitidoProdesp").val();
    //ModelItem.TransmitidoSiafem = $("#TransmitidoSiafem").val();
    //ModelItem.TransmitidoSiafisico = $("#TransmitidoSiafisico").val();

    ModelItem.TransmitirProdesp = $("#transmitirProdesp").is(":checked");
    ModelItem.TransmitirSiafem = $("#transmitirSIAFEM").is(":checked");
    ModelItem.TransmitirSiafisico = $("#transmitirSIAFISICO").is(":checked");

    //ModelItem.DataTransmitidoProdesp = $("").val();
    //ModelItem.DataTransmitidoSiafem = $("").val();
    //ModelItem.DataTransmitidoSiafisico = $("").val();
    //ModelItem.CadastroCompleto = $("").val();
    //ModelItem.MensagemServicoProdesp = $("").val();
    //ModelItem.MensagemServicoSiafem = $("").val();
    //ModelItem.MensagemServicoSiafisico = $("").val();

    ModelItem.DescricaoAutorizadoSupraFolha = $("#AutorizadoSupraFolha").val();
    ModelItem.CodigoEspecificacaoDespesa = $("#EspecDespesa").val();
    ModelItem.DescricaoEspecificacaoDespesa = Concatenar("DescEspecificacaoDespesa");
    ModelItem.CodigoAutorizadoAssinatura = $("#CodAssAutorizado").val();
    ModelItem.CodigoAutorizadoGrupo = $("#txtAutorizadoGrupo").val();
    ModelItem.CodigoAutorizadoOrgao = $("#txtAutorizadoOrgao").val();
    ModelItem.NomeAutorizadoAssinatura = $("#txtAutorizadoNome").val();
    ModelItem.DescricaoAutorizadoCargo = $("#txtAutorizadoCargo").val();
    ModelItem.CodigoExaminadoAssinatura = $("#CodAssExaminado").val();
    ModelItem.CodigoExaminadoGrupo = $("#txtExaminadoGrupo").val();
    ModelItem.CodigoExaminadoOrgao = $("#txtExaminadoOrgao").val();
    ModelItem.NomeExaminadoAssinatura = $("#txtExaminadoNome").val();
    ModelItem.DescricaoExaminadoCargo = $("#txtExaminadoCargo").val();
    ModelItem.CodigoResponsavelAssinatura = $("#CodAssResponsavel").val();
    ModelItem.CodigoResponsavelGrupo = $("#txtResponsavelGrupo").val();
    ModelItem.CodigoResponsavelOrgao = $("#txtResponsavelOrgao").val();
    ModelItem.NomeResponsavelAssinatura = $("#txtResponsavelNome").val();
    ModelItem.DescricaoResponsavelCargo = $("#txtResponsavelCargo").val();



    //DataCadastramento
    //TransmitidoProdesp
    //TransmitidoSiagem
    //TransmitidoSiafisico
    ModelItem.DataCadastramento = $("#DataCadastramento").val();
    ModelItem.DataTransmitidoProdesp = $("#DataTransmitidoProdesp").val();
    ModelItem.DataTransmitidoSiafem = $("#DataTransmitidoSiafem").val();
    ModelItem.DataTransmitidoSiafisico = $("#DataTransmitidoSiafisico").val();
}

//function SomaTotalGeral() {
//    var total = 0;
//    $('#tblPesquisaItem tbody tr').each(function (index, value) {
//        var precoTotal = $(this).find('td')[4];
//        if (precoTotal != undefined) {
//            var innerValue = precoTotal.innerText;
//            total += parseInt(innerValue.replace(/[\.,R$ ]/g, ""));
//        }
//    });
//    $('#ValorGeral').val(total);
//    $('#ValorGeral').maskMoney('mask');
//}

function removeRow(nRow) {
    var row = $(nRow).parent();

    var aData = $('#tblPesquisaItem').dataTable().fnGetData(row);
    $("#CodigoItemServico").val(aData[0]);

    $.each(ModelItemList, function (index, value) {
        if (ModelItemList[index].CodigoItemServico == aData[0]) {
            ModelItemList.splice(index, 1);
        }
    });

    $('#tblPesquisaItem').dataTable().fnDeleteRow(row);
    SomaTotalGeral();
}

var idItem;
var statusIstem;

function editRow(nRow) {
    $('#divNew').hide();
    $('#divSave').show();
    idItem = "";
    statusIstem = "";
    selectedRow = $(nRow).parent();

    var aData = $('#tblPesquisaItem').dataTable().fnGetData(selectedRow);
    idItem = aData[0];
    $("#CodigoItemServico").val(aData[0]);
    $("#CodigoUnidadeFornecimentoItem").val(aData[1]);
    $("#QuantidadeMaterialServico").val(aData[2]);
    $("#ValorUnitario").val(aData[3]);
    $("#ValorTotal").val(aData[4]);
    CarregarTextArea('DescricaoJustificativaPreco', 2, aData[5]);
    SomaTotalGeral();
}

function saveRow() {

    var qtdeMaterial = convertDecimal($("#QuantidadeMaterialServico").val());

    $('#divNew').show();
    $('#divSave').hide();

    $('#tblPesquisaItem').dataTable().fnUpdate($("#CodigoItemServico").val(), selectedRow, 0, false);
    $('#tblPesquisaItem').dataTable().fnUpdate($("#CodigoUnidadeFornecimentoItem").val(), selectedRow, 1, false);
    $('#tblPesquisaItem').dataTable().fnUpdate(MaskQuantidade(qtdeMaterial), selectedRow, 2, false);
    $('#tblPesquisaItem').dataTable().fnUpdate($("#ValorUnitario").val(), selectedRow, 3, false);
    $('#tblPesquisaItem').dataTable().fnUpdate($("#ValorTotal").val(), selectedRow, 4, false);
    $('#tblPesquisaItem').dataTable().fnUpdate(Concatenar('DescricaoJustificativaPreco'), selectedRow, 5, false);
    $('#tblPesquisaItem').dataTable().fnDraw();

    $.each(ModelItemList, function (index, value) {
        if (ModelItemList[index].CodigoItemServico == idItem) {
            ModelItemList[index].CodigoItemServico = $("#CodigoItemServico").val();
            ModelItemList[index].CodigoUnidadeFornecimentoItem = $("#CodigoUnidadeFornecimentoItem").val();
            ModelItemList[index].QuantidadeMaterialServico = qtdeMaterial;
            ModelItemList[index].ValorUnitario = $("#ValorUnitario").valDecimalLimpo();
            ModelItemList[index].ValorTotal = $("#ValorTotal").valDecimalLimpo();
            ModelItemList[index].DescricaoJustificativaPreco = Concatenar('DescricaoJustificativaPreco');
        }
    });

    cleanInput();
    selectedRow = "";
    SomaTotalGeral();
}

function addRow() {
    if ($('#CodigoItemServico').val().length <= 0) {
        AbrirModal("Favor preencher cóidigo do Item");
        return false;
    }
    if ($('#CodigoUnidadeFornecimentoItem').val().length <= 0) {
        AbrirModal("Favor preencher Unidade Fornecimento");
        return false;
    }
    if ($('#QuantidadeMaterialServico').val().length <= 0) {
        AbrirModal("Favor preencher Qtd. Material ou Serviço");
        return false;
    }
    if ($('#ValorUnitario').val().replace(/[\.,R$ ]/g, "") <= 0) {
        AbrirModal("Favor preencher Preço Unitário");
        return false;
    }
    var justificativa = Concatenar('DescricaoJustificativaPreco');

    if (justificativa.length <= 0) {
        AbrirModal("Favor preencher Justificativa");
        return false;
    }

    var qtdeMaterial = convertDecimal($('#QuantidadeMaterialServico').val());

    var item = [
        $('#CodigoItemServico').val(),
        $('#CodigoUnidadeFornecimentoItem').val(),
        qtdeMaterial,
        $('#ValorUnitario').val(),
        $('#ValorTotal').val(),
        justificativa,
        '<button type="button" title="Alterar" class="btn btn-xs btn-info margL7" onclick="editRow(this)"><i class="fa fa-edit"></i></button>' +
        '<button type="button" title="Excluir" class="btn btn-xs btn-danger margL7" onclick="removeRow(this)"><i class="fa fa-trash"></i></button>'
    ];

    var newitem = {
        Id: 0,
        EmpenhoId: ModelItem.Id,
        CodigoItemServico: $('#CodigoItemServico').val(),
        CodigoUnidadeFornecimentoItem: $('#CodigoUnidadeFornecimentoItem').val(),
        QuantidadeMaterialServico: $('#QuantidadeMaterialServico').val(),
        ValorUnitario: $("#ValorUnitario").valDecimalLimpo(),
        ValorTotal: $('#ValorTotal').valDecimalLimpo(),
        DescricaoJustificativaPreco: Concatenar('DescricaoJustificativaPreco'),
        DescricaoUnidadeMedida: null,
        DescricaoItemSiafem: null,
        StatusSiafisicoItem: StatusSiafisicoItemGrid !== "S" ? "N" : "S",
        SequenciaItem: ModelItemList.length + 1
    };

    var existingItem = $.grep(ModelItemList, function (e) { return e.CodigoItemServico == $('#CodigoItemServico').val(); });

    if (existingItem.length > 0) {
        AbrirModal("Não é permitido incluir itens com o mesmo código.");
        return false;
    }

    newitem.QuantidadeMaterialServico = qtdeMaterial;

    ModelItemList[ModelItemList.length] = newitem;

    CreateInstanceModelItem();

    $('#tblPesquisaItem').dataTable().fnAddData(item);
    cleanInput();
    selectedRow = "";
    SomaTotalGeral();
}

function cleanInput() {
    $('#CodigoItemServico').val('');
    $('#CodigoUnidadeFornecimentoItem').val('');
    $('#QuantidadeMaterialServico').val('');
    $('#ValorUnitario').val('');
    $('#ValorTotal').val('');
    CarregarTextArea('DescricaoJustificativaPreco', ';;');
}

function CarregarTextArea(id, numberOfRows, value) {
    if ($("#" + id + "").length > 0) {
        var inputs = $("#" + id + " > .Proximo");

        if (value != null && value.length > 0) {
            var textos = value.split(";");
            for (var index = 0; index < numberOfRows; index++)
                if (textos.length > index)
                    inputs[index].value = textos[index].trim();
        }
    }
}



function CarregarGridItens(itens) {
    ModelItemList = []
    var itensList;
    var cont = 0;
    itens.forEach(function (e) {

        itens[cont].QuantidadeMaterialServico = MaskQuantidadeCorretaDoBackEnd(itens[cont].QuantidadeMaterialServico);
        itens[cont].ValorUnitario = MaskMonetarioCorretoDoBackEnd(itens[cont].ValorUnitario);
        itens[cont].ValorTotal = MaskMonetarioCorretoDoBackEnd(itens[cont].ValorTotal);

        itensList = [
          itens[cont].CodigoItemServico,
            itens[cont].CodigoUnidadeFornecimentoItem,
            itens[cont].QuantidadeMaterialServico,
            "R$ " + itens[cont].ValorUnitario,
            "R$ " + itens[cont].ValorTotal,
            itens[cont].DescricaoJustificativaPreco,
            '<button type="button" title="Alterar" class="btn btn-xs btn-info margL7" onclick="editRow(this)"><i class="fa fa-edit"></i></button>' +
            '<button type="button" title="Excluir" class="btn btn-xs btn-danger margL7" onclick="removeRow(this)"><i class="fa fa-trash"></i></button>'
        ]

        $('#tblPesquisaItem').dataTable().fnAddData(itensList);

        itens[cont].StatusSiafisicoItem = "S", //aqui na consulta chamará sempre o webservice de alteração
        StatusSiafisicoItemGrid = "S",
        ModelItemList[ModelItemList.length] = itens[cont];
        //itens[cont].QuantidadeMaterialServico = MaskQuantidade(itens[cont].QuantidadeMaterialServico);

        cont = cont + 1;
    });

    cleanInput();
    selectedRow = "";
    SomaTotalGeral();
}
