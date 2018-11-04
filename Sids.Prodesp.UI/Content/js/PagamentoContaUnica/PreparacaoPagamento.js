(function (window, document, $) {
    'use strict';

    window.preparacaoPagamento = {};

    preparacaoPagamento.init = function () {


        $(".real").maskMoney({ prefix: "R$ ", allowNegative: false, thousands: ".", decimal: ",", affixesStay: true });
        $(".real").maskMoney('mask');
        if (usuario.RegionalId == 1) {
            $("#Orgao").attr('disabled', true);
        }
        else {
            $("#Orgao").attr('disabled', false);
        }

        $("#Orgao").attr('disabled', usuario.RegionalId !== 1);


        if (ModelItem.TransmitidoProdesp == true)
            $(".lockProdesp").attr("disabled", "disabled");

        preparacaoPagamento.cacheSelectors();
        preparacaoPagamento.provider();
        preparacaoPagamento.reset();
        preparacaoPagamento.scenaryFactory();
        preparacaoPagamento.filterHandler();

        IniciarCreateEdit(preparacaoPagamento.controller);

    }

    preparacaoPagamento.cacheSelectors = function () {
        preparacaoPagamento.body = $('body');
        preparacaoPagamento.controller = window.controller;
        preparacaoPagamento.controllerPreparacaoPagamento = 'preparacaoPagamento';
        preparacaoPagamento.CenarioDocumentoGerador = $('.documentoGerador');
        preparacaoPagamento.CenarioOrgaoDespData = $('.orgaoDespData');
        preparacaoPagamento.cenararioConsultaOrgaoDataVenc = $('#divbtnConsultarOrgaoDespDataVenc');

        preparacaoPagamento.valueCenarioPreparacaoPagamentoTipo = $('#PreparacaoPagamentoTipoId');
        preparacaoPagamento.valueListaDeBoletosDocumentoTipo = $('#DocumentoTipoId');

        preparacaoPagamento.displayHandlerChange = function (e) {
            if (preparacaoPagamento.valueCenarioPreparacaoPagamentoTipo.val() === "") {
                preparacaoPagamento.reset();
            }
            if (preparacaoPagamento.valueCenarioPreparacaoPagamentoTipo.val() !== "") {
                preparacaoPagamento.reset();
                preparacaoPagamento.scenaryFactory();
            }
        }

        preparacaoPagamento.filterHandler = function (e) {
            $('#NumeroDocumento').removeClass("Subempenho");
            $('#NumeroDocumento').removeClass("RapRequisicao");

            if (preparacaoPagamento.valueListaDeBoletosDocumentoTipo.val() == 11) {
                $('#NumeroDocumento').addClass("RapRequisicao");
            } else if (preparacaoPagamento.valueListaDeBoletosDocumentoTipo.val() == 5) {
                $('#NumeroDocumento').addClass("Subempenho");
            }
        }
    }

    preparacaoPagamento.reset = function () {
        preparacaoPagamento.CenarioDocumentoGerador.hide();
        preparacaoPagamento.CenarioOrgaoDespData.hide();
        preparacaoPagamento.cenararioConsultaOrgaoDataVenc.hide();
    }

    preparacaoPagamento.scenaryFactory = function () {
        if (preparacaoPagamento.valueCenarioPreparacaoPagamentoTipo.val() == 2)
            preparacaoPagamento.CenarioDocumentoGerador.show();
        if (preparacaoPagamento.valueCenarioPreparacaoPagamentoTipo.val() == 1){
            preparacaoPagamento.CenarioOrgaoDespData.show();
            preparacaoPagamento.cenararioConsultaOrgaoDataVenc.show();
        }
    }

    preparacaoPagamento.provider = function () {
        preparacaoPagamento.body
            .on('change', '#PreparacaoPagamentoTipoId', preparacaoPagamento.displayHandlerChange)
            .on('change', '#DocumentoTipoId', preparacaoPagamento.filterHandler);
    }

    $(document).on('ready', preparacaoPagamento.init);

})(window, document, jQuery);