(function (window, document, $) {
    'use strict';

    window.desembolsoAgrupamento = {};


    desembolsoAgrupamento.init = function () {
        desembolsoAgrupamento.cacheSelectors();
        desembolsoAgrupamento.resetButtons();
        desembolsoAgrupamento.provider();
    }

    desembolsoAgrupamento.cacheSelectors = function () {
        desembolsoAgrupamento.body = $('body');
        desembolsoAgrupamento.controller = window.controller;

        desembolsoAgrupamento.controllerReclassific = 'ReclassificacaoRetencao';

        desembolsoAgrupamento.EntityList = window.AgrupamentoList;
        desembolsoAgrupamento.programacaoDesembolsoEntityList = desembolsoAgrupamento.EntityList;

        $.each(desembolsoAgrupamento.EntityList, function (id, val) {
            desembolsoAgrupamento.EntityList[id].DataEmissao = desembolsoAgrupamento.EntityList[id].DataEmissao ? ConvertDateCSharp(desembolsoAgrupamento.EntityList[id].DataEmissao) : null;
            desembolsoAgrupamento.EntityList[id].DataVencimento = desembolsoAgrupamento.EntityList[id].DataVencimento ? ConvertDateCSharp(desembolsoAgrupamento.EntityList[id].DataVencimento) : null;
            desembolsoAgrupamento.EntityList[id].DataTransmissaoSiafem = desembolsoAgrupamento.EntityList[id].TransmitidoSiafem ? ConvertDateCSharp(desembolsoAgrupamento.EntityList[id].DataTransmitidoSiafem) : null;
        });

        desembolsoAgrupamento.objectToCreate = [];
        desembolsoAgrupamento.objectToEdit = {};

        //desembolsoAgrupamento.buttonNew = $('#divNewEvento');
        desembolsoAgrupamento.buttonSave = $('#btnConfirmarAgrupamento');
        desembolsoAgrupamento.table = $('#tblPesquisaAgrupamento');
        desembolsoAgrupamento.valorTotal = $('#ValorTotal ');

        desembolsoAgrupamento.Agrupamento = [];
        desembolsoAgrupamento.selectedRow = "";

        desembolsoAgrupamento.newHandler = function (e) {
            e.preventDefault();
            desembolsoAgrupamento.add();
        }

        desembolsoAgrupamento.saveHandler = function (e) {
            e.preventDefault();
            desembolsoAgrupamento.ConfirmarAgrupamento();

        }
    }

    desembolsoAgrupamento.resetButtons = function () {
        //desembolsoAgrupamento.buttonNew.show();
        desembolsoAgrupamento.buttonSave.show();
    }

    desembolsoAgrupamento.validate = function () {
        var itemExists = $.grep(desembolsoAgrupamento.EntityList, function (e) {
            return e.NumeroDocumentoGerador === "";
        });

        return itemExists.length === 0;
    }

    desembolsoAgrupamento.ConfirmarAgrupamento = function () {

        if (getParameterByName("tipo") == "a" && Entity.Agrupamentos.length > 0 ) {

            var $tabela = $("#tblPagamentosPreparar");
            var oTable = $tabela.dataTable();
            var allPages = $tabela.dataTable().fnGetNodes();
            var $checks = $('input[name="checkbox1"]', allPages);

            var values = programacaoDesembolsoList;

            selectedsDocumentoGerador = [];


            $.each(values, function (index, x) {
                var tt = x.NumeroDocumentoGerador;
                var aux = 0;

                $.each($checks, function () {
                    var selectDoc = $(this).val();

                    if (x.NumeroDocumentoGerador == selectDoc) {
                        if ($(this).prop("disabled") !== true) {
                                if ($(this).prop("checked") == true) {
                                    aux = 0;
                                    return false;
                                } else {
                                    aux = 1;
                                    return false;
                                }
                        } else {
                            aux = 1;
                            return false;
                        }
                    }


                });

                if (aux == 0) {
                    selectedsDocumentoGerador.push(x.NumeroDocumentoGerador);
                }

            });
        }



        if (selectedsDocumentoGerador.length <= 0) {
            AbrirModal("Nenhum item selecionado!");
            return false;
        }



        desembolsoAgrupamento.EntityList = [];
        desembolsoAgrupamento.table.DataTable().clear().draw(true);
        FecharModal("#modalPagamentosPreparar");

        if (selectedsDocumentoGerador.length > 1)
            $.each(selectedsDocumentoGerador, function (id, val) {
                var documentoGerador = val;
                var selectedAgrupamento = programacaoDesembolsoList.filter(function (agrupamento) {
                    if (agrupamento.NumeroDocumentoGerador == documentoGerador) return agrupamento;
                });

                if (selectedAgrupamento.length > 0)
                    desembolsoAgrupamento.add(selectedAgrupamento[0]);

            });
        else {
            var val = selectedsDocumentoGerador[0];

            var documentoGerador = val;
            var selectedAgrupamento = programacaoDesembolsoList.filter(function (agrupamento) {
                if (agrupamento.NumeroDocumentoGerador == documentoGerador) return agrupamento;
            });

            if (selectedAgrupamento.length > 0)
                desembolsoAgrupamento.add(selectedAgrupamento[0]);
        }

        desembolsoAgrupamento.sumarize();
    }

    desembolsoAgrupamento.add = function (agrupamento) {
        console.log(agrupamento);
        $('#NumeroContrato').val(agrupamento.NumeroContrato);
        $('#CodigoAplicacaoObra').val(agrupamento.CodigoAplicacaoObra);
        
        desembolsoAgrupamento.objectToCreate = [
            agrupamento.NumeroDocumentoGerador,
            agrupamento.NomeCredorReduzido,
            agrupamento.NumeroCnpjcpfPagto,
            agrupamento.DataVencimento,
            "R$ ".concat(MaskMonetario(agrupamento.Valor)),
            agrupamento.Fonte,
            StringIsNull(agrupamento.NumeroSiafem, ""),
            StringIsNull(agrupamento.MensagemServicoSiafem, "")
        ];


        desembolsoAgrupamento.EntityList[desembolsoAgrupamento.EntityList.length] = agrupamento;

        desembolsoAgrupamento.new();

        desembolsoAgrupamento.table.dataTable().fnAddData(desembolsoAgrupamento.objectToCreate);

        //desembolsoAgrupamento.clean();

        desembolsoAgrupamento.selectedRow = "";

    }

    desembolsoAgrupamento.select = "";

    desembolsoAgrupamento.edit = function () {

        programacaoDesembolsoList = desembolsoAgrupamento.EntityList;
        GerarTabelaDocGerador($("#ProgramacaoDesembolsoTipoId").val());
        $("#MarcarTodos").prop("checked", true);
        ToogleMarcarTodos();
    }

    desembolsoAgrupamento.save = function (select) {

        $.each(programacaoDesembolsoList, function (i, value) {
            if (programacaoDesembolsoList[i].NumeroDocumentoGerador === select[1]) {
                programacaoDesembolsoList[i].Regional = select[2];
                programacaoDesembolsoList[i].CodigoUnidadeGestora = select[3];
                programacaoDesembolsoList[i].CodigoGestao = select[4];
                programacaoDesembolsoList[i].NumeroNLReferencia = select[5];
                programacaoDesembolsoList[i].NumeroCnpjcpfCredor = select[6];
                programacaoDesembolsoList[i].GestaoCredor = select[7];
                programacaoDesembolsoList[i].NumeroBancoCredor = select[8];
                programacaoDesembolsoList[i].NumeroAgenciaCredor = select[9];
                programacaoDesembolsoList[i].NumeroContaCredor = select[10];
                programacaoDesembolsoList[i].NumeroCnpjcpfPagto = select[11];
                programacaoDesembolsoList[i].NomeCredorReduzido = select[12];
                programacaoDesembolsoList[i].GestaoPagto = select[13];
                programacaoDesembolsoList[i].NumeroBancoPagto = select[14];
                programacaoDesembolsoList[i].NumeroAgenciaPagto = select[15];
                programacaoDesembolsoList[i].NumeroContaPagto = select[16];
                programacaoDesembolsoList[i].NumeroProcesso = select[17];
                programacaoDesembolsoList[i].Finalidade = select[18];
                programacaoDesembolsoList[i].NumeroEvento = select[19];
                programacaoDesembolsoList[i].InscricaoEvento = select[20];
                programacaoDesembolsoList[i].RecDespesa = select[21]; // mudar para rec_despesa
                programacaoDesembolsoList[i].Classificacao = select[22];
                programacaoDesembolsoList[i].Fonte = select[23];
                programacaoDesembolsoList[i].DataEmissao = select[24];
                programacaoDesembolsoList[i].DataVencimento = select[25];
                programacaoDesembolsoList[i].NumeroListaAnexo = select[26];
                programacaoDesembolsoList[i].Valor = select[27].replace(/[\R$ .]/g, "");
            }
        });

        desembolsoAgrupamento.selectedRow = "";

        desembolsoAgrupamento.sumarize();

        GerarTabelaDocGerador($("#ProgramacaoDesembolsoTipoId").val());
        TogleCampos();

    }


    desembolsoAgrupamento.sumarize = function () {
        var total = 0;

        $.each(selectedsDocumentoGerador, function (id, val) {
            var documentoGerador = val;
            var selectedAgrupamento = programacaoDesembolsoList.filter(function (agrupamento) {
                if (agrupamento.NumeroDocumentoGerador == documentoGerador) return agrupamento;
            });

            var precoTotal = MaskMonetario(selectedAgrupamento[0].Valor);
            precoTotal = Left(precoTotal, 1) == "," ? "0,".concat(precoTotal) : precoTotal;



            if (precoTotal.length > 6) {
                var numero1 = parseFloat(precoTotal.replace(',', '.').replace('.', ''));
            }
            else {
                var numero1 = parseFloat(precoTotal.replace(',', '.'));
            };




            if (precoTotal.length > 0) {
                precoTotal = numero1;
                total += precoTotal;
            }
        });

        total = total.toFixed(2);

        var totalAtual = MaskMonetario(total);


        desembolsoAgrupamento.valorTotal.val(totalAtual);
        desembolsoAgrupamento.valorTotal.maskMoney({ prefix: "R$ ", allowNegative: false, thousands: ".", decimal: ",", affixesStay: true });
        desembolsoAgrupamento.valorTotal.maskMoney('mask');
        desembolsoAgrupamento.valorTotal.maskMoney('destroy');

    }


    desembolsoAgrupamento.new = function () {
        desembolsoAgrupamento.eventos = desembolsoAgrupamento.EntityList;
    }


    desembolsoAgrupamento.provider = function () {
        desembolsoAgrupamento.body
            .on('click', '#btnConfirmarAgrupamento', desembolsoAgrupamento.saveHandler);
    }

    $(document).on('ready', desembolsoAgrupamento.init);

})(window, document, jQuery);