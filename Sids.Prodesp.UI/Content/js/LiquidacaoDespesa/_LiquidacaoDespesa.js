var tipoContrato = "";
var changedCombox = "N";
var tans = "";
var dadosEspecificacao = null;
var isLoad = false;
var isLoadRap = false;


(function (window, document, $) {
    'use strict';

    window.liquidacao = {};

    liquidacao.init = function () {

        isLoad = true;
        isLoadRap = true;

        liquidacao.cacheSelectors();

        $(".real").maskMoney({ prefix: "R$ ", allowNegative: false, thousands: ".", decimal: ",", affixesStay: true });
        $(".real").maskMoney('mask');

        $('#AnoExercicio').change(function () {
            programa = "0";
            GerarComboPtres();
            GerarComboCfp();
            GerarComboNatureza();
        });
        $("#Programa").change(function () {
            if (!isLoadRap) {
                GerarComboNatureza();
            }
        });


        if (liquidacao.controller === liquidacao.controllerRapInscricao || liquidacao.controller === liquidacao.controllerRapRequisicao || liquidacao.controller === liquidacao.controllerRapAnulacao) {
            if (isLoadRap && (programa === null || programa === 'undefined')) {
                GerarComboCfp();
                //GerarComboNatureza();
            }
        }
        else {
            if (isLoad) {
                GerarComboCfp();
                GerarComboNatureza();
            }
        }

        $('.descricaoObservacao').blur(function () {
            var obs1 = $('#DescricaoObservacao1');
            var obs2 = $('#DescricaoObservacao2');
            var obs3 = $('#DescricaoObservacao3');

            var obs = [obs1, obs2, obs3];
            var values = [];
            for (var i = 0; i < obs.length; i++) {
                if ($(obs[i]).val().trim() != "") {
                    values.push($(obs[i]).val());
                }
            }

            $('.descricaoObservacao').val('');

            for (var i = 0; i < values.length; i++) {
                $('#DescricaoObservacao' + (i + 1)).val(values[i]);
            }

        });


        $("#NumeroMedicao").change(function () {
            this.value = ("000" + this.value).slice(-3)
        });

        if (ModelItem.TransmitidoProdesp == true)
            $(".lockProdesp").attr("disabled", true);

        if (ModelItem.TransmitidoSiafem == true)
            $(".lockSiafem").attr("disabled", true);

        if (ModelItem.TransmitidoSiafisico == true)
            $(".lockSiafisico").attr("disabled", true);

        if ($('#NumeroProdesp').val().length === 0) {
            $('.lockProdesp').removeAttr('disabled');
        }

        if (ModelItem.TransmitidoSiafem == true || ModelItem.TransmitidoSiafisico == true) {
            $("#transmitirSIAFEM").attr("disabled", true);
            $("#transmitirSIAFISICO").attr("disabled", true);
        }


        $("#Orgao").attr("ReadOnly", usuario.RegionalId != 1);
        $('#Orgao > option:not(:selected)').attr('disabled', usuario.RegionalId != 1);





        liquidacao.filter(liquidacao.valueCenarioSiafemSiafisico.val());
        liquidacao.provider();
        liquidacao.scenaryFactory();

        if (liquidacao.controller === liquidacao.controllerInclusao) {
            tipoContrato = 2;
            liquidacao.displayHandlerValorRealizado(); // função para copiar valorRealizado para o campo valor do evento
        }
        if (liquidacao.controller === liquidacao.controllerAnulacao) {
            tipoContrato = 3;
            liquidacao.displayHandlerValorAnulado();
        }
        if (liquidacao.controller === liquidacao.controllerRapAnulacao) {
            tipoContrato = 4;
        }

        $("#NumeroRequisicaoRap").addClass("RapAnulacao");

        IniciarCreateEdit(liquidacao.controller);
        if (liquidacao.controller === "RapAnulacao")
            liquidacao.SaldoAnulacao();

        isLoadRap = false;
        isLoad = false;

        //liquidacao.displayHandlerChange();
    }

    liquidacao.arraySort = function (a, b) {
        var arel = parseInt($(a).attr('value')) || 0;
        var brel = parseInt($(b).attr('value')) || 0;
        return arel == brel ? 0 : arel < brel ? -1 : 1;
    }

    liquidacao.arrayMap = function () {
        return {
            "value": this.value,
            "option": "<option value='" + this.value + "'>" + this.text + "</option>"
        };
    }

    liquidacao.cacheSelectors = function () {
        liquidacao.body = $('body');
        liquidacao.controller = window.controller;

        liquidacao.controllerInclusao = 'Subempenho';
        liquidacao.controllerAnulacao = 'SubempenhoCancelamento';
        liquidacao.controllerRapInscricao = 'RapInscricao';
        liquidacao.controllerRapRequisicao = 'RapRequisicao';
        liquidacao.controllerRapAnulacao = 'RapAnulacao';

        liquidacao.prodespSubEmpenho = 'SubEmpenho';
        liquidacao.prodespSubEmpenhoContrato = 'SubEmpenhoContrato';
        liquidacao.prodespSubEmpenhoRecibo = 'SubEmpenhoRecibo';
        liquidacao.prodespSubEmpenhoOrganizacao7 = 'SubEmpenhoOrganizacao7';


        liquidacao.prodespRAPContrato = 'RAPContrato';
        liquidacao.prodespRAPRecibo = 'RAPRecibo';
        liquidacao.prodespSubRAPOraganizao7 = 'RAPOraganizao7';
        liquidacao.prodespSubEmpenhoRAPSimples = 'RAPSimples';


        liquidacao.prodespRAPSemContrato = 'SemContrato';
        liquidacao.prodespRAPComContrato = 'ComContrato';

        liquidacao.valueSelecaoSiafemSiafisico = $('#transmitirSIAFEM');
        liquidacao.valueCenarioSiafemSiafisico = $('#CenarioSiafemSiafisico');
        liquidacao.valueSelecaoProdesp = $('#transmitirProdesp');
        liquidacao.valueCenarioProdesp = $('#CenarioProdesp');
        liquidacao.partialDadoSaldoValorAnulacao = $('#DadoSaldoValorAnulacao');
        liquidacao.partialPesquisaReservaContrato = $('#PesquisaReservaContrato');
        liquidacao.partialPesquisaEmpenhoCredor = $('#PesquisaEmpenhoCredor');
        liquidacao.partialPesquisaTipoApropriacao = $('#PesquisaTipoApropriacao');
        liquidacao.partialDadoApropriacao = $('#DadoApropriacao');

        liquidacao.partialPesquisaSaldoRap = $('#PesquisaSaldoRap');
        liquidacao.partialDadoInscricao = $('#DadoInscricao');
        liquidacao.partialDadoRequisicaoRap = $('#DadoRequisicaoRap');
        liquidacao.partialSubempenhoInscritoRap = $('#SubempenhoInscritoRap');


        liquidacao.partialDadoLiquidacaoEvento = $('#DadoLiquidacaoEvento');
        liquidacao.partialDadoLiquidacaoEventoGrid = $('#DadoLiquidacaoEventoGrid');
        liquidacao.partialDadoObservacao = $('#DadoObservacao');
        liquidacao.partialNotaLiquidacao = $('#NotaLiquidacao');
        liquidacao.partialDadoLiquidacaoItem = $('#DadoLiquidacaoItem');
        liquidacao.partialDadoLiquidacaoItemGrid = $('#DadoLiquidacaoItemGrid');
        liquidacao.partialDadoReferencia = $('#DadoReferencia');
        liquidacao.partialDadoDataVencimento = $('#divDataVencimento');
        liquidacao.partialDadoAssinatura = $('#DadoAssinatura');
        liquidacao.partialDadoDespesa = $('#DadoDespesa');
        liquidacao.partialDadoCaucao = $('#DadoCaucao');
        liquidacao.nlReferencia = $('#NlReferencia');
        liquidacao.numeroNe = $('#NumeroOriginalSiafemSiafisico');
        liquidacao.numeroCt = $('#NumeroCT');

        liquidacao.divMensagemDiaAnterior = $('#divMensagemDiaAnterior');
        liquidacao.divMensagemTotalLiquidada = $('#divMensagemTotalLiquidada');

        liquidacao.valorRealizado = $('#ValorRealizado');

        liquidacao.optarray = liquidacao.valueCenarioSiafemSiafisico
            .children('option')
            .map(liquidacao.arrayMap);




        $('#QuantidadeMaterialServico').focus(function () {
            $('#QuantidadeMaterialServico').val('');
            $('#QuantidadeMaterialServicoDecimal').val('000');
        });
        $('#QuantidadeMaterialServico').keydown(function (e) {
            switch (e.keyCode) {
                case 8:  // Backspace
                case 9:  // Tab
                case 13: // Enter
                case 37: // Left
                case 38: // Up
                case 39: // Right
                case 40: // Down
                    break;

                default:
                    if (!((e.keyCode >= 48 && e.keyCode <= 57) || (e.keyCode >= 96 && e.keyCode <= 105))) {
                        return false;
                    }
                    if ($(this).val().length >= 9) {
                        $('#QuantidadeMaterialServicoDecimal').val('');
                        $('#QuantidadeMaterialServicoDecimal').focus();
                    } else {
                        $('#QuantidadeMaterialServicoDecimal').val('000');
                    }
            }
        });





        //liquidacao.adicionaZeros = function (e) {
        //    //function adiciona zeros a direita utilizado em subempenho
        //    function pad(num, size) {
        //        if (num !== "" || num !== undefined || num !== null) {
        //            var s = num + "";
        //            while (s.length < size) s = s + "0";
        //            return s;
        //        }
        //    }

        //    $('#QuantidadeMaterialServico').focus(function () {
        //        $('#QuantidadeMaterialServico').val('');
        //        $('#QuantidadeMaterialServicoDecimal').val('000');
        //    });
        //    $('#QuantidadeMaterialServico').keydown(function (e) {
        //        switch (e.keyCode) {
        //            case 8:  // Backspace
        //            case 9:  // Tab
        //            case 13: // Enter
        //            case 37: // Left
        //            case 38: // Up
        //            case 39: // Right
        //            case 40: // Down
        //                break;

        //            default:
        //                if (!((e.keyCode >= 48 && e.keyCode <= 57) || (e.keyCode >= 96 && e.keyCode <= 105))) {
        //                    return false;
        //                }
        //                if ($(this).val().length >= 6) {
        //                    $('#QuantidadeMaterialServicoDecimal').val('');
        //                    $('#QuantidadeMaterialServicoDecimal').focus();
        //                } else {
        //                    $('#QuantidadeMaterialServicoDecimal').val('000');
        //                }
        //        }
        //    });
        //}

        liquidacao.optarray.sort(liquidacao.arraySort);

        liquidacao.displayHandlerButton = function (e) { // chama no click  botão de consulta
            changedCombox = "N";
            if (liquidacao.valueCenarioProdesp.val() !== "") {
                liquidacao.scenaryFactory();
            }
        }

        liquidacao.displayHandlerChange = function (e) { // chama no change da combo
            // e.preventDefault();  

            if (liquidacao.valueCenarioSiafemSiafisico.val() === "") {
                liquidacao.reset();
            }

            if (liquidacao.valueCenarioProdesp.val() !== "" || liquidacao.valueSelecaoProdesp.is(':checked') === false) { //quando há valores de retorno de consulta
                changedCombox = "N";
                liquidacao.scenaryFactory();
            }


            liquidacao.nlReferencia.val('');
            liquidacao.numeroNe.val('');
            liquidacao.numeroCt.val('');

            liquidacao.permutarCenarios();

            liquidacao.checkTransmitirReadOnly();

            liquidacaoItem.reset();
        }

        liquidacao.permutarCenarios = function () {
            liquidacao.body
                .off('blur', '#' + liquidacao.numeroNe.prop('id'), liquidacao.displayHandlerNumeroNEBlur)
                .off('blur', '#' + liquidacao.numeroCt.prop('id'), liquidacao.displayHandlerNumeroCTBlur)
                .off('blur', '#' + liquidacao.nlReferencia.prop('id'), liquidacao.displayHandlerNlReferenciaBlur);

            if (liquidacao.cenarioContem([10, 11])) {
                liquidacao.body
                    .off('blur', '#' + liquidacao.numeroNe.prop('id'), liquidacao.displayHandlerNumeroNEBlur)
                    .off('blur', '#' + liquidacao.numeroCt.prop('id'), liquidacao.displayHandlerNumeroCTBlur)
                    .on('blur', '#' + liquidacao.nlReferencia.prop('id'), liquidacao.displayHandlerNlReferenciaBlur);
            }
            else if (liquidacao.cenarioContem([1, 2, 3, 9])) {
                liquidacao.body
                    .on('blur', '#' + liquidacao.numeroNe.prop('id'), liquidacao.displayHandlerNumeroNEBlur)
                    .on('blur', '#' + liquidacao.numeroCt.prop('id'), liquidacao.displayHandlerNumeroCTBlur)
                    .off('blur', '#' + liquidacao.nlReferencia.prop('id'), liquidacao.displayHandlerNlReferenciaBlur);
            }
            else if (liquidacao.cenarioContem([7])) {
                liquidacao.displayHandlerValorAnular();
                $("#Valor").attr("disabled", false);
            }
            

            if (liquidacao.cenarioContem([10])) {
                liquidacao.divMensagemDiaAnterior.addClass('hidden').hide();
                liquidacao.divMensagemTotalLiquidada.removeClass('hidden').show();
            }
            else {
                liquidacao.divMensagemDiaAnterior.removeClass('hidden').show();
                liquidacao.divMensagemTotalLiquidada.addClass('hidden').hide();
            }
        }

        liquidacao.displayHandlerCheckBox = function (e) { // chama no checkedBox
            e.preventDefault();
            liquidacao.scenaryFactory();
        }

        liquidacao.disableHandler = function (e) {
            e.preventDefault();
            liquidacao.diableSelecaoProdesp();
        }

        liquidacao.diableSelecaoProdesp = function () {
            if (liquidacao.valueCenarioSiafemSiafisico.val() === "4" ||
                liquidacao.valueCenarioSiafemSiafisico.val() === "5" ||
                liquidacao.valueCenarioSiafemSiafisico.val() === "8") {
                liquidacao.valueSelecaoProdesp.removeAttr('checked');
                liquidacao.valueSelecaoProdesp.attr("disabled", "disabled");
            } else {
                if (ModelItem.TransmitidoProdesp == false)
                    liquidacao.valueSelecaoProdesp.removeAttr("disabled");
            }
        }

        liquidacao.filterHandler = function (e) {
            e.preventDefault();
            liquidacao.filter();
        }


        liquidacao.displayHandlerEspDespesa = function (e) {
            e.preventDefault();
            var valor = $("div >#CodigoEspecificacaoDespesa").val();

            if ((valor != '000') && (valor != '')) {
                EspecificacaoDespesa(valor);
            }

        }

        liquidacao.displayHandlerNotaFiscal = function (e) {
            e.preventDefault();
            if (liquidacao.prodespSubEmpenhoRecibo === "SubEmpenhoRecibo" ||
                liquidacao.prodespSubEmpenhoContrato === "SubEmpenhoContrato") {
                var valor = "NOTA FISCAL: " + $("#CodigoNotaFiscalProdesp").val();
                $("#DescricaoEspecificacaoDespesa3").val(valor);
            }
        }

        liquidacao.displayHandlerValorRealizado = function (e) {
            if (ModelItem.TransmitidoProdesp == false && $('#ValorUnitario').val() != undefined) {
                $('#ValorUnitario').val($('#ValorRealizado').val());
                $('#Valor').val($('#ValorRealizado').val());
            }
        }

        liquidacao.displayHandlerValorAnular = function (e) {
            var isNlObras = liquidacao.cenarioContem([7]);
            var naoFoiTransmitido = ModelItem.TransmitidoProdesp == false;
            if (naoFoiTransmitido && $('#ValorUnitario').val() != undefined) {
                if(isNlObras){
                    $('#Valor').val($('#ValorAnular').val());
                }
                else{                    
                    $('#ValorUnitario').val($('#ValorAnular').val());
                }
            }
        }

        liquidacao.displayHandlerValorAnulado = function (e) {
            if (ModelItem.TransmitidoProdesp == false && $('#ValorAnulado').val() != undefined) {
                $('#ValorAnulado').val($('#Valor').val());
                liquidacao.SaldoAnulacao();
            }
        }

        liquidacao.displayHandlerNumeroMedicao = function (e) {
            e.preventDefault();

            var naturezaTp = liquidacao.VerificaNatureza();

            var valor = "PARA ATENDER AS DESPESAS DE " + naturezaTp + " DA MEDICAO: " + $("#NumeroMedicao").val();
            $("#DescricaoEspecificacaoDespesa1").val(valor);
        }

        liquidacao.displayHandlerNaturezaSubempenho = function (e) {
            e.preventDefault();

            var naturezaTp = liquidacao.VerificaNatureza();

            var valor = "PARA ATENDER AS DESPESAS DE " + naturezaTp + " DA MEDICAO: " + $("#NumeroMedicao").val();
            $("#DescricaoEspecificacaoDespesa1").val(valor);

        }

        liquidacao.VerificaNatureza = function (e) {
            var tpNatureza;

            tpNatureza = $("#NaturezaSubempenhoId").val();
            switch (tpNatureza) {
                case "":
                    return "";
                case "1":
                    return "RECALCULO";
                case "2":
                    return "CM SERV/REAJ";
                case "3":
                    return "CM RECALCULO";
                case "4":
                    return "JUROS SERV/REAJ";
                case "5":
                    return "JUROS RECALCULO";
                case "6":
                    return "RECALCULO REPACT";
                case "R":
                    return "REAJUSTE";
                case "RC":
                    return "RECALCULO";
                case "S":
                    return "SERVIÇO";
                default:
                    return "";
            }

            //return tpNatureza;
        }

        liquidacao.displayHandlerReferencia = function (e) {
            e.preventDefault();
            $('#Referencia').blur(function (e) {
                $("#ReferenciaDigitada").val('true');
            });
        }

        liquidacao.displayHandlerNumeroNEBlur = function () {
            ConsultarCtPorNe($(this), function (response) {
                $('.clearFields').val('');
                liquidacao.preencherItens(response);
            });
        }

        liquidacao.displayHandlerNumeroCTBlur = function () {
            ConsultarCtPorCt($(this), function (response) {
                $('.clearFields').val('');
                liquidacao.preencherItens(response);
            });
        }

        liquidacao.displayHandlerNlReferenciaBlur = function () {
            ConsultarNl($(this), function (response) {
                $('.clearFields').val('');
                liquidacao.numeroNe.val(response.Ct.ContratoEmpenhado);
                liquidacao.preencherItens(response);
            });
        }


    }

    liquidacao.filter = function (scenary) {
        liquidacao.valueCenarioSiafemSiafisico.children('option').remove();

        var addoptarr = [];
        var i = 0;

        for (i = 0; i < liquidacao.optarray.length; i++) {
            if (i === 0) {
                addoptarr.push(liquidacao.optarray[i].option);
            }
            else if ((liquidacao.valueSelecaoSiafemSiafisico.is(':checked') === true && (i > 5 && i < 9))) {
                addoptarr.push(liquidacao.optarray[i].option);
            }
            else if (liquidacao.controller === liquidacao.controllerInclusao) {
                if ((liquidacao.valueSelecaoSiafemSiafisico.is(':checked') === false && i < 6)) {
                    addoptarr.push(liquidacao.optarray[i].option);
                }
            }
            else if (liquidacao.controller === liquidacao.controllerAnulacao) {
                if ((liquidacao.valueSelecaoSiafemSiafisico.is(':checked') === false && (i === 4 || i === 5 || i > 8))) {
                    addoptarr.push(liquidacao.optarray[i].option);
                }
            }
        }

        $.each(addoptarr, function (i, item) {
            liquidacao.valueCenarioSiafemSiafisico.append(item);
        });

        liquidacao.valueCenarioSiafemSiafisico.val(scenary);
    }

    liquidacao.reset = function () {
        liquidacao.partialDadoApropriacao.hide();
        liquidacao.partialDadoLiquidacaoEvento.hide();
        liquidacao.partialDadoLiquidacaoEventoGrid.hide();
        liquidacao.partialDadoObservacao.hide();
        liquidacao.partialNotaLiquidacao.hide();
        liquidacao.partialDadoLiquidacaoItem.hide();
        liquidacao.partialDadoLiquidacaoItemGrid.hide();
        liquidacao.partialDadoReferencia.hide();
        liquidacao.partialDadoDataVencimento.hide();
        liquidacao.partialDadoAssinatura.hide();
        liquidacao.partialDadoDespesa.hide();
        liquidacao.partialDadoCaucao.hide();
        liquidacao.partialPesquisaSaldoRap.hide();
        liquidacao.partialDadoInscricao.hide();
        liquidacao.partialDadoRequisicaoRap.hide();
        liquidacao.partialSubempenhoInscritoRap.hide();
        liquidacao.valueCenarioProdesp.val(''); // limpa o retorno do webservice para o cenario
    }

    liquidacao.scenaryFactory = function () {
        liquidacao.scenaryFactoryForPesquisaReservaContrato();
        liquidacao.scenaryFactoryForPesquisaEmpenhoCredor();
        liquidacao.scenaryFactoryForPesquisaTipoApropriacao();
        liquidacao.scenaryFactoryForDadoApropriacao();
        liquidacao.scenaryFactoryForDadoLiquidacaoEvento();
        liquidacao.scenaryFactoryForDadoLiquidacaoEventoGrid();
        liquidacao.scenaryFactoryForDadoObservacao();
        liquidacao.scenaryFactoryForNotaLiquidacao();
        liquidacao.scenaryFactoryForDadoLiquidacaoItem();
        liquidacao.scenaryFactoryForDadoLiquidacaoItemGrid();
        liquidacao.scenaryFactoryForDadoReferencia();
        liquidacao.scenaryFactoryForDadoDataVencimento();
        liquidacao.scenaryFactoryForDadoDespesa();
        liquidacao.scenaryFactoryForDadoAssinatura();
        liquidacao.scenaryFactoryForDadoCaucao();
        liquidacao.scenaryFactoryForDadoInscricao();
        liquidacao.scenaryFactoryForPesquisaSaldoRap();
        liquidacao.scenaryFactoryForDadoRequisicaoRap();
        liquidacao.scenaryFactoryForPesquisaSubempenhoInscritoRap();
        liquidacao.scenaryFactoryForDadoSaldoValorAnulacao();
        liquidacao.scenaryFactoryButtonTransmitir();
        liquidacao.filterTipoEventoList();
        liquidacao.diableSelecaoProdesp();
        liquidacao.disableNlReferencia();
    }

    liquidacao.disableNlReferencia = function () {
        if (liquidacao.valueCenarioSiafemSiafisico.val() === "10" ||
            liquidacao.valueCenarioSiafemSiafisico.val() === "11") {
            liquidacao.nlReferencia.closest('.blocopai').show();
        } else {
            liquidacao.nlReferencia.closest('.blocopai').hide();
        }
    }


    liquidacao.scenaryFactoryForDadoSaldoValorAnulacao = function () {

        liquidacao.partialDadoSaldoValorAnulacao.hide();

        if (liquidacao.controller === liquidacao.controllerRapAnulacao && liquidacao.valueCenarioProdesp.val() !== "" && liquidacao.valueSelecaoProdesp.is(':checked') === true) {
            liquidacao.partialDadoSaldoValorAnulacao.show();
        }
    }

    liquidacao.scenaryFactoryForPesquisaSubempenhoInscritoRap = function () {
        if (liquidacao.controller === liquidacao.controllerRapRequisicao) {
            liquidacao.partialSubempenhoInscritoRap.show();
            $('#divbtnConsultar').show();
        }
    }

    liquidacao.scenaryFactoryButtonTransmitir = function () {
        if (liquidacao.controller === liquidacao.controllerInclusao ||
            liquidacao.controller === liquidacao.controllerAnulacao) {
            if (liquidacao.valueCenarioProdesp.val() != null && liquidacao.valueCenarioProdesp.val() !== "" ||
                liquidacao.valueSelecaoProdesp.is(':checked') === false) {
                $("#btnTransmitir").removeAttr("disabled");
            } else {
                $("#btnTransmitir").attr("disabled", "disabled");
            }
        }
    }

    liquidacao.scenaryFactoryForDadoRequisicaoRap = function () {
        if ((liquidacao.controller === liquidacao.controllerRapRequisicao) ||
            (liquidacao.controller === liquidacao.controllerRapAnulacao)) {
            $('#divNumeroOriginalProdesp').hide();
            $('#divNumeroOriginalSiafemSiafisico').hide();
            $('#divNumeroContrato').hide();
            $('#divDataRealizado').hide();
            $('#divCodigoNotaFiscalProdesp').hide();
            $('#divCodigoDespesa').hide();
            $('#divCodigoTarefa').hide();
            $('#divTarefa').hide();
            $('#divCodigoUnidadeGestora').hide();
            $('#divCodigoGestao').hide();
            $('#divDataEmissao').hide();
            $('#divNumeroMedicao').hide();
            $('#divValor').hide();
            $('#divValorRealizado').hide();
            $('#divValorSubempenhar').hide();
            $('#divDescricaoPrazoPagamento').hide();
            $('#divClassificacao').hide();
            $('#divNumeroCNPJCPFCredor').hide();
            $('#divCodigoGestaoCredor').hide();
            $('#divCodigoAplicacaoObra').hide();
            $('#divAnoMedicao').hide();
            $('#divMesMedicao').hide();
            $('#divRegional').hide(); // órgão
            $('#divPrograma').hide(); //CFP
            $('#divNatureza').hide(); //CED
            $('#divAnoExercicio').hide(); //Ano
            $('#divCodigoCredorOrganizacao').hide();
            $('#divNumeroCNPJCPFFornecedor').hide();
            $('#divNaturezaSubempenho').hide();

            liquidacao.partialDadoRequisicaoRap.hide();
        }

        if (liquidacao.controller === liquidacao.controllerRapAnulacao && liquidacao.valueCenarioProdesp.val() !== "" && (liquidacao.valueSelecaoSiafemSiafisico.is(':checked') === true)) {
            liquidacao.partialDadoRequisicaoRap.show();
            $('#divCodigoDespesa').show();
            $('#divCodigoTarefa').show();
            $('#divCodigoAplicacaoObra').show();
            $('#divPrograma').show(); //CFP
            $('#divNatureza').show(); //CED
            $('#divNumeroCNPJCPFCredor').show();
            $('#divCodigoGestaoCredor').show();
            $('#divNumeroOriginalSiafemSiafisico').show();
            $('#divCodigoUnidadeGestora').show();
            $('#divCodigoGestao').show();
            $('#divValor').show();
            $('#divDataEmissao').show();
            $('#divClassificacao').show();
            $('#divAnoMedicao').show();
            $('#divMesMedicao').show();
            $('#divTipoServicoId').show(); //Tipo
            $('#divRegional :input').attr("disabled", true); // órgão
            $('#divPrograma :input').attr("disabled", true); //CFP
            $('#divNatureza :input').attr("disabled", true); //CED
            $('#divAnoExercicio :input').attr("disabled", true); //Ano
        }

        if ((liquidacao.controller === liquidacao.controllerRapRequisicao)) {

            if ($('#Programa :selected').text().substr(12, 1) === "2") {
                $('#divTarefa').show();
            }

            if (liquidacao.valueCenarioProdesp.val() === liquidacao.prodespSubEmpenhoRAPSimples)
                $(".referencia").mask("2000NE00000 N.F.000000       ", {
                    placeholder: "2___NE_____ N.F._____________"
                });
            if (liquidacao.valueCenarioProdesp.val() === liquidacao.prodespRAPContrato)
                $(".referencia").mask("2000NE00000 N.F.000000       ", {
                    placeholder: "2___NE_____ N.F._____________"
                });
            if (liquidacao.valueCenarioProdesp.val() === liquidacao.prodespRAPRecibo)
                $(".referencia").mask("2000NE00000 N.F.000000-00/00 ", {
                    placeholder: "2___NE_____ N.F.______-__/__ "
                });
            if (liquidacao.valueCenarioProdesp.val() === liquidacao.prodespSubRAPOraganizao7)
                $(".referencia").mask("2000NE00000 N.F.000000       ", {
                    placeholder: "2___NE_____ N.F._____________"
                });


            if (liquidacao.valueCenarioProdesp.val() !== "") {
                liquidacao.partialDadoRequisicaoRap.show();
                $('#divAnoMedicao').show();
                $('#divMesMedicao').show();
                $('#divRegional').show(); // órgão
                $('#divPrograma').show(); //CFP
                $('#divNatureza').show(); //CED
                $('#divAnoExercicio').show(); //Ano
                $('#divTipoServicoId').show(); //Tipo

                $('#divRegional :input').attr("disabled", true); // órgão
                $('#divPrograma :input').attr("disabled", true); //CFP
                $('#divNatureza :input').attr("disabled", true); //CED
                $('#divAnoExercicio :input').attr("disabled", true); //Ano

            }



            if (liquidacao.valueCenarioProdesp.val() === liquidacao.prodespSubEmpenhoRAPSimples) {
                $('#divNumeroOriginalProdesp').show();
                $('#divNumeroOriginalSiafemSiafisico').show();
                $('#divNumeroContrato').show();
                $('#divCodigoAplicacaoObra').show();
                $('#divCodigoUnidadeGestora').show();
                $('#divCodigoGestao').show();
                $('#divDataEmissao').show();
                $('#divValor').show();
                $('#divCodigoDespesa').show();
                $('#divCodigoTarefa').show();
                $('#divValorRealizado').show();
                $('#divDescricaoPrazoPagamento').show();
                $('#divDataRealizado').show();
                $('#divClassificacao').show();
                $('#divNumeroCNPJCPFCredor').show();
                $('#divCodigoGestaoCredor').show();
            }

            if (liquidacao.valueCenarioProdesp.val() === liquidacao.prodespRAPContrato) {
                $('#divNumeroOriginalProdesp').show();
                $('#divNumeroOriginalSiafemSiafisico').show();
                $('#divNumeroContrato').show();
                $('#divCodigoAplicacaoObra').show();

                $('#divCodigoUnidadeGestora').show();
                $('#divCodigoGestao').show();
                $('#divValor').show();
                $('#divCodigoNotaFiscalProdesp').show();
                $('#divNumeroMedicao').show();
                $('#NumeroMedicao').prop("disabled", false);     // habilita para este cenário
                $('#NumeroMedicao').prop("readonly", false);

                $('#divNaturezaSubempenho').show();
                $('#NaturezaSubempenhoId').prop("disabled", false);  // habilita para este cenário
                $('#divDataEmissao').show();
                $('#divCodigoDespesa').show();
                $('#divCodigoTarefa').show();
                $('#divValorRealizado').show();
                $('#divDescricaoPrazoPagamento').show();
                $('#divDataRealizado').show();
                $('#divClassificacao').show();
                $('#divNumeroCNPJCPFCredor').show();
                $('#divCodigoGestaoCredor').show();
            }

            if (liquidacao.valueCenarioProdesp.val() === liquidacao.prodespRAPRecibo) {
                $('#divNumeroOriginalProdesp').show();
                $('#divNumeroOriginalSiafemSiafisico').show();
                $('#divNumeroContrato').show();
                $('#divCodigoAplicacaoObra').show();

                $('#divCodigoUnidadeGestora').show();
                $('#divCodigoGestao').show();
                $('#divValor').show();
                $('#divCodigoNotaFiscalProdesp').show();
                $('#divNaturezaSubempenho').show();
                $('#divNumeroMedicao').show();

                IsAttr($('#NaturezaSubempenhoId'), "", "disabled");
                IsAttr($('#NumeroMedicao'), "", "Readonly");

                $('#divDataRealizado').show();

                $('#divDataEmissao').show();
                $('#divCodigoDespesa').show();
                $('#divCodigoTarefa').show();
                $('#divValorRealizado').show();
                $('#divClassificacao').show();
                $('#divNumeroCNPJCPFCredor').show();
                $('#divCodigoGestaoCredor').show();
                $('#divAnoMedicao').show();
                $('#divMesMedicao').show();
            }

            if (liquidacao.valueCenarioProdesp.val() === liquidacao.prodespSubRAPOraganizao7) {
                $('#divNumeroOriginalProdesp').show();
                $('#divNumeroOriginalSiafemSiafisico').show();
                $('#divNumeroContrato').show();
                $('#divCodigoAplicacaoObra').show();

                $('#divCodigoUnidadeGestora').show();
                $('#divCodigoGestao').show();
                $('#divValor').show();
                $('#divDataEmissao').show();

                $('#divCodigoDespesa').show();
                $('#divCodigoTarefa').show();
                $('#divValorRealizado').show();
                $('#divDescricaoPrazoPagamento').show();
                $('#divDataRealizado').show();
                $('#divNumeroMedicao').show();
                $('#divClassificacao').show();
                $('#divNumeroCNPJCPFCredor').show();
                $('#divCodigoGestaoCredor').show();
                $('#divAnoMedicao').show();
                $('#divMesMedicao').show();
                $('#divCodigoCredorOrganizacao').show();
                $('#divNumeroCNPJCPFFornecedor').show();
            }
        }
    }

    liquidacao.scenaryFactoryForPesquisaReservaContrato = function () {
        liquidacao.partialPesquisaReservaContrato.show();
    }

    liquidacao.scenaryFactoryForPesquisaEmpenhoCredor = function () {
        liquidacao.partialPesquisaEmpenhoCredor.show();
    }

    liquidacao.scenaryFactoryForPesquisaTipoApropriacao = function () {
        liquidacao.partialPesquisaTipoApropriacao.hide();

        $('#divNumeroEmpenhoProdesp').hide();
        $('#divNumeroSubempenhoProdesp').hide(); // cancelamento
        $('#divValorRealizado').hide();
        $('#divCodigoTarefa').hide();
        $('#divCodigoDespesa').hide();
        $('#divNumeroRecibo').hide();
        $('#divPrazoPagamento').hide();
        $('#divOU').hide();
        $('#divDataRealizado').hide();
        $('#divbtnConsultar').hide();

        if (liquidacao.valueSelecaoProdesp.is(':checked') === true && liquidacao.controller === liquidacao.controllerInclusao && liquidacao.valueCenarioSiafemSiafisico.val() !== "8") {
            if (liquidacao.controller === liquidacao.controllerInclusao) {
                liquidacao.partialPesquisaTipoApropriacao.show();

                $('#divNumeroEmpenhoProdesp').show();
                $('#divValorRealizado').show();
                $('#divCodigoTarefa').show();
                $('#divCodigoDespesa').show();
                $('#divNumeroRecibo').show();
                $('#divPrazoPagamento').show();
                $('#divOU').show();
                $('#divDataRealizado').show();
                $('#divbtnConsultar').show();

            }

            if (liquidacao.valueCenarioProdesp.val() === liquidacao.prodespSubEmpenhoRecibo && liquidacao.valueCenarioSiafemSiafisico.val() !== "7") {
                $('#divCodigoTarefa').show();
            }
        }

        else if (liquidacao.valueSelecaoProdesp.is(':checked') === true && liquidacao.controller === liquidacao.controllerAnulacao) {
            liquidacao.partialPesquisaTipoApropriacao.show();
            $('#divNumeroSubempenhoProdesp').show();
            $('#divValorRealizado').show();
            $('#divbtnConsultar').show();
        }
        if (liquidacao.controller === liquidacao.controllerRapInscricao) {

            liquidacao.partialDadoApropriacao.show();
            liquidacao.partialPesquisaSaldoRap.show();
            liquidacao.partialDadoInscricao.show();

            $('#divbtnConsultar').show();
        }

    }

    liquidacao.scenaryFactoryForDadoApropriacao = function () {
        liquidacao.partialDadoApropriacao.hide();

        $('#divNumeroOriginalSiafemSiafisico').hide();
        $('#divNumeroCT').hide();
        $('#divCodigoGestaoCredor').hide();
        $('#divNumeroCNPJCPFCredor').hide();
        $('#divDataEmissao').hide();
        $('#divCodigoUnidadeGestora').hide();
        $('#divCodigoGestao').hide();
        $('#divCodigoNotaFiscalProdesp').hide();
        $('#divNumeroMedicao').hide();
        $('#divTipoEvento').hide();
        $('#divMesMedicao').hide();
        $('#divAnoMedicao').hide();
        $('#divValor').hide();
        $('#divPercentual').hide();
        $('#divRegional').hide(); // órgão
        $('#divPrograma').hide(); //CFP
        $('#divNatureza').hide(); //CED
        $('#divAnoExercicio').hide(); //Ano
        $('#divTipoServicoId').hide(); //Tipo
        $('#divCodigoAplicacaoObra').hide();
        $('#divCodigoUnidadeGestoraObra').hide();
        $('#divNaturezaSubempenho').hide();
        $('#divCodigoCredorOrganizacao').hide();
        $('#divNumeroCNPJCPFFornecedor').hide();
        $('#divFonte').hide();
        $('#divNumeroObra').hide();
        //$('#divOu').hide();
        $('#divCodigoEvento').hide();
        $('#divNewEvento').hide();
        $('#divOu').show();

        if (liquidacao.controller === liquidacao.controllerInclusao) {
            liquidacao.partialPesquisaSaldoRap.hide();
            if (liquidacao.controller === liquidacao.controllerRapInscricao) {
                liquidacao.partialPesquisaSaldoRap.show();
                $('#divbtnConsultar').show();
            }
        }
        $('#divTipoObraCombo').hide();
        $('#divTipoObraTxtBox').hide();

        if (liquidacao.valueCenarioSiafemSiafisico.val() !== "") {

            if (liquidacao.controller === liquidacao.controllerInclusao) {
                liquidacao.partialDadoApropriacao.show();

                if (liquidacao.valueCenarioSiafemSiafisico.val() === "1") {
                    $('#divDataEmissao').show();
                    $('#divCodigoUnidadeGestora').show();
                    $('#divNumeroOriginalSiafemSiafisico').show();
                    $('#divCodigoGestao').show();
                    $('#divTipoEvento').show();
                    $('#divNumeroCT').show();
                    //$('#divOu').show();

                }
                if (liquidacao.valueCenarioSiafemSiafisico.val() === "2") {
                    $('#divDataEmissao').show();
                    $('#divCodigoUnidadeGestora').show();
                    $('#divNumeroOriginalSiafemSiafisico').show();
                    $('#divCodigoGestao').show();
                    $('#divTipoEvento').show();
                    $('#divNumeroCT').show();
                    //$('#divOu').show();
                }
                if (liquidacao.valueCenarioSiafemSiafisico.val() === "3") {
                    $('#divCodigoUnidadeGestora').show();
                    $('#divCodigoGestao').show();
                    $('#divTipoEvento').show();
                    $('#divDataEmissao').show();
                    $('#divNumeroOriginalSiafemSiafisico').show();
                    $('#divValor').show();
                    $('#divNumeroCT').show();
                    //$('#divNumeroCNPJCPFCredor').show();
                    //$('#divCodigoGestaoCredor').show();
                    //$('#divOu').show();
                }
                if (liquidacao.valueCenarioSiafemSiafisico.val() === "4") {
                    $('#divDataEmissao').show();
                    $('#divCodigoUnidadeGestora').show();
                    $('#divCodigoGestao').show();
                    $('#divNumeroCNPJCPFCredor').show();
                    $('#divValor').show();
                    $('#divCodigoEvento').show();
                    $('#divOu').hide();
                }
                if (liquidacao.valueCenarioSiafemSiafisico.val() === "5") {
                    $('#divDataEmissao').show();
                    $('#divCodigoUnidadeGestora').show();
                    $('#divCodigoGestao').show();
                    $('#divNumeroOriginalSiafemSiafisico').show();
                    $('#divValor').show();
                    $('#divOu').hide();
                }
                if (liquidacao.valueCenarioSiafemSiafisico.val() === "6") {
                    $('#divDataEmissao').show();
                    $('#divCodigoUnidadeGestora').show();
                    $('#divCodigoGestao').show();
                    $('#divNumeroCNPJCPFCredor').show();
                    $('#divCodigoGestaoCredor').show();
                    $('#divNumeroOriginalSiafemSiafisico').hide();
                    $('#divOu').hide();
                }
                if (liquidacao.valueCenarioSiafemSiafisico.val() === "7") {
                    $('#divNumeroOriginalSiafemSiafisico').show();
                    $('#divDataEmissao').show();
                    $('#divCodigoUnidadeGestora').show();
                    $('#divCodigoGestao').show();
                    $('#divMesMedicao').show();
                    $('#divAnoMedicao').show();
                    $('#divValor').show();
                    $('#divPercentual').show();
                    $('#divTipoObraCombo').show();
                    $('#divOu').hide();

                }
                if (liquidacao.valueCenarioSiafemSiafisico.val() === "8") {
                    $('#divDataEmissao').show();
                    $('#divCodigoUnidadeGestora').show();
                    $('#divCodigoGestao').show();
                    $('#divNumeroCNPJCPFCredor').show();
                    $('#divCodigoGestaoCredor').show();
                    $('#divTipoObraTxtBox').show();
                    $('#divCodigoUnidadeGestoraObra').show();
                    $('#divMesMedicao').show();
                    $('#divAnoMedicao').show();
                    $('#divValor').show();
                    $('#divNumeroObra').show();
                    $('#divOu').hide();
                }

                if (liquidacao.valueSelecaoProdesp.is(':checked') === true && changedCombox === "N") {
                    if (liquidacao.valueCenarioProdesp.val() === liquidacao.prodespSubEmpenhoRecibo) {
                        //$('#divNumeroOriginalSiafemSiafisico').show();
                        $('#divCodigoNotaFiscalProdesp').show();
                        //campos retornados do webservice
                        $('#divCodigoAplicacaoObra').show();
                        $('#divNatureza').show();
                        $('#divPrograma').show();
                        $('#divNumeroCNPJCPFFornecedor').show();
                        $('#divCodigoCredorOrganizacao').show();
                        $('#divCenarioProdesp').show();
                        $('#divRegional').show();

                    }
                    if (liquidacao.valueCenarioProdesp.val() === liquidacao.prodespSubEmpenhoContrato) {
                        //$('#divNumeroOriginalSiafemSiafisico').show();
                        $('#divCodigoNotaFiscalProdesp').show();
                        $('#divNatureza').show();
                        $('#divCodigoAplicacaoObra').show();
                        $('#divPrograma').show();
                        $('#divNumeroCNPJCPFFornecedor').show();
                        $('#divCodigoCredorOrganizacao').show();
                        $('#divCenarioProdesp').show();
                        $('#divRegional').show();
                        $('#divNaturezaSubempenho').show();
                        $('#NaturezaSubempenhoId').prop("disabled", false);  // habilita para este cenário
                        $('#divNumeroMedicao').show();
                        $('#NumeroMedicao').prop("disabled", false);     // habilita para este cenário
                    }
                    if (liquidacao.valueCenarioProdesp.val() === liquidacao.prodespSubEmpenhoOrganizacao7) {
                        //$('#divNumeroOriginalSiafemSiafisico').show();
                        $('#divCodigoNotaFiscalProdesp').show();
                        $('#divCodigoCredorOrganizacao').show();
                        $('#divNumeroCNPJCPFFornecedor').show();
                        $('#divCodigoAplicacaoObra').show();
                        $('#divNatureza').show();
                        $('#divPrograma').show();
                        $('#divCodigoCredorOrganizacao').show();
                        $('#divCenarioProdesp').show();
                        $('#divNaturezaSubempenho').show();
                        $('#divNumeroMedicao').show();
                        $('#divRegional').show();
                    }
                    if (liquidacao.valueCenarioProdesp.val() === liquidacao.prodespSubEmpenho) {
                        //$('#divNumeroOriginalSiafemSiafisico').show();
                        $('#divCodigoNotaFiscalProdesp').show();
                        $('#divCodigoAplicacaoObra').show();
                        $('#divNatureza').show();
                        $('#divPrograma').show();
                        $('#divNumeroCNPJCPFFornecedor').show();
                        $('#divCodigoCredorOrganizacao').show();
                        $('#divCenarioProdesp').show();
                        //$('#divNaturezaSubempenho').show();
                        $('#divRegional').show();
                    }
                }
            }

            if (liquidacao.controller === liquidacao.controllerAnulacao) {
                liquidacao.partialDadoApropriacao.show();

                if (liquidacao.valueCenarioSiafemSiafisico.val() === "4") {
                    $('#divDataEmissao').show();
                    $('#divCodigoUnidadeGestora').show();
                    $('#divCodigoGestao').show();
                    $('#divNumeroCNPJCPFCredor').show();
                    $('#divCodigoGestaoCredor').show();
                    $('#divValor').show();
                    $('#divOu').hide();
                }
                if (liquidacao.valueCenarioSiafemSiafisico.val() === "5") {
                    $('#divDataEmissao').show();
                    $('#divCodigoUnidadeGestora').show();
                    $('#divNumeroOriginalSiafemSiafisico').show();
                    $('#divNumeroCT').show();
                    $('#divCodigoGestao').show();
                    $('#divValor').show();
                    $('#divTipoEvento').show();
                }
                if (liquidacao.valueCenarioSiafemSiafisico.val() === "6") {
                    $('#divDataEmissao').show();
                    $('#divCodigoUnidadeGestora').show();
                    $('#divCodigoGestao').show();
                    $('#divNumeroCNPJCPFCredor').show();
                    $('#divCodigoGestaoCredor').show();
                    $('#divOu').hide();
                }
                if (liquidacao.valueCenarioSiafemSiafisico.val() === "7") {
                    $('#divDataEmissao').show();
                    $('#divCodigoUnidadeGestora').show();
                    $('#divCodigoGestao').show();
                    $('#divNumeroOriginalSiafemSiafisico').show();
                    $('#divMesMedicao').show();
                    $('#divAnoMedicao').show();
                    $('#divValor').show();
                    $('#divPercentual').show();
                    $('#divTipoObraCombo').show();
                    $('#divOu').hide();
                }
                if (liquidacao.valueCenarioSiafemSiafisico.val() === "8") {
                    $('#divNumeroOriginalSiafemSiafisico').show();
                    $('#divCodigoUnidadeGestora').show();
                    $('#divCodigoGestao').show();
                    $('#divDataEmissao').show();
                    $('#divNumeroCNPJCPFCredor').show();
                    $('#divCodigoGestaoCredor').show();
                    $('#divTipoObraTxtBox').show();
                    $('#divCodigoUnidadeGestoraObra').show();
                    $('#divMesMedicao').show();
                    $('#divAnoMedicao').show();
                    $('#divValor').show();
                    $('#ValorRealizado').val('R$ ' + MaskMonetario(ModelItem.Valor)); // campo é preenchido aqui, devido à regra de atribuição campo valorRealizado para campo valor na inclusão. Na edição aqui é realizado a atribuição inversa.
                    $('#divPercentual').show();
                    $('#divNumeroObra').show();
                    $('#divOu').hide();
                }
                if (liquidacao.valueCenarioSiafemSiafisico.val() === "9") {
                    $('#divDataEmissao').show();
                    $('#divCodigoUnidadeGestora').show();
                    $('#divNumeroCT').show();
                    $('#divNumeroOriginalSiafemSiafisico').show();
                    $('#divCodigoGestao').show();
                    $('#divTipoEvento').show();
                }
                if (liquidacao.valueCenarioSiafemSiafisico.val() === "10" || liquidacao.valueCenarioSiafemSiafisico.val() === "11") {
                    $('#divDataEmissao').show();
                    $('#divCodigoUnidadeGestora').show();
                    $('#divNumeroCT').show();
                    $('#divNumeroOriginalSiafemSiafisico').show();
                    $('#divCodigoGestao').show();
                    $('#divTipoEvento').show();
                }

                if (liquidacao.valueSelecaoProdesp.is(':checked') === true && changedCombox === "N") {
                    if (liquidacao.valueCenarioProdesp.val() === liquidacao.prodespSubEmpenho) {
                        //$('#divNumeroOriginalSiafemSiafisico').show();
                        $('#divValor').show();
                    }

                    if (liquidacao.valueCenarioProdesp.val() === liquidacao.prodespSubEmpenhoContrato) {
                        //$('#divNumeroOriginalSiafemSiafisico').show();
                        $('#divValor').show();
                    }

                }

                if (liquidacao.controller === liquidacao.controllerRapInscricao) {

                    liquidacao.partialDadoApropriacao.show();
                    liquidacao.partialPesquisaSaldoRap.show();
                    liquidacao.partialDadoInscricao.show();


                }
            }

            $('#NumeroOriginalSiafemSiafisico').keydown(function (e) {
                if (liquidacao.valueCenarioSiafemSiafisico.val() === "1" || liquidacao.valueCenarioSiafemSiafisico.val() === "2" || liquidacao.valueCenarioSiafemSiafisico.val() === "3") {
                    $('#NumeroCT').val('');
                }
            });

            $('#NumeroCT').keydown(function (e) {
                if (liquidacao.valueCenarioSiafemSiafisico.val() === "1" || liquidacao.valueCenarioSiafemSiafisico.val() === "2" || liquidacao.valueCenarioSiafemSiafisico.val() === "3") {
                    $('#NumeroOriginalSiafemSiafisico').val('');
                }
            });
        }
    }

    liquidacao.scenaryFactoryForDadoInscricao = function () {
        liquidacao.partialDadoInscricao.hide();
        if (liquidacao.controller === liquidacao.controllerRapInscricao) {
            liquidacao.partialDadoInscricao.show();

            $(".valorRapInscricao").attr("disabled", "disabled");

            $('#divNumeroOriginalProdesp').show();
            $('#divNumeroOriginalSiafemSiafisico').show();
            $('#divCodigoAplicacaoObra').show();
            $('#divCodigoUnidadeGestora').show();
            $('#divCodigoGestao').show();
            $('#divValor').show();
            $('#divDataEmissao').show();
            $('#divCodigoCredorOrganizacao').show();
            $('#divNumeroCNPJCPFFornecedor').show();
            $('#divPrograma').show();
            $('#divNatureza').show();
            $('#divTipoServico').show();
            $('#divNumeroContrato').show();

            $('#divPrograma :input').attr("disabled", true); //CFP
            $('#divNatureza :input').attr("disabled", true); //CED

        }
    }

    liquidacao.scenaryFactoryForPesquisaSaldoRap = function () {
        liquidacao.partialPesquisaSaldoRap.hide();
        if (liquidacao.controller === liquidacao.controllerRapInscricao) {
            liquidacao.partialPesquisaSaldoRap.show();
            $('#divbtnConsultar').show();
        }
    }

    liquidacao.scenaryFactoryForDadoLiquidacaoEvento = function () {
        liquidacao.partialDadoLiquidacaoEvento.hide();

        //inclusão
        if (liquidacao.controller === liquidacao.controllerInclusao) {
            if (liquidacao.valueSelecaoSiafemSiafisico.is(':checked') === true) {
                if (liquidacao.valueCenarioSiafemSiafisico.val() === "6") {
                    liquidacao.partialDadoLiquidacaoEvento.show();
                    $('#DadoLiquidacaoEvento').show();
                    //$('#divNumeroEvento').show();
                    //$('#divInscricaoEvento').show();
                    //$('#divClassificacao').show();
                    //$('#DivFonteId').show();
                    //$('#divValorUnitario').show();
                    $('#divNewEvento').show();
                }
            }
            else
                if (liquidacao.valueCenarioSiafemSiafisico.val() === "3") {
                    liquidacao.partialDadoLiquidacaoEvento.show();
                    //$('#divInscricaoEvento').hide();
                    //$('#divClassificacao').hide();
                    //$('#DivFonteId').hide();
                    //$('#divValorUnitario').hide();
                    $('#divNewEvento').hide();
                    //$('#divNumeroEvento').hide();
                    $('#DadoLiquidacaoEvento').hide();
                }
            if (liquidacao.valueCenarioSiafemSiafisico.val() === "2" || liquidacao.valueCenarioSiafemSiafisico.val() === "1") {
                liquidacao.partialDadoLiquidacaoEvento.hide();
            }

        }
        //anulação
        if (liquidacao.controller === liquidacao.controllerAnulacao) {
            if (liquidacao.valueSelecaoSiafemSiafisico.is(':checked') === true) {
                if (liquidacao.valueCenarioSiafemSiafisico.val() === "6") {
                    liquidacao.partialDadoLiquidacaoEvento.show();
                    $('#DadoLiquidacaoEvento').show();
                    //$('#divNumeroEvento').show();
                    //$('#divInscricaoEvento').show();
                    //$('#divClassificacao').show();
                    //$('#DivFonteId').show();
                    //$('#divValorUnitario').show();
                    $('#divNewEvento').show();
                }
            }

                //siafisico
            else if (liquidacao.valueSelecaoSiafemSiafisico.is(':checked') === false) {
                if (liquidacao.valueCenarioSiafemSiafisico.val() === "4") {
                    liquidacao.partialDadoLiquidacaoEvento.hide();
                    $('#divCodigoEvento').show();
                }
            }


        }

    }
    liquidacao.scenaryFactoryForDadoLiquidacaoEventoGrid = function () {
        liquidacao.partialDadoLiquidacaoEventoGrid.hide();
        //inclusão
        if (liquidacao.controller === liquidacao.controllerInclusao) {
            if (liquidacao.valueSelecaoSiafemSiafisico.is(':checked') === true) {
                if (liquidacao.valueCenarioSiafemSiafisico.val() === "6") {
                    liquidacao.partialDadoLiquidacaoEventoGrid.show();
                }
            }
        }
            //anulação
        else
            if (liquidacao.controller === liquidacao.controllerAnulacao) {
                if (liquidacao.valueSelecaoSiafemSiafisico.is(':checked') === true) {
                    if (liquidacao.valueCenarioSiafemSiafisico.val() === "6") {
                        liquidacao.partialDadoLiquidacaoEventoGrid.show();
                    }
                }
            }

    }
    liquidacao.scenaryFactoryForDadoObservacao = function () {
        liquidacao.partialDadoObservacao.hide();
        $('#DescricaoObservacao2').hide();
        $('#DescricaoObservacao3').hide();

        //inclusão
        if (liquidacao.controller === liquidacao.controllerInclusao) {
            if (liquidacao.valueCenarioSiafemSiafisico.val() === "1" || liquidacao.valueCenarioSiafemSiafisico.val() === "2" || liquidacao.valueCenarioSiafemSiafisico.val() === "3" || liquidacao.valueCenarioSiafemSiafisico.val() === "4" || liquidacao.valueCenarioSiafemSiafisico.val() === "5" || liquidacao.valueCenarioSiafemSiafisico.val() === "6" || liquidacao.valueCenarioSiafemSiafisico.val() === "7" || liquidacao.valueCenarioSiafemSiafisico.val() === "8") {
                liquidacao.partialDadoObservacao.show();
                $('#DescricaoObservacao1').show();
                $('#DescricaoObservacao2').show();
                $('#DescricaoObservacao3').show();
            }
        }
        //anulação
        if (liquidacao.controller === liquidacao.controllerAnulacao) {
            if (liquidacao.valueCenarioSiafemSiafisico.val() === "4" || liquidacao.valueCenarioSiafemSiafisico.val() === "5" || liquidacao.valueCenarioSiafemSiafisico.val() === "6" || liquidacao.valueCenarioSiafemSiafisico.val() === "7" || liquidacao.valueCenarioSiafemSiafisico.val() === "8" || liquidacao.valueCenarioSiafemSiafisico.val() === "9" || liquidacao.valueCenarioSiafemSiafisico.val() === "10" || liquidacao.valueCenarioSiafemSiafisico.val() === "11") {
                liquidacao.partialDadoObservacao.show();
                $('#DescricaoObservacao1').show();
                $('#DescricaoObservacao2').show();
                $('#DescricaoObservacao3').show();
            }
        }

        //Inscrição Rap
        if (liquidacao.controller === liquidacao.controllerRapInscricao) {
            liquidacao.partialDadoObservacao.show();
            $('#DescricaoObservacao1').show();
            $('#DescricaoObservacao2').show();
            $('#DescricaoObservacao3').show();
        }

        if (liquidacao.controller === liquidacao.controllerRapRequisicao && liquidacao.valueCenarioProdesp.val() !== "") {
            liquidacao.partialDadoObservacao.show();
            $('#DescricaoObservacao1').show();
            $('#DescricaoObservacao2').show();
            $('#DescricaoObservacao3').show();
        }


        if (liquidacao.controller === liquidacao.controllerRapAnulacao && liquidacao.valueCenarioProdesp.val() !== "") {
            liquidacao.partialDadoObservacao.show();
            $('#DescricaoObservacao1').show();
            $('#DescricaoObservacao2').show();
            $('#DescricaoObservacao3').show();
        }
    }

    liquidacao.scenaryFactoryForNotaLiquidacao = function () {
        liquidacao.partialNotaLiquidacao.hide();

        //inclusão
        if (liquidacao.controller === liquidacao.controllerInclusao) {
            if (liquidacao.valueCenarioSiafemSiafisico.val() === "1" || liquidacao.valueCenarioSiafemSiafisico.val() === "2" || liquidacao.valueCenarioSiafemSiafisico.val() === "3" || liquidacao.valueCenarioSiafemSiafisico.val() === "5" || liquidacao.valueCenarioSiafemSiafisico.val() === "6" || liquidacao.valueCenarioSiafemSiafisico.val() === "7") {
                liquidacao.partialNotaLiquidacao.show();
            }
        }
            //anulação
        else
            if (liquidacao.controller === liquidacao.controllerAnulacao) {
                if (liquidacao.valueCenarioSiafemSiafisico.val() === "5" || liquidacao.valueCenarioSiafemSiafisico.val() === "6" || liquidacao.valueCenarioSiafemSiafisico.val() === "7" || liquidacao.valueCenarioSiafemSiafisico.val() === "9" || liquidacao.valueCenarioSiafemSiafisico.val() === "10" || liquidacao.valueCenarioSiafemSiafisico.val() === "11") {
                    liquidacao.partialNotaLiquidacao.show();
                }
            }

        if (liquidacao.controller === liquidacao.controllerRapInscricao) {
            liquidacao.partialNotaLiquidacao.show();
        }

        if (liquidacao.controller === liquidacao.controllerRapRequisicao && liquidacao.valueCenarioProdesp.val() !== "") {
            liquidacao.partialNotaLiquidacao.show();
        }



        if (liquidacao.controller === liquidacao.controllerRapAnulacao && liquidacao.valueCenarioProdesp.val() !== "" && liquidacao.valueSelecaoSiafemSiafisico.is(':checked') === true) {
            liquidacao.partialNotaLiquidacao.show();
        }
    }
    liquidacao.scenaryFactoryForDadoLiquidacaoItem = function () {
        liquidacao.partialDadoLiquidacaoItem.hide();
        //inscrição
        if (liquidacao.controller === liquidacao.controllerInclusao) {
            if (liquidacao.valueCenarioSiafemSiafisico.val() === "1" || liquidacao.valueCenarioSiafemSiafisico.val() === "2" || liquidacao.valueCenarioSiafemSiafisico.val() === "3") {
                liquidacao.partialDadoLiquidacaoItem.show();
            }
        }

            //anulação
        else if (liquidacao.controller === liquidacao.controllerAnulacao) {
            if (liquidacao.valueCenarioSiafemSiafisico.val() === "9" || liquidacao.valueCenarioSiafemSiafisico.val() === "10" || liquidacao.valueCenarioSiafemSiafisico.val() === "11") {
                liquidacao.partialDadoLiquidacaoItem.show();
            }
        }
    }
    liquidacao.scenaryFactoryForDadoLiquidacaoItemGrid = function () {
        liquidacao.partialDadoLiquidacaoItemGrid.hide();
        if (liquidacao.controller === liquidacao.controllerInclusao) {
            if (liquidacao.valueCenarioSiafemSiafisico.val() === "1" || liquidacao.valueCenarioSiafemSiafisico.val() === "2" || liquidacao.valueCenarioSiafemSiafisico.val() === "3") {
                liquidacao.partialDadoLiquidacaoItemGrid.show();
            }
        }

        else if (liquidacao.controller === liquidacao.controllerAnulacao) {
            if (liquidacao.valueCenarioSiafemSiafisico.val() === "9" || liquidacao.valueCenarioSiafemSiafisico.val() === "10" || liquidacao.valueCenarioSiafemSiafisico.val() === "11") {
                liquidacao.partialDadoLiquidacaoItemGrid.show();
            }
        }
    }

    liquidacao.scenaryFactoryForDadoReferencia = function () {
        liquidacao.partialDadoReferencia.hide();
        //inscrição
        if (liquidacao.controller === liquidacao.controllerInclusao && liquidacao.valueSelecaoProdesp.is(':checked') === true) {

            if (ModelItem.TransmitidoProdesp) {
                if (liquidacao.valueCenarioProdesp.val() === liquidacao.prodespSubEmpenhoOrganizacao7 || liquidacao.valueCenarioProdesp.val() === liquidacao.prodespSubEmpenho || liquidacao.valueCenarioProdesp.val() === liquidacao.prodespSubEmpenhoRecibo || liquidacao.valueCenarioProdesp.val() === liquidacao.prodespSubEmpenhoContrato) {
                    liquidacao.partialDadoReferencia.show();
                }
            }

            else {

                if (changedCombox === "N") {
                    if ((liquidacao.valueCenarioProdesp.val() === liquidacao.prodespSubEmpenhoOrganizacao7 || liquidacao.valueCenarioProdesp.val() === liquidacao.prodespSubEmpenho)) {

                        liquidacao.partialDadoReferencia.show();

                        //if (Entity.Referencia !== null && Entity.Id !== 0) {
                        if (Entity.Referencia !== null) {
                            if (!Entity.ReferenciaDigitada) {
                                $(".lockRegras").attr("disabled", "disabled");
                            }
                        }
                    }

                    if ((liquidacao.valueCenarioProdesp.val() === liquidacao.prodespSubEmpenhoRecibo || liquidacao.valueCenarioProdesp.val() === liquidacao.prodespSubEmpenhoContrato)) {


                        //if (Entity.Referencia !== null && Entity.Id !== 0) {
                        if (Entity.Referencia !== null) {
                            liquidacao.partialDadoReferencia.show();
                            if (!Entity.ReferenciaDigitada) {
                                $(".lockRegras").attr("disabled", "disabled");
                            }
                        }
                    }
                }

            }

        }

        //RAP Cadastrar Requisição
        if (liquidacao.controller === liquidacao.controllerRapRequisicao) {
            if (ModelItem.TransmitidoProdesp) {
                if (liquidacao.valueCenarioProdesp.val() === liquidacao.prodespSubRAPOraganizao7 || liquidacao.valueCenarioProdesp.val() === liquidacao.prodespSubEmpenhoRAPSimples) {
                    liquidacao.partialDadoReferencia.show();
                } else if (liquidacao.valueCenarioProdesp.val() === liquidacao.prodespRAPRecibo || liquidacao.valueCenarioProdesp.val() === liquidacao.prodespRAPContrato) {
                    liquidacao.partialDadoReferencia.hide();
                }
            }

            else {

                if (changedCombox === "N") {
                    if ((liquidacao.valueCenarioProdesp.val() === liquidacao.prodespSubRAPOraganizao7 || liquidacao.valueCenarioProdesp.val() === liquidacao.prodespSubEmpenhoRAPSimples)) {

                        liquidacao.partialDadoReferencia.show();

                        //if (Entity.Referencia !== null && Entity.Id !== 0) {
                        if (Entity.Referencia !== null) {
                            if (!Entity.ReferenciaDigitada) {
                                $(".lockRegras").attr("disabled", "disabled");
                            }
                        }
                    }

                    if ((liquidacao.valueCenarioProdesp.val() === liquidacao.prodespRAPRecibo || liquidacao.valueCenarioProdesp.val() === liquidacao.prodespRAPContrato)) {

                        //liquidacao.partialDadoReferencia.show();

                        //if (Entity.Referencia !== null && Entity.Id !== 0) {
                        if (Entity.Referencia !== null) {
                            if (!Entity.ReferenciaDigitada) {
                                $(".lockRegras").attr("disabled", "disabled");
                            }
                        }
                    }
                }

            }

        }

    }

    liquidacao.scenaryFactoryForDadoDataVencimento = function () {
        liquidacao.partialDadoDataVencimento.hide();

        if (liquidacao.isRapRequisicao() && ModelItem.TransmitidoProdesp == true) {
            liquidacao.partialDadoDataVencimento.show();
        }
    }


    liquidacao.scenaryFactoryForDadoDespesa = function () {
        liquidacao.partialDadoDespesa.hide();

        $('#divNumeroProcesso').hide();
        $('#divDescricaoAutorizadoSupraFolha').hide();
        $('#divNlRetencaoInss').hide();
        $('#divLista').hide();
        $('#divCodigoEspecificacaoDespesa').hide();
        $('#divDescricaoEspecificacaoDespesa').hide();
        $('#divDescricaoEspecificacaoDespesa :input').hide();

        if (changedCombox === "N" && liquidacao.valueSelecaoProdesp.is(':checked') === true) {
            //inscrição
            if (liquidacao.controller === liquidacao.controllerInclusao) {
                if (liquidacao.valueCenarioProdesp.val() === liquidacao.prodespSubEmpenhoRecibo || liquidacao.valueCenarioProdesp.val() === liquidacao.prodespSubEmpenhoContrato) {
                    liquidacao.partialDadoDespesa.show();
                    $('#divNumeroProcesso').show();
                    $('#divDescricaoAutorizadoSupraFolha').show();
                    $('#divNlRetencaoInss').show();
                    $('#divLista').show();
                    $('#divDescricaoEspecificacaoDespesa').show();
                    $('#DescricaoEspecificacaoDespesa1').show();
                    $('#DescricaoEspecificacaoDespesa2').show();
                    $('#DescricaoEspecificacaoDespesa3').show();
                }
                if (liquidacao.valueCenarioProdesp.val() === liquidacao.prodespSubEmpenhoOrganizacao7 || liquidacao.valueCenarioProdesp.val() === liquidacao.prodespSubEmpenho) {
                    liquidacao.partialDadoDespesa.show();
                    $('#divCodigoEspecificacaoDespesa').show();
                    $('#divNumeroProcesso').show();
                    $('#divDescricaoAutorizadoSupraFolha').show();
                    $('#divNlRetencaoInss').show();
                    $('#divLista').show();
                    $('#divDescricaoEspecificacaoDespesa').show();
                    $('#DescricaoEspecificacaoDespesa1').show();
                    $('#DescricaoEspecificacaoDespesa2').show();
                    $('#DescricaoEspecificacaoDespesa3').show();
                    $('#DescricaoEspecificacaoDespesa4').show();
                    $('#DescricaoEspecificacaoDespesa5').show();
                    $('#DescricaoEspecificacaoDespesa6').show();
                    $('#DescricaoEspecificacaoDespesa7').show();
                    $('#DescricaoEspecificacaoDespesa8').show();
                }
            }
                //anulação
            else if (liquidacao.controller === liquidacao.controllerAnulacao) {
                if (liquidacao.valueCenarioProdesp.val() === liquidacao.prodespSubEmpenho) {
                    liquidacao.partialDadoDespesa.show();
                    $('#divCodigoEspecificacaoDespesa').show();
                    $('#divNumeroProcesso').show();
                    $('#divDescricaoAutorizadoSupraFolha').show();
                    $('#divDescricaoEspecificacaoDespesa').show();

                    $('#DescricaoEspecificacaoDespesa1').show();
                    $('#DescricaoEspecificacaoDespesa2').show();
                    $('#DescricaoEspecificacaoDespesa3').show();
                    $('#DescricaoEspecificacaoDespesa4').show();
                    $('#DescricaoEspecificacaoDespesa5').show();
                    $('#DescricaoEspecificacaoDespesa6').show();
                    $('#DescricaoEspecificacaoDespesa7').show();
                    $('#DescricaoEspecificacaoDespesa8').show();

                }
                if (liquidacao.valueCenarioProdesp.val() === liquidacao.prodespSubEmpenhoContrato) {
                    liquidacao.partialDadoDespesa.show();
                    $('#divNumeroProcesso').show();
                    $('#divDescricaoAutorizadoSupraFolha').show();
                    $('#divDescricaoEspecificacaoDespesa').show();
                    $('#DescricaoEspecificacaoDespesa1').show();
                    $('#DescricaoEspecificacaoDespesa2').show();
                    $('#DescricaoEspecificacaoDespesa3').show();
                }


            }

            if (liquidacao.controller === liquidacao.controllerRapInscricao) {
                liquidacao.partialDadoDespesa.show();
            }


            if (liquidacao.controller === liquidacao.controllerRapRequisicao && liquidacao.valueCenarioProdesp.val() !== "") {
                liquidacao.partialDadoDespesa.show();
                $('#divNumeroProcesso').show();
                $('#divNlRetencaoInss').show();
                $('#divLista').show();
                $('#divDescricaoAutorizadoSupraFolha').show();
                $('#divCodigoEspecificacaoDespesa').show();
                $('#divDescricaoEspecificacaoDespesa').show();
                $('#divDescricaoEspecificacaoDespesa :input').show();
            }

            if (liquidacao.controller === liquidacao.controllerRapAnulacao && liquidacao.valueCenarioProdesp.val() === liquidacao.prodespRAPComContrato) {
                liquidacao.partialDadoDespesa.show();
                $('#divNumeroProcesso').show();
                $('#divDescricaoAutorizadoSupraFolha').show();
                $('#divCodigoEspecificacaoDespesa').show();
                $('#divDescricaoEspecificacaoDespesa').show();
                $('#DescricaoEspecificacaoDespesa1').show();
                $('#DescricaoEspecificacaoDespesa2').show();
                $('#DescricaoEspecificacaoDespesa3').show();
            }

            if (liquidacao.controller === liquidacao.controllerRapAnulacao && liquidacao.valueCenarioProdesp.val() === liquidacao.prodespRAPSemContrato) {
                liquidacao.partialDadoDespesa.show();
                $('#divNumeroProcesso').show();
                $('#divDescricaoAutorizadoSupraFolha').show();
                $('#divCodigoEspecificacaoDespesa').show();
                $('#divDescricaoEspecificacaoDespesa').show();
                $('#divDescricaoEspecificacaoDespesa :input').show();
            }
        }

    }


    liquidacao.scenaryFactoryForDadoAssinatura = function () {
        liquidacao.partialDadoAssinatura.hide();
        //inscrição
        if (liquidacao.controller === liquidacao.controllerInclusao) {
            if (changedCombox === "N" && liquidacao.valueSelecaoProdesp.is(':checked') === true) {
                if (liquidacao.valueCenarioProdesp.val() === liquidacao.prodespSubEmpenhoRecibo || liquidacao.valueCenarioProdesp.val() === liquidacao.prodespSubEmpenhoContrato || liquidacao.valueCenarioProdesp.val() === liquidacao.prodespSubEmpenhoOrganizacao7 || liquidacao.valueCenarioProdesp.val() === liquidacao.prodespSubEmpenho) {
                    liquidacao.partialDadoAssinatura.show();
                }
            }
        }
            //anulação
        else if (liquidacao.controller === liquidacao.controllerAnulacao) {
            if (liquidacao.valueSelecaoProdesp.is(':checked') === true) {
                if (liquidacao.valueCenarioProdesp.val() === liquidacao.prodespSubEmpenho) {
                    liquidacao.partialDadoAssinatura.show();
                }

                if (liquidacao.valueCenarioProdesp.val() === liquidacao.prodespSubEmpenhoContrato) {
                    liquidacao.partialDadoAssinatura.show();
                }
            }
        }
        if (liquidacao.controller === liquidacao.controllerRapInscricao) {
            liquidacao.partialDadoAssinatura.show();
        }
        if (liquidacao.controller === liquidacao.controllerRapRequisicao && liquidacao.valueCenarioProdesp.val() !== "") {
            liquidacao.partialDadoAssinatura.show();
        }

        if (liquidacao.controller === liquidacao.controllerRapAnulacao && liquidacao.valueCenarioProdesp.val() !== "" && liquidacao.valueSelecaoProdesp.is(':checked') === true) {
            liquidacao.partialDadoAssinatura.show();
        }
    }




    liquidacao.scenaryFactoryForDadoCaucao = function () {
        liquidacao.partialDadoCaucao.hide();
        //inscrição
        if (liquidacao.controller === liquidacao.controllerInclusao) {
            if (changedCombox === "N" && liquidacao.valueSelecaoProdesp.is(':checked') === true) {
                if (liquidacao.valueCenarioProdesp.val() === liquidacao.prodespSubEmpenhoRecibo) {
                    liquidacao.partialDadoCaucao.show();
                }
            }
        }
        if (liquidacao.controller === liquidacao.controllerRapInscricao) {
            liquidacao.partialDadoCaucao.show();

            $('#divNumeroGuia').hide();
            $('#divValorCaucionado').hide();
            $('#divValorRealizado').hide();

            $('#divNumeroProcesso').show();
            $('#divDescricaoAutorizadoSupraFolha').show();
            $('#divCodigoEspecificacaoDespesa').show();
            $('#divDescricaoEspecificacaoDespesa').show();
            $('#divDescricaoEspecificacaoDespesa :input').show();
        }



        if (liquidacao.controller === liquidacao.controllerRapRequisicao && liquidacao.valueCenarioProdesp.val() !== "") {
            liquidacao.partialDadoCaucao.show();
            $('#divValorCaucionado').show();
            $('#divNumeroGuia').show();
        }
    }



    liquidacao.filterTipoEventoList = function () {
        var cenaryEventoTipo = window.eventoTipoList.filter(function (eventoTipo) {
            if (liquidacao.valueCenarioSiafemSiafisico.val() === "9") {
                if (liquidacao.controller === liquidacao.controllerAnulacao) {
                    if (eventoTipo.TpEventoSubempenho == 3)
                        return eventoTipo;
                } else {
                    if (eventoTipo.TpEventoSubempenho >= 1)
                        return eventoTipo;
                }
            } else {
                if (eventoTipo.TpEventoSubempenho == 1)
                    return eventoTipo;
            }
        });

        liquidacao.FactorydropDownTipoEventoList(cenaryEventoTipo);
    }



    liquidacao.FactorydropDownTipoEventoList = function (cenaryEventoTipoList) {

        $("#TipoEventoId").empty(); // remove all options bar first
        $("#TipoEventoId").append("<option value='' >Selecione</option>");

        cenaryEventoTipoList.forEach(function (eventoTipo) {
            $("#TipoEventoId").append("<option value='" + eventoTipo.Id + "' >" + eventoTipo.Descricao + "</option>");
        });

        $('#TipoEventoId option[value="' + window.Entity.TipoEventoId + '"]').attr({ selected: "selected" });
    }



    liquidacao.provider = function () {
        liquidacao.body
            .on('change', '#CenarioSiafemSiafisico', liquidacao.disableHandler)
            .on('change', '#transmitirSIAFEM', liquidacao.filterHandler)
            .on('change', '#transmitirSIAFISICO', liquidacao.filterHandler)
            .on('change', '#transmitirSIAFEM', liquidacao.displayHandlerCheckBox)
            .on('change', '#transmitirSIAFISICO', liquidacao.displayHandlerCheckBox)
            .on('change', '#transmitirProdesp', liquidacao.displayHandlerCheckBox)
            .on('change', '#CenarioSiafemSiafisico', liquidacao.displayHandlerChange)
            .on('change', '#CodigoEspecificacaoDespesa', liquidacao.displayHandlerEspDespesa)
            .on('change', '#CodigoNotaFiscalProdesp', liquidacao.displayHandlerNotaFiscal)
            .on('change', '#ValorRealizado', liquidacao.displayHandlerValorRealizado)
            .on('change', '#ValorAnular', liquidacao.displayHandlerValorAnular)

            .on('change', '#Referencia', liquidacao.displayHandlerReferencia)

            .on('change', '#NumeroMedicao', liquidacao.displayHandlerNumeroMedicao)
            .on('change', '#NaturezaSubempenhoId', liquidacao.displayHandlerNaturezaSubempenho)
            .on('change', '#Valor', liquidacao.displayHandlerValorAnulado);
        //.on('blur', '#NumeroOriginalSiafemSiafisico', liquidacao.displayHandlerNumeroNEBlur)
        //.on('blur', '#NumeroCT', liquidacao.displayHandlerNumeroCTBlur)
        //.on('blur', '#' + liquidacao.nlReferencia.prop('id'), liquidacao.displayHandlerNlReferenciaBlur);

        liquidacao.permutarCenarios();
    }

    liquidacao.GerarUGO = function () {
        regionais.forEach(function (regional) {
            if (regional.Id == $("#Orgao").val()) {
                $("#CodigoUnidadeGestora").val(regional.Uge);
            }
        });
    }





    $(".real").maskMoney({ prefix: "R$ ", allowNegative: false, thousands: ".", decimal: ",", affixesStay: true });
    $(".real").maskMoney('mask');

    liquidacao.GerarUGO();

    $('#ValorSaldoAnteriorSubempenho').change(function () {
        liquidacao.SaldoAnulacao();
    });

    $('#ValorAnulado').change(function () {
        liquidacao.SaldoAnulacao();
    });

    $("#Orgao").change(function () {
        liquidacao.GerarUGO();
    });



    $("#Natureza").change(function () {
        if (!isLoad) {
            GerarAplicacao("CodigoAplicacaoObra");
        }
    });



    $(document).on('ready', liquidacao.init);



    liquidacao.SaldoAnulacao = function () {
        var total = 0;

        var valorAnterior = $('#ValorSaldoAnteriorSubempenho').val().replace(/[\.,R$ ]/g, "");
        var valorAnulado = $('#ValorAnulado').val().replace(/[\.,R$ ]/g, "");

        if (valorAnterior === "") {
            valorAnterior = "000";
            $('#ValorSaldoAnteriorSubempenho').val("R$ 0,00");
        }

        total = parseInt(valorAnterior) + parseInt(valorAnulado);

        total = total / 100;
        var valor = String(total).replace(".", ",");
        valor = valor.indexOf(',') == -1 ? valor + ",00" : valor;
        valor = valor.length - valor.indexOf(',') < 3 ? valor + "0" : valor;

        $('#ValorSaldoAposAnulacao').val("R$ " + MaskMonetario(valor));
        $("#ValorAnulado").attr("disabled", true);
        $("#Valor").attr("disabled", true);
    }


    liquidacao.verificarCenarioNlNlObrasSiafem = function () {
        return liquidacao.cenarioContem([6, 7]);
    }

    liquidacao.verificarCenarioNlPregaoBec = function () {
        return liquidacao.cenarioContem([1, 2, 3, 6, 9, 10, 11]);
    }

    liquidacao.verificarCenarioPregaoBec = function () {
        return liquidacao.cenarioContem([2, 3, 10, 11]);
    }

    liquidacao.verificarCenarioBec = function () {
        return liquidacao.cenarioContem([3, 11]);
    }

    liquidacao.cenarioContem = function (cenarios) {
        var cenario = parseInt(liquidacao.valueCenarioSiafemSiafisico.val());
        return cenarios.indexOf(cenario) !== -1;
    }



    liquidacao.checkTransmitirReadOnly = function () {
        liquidacao.checkboxesItens = $('#tblPesquisaItem input[type=checkbox]');
        //Liquidação de Pregão Eletrônico – NLPREGAO = 2 E Nota de Lançamento BEC – NLBEC = 3
        var transmitirReadOnly = false;
        if (!liquidacao.cenarioContem([2, 3, 10, 11])) {
            transmitirReadOnly = true;
            liquidacao.checkboxesItens.prop("checked", true);
            liquidacao.checkboxesItens.trigger('change');
        }

        liquidacao.checkboxesItens.prop("disabled", transmitirReadOnly);

        return transmitirReadOnly;
    };


    isLoad = false;


    liquidacao.preencherItens = function (dados) {
        if (dados.baseItem !== null) {
            liquidacao.carregarGridItens(dados.baseItem);
        }
    }

    liquidacao.carregarGridItens = function (itens) {
        var cont = 0;

        liquidacaoItem.EntityList = window.ItemList = [];

        itens.forEach(function (e) {
            var codigo = itens[cont].CodigoItemServico;
            var unidadeFornecimento = itens[cont].CodigoUnidadeFornecimentoItem;
            //var quantidade = MaskQuantidade(itens[cont].QuantidadeMaterialServico);
            var quantidade = MaskQuantidadeCorretaDoBackEnd(itens[cont].QuantidadeMaterialServico);
            var valor = MaskMonetarioCorretoDoBackEnd(itens[cont].ValorTotal);
            var transmitir = itens[cont].Transmitir === undefined ? true : itens[cont].Transmitir;

            cont = cont + 1;

            var arrQtd = quantidade.split(',');
            var qtdInteiro = arrQtd[0];
            var qtdDecimal = arrQtd[1].padRight(3, "0");

            var arrValor = valor.split(',');
            var vlrInteiro = arrValor[0];
            var vlrDecimal = arrValor[1].padRight(2, "0");
            valor = vlrInteiro + "," + vlrDecimal;

            liquidacaoItem.addComParametros(codigo, unidadeFornecimento, qtdInteiro, qtdDecimal, valor, qtdInteiro, qtdDecimal, transmitir);  // TODO carregar zero?
        });

        if (liquidacao.verificarCenarioNlPregaoBec()) {
            liquidacaoItem.reset();
        }

        $('#CodigoItemServico').val('');
        $('#CodigoUnidadeFornecimentoItem').val('');
        $('#QuantidadeMaterialServico').val('');
        $('#ValorUnitario').val('');
        $('#ValorTotal').val('');
    }

    liquidacao.desabilitaElemento = function (elemento) {
        elemento.prop('readonly', true);
    }

    liquidacao.habilitaElemento = function (elemento) {
        elemento.prop('readonly', false);
    }

    liquidacao.prodespSelecionada = function () {
        return liquidacao.valueSelecaoProdesp.is(':checked') === true;
    }

    liquidacao.isInclusao = function () {
        return liquidacao.controller === liquidacao.controllerInclusao;
    }

    liquidacao.isAnulacao = function () {
        return liquidacao.controller === liquidacao.controllerAnulacao;
    }

    liquidacao.isRapRequisicao = function () {
        return liquidacao.controller === liquidacao.controllerRapRequisicao;
    }

})(window, document, jQuery);

// legado
function CarregarGridItens(itens) {
    return liquidacao.carregarGridItens(itens);
}