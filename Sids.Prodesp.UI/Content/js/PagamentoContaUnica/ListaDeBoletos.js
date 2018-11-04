(function (window, document, $) {
    'use strict';

    window.listaDeBoletos = {};
    var leitor = 0;


    listaDeBoletos.init = function () {

        $(".real").maskMoney({ prefix: "R$ ", allowNegative: false, thousands: ".", decimal: ",", affixesStay: true });
        $(".real").maskMoney('mask');

        if (ModelItem.TransmitidoSiafem == true)
            $(".lockSiafem").attr("disabled", "disabled");

        listaDeBoletos.cacheSelectors();
        listaDeBoletos.provider();
        listaDeBoletos.reset();
        listaDeBoletos.scenaryFactory();
        listaDeBoletos.filterHandler();

        listaDeBoletos.EntityList = window.EntityList;

        IniciarCreateEdit(listaDeBoletos.controller);

        if (getParameterByName("tipo") == "c") {
            $("#divTipoBoleto").hide();
            $("#divInputCodBarras").hide();
            $("select[name='tblCodigoBarras_length']").removeAttr("Readonly");
            $("select[name='tblCodigoBarras_length'] option").removeAttr("disabled");
            $("#tblCodigoBarras_filter :input").removeAttr("Readonly");
        }

        var inputs = $('.barras').keyup(function (e) {
            if ($(this).val().length == $(this).attr('maxlength')) {
                e.preventDefault();
                var nextInput = inputs.get(inputs.index(this) + 1);
                if (nextInput) {
                    nextInput.focus();
                }
            }
        });



        $("#CodigoDeBarras").keyup(function (event) { // ao digitar Enter preenche o grid do Cod. Barra
            if (event.which == 13) {
                listaDeBoletosCodicoBarras.formatCode();
                listaDeBoletosCodicoBarras.add();
                if ($("#CodigoDeBarras").is(':visible')) {
                    $("#CodigoDeBarras").focus();
                }
                else {
                    if ($("#NumeroTaxa1").is(':visible')) {
                        $("#NumeroTaxa1").focus();
                    }
                    else {
                        $("#NumeroBoleto1").focus();
                    }
                }

            }
        });

        $("#NumeroBoleto7").keyup(function (event) { // ao digitar Enter preenche o grid do Cod. Barra
            if (event.which == 13) {
                listaDeBoletosCodicoBarras.add();
                $("#NumeroBoleto1").focus();

            }
        });

        $("#NumeroTaxa4").keyup(function (event) { // ao digitar Enter preenche o grid do Cod. Barra
            if (event.which == 13) {
                listaDeBoletosCodicoBarras.add();
                $("#NumeroTaxa1").focus();

            }
        });
        

    }

    listaDeBoletos.cacheSelectors = function () {
        listaDeBoletos.body = $('body');
        listaDeBoletos.controller = window.controller;

        listaDeBoletos.controllerListaDeBoletos = 'ListaDeBoletos';


        listaDeBoletos.CenarioBoleto = $('.taxa');
        listaDeBoletos.CenarioTaxa = $('.boleto');
        listaDeBoletos.CenarioLeitor = $('.leitor');

        listaDeBoletos.valueCenarioTipoBoleto = $('#TipoBoleto');
        listaDeBoletos.valueListaDeBoletosDocumentoTipo = $('#DocumentoTipoId');

        listaDeBoletos.valueDigitarCodigo = $('#divDigitarCodigo');


    }

    listaDeBoletos.displayHandlerChange = function (e) {
        if (listaDeBoletos.valueCenarioTipoBoleto.val() === "") {
            listaDeBoletos.reset();
        }
        if (listaDeBoletos.valueCenarioTipoBoleto.val() !== "") {
            leitor = 0;
            listaDeBoletos.reset();
            listaDeBoletos.scenaryFactory();
        }
    }

    listaDeBoletos.filterHandler = function (e) {
        $('#NumeroDocumento').removeClass("Subempenho");
        $('#NumeroDocumento').removeClass("RapRequisicao");

        if (listaDeBoletos.valueListaDeBoletosDocumentoTipo.val() == 11) {
            $('#NumeroDocumento').addClass("RapRequisicao");
        } else if (listaDeBoletos.valueListaDeBoletosDocumentoTipo.val() == 5) {
            $('#NumeroDocumento').addClass("Subempenho");
        }
    }
    listaDeBoletos.reset = function () {
        listaDeBoletos.CenarioBoleto.hide();
        listaDeBoletos.CenarioTaxa.hide();
        listaDeBoletos.CenarioLeitor.show();
        listaDeBoletos.valueDigitarCodigo.hide();
    }

    listaDeBoletos.scenaryFactory = function () {

        if (leitor == 1) {
            
            listaDeBoletos.CenarioBoleto.hide();
            listaDeBoletos.CenarioTaxa.hide();
            listaDeBoletos.CenarioLeitor.show();
            listaDeBoletos.valueDigitarCodigo.show();
        }
        else {

            if (listaDeBoletos.valueCenarioTipoBoleto.val() == 1) {
                listaDeBoletos.CenarioBoleto.show();
                listaDeBoletos.CenarioLeitor.hide();
                $("#NumeroTaxa1").focus();
            }
            if (listaDeBoletos.valueCenarioTipoBoleto.val() == 2) {
                listaDeBoletos.CenarioTaxa.show();
                listaDeBoletos.CenarioLeitor.hide();
                $("#NumeroBoleto1").focus();
            }
        }
    }

    listaDeBoletos.provider = function () {
        listaDeBoletos.body
            .on('change', '#TipoBoleto', listaDeBoletos.displayHandlerChange)
            .on('change', '#DocumentoTipoId', listaDeBoletos.filterHandler);
    }

    listaDeBoletos.UtilizarLeitor = function () {
        listaDeBoletos.reset();
        listaDeBoletos.valueDigitarCodigo.show();
        $("#CodigoDeBarras").focus();
        leitor = 1;
        
    }

    $(document).on('ready', listaDeBoletos.init);


})(window, document, jQuery);