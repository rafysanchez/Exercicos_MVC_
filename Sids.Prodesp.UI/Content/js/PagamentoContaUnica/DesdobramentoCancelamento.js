(function (window, document, $) {
    'use strict';

    window.desdobramentoCancelamento = {};

    
    desdobramentoCancelamento.init = function () {
        desdobramentoCancelamento.cacheSelectors();
        desdobramentoCancelamento.provider();
    }



    desdobramentoCancelamento.cacheSelectors = function () {
        desdobramentoCancelamento.buttonTransmitir = $("#btnTransmitir");
        desdobramentoCancelamento.Entity = window.Entity;
        desdobramentoCancelamento.controller = window.controller;

      

    }

    
    desdobramentoCancelamento.transmitir = function () {
        if (navigator.onLine !== true) {
            AbrirModal("Erro de conexão");
            return false;
        }

        desdobramentoCancelamento.new();
        ModelItem = desdobramentoCancelamento.Entity;
        Transmissao(JSON.stringify(desdobramentoCancelamento.Entity), desdobramentoCancelamento.controller);
    }

    desdobramentoCancelamento.new = function () {

       
        desdobramentoCancelamento.Entity.DocumentoTipo.Descricao = $("#DocumentoTipo_Descricao").val(),
        desdobramentoCancelamento.Entity.NumeroDocumento = $("#NumeroDocumento").val(),
        desdobramentoCancelamento.Entity.DescricaoCredor = $("#DescricaoCredor").val(),
        desdobramentoCancelamento.Entity.TipoDespesa = $("#TipoDespesa").val(),
       
        desdobramentoCancelamento.Entity.TransmitirProdesp = true;
        desdobramentoCancelamento.Entity.TransmitidoProdesp = false;

    }

    desdobramentoCancelamento.provider = function () {
        desdobramentoCancelamento.buttonTransmitir
        .on('click', desdobramentoCancelamento.transmitir);
    }

    $(document).on('ready', desdobramentoCancelamento.init);
})(window, document, jQuery);