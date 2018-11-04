(function (window, document, $) {
    'use strict';

    window.reclassificacaoRetencao = {};

    reclassificacaoRetencao.init = function () {


        $(".real").maskMoney({ prefix: "R$ ", allowNegative: false, thousands: ".", decimal: ",", affixesStay: true });
        $(".real").maskMoney('mask');

        if (ModelItem.TransmitidoSiafem == true)
            $(".lockSiafem").attr("disabled", "disabled");

        reclassificacaoRetencao.cacheSelectors();
        reclassificacaoRetencao.provider();
        reclassificacaoRetencao.reset();
        reclassificacaoRetencao.scenaryFactory();
        reclassificacaoRetencao.filterHandler();

        if (window.Entity.ReclassificacaoRetencaoTipoId == 1) {
            reclassificacaoRetencao.EntityListISSQN = window.EntityIdentities;
            reclassificacaoRetencao.EntityListOutros = [];
        }
        if (window.Entity.ReclassificacaoRetencaoTipoId == 2) {
            reclassificacaoRetencao.EntityListOutros = window.EntityIdentities;
            reclassificacaoRetencao.EntityListISSQN = [];
        }
        else {
            reclassificacaoRetencao.EntityListOutros = [];
            reclassificacaoRetencao.EntityListISSQN = [];
        }

        if (reclassificacaoRetencao.controller === reclassificacaoRetencao.controllerInclusao) {
            tipoContrato = 4;
        }
        
        IniciarCreateEdit(reclassificacaoRetencao.controller);

    }

    reclassificacaoRetencao.cacheSelectors = function () {
        reclassificacaoRetencao.body = $('body');
        reclassificacaoRetencao.controller = window.controller;

        reclassificacaoRetencao.controllerReclassificacaoRetencao = 'ReclassificacaoRetencao';

        
        reclassificacaoRetencao.CenarioNotanl = $('.notanl');
        reclassificacaoRetencao.CenarioOpso = $('.opso');
        reclassificacaoRetencao.CenarioRissp = $('.rissp');
        reclassificacaoRetencao.CenarioRinss = $('.rinss');
        reclassificacaoRetencao.CenarioRap = $('.rap');

        reclassificacaoRetencao.valueCenarioReclassificacaoRetencaoTipo = $('#ReclassificacaoRetencaoTipoId');
        reclassificacaoRetencao.valueReclassificacaoRetencaoDocumentoTipo = $('#DocumentoTipoId');


        reclassificacaoRetencao.displayHandlerChange = function (e) {
            if (reclassificacaoRetencao.valueCenarioReclassificacaoRetencaoTipo.val() === "") {
                reclassificacaoRetencao.reset();
            }
            if (reclassificacaoRetencao.valueCenarioReclassificacaoRetencaoTipo.val() !== "") {
                reclassificacaoRetencao.reset();
                reclassificacaoRetencao.scenaryFactory();
            }
        }


    }
    reclassificacaoRetencao.filterHandler = function (e) {
        $('#NumeroDocumento').removeClass("Subempenho");
        $('#NumeroDocumento').removeClass("RapRequisicao");

        if (reclassificacaoRetencao.valueReclassificacaoRetencaoDocumentoTipo.val() == 11) {
            $('#NumeroDocumento').addClass("RapRequisicao");
        } else if (reclassificacaoRetencao.valueReclassificacaoRetencaoDocumentoTipo.val() == 5) {
            $('#NumeroDocumento').addClass("Subempenho");
        }
    }
    reclassificacaoRetencao.reset = function () {
        reclassificacaoRetencao.CenarioNotanl.hide();
        reclassificacaoRetencao.CenarioOpso.hide();
        reclassificacaoRetencao.CenarioRissp.hide();
        reclassificacaoRetencao.CenarioRinss.hide();
        reclassificacaoRetencao.CenarioRap.hide();
    }

    reclassificacaoRetencao.scenaryFactory = function () {
        
        if (reclassificacaoRetencao.valueCenarioReclassificacaoRetencaoTipo.val() == 2) {
            reclassificacaoRetencao.CenarioNotanl.show();
        }
        if (reclassificacaoRetencao.valueCenarioReclassificacaoRetencaoTipo.val() == 3) {
            reclassificacaoRetencao.CenarioOpso.show();
            reclassificacaoRetencao.CenarioRap.show();
        }
        if (reclassificacaoRetencao.valueCenarioReclassificacaoRetencaoTipo.val() == 4) {
            reclassificacaoRetencao.CenarioRissp.show();
        }
        if (reclassificacaoRetencao.valueCenarioReclassificacaoRetencaoTipo.val() == 5) {
            reclassificacaoRetencao.CenarioRinss.show();
        }
        
    }

    reclassificacaoRetencao.provider = function () {
        reclassificacaoRetencao.body
            .on('change', '#ReclassificacaoRetencaoTipoId', reclassificacaoRetencao.displayHandlerChange)
            .on('change', '#DocumentoTipoId', reclassificacaoRetencao.filterHandler);
    }


    $(document).on('ready', reclassificacaoRetencao.init);

})(window, document, jQuery);