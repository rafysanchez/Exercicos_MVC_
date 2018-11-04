(function (window, document, $) {
    'use strict';

    window.liquidacaoItem = {};



    liquidacaoItem.init = function () {
        liquidacaoItem.cacheSelectors();
        liquidacaoItem.reset();
        liquidacaoItem.provider();
        liquidacaoItem.clean();
    }

    liquidacaoItem.cacheSelectors = function () {
        liquidacaoItem.body = $('body');
        liquidacaoItem.controller = window.controller;

        liquidacaoItem.controllerInclusao = 'Subempenho';
        liquidacaoItem.controllerAnulacao = 'SubempenhoCancelamento';

        liquidacaoItem.Entity = window.Entity;
        liquidacaoItem.EntityList = window.ItemList;

        liquidacaoItem.objectToCreate = [];
        liquidacaoItem.objectToEdit = {};

        liquidacaoItem.buttonNew = $('#divNewItem');
        liquidacaoItem.buttonSave = $('#divSaveItem');
        liquidacaoItem.table = $('#tblPesquisaItem');

        liquidacaoItem.divEdicao = $('#DadoLiquidacaoItem');
        liquidacaoItem.codigoItemServico = $('#CodigoItemServico');
        liquidacaoItem.transmitir = $('#Transmitir');
        liquidacaoItem.codigoUnidadeFornecimentoItem = $('#CodigoUnidadeFornecimentoItem');
        liquidacaoItem.quantidadeMaterialServico = $('#QuantidadeMaterialServico');
        liquidacaoItem.quantidadeMaterialServicoDecimal = $('#QuantidadeMaterialServicoDecimal');
        liquidacaoItem.valor = $('#Valor', '#DadoLiquidacaoItem');
        liquidacaoItem.quantidadeLiquidar = $('#QuantidadeLiquidar', '#DadoLiquidacaoItem');
        liquidacaoItem.quantidadeLiquidarDecimal = $('#QuantidadeLiquidarDecimal', '#DadoLiquidacaoItem');
        liquidacaoItem.buttonEditSeletor = '#tblPesquisaItem .btn-edit';
        liquidacaoItem.buttonRemoveSeletor = '#tblPesquisaItem .btn-excluir';
        liquidacaoItem.buttonEditHtml = '<button type="button" title="Alterar" class="btn btn-xs btn-info btn-edit margL7" onclick="liquidacaoItem.edit(this)"><i class="fa fa-edit"></i></button>';
        liquidacaoItem.buttonRemoveHtml = '<button type="button" title="Excluir" class="btn btn-xs btn-danger margL7" onclick="liquidacaoItem.remove(this)"><i class="fa fa-trash"></i></button>';


        liquidacaoItem.itens = [];
        liquidacaoItem.selectedRow = "";

        liquidacaoItem.newHandler = function (e) {
            e.preventDefault();
            liquidacaoItem.add();
        }

        liquidacaoItem.saveHandler = function (e) {
            e.preventDefault();
            liquidacaoItem.save();
        }

        liquidacaoItem.editHandler = function (e) {
            e.preventDefault();
            var element = e.target.tagName.toLowerCase() === "i" ? e.target.parentNode : e.target;
            liquidacaoItem.edit(element);
        }


        liquidacaoItem.removeHandler = function (e) {
            e.preventDefault();
            var element = e.target.tagName.toLowerCase() === "i" ? e.target.parentNode : e.target;
            liquidacaoItem.remove(element);
        }
    }

    liquidacaoItem.reset = function () {
        liquidacaoItem.clean();

        if (liquidacao.verificarCenarioNlPregaoBec()) {
            liquidacaoItem.buttonNew.hide();
        }
        else {
            liquidacaoItem.buttonNew.show();
        }

        liquidacaoItem.buttonSave.hide();

        if (liquidacao.verificarCenarioNlPregaoBec()) {
            liquidacao.desabilitaElemento(liquidacaoItem.codigoItemServico);
            liquidacao.desabilitaElemento(liquidacaoItem.codigoUnidadeFornecimentoItem);
            liquidacao.desabilitaElemento(liquidacaoItem.quantidadeLiquidar);
            liquidacao.desabilitaElemento(liquidacaoItem.quantidadeLiquidarDecimal);
        }

        if (liquidacao.verificarCenarioBec()) {
            $(liquidacaoItem.buttonEditSeletor).hide();
        }

        $(liquidacaoItem.buttonRemoveSeletor).hide();
    }

    liquidacaoItem.validate = function () {
        var itemExists = $.grep(liquidacaoItem.EntityList, function (e) {
            return e.CodigoItemServico === liquidacaoItem.codigoItemServico.val();
        });

        return itemExists.length === 0;
    }

    liquidacaoItem.add = function () {
        var codigo = liquidacaoItem.codigoItemServico.val();
        var codigoUnidadeFornecimento = liquidacaoItem.codigoUnidadeFornecimentoItem.val();
        var quantidade = liquidacaoItem.quantidadeMaterialServico.val();
        var quantidadeDecimal = liquidacaoItem.quantidadeMaterialServicoDecimal.val();
        var valor = liquidacaoItem.valor.val();
        var quantidadeLiquidar = liquidacaoItem.quantidadeLiquidar.val();
        var quantidadeLiquidarDecimal = liquidacaoItem.quantidadeLiquidarDecimal.val();
        var transmitir = liquidacaoItem.transmitir.is(':checked');

        if (codigo.length <= 0) {
            AbrirModal("Favor preencher Item Material");
            return false;
        }

        if (codigo.length < 10) {
            var item = codigo.replace(/[\.-]/g, "");
            for (var i; item.length < 9; i++) {
                item = "0" + item;
            }
            liquidacaoItem.codigoItemServico.val(item.replace(/(\d{8})(\d{1})/g, "\$1\-\$2"));
        }

        if (codigoUnidadeFornecimento.length <= 0) {
            AbrirModal("Favor preencher Unidade de Fornecimento");
            return false;
        }

        if (quantidadeLiquidar.length <= 0) {
            AbrirModal("Favor preencher Quantidade");
            return false;
        }

        return liquidacaoItem.addComParametros(codigo, codigoUnidadeFornecimento, quantidade, quantidadeDecimal, valor, quantidadeLiquidar, quantidadeLiquidarDecimal, transmitir);
    }

    liquidacaoItem.addComParametros = function (codigo, codigoUnidadeFornecimento, quantidade, quantidadeDecimal, valor, quantidadeLiquidar, quantidadeLiquidarDecimal, transmitir) {
        codigo = liquidacaoItem.formataCodigoItem(codigo);

        var qtdFormatada = quantidade + "," + quantidadeDecimal;
        var qtdLiquidarFormatada = 0;//quantidadeLiquidar + "," + quantidadeLiquidarDecimal;

        var buttons = liquidacaoItem.buttonEditHtml;

        if (liquidacao.verificarCenarioBec()) {
            buttons = '';
        }

        //Liquidação de Pregão Eletrônico – NLPREGAO = 2 E Nota de Lançamento BEC – NLBEC = 3
        var transmitirReadOnly = false;
        if (!liquidacao.verificarCenarioPregaoBec()) {
            transmitirReadOnly = true;
        }

        if (liquidacao.verificarCenarioBec()) {
            qtdLiquidarFormatada = qtdFormatada;
        }

        liquidacaoItem.objectToCreate = [
            '<input type="checkbox" name="Transmitir" onchange="liquidacaoItem.changeTransmitir(' + codigo + ',this)" ' + (transmitirReadOnly || transmitir ? 'checked' : '') + ' ' + (transmitirReadOnly ? 'disabled' : '') + ' />',
            codigo,
            codigoUnidadeFornecimento,
            qtdFormatada,
            valor,
            qtdLiquidarFormatada,
            buttons
        ];

        liquidacaoItem.objectToEdit = {
            Id: 0,
            SubempenhoId: liquidacaoItem.Entity.Id,
            CodigoItemServico: codigo,
            CodigoUnidadeFornecimentoItem: codigoUnidadeFornecimento,
            QuantidadeMaterialServico: qtdFormatada,
            Valor: valor,
            QuantidadeLiquidar: qtdLiquidarFormatada,
            Transmitir: transmitir,
            SequenciaItem: liquidacaoItem.EntityList.length + 1
        };


        if (liquidacaoItem.validate() === false) {
            AbrirModal("Não é permitido incluir itens com o mesmo código.");
            return false;
        }
        liquidacaoItem.EntityList[liquidacaoItem.EntityList.length] = liquidacaoItem.objectToEdit;

        liquidacaoItem.new();

        liquidacaoItem.table.dataTable().fnAddData(liquidacaoItem.objectToCreate);


        //liquidacao.checkTransmitirReadOnly();

        liquidacaoItem.clean();

        liquidacaoItem.selectedRow = "";
    }


    liquidacaoEvento.select = "";

    liquidacaoItem.edit = function (nRow) {
        liquidacaoItem.buttonNew.hide();
        liquidacaoItem.buttonSave.show();

        liquidacaoItem.selectedRow = $(nRow).parent();
        var aData = liquidacaoItem.table.dataTable().fnGetData(liquidacaoItem.selectedRow);

        var chkTransmitir = liquidacaoItem.selectedRow.parent().find('input[type="checkbox"]').is(':checked');
        liquidacaoItem.transmitir.val(chkTransmitir);

        liquidacaoEvento.select = aData[1];

        liquidacaoItem.codigoItemServico.val(aData[1]);
        liquidacaoItem.codigoUnidadeFornecimentoItem.val(aData[2]);

        aData[3] = aData[3] === 0 || aData[3].length === 0 ? "0,000" : aData[3];
        var arr = aData[3].split(',');
        var qtdeInteira = arr[0];
        var qtdeDecimal = arr[1];


        liquidacaoItem.quantidadeMaterialServico.val(qtdeInteira);
        liquidacaoItem.quantidadeMaterialServicoDecimal.val(qtdeDecimal);

        liquidacaoItem.valor.val(aData[4]);

        if (aData[5] === 0 || aData[5].length === 0) {
            aData[5] = '0,000';
        }

        var qtdLiquidarArray = aData[5].split(',');


        liquidacaoItem.quantidadeLiquidar.val(qtdLiquidarArray[0]);
        liquidacaoItem.quantidadeLiquidarDecimal.val(qtdLiquidarArray[1]);


        if (liquidacao.verificarCenarioNlPregaoBec()) {
            liquidacao.desabilitaElemento(liquidacaoItem.codigoItemServico);
            liquidacao.desabilitaElemento(liquidacaoItem.codigoUnidadeFornecimentoItem);

            //if (liquidacao.cenarioContem([9, 10, 11]) || liquidacao.verificarCenarioBec() || parseFloat(aData[3]) === 0) {
            if (liquidacao.verificarCenarioBec()) {
                liquidacao.desabilitaElemento(liquidacaoItem.quantidadeLiquidar);
                liquidacao.desabilitaElemento(liquidacaoItem.quantidadeLiquidarDecimal);
            }
            else {
                liquidacao.habilitaElemento(liquidacaoItem.quantidadeLiquidar);
                liquidacao.habilitaElemento(liquidacaoItem.quantidadeLiquidarDecimal);
            }
        }
    }

    liquidacaoItem.remove = function (nRow) {
        var row = $(nRow).parent();

        var aData = liquidacaoItem.table.dataTable().fnGetData(row);
        $.each(liquidacaoItem.EntityList, function (index, value) {
            if (liquidacaoItem.EntityList[index].CodigoItemServico === aData[0]) {
                liquidacaoItem.EntityList.splice(index, 1);
            }
        });

        liquidacaoItem.table.dataTable().fnDeleteRow(row);
    }

    liquidacaoItem.save = function () {
        if (liquidacaoItem.codigoItemServico.val().length <= 0) {
            AbrirModal("Favor preencher Item Material");
            return false;
        }

        if (liquidacaoItem.codigoItemServico.val().length < 10) {
            liquidacaoItem.codigoItemServico.val(liquidacaoItem.formataCodigoItem(liquidacaoItem.codigoItemServico.val()));
        }

        if (liquidacaoItem.codigoUnidadeFornecimentoItem.val().length <= 0) {
            AbrirModal("Favor preencher Unidade de Fornecimento");
            return false;
        }

        var qtdPreenchida = true;

        if (liquidacaoItem.quantidadeLiquidar.length > 0) {
            var qtdLiquidar = parseInt(liquidacaoItem.quantidadeLiquidar.val());
            var qtdLiquidarDecimal = parseInt(liquidacaoItem.quantidadeLiquidarDecimal.val());
            qtdPreenchida = liquidacaoItem.quantidadeLiquidar.val().length > 0 && (qtdLiquidar > 0 || qtdLiquidarDecimal > 0);
        }
        else {
            var qtd = parseInt(liquidacaoItem.quantidadeMaterialServico.val());
            var qtdDecimal = parseInt(liquidacaoItem.quantidadeMaterialServicoDecimal.val());
            qtdPreenchida = liquidacaoItem.quantidadeMaterialServico.val().length > 0 && (qtd > 0 || qtdDecimal > 0);
        }

        if (!qtdPreenchida) {
            AbrirModal("Favor preencher Quantidade");
            return false;
        }

        var existeQuantidadeOriginal = liquidacaoItem.quantidadeMaterialServico.val().length > 0;


        if (existeQuantidadeOriginal && liquidacaoItem.quantidadeMaterialServicoDecimal.val().length <= 0) {
            liquidacaoItem.quantidadeMaterialServicoDecimal.val("000");
        }

        var select = liquidacaoEvento.select;

        var qtdOriginalFormatada = existeQuantidadeOriginal ? liquidacaoItem.quantidadeMaterialServico.val() + "," + liquidacaoItem.quantidadeMaterialServicoDecimal.val() : '';
        var qtdLiquidarFormatada = liquidacaoItem.quantidadeLiquidar.val() + "," + liquidacaoItem.quantidadeLiquidarDecimal.val();
        var transmitir = liquidacaoItem.selectedRow.parent().find('input[type="checkbox"]').is(':checked');
        var codigo = liquidacaoItem.limparCodigoItem(liquidacaoItem.codigoItemServico.val());


        var checkbox = '<input type="checkbox" name="Transmitir" data-id="' + codigo + '" onchange="liquidacaoItem.changeTransmitir('+codigo+',this)" '+(transmitir ? 'checked' : '')+' />';


        liquidacaoItem.table.dataTable().fnUpdate(checkbox, liquidacaoItem.selectedRow, 0, false);
        liquidacaoItem.table.dataTable().fnUpdate(codigo, liquidacaoItem.selectedRow, 1, false);
        liquidacaoItem.table.dataTable().fnUpdate(liquidacaoItem.codigoUnidadeFornecimentoItem.val(), liquidacaoItem.selectedRow, 2, false);
        liquidacaoItem.table.dataTable().fnUpdate(qtdOriginalFormatada, liquidacaoItem.selectedRow, 3, false);
        liquidacaoItem.table.dataTable().fnUpdate(liquidacaoItem.valor.val(), liquidacaoItem.selectedRow, 4, false);
        liquidacaoItem.table.dataTable().fnUpdate(qtdLiquidarFormatada, liquidacaoItem.selectedRow, 5, false);
        liquidacaoItem.table.dataTable().fnDraw();

        liquidacao.checkTransmitirReadOnly();

        liquidacaoItem.atualizarItemEntityList(codigo, liquidacaoItem.codigoUnidadeFornecimentoItem.val(), qtdOriginalFormatada, liquidacaoItem.valor.val(), qtdLiquidarFormatada, transmitir);

        liquidacaoItem.reset();
        //liquidacaoItem.clean();

        liquidacaoItem.selectedRow = "";
    }

    liquidacaoItem.atualizarItemEntityList = function (codigo, codigoUnidadeFornecimento, qtdOriginalFormatada, valor, qtdLiquidarFormatada, transmitir) {
        var item = liquidacaoItem.selecionarItemEntityList(codigo);

        if (item !== null) {
            item.CodigoItemServico = codigo;
            item.CodigoUnidadeFornecimentoItem = codigoUnidadeFornecimento;
            item.QuantidadeMaterialServico = qtdOriginalFormatada;
            item.Valor = valor;
            item.QuantidadeLiquidar = qtdLiquidarFormatada;
            item.Transmitir = transmitir;
        }
    }

    liquidacaoItem.selecionarItemEntityList = function (codigo) {
        for (var i = 0; i < liquidacaoItem.EntityList.length; i++) {
            var codigoAtual = liquidacaoItem.EntityList[i].CodigoItemServico;

            if (liquidacaoItem.limparCodigoItem(codigoAtual) === liquidacaoItem.limparCodigoItem(codigo)) {
                return liquidacaoItem.EntityList[i];
            }
        }
        return null;
    };

    liquidacaoItem.removerItemEntityList = function (codigo) {
        for (var i = 0; i < liquidacaoItem.EntityList.length; i++) {
            var codigoAtual = liquidacaoItem.EntityList[i].CodigoItemServico;

            if (liquidacaoItem.limparCodigoItem(codigoAtual) === liquidacaoItem.limparCodigoItem(codigo)) {
                return liquidacaoItem.EntityList[i].pop();
            }
        }
        return null;
    };

    liquidacaoItem.limparCodigoItem = function (codigo) {
        return codigo.toString().replace('-', '');
    }

    liquidacaoItem.changeTransmitir = function (codigo, element) {
        var sender = $(element);
        var item = liquidacaoItem.selecionarItemEntityList(codigo);

        if (item !== null) {
            item.Transmitir = sender.is(":checked");
        }
    };

    liquidacaoItem.clean = function () {
        liquidacaoItem.codigoItemServico.val('');
        liquidacaoItem.codigoUnidadeFornecimentoItem.val('');
        liquidacaoItem.quantidadeMaterialServico.val('');
        liquidacaoItem.quantidadeMaterialServicoDecimal.val('');
        liquidacaoItem.quantidadeLiquidar.val('');
        liquidacaoItem.quantidadeLiquidarDecimal.val('');
        liquidacaoItem.transmitir.val(false);
    }

    liquidacaoItem.formataCodigoItem = function (codigoItem) {
        var item = codigoItem.replace(/[\.-]/g, "");
        for (var i; item.length < 9; i++) {
            item = "0" + item;
        }
        return item.replace(/(\d{8})(\d{1})/g, "\$1\-\$2");
    }


    liquidacaoItem.new = function () {
        liquidacaoItem.itens = liquidacaoItem.EntityList;
    }


    liquidacaoItem.provider = function () {
        liquidacaoItem.body
            .on('click', '#newItem', liquidacaoItem.newHandler)
            .on('click', '#saveItem', liquidacaoItem.saveHandler)
            .on('click', '.btn-edit', liquidacaoItem.editHandler)
            .on('click', '.btn-excluir', liquidacaoItem.removeHandler);
    }

    $(document).on('ready', liquidacaoItem.init);

})(window, document, jQuery);