(function (window, document, $) {
    'use strict';

    window.query = {};

    query.init = function () {
        query.cacheSelectors();
        query.provider();
    }

    query.cacheSelectors = function () {
        query.body = $('body');

        query.area = window.area;
        query.controller = window.controller;
        query.action = window.action;

        query.Contrato = window.Entity;


        query.controllerInclusao = 'Subempenho';
        query.controllerAnulacao = 'SubempenhoCancelamento';

        query.numero = $('#Contrato');
        query.cnpj = $('#txtCNPJ');
        query.obra = $('#Obra');
        query.contratada = $('#txtContratada');
        query.objeto = $('#txtObjeto');
        query.processoSiafem = $('#ProcessoSiafem');
        query.pragmatica = $('#txtPragmatica');
        query.ced = $('#txtCED');

        query.viewContrato = $('#modalConsultaContrato');
        query.tableContratos = $('#tblListaContrato');


        query.numeroSubempenho = $('#txtNumSubempenho');
        query.numeroProcesso = $('#txtNumProcesso');
        query.cpf = $('#txtCfp');
        query.numeroAplicacao = $('#txtNumAplicacao');
        query.dataEmissao = $('#txtDataEmissao');
        query.previsaoInicio = $('#txtPrevisaoInicio');
        query.destino = $('#txtDestino');
        query.codigoObra = $('#txtCodObra');
        query.valor = $('#txtValor');
        query.numeroContrato = $('#txtNumContrato');
        query.valorReforco = $('#txtValorReforco');
        query.valorAnulado = $('#txtValorAnulado');
        query.valorReserva = $('#ValorReserva');
        query.valorEmpenhado = $('#txtValorEmprenhado');
        query.squencia = $('#txtSeqTam');
        query.quota1 = $('#txtQuota1');
        query.quota2 = $('#txtQuota2');
        query.quota3 = $('#txtQuota3');
        query.quota4 = $('#txtQuota4');
        query.especificacao = $('#txtEspecificacao');

        query.viewSubempenho = $('#modalDadosReserva');

        query.route = '/' + query.area + '/' + query.controller;

        query.displayHandler = function (e) {
            e.preventDefault();
            query.consultarContrato();
        }
    }



    query.row = function (items) {
        query.tableContratos.DataTable().clear().draw(true);
        $.each(items, function (i, item) {
            query.tableContratos.DataTable().row.add([
                item.OutEvento,
                item.OutNumero,
                item.OutData,
                item.OutValor,
                "<button class='btn btn-xs btn-primary' onclick='query.consultarSubempenho('" + item.OutNumero + ", 1)'><i class='fa fa-search'></i></button>"
            ]).draw(true);
        });
    }


    query.formatNatureza = function (entity) {
        return entity.OutCed1.concat('.',
            entity.OutCed2, '.',
            entity.OutCed3, '.',
            entity.OutCed4, '.',
            entity.OutCed5
        );
    }

    query.formatProgram = function (entity) {
        return entity.OutCfp1.concat('.',
            entity.OutCfp2, '.',
            entity.OutCfp3, '.',
            entity.OutCfp4, '.',
            entity.OutCfp5, '/',
            query.formatNatureza(entity)
        );
    }

    query.formatEspecificacaoDespesa = function (entity) {
        return entity.OutEspecDespesa1.concat('\n',
            entity.OutEspecDespesa2, '\n',
            entity.OutEspecDespesa3, '\n',
            entity.OutEspecDespesa4, '\n',
            entity.OutEspecDespesa5, '\n',
            entity.OutEspecDespesa5, '\n',
            entity.OutEspecDespesa7, '\n',
            entity.OutEspecDespesa8, '\n',
            entity.OutEspecDespesa9
        );
    }


    query.populateContrato = function (entity) {
        query.numero.val(entity.OutContrato.replace(/\s/g, '').replace(/[\.-]/g, '').replace(/(\d{2})(\d{2})(\d{5})(\d{1})/g, '\$1.\$2.\$3\-\$4'));
        query.cnpj.val(entity.OutCpfcnpj.replace(/(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})/g, '\$1.\$2.\$3\/\$4\-\$5'));
        query.obra.val(entity.OutCodObra);
        query.contratada.val(entity.OutContratada);
        query.objeto.val(entity.OutObjeto);
        query.processoSiafem.val(entity.OutProcesSiafem);
        query.pragmatica.val(entity.OutPrograma);
        query.ced.val(entity.OutCED);
    }

    query.populateSubempenho = function (entity) {
        query.numeroSubempenho.val(entity.OutNumReserva);
        query.numeroProcesso.val(entity.OutNumProcesso);
        query.cpf.val(query.formatProgram(entity));
        query.numeroAplicacao.val(entity.OutCodAplicacao);
        query.dataEmissao.val(entity.OutDataEmissao);
        query.previsaoInicio.val(entity.OutPrevInic);
        query.destino.val(entity.OutDestRecurso);
        query.codigoObra.val(entity.OutCodObra);
        query.valor.val(entity.OutValorReserva);
        query.numeroContrato.val(entity.OutIdentContrato);
        query.valorReforco.val(entity.OutValorReforco);
        query.valorAnulado.val(entity.OutValorAnulado);
        query.valorReserva.val(entity.OutSaldoReserva);
        query.valorEmpenhado.val(entity.OutValorEmpenhado);
        query.squencia.val(entity.OutSeqTam);
        query.quota1.val(entity.OutSaldoQ1);
        query.quota2.val(entity.OutSaldoQ2);
        query.quota3.val(entity.OutSaldoQ3);
        query.quota4.val(entity.OutSaldoQ4);
        query.especificacao.text(query.formatEspecificacaoDespesa(entity));
    }


    query.validateEntityIsNotNullOrEmpty = function (entity) {
        if (entity === undefined || entity === null || entity === 0) {
            AbrirModal('Favor informar o número do Subempenho!');
            return false;
        }
    }

    query.validateContratNumberIsNotNullOrEmpty = function (number) {
        if (number.length === 0) {
            AbrirModal('Favor informar número do contrato!');
            return false;
        }
    }

    query.validateBrowserIsOnLine = function () {
        if (navigator.onLine != true) {
            AbrirModal("Erro de conexão");
            return false;
        }
    }


    query.actionConsultarContrato = function (number) {

        var contrato = {
            NumContrato: number,
            Type: typoContract
        }

        $.ajax({
            datatype: 'JSON',
            type: 'POST',
            url: query.route + '/ConsultarContrato',
            data: JSON.stringify(contrato),
            contentType: 'application/json; charset=utf-8',
            beforeSend: function () {
                waitingDialog.show('Consultando');
            },
            success: function (response) {
                if (response.Status === 'Sucesso') {
                    query.Contrato = response.Contrato;
                    query.viewContrato.modal();
                    query.populateContrato(query.Contrato);
                    query.row(query.Contrato.ListConsultaContrato);
                }
                else {
                    AbrirModal(response.Msg);
                }
            },
            error: function (response) {
                AbrirModal(response);
            },
            complete: function () {
                waitingDialog.hide();
            }
        });
    }

    query.actionConsultarSubempenho = function (entity, type) {
        $.ajax({
            datatype: 'JSON',
            type: 'POST',
            url: query.route + '/ConsultarSubempenho',
            data: JSON.stringify(entity),
            contentType: 'application/json; charset=utf-8',
            beforeSend: function () {
                waitingDialog.show('Consultando');
            },
            success: function (response) {
                if (response.Status === 'Sucesso') {
                    if (type === 1) {
                        query.viewContrato.modal('toggle');
                    }

                    query.populateSubempenho(response.Subempenho);
                    query.viewSubempenho.modal();
                }
                else {
                    AbrirModal(response.Msg);
                }
            },
            error: function (response) {
                AbrirModal(response);
            },
            complete: function () {
                waitingDialog.hide();
            }
        });
    }


    query.consultarContrato = function () {
        query.validateBrowserIsOnLine();

        var numero = query.numero.val().replace(/[\s*\.-]/g, '');
        query.validateContratNumberIsNotNullOrEmpty();

        var numcontrato = query.Contrato !== null ? query.Contrato.OutContrato.replace(/\s/g, '').replace(/[\.-]/g, '') : '';
        if (numero === numcontrato) {
            waitingDialog.show('Consultando');

            query.viewContrato.modal('toggle');
            query.populateContrato(query.Contrato);
            query.row([]);

            waitingDialog.hide();
        }
        else {
            query.actionConsultarContrato(numero)
        }
    }

    query.consultarSubempenho = function (entidade, type) {
        query.validateBrowserIsOnLine();
        query.validateEntityIsNotNullOrEmpty(entidade);

        query.actionConsultarSubempenho(entidade, type);
    };



    query.provider = function () {
        query.body
            .on('click', '#btnConsultarContrato', query.displayHandler);
    }


    $(document).on('ready', query.init);

})(window, document, jQuery);