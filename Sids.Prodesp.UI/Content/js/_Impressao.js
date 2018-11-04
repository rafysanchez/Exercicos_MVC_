(function (window, document, $) {
    'use strict';

    window.impressao = {};

    impressao.init = function () {
        impressao.cacheSelectors();
    }

    impressao.cacheSelectors = function () {
        impressao.body = $('body');
        impressao.form = $('form');

        impressao.area = window.area;
        impressao.controller = window.controller;
        impressao.action = window.action;

        impressao.route = '/' + impressao.area + '/' + impressao.controller;
    }


    impressao.print = function (id) {
        $.ajax({
            datatype: 'JSON',
            type: 'POST',
            url: impressao.route + '/Imprimir',
            data: id,
            contentType: 'application/json;',
            beforeSend: function(){
                waitingDialog.show('Gerando impressão');
            },
            success: function (dados) {
                if (impressao.action !== 'Index' && dados.Status === 'Sucesso') {
                    impressao.form.submit();
                } else {
                    AbrirModal(dados.Msg);
                }
            },
            error: function (dados) {
                AbrirModal(dados);
            },
            complete: function () {
                waitingDialog.hide();
            }
        });
    }


    $(document).on('ready', impressao.init);

})(window, document, jQuery);