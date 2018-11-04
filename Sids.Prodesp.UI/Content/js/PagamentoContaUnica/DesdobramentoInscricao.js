(function (window, document, $) {
    'use strict';

    window.desdobramento = {};

    desdobramento.init = function () {


        $(".real").maskMoney({ prefix: "R$ ", allowNegative: false, thousands: ".", decimal: ",", affixesStay: true });
        $(".real").maskMoney('mask');




        if (window.Entity.DesdobramentoTipoId == 1) {
            desdobramento.EntityListISSQN = window.EntityIdentities;
            desdobramento.EntityListOutros = [];
        }
        if (window.Entity.DesdobramentoTipoId == 2) {
            desdobramento.EntityListOutros = window.EntityIdentities;
            desdobramento.EntityListISSQN = [];
        }
        else {
            desdobramento.EntityListOutros = window.EntityIdentities;
            desdobramento.EntityListISSQN = window.EntityIdentities;
        }

        desdobramento.cacheSelectors();
        desdobramento.provider();
        desdobramento.reset();
        desdobramento.scenaryFactory();
        desdobramento.filterHandler();

        if (desdobramento.controller === desdobramento.controllerInclusao) {
            tipoContrato = 3;
        }

        //$("#NumeroRequisicaoRap").addClass("RapAnulacao");

        IniciarCreateEdit(desdobramento.controller);

        if (ModelItem.TransmitidoProdesp == true) {
            $("#identificacaoForm").hide();
            $(".lockProdesp").attr("disabled", "disabled");
        }

        if (getParameterByName("tipo") == "c") {
            $("#identificacaoForm").hide();
            $("select[name='tblPesquisaIssqn_length']").removeAttr("Readonly");
            $("select[name='tblPesquisaIssqn_length'] option").removeAttr("disabled");
            $("#tblPesquisaIssqn_filter :input").removeAttr("Readonly");
            $("select[name='tblPesquisaOutros_length']").removeAttr("Readonly");
            $("select[name='tblPesquisaOutros_length'] option").removeAttr("disabled");
            $("#tblPesquisaOutros_filter :input").removeAttr("Readonly");
        }

    }

    desdobramento.cacheSelectors = function () {
        desdobramento.body = $('body');
        desdobramento.controller = window.controller;

        desdobramento.controllerDesdobramento = 'Desdobramento';

        desdobramento.CenarioISSQN = $('.issqn');
        desdobramento.CenarioOutros = $('.outros');

        desdobramento.valueCenarioDesdobramentoTipo = $('#DesdobramentoTipoId');
        desdobramento.valueDesdobramentoDocumentoTipo = $('#DocumentoTipoId');


        desdobramento.displayHandlerChange = function (e) {
            if (desdobramento.valueCenarioDesdobramentoTipo.val() === "") {
                desdobramento.reset();
            }
            if (desdobramento.valueCenarioDesdobramentoTipo.val() !== "") {
                desdobramento.reset();
                desdobramento.scenaryFactory();
            }
        }


    }
    desdobramento.filterHandler = function (e) {
        $('#NumeroDocumento').removeClass("Subempenho");
        $('#NumeroDocumento').removeClass("RapRequisicao");

        if (desdobramento.valueDesdobramentoDocumentoTipo.val() == 11) {
            $('#NumeroDocumento').addClass("RapRequisicao");
        } else if (desdobramento.valueDesdobramentoDocumentoTipo.val() == 5) {
            $('#NumeroDocumento').addClass("Subempenho");
        }
    }
    desdobramento.reset = function () {
        desdobramento.CenarioISSQN.hide();
        desdobramento.CenarioOutros.hide();
    }

    desdobramento.scenaryFactory = function () {
        if (desdobramento.valueCenarioDesdobramentoTipo.val() == 1) {
            desdobramento.CenarioISSQN.show();
            $("#TipoDespesa").removeAttr("Readonly");
            $("#ValorDistribuidoOutros").removeAttr("Readonly");
            desdobramentoList.EntityList = desdobramento.EntityListISSQN;
        }

        if (desdobramento.valueCenarioDesdobramentoTipo.val() == 2) {
            desdobramento.CenarioOutros.show();

            $("#TipoDespesa").attr("Readonly", true);
            $("#ValorDistribuidoOutros").attr("Readonly", true);
            desdobramentoList.EntityList = desdobramento.EntityListOutros;
        }


    }

    desdobramento.valorDedobradoChange = function () {

        if ($("#Tipo").val() == 4) {
            $("#ValorDistribuicao").attr("disabled", true);
            $("#BaseCalc").attr("disabled", true);
            $("#ReterId").attr("disabled", true);
        } else {
            $("#ValorDistribuicao").removeAttr("disabled");
            $("#BaseCalc").removeAttr("disabled");
            $("#ReterId").removeAttr("disabled");
        }

        if ($("#Tipo").val() == 2) {
            $("#ValorDesdobrado").removeAttr("disabled");
        } else {
            $("#ValorDesdobrado").attr("disabled", true);
        }

    }

    desdobramento.validateAutoComplete = function () {
        var nomeCredor = $("#ReduzidoCredor").val();

        var nomes = window.ListaCredores.filter(function (credor) {
            if (credor.Nome.toUpperCase() === nomeCredor.toUpperCase() || credor.Reduzido.toUpperCase() === nomeCredor.toUpperCase()) {
                $("#ReduzidoCredor").val(credor.Reduzido);
                $("#BaseCalc").removeAttr("disabled");

                if (credor.BaseCalculo == true) {
                    $("#BaseCalc").attr("disabled", "disabled");
                    $("#BaseCalc").val("");
                }
                return credor.Reduzido;
            }

        });

        if ($("#Tipo").val().length > 0 && $("#Tipo").val() != 2 && desdobramento.valueCenarioDesdobramentoTipo.val() == 1 && $("#Tipo").val() != 4)
            if (nomes.length === 0 && nomeCredor.length > 0) {
                if ($("#ReduzidoCredor").parent().parent().hasClass("has-error") == false) {
                    $("#ReduzidoCredor").parent().parent().addClass("has-error");
                    $("#ReduzidoCredor").parent().append('<small data-bv-validator="callback" data-bv-validator-for="ReduzidoCredor" class="help-block" style="">Campo inválido</small>');
                    $('small [data-bv-validator-for="ReduzidoCredor"]').show();
                    $("#newIdenficacao").attr("disabled", true);
                    $("#saveIdentificacao").attr("disabled", true);
                }
            } else {
                $("#ReduzidoCredor").parent().parent().removeClass("has-error");
                //desdobramento.AtulizarDadosCredor(nomeCredor);
                $('small [data-bv-validator-for="ReduzidoCredor"]').remove();
                $("#newIdenficacao").removeAttr("disabled");
                $("#saveIdentificacao").removeAttr("disabled");
            }
    }

    desdobramento.AtulizarDadosCredor = function (nomeCredor) {

        /*$.ajax({
            datatype: 'JSON',
            type: 'POST',
            url: "/PagamentoContaUnica/Desdobramento/ConsultarNomeReduzido",
            data: JSON.stringify({ nome: nomeCredor }),
            contentType: "application/json; charset=utf-8",
            beforeSend: function () {
                waitingDialog.show('Consultando');
            },
            success: function (dados) {
                if (dados.Status === 'Sucesso') {
                    
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
        });*/
    }

    desdobramento.valorBaseCalcChange = function () {

        if (parseFloat($("#BaseCalc").val().replace(",",".")) > 100) {
            if ($("#BaseCalc").parent().parent().hasClass("has-error") == false) {
                $("#BaseCalc").parent().parent().addClass("has-error");
                $("#BaseCalc").parent().append('<small data-bv-validator="callback" data-bv-validator-for="BaseCalc" class="help-block" style="">Valor inválido</small>');
                $('small [data-bv-validator-for="ReduzidoCredor"]').show();
                $("#newIdenficacao").attr("disabled", true);
                $("#saveIdentificacao").attr("disabled", true);
            }
        } else {
            $("#BaseCalc").parent().parent().removeClass("has-error");
            //desdobramento.AtulizarDadosCredor(nomeCredor);
            $('small [data-bv-validator-for="BaseCalc"]').remove();
            $("#newIdenficacao").removeAttr("disabled");
            $("#saveIdentificacao").removeAttr("disabled");
        }


    }

    desdobramento.provider = function () {
        desdobramento.body
            .on('change', '#DesdobramentoTipoId', desdobramento.displayHandlerChange)
            .on('change', '#Tipo', desdobramento.valorDedobradoChange)
            .on('change', '#DocumentoTipoId', desdobramento.filterHandler)
            .on('blur', '#ReduzidoCredor', desdobramento.validateAutoComplete)
            .on('blur', '#BaseCalc', desdobramento.valorBaseCalcChange)
            .on('blur', '#NumeroDocumento', ConsultaDocumento)
            .on('change', '#DocumentoTipoId', ConsultaDocumento)
        ;

        $("#ReduzidoCredor").autocomplete({
            source: window.NomeReduzidoList
        });
    }

    $(document).on('ready', desdobramento.init);

})(window, document, jQuery);