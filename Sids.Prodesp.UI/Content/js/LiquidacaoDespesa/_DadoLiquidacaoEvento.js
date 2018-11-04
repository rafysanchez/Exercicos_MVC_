(function (window, document, $) {
    'use strict';

    window.liquidacaoEvento = {};



    liquidacaoEvento.init = function () {
        liquidacaoEvento.cacheSelectors();
        liquidacaoEvento.resetButtons();
        liquidacaoEvento.provider();
    }

    liquidacaoEvento.cacheSelectors = function () {
        liquidacaoEvento.body = $('body');
        liquidacaoEvento.controller = window.controller;

        liquidacaoEvento.controllerInclusao = 'Subempenho';
        liquidacaoEvento.controllerAnulacao = 'SubempenhoCancelamento';

        liquidacaoEvento.Entity = window.Entity;
        liquidacaoEvento.EntityList = window.EventoList;

        liquidacaoEvento.objectToCreate = [];
        liquidacaoEvento.objectToEdit = {};

        liquidacaoEvento.buttonNew = $('#divNewEvento');
        liquidacaoEvento.buttonSave = $('#divSaveEvento');
        liquidacaoEvento.table = $('#tblPesquisaEvento');

        liquidacaoEvento.numeroEvento = $('#NumeroEvento');
        liquidacaoEvento.inscricaoEvento = $('#InscricaoEvento');
        liquidacaoEvento.classificacao = $('#Classificacao');
        liquidacaoEvento.fonte = $('#Fonte');
        liquidacaoEvento.valorUnitario = $('#ValorUnitario');
        liquidacaoEvento.valorTotal = $('#Total');
        liquidacaoEvento.buttonEdit = '<button type="button" title="Alterar" class="btn btn-xs btn-info margL7" onclick="liquidacaoEvento.edit(this)"><i class="fa fa-edit"></i></button>';
        liquidacaoEvento.buttonRemove = '<button type="button" title="Excluir" class="btn btn-xs btn-danger margL7" onclick="liquidacaoEvento.remove(this)"><i class="fa fa-trash"></i></button>';

        liquidacaoEvento.eventos = [];
        liquidacaoEvento.selectedRow = "";

        liquidacaoEvento.newHandler = function (e) {
            e.preventDefault();
            liquidacaoEvento.add();
        }

        liquidacaoEvento.saveHandler = function (e) {
            e.preventDefault();
            liquidacaoEvento.save();
        }
    }

    liquidacaoEvento.resetButtons = function () {
        liquidacaoEvento.buttonNew.show();
        liquidacaoEvento.buttonSave.hide();
    }

    liquidacaoEvento.validate = function () {
        var itemExists = $.grep(liquidacaoEvento.EntityList, function (e) {
            return e.NumeroEvento === liquidacaoEvento.numeroEvento.val();
        });

        return itemExists.length === 0;
    }

    liquidacaoEvento.add = function () {


        if (liquidacaoEvento.numeroEvento.val().length <= 0) {
            AbrirModal("Favor preencher Evento");
            return false;
        }
        if (liquidacaoEvento.inscricaoEvento.val().length <= 0) {
            AbrirModal("Favor preencher Inscrição do Evento");
            return false;
        }

        if (liquidacaoEvento.fonte.val().length <= 0) {
            AbrirModal("Favor preencher Fonte");
            return false;
        }
        if (liquidacaoEvento.valorUnitario.val().replace(/[\.,R$ ]/g, "") <= 0) {
            AbrirModal("Favor preencher Valor do Evento");
            return false;
        }

        if (window.controller === 'Subempenho') {
            var valorAtual = parseInt($('#Total').val().replace(/[\.,R$ ]/g, "")) + parseInt($('#ValorUnitario').val().replace(/[\.,R$ ]/g, ""));
            if (liquidacao.prodespSelecionada() && valorAtual > $('#ValorRealizado').val().replace(/[\.,R$ ]/g, "")) {
                AbrirModal("Valor Total do Evento maior que o Valor Realizado");
                return false;
            }
        }
        else {
            if (window.controller === 'SubempenhoCancelamento') {
                var valorAtual = parseInt($('#Total').val().replace(/[\.,R$ ]/g, "")) + parseInt($('#ValorUnitario').val().replace(/[\.,R$ ]/g, ""));
                if (liquidacao.prodespSelecionada() && valorAtual > $('#ValorAnular').val().replace(/[\.,R$ ]/g, "")) {
                    AbrirModal("Valor Total do Evento maior que o Valor a Anular");
                    return false;
                }
            }
        }

        liquidacaoEvento.objectToCreate = [
            liquidacaoEvento.numeroEvento.val(),
            liquidacaoEvento.inscricaoEvento.val(),
            liquidacaoEvento.classificacao.val(),
            liquidacaoEvento.fonte.val(),
            liquidacaoEvento.valorUnitario.val(),
            liquidacaoEvento.buttonEdit + liquidacaoEvento.buttonRemove
        ];

        liquidacaoEvento.objectToEdit = {
            Id: 0,
            SubempenhoId: liquidacaoEvento.Entity.Id,
            NumeroEvento: liquidacaoEvento.numeroEvento.val(),
            InscricaoEvento: liquidacaoEvento.inscricaoEvento.val(),
            Classificacao: liquidacaoEvento.classificacao.val(),
            Fonte: liquidacaoEvento.fonte.val(),
            ValorUnitario: liquidacaoEvento.valorUnitario.val().replace("R$ ", "").replace(/[.,]/g, "")
        };

        if (liquidacaoEvento.validate() === false) {
            AbrirModal("Não é permitido incluir eventos com o mesmo número.");
            return false;
        }

        liquidacaoEvento.EntityList[liquidacaoEvento.EntityList.length] = liquidacaoEvento.objectToEdit;

        liquidacaoEvento.new();

        liquidacaoEvento.table.dataTable().fnAddData(liquidacaoEvento.objectToCreate);

        liquidacaoEvento.clean();

        liquidacaoEvento.selectedRow = "";

        liquidacaoEvento.sumarize();
    }

    liquidacaoEvento.select = "";

    liquidacaoEvento.edit = function (nRow) {

        liquidacaoEvento.select = "";
        liquidacaoEvento.buttonNew.hide();
        liquidacaoEvento.buttonSave.show();

        liquidacaoEvento.selectedRow = $(nRow).parent();
        var aData = liquidacaoEvento.table.dataTable().fnGetData(liquidacaoEvento.selectedRow);
        liquidacaoEvento.select = aData[0];
        liquidacaoEvento.numeroEvento.val(aData[0]);
        liquidacaoEvento.inscricaoEvento.val(aData[1]);
        liquidacaoEvento.classificacao.val(aData[2]);
        liquidacaoEvento.fonte.val(aData[3]);
        liquidacaoEvento.valorUnitario.val(aData[4]);

        liquidacaoEvento.sumarize();
    }

    liquidacaoEvento.remove = function (nRow) {
        var row = $(nRow).parent();

        var aData = liquidacaoEvento.table.dataTable().fnGetData(row);
        $.each(liquidacaoEvento.EntityList, function (index, value) {
            if (liquidacaoEvento.EntityList[index].NumeroEvento === aData[0]) {
                liquidacaoEvento.EntityList.splice(index, 1);
                return false;
            }
        });

        liquidacaoEvento.table.dataTable().fnDeleteRow(row);

        liquidacaoEvento.sumarize();
    }

    liquidacaoEvento.save = function () {
        if (liquidacaoEvento.numeroEvento.val().length <= 0) {
            AbrirModal("Favor preencher Evento");
            return false;
        }
        if (liquidacaoEvento.inscricaoEvento.val().length <= 0) {
            AbrirModal("Favor preencher Inscrição do Evento");
            return false;
        }

        if (liquidacaoEvento.fonte.val().length <= 0) {
            AbrirModal("Favor preencher Fonte");
            return false;
        }
        if (liquidacaoEvento.valorUnitario.val().replace(/[\.,R$ ]/g, "") <= 0) {
            AbrirModal("Favor preencher Valor do Evento");
            return false;
        }

        var select = liquidacaoEvento.select;
        liquidacaoEvento.buttonNew.show();
        liquidacaoEvento.buttonSave.hide();

        liquidacaoEvento.table.dataTable().fnUpdate(liquidacaoEvento.numeroEvento.val(), liquidacaoEvento.selectedRow, 0, false);
        liquidacaoEvento.table.dataTable().fnUpdate(liquidacaoEvento.inscricaoEvento.val(), liquidacaoEvento.selectedRow, 1, false);
        liquidacaoEvento.table.dataTable().fnUpdate(liquidacaoEvento.classificacao.val(), liquidacaoEvento.selectedRow, 2, false);
        liquidacaoEvento.table.dataTable().fnUpdate(liquidacaoEvento.fonte.val(), liquidacaoEvento.selectedRow, 3, false);
        liquidacaoEvento.table.dataTable().fnUpdate(liquidacaoEvento.valorUnitario.val(), liquidacaoEvento.selectedRow, 4, false);
        liquidacaoEvento.table.dataTable().fnDraw();

        $.each(liquidacaoEvento.EntityList, function (i, value) {
            if (liquidacaoEvento.EntityList[i].NumeroEvento === select) {
                liquidacaoEvento.EntityList[i].NumeroEvento = liquidacaoEvento.numeroEvento.val();
                liquidacaoEvento.EntityList[i].InscricaoEvento = liquidacaoEvento.inscricaoEvento.val();
                liquidacaoEvento.EntityList[i].Classificacao = liquidacaoEvento.classificacao.val();
                liquidacaoEvento.EntityList[i].Fonte = liquidacaoEvento.fonte.val();
                liquidacaoEvento.EntityList[i].ValorUnitario = liquidacaoEvento.valorUnitario.val().replace(/[\.,R$ ]/g, "");
            }
        });

        liquidacaoEvento.clean();

        liquidacaoEvento.selectedRow = "";

        liquidacaoEvento.sumarize();
    }

    liquidacaoEvento.clean = function () {
        liquidacaoEvento.numeroEvento.val('');
        liquidacaoEvento.inscricaoEvento.val('');
        liquidacaoEvento.classificacao.val('');
        liquidacaoEvento.fonte.val('');
        liquidacaoEvento.valorUnitario.val('');
    }


    liquidacaoEvento.sumarize = function () {
        var total = 0;
        $('#tblPesquisaEvento tbody tr').each(function (index, value) {
            var precoTotal = $(this).find('td')[4];
            if (precoTotal != undefined) {
                total += parseInt(precoTotal.innerText.replace(/[\.,R$ ]/g, ""));
            }
        });


        liquidacaoEvento.valorTotal.val(total);
        liquidacaoEvento.valorTotal.maskMoney({ prefix: "R$ ", allowNegative: false, thousands: ".", decimal: ",", affixesStay: true });
        liquidacaoEvento.valorTotal.maskMoney('mask');
        liquidacaoEvento.valorTotal.maskMoney('destroy');

        if (!liquidacao.prodespSelecionada()) {
            liquidacao.valorRealizado.val(total);
            liquidacao.valorRealizado.maskMoney({ prefix: "R$ ", allowNegative: false, thousands: ".", decimal: ",", affixesStay: true });
            liquidacao.valorRealizado.maskMoney('mask');
            liquidacao.valorRealizado.maskMoney('destroy');
        }
    }


    liquidacaoEvento.new = function () {
        liquidacaoEvento.eventos = liquidacaoEvento.EntityList;
    }


    liquidacaoEvento.provider = function () {
        liquidacaoEvento.body
            .on('click', '#newEvento', liquidacaoEvento.newHandler)
            .on('click', '#saveEvento', liquidacaoEvento.saveHandler);
    }

    $(document).on('ready', liquidacaoEvento.init);

})(window, document, jQuery);