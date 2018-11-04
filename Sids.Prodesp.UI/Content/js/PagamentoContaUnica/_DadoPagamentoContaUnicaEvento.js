(function (window, document, $) {
    'use strict';

    window.pagamentoEvento = {};



    pagamentoEvento.init = function () {
        pagamentoEvento.cacheSelectors();
        pagamentoEvento.resetButtons();
        pagamentoEvento.provider();
        debugger
        $('#tblPesquisaEvento').DataTable().order(0, 'desc').draw();
    }

    pagamentoEvento.cacheSelectors = function () {
        pagamentoEvento.body = $('body');
        pagamentoEvento.controller = window.controller;

        pagamentoEvento.controllerReclassific = 'ReclassificacaoRetencao';

        pagamentoEvento.Entity = window.Entity;
        pagamentoEvento.EntityList = window.EventoList;

        pagamentoEvento.objectToCreate = [];
        pagamentoEvento.objectToEdit = {};

        pagamentoEvento.buttonNew = $('#divNewEvento');
        pagamentoEvento.buttonSave = $('#divSaveEvento');
        pagamentoEvento.table = $('#tblPesquisaEvento');

        pagamentoEvento.numeroEvento = $('#NumeroEvento');
        pagamentoEvento.inscricaoEvento = $('#InscricaoEvento');
        pagamentoEvento.classificacao = $('#Classificacao');
        pagamentoEvento.fonte = $('#Fonte');
        pagamentoEvento.valorUnitario = $('#ValorUnitario');
        pagamentoEvento.valorTotal = $('#Total');
        pagamentoEvento.buttonEdit = '<button type="button" title="Alterar" class="btn btn-xs btn-info margL7" onclick="pagamentoEvento.edit(this)"><i class="fa fa-edit"></i></button>';
        pagamentoEvento.buttonRemove = '<button type="button" title="Excluir" class="btn btn-xs btn-danger margL7" onclick="pagamentoEvento.remove(this)"><i class="fa fa-trash"></i></button>';

        pagamentoEvento.eventos = [];
        pagamentoEvento.selectedRow = "";

        pagamentoEvento.newHandler = function (e) {
            e.preventDefault();
            pagamentoEvento.add();
        }

        pagamentoEvento.saveHandler = function (e) {
            e.preventDefault();
            pagamentoEvento.save();
        }
    }

    pagamentoEvento.resetButtons = function () {
        pagamentoEvento.buttonNew.show();
        pagamentoEvento.buttonSave.hide();
    }

    pagamentoEvento.validate = function () {
        var itemExists = $.grep(pagamentoEvento.EntityList, function (e) {
            return e.NumeroEvento === pagamentoEvento.numeroEvento.val();
        });

        return itemExists.length === 0;
    }

    pagamentoEvento.add = function () {
        //if (pagamentoEvento.numeroEvento.val().length <= 0) {
        //    AbrirModal("Favor preencher Evento");
        //    return false;
        //}
        //if (pagamentoEvento.inscricaoEvento.val().length <= 0) {
        //    AbrirModal("Favor preencher Inscrição do Evento");
        //    return false;
        //}

        //if (pagamentoEvento.fonte.val().length <= 0) {
        //    AbrirModal("Favor preencher Fonte");
        //    return false;
        //}
        //if (pagamentoEvento.valorUnitario.val().replace(/[\.,R$ ]/g, "") <= 0) {
        //    AbrirModal("Favor preencher Valor do Evento");
        //    return false;
        //}

        pagamentoEvento.objectToCreate = [
            pagamentoEvento.numeroEvento.val(),
            pagamentoEvento.inscricaoEvento.val(),
            pagamentoEvento.classificacao.val(),
            pagamentoEvento.fonte.val(),
            pagamentoEvento.valorUnitario.val(),
            pagamentoEvento.buttonEdit + pagamentoEvento.buttonRemove
        ];

        pagamentoEvento.objectToEdit = {
            Id: 0,
            SubempenhoId: pagamentoEvento.Entity.Id,
            NumeroEvento: pagamentoEvento.numeroEvento.val(),
            InscricaoEvento: pagamentoEvento.inscricaoEvento.val(),
            Classificacao: pagamentoEvento.classificacao.val(),
            Fonte: pagamentoEvento.fonte.val(),
            ValorUnitario: pagamentoEvento.valorUnitario.val().replace("R$ ", "").replace(/[.,]/g, "") 
        };

        if (pagamentoEvento.validate() === false) {
            AbrirModal("Não é permitido incluir eventos com o mesmo número.");
            return false;
        }

        pagamentoEvento.EntityList[pagamentoEvento.EntityList.length] = pagamentoEvento.objectToEdit;

        pagamentoEvento.new();

        pagamentoEvento.table.dataTable().fnAddData(pagamentoEvento.objectToCreate);

        pagamentoEvento.clean();

        pagamentoEvento.selectedRow = "";

        pagamentoEvento.sumarize();
    }

    pagamentoEvento.select = "";

    pagamentoEvento.edit = function (nRow) {

        pagamentoEvento.select = "";
        pagamentoEvento.buttonNew.hide();
        pagamentoEvento.buttonSave.show();

        pagamentoEvento.selectedRow = $(nRow).parent();
        var aData = pagamentoEvento.table.dataTable().fnGetData(pagamentoEvento.selectedRow);
        pagamentoEvento.select = aData[0];
        pagamentoEvento.numeroEvento.val(aData[0]);
        pagamentoEvento.inscricaoEvento.val(aData[1]);
        pagamentoEvento.classificacao.val(aData[2]);
        pagamentoEvento.fonte.val(aData[3]);
        pagamentoEvento.valorUnitario.val(aData[4]);

        pagamentoEvento.sumarize();
    }

    pagamentoEvento.remove = function (nRow) {
        var row = $(nRow).parent();

        var aData = pagamentoEvento.table.dataTable().fnGetData(row);
        $.each(pagamentoEvento.EntityList, function (index, value) {
            if (pagamentoEvento.EntityList[index].NumeroEvento === aData[0]) {
                pagamentoEvento.EntityList.splice(index, 1);
                return false;
            }
        });

        pagamentoEvento.table.dataTable().fnDeleteRow(row);

        pagamentoEvento.sumarize();
    }

    pagamentoEvento.save = function () {
        if (pagamentoEvento.numeroEvento.val().length <= 0) {
            AbrirModal("Favor preencher Evento");
            return false;
        }
        if (pagamentoEvento.inscricaoEvento.val().length <= 0) {
            AbrirModal("Favor preencher Inscrição do Evento");
            return false;
        }
        if (pagamentoEvento.fonte.val().length <= 0) {
            AbrirModal("Favor preencher Fonte");
            return false;
        }
        if (pagamentoEvento.valorUnitario.val().replace(/[\.,R$ ]/g, "") <= 0) {
            AbrirModal("Favor preencher Valor do Evento");
            return false;
        }

        var select = pagamentoEvento.select;
        pagamentoEvento.buttonNew.show();
        pagamentoEvento.buttonSave.hide();

        pagamentoEvento.table.dataTable().fnUpdate(pagamentoEvento.numeroEvento.val(), pagamentoEvento.selectedRow, 0, false);
        pagamentoEvento.table.dataTable().fnUpdate(pagamentoEvento.inscricaoEvento.val(), pagamentoEvento.selectedRow, 1, false);
        pagamentoEvento.table.dataTable().fnUpdate(pagamentoEvento.classificacao.val(), pagamentoEvento.selectedRow, 2, false);
        pagamentoEvento.table.dataTable().fnUpdate(pagamentoEvento.fonte.val(), pagamentoEvento.selectedRow, 3, false);
        pagamentoEvento.table.dataTable().fnUpdate(pagamentoEvento.valorUnitario.val(), pagamentoEvento.selectedRow, 4, false);
        pagamentoEvento.table.dataTable().fnDraw();

        $.each(pagamentoEvento.EntityList, function (i, value) {
            if (pagamentoEvento.EntityList[i].NumeroEvento === select) {
                pagamentoEvento.EntityList[i].NumeroEvento = pagamentoEvento.numeroEvento.val();
                pagamentoEvento.EntityList[i].InscricaoEvento = pagamentoEvento.inscricaoEvento.val();
                pagamentoEvento.EntityList[i].Classificacao = pagamentoEvento.classificacao.val();
                pagamentoEvento.EntityList[i].Fonte = pagamentoEvento.fonte.val();
                pagamentoEvento.EntityList[i].ValorUnitario = pagamentoEvento.valorUnitario.val().replace(/[\.,R$ ]/g, "");
            }
        });

        pagamentoEvento.clean();

        pagamentoEvento.selectedRow = "";

        pagamentoEvento.sumarize();
    }

    pagamentoEvento.clean = function () {
        pagamentoEvento.numeroEvento.val('');
        pagamentoEvento.inscricaoEvento.val('');
        pagamentoEvento.classificacao.val('');
        pagamentoEvento.fonte.val('');
        pagamentoEvento.valorUnitario.val('');
    }


    pagamentoEvento.sumarize = function () {
        var total = 0;
        $('#tblPesquisaEvento tbody tr').each(function (index, value) {
            var precoTotal = $(this).find('td')[4];
            if (precoTotal != undefined) {
                total += parseInt(precoTotal.innerText.replace(/[\.,R$ ]/g, ""));
            }
        });
       
        
        pagamentoEvento.valorTotal.val(total); 
        pagamentoEvento.valorTotal.maskMoney({ prefix: "R$ ", allowNegative: false, thousands: ".", decimal: ",", affixesStay: true });
        pagamentoEvento.valorTotal.maskMoney('mask');
        pagamentoEvento.valorTotal.maskMoney('destroy');
        
    }


    pagamentoEvento.new = function () {
        pagamentoEvento.eventos = pagamentoEvento.EntityList;
    }


    pagamentoEvento.provider = function () {
        pagamentoEvento.body
            .on('click', '#newEvento', pagamentoEvento.newHandler)
            .on('click', '#saveEvento', pagamentoEvento.saveHandler);
    }

    $(document).on('ready', pagamentoEvento.init);

})(window, document, jQuery);