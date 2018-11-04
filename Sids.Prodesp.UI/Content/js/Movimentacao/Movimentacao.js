var isLoad;
var tans = "";

var TipoMovimentacao = {
    TransferenciaComNcDistribuicao: 1,
    EstornoComNcCancelamento: 2,
    RedistribuicaoSemNc: 3
};

var TipoDocumento = {
    CancelamentoReducao: 1,
    DistribuicaoSuplementacao: 2,
    NotaDeCredito: 3
};

(function (window, document, $) {
    'use strict';

    window.Movimentacao = {};
    Movimentacao.selectedRow = "";
    Movimentacao.selectedRowNc = "";

    Movimentacao.init = function () {
        isLoad = true;
        $(".real").maskMoney({ prefix: "R$ ", allowNegative: false, thousands: ".", decimal: ",", affixesStay: true, allowZero: true });
        $('input[name="TotalQ1"]', '#frmPainelCadastrar').val('R$ 0,00');
        $('input[name="TotalQ2"]', '#frmPainelCadastrar').val('R$ 0,00');
        $('input[name="TotalQ3"]', '#frmPainelCadastrar').val('R$ 0,00');
        $('input[name="TotalQ4"]', '#frmPainelCadastrar').val('R$ 0,00');
        $('input[name="Total"]', '#frmPainelCadastrar').val('R$ 0,00');
        $(".real").maskMoney('mask');
        
        Movimentacao.GerarUGO();

        Movimentacao.cacheSelectors();
        Movimentacao.BindEventos();
        Movimentacao.Redefinir();
        Movimentacao.selecionarCenario();

        IniciarCreateEdit(Movimentacao.controller);

        setAfterSaveButtons();

        $('#EventoNC').val("300061")

        $("#btnSalvar").click(function () {
            tans = "S";
            criarEntidadeMemoria();
            
            var valido = validarSalvar();
            if (valido) {
                $("#frmPainelCadastrar").data("bootstrapValidator").resetForm();
                $("#frmPainelCadastrar").submit();
            }
        });

        $("#btnTransmitir").click(function () {
            tans = "T";
            criarEntidadeMemoria();

            var valido = validarTransmitir();
            if (valido) {
                $("#frmPainelCadastrar").data("bootstrapValidator").resetForm();
                $("#frmPainelCadastrar").submit();
            }
        });

        $("#new").click(function (e) {
            e.preventDefault();
            tans = "A";
            var validator = $("#frmPainelCadastrar").data("bootstrapValidator")
            validator.resetForm();
            $("#frmPainelCadastrar").submit();
            if (validator.isValid()) {
                addRow();
            }
        });

        if (!ModelItem.Meses) {
            ModelItem.Meses = [];
        }

        // Caso não seja o botão visualizar do filtro grid
        if (window.action.toLowerCase() === "create") {
            Movimentacao.selecionarPrograma();
        }


        if (ModelItem.TransmitidoSiafem == true)
            $(".lockSiafem").attr("disabled", "disabled");

        if (ModelItem.TransmitidoProdesp == true)
            $(".lockProdesp").attr("disabled", "disabled");
    }

    Movimentacao.cacheSelectors = function () {
        Movimentacao.body = $('body');
        Movimentacao.controller = window.controller;

        Movimentacao.valueSelecaoProdesp = $('#TransmitirProdesp');
        Movimentacao.valueSelecaoSiafem = $('#TransmitirSiafem');

        Movimentacao.tipoMovimentacao = $('#IdTipoMovimentacao');
        Movimentacao.tipoDocumento = $('#IdTipoDocumento');

        var ano = new Date().getFullYear();
        var anos = [];
        for (var i = 0; i < window.programasInfo.length; i++) {
            if (window.programasInfo[i].Ano.toString() === ano.toString()) {
                anos.push(window.programasInfo[i]);
            }
        }

        Movimentacao.ProgramaList = anos;
        Movimentacao.EstruturaList = window.estruturaInfo;

        Movimentacao.Estrutura = $('#Natureza');
        Movimentacao.Programa = $('#Programa');
        Movimentacao.NrOrgao = $("#NrOrgao");
        Movimentacao.Total = $("#Total");
        Movimentacao.CategoriaGasto = $("#CategoriaGasto");

        Movimentacao.partialDadoCancelamentoReducaoGrid = $('#DadoCancelamentoReducaoGrid');
        Movimentacao.TabelaCancelamentoReducao = $('#tblPesquisaCancelamentoReducao');

        Movimentacao.partialDadoDistribuicaoSuplementacaoGrid = $('#DadoDistribuicaoSuplementacaoGrid');
        Movimentacao.TabelaDistribuicaoSuplementacao = $('#tblPesquisaDistribuicaoSuplementacao');

        Movimentacao.partialDadoNotaCreditoGrid = $('#DadoNotaCreditoGrid');
        Movimentacao.TabelaNotaDeCredito = $('#tblPesquisaNotaCredito');

        Movimentacao.TipoDocumento1 = $("#IdTipoDocumento option[value='1']");
        Movimentacao.TipoDocumento2 = $("#IdTipoDocumento option[value='2']");

        Movimentacao.partialAssinaturas = $('#DadoAssinatura');

        Movimentacao.EspecificacaoDespesaCodigo = $("#EspecDespesa");
    }

    Movimentacao.selecionarTipoMainframe = function (e) {
        e.preventDefault();
        Movimentacao.selecionarCenario();
    }

    Movimentacao.selecionarTipoMovimentacao = function (e) {
        var transmitirSiafem = Movimentacao.valueSelecaoSiafem.is(':checked');
        var transmitirProdesp = Movimentacao.valueSelecaoProdesp.is(':checked');
        var tipoMovimentacao = parseInt(Movimentacao.tipoMovimentacao.val());

        if (!isNaN(tipoMovimentacao)) {
            ModelItem.IdTipoMovimentacao = $("#IdTipoMovimentacao").val();

            if (transmitirProdesp === true && transmitirSiafem === true) {
                Movimentacao.TipoDocumento1.text("Cancelamento/Redução");
                Movimentacao.TipoDocumento2.text("Distribuição/Suplementação");

                if (tipoMovimentacao === TipoMovimentacao.RedistribuicaoSemNc) {
                    Movimentacao.partialDadoNotaCreditoGrid.hide();
                    $('#divObservacaoNC').hide();
                }
                else {
                    Movimentacao.partialDadoNotaCreditoGrid.show();
                    $('#divObservacaoNC').show();
                }
            }
            else if (transmitirProdesp === true && transmitirSiafem === false) {
                Movimentacao.TipoDocumento1.text("Redução");
                Movimentacao.TipoDocumento2.text("Suplementação");
            }
            else if (transmitirProdesp === false && transmitirSiafem === true) {
                Movimentacao.TipoDocumento1.text("Cancelamento");
                Movimentacao.TipoDocumento2.text("Distribuição");

                if (tipoMovimentacao === TipoMovimentacao.RedistribuicaoSemNc) {
                    $('#divPrograma').hide();
                    $('#divNatureza').hide();
                    $('#Fonte').removeAttr("readOnly");
                    Movimentacao.partialDadoNotaCreditoGrid.hide();
                    $('#divObservacaoNC').hide();
                }
                else {
                    $('#divPrograma').show();
                    $('#divNatureza').show();
                    $('#Fonte').attr("readOnly", true);
                    Movimentacao.partialDadoNotaCreditoGrid.show();
                    $('#divObservacaoNC').show();
                }
            }
        }
    }

    Movimentacao.selecionarPrograma = function (e) {
        if (Movimentacao.Programa.val() !== "") {
            GerarComboNatureza();
        }
    }

    Movimentacao.selecionarNatureza = function (e) {
        if (Movimentacao.Estrutura.val() !== "") {
            var naturezaSelecionada = $('#Natureza option:selected').text();

            $('#Fonte').val('0' + naturezaSelecionada.substring(12, 14));
            var categoria = naturezaSelecionada.substring(2, 3);
            $('#CategoriaGasto').val(categoria);
            $('#NatDespesa').val(naturezaSelecionada.substring(0, 9).replace(/[\.-]/g, ''));

            $('#OrigemRecurso').val(naturezaSelecionada.substring(12, 14));

            var valorMacro = ConsultarPorEstruturaMacroInfo();

            $('#NrObra').val('00' + valorMacro);

            var textoNatureza = naturezaSelecionada.split("-")[2].trim();
            $("#ObservacaoNC").val("Para atender despesa com " + textoNatureza);
        }
        else {
            $("#ObservacaoNC").val("Para atender despesa com...");
        }
    }

    Movimentacao.selecionarTipoDocumento = function (e) {
        var valorSelecionado = parseInt(Movimentacao.tipoDocumento.val());

        if (!isNaN(valorSelecionado)) {
            if (valorSelecionado === TipoDocumento.CancelamentoReducao) {
                if ($('#Fonte').val() == "001") {
                    $('#CancDist').val("546812");
                }
                else {
                    $('#CancDist').val("546813");
                }

                $("#ObservacaoCancelamento").val("Cancelamento para atender regionais...");
                Movimentacao.EspecificacaoDespesaCodigo.val("011");
            }
            else if (valorSelecionado === TipoDocumento.DistribuicaoSuplementacao) {
                if ($('#Fonte').val() == "001") {
                    $('#CancDist').val("541812");
                }
                else {
                    $('#CancDist').val("541813");
                }

                $("#ObservacaoCancelamento").val("Distribuição conforme Nota de Crédito...");
                Movimentacao.EspecificacaoDespesaCodigo.val("012");
            }

            var codigoEspecDespesa = Movimentacao.EspecificacaoDespesaCodigo.val();

            if (codigoEspecDespesa !== undefined && codigoEspecDespesa !== null && codigoEspecDespesa.length === 3) {
                Movimentacao.EspecificacaoDespesaCodigo.trigger('change');
            }
        }
    }


    Movimentacao.selecionarCenario = function () {
        var transmitirSiafem = Movimentacao.valueSelecaoSiafem.is(':checked');
        var transmitirProdesp = Movimentacao.valueSelecaoProdesp.is(':checked');

        var tipoMovimentacao1 = $("#IdTipoMovimentacao option[value='1']"); // Transferência
        var tipoMovimentacao2 = $("#IdTipoMovimentacao option[value='2']"); // Estorno
        var tipoMovimentacao3 = $("#IdTipoMovimentacao option[value='3']"); // Redistribuição
        var tipoMovimentacao4 = $("#IdTipoMovimentacao option[value='']"); // Selecione

        Movimentacao.mostrarTodosOsCampos();

        if (transmitirProdesp === true && transmitirSiafem === true) {
            $("#IdTipoMovimentacao option").each(function () {
                $(this).removeAttr("selected");
            });

            tipoMovimentacao1.attr("selected", true);
            tipoMovimentacao2.show();
            tipoMovimentacao3.show();
            tipoMovimentacao4 .show();

            Movimentacao.tipoMovimentacao.val(TipoMovimentacao.TransferenciaComNcDistribuicao);

            Movimentacao.partialDadoCancelamentoReducaoGrid.find('.tituloItensPagina').text("Cancelamento/Redução");
            Movimentacao.partialDadoDistribuicaoSuplementacaoGrid.find('.tituloItensPagina').text("Distribuição/Suplementação");

            Movimentacao.partialDadoCancelamentoReducaoGrid.show();
            Movimentacao.partialDadoNotaCreditoGrid.show();
            Movimentacao.partialDadoDistribuicaoSuplementacaoGrid.show();


            $('#divCancDist').show();
            // trocar titulos das tabelas
        }
        else if (transmitirProdesp === false && transmitirSiafem === false) {
            $("#IdTipoMovimentacao option").each(function () {
                //$(this).removeAttr("selected");
                tipoMovimentacao1.hide();
                tipoMovimentacao2.hide();
                tipoMovimentacao3.hide();
                tipoMovimentacao4.attr("selected", true);
            });

            Movimentacao.tipoMovimentacao.val('');

            Movimentacao.partialDadoCancelamentoReducaoGrid.hide();
            Movimentacao.partialDadoNotaCreditoGrid.hide();
            Movimentacao.partialDadoDistribuicaoSuplementacaoGrid.hide();
        }
        else if (transmitirProdesp === true && transmitirSiafem === false) {
            if (Movimentacao.tipoMovimentacao.val().toString() !== TipoMovimentacao.EstornoComNcCancelamento.toString()) {
                tipoMovimentacao1.show();
                $("#IdTipoMovimentacao").val(TipoMovimentacao.TransferenciaComNcDistribuicao);
                tipoMovimentacao2.hide();
                tipoMovimentacao3.hide();
            }

            $('#divUnidadeGestoraEmitente').hide();
            $('#divGestaoEmitente').hide();
            $('#divUnidadeGestoraFavorecida').hide();
            $('#divGestaoFavorecida').hide();

            $('#divCancDist').hide();
            $('#divFonte').hide();
            $('#divCategoriaGasto').hide();

            $('#divFonte').hide();
            $('#divCategoriaGasto').hide();

            $('#divEventoNC').hide();
            $('#divUo').hide();
            $('#divFonteRecurso').hide();
            $('#divNatDespesa').hide();
            $('#divUGO').hide();
            $('#divplano_interno').hide();

            $('#divObservacaoCancelamento').hide();
            $('#divObservacaoNC').hide();

            Movimentacao.partialDadoCancelamentoReducaoGrid.find('.tituloItensPagina').text("Redução");
            Movimentacao.partialDadoDistribuicaoSuplementacaoGrid.find('.tituloItensPagina').text("Suplementação");

            Movimentacao.partialDadoCancelamentoReducaoGrid.show();
            Movimentacao.partialDadoNotaCreditoGrid.hide();
            Movimentacao.partialDadoDistribuicaoSuplementacaoGrid.show();

            // trocar titulos das tabelas
        }
        else if (transmitirProdesp === false && transmitirSiafem === true) {
            if (window.action.toLowerCase() !== "edit") {
                tipoMovimentacao1.show();
                tipoMovimentacao2.show();
                $("#IdTipoMovimentacao").val(TipoMovimentacao.EstornoComNcCancelamento);
                tipoMovimentacao3.show();
            }

            $('#divAnoExercicio').hide();
            $('#divNrProcesso').hide();
            $('#divOrigemRecurso').hide();
            $('#divDestinoRecurso').hide();
            $('#divFlProc').hide();
            $('#divNrOrgao').hide();
            $('#divNrObra').hide();
            $('#divEspecDespesa').hide();
            $('#divDescEspecificacaoDespesa').hide();
            $('#divObservacaoNC').show();

            Movimentacao.partialDadoCancelamentoReducaoGrid.find('.tituloItensPagina').text("Cancelamento");
            Movimentacao.partialDadoDistribuicaoSuplementacaoGrid.find('.tituloItensPagina').text("Distribuição");

            Movimentacao.partialDadoCancelamentoReducaoGrid.show();
            Movimentacao.partialDadoNotaCreditoGrid.show();
            Movimentacao.partialDadoDistribuicaoSuplementacaoGrid.show();

            // trocar titulos das tabelas
        }

        if (transmitirProdesp === true) {
            Movimentacao.partialAssinaturas.show();
        }
        else {
            Movimentacao.partialAssinaturas.hide();
        }

        // Tratamento para exibir o tipo de movimento estorno ao clicar no botão visualizar do filtro grid
        // O método abaixo "Movimentacao.selecionarTipoMovimentacao()" está alterando o ModelItem.IdTipoMovimentacao para 1
        if (action.toLowerCase() === "estornar" || ModelItem.IdTipoMovimentacao === 2) {
            tipoMovimentacao1.hide();
            tipoMovimentacao2.show();
            tipoMovimentacao3.hide();
            tipoMovimentacao4.hide();
            $("#IdTipoMovimentacao").val(TipoMovimentacao.EstornoComNcCancelamento);
        }

        // Marcar tipo de movimentação Redistribuição
        if (ModelItem.IdTipoMovimentacao === 3) {
            tipoMovimentacao1.show();
            tipoMovimentacao2.show();
            tipoMovimentacao3.show();
            tipoMovimentacao4.show();
            $("#IdTipoMovimentacao").val(TipoMovimentacao.RedistribuicaoSemNc);
        }

        Movimentacao.selecionarTipoMovimentacao();
    }

    Movimentacao.mostrarTodosOsCampos = function () {
        $('#divUnidadeGestoraEmitente').show();
        $('#divGestaoEmitente').show();
        $('#divUnidadeGestoraFavorecida').show();
        $('#divGestaoFavorecida').show();
        $('#divCancDist').show();
        $('#divFonte').show();
        $('#divCategoriaGasto').show();
        $('#divFonte').show();
        $('#divCategoriaGasto').show();
        $('#divEventoNC').show();
        $('#divUo').show();
        $('#divFonteRecurso').show();
        $('#divNatDespesa').show();
        $('#divUGO').show();
        $('#divplano_interno').show();
        $('#divObservacaoCancelamento').show();
        $('#divAnoExercicio').show();
        $('#divNrProcesso').show();
        $('#divOrigemRecurso').show();
        $('#divDestinoRecurso').show();
        $('#divFlProc').show();
        $('#divNrOrgao').show();
        $('#divNrObra').show();
        $('#divEspecDespesa').show();
        $('#divDescEspecificacaoDespesa').show();
        $('#divObservacaoNC').show();
    }

    Movimentacao.obterEstruturaProgramaInfo = function (programa) {
        var prog = "";

        $.each(Movimentacao.ProgramaList, function (index, value) {
            if (Movimentacao.ProgramaList[index].Codigo == programa) {
                prog = Movimentacao.ProgramaList[index].Cfp;
            }
        });

        return prog;
    }

    Movimentacao.obterEstruturaNaturezaInfo = function (natureza, programa) {
        var nat = "";

        $.each(Movimentacao.EstruturaList, function (index, value) {

            if (Movimentacao.EstruturaList[index].Programa == programa && Movimentacao.EstruturaList[index].Codigo == natureza) {
                nat = Movimentacao.EstruturaList[index].Natureza;
            }

        });

        return nat;
    }

    Movimentacao.GerarUGO = function () {
        regionais.forEach(function (regional) {
            if (regional.Id == usuario.RegionalId) {
                $("#UnidadeGestoraEmitente").val(regional.Uge);
            }
        });

        $("#IdGestaoEmitente").val("16055");
    }

    Movimentacao.SomarTotalTrimestre = function () {
        var total = 0;
        var total2 = 0;
        var total3 = 0;
        var total4 = 0;

        var valor1 = $('input[name="Mes01"]').val().replace(".", "").replace(".", "").replace(".", "").replace(".", "").replace(".", "");
        valor1 = valor1.replace(",", ".").replace("R$ ", "");
        var valor2 = $('input[name="Mes02"]').val().replace(".", "").replace(".", "").replace(".", "").replace(".", "").replace(".", "");
        valor2 = valor2.replace(",", ".").replace("R$ ", "");
        var valor3 = $('input[name="Mes03"]').val().replace(".", "").replace(".", "").replace(".", "").replace(".", "").replace(".", "");
        valor3 = valor3.replace(",", ".").replace("R$ ", "");

        var valor4 = $('input[name="Mes04"]').val().replace(".", "").replace(".", "").replace(".", "").replace(".", "").replace(".", "");
        valor4 = valor4.replace(",", ".").replace("R$ ", "");
        var valor5 = $('input[name="Mes05"]').val().replace(".", "").replace(".", "").replace(".", "").replace(".", "").replace(".", "");
        valor5 = valor5.replace(",", ".").replace("R$ ", "");
        var valor6 = $('input[name="Mes06"]').val().replace(".", "").replace(".", "").replace(".", "").replace(".", "").replace(".", "");
        valor6 = valor6.replace(",", ".").replace("R$ ", "");

        var valor7 = $('input[name="Mes07"]').val().replace(".", "").replace(".", "").replace(".", "").replace(".", "").replace(".", "");
        valor7 = valor7.replace(",", ".").replace("R$ ", "");
        var valor8 = $('input[name="Mes08"]').val().replace(".", "").replace(".", "").replace(".", "").replace(".", "").replace(".", "");
        valor8 = valor8.replace(",", ".").replace("R$ ", "");
        var valor9 = $('input[name="Mes09"]').val().replace(".", "").replace(".", "").replace(".", "").replace(".", "").replace(".", "");
        valor9 = valor9.replace(",", ".").replace("R$ ", "");

        var valor10 = $('input[name="Mes10"]').val().replace(".", "").replace(".", "").replace(".", "").replace(".", "").replace(".", "");
        valor10 = valor10.replace(",", ".").replace("R$ ", "");
        var valor11 = $('input[name="Mes11"]').val().replace(".", "").replace(".", "").replace(".", "").replace(".", "").replace(".", "");
        valor11 = valor11.replace(",", ".").replace("R$ ", "");
        var valor12 = $('input[name="Mes12"]').val().replace(".", "").replace(".", "").replace(".", "").replace(".", "").replace(".", "");
        valor12 = valor12.replace(",", ".").replace("R$ ", "");


        if (valor1 == "")
            valor1 = "0.00";
        if (valor2 == "")
            valor2 = "0.00";
        if (valor3 == "")
            valor3 = "0.00";
        if (valor4 == "")
            valor4 = "0.00";
        if (valor5 == "")
            valor5 = "0.00";
        if (valor6 == "")
            valor6 = "0.00";
        if (valor7 == "")
            valor7 = "0.00";
        if (valor8 == "")
            valor8 = "0.00";
        if (valor9 == "")
            valor9 = "0.00";
        if (valor10 == "")
            valor10 = "0.00";
        if (valor11 == "")
            valor11 = "0.00";
        if (valor12 == "")
            valor12 = "0.00";



        total += parseFloat((valor1));
        total += parseFloat((valor2));
        total += parseFloat((valor3));

        total = parseFloat(total.toFixed(2));
        var valor = String(total).replace(".", ",");
        valor = valor.indexOf(',') == -1 ? valor + ",00" : valor;
        valor = valor.length - valor.indexOf(',') < 3 ? valor + "0" : valor;
        $("#TotalQ1").val("R$ " + MaskMonetario(valor));
        $("#TotalQ1").trigger("input");

        total2 += parseFloat((valor4));
        total2 += parseFloat((valor5));
        total2 += parseFloat((valor6));

        total2 = parseFloat(total2.toFixed(2));
        var valor2 = String(total2).replace(".", ",");
        valor2 = valor2.indexOf(',') == -1 ? valor2 + ",00" : valor2;
        valor2 = valor2.length - valor2.indexOf(',') < 3 ? valor2 + "0" : valor2;
        $("#TotalQ2").val("R$ " + MaskMonetario(valor2));
        $("#TotalQ2").trigger("input");

        total3 += parseFloat((valor7));
        total3 += parseFloat((valor8));
        total3 += parseFloat((valor9));

        total3 = parseFloat(total3.toFixed(2));
        var valor3 = String(total3).replace(".", ",");
        valor3 = valor3.indexOf(',') == -1 ? valor3 + ",00" : valor3;
        valor3 = valor3.length - valor3.indexOf(',') < 3 ? valor3 + "0" : valor3;
        $("#TotalQ3").val("R$ " + MaskMonetario(valor3));
        $("#TotalQ3").trigger("input");

        total4 += parseFloat((valor10));
        total4 += parseFloat((valor11));
        total4 += parseFloat((valor12));

        total4 = parseFloat(total4.toFixed(2));
        var valor4 = String(total4).replace(".", ",");
        valor4 = valor4.indexOf(',') == -1 ? valor4 + ",00" : valor4;
        valor4 = valor4.length - valor4.indexOf(',') < 3 ? valor4 + "0" : valor4;
        $("#TotalQ4").val("R$ " + MaskMonetario(valor4));
        $("#TotalQ4").trigger("input");
    }

    Movimentacao.Redefinir = function () {
        Movimentacao.partialDadoCancelamentoReducaoGrid.hide();
        Movimentacao.partialDadoNotaCreditoGrid.hide();
        Movimentacao.partialDadoDistribuicaoSuplementacaoGrid.hide();
    }

    Movimentacao.BindEventos = function () {
        Movimentacao.body
            .on('change', '#TransmitirSiafem', Movimentacao.selecionarTipoMainframe)
            .on('change', '#TransmitirProdesp', Movimentacao.selecionarTipoMainframe)
            .on('change', '#IdTipoMovimentacao', Movimentacao.selecionarTipoMovimentacao)
            .on('change', '#Programa', Movimentacao.selecionarPrograma)
            .on('change', '#Natureza', Movimentacao.selecionarNatureza)
            .on('change', '#IdTipoDocumento', Movimentacao.selecionarTipoDocumento)
            .on('click', '#viewRowCR', Movimentacao.viewRowCR)
            .on('click', '#editRowCR', Movimentacao.editRowCR)
            .on('click', '#removeRowCR', Movimentacao.removeRowCR)
            .on('click', '#viewRowDS', Movimentacao.viewRowDS)
            .on('click', '#editRowDS', Movimentacao.editRowDS)
            .on('click', '#removeRowDS', Movimentacao.removeRowDS)
            .on('click', '#viewRowNC', Movimentacao.viewRowNC)
            .on('click', '#saveCancelamentoReducao', Movimentacao.saveRowCR)
            .on('click', '#saveDistribuicaoSuplementacao', Movimentacao.saveRowDS)
    }


    // CancelamentoReducao Inicio
    Movimentacao.viewRowCR = function (nRow) {
        var transmitirProdesp = Movimentacao.valueSelecaoProdesp.is(':checked');
        var transmitirSiafem = Movimentacao.valueSelecaoSiafem.is(':checked');
        var tab = null;

        Movimentacao.selectedRow = $(nRow).parent();

        var aData = Movimentacao.TabelaCancelamentoReducao.dataTable().fnGetData(Movimentacao.selectedRow);

        var seq = aData[0];

        Movimentacao.SomarTotalTrimestre();

        $('input#txtIdGestaoEmitente', '#modalDadosMovimentacao').val(ModelItem.GestaoEmitente);
        $('input#txtUnidadeGestoraEmitente', '#modalDadosMovimentacao').val(ModelItem.UnidadeGestoraEmitente);
        $('input#txtProgramaId', '#modalDadosMovimentacao').val(ModelItem.CfpDesc);
        $('input#txtNaturezaId', '#modalDadosMovimentacao').val(ModelItem.CedDesc);

        if (transmitirSiafem === true) {
            tab = "C";
            var selectedCancelamento = selecionarCancelamento(seq);
            if (selectedCancelamento !== null) {
                var selectedNotaDeCredito = selecionarNotaDeCredito(seq, tab);
                if (selectedNotaDeCredito !== null) {
                    //$('#txtNaturezaId', '#modalDadosMovimentacao').val(selectedNotaDeCredito.Object.IdEstrutura);
                    //$('#txtProgramaId', '#modalDadosMovimentacao').val(selectedNotaDeCredito.Object.IdPrograma);
                    $('input#txtUo', '#modalDadosMovimentacao').val(selectedNotaDeCredito.Object.Uo);
                    $('input#txtUgo', '#modalDadosMovimentacao').val(selectedNotaDeCredito.Object.Ugo);
                    $('input#txtplano_interno', '#modalDadosMovimentacao').val(selectedNotaDeCredito.Object.PlanoInterno);

                    $('input#txtObservacaoNCModal', '#modalDadosMovimentacao').val(selectedNotaDeCredito.Object.Observacao);
                    $('input#txtObservacaoNC2Modal', '#modalDadosMovimentacao').val(selectedNotaDeCredito.Object.Observacao2);
                    $('input#txtObservacaoNC3Modal', '#modalDadosMovimentacao').val(selectedNotaDeCredito.Object.Observacao3);
                }

                $('#modalDadosMovimentacao input#txtCancDist').val(selectedCancelamento.Object.Evento);

                $('#modalDadosMovimentacao input#txtEventoNC').val(selectedCancelamento.Object.EventoNC);

                $('#modalDadosMovimentacao input#txtFonteRecurso').val("");
                $('#modalDadosMovimentacao input#txtNatDespesa').val(selectedCancelamento.Object.NatDespesa);

                $('#modalDadosMovimentacao input#txtplano_interno').val(selectedCancelamento.Object.PlanoInterno);

                $('#modalDadosMovimentacao input#txtFonte').val(selectedCancelamento.Object.IdFonte);

                $('#modalDadosMovimentacao input#txtObservacaoCancelamentoModal').val(selectedCancelamento.Object.Observacao);
                $('#modalDadosMovimentacao input#txtObservacaoCancelamento2Modal').val(selectedCancelamento.Object.Observacao2);
                $('#modalDadosMovimentacao input#txtObservacaoCancelamento2Modal').val(selectedCancelamento.Object.Observacao3);
            }
        }

        if (transmitirProdesp === true) {
            var selectedReducao = selecionarReducao(seq);
            if (selectedReducao !== null) {
                tab = "R";

                $('#modalDadosMovimentacao input#txtUnidadeGestoraFavorecida').val(selectedReducao.Object.UnidadeGestoraFavorecida);

                $('#modalDadosMovimentacao input#txtNrObra').val(selectedReducao.Object.NrObra);
                $('#modalDadosMovimentacao input#txtNrOrgao').val(selectedReducao.NrOrgao);
                $('#modalDadosMovimentacao input#txtFonteRecurso').val("");
                $('#modalDadosMovimentacao input#txtNatDespesa').val(selectedReducao.Object.NatDespesa);

                $('#modalDadosMovimentacao input#txtEspecDespesaModal').val(selectedReducao.Object.EspecDespesa);
                CarregarTextArea("DescEspecificacaoDespesaModal", 9, selectedReducao.Object.DescEspecDespesa);
            }
        }

        preencherMesesModal(ModelItem.Meses, seq, tab);

        $("#modalDadosMovimentacao").modal();
    }

    Movimentacao.editRowCR = function (nRow) {
        var transmitirProdesp = Movimentacao.valueSelecaoProdesp.is(':checked');
        var transmitirSiafem = Movimentacao.valueSelecaoSiafem.is(':checked');
        var tipoMovimentacao = parseInt(Movimentacao.tipoMovimentacao.val());
        var tab = null;

        Movimentacao.selectedRow = $(nRow).parent();

        $('#divNew').hide();
        $('#divSaveDistribuicaoSuplementacao').hide();
        $('#divSaveCancelamentoReducao').show();

        var aData = Movimentacao.TabelaCancelamentoReducao.dataTable().fnGetData(Movimentacao.selectedRow);

        var seq = parseInt(aData[0]);

        if (action.toLowerCase() === "estornar" || tipoMovimentacao === TipoMovimentacao.EstornoComNcCancelamento) {
            $(Movimentacao.TabelaNotaDeCredito).find('tr').each(function (index, element) {
                var tdSeq = $(this).find('td')[0];
                if (tdSeq !== undefined) {
                    var seqRowNc = parseInt($(tdSeq).text());

                    if (!isNaN(seqRowNc) && seqRowNc === seq) {
                        Movimentacao.selectedRowNc = $(tdSeq);
                    }
                }
            });
        }

        $('#IdTipoMovimentacao', '#DadoApropriacao').val(ModelItem.IdTipoMovimentacao);
        $('#AnoExercicio', '#DadoApropriacao').val(ModelItem.AnoExercicio);
        $('#UnidadeGestoraEmitente', '#DadoApropriacao').val(ModelItem.UnidadeGestoraEmitente);
        $('#IdGestaoEmitente', '#DadoApropriacao').val(ModelItem.GestaoEmitente);
        $('#Natureza', '#DadoApropriacao').val(ModelItem.IdEstrutura);
        $('#Programa', '#DadoApropriacao').val(ModelItem.IdPrograma);

        if (transmitirSiafem === true) {
            tab = "C";
            var selectedCancelamento = selecionarCancelamento(seq);
            if (selectedCancelamento !== null) {
                var selectedNotaDeCredito = selecionarNotaDeCredito(seq, tab);
                if (selectedNotaDeCredito !== null) {
                    $('#Natureza', '#DadoApropriacao').val(selectedNotaDeCredito.Object.IdEstrutura);
                    $('#Programa', '#DadoApropriacao').val(selectedNotaDeCredito.Object.IdPrograma);
                    
                    if (action.toLowerCase() === "estornar" || tipoMovimentacao === TipoMovimentacao.EstornoComNcCancelamento) {
                        $('#FonteRecurso', '#DadoApropriacao').val(selectedNotaDeCredito.Object.FonteRecurso);
                    }
                }

                $('#IdTipoDocumento', '#DadoApropriacao').val(selectedCancelamento.Object.IdTipoDocumento);

                $('#UnidadeGestoraFavorecida', '#DadoApropriacao').val(selectedCancelamento.Object.UnidadeGestoraFavorecida);
                $('#IdGestaoFavorecida', '#DadoApropriacao').val(selectedCancelamento.Object.GestaoFavorecida);

                $('#CancDist', '#DadoApropriacao').val(selectedCancelamento.Object.Evento);
                $('#CategoriaGasto', '#DadoApropriacao').val(selectedCancelamento.Object.CategoriaGasto);

                $('#EventoNC', '#DadoApropriacao').val(selectedCancelamento.Object.EventoNC);

                $('#ObservacaoCancelamento', '#divObservacaoCancelamento').val(selectedCancelamento.Object.Observacao);
                $('#ObservacaoCancelamento2', '#divObservacaoCancelamento').val(selectedCancelamento.Object.Observacao2);
                $('#ObservacaoCancelamento3', '#divObservacaoCancelamento').val(selectedCancelamento.Object.Observacao3);

                Movimentacao.Total.val("R$ " + MaskMonetario(selectedCancelamento.Object.Valor.toFixed(2)));
            }
        }
        if (transmitirProdesp === true) {
            var selectedReducao = selecionarReducao(seq);
            if (selectedReducao !== null) {
                tab = "R";
                //$('#IdTipoMovimentacao', '#DadoApropriacao').val(selectedReducao.Object.IdTipoMovimentacao);

                //$('#Natureza', '#DadoApropriacao').val(selectedReducao.Object.IdEstrutura);
                //$('#Programa', '#DadoApropriacao').val(selectedReducao.Object.IdPrograma);

                $('#NrProcesso', '#DadoApropriacao').val(selectedReducao.Object.NrProcesso);
                $('#OrigemRecurso', '#DadoApropriacao').val(selectedReducao.Object.OrigemRecurso);
                $('#DestinoRecurso', '#DadoApropriacao').val(selectedReducao.Object.DestinoRecurso);
                $('#FlProc', '#DadoApropriacao').val(selectedReducao.Object.FlProc);

                $('#IdTipoDocumento', '#DadoApropriacao').val(selectedReducao.Object.IdTipoDocumento);

                $('#NrOrgao', '#DadoApropriacao').val(selectedReducao.Object.NrOrgao);
                $('#NrObra', '#DadoApropriacao').val(selectedReducao.Object.NrObra);

                $('#EspecDespesa').val(selectedReducao.Object.EspecDespesa);
                CarregarTextArea("DescEspecificacaoDespesa", 9, selectedReducao.Object.DescEspecDespesa);

                Movimentacao.Total.val("R$ " + MaskMonetario(selectedReducao.Object.Valor.toFixed(2)));
            }
        }

        preencherMeses(ModelItem.Meses, seq, tab);
    }

    Movimentacao.saveRowCR = function (nRow) {
        var transmitirProdesp = Movimentacao.valueSelecaoProdesp.is(':checked');
        var transmitirSiafem = Movimentacao.valueSelecaoSiafem.is(':checked');
        var tipoMovimentacao = parseInt(Movimentacao.tipoMovimentacao.val());
        var tab = null;

        setAfterSaveButtons();

        var aData = Movimentacao.TabelaCancelamentoReducao.dataTable().fnGetData(Movimentacao.selectedRow);
        var seq = parseInt(aData[0]);

        Movimentacao.TabelaCancelamentoReducao.dataTable().fnUpdate($("#UnidadeGestoraEmitente").val(), Movimentacao.selectedRow, 3, false);
        Movimentacao.TabelaCancelamentoReducao.dataTable().fnUpdate($("#UnidadeGestoraFavorecida").val(), Movimentacao.selectedRow, 4, false);
        Movimentacao.TabelaCancelamentoReducao.dataTable().fnUpdate(Movimentacao.NrOrgao.val(), Movimentacao.selectedRow, 5, false);
        Movimentacao.TabelaCancelamentoReducao.dataTable().fnUpdate($('#Fonte').val(), Movimentacao.selectedRow, 6, false);
        Movimentacao.TabelaCancelamentoReducao.dataTable().fnUpdate(Movimentacao.CategoriaGasto.val(), Movimentacao.selectedRow, 7, false);
        Movimentacao.TabelaCancelamentoReducao.dataTable().fnUpdate(Movimentacao.Total.val(), Movimentacao.selectedRow, 8, false);
        Movimentacao.TabelaCancelamentoReducao.dataTable().fnDraw();

        ModelItem.IdPrograma = Movimentacao.Programa.val();
        ModelItem.IdEstrutura = Movimentacao.Estrutura.val();

        if (transmitirSiafem === true) {

            if (action.toLowerCase() === "estornar" || tipoMovimentacao === TipoMovimentacao.EstornoComNcCancelamento) {
                Movimentacao.TabelaNotaDeCredito.dataTable().fnUpdate($("#UnidadeGestoraFavorecida").val(), Movimentacao.selectedRowNc, 2, false);
                Movimentacao.TabelaNotaDeCredito.dataTable().fnUpdate($("#UnidadeGestoraEmitente").val(), Movimentacao.selectedRowNc, 3, false);
                Movimentacao.TabelaNotaDeCredito.dataTable().fnUpdate(Movimentacao.NrOrgao.val(), Movimentacao.selectedRowNc, 4, false);
                Movimentacao.TabelaNotaDeCredito.dataTable().fnUpdate($('#FonteRecurso').val(), Movimentacao.selectedRowNc, 5, false);
                Movimentacao.TabelaNotaDeCredito.dataTable().fnUpdate(Movimentacao.CategoriaGasto.val(), Movimentacao.selectedRowNc, 6, false);
                Movimentacao.TabelaNotaDeCredito.dataTable().fnUpdate(Movimentacao.Total.val(), Movimentacao.selectedRowNc, 7, false);
                Movimentacao.TabelaNotaDeCredito.dataTable().fnDraw();
            }

            var selectedNotaDeCredito = selecionarNotaDeCredito(seq, "C");

            if (selectedNotaDeCredito !== null) {
                tab = "N";

                selectedNotaDeCredito.Object.IdTipoMovimentacao = $("#IdTipoMovimentacao").val();

                selectedNotaDeCredito.Object.IdPrograma = Movimentacao.Programa.val();
                selectedNotaDeCredito.Object.IdEstrutura = Movimentacao.Estrutura.val();

                selectedNotaDeCredito.Object.IdTipoDocumento = $("#IdTipoDocumento").val();

                selectedNotaDeCredito.Object.UnidadeGestoraEmitente = $("#UnidadeGestoraFavorecida").val();
                selectedNotaDeCredito.Object.UnidadeGestoraFavorecida = $("#UnidadeGestoraEmitente").val();
                
                selectedNotaDeCredito.Object.GestaoFavorecida = $("#IdGestaoFavorecida").val();

                selectedNotaDeCredito.Object.EventoNC = $("#EventoNC").val();

                selectedNotaDeCredito.Object.Uo = $("#Uo").val();
                selectedNotaDeCredito.Object.Ugo = $("#UGO").val();
                selectedNotaDeCredito.Object.FonteRecurso = $("#FonteRecurso").val();

                selectedNotaDeCredito.Object.PlanoInterno = $("#PlanoInterno").val();

                selectedNotaDeCredito.Object.Valor = util.valorToDecimal(Movimentacao.Total.val());

                atualizarMeses(ModelItem.Meses, seq, tab);

                atualizarNotaDeCreditoEmMemoria(selectedNotaDeCredito);
            }

            var selectedCancelamento = selecionarCancelamento(seq);
            if (selectedCancelamento !== null) {
                tab = "C";
                selectedCancelamento.Object.UnidadeGestoraEmitente = $("#UnidadeGestoraEmitente").val();

                selectedCancelamento.Object.IdTipoMovimentacao = $("#IdTipoMovimentacao").val();

                selectedCancelamento.Object.IdTipoDocumento = $("#IdTipoDocumento").val();
                selectedCancelamento.Object.UnidadeGestoraEmitente = $("#UnidadeGestoraEmitente").val();
                selectedCancelamento.Object.GestaoEmitente = $("#IdGestaoEmitente").val();
                selectedCancelamento.Object.UnidadeGestoraFavorecida = $("#UnidadeGestoraFavorecida").val();
                selectedCancelamento.Object.GestaoFavorecida = $("#IdGestaoFavorecida").val();

                selectedCancelamento.Object.Evento = $("#CancDist").val();
                selectedCancelamento.Object.IdFonte = $("#Fonte").val();
                selectedCancelamento.Object.CategoriaGasto = $("#CategoriaGasto").val();

                selectedCancelamento.Object.EventoNC = $("#EventoNC").val();

                selectedCancelamento.Object.Valor = util.valorToDecimal(Movimentacao.Total.val());

                selectedCancelamento.Object.Observacao = $('#ObservacaoCancelamento').val();
                selectedCancelamento.Object.Observacao2 = $('#ObservacaoCancelamento2').val();
                selectedCancelamento.Object.Observacao3 = $('#ObservacaoCancelamento3').val();

                atualizarMeses(ModelItem.Meses, seq, tab);

                atualizarCancelamentoEmMemoria(selectedCancelamento);
            }
        }

        if (transmitirProdesp === true) {
            var selectedReducao = selecionarReducao(seq);
            if (selectedReducao !== null) {
                tab = "R";
                selectedReducao.Object.UnidadeGestoraEmitente = $("#UnidadeGestoraEmitente").val();

                selectedReducao.Object.IdTipoMovimentacao = $("#IdTipoMovimentacao").val();

                selectedReducao.Object.IdPrograma = $("#Programa").val();
                selectedReducao.Object.IdEstrutura = $("#Natureza").val();

                selectedReducao.Object.AnoExercicio = $("#AnoExercicio").val();
                selectedReducao.Object.NrProcesso = $('#NrProcesso').val();
                selectedReducao.Object.OrigemRecurso = $('#OrigemRecurso').val();
                selectedReducao.Object.DestinoRecurso = $('#DestinoRecurso').val();
                selectedReducao.Object.FlProc = $('#FlProc').val();

                selectedReducao.Object.IdTipoDocumento = $("#IdTipoDocumento").val();

                selectedReducao.Object.NrOrgao = Movimentacao.NrOrgao.val();
                selectedReducao.Object.NrObra = $('#NrObra').val();

                selectedReducao.Object.EspecDespesa = $('#EspecDespesa').val();
                selectedReducao.Object.DescEspecDespesa = Concatenar("DescEspecificacaoDespesa");

                selectedReducao.Object.Valor = util.valorToDecimal(Movimentacao.Total.val());

                selectedReducao.Object.CodigoAutorizadoAssinatura = $("#CodAssAutorizado").val();
                selectedReducao.Object.CodigoAutorizadoGrupo = $("#txtAutorizadoGrupo").val();
                selectedReducao.Object.CodigoAutorizadoOrgao = $("#txtAutorizadoOrgao").val();
                selectedReducao.Object.NomeAutorizadoAssinatura = $("#txtAutorizadoNome").val();
                selectedReducao.Object.AutorizadoCargo = $("#txtAutorizadoCargo").val();

                selectedReducao.Object.CodigoExaminadoAssinatura = $("#CodAssExaminado").val();
                selectedReducao.Object.CodigoExaminadoGrupo = $("#txtExaminadoGrupo").val();
                selectedReducao.Object.CodigoExaminadoOrgao = $("#txtExaminadoOrgao").val();
                selectedReducao.Object.NomeExaminadoAssinatura = $("#txtExaminadoNome").val();
                selectedReducao.Object.ExaminadoCargo = $("#txtExaminadoCargo").val();

                selectedReducao.Object.CodigoResponsavelAssinatura = $("#CodAssResponsavel").val();
                selectedReducao.Object.CodigoResponsavelGrupo = $("#txtResponsavelGrupo").val();
                selectedReducao.Object.CodigoResponsavelOrgao = $("#txtResponsavelOrgao").val();
                selectedReducao.Object.NomeResponsavelAssinatura = $("#txtResponsavelNome").val();
                selectedReducao.Object.ResponsavelCargo = $("#txtResponsavelCargo").val();

                selectedReducao.Object.TotalQ1 = parseFloat($("#TotalQ1").val().replace(/[\.,R$ ]/g, ""));
                selectedReducao.Object.TotalQ2 = parseFloat($("#TotalQ2").val().replace(/[\.,R$ ]/g, ""));
                selectedReducao.Object.TotalQ3 = parseFloat($("#TotalQ3").val().replace(/[\.,R$ ]/g, ""));
                selectedReducao.Object.TotalQ4 = parseFloat($("#TotalQ4").val().replace(/[\.,R$ ]/g, ""));

                atualizarMeses(ModelItem.Meses, seq, tab);

                atualizarReducaoEmMemoria(selectedReducao);
            }
        }

        Movimentacao.selectedRow = "";
    }

    Movimentacao.removeRowCR = function (nRow) {
        var tipoMovimentacao = parseInt(Movimentacao.tipoMovimentacao.val());
        var row = $(nRow).parent();

        var aData = Movimentacao.TabelaCancelamentoReducao.dataTable().fnGetData(row);
        var seq = aData[0];

        limparMemoria(ModelItem.Cancelamento, seq, "C");
        limparMemoriaReducaoSuplementacao(seq, "R");

        Movimentacao.TabelaCancelamentoReducao.dataTable().fnDeleteRow(row);

        if (tipoMovimentacao === TipoMovimentacao.EstornoComNcCancelamento) {
            limparMemoria(ModelItem.NotasCreditos, seq);
            Movimentacao.TabelaNotaDeCredito.dataTable().fnDeleteRow(row);

            reordenarTabela(Movimentacao.TabelaNotaDeCredito);
        }

        reordenarTabela(Movimentacao.TabelaCancelamentoReducao);
    }
    // CancelamentoReducao Fim


    // NotaDeCredito Inicio
    Movimentacao.viewRowNC = function (nRow, canDis) {
        var tab = "N";

        Movimentacao.selectedRow = $(nRow).parent();

        var aData = $('#tblPesquisaNotaCredito').dataTable().fnGetData(Movimentacao.selectedRow);
        var ug = aData[3];
        var seq = aData[0];
        var valor = util.valorToDecimal(aData[7]);

        if (ModelItem.IdTipoMovimentacao === "2") {
            tab = "C";
        }else {
            tab = "D";
        }

        preencherMesesModal(ModelItem.Meses, seq, tab);

        Movimentacao.SomarTotalTrimestre();

        $('input#txtUnidadeGestoraEmitente').val(ModelItem.UnidadeGestoraEmitente);
        $('input#txtIdGestaoEmitente').val(ModelItem.GestaoEmitente);

        var select = selecionarNotaDeCredito(seq, canDis);

        if (select.Object.UnidadeGestoraFavorecida === ug.toString()) {
            $('input#txtEspecDespesaModal', '#modalDadosMovimentacao').val('');
            $('input.Proximo', '#modalDadosMovimentacao').val('');

            $('input#txtProgramaId', '#modalDadosMovimentacao').val(ModelItem.CfpDesc);
            $('input#txtNaturezaId', '#modalDadosMovimentacao').val(ModelItem.CedDesc);

            $('input#txtUnidadeGestoraFavorecida').val(select.Object.UnidadeGestoraFavorecida);
            $('input#txtIdGestaoFavorecida').val(select.Object.GestaoFavorecida);
            $('input#txtFonte').val(select.Object.IdFonte);
            $('input#txtCategoriaGasto').val(select.Object.CategoriaGasto);

            $('input#txtEventoNC').val(select.Object.EventoNC);
            $('input#txtUo').val(select.Object.Uo);
            $('input#txtUGO').val(select.Object.Ugo);
            $('input#txtFonteRecurso').val(select.Object.FonteRecurso);
            $('input#txtNatDespesa').val(select.Object.Fonte);

            $('input#txtObservacaoCancelamentoModal').val(select.Object.ObservacaoCancelamento);
            $('input#txtObservacaoCancelamento2Modal').val(select.Object.ObservacaoCancelamento2);
            $('input#txtObservacaoCancelamento3Modal').val(select.Object.ObservacaoCancelamento3);

            $('input#txtObservacaoNCModal').val(select.Object.Observacao);
            $('input#txtObservacaoNC2Modal').val(select.Object.Observacao2);
            $('input#txtObservacaoNC3Modal').val(select.Object.Observacao3);

            $("#TotalModal").val("R$ " + MaskMonetario(valor.toFixed(2)));
        }

        $("#modalDadosMovimentacao").modal();
    }
    // NotaDeCredito Fim


    // DistribuicaoSuplementacao Inicio
    Movimentacao.viewRowDS = function (nRow) {
        var transmitirProdesp = Movimentacao.valueSelecaoProdesp.is(':checked');
        var transmitirSiafem = Movimentacao.valueSelecaoSiafem.is(':checked');
        var tab = null;

        Movimentacao.selectedRow = $(nRow).parent();

        var aData = Movimentacao.TabelaDistribuicaoSuplementacao.dataTable().fnGetData(Movimentacao.selectedRow);
        var seq = aData[0];

        Movimentacao.SomarTotalTrimestre();

        $('input#txtIdGestaoEmitente', '#modalDadosMovimentacao').val(ModelItem.GestaoEmitente);
        $('input#txtUnidadeGestoraEmitente', '#modalDadosMovimentacao').val(ModelItem.UnidadeGestoraEmitente);
        $('input#txtProgramaId', '#modalDadosMovimentacao').val(ModelItem.CfpDesc);
        $('input#txtNaturezaId', '#modalDadosMovimentacao').val(ModelItem.CedDesc);

        if (transmitirSiafem === true) {
            tab = "D";
            var selectedDistribuicao = selecionarDistribuicao(seq);
            if (selectedDistribuicao !== null) {
                var selectedNotaDeCredito = selecionarNotaDeCredito(seq, tab);
                if (selectedNotaDeCredito !== null) {
                    //$('#txtNaturezaId', '#modalDadosMovimentacao').val(selectedNotaDeCredito.Object.IdEstrutura);
                    //$('#txtProgramaId', '#modalDadosMovimentacao').val(selectedNotaDeCredito.Object.IdPrograma);
                    $('input#txtUo', '#modalDadosMovimentacao').val(selectedNotaDeCredito.Object.Uo);
                    $('input#txtUgo', '#modalDadosMovimentacao').val(selectedNotaDeCredito.Object.Ugo);
                    $('input#txtplano_interno', '#modalDadosMovimentacao').val(selectedNotaDeCredito.Object.PlanoInterno);

                    $('input#txtObservacaoNCModal', '#modalDadosMovimentacao').val(selectedNotaDeCredito.Object.Observacao);
                    $('input#txtObservacaoNC2Modal', '#modalDadosMovimentacao').val(selectedNotaDeCredito.Object.Observacao2);
                    $('input#txtObservacaoNC3Modal', '#modalDadosMovimentacao').val(selectedNotaDeCredito.Object.Observacao3);
                }

                $('#modalDadosMovimentacao input#txtCancDist').val(selectedDistribuicao.Object.Evento);
                $('#modalDadosMovimentacao input#txtUnidadeGestoraFavorecida').val(selectedDistribuicao.Object.UnidadeGestoraFavorecida);
                $('#modalDadosMovimentacao input#txtIdGestaoFavorecida').val(selectedDistribuicao.Object.IdGestaoFavorecida);

                $('#modalDadosMovimentacao input#txtEventoNC').val(selectedDistribuicao.Object.EventoNC);

                $('#modalDadosMovimentacao input#txtFonteRecurso').val("");
                $('#modalDadosMovimentacao input#txtNatDespesa').val(selectedDistribuicao.Object.NatDespesa);

                $('#modalDadosMovimentacao input#txtFonte').val(selectedDistribuicao.Object.IdFonte);

                $('#modalDadosMovimentacao input#txtObservacaoCancelamentoModal').val(selectedDistribuicao.Object.Observacao);
                $('#modalDadosMovimentacao input#txtObservacaoCancelamento2Modal').val(selectedDistribuicao.Object.Observacao2);
                $('#modalDadosMovimentacao input#txtObservacaoCancelamento2Modal').val(selectedDistribuicao.Object.Observacao3);
            }
        }

        if (transmitirProdesp === true) {
            var selectedSuplementacao = selecionarSuplementacao(seq);
            if (selectedSuplementacao !== null) {
                tab = "S";
                $('#modalDadosMovimentacao input#txtUnidadeGestoraFavorecida').val(selectedSuplementacao.Object.UnidadeGestoraFavorecida);
                $('#modalDadosMovimentacao input#txtIdGestaoFavorecida').val(selectedSuplementacao.Object.IdGestaoFavorecida);

                $('#AnoExercicio', '#modalDadosMovimentacao').val(selectedSuplementacao.Object.AnoExercicio);
                $('#NrProcesso', '#modalDadosMovimentacao').val(selectedSuplementacao.Object.NrProcesso);
                $('#OrigemRecurso', '#modalDadosMovimentacao').val(selectedSuplementacao.Object.OrigemRecurso);
                $('#DestinoRecurso', '#modalDadosMovimentacao').val(selectedSuplementacao.Object.DestinoRecurso);
                $('#FlProc', '#modalDadosMovimentacao').val(selectedSuplementacao.Object.FlProc);
                $('input#txtEventoNC', '#modalDadosMovimentacao').val(selectedSuplementacao.Object.EventoNC);

                $('#modalDadosMovimentacao input#txtNrObra').val(selectedSuplementacao.Object.NrObra);
                $('#modalDadosMovimentacao input#txtNrOrgao').val(selectedSuplementacao.NrOrgao);
                $('#modalDadosMovimentacao input#txtFonteRecurso').val("");
                $('#modalDadosMovimentacao input#txtNatDespesa').val(selectedSuplementacao.Object.NatDespesa);

                $('#modalDadosMovimentacao input#txtEspecDespesaModal').val(selectedSuplementacao.Object.EspecDespesa);
                CarregarTextArea("DescEspecificacaoDespesaModal", 9, selectedSuplementacao.Object.DescEspecDespesa);
            }
        }

        preencherMesesModal(ModelItem.Meses, seq, tab);

        $("#modalDadosMovimentacao").modal();
    }

    Movimentacao.editRowDS = function (nRow) {
        var transmitirProdesp = Movimentacao.valueSelecaoProdesp.is(':checked');
        var transmitirSiafem = Movimentacao.valueSelecaoSiafem.is(':checked');
        var tipoMovimentacao = parseInt(Movimentacao.tipoMovimentacao.val());

        var tab = null;

        Movimentacao.selectedRow = $(nRow).parent();

        $('#divNew').hide();
        $('#divSaveCancelamentoReducao').hide();
        $('#divSaveDistribuicaoSuplementacao').show();

        var aData = Movimentacao.TabelaDistribuicaoSuplementacao.dataTable().fnGetData(Movimentacao.selectedRow);
        var seq = parseInt(aData[0]);

        if (action.toLowerCase() !== "estornar" || tipoMovimentacao !== TipoMovimentacao.EstornoComNcCancelamento) {
            $(Movimentacao.TabelaNotaDeCredito).find('tr').each(function (index, element) {
                var tdSeq = $(this).find('td')[0];
                if (tdSeq !== undefined) {
                    var seqRowNc = parseInt($(tdSeq).text());

                    if (!isNaN(seqRowNc) && seqRowNc === seq) {
                        Movimentacao.selectedRowNc = $(tdSeq);
                    }
                }
            });
        }

        $('#IdTipoMovimentacao', '#DadoApropriacao').val(ModelItem.IdTipoMovimentacao);
        $('#AnoExercicio', '#DadoApropriacao').val(ModelItem.AnoExercicio);
        $('#UnidadeGestoraEmitente', '#DadoApropriacao').val(ModelItem.UnidadeGestoraEmitente);
        $('#IdGestaoEmitente', '#DadoApropriacao').val(ModelItem.GestaoEmitente);
        $('#Natureza', '#DadoApropriacao').val(ModelItem.IdEstrutura);
        $('#Programa', '#DadoApropriacao').val(ModelItem.IdPrograma);

        if (transmitirSiafem === true) {
            tab = "D";
            var selectedDistribuicao = selecionarDistribuicao(seq);
            if (selectedDistribuicao !== null) {
                var selectedNotaDeCredito = selecionarNotaDeCredito(seq, tab);
                if (selectedNotaDeCredito !== null) {
                    $('#Natureza', '#DadoApropriacao').val(selectedNotaDeCredito.Object.IdEstrutura);
                    $('#Programa', '#DadoApropriacao').val(selectedNotaDeCredito.Object.IdPrograma);

                    $('#Uo', '#DadoApropriacao').val(selectedNotaDeCredito.Object.Uo);
                    $('#FonteRecurso', '#DadoApropriacao').val(selectedNotaDeCredito.Object.FonteRecurso);
                    $('#UGO', '#DadoApropriacao').val(selectedNotaDeCredito.Object.Ugo);
                    $('#PlanoInterno', '#DadoApropriacao').val(selectedNotaDeCredito.Object.PlanoInterno);

                    $('#ObservacaoNC', '#divObservacaoNC').val(selectedNotaDeCredito.Object.Observacao);
                    $('#ObservacaoNC2', '#divObservacaoNC').val(selectedNotaDeCredito.Object.Observacao2);
                    $('#ObservacaoNC3', '#divObservacaoNC').val(selectedNotaDeCredito.Object.Observacao3);
                }

                $('#IdTipoDocumento', '#DadoApropriacao').val(selectedDistribuicao.Object.IdTipoDocumento);
                $('#UnidadeGestoraFavorecida', '#DadoApropriacao').val(selectedDistribuicao.Object.UnidadeGestoraFavorecida);
                $('#IdGestaoFavorecida', '#DadoApropriacao').val(selectedDistribuicao.Object.GestaoFavorecida);

                $('#CancDist', '#DadoApropriacao').val(selectedDistribuicao.Object.Evento);
                $('#CategoriaGasto', '#DadoApropriacao').val(selectedDistribuicao.Object.CategoriaGasto);

                $('#EventoNC', '#DadoApropriacao').val(selectedDistribuicao.Object.EventoNC);

                $('#ObservacaoCancelamento', '#divObservacaoCancelamento').val(selectedDistribuicao.Object.Observacao);
                $('#ObservacaoCancelamento2', '#divObservacaoCancelamento').val(selectedDistribuicao.Object.Observacao2);
                $('#ObservacaoCancelamento3', '#divObservacaoCancelamento').val(selectedDistribuicao.Object.Observacao3);

                Movimentacao.Total.val("R$ " + MaskMonetario(selectedDistribuicao.Object.Valor.toFixed(2)));
            }
        }

        if (transmitirProdesp === true) {
            tab = "S";
            var selectedSuplementacao = selecionarSuplementacao(seq);
            if (selectedSuplementacao !== null) {
                $('#NrProcesso', '#DadoApropriacao').val(selectedSuplementacao.Object.NrProcesso);
                $('#OrigemRecurso', '#DadoApropriacao').val(selectedSuplementacao.Object.OrigemRecurso);
                $('#DestinoRecurso', '#DadoApropriacao').val(selectedSuplementacao.Object.DestinoRecurso);
                $('#FlProc', '#DadoApropriacao').val(selectedSuplementacao.Object.FlProc);

                $('#IdTipoDocumento', '#DadoApropriacao').val(selectedSuplementacao.Object.IdTipoDocumento);

                $('#NrOrgao', '#DadoApropriacao').val(selectedSuplementacao.Object.NrOrgao);
                $('#NrObra', '#DadoApropriacao').val(selectedSuplementacao.Object.NrObra);

                $('#EspecDespesa').val(selectedSuplementacao.Object.EspecDespesa);
                CarregarTextArea("DescEspecificacaoDespesa", 9, selectedSuplementacao.Object.DescEspecDespesa);

                Movimentacao.Total.val("R$ " + MaskMonetario(selectedSuplementacao.Object.Valor.toFixed(2)));
            }
        }

        preencherMeses(ModelItem.Meses, seq, tab);
    }

    Movimentacao.saveRowDS = function (nRow) {
        var transmitirProdesp = Movimentacao.valueSelecaoProdesp.is(':checked');
        var transmitirSiafem = Movimentacao.valueSelecaoSiafem.is(':checked');
        var tipoMovimentacao = parseInt(Movimentacao.tipoMovimentacao.val());

        var tab = null;

        setAfterSaveButtons();

        var aData = Movimentacao.TabelaDistribuicaoSuplementacao.dataTable().fnGetData(Movimentacao.selectedRow);
        var seq = parseInt(aData[0]);

        Movimentacao.TabelaDistribuicaoSuplementacao.dataTable().fnUpdate($("#UnidadeGestoraEmitente").val(), Movimentacao.selectedRow, 3, false);
        Movimentacao.TabelaDistribuicaoSuplementacao.dataTable().fnUpdate($("#UnidadeGestoraFavorecida").val(), Movimentacao.selectedRow, 4, false);
        Movimentacao.TabelaDistribuicaoSuplementacao.dataTable().fnUpdate(Movimentacao.NrOrgao.val(), Movimentacao.selectedRow, 5, false);
        Movimentacao.TabelaDistribuicaoSuplementacao.dataTable().fnUpdate($('#Fonte').val(), Movimentacao.selectedRow, 6, false);
        Movimentacao.TabelaDistribuicaoSuplementacao.dataTable().fnUpdate(Movimentacao.CategoriaGasto.val(), Movimentacao.selectedRow, 7, false);
        Movimentacao.TabelaDistribuicaoSuplementacao.dataTable().fnUpdate(Movimentacao.Total.val(), Movimentacao.selectedRow, 8, false);
        Movimentacao.TabelaDistribuicaoSuplementacao.dataTable().fnDraw();

        ModelItem.IdPrograma = Movimentacao.Programa.val();
        ModelItem.IdEstrutura = Movimentacao.Estrutura.val();

        if (transmitirSiafem === true) {
            var selectedNotaDeCredito = selecionarNotaDeCredito(seq, "D");

            if (selectedNotaDeCredito !== null && action.toLowerCase() !== "estornar" && tipoMovimentacao !== TipoMovimentacao.EstornoComNcCancelamento) {
                tab = "N";

                Movimentacao.TabelaNotaDeCredito.dataTable().fnUpdate($("#UnidadeGestoraEmitente").val(), Movimentacao.selectedRowNc, 2, false);
                Movimentacao.TabelaNotaDeCredito.dataTable().fnUpdate($("#UnidadeGestoraFavorecida").val(), Movimentacao.selectedRowNc, 3, false);
                Movimentacao.TabelaNotaDeCredito.dataTable().fnUpdate(Movimentacao.NrOrgao.val(), Movimentacao.selectedRowNc, 4, false);
                Movimentacao.TabelaNotaDeCredito.dataTable().fnUpdate($('#FonteRecurso').val(), Movimentacao.selectedRowNc, 5, false);
                Movimentacao.TabelaNotaDeCredito.dataTable().fnUpdate(Movimentacao.CategoriaGasto.val(), Movimentacao.selectedRowNc, 6, false);
                Movimentacao.TabelaNotaDeCredito.dataTable().fnUpdate(Movimentacao.Total.val(), Movimentacao.selectedRowNc, 7, false);
                Movimentacao.TabelaNotaDeCredito.dataTable().fnDraw();

                selectedNotaDeCredito.Object.IdTipoMovimentacao = $("#IdTipoMovimentacao").val();

                selectedNotaDeCredito.Object.IdPrograma = Movimentacao.Programa.val();
                selectedNotaDeCredito.Object.IdEstrutura = Movimentacao.Estrutura.val();

                selectedNotaDeCredito.Object.IdTipoDocumento = $("#IdTipoDocumento").val();
                selectedNotaDeCredito.Object.UnidadeGestoraFavorecida = $("#UnidadeGestoraFavorecida").val();
                selectedNotaDeCredito.Object.GestaoFavorecida = $("#IdGestaoFavorecida").val();

                selectedNotaDeCredito.Object.EventoNC = $("#EventoNC").val();

                selectedNotaDeCredito.Object.Uo = $("#Uo").val();
                selectedNotaDeCredito.Object.Ugo = $("#UGO").val();
                selectedNotaDeCredito.Object.FonteRecurso = $("#FonteRecurso").val();

                selectedNotaDeCredito.Object.PlanoInterno = $("#PlanoInterno").val();

                selectedNotaDeCredito.Object.Valor = util.valorToDecimal(Movimentacao.Total.val());

                selectedNotaDeCredito.Object.Observacao = $('#ObservacaoNC').val();
                selectedNotaDeCredito.Object.Observacao2 = $('#ObservacaoNC2').val();
                selectedNotaDeCredito.Object.Observacao3 = $('#ObservacaoNC3').val();

                atualizarMeses(ModelItem.Meses, seq, tab);

                atualizarNotaDeCreditoEmMemoria(selectedNotaDeCredito);
            }

            var selectedDistribuicao = selecionarDistribuicao(seq);
            if (selectedDistribuicao !== null) {
                tab = "D";
                selectedDistribuicao.Object.UnidadeGestoraEmitente = $("#UnidadeGestoraEmitente").val();

                selectedDistribuicao.Object.IdTipoMovimentacao = $("#IdTipoMovimentacao").val();
                selectedDistribuicao.Object.UnidadeGestoraEmitente = $("#UnidadeGestoraEmitente").val();
                selectedDistribuicao.Object.GestaoEmitente = $("#IdGestaoEmitente").val();
                selectedDistribuicao.Object.UnidadeGestoraFavorecida = $("#UnidadeGestoraFavorecida").val();
                selectedDistribuicao.Object.GestaoFavorecida = $("#IdGestaoFavorecida").val();

                selectedDistribuicao.Object.Evento = $("#CancDist").val();
                selectedDistribuicao.Object.IdFonte = $("#Fonte").val();
                selectedDistribuicao.Object.CategoriaGasto = $("#CategoriaGasto").val();

                selectedDistribuicao.Object.EventoNC = $("#EventoNC").val();

                selectedDistribuicao.Object.IdTipoDocumento = $("#IdTipoDocumento").val();

                selectedDistribuicao.Object.Valor = util.valorToDecimal(Movimentacao.Total.val());

                selectedDistribuicao.Object.Observacao = $('#ObservacaoCancelamento').val();
                selectedDistribuicao.Object.Observacao2 = $('#ObservacaoCancelamento2').val();
                selectedDistribuicao.Object.Observacao3 = $('#ObservacaoCancelamento3').val();

                atualizarMeses(ModelItem.Meses, seq, tab);

                atualizarDistribuicaoEmMemoria(selectedDistribuicao);
            }
        }
        if (transmitirProdesp === true) {
            tab = "S";
            var selectedSuplementacao = selecionarSuplementacao(seq);
            if (selectedSuplementacao !== null) {
                selectedSuplementacao.Object.UnidadeGestoraEmitente = $("#UnidadeGestoraEmitente").val();

                selectedSuplementacao.Object.IdTipoMovimentacao = $("#IdTipoMovimentacao").val();

                selectedSuplementacao.Object.IdPrograma = Movimentacao.Programa.val();
                selectedSuplementacao.Object.IdEstrutura = Movimentacao.Estrutura.val();

                selectedSuplementacao.Object.AnoExercicio = $("#AnoExercicio").val();
                selectedSuplementacao.Object.NrProcesso = $('#NrProcesso').val();
                selectedSuplementacao.Object.OrigemRecurso = $('#OrigemRecurso').val();
                selectedSuplementacao.Object.DestinoRecurso = $('#DestinoRecurso').val();
                selectedSuplementacao.Object.FlProc = $('#FlProc').val();

                selectedSuplementacao.Object.IdTipoDocumento = $("#IdTipoDocumento").val();

                selectedSuplementacao.Object.NrOrgao = Movimentacao.NrOrgao.val();
                selectedSuplementacao.Object.NrObra = $('#NrObra').val();

                selectedSuplementacao.Object.EspecDespesa = $('#EspecDespesa').val();
                selectedSuplementacao.Object.DescEspecDespesa = Concatenar("DescEspecificacaoDespesa");

                selectedSuplementacao.Object.Valor = util.valorToDecimal(Movimentacao.Total.val());

                selectedSuplementacao.Object.CodigoAutorizadoAssinatura = $("#CodAssAutorizado").val();
                selectedSuplementacao.Object.CodigoAutorizadoGrupo = $("#txtAutorizadoGrupo").val();
                selectedSuplementacao.Object.CodigoAutorizadoOrgao = $("#txtAutorizadoOrgao").val();
                selectedSuplementacao.Object.NomeAutorizadoAssinatura = $("#txtAutorizadoNome").val();
                selectedSuplementacao.Object.AutorizadoCargo = $("#txtAutorizadoCargo").val();

                selectedSuplementacao.Object.CodigoExaminadoAssinatura = $("#CodAssExaminado").val();
                selectedSuplementacao.Object.CodigoExaminadoGrupo = $("#txtExaminadoGrupo").val();
                selectedSuplementacao.Object.CodigoExaminadoOrgao = $("#txtExaminadoOrgao").val();
                selectedSuplementacao.Object.NomeExaminadoAssinatura = $("#txtExaminadoNome").val();
                selectedSuplementacao.Object.ExaminadoCargo = $("#txtExaminadoCargo").val();

                selectedSuplementacao.Object.CodigoResponsavelAssinatura = $("#CodAssResponsavel").val();
                selectedSuplementacao.Object.CodigoResponsavelGrupo = $("#txtResponsavelGrupo").val();
                selectedSuplementacao.Object.CodigoResponsavelOrgao = $("#txtResponsavelOrgao").val();
                selectedSuplementacao.Object.NomeResponsavelAssinatura = $("#txtResponsavelNome").val();

                selectedSuplementacao.Object.ResponsavelCargo = $("#txtResponsavelCargo").val();



                selectedSuplementacao.Object.TotalQ1 = parseFloat($("#TotalQ1").val().replace(/[\.,R$ ]/g, ""));
                selectedSuplementacao.Object.TotalQ2 = parseFloat($("#TotalQ2").val().replace(/[\.,R$ ]/g, ""));
                selectedSuplementacao.Object.TotalQ3 = parseFloat($("#TotalQ3").val().replace(/[\.,R$ ]/g, ""));
                selectedSuplementacao.Object.TotalQ4 = parseFloat($("#TotalQ4").val().replace(/[\.,R$ ]/g, ""));

                atualizarMeses(ModelItem.Meses, seq, tab);

                atualizarSuplementacaoEmMemoria(selectedSuplementacao);
            }
        }

        Movimentacao.selectedRow = "";
    }

    Movimentacao.removeRowDS = function (nRow) {
        var tipoMovimentacao = parseInt(Movimentacao.tipoMovimentacao.val());

        var row = $(nRow).parent();

        var aData = Movimentacao.TabelaDistribuicaoSuplementacao.dataTable().fnGetData(row);
        var seq = aData[0];

        limparMemoria(ModelItem.Distribuicao, seq, "D");
        limparMemoriaReducaoSuplementacao(seq, "S");

        Movimentacao.TabelaDistribuicaoSuplementacao.dataTable().fnDeleteRow(row);

        if (tipoMovimentacao === TipoMovimentacao.TransferenciaComNcDistribuicao) {
            limparMemoria(ModelItem.NotasCreditos, seq);
            Movimentacao.TabelaNotaDeCredito.dataTable().fnDeleteRow(row);

            reordenarTabela(Movimentacao.TabelaNotaDeCredito);
        }

        reordenarTabela(Movimentacao.TabelaDistribuicaoSuplementacao);
    }
    // DistribuicaoSuplementacao Fim


    function addRow() {
        var transmitirProdesp = Movimentacao.valueSelecaoProdesp.is(':checked');
        var transmitirSiafem = Movimentacao.valueSelecaoSiafem.is(':checked');
        var tipoDocumentoSelecionado = parseInt(Movimentacao.tipoDocumento.val());
        var tipoMovimentacao = parseInt(Movimentacao.tipoMovimentacao.val());

        var tab = null;

        //ModelItem.IdTipoMovimentacao = $("#IdTipoMovimentacao").val();
        ModelItem.UnidadeGestoraEmitente = $("#UnidadeGestoraEmitente").val();
        ModelItem.GestaoEmitente = $("#IdGestaoEmitente").val();
        ModelItem.IdPrograma = Movimentacao.Programa.val();
        ModelItem.IdEstrutura = Movimentacao.Estrutura.val();

        var itemMemoriaCR = null;
        var itemMemoriaDS = null;
        if (tipoDocumentoSelecionado === TipoDocumento.CancelamentoReducao) { // Cancelamento/Redução
            if (transmitirProdesp) {
                tab = "R";

                var seqR = obterSequenciaReducaoSuplementacao("R");
                var total = util.valorToDecimal(Movimentacao.Total.val());

                var itemMemoriaR = {
                    Id: 0,
                    IdMovimentacao: ModelItem.Id,

                    NrSequencia: seqR,

                    FlProc: $('#FlProc').val(),
                    NrProcesso: $('#NrProcesso').val(),
                    NrOrgao: $('#NrOrgao').val(),
                    NrObra: $('#NrObra').val(),
                    RedSup: tab,

                    IdPrograma: Movimentacao.Programa.val(),
                    UnidadeGestoraEmitente: $('#UnidadeGestoraEmitente').val(),
                    GestaoEmitente: $('#IdGestaoEmitente').val(),
                    UnidadeGestoraFavorecida: $('#UnidadeGestoraFavorecida').val(),
                    IdGestaoFavorecida: $('#IdGestaoFavorecida').val(),

                    Evento: $('#Evento').val(),
                    CategoriaGasto: $('#CategoriaGasto').val(),
                    IdFonte: $('#Fonte').val(),


                    OrigemRecurso: $('#OrigemRecurso').val(),
                    DestinoRecurso: $('#DestinoRecurso').val(),

                    EspecDespesa: $('#EspecDespesa').val(),
                    DescEspecDespesa: Concatenar("DescEspecificacaoDespesa"),

                    CodigoAutorizadoAssinatura: $("#CodAssAutorizado").val(),
                    CodigoAutorizadoGrupo: $("#txtAutorizadoGrupo").val(),
                    CodigoAutorizadoOrgao: $("#txtAutorizadoOrgao").val(),
                    DescricaoAutorizadoCargo: $("#txtAutorizadoCargo").val(),
                    NomeAutorizadoAssinatura: $("#txtAutorizadoNome").val(),
                    CodigoExaminadoAssinatura: $("#CodAssExaminado").val(),
                    CodigoExaminadoGrupo: $("#txtExaminadoGrupo").val(),
                    CodigoExaminadoOrgao: $("#txtExaminadoOrgao").val(),
                    DescricaoExaminadoCargo: $("#txtExaminadoCargo").val(),
                    NomeExaminadoAssinatura: $("#txtExaminadoNome").val(),
                    CodigoResponsavelAssinatura: $("#CodAssResponsavel").val(),
                    CodigoResponsavelGrupo: $("#txtResponsavelGrupo").val(),
                    CodigoResponsavelOrgao: $("#txtResponsavelOrgao").val(),
                    DescricaoResponsavelCargo: $("#txtResponsavelCargo").val(),
                    NomeResponsavelAssinatura: $("#txtResponsavelNome").val(),

                    Valor: total,
                    TotalQ1: parseFloat($("#TotalQ1").val().replace(/[\.,R$ ]/g, "")),
                    TotalQ2: parseFloat($("#TotalQ2").val().replace(/[\.,R$ ]/g, "")),
                    TotalQ3: parseFloat($("#TotalQ3").val().replace(/[\.,R$ ]/g, "")),
                    TotalQ4: parseFloat($("#TotalQ4").val().replace(/[\.,R$ ]/g, "")),

                    IdTipoDocumento: tipoDocumentoSelecionado
                };

                ModelItem.ReducaoSuplementacao[ModelItem.ReducaoSuplementacao.length] = itemMemoriaR;
                salvarMesesMemoria(itemMemoriaR.NrSequencia, tab);

                itemMemoriaCR = itemMemoriaR;
            }

            if (transmitirSiafem) {
                tab = "C";

                var seqC = obterSequencia(ModelItem.Cancelamento);
                var total = util.valorToDecimal(Movimentacao.Total.val());

                var itemMemoriaC = {
                    Id: 0,
                    IdMovimentacao: ModelItem.Id,

                    IdPrograma: Movimentacao.Programa.val(),
                    IdEstrutura: Movimentacao.Estrutura.val(),

                    IdTipoDocumento: tipoDocumentoSelecionado,
                    UnidadeGestoraEmitente: $('#UnidadeGestoraEmitente').val(),
                    GestaoEmitente: $('#IdGestaoEmitente').val(),
                    UnidadeGestoraFavorecida: $('#UnidadeGestoraFavorecida').val(),
                    GestaoFavorecida: $('#IdGestaoFavorecida').val(),

                    Evento: $('#CancDist').val(),
                    CategoriaGasto: $('#CategoriaGasto').val(),
                    IdFonte: $('#Fonte').val(),

                    EventoNC: $('#EventoNC').val(),

                    Observacao: $('#ObservacaoCancelamento').val(),
                    Observacao2: $('#ObservacaoCancelamento2').val(),
                    Observacao3: $('#ObservacaoCancelamento3').val(),

                    FlProc: $('#FlProc').val(),
                    NrProcesso: $('#NrProcesso').val(),

                    NrOrgao: $('#NrOrgao').val(),

                    NrObra: $('#NrObra').val(),
                    OrigemRecurso: $('#OrigemRecurso').val(),
                    DestinoRecurso: $('#DestinoRecurso').val(),

                    EspecDespesa: $('#EspecDespesa').val(),
                    DescEspecDespesa: Concatenar("DescEspecificacaoDespesa"),

                    CodigoAutorizadoAssinatura: $("#CodAssAutorizado").val(),
                    CodigoAutorizadoGrupo: $("#txtAutorizadoGrupo").val(),
                    CodigoAutorizadoOrgao: $("#txtAutorizadoOrgao").val(),
                    DescricaoAutorizadoCargo: $("#txtAutorizadoCargo").val(),
                    NomeAutorizadoAssinatura: $("#txtAutorizadoNome").val(),
                    CodigoExaminadoAssinatura: $("#CodAssExaminado").val(),
                    CodigoExaminadoGrupo: $("#txtExaminadoGrupo").val(),
                    CodigoExaminadoOrgao: $("#txtExaminadoOrgao").val(),
                    DescricaoExaminadoCargo: $("#txtExaminadoCargo").val(),
                    NomeExaminadoAssinatura: $("#txtExaminadoNome").val(),
                    CodigoResponsavelAssinatura: $("#CodAssResponsavel").val(),
                    CodigoResponsavelGrupo: $("#txtResponsavelGrupo").val(),
                    CodigoResponsavelOrgao: $("#txtResponsavelOrgao").val(),
                    DescricaoResponsavelCargo: $("#txtResponsavelCargo").val(),
                    NomeResponsavelAssinatura: $("#txtResponsavelNome").val(),

                    Valor: total,
                    TotalQ1: parseFloat($("#TotalQ1").val().replace(/[\.,R$ ]/g, "")),
                    TotalQ2: parseFloat($("#TotalQ2").val().replace(/[\.,R$ ]/g, "")),
                    TotalQ3: parseFloat($("#TotalQ3").val().replace(/[\.,R$ ]/g, "")),
                    TotalQ4: parseFloat($("#TotalQ4").val().replace(/[\.,R$ ]/g, "")),

                    NrSequencia: seqC
                };

                ModelItem.Cancelamento[ModelItem.Cancelamento.length] = itemMemoriaC;
                salvarMesesMemoria(itemMemoriaC.NrSequencia, tab);

                itemMemoriaCR = itemMemoriaC;
            }

            if (itemMemoriaCR !== null) {
                var itemTabelaCR = [
                    itemMemoriaCR.NrSequencia,
                    "",
                    "",
                    itemMemoriaCR.UnidadeGestoraEmitente,
                    itemMemoriaCR.UnidadeGestoraFavorecida,
                    itemMemoriaCR.NrOrgao,
                    itemMemoriaCR.IdFonte,
                    itemMemoriaCR.CategoriaGasto,
                    'R$ ' + MaskMonetario(itemMemoriaCR.Valor.toFixed(2)),
                    "",
                    "",
                    '<button type="button" title="Alterar" class="btn btn-xs btn-info margL7" onclick="Movimentacao.editRowCR(this)"><i class="fa fa-edit"></i></button>' +
                    '<button type="button" title="Excluir" class="btn btn-xs btn-danger margL7" onclick="Movimentacao.removeRowCR(this)"><i class="fa fa-trash"></i></button>' +
                    '<button type="button" title="Visualizar" style="background-color:darkblue" class="btn btn-xs btn-info margL7 lockSiafisico lockSiafem" onclick="Movimentacao.viewRowCR(this)"><i class="fa fa-search"></i></button>'
                ];

                Movimentacao.TabelaCancelamentoReducao.dataTable().fnAddData(itemTabelaCR);
            }
        }
        else if (tipoDocumentoSelecionado === TipoDocumento.DistribuicaoSuplementacao) { // Distribuição/Suplementação
            if (transmitirProdesp) {
                tab = "S";
                var seqS = obterSequenciaReducaoSuplementacao("S");

                var itemMemoriaS = {
                    Id: 0,
                    IdMovimentacao: ModelItem.Id,

                    IdPrograma: Movimentacao.Programa.val(),

                    IdTipoDocumento: tipoDocumentoSelecionado,
                    UnidadeGestoraEmitente: $('#UnidadeGestoraEmitente').val(),
                    GestaoEmitente: $('#IdGestaoEmitente').val(),
                    UnidadeGestoraFavorecida: $('#UnidadeGestoraFavorecida').val(),
                    GestaoFavorecida: $('#IdGestaoFavorecida').val(),

                    FlProc: $('#FlProc').val(),
                    NrProcesso: $('#NrProcesso').val(),

                    NrOrgao: $('#NrOrgao').val(),
                    NrObra: $('#NrObra').val(),
                    OrigemRecurso: $('#OrigemRecurso').val(),
                    DestinoRecurso: $('#DestinoRecurso').val(),

                    CategoriaGasto: $('#CategoriaGasto').val(),
                    IdFonte: $('#Fonte').val(),

                    EspecDespesa: $('#EspecDespesa').val(),
                    DescEspecDespesa: Concatenar("DescEspecificacaoDespesa"),

                    CodigoAutorizadoAssinatura: $("#CodAssAutorizado").val(),
                    CodigoAutorizadoGrupo: $("#txtAutorizadoGrupo").val(),
                    CodigoAutorizadoOrgao: $("#txtAutorizadoOrgao").val(),
                    DescricaoAutorizadoCargo: $("#txtAutorizadoCargo").val(),
                    NomeAutorizadoAssinatura: $("#txtAutorizadoNome").val(),
                    CodigoExaminadoAssinatura: $("#CodAssExaminado").val(),
                    CodigoExaminadoGrupo: $("#txtExaminadoGrupo").val(),
                    CodigoExaminadoOrgao: $("#txtExaminadoOrgao").val(),
                    DescricaoExaminadoCargo: $("#txtExaminadoCargo").val(),
                    NomeExaminadoAssinatura: $("#txtExaminadoNome").val(),
                    CodigoResponsavelAssinatura: $("#CodAssResponsavel").val(),
                    CodigoResponsavelGrupo: $("#txtResponsavelGrupo").val(),
                    CodigoResponsavelOrgao: $("#txtResponsavelOrgao").val(),
                    DescricaoResponsavelCargo: $("#txtResponsavelCargo").val(),
                    NomeResponsavelAssinatura: $("#txtResponsavelNome").val(),

                    Valor: util.valorToDecimal(Movimentacao.Total.val()),
                    TotalQ1: parseFloat($("#TotalQ1").val().replace(/[\.,R$ ]/g, "")),
                    TotalQ2: parseFloat($("#TotalQ2").val().replace(/[\.,R$ ]/g, "")),
                    TotalQ3: parseFloat($("#TotalQ3").val().replace(/[\.,R$ ]/g, "")),
                    TotalQ4: parseFloat($("#TotalQ4").val().replace(/[\.,R$ ]/g, "")),

                    NrSequencia: seqS,
                    RedSup: tab
                };

                ModelItem.ReducaoSuplementacao[ModelItem.ReducaoSuplementacao.length] = itemMemoriaS;
                salvarMesesMemoria(itemMemoriaS.NrSequencia, tab);

                itemMemoriaDS = itemMemoriaS;
            }

            if (transmitirSiafem) {
                tab = "D";
                var seqD = obterSequencia(ModelItem.Distribuicao);

                var itemMemoriaD = {
                    Id: 0,
                    IdMovimentacao: ModelItem.Id,

                    IdPrograma: Movimentacao.Programa.val(),
                    IdEstrutura: Movimentacao.Estrutura.val(),

                    IdTipoDocumento: tipoDocumentoSelecionado,
                    UnidadeGestoraEmitente: $('#UnidadeGestoraEmitente').val(),
                    GestaoEmitente: $('#IdGestaoEmitente').val(),
                    UnidadeGestoraFavorecida: $('#UnidadeGestoraFavorecida').val(),
                    GestaoFavorecida: $('#IdGestaoFavorecida').val(),

                    FlProc: $('#FlProc').val(),
                    NrProcesso: $('#NrProcesso').val(),

                    NrOrgao: $('#NrOrgao').val(),
                    NrObra: $('#NrObra').val(),
                    Evento: $('#CancDist').val(),
                    IdFonte: $('#Fonte').val(),
                    CategoriaGasto: $('#CategoriaGasto').val(),

                    EventoNC: $('#EventoNC').val(),

                    Observacao: $('#ObservacaoCancelamento').val(),
                    Observacao2: $('#ObservacaoCancelamento2').val(),
                    Observacao3: $('#ObservacaoCancelamento3').val(),

                    Valor: util.valorToDecimal(Movimentacao.Total.val()),

                    NrSequencia: seqD,
                    CanDis: tab
                };

                ModelItem.Distribuicao[ModelItem.Distribuicao.length] = itemMemoriaD;
                salvarMesesMemoria(itemMemoriaD.NrSequencia, tab);

                itemMemoriaDS = itemMemoriaD;
            }

            if (itemMemoriaDS !== null) {
                var itemTabelaDS = [
                    itemMemoriaDS.NrSequencia,
                    "",
                    "",
                    itemMemoriaDS.UnidadeGestoraEmitente,
                    itemMemoriaDS.UnidadeGestoraFavorecida,
                    itemMemoriaDS.NrOrgao,
                    itemMemoriaDS.IdFonte,
                    itemMemoriaDS.CategoriaGasto,
                    'R$ ' + MaskMonetario(itemMemoriaDS.Valor.toFixed(2)),
                    "",
                    "",
                    '<button type="button" title="Alterar" class="btn btn-xs btn-info margL7" onclick="Movimentacao.editRowDS(this)"><i class="fa fa-edit"></i></button>' +
                    '<button type="button" title="Excluir" class="btn btn-xs btn-danger margL7" onclick="Movimentacao.removeRowDS(this)"><i class="fa fa-trash"></i></button>' +
                    '<button type="button" title="Visualizar" style="background-color:darkblue" class="btn btn-xs btn-info margL7 lockSiafisico lockSiafem" onclick="Movimentacao.viewRowDS(this)"><i class="fa fa-search"></i></button>'
                ];

                Movimentacao.TabelaDistribuicaoSuplementacao.dataTable().fnAddData(itemTabelaDS);
            }
        }

        if (transmitirSiafem === true) {
            var seqNC = obterSequencia(ModelItem.NotasCreditos);

            var itemMemoriaNC = null;
            var itemTabelaNC = null;
            var itemMemoriaCRDS = null;
            var tabNC = null;

            if (tipoMovimentacao === TipoMovimentacao.TransferenciaComNcDistribuicao) {
                tabNC = "D";
                if (tipoDocumentoSelecionado === TipoDocumento.DistribuicaoSuplementacao) {
                    itemMemoriaCRDS = itemMemoriaDS;
                }
            }
            else if (tipoMovimentacao === TipoMovimentacao.EstornoComNcCancelamento) {
                tabNC = "C";
                if (tipoDocumentoSelecionado === TipoDocumento.CancelamentoReducao) {
                    itemMemoriaCRDS = itemMemoriaCR;
                }
            }

            if (itemMemoriaCRDS !== null) {
                var emitente = itemMemoriaCRDS.UnidadeGestoraEmitente;
                var favorecida = itemMemoriaCRDS.UnidadeGestoraFavorecida;

                if (tipoMovimentacao === TipoMovimentacao.EstornoComNcCancelamento) {
                    emitente = itemMemoriaCRDS.UnidadeGestoraFavorecida;
                    favorecida = itemMemoriaCRDS.UnidadeGestoraEmitente;
                }

                itemMemoriaNC = {
                    Id: 0,
                    IdPrograma: Movimentacao.Programa.val(),
                    IdFonte: $('#Fonte').val(),
                    IdEstrutura: Movimentacao.Estrutura.val(),
                    IdMovimentacao: ModelItem.Id,
                    NrSequencia: seqNC,
                    CanDis: tabNC,
                    Valor: util.valorToDecimal(Movimentacao.Total.val()),
                    UnidadeGestoraEmitente: emitente,
                    UnidadeGestoraFavorecida: favorecida,
                    Uo: $("#Uo").val(),
                    PlanoInterno: $("#PlanoInterno").val(),
                    EventoNC: $('#EventoNC').val(),
                    GestaoFavorecida: $('#IdGestaoFavorecida').val(),
                    Ugo: $("#UGO").val(),
                    FonteRecurso: $("#FonteRecurso").val(),

                    Observacao: $('#ObservacaoNC').val(),
                    Observacao2: $('#ObservacaoNC2').val(),
                    Observacao3: $('#ObservacaoNC3').val(),

                    IdTipoDocumento: tipoDocumentoSelecionado
                };

                ModelItem.NotasCreditos[ModelItem.NotasCreditos.length] = itemMemoriaNC;

                itemTabelaNC = [
                    itemMemoriaCRDS.NrSequencia,
                    "",
                    emitente,
                    favorecida,
                    itemMemoriaCRDS.NrOrgao,
                    itemMemoriaNC.FonteRecurso,
                    itemMemoriaCRDS.CategoriaGasto,
                    'R$ ' + MaskMonetario(itemMemoriaCRDS.Valor.toFixed(2)),
                    "",
                    "",
                    '<button type="button" title="Visualizar" style="background-color:darkblue" class="btn btn-xs btn-info margL7 lockSiafisico lockSiafem" onclick="Movimentacao.viewRowNC(this, \'' + tab + '\')"><i class="fa fa-search"></i></button>'
                ];

                Movimentacao.TabelaNotaDeCredito.dataTable().fnAddData(itemTabelaNC);
            }

        }

        Movimentacao.selectedRow = "";
    }


    function selecionarCancelamento(seq) {

        var selected = null
        $.each(ModelItem.Cancelamento, function (index, value) {
            if (ModelItem.Cancelamento[index].NrSequencia.toString() === seq.toString()) {
                selected = {
                    Index: index,
                    Object: ModelItem.Cancelamento[index]
                }
            }
        });
        return selected;
    }
    function selecionarDistribuicao(seq) {

        var selected = null
        $.each(ModelItem.Distribuicao, function (index, value) {
            if (ModelItem.Distribuicao[index].NrSequencia.toString() === seq.toString()) {
                selected = {
                    Index: index,
                    Object: ModelItem.Distribuicao[index]
                }
            }
        });
        return selected;
    }
    function selecionarNotaDeCredito(seq, canDis) {

        var selected = null

        $.each(ModelItem.NotasCreditos, function (index, value) {
            if (ModelItem.NotasCreditos[index].NrSequencia.toString() === seq.toString() && ModelItem.NotasCreditos[index].CanDis === canDis) {
                selected = {
                    Index: index,
                    Object: ModelItem.NotasCreditos[index]
                };
            }
        });

        return selected;
    }

    function selecionarReducao(seq) {

        var selected = null

        $.each(ModelItem.ReducaoSuplementacao, function (index, value) {
            if (ModelItem.ReducaoSuplementacao[index].NrSequencia.toString() === seq.toString() && ModelItem.ReducaoSuplementacao[index].RedSup === "R") {
                selected = {
                    Index: index,
                    Object: ModelItem.ReducaoSuplementacao[index]
                };
            }
        });

        return selected;
    }
    function selecionarSuplementacao(seq) {

        var selected = null

        $.each(ModelItem.ReducaoSuplementacao, function (index, value) {
            if (ModelItem.ReducaoSuplementacao[index].NrSequencia.toString() === seq.toString() && ModelItem.ReducaoSuplementacao[index].RedSup === "S") {
                selected = {
                    Index: index,
                    Object: ModelItem.ReducaoSuplementacao[index]
                };
            }
        });

        return selected;
    }

    function atualizarCancelamentoEmMemoria(selected) {
        ModelItem.Cancelamento[selected.Index] = selected.Object;
    }
    function atualizarNotaDeCreditoEmMemoria(selected) {
        ModelItem.NotasCreditos[selected.Index] = selected.Object;
    }
    function atualizarDistribuicaoEmMemoria(selected) {
        ModelItem.Distribuicao[selected.Index] = selected.Object;
    }
    function atualizarReducaoEmMemoria(selected) {
        ModelItem.ReducaoSuplementacao[selected.Index] = selected.Object;
    }
    function atualizarSuplementacaoEmMemoria(selected) {
        ModelItem.ReducaoSuplementacao[selected.Index] = selected.Object;
    }


    // Gestao de Meses na Memória Início
    function salvarMesesMemoria(sequencia, tab, mesesRemovidos) {
        $.each($("div > #Valor"), function (index, element) {
            adicionarMes(ModelItem.Meses, sequencia, element, tab, mesesRemovidos);
        });
    }

    function adicionarMes(mesesMemoria, sequencia, element, tab, mesesRemovidos) {
        var desc = element.name.replace("Mes", "");
        var ug = $('#UnidadeGestoraEmitente').val();

        if (mesesRemovidos === undefined || mesesRemovidos === null) {
            mesesRemovidos = [];
        }

        if (element.value !== "") {
            var valorFloat = util.valorToDecimal(element.value);

            if (valorFloat > 0) {
                var mes = {};
                if (mesesRemovidos.length > 0) {
                    for (var i = 0; i < mesesRemovidos.length; i++) {
                        if (mesesRemovidos[i].NrSequencia === sequencia) {
                            mes = mesesRemovidos[i];
                        }
                    }
                    mes.ValorMes = valorFloat;
                    mes.NrSequencia = sequencia;
                }
                else {
                    mes = criarMes(desc, 0, 0, 0, 0, 0, 0, sequencia, ug, valorFloat, tab);
                }

                mesesMemoria[mesesMemoria.length] = mes;
            }
        }
    }

    function atualizarMeses(mesesMemoria, seq, tab) {
        $.each($("div > #Valor"), function (index, element) {
            var desc = element.name.replace("Mes", "");
            if (element.value !== "") {
                var valorFloat = util.valorToDecimal(element.value);

                var existe = false;
                for (var i = 0; i < mesesMemoria.length; i++) {
                    if (mesesMemoria[i].tab === tab && mesesMemoria[i].NrSequencia.toString() === seq.toString() && mesesMemoria[i].Descricao === desc) {
                        if (valorFloat > 0) {
                            mesesMemoria[i].ValorMes = valorFloat;
                        }
                        else {
                            removerUmMesMemoria(desc, seq, tab);
                        }
                        existe = true;
                    }
                    else {
                        existe = existe || false;
                    }
                }

                if (valorFloat > 0 && existe === false) {
                    adicionarMes(ModelItem.Meses, seq, element, tab);
                }
            }
        });
    }

    function criarMes(descricao, id, idCancelamento, idDistribuicao, idMovimentacao, idReducaoSuplementacao, nrAgrupamento, seq, ug, valor, tab) {
        var mes = {
            Descricao: descricao,
            Id: id,
            IdCancelamento: idCancelamento,
            IdDistribuicao: idDistribuicao,
            IdMovimentacao: idMovimentacao,
            IdReducaoSuplementacao: idReducaoSuplementacao,
            NrAgrupamento: nrAgrupamento,
            NrSequencia: seq,
            UnidadeGestora: ug,
            tab: tab,
            ValorMes: valor
        };

        return mes;
    }

    function limparMemoria(listaItems, seq, tab) {
        var indexesToRemove = [];
        var newSeq = 1;
        $.each(listaItems, function (index, value) {
            if (listaItems[index].NrSequencia.toString() === seq.toString()) {
                indexesToRemove.push(index);
            }
            else {
                listaItems[index].NrSequencia = newSeq;

                newSeq++
            }
        });

        for (var i = 0; i < indexesToRemove.length; i++) {
            listaItems.splice(indexesToRemove[i], 1);
        }

        if (tab !== null && tab !== undefined) {
            removerMesesMemoria(seq, tab);
        }
    }

    function limparMemoriaReducaoSuplementacao(seq, tab) {
        if (tab !== null && tab !== undefined) {
            var indexesToRemove = [];
            var newSeq = 1;

            $.each(ModelItem.ReducaoSuplementacao, function (index, value) {
                if (ModelItem.ReducaoSuplementacao[index].RedSup === tab) {
                    if (ModelItem.ReducaoSuplementacao[index].NrSequencia.toString() === seq.toString()) {
                        indexesToRemove.push(index);
                    }
                    else {
                        ModelItem.ReducaoSuplementacao[index].NrSequencia = newSeq++;
                    }
                }
            });

            for (var i = 0; i < indexesToRemove.length; i++) {
                ModelItem.ReducaoSuplementacao.splice(indexesToRemove[i], 1);
            }

            removerMesesMemoria(seq, tab);
        }
    }

    function removerMesesMemoria(seq, tab) {
        var mesesRemovidos = [];
        var indexesToRemove = [];
        var newSeq = 0;
        var seqAnterior = 0;

        for (var i = 0; i < ModelItem.Meses.length; i++) {
            if (ModelItem.Meses[i].tab === tab && ModelItem.Meses[i].NrSequencia.toString() === seq.toString()) {
                indexesToRemove.push(i);
            }
        }

        for (var i = 0; i < indexesToRemove.length; i++) {
            ModelItem.Meses.splice(indexesToRemove[i], 1);
            mesesRemovidos.push(ModelItem.Meses[i]);
        }

        for (var i = 0; i < ModelItem.Meses.length; i++) {
            if (ModelItem.Meses[i].tab === tab) {
                if (seqAnterior !== ModelItem.Meses[i].NrSequencia) {
                    seqAnterior = ModelItem.Meses[i].NrSequencia;
                    newSeq++
                }

                ModelItem.Meses[i].NrSequencia = newSeq;
            }
        }

        return mesesRemovidos;
    }
    function removerUmMesMemoria(descricao, seq, tab) {
        var mesesRemovidos = [];
        var indexesToRemove = [];
        for (var i = 0; i < ModelItem.Meses.length; i++) {
            if (ModelItem.Meses[i].Descricao === descricao && ModelItem.Meses[i].NrSequencia.toString() === seq.toString() && ModelItem.Meses[i].tab === tab) {
                indexesToRemove.push(i);
            }
        }

        for (var i = 0; i < indexesToRemove.length; i++) {
            mesesRemovidos.push(ModelItem.Meses[i]);
            ModelItem.Meses.splice(indexesToRemove[i], 1);
        }

        return mesesRemovidos;
    }
    // Gestao de Meses na Memória Fim


    // Movimentacao de Meses na tela Início
    function preencherMeses(meses, seq, tab) {
        if (meses !== null) {
            var mesesDoRegistro = [];
            for (var i = 0; i < meses.length; i++) {
                var mes = meses[i];
                if (mes.NrSequencia.toString() === seq.toString() && mes.tab === tab) {
                    mesesDoRegistro.push(mes);
                }
            }

            var allMeses = $('input[name^="Mes"]', '#frmPainelCadastrar');
            var totalQ1 = 0, totalQ2 = 0, totalQ3 = 0, totalQ4 = 0, total = 0;

            for (var i = 0; i < allMeses.length; i++) {
                $(allMeses[i]).val("R$ 0,00");
            }

            for (var j = 0; j < mesesDoRegistro.length; j++) {
                var mesSelecionado = mesesDoRegistro[j];
                var descMes = mesSelecionado.Descricao === null ? mesSelecionado.Descricao : mesSelecionado.Descricao;
                $('input[name="Mes' + descMes + '"]', '#frmPainelCadastrar').val("R$ " + MaskMonetario(mesSelecionado.ValorMes.toFixed(2)));

                if (parseInt(descMes) > 1) totalQ1 += 0;
                if (parseInt(descMes) > 2) totalQ1 += 0;
                if (parseInt(descMes) > 3) totalQ1 += 0;
                if (parseInt(descMes) > 4) totalQ2 += 0;
                if (parseInt(descMes) > 5) totalQ2 += 0;
                if (parseInt(descMes) > 6) totalQ2 += 0;
                if (parseInt(descMes) > 7) totalQ3 += 0;
                if (parseInt(descMes) > 8) totalQ3 += 0;
                if (parseInt(descMes) > 9) totalQ3 += 0;
                if (parseInt(descMes) > 10) totalQ4 += 0;
                if (parseInt(descMes) > 11) totalQ4 += 0;
                if (parseInt(descMes) > 12) totalQ4 += 0;

                if (parseInt(descMes) <= 3) {
                    totalQ1 += mesSelecionado.ValorMes;
                } else if (parseInt(descMes) > 3 && parseInt(descMes) <= 6) {
                    totalQ2 += mesSelecionado.ValorMes;
                } else if (parseInt(descMes) > 6 && parseInt(descMes) <= 9) {
                    totalQ3 += mesSelecionado.ValorMes;
                } else {
                    totalQ4 += mesSelecionado.ValorMes;
                }
            }

            total += totalQ1 + totalQ2 + totalQ3 + totalQ4;

            $('input[name="TotalQ1"]', '#frmPainelCadastrar').val("R$ " + MaskMonetario(totalQ1.toFixed(2)));
            $('input[name="TotalQ2"]', '#frmPainelCadastrar').val("R$ " + MaskMonetario(totalQ2.toFixed(2)));
            $('input[name="TotalQ3"]', '#frmPainelCadastrar').val("R$ " + MaskMonetario(totalQ3.toFixed(2)));
            $('input[name="TotalQ4"]', '#frmPainelCadastrar').val("R$ " + MaskMonetario(totalQ4.toFixed(2)));
            $('input[name="Total"]', '#frmPainelCadastrar').val("R$ " + MaskMonetario(total.toFixed(2)));
        }
    }

    function preencherMesesModal(meses, seq, tab) {
        if (meses !== null) {
            var mesesDoRegistro = [];
            for (var i = 0; i < meses.length; i++) {
                var mes = meses[i];
                if (mes.NrSequencia.toString() === seq.toString() && mes.tab === tab) {
                    mesesDoRegistro.push(mes);
                }
            }

            var allMeses = $('input[name^="Mes"]', '#modalDadosMovimentacao');
            var totalQ1Modal = 0, totalQ2Modal = 0, totalQ3Modal = 0, totalQ4Modal = 0, totalModal = 0;

            for (var i = 0; i < allMeses.length; i++) {
                $(allMeses[i]).val("R$ 0,00");
            }

            for (var j = 0; j < mesesDoRegistro.length; j++) {
                var mesSelecionado = mesesDoRegistro[j];
                var descMes = mesSelecionado.Descricao === null ? mesSelecionado.Descricao : mesSelecionado.Descricao;

                $('input[name="Mes' + mesSelecionado.Descricao + 'Modal"]', '#modalDadosMovimentacao').val("R$ " + MaskMonetario(mesSelecionado.ValorMes.toFixed(2)));

                if (parseInt(descMes) > 1) totalQ1Modal += 0;
                if (parseInt(descMes) > 2) totalQ1Modal += 0;
                if (parseInt(descMes) > 3) totalQ1Modal += 0;
                if (parseInt(descMes) > 4) totalQ2Modal += 0;
                if (parseInt(descMes) > 5) totalQ2Modal += 0;
                if (parseInt(descMes) > 6) totalQ2Modal += 0;
                if (parseInt(descMes) > 7) totalQ3Modal += 0;
                if (parseInt(descMes) > 8) totalQ3Modal += 0;
                if (parseInt(descMes) > 9) totalQ3Modal += 0;
                if (parseInt(descMes) > 10) totalQ4Modal += 0;
                if (parseInt(descMes) > 11) totalQ4Modal += 0;
                if (parseInt(descMes) > 12) totalQ4Modal += 0;

                if (parseInt(descMes) <= 3) {
                    totalQ1Modal += mesSelecionado.ValorMes;
                } else if (parseInt(descMes) > 3 && parseInt(descMes) <= 6) {
                    totalQ2Modal += mesSelecionado.ValorMes;
                } else if (parseInt(descMes) > 6 && parseInt(descMes) <= 9) {
                    totalQ3Modal += mesSelecionado.ValorMes;
                } else {
                    totalQ4Modal += mesSelecionado.ValorMes;
                }
            }

            totalModal += totalQ1Modal + totalQ2Modal + totalQ3Modal + totalQ4Modal;

            $('input[name="TotalQ1Modal"]', '#modalDadosMovimentacao').val("R$ " + MaskMonetario(totalQ1Modal.toFixed(2)));
            $('input[name="TotalQ2Modal"]', '#modalDadosMovimentacao').val("R$ " + MaskMonetario(totalQ2Modal.toFixed(2)));
            $('input[name="TotalQ3Modal"]', '#modalDadosMovimentacao').val("R$ " + MaskMonetario(totalQ3Modal.toFixed(2)));
            $('input[name="TotalQ4Modal"]', '#modalDadosMovimentacao').val("R$ " + MaskMonetario(totalQ4Modal.toFixed(2)));
            $('input[name="TotalModal"]', '#modalDadosMovimentacao').val("R$ " + MaskMonetario(totalModal.toFixed(2)));
        }
    }
    // Movimentacao de Meses na tela Fim


    function setAfterSaveButtons() {
        $('#divNew').show();
        $('#divSaveDistribuicaoSuplementacao').hide();
        $('#divSaveCancelamentoReducao').hide();
    }

    function CarregarTextArea(id, numberOfRows, value) {
        if ($("#" + id + "").length > 0) {
            var inputs = $("#" + id + " > .Proximo");

            if (value != null && value.length > 0) {
                var textos = value.split(";");
                for (var index = 0; index < numberOfRows; index++) {
                    if (textos.length > index) {
                        inputs[index].value = textos[index].trim();
                    }
                    else {
                        inputs[index].value = '';
                    }
                }
            }
        }
    }


    function reordenarTabela(table) {
        var trs = $(table).find('tbody tr');
        if (trs.length > 0) {
            trs.each(function (index, element) {
                var countTd = $(this).find('td').length;
                if (countTd > 1) {
                    table.dataTable().fnUpdate(index + 1, element, 0, false);
                }
            });
        }
    }


    function obterSequencia(itens) {
        return itens.length + 1;
    }
    function obterSequenciaReducaoSuplementacao(redsup) {
        var seq = 1;
        for (var i = 0; i < ModelItem.ReducaoSuplementacao.length; i++) {
            if (ModelItem.ReducaoSuplementacao[i].RedSup === redsup) {
                seq++;
            }
        }

        return seq;
    }
    
    function validarSalvar() {
        var valido = true;

        var preenchido = validarPreenchido("salvar");

        valido = preenchido;

        return valido;
    }

    function validarTransmitir() {
        var transmitirProdesp = Movimentacao.valueSelecaoProdesp.is(':checked');
        var transmitirSiafem = Movimentacao.valueSelecaoSiafem.is(':checked');
        var valido = false;

        var aDataCr = Movimentacao.TabelaCancelamentoReducao.DataTable().rows().data();
        var ttlCr = 0;
        for (var i = 0; i < aDataCr.length; i++) {
            var valor = util.valorToDecimal(aDataCr[i][8]);
            ttlCr += parseFloat(valor);
        }

        var aDataDs = Movimentacao.TabelaDistribuicaoSuplementacao.DataTable().rows().data();
        var ttlDs = 0;
        for (var i = 0; i < aDataDs.length; i++) {
            var valor = util.valorToDecimal(aDataDs[i][8]);
            ttlDs += parseFloat(valor);
        }

        valido = ttlCr.toFixed(2) === ttlDs.toFixed(2);

        var msg = "A soma dos valores de Cancelamento/Redução e Distribuição/Suplementação devem ser iguais";
        if (!transmitirProdesp) {
            msg = "A soma dos valores de Cancelamento e Distribuição devem ser iguais";
        }

        if (!transmitirProdesp) {
            msg = "A soma dos valores de Redução e Suplementação devem ser iguais";
        }

        if (!valido) {
            $.confirm({
                text: msg,
                title: "Valores incorretos!",
                cancel: function () {
                    return false;
                },
                cancelButton: "Ok",
                confirmButton: "",
                post: true,
                cancelButtonClass: "btn-default",
                modalOptionsBackdrop: true
            });
        }

        var preenchido = validarPreenchido("transmitir");

        valido = valido && preenchido;

        return valido;
    }

    function validarPreenchido(operacao) {
        var valido = false;

        var rowsCr = Movimentacao.TabelaCancelamentoReducao.find('tbody tr td:not(.dataTables_empty)').length
        var rowsNc = Movimentacao.TabelaNotaDeCredito.find('tbody tr td:not(.dataTables_empty)').length
        var rowsDs = Movimentacao.TabelaDistribuicaoSuplementacao.find('tbody tr td:not(.dataTables_empty)').length

        valido = (rowsCr + rowsNc + rowsDs) > 0;


        if (!valido) {
            $.confirm({
                text: "É preciso ter ao menos um documento para poder " + operacao + ".",
                title: "Sem documentos!",
                cancel: function () {
                    return false;
                },
                cancelButton: "Ok",
                confirmButton: "",
                post: true,
                cancelButtonClass: "btn-default",
                modalOptionsBackdrop: true
            });
        }

        return valido;
    }

    // soltos início
    $("div > #Valor").change(function () {
        SomarTotal();
        Movimentacao.SomarTotalTrimestre();
    });

    function criarEntidadeMemoria() {
        ModelItem.Id = $("#Codigo").val();
        ModelItem.TransmitirProdesp = $("#TransmitirProdesp").is(":checked");
        ModelItem.TransmitirSiafem = $("#TransmitirSiafem").is(":checked");
        ModelItem.IdTipoMovimentacao = $("#IdTipoMovimentacao").val();
        ModelItem.IdTipoDocumento = $("#IdTipoDocumento").val();
        ModelItem.UnidadeGestoraEmitente = $("#UnidadeGestoraEmitente").val();
        ModelItem.GestaoEmitente = $("#IdGestaoEmitente").val();
        ModelItem.AnoExercicio = $("#AnoExercicio").val();
        ModelItem.IdRegional = usuario.RegionalId;
        ModelItem.Programa = $("#Programa").val();
        ModelItem.IdFonte = $("#Fonte").val();
        ModelItem.Estrutura = $("#Natureza").val();
        ModelItem.CodigoAutorizadoAssinatura = $("#CodAssAutorizado").val();
        ModelItem.CodigoAutorizadoGrupo = $("#txtAutorizadoGrupo").val();
        ModelItem.CodigoAutorizadoOrgao = $("#txtAutorizadoOrgao").val();
        ModelItem.DescricaoAutorizadoCargo = $("#txtAutorizadoCargo").val();
        ModelItem.NomeAutorizadoAssinatura = $("#txtAutorizadoNome").val();
        ModelItem.CodigoExaminadoAssinatura = $("#CodAssExaminado").val();
        ModelItem.CodigoExaminadoGrupo = $("#txtExaminadoGrupo").val();
        ModelItem.CodigoExaminadoOrgao = $("#txtExaminadoOrgao").val();
        ModelItem.DescricaoExaminadoCargo = $("#txtExaminadoCargo").val();
        ModelItem.NomeExaminadoAssinatura = $("#txtExaminadoNome").val();
        ModelItem.CodigoResponsavelAssinatura = $("#CodAssResponsavel").val();
        ModelItem.CodigoResponsavelGrupo = $("#txtResponsavelGrupo").val();
        ModelItem.CodigoResponsavelOrgao = $("#txtResponsavelOrgao").val();
        ModelItem.DescricaoResponsavelCargo = $("#txtResponsavelCargo").val();
        ModelItem.NomeResponsavelAssinatura = $("#txtResponsavelNome").val();
    }
    // soltos fim

    function Transmitir() {

        if (navigator.onLine != true) {
            AbrirModal("Erro de conexão");
            return false;
        }

        criarEntidadeMemoria();

        Transmissao(JSON.stringify(modelSalvar), "Movimentacao");
    }

    $(document).on('ready', Movimentacao.init);

})(window, document, jQuery);