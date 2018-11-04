(function (window, document, $) {
    'use strict';

    window.listaDeBoletosCodicoBarras = {};
    
    listaDeBoletosCodicoBarras.init = function () {
        listaDeBoletosCodicoBarras.cacheSelectors();
        listaDeBoletosCodicoBarras.resetButtons();
        listaDeBoletosCodicoBarras.provider();
    }

    listaDeBoletosCodicoBarras.cacheSelectors = function () {
        listaDeBoletosCodicoBarras.body = $('body');
        listaDeBoletosCodicoBarras.controller = window.controller;

        listaDeBoletosCodicoBarras.controllerListaDeBoletos = 'ListaDeBoletos';

        listaDeBoletosCodicoBarras.Entity = window.Entity;
        listaDeBoletosCodicoBarras.EntityList = window.EntityList;

        listaDeBoletosCodicoBarras.objectToCreate = [];
        listaDeBoletosCodicoBarras.objectToEdit = {};

        listaDeBoletosCodicoBarras.buttonNew = $('#divNewCodigo');
        listaDeBoletosCodicoBarras.buttonDigitar = $('#divDigitarCodigo');
        listaDeBoletosCodicoBarras.buttonSave = $('#divSaveCodigo');
        listaDeBoletosCodicoBarras.table = $('#tblCodigoBarras');
        listaDeBoletosCodicoBarras.tipo = $("#TipoBoleto");
        listaDeBoletosCodicoBarras.codigoDeBarras = $("#CodigoDeBarras");
        listaDeBoletosCodicoBarras.valorTotal = $("#ValorTotalLista");
        listaDeBoletosCodicoBarras.atde = $("#TotalCredores");

        listaDeBoletosCodicoBarras.numeroTaxa1 = $('#NumeroTaxa1');
        listaDeBoletosCodicoBarras.numeroTaxa2 = $('#NumeroTaxa2');
        listaDeBoletosCodicoBarras.numeroTaxa3 = $('#NumeroTaxa3');
        listaDeBoletosCodicoBarras.numeroTaxa4 = $('#NumeroTaxa4');

        listaDeBoletosCodicoBarras.numeroBoleto1 = $('#NumeroBoleto1');
        listaDeBoletosCodicoBarras.numeroBoleto2 = $('#NumeroBoleto2');
        listaDeBoletosCodicoBarras.numeroBoleto3 = $('#NumeroBoleto3');
        listaDeBoletosCodicoBarras.numeroBoleto4 = $('#NumeroBoleto4');
        listaDeBoletosCodicoBarras.numeroBoleto5 = $('#NumeroBoleto5');
        listaDeBoletosCodicoBarras.numeroBoleto6 = $('#NumeroBoleto6');
        listaDeBoletosCodicoBarras.numeroDigito = $('#NumeroDigito');
        listaDeBoletosCodicoBarras.numeroBoleto7 = $('#NumeroBoleto7');

        listaDeBoletosCodicoBarras.buttonEdit ='<button type="button" title="Alterar" class="btn btn-xs btn-info margL7   btn-edit lockProdesp lockSiafem" onclick="listaDeBoletosCodicoBarras.edit(this)"><i class="fa fa-edit"></i></button>';
        listaDeBoletosCodicoBarras.buttonRemove ='<button type="button" title="Excluir" class="btn btn-xs btn-danger margL7  btn-trash lockProdesp lockSiafem" onclick="listaDeBoletosCodicoBarras.remove(this)"><i class="fa fa-trash"></i></button>';
        //listaDeBoletosCodicoBarras.buttonEdit = '<button type="button" title="Alterar" class="btn btn-xs btn-info margL7 lockSiafem" onclick="listaDeBoletosCodicoBarras.edit(this)"><i class="fa fa-edit"></i></button>';
        //listaDeBoletosCodicoBarras.buttonRemove = '<button type="button" title="Excluir" class="btn btn-xs btn-danger margL7 lockSiafem" onclick="listaDeBoletosCodicoBarras.remove(this)"><i class="fa fa-trash"></i></button>';

        listaDeBoletosCodicoBarras.eventos = [];
        listaDeBoletosCodicoBarras.selectedRow = "";

        listaDeBoletosCodicoBarras.newHandler = function (e) {
            e.preventDefault();
            listaDeBoletosCodicoBarras.add();
        }

        listaDeBoletosCodicoBarras.saveHandler = function (e) {
            e.preventDefault();
            listaDeBoletosCodicoBarras.save();
        }
    }

    listaDeBoletosCodicoBarras.resetButtons = function () {
        listaDeBoletosCodicoBarras.buttonNew.show();
        listaDeBoletosCodicoBarras.buttonDigitar.hide();
        listaDeBoletosCodicoBarras.buttonSave.hide();
    }

    listaDeBoletosCodicoBarras.validate = function () {
        var itemExists = $.grep(listaDeBoletosCodicoBarras.EntityList, function (e) {
            return listaDeBoletosCodicoBarras.concatEntityCodigo(e) === listaDeBoletosCodicoBarras.concatCodigo() && listaDeBoletosCodicoBarras.select !== listaDeBoletosCodicoBarras.concatCodigo();
        });

        return itemExists.length === 0;
    }

    listaDeBoletosCodicoBarras.add = function () {

        listaDeBoletosCodicoBarras.formatCode();

        if (listaDeBoletosCodicoBarras.validateForm() === true) {
            AbrirModal("O código de barras inválido!");
            return false;
        }

        listaDeBoletosCodicoBarras.objectToCreate = [
            listaDeBoletosCodicoBarras.concatCodigo(),
            listaDeBoletosCodicoBarras.getValue(),
            listaDeBoletosCodicoBarras.buttonEdit + listaDeBoletosCodicoBarras.buttonRemove
        ];

        listaDeBoletosCodicoBarras.objectToEdit = {
            Id: 0,
            ListaBoletosId: listaDeBoletosCodicoBarras.Entity.Id,
            TipoBoletoId: listaDeBoletosCodicoBarras.tipo.val(),
            Valor: listaDeBoletosCodicoBarras.getValue().replace(/[\R$ .]/g, ""),
            CodigoBarraTaxa: listaDeBoletosCodicoBarras.factoryTaxa(),
            CodigoBarraBoleto: listaDeBoletosCodicoBarras.factoryBoleto()
        };

        if (listaDeBoletosCodicoBarras.validate() === false) {
            AbrirModal("Não é permitido incluir o mesmo número de Codigo de Barras  .");
            return false;
        }

        listaDeBoletosCodicoBarras.EntityList[listaDeBoletosCodicoBarras.EntityList.length] = listaDeBoletosCodicoBarras.objectToEdit;

        listaDeBoletosCodicoBarras.new();

        listaDeBoletosCodicoBarras.table.dataTable().fnAddData(listaDeBoletosCodicoBarras.objectToCreate);

       
        listaDeBoletosCodicoBarras.limparCampos();
        listaDeBoletosCodicoBarras.selectedRow = "";

        listaDeBoletosCodicoBarras.sumarize();


        listaDeBoletos.valueCenarioTipoBoleto.val('');
    }

    listaDeBoletosCodicoBarras.select = "";

    listaDeBoletosCodicoBarras.edit = function (nRow) {

        listaDeBoletosCodicoBarras.select = "";
        listaDeBoletosCodicoBarras.buttonNew.hide();
        listaDeBoletosCodicoBarras.buttonSave.show();

        listaDeBoletosCodicoBarras.selectedRow = $(nRow).parent();
        var aData = listaDeBoletosCodicoBarras.table.dataTable().fnGetData(listaDeBoletosCodicoBarras.selectedRow);

        listaDeBoletosCodicoBarras.select = aData[0];

        RefazerCombo("#TipoBoleto");
        if (aData[0].length === 51) {
            listaDeBoletosCodicoBarras.numeroTaxa1.val(aData[0].split(".")[0]);
            listaDeBoletosCodicoBarras.numeroTaxa2.val(aData[0].split(".")[1]);
            listaDeBoletosCodicoBarras.numeroTaxa3.val(aData[0].split(".")[2]);
            listaDeBoletosCodicoBarras.numeroTaxa4.val(aData[0].split(".")[3]);
            $("#TipoBoleto option[value='1']").attr("selected", true);
        } else {
            listaDeBoletosCodicoBarras.numeroBoleto1.val(aData[0].split(".")[0]);
            listaDeBoletosCodicoBarras.numeroBoleto2.val(aData[0].split(".")[1]);
            listaDeBoletosCodicoBarras.numeroBoleto3.val(aData[0].split(".")[2]);
            listaDeBoletosCodicoBarras.numeroBoleto4.val(aData[0].split(".")[3]);
            listaDeBoletosCodicoBarras.numeroBoleto5.val(aData[0].split(".")[4]);
            listaDeBoletosCodicoBarras.numeroBoleto6.val(aData[0].split(".")[5]);
            listaDeBoletosCodicoBarras.numeroDigito.val(aData[0].split(".")[6]);
            listaDeBoletosCodicoBarras.numeroBoleto7.val(aData[0].split(".")[7]);
            $("#TipoBoleto option[value='2']").attr("selected", true);
        }

        $("#TipoBoleto").trigger("change");
    }

    listaDeBoletosCodicoBarras.remove = function (nRow) {
        var row = $(nRow).parent();

        var aData = listaDeBoletosCodicoBarras.table.dataTable().fnGetData(row);
        $.each(listaDeBoletosCodicoBarras.EntityList, function (index, value) {
            if (listaDeBoletosCodicoBarras.concatEntityCodigo(listaDeBoletosCodicoBarras.EntityList[index]) === aData[0]) {
                listaDeBoletosCodicoBarras.EntityList.splice(index, 1);
                return false;
            }
        });

        listaDeBoletosCodicoBarras.table.dataTable().fnDeleteRow(row);
        listaDeBoletosCodicoBarras.sumarize();
    }

    listaDeBoletosCodicoBarras.save = function () {
        if (listaDeBoletosCodicoBarras.validateForm() === true) {
            AbrirModal("O código de barras inválido!!");
            return false;
        }

        if (listaDeBoletosCodicoBarras.validate() === false) {
            AbrirModal("Não é permitido incluir o mesmo número de Codigo de Barras  .");
            return false;
        }

        var select = listaDeBoletosCodicoBarras.select;
        listaDeBoletosCodicoBarras.buttonNew.show();
        listaDeBoletosCodicoBarras.buttonSave.hide();

        listaDeBoletosCodicoBarras.table.dataTable().fnUpdate(listaDeBoletosCodicoBarras.concatCodigo(), listaDeBoletosCodicoBarras.selectedRow, 0, false);
        listaDeBoletosCodicoBarras.table.dataTable().fnUpdate(listaDeBoletosCodicoBarras.getValue(), listaDeBoletosCodicoBarras.selectedRow, 1, false);
        listaDeBoletosCodicoBarras.table.dataTable().fnDraw();

        $.each(listaDeBoletosCodicoBarras.EntityList, function (i, value) {
            if (listaDeBoletosCodicoBarras.concatEntityCodigo(listaDeBoletosCodicoBarras.EntityList[i]) === select) {
                listaDeBoletosCodicoBarras.EntityList[i].ListaBoletosId = listaDeBoletosCodicoBarras.Entity.Id;
                listaDeBoletosCodicoBarras.EntityList[i].TipoBoletoId = listaDeBoletosCodicoBarras.tipo.val();
                listaDeBoletosCodicoBarras.EntityList[i].Valor = listaDeBoletosCodicoBarras.getValue().replace(/[\R$ .]/g, "");
                listaDeBoletosCodicoBarras.EntityList[i].CodigoBarraTaxa = listaDeBoletosCodicoBarras.factoryTaxa();
                listaDeBoletosCodicoBarras.EntityList[i].CodigoBarraBoleto = listaDeBoletosCodicoBarras.factoryBoleto();
            }
        });

        listaDeBoletosCodicoBarras.clean();

        listaDeBoletosCodicoBarras.selectedRow = "";

        listaDeBoletosCodicoBarras.sumarize();


    }

    listaDeBoletosCodicoBarras.clean = function () {
        listaDeBoletosCodicoBarras.numeroBoleto1.val('');
        listaDeBoletosCodicoBarras.numeroBoleto2.val('');
        listaDeBoletosCodicoBarras.numeroBoleto3.val('');
        listaDeBoletosCodicoBarras.numeroBoleto4.val('');
        listaDeBoletosCodicoBarras.numeroBoleto5.val('');
        listaDeBoletosCodicoBarras.numeroBoleto6.val('');
        listaDeBoletosCodicoBarras.numeroDigito.val('');
        listaDeBoletosCodicoBarras.numeroBoleto7.val('');

        listaDeBoletosCodicoBarras.numeroTaxa1.val('');
        listaDeBoletosCodicoBarras.numeroTaxa2.val('');
        listaDeBoletosCodicoBarras.numeroTaxa3.val('');
        listaDeBoletosCodicoBarras.numeroTaxa4.val('');
        listaDeBoletosCodicoBarras.codigoDeBarras.val('');
        RefazerCombo("#TipoBoleto");
        listaDeBoletosCodicoBarras.tipo.trigger('change');
        listaDeBoletosCodicoBarras.select = "";
    }

    listaDeBoletosCodicoBarras.limparCampos = function () {
        listaDeBoletosCodicoBarras.numeroBoleto1.val('');
        listaDeBoletosCodicoBarras.numeroBoleto2.val('');
        listaDeBoletosCodicoBarras.numeroBoleto3.val('');
        listaDeBoletosCodicoBarras.numeroBoleto4.val('');
        listaDeBoletosCodicoBarras.numeroBoleto5.val('');
        listaDeBoletosCodicoBarras.numeroBoleto6.val('');
        listaDeBoletosCodicoBarras.numeroDigito.val('');
        listaDeBoletosCodicoBarras.numeroBoleto7.val('');

        listaDeBoletosCodicoBarras.numeroTaxa1.val('');
        listaDeBoletosCodicoBarras.numeroTaxa2.val('');
        listaDeBoletosCodicoBarras.numeroTaxa3.val('');
        listaDeBoletosCodicoBarras.numeroTaxa4.val('');
        listaDeBoletosCodicoBarras.codigoDeBarras.val('');
    }

    listaDeBoletosCodicoBarras.validateForm = function () {
        var retorno = false;
        if (listaDeBoletosCodicoBarras.tipo.val() == 1) {

            if (listaDeBoletosCodicoBarras.numeroTaxa1.val().length < 12)
                retorno = true;
            else if (listaDeBoletosCodicoBarras.numeroTaxa2.val().length < 12)
                retorno = true;
            else if (listaDeBoletosCodicoBarras.numeroTaxa3.val().length < 12)
                retorno = true;
            else if (listaDeBoletosCodicoBarras.numeroTaxa4.val().length < 12)
                retorno = true;
        } else if (listaDeBoletosCodicoBarras.tipo.val() == 2) {

            if (listaDeBoletosCodicoBarras.numeroBoleto1.val().length < 5)
                retorno = true;
            else if (listaDeBoletosCodicoBarras.numeroBoleto2.val().length < 5)
                retorno = true;
            else if (listaDeBoletosCodicoBarras.numeroBoleto3.val().length < 5)
                retorno = true;
            else if (listaDeBoletosCodicoBarras.numeroBoleto4.val().length < 6)
                retorno = true;
            else if (listaDeBoletosCodicoBarras.numeroBoleto5.val().length < 5)
                retorno = true;
            else if (listaDeBoletosCodicoBarras.numeroBoleto6.val().length < 6)
                retorno = true;
            else if (listaDeBoletosCodicoBarras.numeroDigito.val().length < 1)
                retorno = true;
            else if (listaDeBoletosCodicoBarras.numeroBoleto7.val().length < 14)
                retorno = true;
        } else {
            retorno = true;
        }

        return retorno;

    }

    listaDeBoletosCodicoBarras.sumarize = function () {
        //var total = 0;
        //var qtd = 0;
        //$('#tblCodigoBarras tbody tr').each(function (index, value) {
        //    var precoTotal = $(this).find('td')[1];
        //    if (precoTotal != undefined) {
        //        total += parseInt(precoTotal.innerText.replace(/[\.,R$ ]/g, ""));
        //        qtd += 1;
        //    }
        //});

        //listaDeBoletosCodicoBarras.atde.val(qtd);
        //listaDeBoletosCodicoBarras.valorTotal.val(total);
        //listaDeBoletosCodicoBarras.valorTotal.maskMoney({ prefix: "R$ ", allowNegative: false, thousands: ".", decimal: ",", affixesStay: true });
        //listaDeBoletosCodicoBarras.valorTotal.maskMoney('mask');
        //listaDeBoletosCodicoBarras.valorTotal.maskMoney('destroy');

    }

    listaDeBoletosCodicoBarras.concatEntityCodigo = function (entity) {
        var retorno = "";
        if (entity.TipoBoletoId == 1) {

            retorno = StringIsNull(entity.CodigoBarraTaxa.NumeroConta1, "")
                .concat(
                    ".",
                    StringIsNull(entity.CodigoBarraTaxa.NumeroConta2,""),
                    ".",
                    StringIsNull(entity.CodigoBarraTaxa.NumeroConta3,""),
                    ".",
                    StringIsNull(entity.CodigoBarraTaxa.NumeroConta4, ""));

        }

        if (entity.TipoBoletoId == 2) {

            retorno = StringIsNull(entity.CodigoBarraBoleto.NumeroConta1,"")
                .concat(
                    ".",
                    StringIsNull(entity.CodigoBarraBoleto.NumeroConta2,""),
                    ".",
                    StringIsNull(entity.CodigoBarraBoleto.NumeroConta3,""),
                    ".",
                    StringIsNull(entity.CodigoBarraBoleto.NumeroConta4,""),
                    ".",
                    StringIsNull(entity.CodigoBarraBoleto.NumeroConta5,""),
                    ".",
                    StringIsNull(entity.CodigoBarraBoleto.NumeroConta6,""),
                    ".",
                    StringIsNull(entity.CodigoBarraBoleto.NumeroDigito,""),
                    ".",
                    StringIsNull(entity.CodigoBarraBoleto.NumeroConta7, ""));
        }

        return retorno;
    }

    listaDeBoletosCodicoBarras.concatCodigo = function () {
        var retorno = "";
        if (listaDeBoletosCodicoBarras.tipo.val() == 1) {

            retorno = listaDeBoletosCodicoBarras.numeroTaxa1.val()
                .concat(
                    ".",
                    listaDeBoletosCodicoBarras.numeroTaxa2.val(),
                    ".",
                    listaDeBoletosCodicoBarras.numeroTaxa3.val(),
                    ".",
                    listaDeBoletosCodicoBarras.numeroTaxa4.val());

        }

        if (listaDeBoletosCodicoBarras.tipo.val() == 2) {

            retorno = listaDeBoletosCodicoBarras.numeroBoleto1.val()
                .concat(
                    ".",
                    listaDeBoletosCodicoBarras.numeroBoleto2.val(),
                    ".",
                    listaDeBoletosCodicoBarras.numeroBoleto3.val(),
                    ".",
                    listaDeBoletosCodicoBarras.numeroBoleto4.val(),
                    ".",
                    listaDeBoletosCodicoBarras.numeroBoleto5.val(),
                    ".",
                    listaDeBoletosCodicoBarras.numeroBoleto6.val(),
                    ".",
                    listaDeBoletosCodicoBarras.numeroDigito.val(),
                    ".",
                    listaDeBoletosCodicoBarras.numeroBoleto7.val());
        }

        return retorno;
    }

    listaDeBoletosCodicoBarras.getValue = function () {
        var valorTotal = 0;
        if (listaDeBoletosCodicoBarras.tipo.val() == 1) {

            valorTotal = (listaDeBoletosCodicoBarras.numeroTaxa1.val().substring(5, 11).concat(
                    Left(listaDeBoletosCodicoBarras.numeroTaxa2.val(), 4)) / 100);

        }

        if (listaDeBoletosCodicoBarras.tipo.val() == 2) {

            valorTotal = listaDeBoletosCodicoBarras.numeroBoleto7.val().substring(5, 14) / 100;
        }

        var total = parseFloat(valorTotal.toFixed(2));
        var valor = String(total).replace(".", ",");
        valor = valor.indexOf(',') == -1 ? valor + ",00" : valor;
        valor = valor.length - valor.indexOf(',') < 3 ? valor + "0" : valor;
        return "R$ " + MaskMonetario(valor);

    }

    listaDeBoletosCodicoBarras.factoryTaxa = function () {
        if (listaDeBoletosCodicoBarras.tipo.val() == 1)
            return {
                Id: 0,
                NumeroConta1: listaDeBoletosCodicoBarras.numeroTaxa1.val(),
                NumeroConta2: listaDeBoletosCodicoBarras.numeroTaxa2.val(),
                NumeroConta3: listaDeBoletosCodicoBarras.numeroTaxa3.val(),
                NumeroConta4: listaDeBoletosCodicoBarras.numeroTaxa4.val(),
                CodigoBarraId: 0
            };
        else
            return {}
    }

    listaDeBoletosCodicoBarras.factoryBoleto = function () {
        if (listaDeBoletosCodicoBarras.tipo.val() == 2)
            return {
                Id: 0,
                NumeroConta1: listaDeBoletosCodicoBarras.numeroBoleto1.val(),
                NumeroConta2: listaDeBoletosCodicoBarras.numeroBoleto2.val(),
                NumeroConta3: listaDeBoletosCodicoBarras.numeroBoleto3.val(),
                NumeroConta4: listaDeBoletosCodicoBarras.numeroBoleto4.val(),
                NumeroConta5: listaDeBoletosCodicoBarras.numeroBoleto5.val(),
                NumeroConta6: listaDeBoletosCodicoBarras.numeroBoleto6.val(),
                NumeroDigito: listaDeBoletosCodicoBarras.numeroDigito.val(),
                NumeroConta7: listaDeBoletosCodicoBarras.numeroBoleto7.val(),
                CodigoBarraId: 0
            };
        else
            return {}
    }

    listaDeBoletosCodicoBarras.new = function () {
        listaDeBoletosCodicoBarras.ListaCodigoBarras = listaDeBoletosCodicoBarras.EntityList;
    }


    listaDeBoletosCodicoBarras.formatCode = function () {
        if (listaDeBoletosCodicoBarras.codigoDeBarras.val().length < 47 || listaDeBoletosCodicoBarras.codigoDeBarras.val().length > 48) {
            return false;
        }
        RefazerCombo("#TipoBoleto");
        if (listaDeBoletosCodicoBarras.codigoDeBarras.val().length == 48) {

            listaDeBoletosCodicoBarras.numeroTaxa1.val(listaDeBoletosCodicoBarras.codigoDeBarras.val().substr(0, 12));
            listaDeBoletosCodicoBarras.numeroTaxa2.val(listaDeBoletosCodicoBarras.codigoDeBarras.val().substr(12, 12));
            listaDeBoletosCodicoBarras.numeroTaxa3.val(listaDeBoletosCodicoBarras.codigoDeBarras.val().substr(24, 12));
            listaDeBoletosCodicoBarras.numeroTaxa4.val(listaDeBoletosCodicoBarras.codigoDeBarras.val().substr(36, 12));
            $("#TipoBoleto option[value='1']").attr("selected", true);
        } else {

            listaDeBoletosCodicoBarras.numeroBoleto1.val(listaDeBoletosCodicoBarras.codigoDeBarras.val().substr(0, 5));
            listaDeBoletosCodicoBarras.numeroBoleto2.val(listaDeBoletosCodicoBarras.codigoDeBarras.val().substr(5, 5));
            listaDeBoletosCodicoBarras.numeroBoleto3.val(listaDeBoletosCodicoBarras.codigoDeBarras.val().substr(10, 5));
            listaDeBoletosCodicoBarras.numeroBoleto4.val(listaDeBoletosCodicoBarras.codigoDeBarras.val().substr(15, 6));
            listaDeBoletosCodicoBarras.numeroBoleto5.val(listaDeBoletosCodicoBarras.codigoDeBarras.val().substr(21, 5));
            listaDeBoletosCodicoBarras.numeroBoleto6.val(listaDeBoletosCodicoBarras.codigoDeBarras.val().substr(26, 6));
            listaDeBoletosCodicoBarras.numeroDigito.val(listaDeBoletosCodicoBarras.codigoDeBarras.val().substr(32, 1));
            listaDeBoletosCodicoBarras.numeroBoleto7.val(listaDeBoletosCodicoBarras.codigoDeBarras.val().substr(33, 14));
            $("#TipoBoleto option[value='2']").attr("selected", true);
        }
        //$("#TipoBoleto").trigger("change");
    }

    listaDeBoletosCodicoBarras.provider = function () {
        listaDeBoletosCodicoBarras.body
            .on('click', '#newCodigo', listaDeBoletosCodicoBarras.newHandler)
            .on('click', '#saveCodigo', listaDeBoletosCodicoBarras.saveHandler)
            .on('click', '#digitarCodigo', listaDeBoletos.displayHandlerChange) // retorna para cenario de digitação cod. Barras, de acordo com item selecionado na combo.
        
        //.on('blur', '#CodigoDeBarras', listaDeBoletosCodicoBarras.formatCode);
    }

    $(document).on('ready', listaDeBoletosCodicoBarras.init);

})(window, document, jQuery);