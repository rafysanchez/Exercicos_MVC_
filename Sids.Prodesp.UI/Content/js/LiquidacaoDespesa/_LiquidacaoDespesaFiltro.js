(function (window, document, $) {
    'use strict';

    window.filtro = {};

    filtro.init = function () {
        filtro.cacheSelectors();
        filtro.provider();
        $("#NumeroSubempenhoProdesp").addClass(window.controller);
    }

    filtro.cacheSelectors = function () {
        filtro.body = $('body');

        filtro.area = window.area;
        filtro.controller = window.controller;
        filtro.action = window.action;

        filtro.form = $("#form_filtro");
        filtro.buttonResend = $('#btnReTransmitir');
        filtro.buttonSelectAll = $('#idSelecionar');


        filtro.valueDataCadastramentoDe = $("#DataCadastramentoDe");
        filtro.valueDataCadastramentoAte = $("#DataCadastramentoAte");


        filtro.partialFiltro = $('#selecaoFiltro');
        filtro.inputsForFiltro = filtro.partialFiltro.find("input, select");

        filtro.route = '/' + filtro.area + '/' + filtro.controller;


        filtro.selectAllHandler = function () {
            MarcaTodos();
        }

        filtro.reSendHandler = function (e) {
            e.preventDefault();
            
            ObterId(controller);
        }

    }


    filtro.counter = function () {
        var count = 0;

        filtro.inputsForFiltro.each(function () {
            if ($(this).val() !== "") {
                count++;
            }
        });

        return count;
    }


    ////////////// validação de filtro

    filtro.validate = function () {
        var msg = "";
        var data1;
        var data2;

        if (navigator.onLine !== true) {
            AbrirModal("Erro de conexão");
            return false;
        }

        ShowLoading();

        if (filtro.counter() === 0) {
            HideLoading();
            AbrirModal("Informe ao menos um campo para filtro!");
            return false;
        }

        if (filtro.valueDataCadastramentoDe.length > 0) {
            if (filtro.valueDataCadastramentoDe.val().length > 0 &&
                isValidDate(filtro.valueDataCadastramentoDe.val()) === false) {
                msg = "A data inicial é invalida!";
            } else {
                data1 = new Date(
                    filtro.valueDataCadastramentoDe.val().substr(3, 2) + "/" +
                    filtro.valueDataCadastramentoDe.val().substr(0, 2) + "/" +
                    filtro.valueDataCadastramentoDe.val().substr(6, 4)
                );
            }

            if (filtro.valueDataCadastramentoAte.val().length > 0 &&
                isValidDate(filtro.valueDataCadastramentoAte.val()) === false) {
                msg = "A data final é invalida!";
            } else {
                data2 = new Date(
                    filtro.valueDataCadastramentoAte.val().substr(3, 2) + "/" +
                    filtro.valueDataCadastramentoAte.val().substr(0, 2) + "/" +
                    filtro.valueDataCadastramentoAte.val().substr(6, 4)
                );
            }

            if (isValidDate(filtro.valueDataCadastramentoAte.val()) &&
                isValidDate(filtro.valueDataCadastramentoDe.val())) {
                if (data1 && data2 && data1.getTime() > data2.getTime() &&
                    filtro.valueDataCadastramentoAte.val().length > 0) {
                    msg = "A data De deve ser menor ou igual a data Até!";
                }
            }
        }

        if (filtro.valueDataCadastramentoAte.length > 0) {
            if (filtro.valueDataCadastramentoAte.val().length > 0 &&
                isValidDate(filtro.valueDataCadastramentoAte.val()) === false) {
                msg = "A data final é invalida!";
            }
        }

        if (msg !== "") {
            HideLoading();
            AbrirModal(msg);
            return false;
        }

        HideLoading();
        filtro.form.submit();
    }

   //////////////////

    filtro.provider = function () {
        filtro.buttonResend
            .on('click', filtro.reSendHandler);

        filtro.buttonSelectAll
            .on('click', filtro.selectAllHandler);

    }

    $(document).on('ready', filtro.init);

})(window, document, jQuery);