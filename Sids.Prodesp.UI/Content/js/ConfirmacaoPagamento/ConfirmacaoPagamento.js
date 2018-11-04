var obj = "ConfirmacaoPagamento";

var controleCheck = '.chkitens:checked';

function Alterar(id) {
    AbrirDetalhe('@Url.Action("Alterar")' + '?id=' + id);
}

function Visualizar(id) {
    AbrirVisualizar('@Url.Action("Visualizar")' + '?id=' + id);
}

function Excluir(id) {
    AbrirExcluir('@Url.Action("Excluir")' + '?id=' + id);
}

function ShowExt() {
    var qtdeItensCheck = $(controleCheck).length;
    var conteudoRadio = obterValoresPorControle(controleCheck);

    $('#selecionados').val(conteudoRadio);
    alert(conteudoRadio);

    $(controleCheck).each(function () {
        //$(this).attr('disabled', 'disabled');
        if ($(this).attr('data-id') == conteudoRadio) {
            //alert($(this).attr('data-id'));
            //$(this).attr('disabled', 'disabled');
            return;
        }
    })
}

function obterValoresPorControle(nomeControle) {
    var controlArray = [];

    $(nomeControle).each(function () {
        controlArray.push($(this).attr('data-id'));
    });

    var selecionado;
    selecionado = controlArray.join(',');

    if (selecionado.length > 0) {
        $(controleCheck).enabled = false;

        return selecionado;
    }
    else {
        return '';
    }
}


var CONFIRMACAOPAGAMENTO = (function () {

    function init() {
    }

    return {
        Init: init
    }
}());

$(function () {
    CONFIRMACAOPAGAMENTO.Init();
})