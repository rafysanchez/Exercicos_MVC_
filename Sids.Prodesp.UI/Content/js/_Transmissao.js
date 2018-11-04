(function (window, document, $) {
    'use strict';

    window.transmissao = {};

    transmissao.init = function () {
        transmissao.cacheSelectors();
    }

    transmissao.cacheSelectors = function () {
        transmissao.body = $('body');

        transmissao.area = window.area;
        transmissao.controller = window.controller;

        transmissao.Entity = window.Entity;

        transmissao.controllerInclusao = 'Subempenho';
        transmissao.controllerAnulacao = 'SubempenhoCancelamento';

        transmissao.classProdesp = $(".prodesp");
        transmissao.classSiafem = $(".siafem");
        transmissao.classSiafisico = $(".siafisico");

        transmissao.formPainelCadastrar = $("#frmPainelCadastrar");
        transmissao.formPainelFiltrar = $("#form_filtro");
        transmissao.numeroSiafemSiafisico = $("#NumSiafemSiafisico");

        transmissao.valueSelecaoSiafemSiafisico = $('#transmitirSIAFEM');
        transmissao.valueCenarioSiafemSiafisico = $('#CenarioSiafemSiafisico');
        transmissao.valueSelecaoProdesp = $('#transmitirProdesp');
        transmissao.valueCenarioProdesp = $('#CenarioProdesp');

        transmissao.valueSelecaoTodos = $(".idSelecionar");
        transmissao.valueResend = $(".idRetrasmitir");

        transmissao.route = "/" + transmissao.area + "/" + transmissao.controller;

        transmissao.EntityId = $("#Id");
    }


    transmissao.validate = function () {
        returnv (JSON.parse(transmissao.Entity.TransmitidoSiafemSiafisico) === false && transmissao.valueSelecaoSiafemSiafisico.is('checked') === true) ||
        (JSON.parse(transmissao.Entity.TransmitidoSiafemSiafisico) === false && transmissao.valueSelecaoSiafemSiafisico.is('checked') === false) ||
        (JSON.parse(transmissao.Entity.TransmitidoProdesp) === false && transmissao.valueSelecaoProdesp.is('checked') === true);
    }

    transmissao.redirect = function (id) {
        window.location.href = transmissao.route + '/Edit/' + id + '?tipo=a';
    }

    transmissao.failModal = function failModal(id) {
        ShowLoading();
        transmissao.redirect(id);
    }

    transmissao.sendResponseModal = function (id, message) {
        $.confirm({
            text: message,
            title: "Transmitir",
            cancel: function () {
                liquidacaoValidator.action = "I";
                if (JSON.parse(transmissao.Entity.TransmitidoSiafemSiafisico) === true) {
                    impressao.print(dados.Codigo);
                }
                else {
                    transmissao.redirect(id);
                }
            },
            cancelButton: "Fechar",
            confirmButton: ""
        });
    }

    transmissao.resendResponse = function (message) {
        $.confirm({
            text: message,
            title: "Retransmitir",
            cancel: function () {
                transmissao.formPainelFiltrar.submit();
            },
            cancelButton: "Fechar",
            confirmButton: ""
        });
    }


    transmissao.send = function (object, controller) {
        if (transmissao.validate() === false) {
            AbrirModal("Por favor selecione um sistema qua ainda não tenha sido transmitido!");
            return false;
        }


        $.ajax({
            datatype: 'JSON',
            type: 'POST',
            url: transmissao.route + "/Transmitir",
            data: object,
            // data: transmissao.Entity,
            contentType: "application/json; charset=utf-8",
            beforeSend: function () {
                waitingDialog.show("Transmitindo");
            },
            error: function (dados) {
                AbrirModal(dados);
            },
            success: function (dados) {
                switch (dados.Status) {
                    case 'Sucesso':
                        transmissao.Entity = dados.objModel;
                        transmissao.EntityId.val(dados.Codigo);
                        transmissao.sendResponseModal(transmissao.Entity.Id, "Transmissão realizada com sucesso");
                        break;
                    case 'Falha':
                        transmissao.EntityId.val(dados.Codigo);
                        AbrirModal(dados.Msg, transmissao.failModal(transmissao.EntityId.val()));
                        break;
                    case 'Falha Doc':
                        transmissao.Entity = dados.objModel;
                        transmissao.sendResponseModal(transmissao.Entity.Id, "Inserir Documento" + dados.Msg);
                        break;
                    case 'Falha Prodesp':
                        transmissao.Entity = dados.objModel;
                        transmissao.EntityId.val(transmissao.Entity.Id);
                        transmissao.numeroSiafemSiafisico.val(transmissao.Entity.NumeroSiafemSiafisico);
                        transmissao.sendResponseModal(transmissao.Entity.Id, dados.Msg);
                        break;
                    default:
                        transmissao.Entity = dados.objModel;
                        transmissao.EntityId.val(transmissao.Entity.Id);
                        transmissao.sendResponseModal(transmissao.Entity.Id, "Transmitido Siafem/Siafisico | " + dados.Msg);
                        break;
                }
            },
            complete: function () {
                waitingDialog.hide();
            }
        });
    }

    transmissao.resend = function () {
        $.ajax({
            datatype: 'JSON',
            type: 'POST',
            url: transmissao.route + "/Retransmitir",
            data: JSON.stringify(transmissao.getSelectedItems()),
            contentType: "application/json; charset=utf-8",
            beforeSend: function () {
                waitingDialog.show('Retransmitindo');
            },
            error: function (dados) {
                AbrirModal(dados);
            },
            success: function (dados) {
                switch (dados.Status) {
                    case 'Sucesso':
                        transmissao.resendResponse("Retransmissão realizada com sucesso");
                        break;
                    default:
                        transmissao.resendResponse(dados.Msg.replace(/[\;]/g, "\n"));
                        break;
                }
            },
            complete: function () {
                waitingDialog.hide();
            }
        });
    }

    transmissao.selectAll = function () {
        if(transmissao.valueSelecaoTodos.is(':checked') === true){
            transmissao.valueResend.prop("checked", true);
            transmissao.valueSelecaoTodos.prop("checked", true);
        }
        if (transmissao.valueSelecaoTodos.is(':checked') === false) {
            transmissao.valueSelecaoTodos.prop("checked", false);
            transmissao.valueResend.prop("checked", false);
        }
    }

   

    transmissao.getSelectedItems = function () {
        var ids = [];
        $.each($("td >.idRetrasmitir"), function (id, val) {
            if ($(val).is(":checked"))
                ids[ids.length] = $(val).val();
        });

        if (ids.length < 1) {
            AbrirModal("Ao menos um registro deve ser selecionado");
            return false;
        }

        return ids;
    }


    $(document).on('ready', transmissao.init);

})(window, document, jQuery);