(function (window, document, $) {
    'use strict';

    window.arquivoRemessa = {};

    arquivoRemessa.init = function () {


        $(".real").maskMoney({ prefix: "R$ ", allowNegative: false, thousands: ".", decimal: ",", affixesStay: true });
        $(".real").maskMoney('mask');

        //$("#RegionalId").attr("ReadOnly", usuario.RegionalId != 1);
        $('#divbtnConsultar').show();

        arquivoRemessa.cacheSelectors();
        arquivoRemessa.provider();
        arquivoRemessa.reset();
        arquivoRemessa.scenaryFactory();
        arquivoRemessa.filterHandler();

        IniciarCreateEdit(arquivoRemessa.controller);

    }

    arquivoRemessa.cacheSelectors = function () {
        arquivoRemessa.body = $('body');
        arquivoRemessa.controller = window.controller;

        if (ModelItem.StatusProdesp == "S") {
            $(".lockProdesp").attr("disabled", "disabled");
 
        }


        //arquivoRemessa.cenararioConsulta = $('#divbtnConsultar');


        arquivoRemessa.displayHandlerChange = function (e) {

        }

        arquivoRemessa.filterHandler = function (e) {
     
        }
    }


 

    arquivoRemessa.reset = function () {

    }

    arquivoRemessa.scenaryFactory = function () {

    
    }

    arquivoRemessa.provider = function () {

    }



    $(document).on('ready', arquivoRemessa.init);

})(window, document, jQuery);