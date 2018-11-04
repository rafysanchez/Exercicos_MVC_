(function (window, document, $) {
    'use strict';

    window.desdobramentoList = {};


    desdobramentoList.init = function () {
        desdobramentoList.cacheSelectors();
        desdobramentoList.resetButtons();
        desdobramentoList.provider();
    }

    desdobramentoList.cacheSelectors = function () {
        desdobramentoList.body = $('body');
        desdobramentoList.controller = window.controller;

        desdobramentoList.controllerInclusao = 'Subempenho';
        desdobramentoList.controllerAnulacao = 'SubempenhoCancelamento';

        desdobramentoList.Entity = window.Entity;

        desdobramentoList.objectToCreate = [];
        desdobramentoList.objectToEdit = {};

        desdobramentoList.buttonNew = $('#divnewIdenficacao');
        desdobramentoList.buttonSave = $('#divsaveIdentificacao');
        desdobramentoList.tableIssqn = $('#tblPesquisaIssqn');
        desdobramentoList.tableOutros = $('#tblPesquisaOutros');

        desdobramentoList.ValorPercentual = $('#ValorPercentual');
        desdobramentoList.Tipo = $('#Tipo');
        desdobramentoList.ReduzidoCredor = $('#ReduzidoCredor');
        desdobramentoList.ValorDistribuicao = $('#ValorDistribuicao');
        desdobramentoList.BaseCalc = $('#BaseCalc');
        desdobramentoList.ValorDesdobrado = $('#ValorDesdobrado');
        desdobramentoList.ReterId = $('#ReterId');
        desdobramentoList.buttonEditIssqn = '<button type="button" title="Alterar" class="btn btn-xs btn-info margL7 lockProdesp" onclick="desdobramentoList.editIssqn(this)"><i class="fa fa-edit"></i></button>';
        desdobramentoList.buttonEditOutros = '<button type="button" title="Alterar" class="btn btn-xs btn-info margL7 lockProdesp" onclick="desdobramentoList.editOutros(this)"><i class="fa fa-edit"></i></button>';
        desdobramentoList.buttonRemoveIssqn = '<button type="button" title="Excluir" class="btn btn-xs btn-danger margL7 lockProdesp" onclick="desdobramentoList.removeIssqn(this)"><i class="fa fa-trash"></i></button>';
        desdobramentoList.buttonRemove = '<button type="button" title="Excluir" class="btn btn-xs btn-danger margL7 lockProdesp" onclick="desdobramentoList.remove(this)"><i class="fa fa-trash"></i></button>';

        desdobramentoList.indetificacao = [];
        desdobramentoList.selectedRow = "";



        desdobramentoList.newHandler = function (e) {
            e.preventDefault();
            if ($('#DesdobramentoTipoId').val() == 1)
                desdobramentoList.addIssqn();
            else
                desdobramentoList.addOutros();
        }

        desdobramentoList.saveHandler = function (e) {
            e.preventDefault();

            if ($('#DesdobramentoTipoId').val() == 1)
                desdobramentoList.saveIssqn();
            else
                desdobramentoList.saveOutros();
        }

        desdobramentoList.disabledField = function () {


            desdobramentoList.ValorDesdobrado.removeAttr("disabled");
            desdobramentoList.ValorPercentual.removeAttr("disabled");

            if (desdobramentoList.ValorPercentual.val().length > 0) {
                desdobramentoList.ValorDesdobrado.val("");
                desdobramentoList.ValorDesdobrado.attr("disabled", true);
            }
            else
                if (desdobramentoList.ValorDesdobrado.val().length > 0) {
                    desdobramentoList.ValorPercentual.val("");
                    desdobramentoList.ValorPercentual.attr("disabled", true);
                }
        }
    }

    desdobramentoList.resetButtons = function () {
        desdobramentoList.buttonNew.show();
        desdobramentoList.buttonSave.hide();
    }

    desdobramentoList.validate = function () {

        var itemExists = $.grep(desdobramentoList.EntityList, function (e) {
            return e.NomeReduzidoCredor.toUpperCase() === desdobramentoList.ReduzidoCredor.val().toUpperCase() && desdobramentoList.select.toUpperCase() !== desdobramentoList.ReduzidoCredor.val().toUpperCase();
        });

        return itemExists.length === 0;

    }

    desdobramentoList.addIssqn = function () {
        if (desdobramentoList.validateForm() != "") {
            AbrirModal(desdobramentoList.validateForm());
            return false;
        }



        desdobramentoList.objectToCreate = [
            $("#Tipo :selected").text(),
            desdobramentoList.ReduzidoCredor.val(),
            desdobramentoList.ValorDistribuicao.val(),
            desdobramentoList.BaseCalc.val() + "%",
            desdobramentoList.ValorDesdobrado.val(),
            desdobramentoList.ReterId.val() == "" ? "" : $("#ReterId :selected").text(),
            desdobramentoList.buttonEditIssqn + desdobramentoList.buttonRemoveIssqn
        ];

        desdobramentoList.objectToEdit = {
            Id: 0,
            Desdobramento: desdobramentoList.Entity.Id,
            DesdobramentoTipoId: desdobramentoList.Tipo.val(),
            NomeReduzidoCredor: desdobramentoList.ReduzidoCredor.val(),
            ValorPercentual: desdobramentoList.BaseCalc.val(),
            ValorDesdobrado: desdobramentoList.ValorDesdobrado.val().replace(/[\R$ .]/g, ""),
            ValorDistribuicao: desdobramentoList.ValorDistribuicao.val().replace(/[\R$ .]/g, ""),
            ValorDesdobradoInicial: desdobramentoList.ValorDesdobrado.val().replace(/[\R$ .]/g, ""),
            ReterId: desdobramentoList.ReterId.val(),
            Sequencia: parseInt(desdobramentoList.EntityList.length + 1)
        };

        if (desdobramentoList.validate() === false) {
            AbrirModal("Não é permitido incluir com o mesmo Nome de Credor.");
            return false;
        }


        if (desdobramentoList.EntityList.length === 130) {
            AbrirModal("Excede ao número máximo(130) de registros permitos!");
            return false;
        }

        desdobramentoList.EntityList[desdobramentoList.EntityList.length] = desdobramentoList.objectToEdit;

        desdobramentoList.new();

        desdobramentoList.tableIssqn.dataTable().fnAddData(desdobramentoList.objectToCreate);

        desdobramentoList.clean();

        desdobramentoList.selectedRow = "";

        desdobramento.EntityListISSQN = desdobramentoList.EntityList;

        desdobramentoList.Tipo.focus();

    }

    desdobramentoList.addOutros = function () {
        if (desdobramentoList.validateForm() != "") {
            AbrirModal(desdobramentoList.validateForm());
            return false;
        }

        desdobramentoList.objectToCreate = [
            ("0" + parseInt(desdobramentoList.EntityList.length + 1)).slice(-2),
            desdobramentoList.ReduzidoCredor.val(),
            desdobramentoList.ValorPercentual.val() + "%",
            desdobramentoList.ValorDesdobrado.val(),
            "",
            desdobramentoList.buttonEditOutros + desdobramentoList.buttonRemove
        ];

        desdobramentoList.objectToEdit = {
            Id: 0,
            Sequencia: parseInt(desdobramentoList.EntityList.length + 1),
            Desdobramento: desdobramentoList.Entity.Id,
            NomeReduzidoCredor: desdobramentoList.ReduzidoCredor.val(),
            ValorPercentual: desdobramentoList.ValorPercentual.val(),
            ValorDesdobrado: desdobramentoList.ValorDesdobrado.val().replace(/[\R$ .]/g, ""),
            ValorDesdobradoInicial: desdobramentoList.ValorDesdobrado.val().replace(/[\R$ .]/g, "")
        };

        if (desdobramentoList.validate() === false) {
            AbrirModal("Não é permitido incluir com o mesmo Nome de Credor.");
            return false;
        }

        if (desdobramentoList.EntityList.length >= 10) {
            AbrirModal("Excede ao número máximo(10) de registros permitos!");
            return false;
        }


        desdobramentoList.EntityList[desdobramentoList.EntityList.length] = desdobramentoList.objectToEdit;

        desdobramentoList.new();

        desdobramentoList.tableOutros.dataTable().fnAddData(desdobramentoList.objectToCreate);

        desdobramentoList.clean();

        desdobramentoList.selectedRow = "";
        desdobramentoList.disabledField();

        desdobramento.EntityListIOutros = desdobramentoList.EntityList;
        desdobramentoList.ReduzidoCredor.focus();
    }

    desdobramentoList.select = "";

    desdobramentoList.editIssqn = function (nRow) {

        desdobramentoList.select = "";
        desdobramentoList.buttonNew.hide();
        desdobramentoList.buttonSave.show();

        desdobramentoList.selectedRow = $(nRow).parent();
        var aData = desdobramentoList.tableIssqn.dataTable().fnGetData(desdobramentoList.selectedRow);

        desdobramentoList.select = aData[1];


        RefazerCombo("#Tipo");
        $('#Tipo option').filter(function () { return $(this).html() == aData[0]; }).prop('selected', true);
        desdobramentoList.ReduzidoCredor.val(aData[1]);
        desdobramentoList.ValorDistribuicao.val(aData[2]);
        desdobramentoList.BaseCalc.val(parseInt(aData[3].replace(/[% ,.]/g, "")) === 0 ? "" : aData[3].replace(/[% ]/g, ""));
        desdobramentoList.ValorDesdobrado.val(aData[4]);
        RefazerCombo("#ReterId");
        $('#ReterId option').filter(function () { return $(this).html() == aData[5]; }).prop('selected', true);
        desdobramentoList.disabledField();
        desdobramentoList.Tipo.trigger("change");
        desdobramentoList.ReduzidoCredor.trigger("blur");
    }

    desdobramentoList.editOutros = function (nRow) {

        desdobramentoList.select = "";
        desdobramentoList.buttonNew.hide();
        desdobramentoList.buttonSave.show();

        desdobramentoList.selectedRow = $(nRow).parent();
        var aData = desdobramentoList.tableOutros.dataTable().fnGetData(desdobramentoList.selectedRow);

        desdobramentoList.select = aData[1];

        desdobramentoList.ReduzidoCredor.val(aData[1]);
        desdobramentoList.ValorPercentual.val(parseInt(aData[2].replace(/[% ,.]/g, "")) === 0 ? "" : aData[2].replace(/[% ]/g, ""));
        desdobramentoList.ValorDesdobrado.val(aData[3]);
        desdobramentoList.disabledField();
    }

    desdobramentoList.removeIssqn = function (nRow) {
        var row = $(nRow).parent();
        var aData = desdobramentoList.tableIssqn.dataTable().fnGetData(row);


        $.each(desdobramentoList.EntityList, function (index, value) {
            if (desdobramentoList.EntityList[index].NomeReduzidoCredor === aData[1]) {
                desdobramentoList.EntityList.splice(index, 1);
                return false;
            }
        });

        desdobramentoList.tableIssqn.dataTable().fnDeleteRow(row);

        desdobramento.EntityListISSQN = desdobramentoList.EntityList;
    }

    desdobramentoList.remove = function (nRow) {
        var row = $(nRow).parent();
        var aData = desdobramentoList.tableOutros.dataTable().fnGetData(row);


        $.each(desdobramentoList.EntityList, function (index, value) {
            if (desdobramentoList.EntityList[index].NomeReduzidoCredor === aData[1]) {
                desdobramentoList.EntityList.splice(index, 1);
                return false;
            }
        });

        desdobramentoList.tableOutros.dataTable().fnDeleteRow(row);


        $.each(desdobramentoList.tableOutros.dataTable().fnGetData(), function (i, value) {
            desdobramentoList.tableOutros.dataTable().fnUpdate(("0" + String(i + 1)).slice(-2), i, 0, false);
        });

        $.each(desdobramentoList.EntityList, function (i, value) {
            desdobramentoList.EntityList[i].Sequencia = i + 1;
        });

        desdobramento.EntityListOutros = desdobramentoList.EntityList;
    }

    desdobramentoList.saveIssqn = function () {

        if (desdobramentoList.validateForm() != "") {
            AbrirModal(desdobramentoList.validateForm());
            return false;
        }

        if (desdobramentoList.validate() === false) {
            AbrirModal("Não é permitido incluir com o mesmo Nome de Credor.");
            return false;
        }

        var select = desdobramentoList.select;
        desdobramentoList.buttonNew.show();
        desdobramentoList.buttonSave.hide();

        desdobramentoList.tableIssqn.dataTable().fnUpdate($("#Tipo :selected").text(), desdobramentoList.selectedRow, 0, false);
        desdobramentoList.tableIssqn.dataTable().fnUpdate(desdobramentoList.ReduzidoCredor.val(), desdobramentoList.selectedRow, 1, false);
        desdobramentoList.tableIssqn.dataTable().fnUpdate(desdobramentoList.ValorDistribuicao.val(), desdobramentoList.selectedRow, 2, false);
        desdobramentoList.tableIssqn.dataTable().fnUpdate(desdobramentoList.BaseCalc.val() == "" ? "" : desdobramentoList.BaseCalc.val() + "%", desdobramentoList.selectedRow, 3, false);
        desdobramentoList.tableIssqn.dataTable().fnUpdate(desdobramentoList.ValorDesdobrado.val(), desdobramentoList.selectedRow, 4, false);
        desdobramentoList.tableIssqn.dataTable().fnUpdate(desdobramentoList.ReterId.val() == "" ? "" : $("#ReterId :selected").text(), desdobramentoList.selectedRow, 5, false);
        //desdobramentoList.tableIssqn.dataTable().fnDraw();


        $.each(desdobramentoList.EntityList, function (i, value) {
            if (desdobramentoList.EntityList[i].NomeReduzidoCredor === select) {
                desdobramentoList.EntityList[i].DesdobramentoTipoId = desdobramentoList.Tipo.val();
                desdobramentoList.EntityList[i].NomeReduzidoCredor = desdobramentoList.ReduzidoCredor.val();
                desdobramentoList.EntityList[i].ValorPercentual = desdobramentoList.BaseCalc.val();
                desdobramentoList.EntityList[i].ValorDesdobrado = desdobramentoList.ValorDesdobrado.val().replace(/[\R$ .]/g, "");
                desdobramentoList.EntityList[i].ValorDesdobradoInicial = desdobramentoList.ValorDesdobrado.val().replace(/[\R$ .]/g, "");
                desdobramentoList.EntityList[i].ValorDistribuicao = desdobramentoList.ValorDistribuicao.val().replace(/[\R$ .]/g, "");
                desdobramentoList.EntityList[i].ReterId = desdobramentoList.ReterId.val();
            }
        });

        desdobramentoList.clean();

        desdobramentoList.selectedRow = "";

        desdobramento.EntityListISSQN = desdobramentoList.EntityList;

        desdobramentoList.Tipo.focus();
    }

    desdobramentoList.saveOutros = function () {

        if (desdobramentoList.validateForm() != "") {
            AbrirModal(desdobramentoList.validateForm());
            return false;
        }


        if (desdobramentoList.validate() === false) {
            AbrirModal("Não é permitido incluir com o mesmo Nome de Credor.");
            return false;
        }

        var select = desdobramentoList.select;
        desdobramentoList.buttonNew.show();
        desdobramentoList.buttonSave.hide();

        desdobramentoList.tableOutros.dataTable().fnUpdate(desdobramentoList.ReduzidoCredor.val(), desdobramentoList.selectedRow, 1, false);
        desdobramentoList.tableOutros.dataTable().fnUpdate(desdobramentoList.ValorPercentual.val() + "%", desdobramentoList.selectedRow, 2, false);
        desdobramentoList.tableOutros.dataTable().fnUpdate(desdobramentoList.ValorDesdobrado.val(), desdobramentoList.selectedRow, 3, false);
        desdobramentoList.tableOutros.dataTable().fnDraw();

        $.each(desdobramentoList.EntityList, function (i, value) {
            if (desdobramentoList.EntityList[i].NomeReduzidoCredor === select) {
                desdobramentoList.EntityList[i].NomeReduzidoCredor = desdobramentoList.ReduzidoCredor.val();
                desdobramentoList.EntityList[i].ValorPercentual = desdobramentoList.ValorPercentual.val();
                desdobramentoList.EntityList[i].ValorDesdobrado = desdobramentoList.ValorDesdobrado.val().replace(/[\R$ .]/g, "");
                desdobramentoList.EntityList[i].ValorDesdobradoInicial = desdobramentoList.ValorDesdobrado.val().replace(/[\R$ .]/g, "");
            }
        });

        desdobramentoList.clean();

        desdobramentoList.selectedRow = "";
        desdobramentoList.disabledField();

        desdobramento.EntityListOutros = desdobramentoList.EntityList;

        desdobramentoList.ReduzidoCredor.focus();
    }

    desdobramentoList.validateForm = function () {
        var result = "";


        if (desdobramentoList.Tipo.is(":visible") == true && desdobramentoList.Tipo.val().length <= 0) {
            result = "Favor preencher Tipo Desdobramento";
        }
        if (desdobramentoList.ReduzidoCredor.val().length <= 0) {
            result = "Favor preencher Nome Reduzido";
        }
        if (desdobramentoList.Tipo.val() != 2 && desdobramentoList.Tipo.val() != 4) {
            if (desdobramentoList.ValorDistribuicao.is(":visible") == true &&
                desdobramentoList.ValorDistribuicao.val().length <= 0) {
                result = "Favor preencher Valor Distribuição";
            }
            if (desdobramentoList.BaseCalc.is(":visible") == true &&
                desdobramentoList.BaseCalc.filter('[disabled]').length <= 0 &&
                desdobramentoList.BaseCalc.val().replace(/[\.,R$ ]/g, "") <= 0) {
                result = "Favor preencher % Base Calc";
            }
        }

        var nomeCredor = $("#ReduzidoCredor").val();
        var nomes = window.ListaCredores.filter(function (credor) {
            if (credor.Nome.toUpperCase() === nomeCredor.toUpperCase() || credor.Reduzido.toUpperCase() === nomeCredor.toUpperCase()) {
                $("#ReduzidoCredor").val(credor.Reduzido);
                $("#BaseCalc").removeAttr("disabled");

                if (credor.BaseCalculo == true) {
                    $("#BaseCalc").attr("disabled", "disabled");
                    $("#BaseCalc").val("");
                }
                return credor.Reduzido;
            }

        });        


        return result;
    }

    desdobramentoList.clean = function () {

        desdobramentoList.ValorPercentual.val("");
        desdobramentoList.Tipo.val("");
        desdobramentoList.ReduzidoCredor.val("");
        desdobramentoList.BaseCalc.val("");
        desdobramentoList.ValorDesdobrado.val("");
        desdobramentoList.ValorDistribuicao.val("");
        desdobramentoList.ReterId.val("");

    }

    desdobramentoList.new = function () {
        desdobramentoList.indetificacao = desdobramentoList.EntityList;
    }

    desdobramentoList.provider = function () {
        desdobramentoList.body
            .on('click', '#newIdenficacao', desdobramentoList.newHandler)
            .on('click', '#saveIdentificacao', desdobramentoList.saveHandler)

            .on('change', '#ValorPercentual', desdobramentoList.disabledField)
            .on('change', '#ValorDesdobrado', desdobramentoList.disabledField);
    }

    $(document).on('ready', desdobramentoList.init);

})(window, document, jQuery);