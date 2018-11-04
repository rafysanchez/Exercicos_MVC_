(function (window, document, $) {
    'use strict';

    window.consulta = window.consulta || {};

    consulta.init = function () {
        consulta.cacheSelectors();

        if (consulta.controller != 'RapInscricao' && consulta.controller != 'RapRequisicao') {
            document.getElementById('btnConsultarContratoTodos').onclick = function (e) {
                consulta.resumoEventHandler(e);
            }

            document.getElementById('btnConfirmarContratoTodos').onclick = function (e) {
                consulta.detalheEventHandler(e)
            }
        }

        $("#EspecDespesa").change(function (e) {
            var valor = $("div >#EspecDespesa").val();
            if ((valor != '000') && (valor != '')) {
                consulta.consultarEspecificacao(valor);
            }

        });


        var tipo = 0;
        $("#CodAssAutorizado").change(function (e) {
            var valor = $("div >#CodAssAutorizado").val();

            tipo = 1;
            consulta.LimparCombo(tipo);

            if (valor.length != 0 & valor.length == 5 & valor != "") {
                consulta.consultarAssinatura(valor, tipo);
            }
        });

        $("#CodAssExaminado").change(function (e) {
            var valor = $("div >#CodAssExaminado").val();
            tipo = 2;
            consulta.LimparCombo(tipo);
            if (valor.length != 0 & valor.length == 5 & valor != "") {
                consulta.consultarAssinatura(valor, tipo);
            }
        });

        $("#CodAssResponsavel").change(function (e) {
            var valor = $("div >#CodAssResponsavel").val();
            tipo = 3;
            consulta.LimparCombo(tipo);
            if (valor.length != 0 & valor.length == 5 & valor != "") {
                consulta.consultarAssinatura(valor, tipo);
            }
        });

        consulta.provider();
    }

    consulta.cacheSelectors = function () {
        consulta.body = $('body');
        consulta.form = $('form');

        consulta.area = window.area;
        consulta.controller = window.controller;
        consulta.action = window.action;
        consulta.type = window.type;

        consulta.EntityContrato = window.EntityContrato;
        consulta.EntityContratoList = [];

        consulta.route = '/' + consulta.area + '/' + consulta.controller;



        consulta.Programa = window.Programa;
        consulta.ProgramaList = window.ProgramaList;
        consulta.EstruturaList = window.EstruturaList;



        consulta.resumoEventHandler = function (e) {
            e.preventDefault();
            consulta.consultarContrato(window.type);
        }

        consulta.detalheEventHandler = function (e) {
            e.preventDefault();
            consulta.confirmarContrato();
        }
    }

    consulta.validateConnectionIsOpen = function () {
        if (navigator.onLine !== true) {
            AbrirModal('Erro de conexão');
        }

        return false;
    }

    consulta.obterEstruturaProgramaInfo = function () {
        consulta.ProgramaList.forEach(function (value) {
            if (value.Codigo === $('#Programa').val()) {
                return value.Cfp;
            }
        });

        return;
    }

    consulta.obterEstruturaNaturezaInfo = function () {
        consulta.EstruturaList.forEach(function (value) {
            if (value.Codigo === $('#Natureza').val()) {
                return value.Natureza;
            }
        });

        return;
    }

    consulta.obterEstruturaFonteInfo = function () {
        fonteInfo.forEach(function (value) {
            if (value.Id === $('#Fonte').val()) {
                return value.Codigo;
            }
        });

        return;
    }

    consulta.obterEstruturaRegionalInfo = function () {
        regionais.forEach(function (value) {
            if (value.Id === $('#Regional').val()) {
                return value.Descricao;
            }
        });

        return;
    }

    consulta.carregarDadosContrato = function (modal) {
        FecharModal('#' + modal);

        if (obj !== '/Reserva/Reserva')
            return false;

        $('#modalConsultaContrato').modal('toggle');

        $('#Contrato').val(contrato.OutContrato);
        $('#txtCNPJ').val(contrato.OutCpfcnpj);
        $('#Obra').val(contrato.OutCodObra);
        $('#txtContratada').val(contrato.OutContratada);
        $('#txtObjeto').val(contrato.OutObjeto);
        $('#ProcessoSiafem').val(contrato.OutProcesSiafem);
        $('#txtPragmatica').val(contrato.OutPrograma);
        $('#txtCED').val(contrato.OutCED);

        consulta.datatableAddRowForContrato(table);
    };

    consulta.carregarDadosOc = function (oc) {
        $('#Processo').val(oc.Processo);
        SelecioarComboCfp();

        $("#Fonte option[value='" + oc.Fonte + "']").attr('selected', true);

        var cont = 0;
        consulta.ProgramaList.forEach(function (value) {
            if (value.Ptres === oc.Ptres) {
                $("#Programa option[value='" + value.Codigo + "']").attr('selected', true);
                cont += 1;
            }
        });

        var fonte = $('#Fonte :selected').text();
        consulta.selecionarDropDownPrograma();

        var msg = '';

        if (cont === 0) {
            msg = 'Ptres ' + oc.Ptres + ' não Cadastrado no SIDS';
        }

        consulta.dropDownNatureza();

        var contN = 0;
        var natureza = oc.Natureza;
        consulta.EstruturaList.forEach(function (value) {
            if (value.Natureza === natureza && value.Fonte === fonte.substr(1, 2)) {
                $("#Natureza option[value='" + value.Codigo + "']").attr('selected', true);
                contN += 1;
            }
        });


        if (contN === 0) {
            msg = (msg.length === 0 ? '\n' : '') + 'CED ' + natureza + '-' + fonte.substr(1, 2) + ' não Cadastrada no SIDS';
        }

        if (msg.length > 0) {
            AbrirModal(msg);
        }
    }

    consulta.carregarDadosCtSiafisico = function (ct) {
        $('#Processo').val(ct.Processo);
        $('#credor').val(ct.CGCCpf);
        $('#NumeroCNPJCPFUGCredor').val(ct.CGCCpf);
        $('#DataEntregaMaterial').val(ct.DataEntregaPrevista);
        $('#DataEmissao').val(ct.DataEmpenhoOriginal);
        $('#CodigoEvento').val(Left(ct.Evento, 6));
        $('#CodigoGestao').val(ct.Gestao);
        $('#DescricaoLogradouroEntrega').val(ct.LocalEntrega);
        $('#DescricaoBairroEntrega').val(ct.Bairro);
        $('#DescricaoCidadeEntrega').val(ct.Cidade);
        $('#NumeroCEPEntrega').val(ct.CEP);

        CarregarTextArea('DescricaoInformacoesAdicionaisEntrega', 3, ct.InformacoesAdicionais);

        $('#NumeroContratoFornecedor').val(ct.NumeroContratoForn);
        $('#NumeroEdital').val(ct.NumeroEdital);
        $("#OrigemMaterialId option[selected='selected']").removeAttr('selected');
        $("#TipoAquisicaoId option[selected='selected']").removeAttr('selected');
        $("#LicitacaoId option[selected='selected']").removeAttr('selected');
        $("#ModalidadeId option[selected='selected']").removeAttr('selected');
        $("#CodigoFonteSiafisico option[selected='selected']").removeAttr('selected');
        $("#OrigemMaterialId option[value='" + Left(ct.OrigemMaterial, 1) + "']").attr('selected', true);
        $("#TipoAquisicaoId option[value='" + Left(ct.ServicoMaterial, 1) + "']").attr('selected', true);
        $("#LicitacaoId option[value='" + Left(ct.TipoCompraLicitacao, 1) + "']").attr('selected', true);
        $("#ModalidadeId option[value='" + Left(ct.ModalidadeEmpenho, 1) + "']").attr('selected', true);
        $("#CodigoFonteSiafisico option[value='" + ct.Fonte + "']").attr('selected', true);
        $('#DescricaoReferenciaLegal').val(ct.ReferenciaLegal);
        $('#CodigoUnidadeGestora').val(ct.UG);
        $('#CodigoUGO').val(ct.UGR);
        $('#CodigoNaturezaItem').val(Right(ct.NaturezaDespesa, 2));

        SelecioarComboCfp();

        $("#Fonte option[value='" + ct.Fonte + "']").attr('selected', true);

        var cont = 0;
        consulta.ProgramaList.forEach(function (programa) {
            if (programa.Ptres === ct.PTRES) {
                $('#Programa option[value="' + programa.Codigo + '"]').attr("selected", true);
                cont += 1;
            }
        });

        consulta.selecionarDropDownPrograma();

        var msg = '';

        if (cont === 0) {
            msg = 'Ptres ' + ct.PTRES + ' não Cadastrado no SIDS';
        }

        consulta.dropDownNatureza();

        var contN = 0;
        var natureza = Left(ct.NaturezaDespesa, 6);
        consulta.EstruturaList.forEach(function (estrutura) {
            if (estrutura.Natureza === natureza && estrutura.Fonte === ct.Fonte.substr(1, 2)) {
                $("#Natureza option[value='" + estrutura.Codigo + "']").attr('selected', true);
                contN += 1;
            }
        });


        if (contN === 0) {
            msg = (msg.length === 0 ? '\n' : '') + 'CED ' + natureza + ' não Cadastrada no SIDS';
        }

        if (msg.length > 0) {
            AbrirModal(msg);
        }
    }

    consulta.carregarDadosEmpenhoSiafem = function () {
        $('#NumeroCNPJCPFUGCredor').val(Left(dadosEmpenho.CgcCpf, 14));
        $('#DataEmissao').val(ConverterData(dadosEmpenho.DataEmissao));
        $('#CodigoEvento').val(Left(dadosEmpenho.Evento, 6));
        $('#CodigoGestaoCredor').val(dadosEmpenho.GestaoCredor);
        $("#OrigemMaterialId option[value='" + Left(dadosEmpenho.OrigemMaterial, 1) + "']").attr('selected', true);
        $("#TipoAquisicaoId option[value='" + Left(dadosEmpenho.ServicoouMaterial, 1) + "']").attr('selected', true);
        $("#LicitacaoId option[value='" + Left(dadosEmpenho.Licitacao, 1) + "']").attr('selected', true);
        $("#ModalidadeId option[value='" + Left(dadosEmpenho.Modalidade, 1) + "']").attr('selected', true);
        $("#CodigoFonteSiafisico option[value='" + dadosEmpenho.Fonte + "']").attr('selected', true);
        $("#Fonte option:contains('" + dadosEmpenho.Fonte + "')").attr('selected', true);
        $("#CodigoNaturezaNe option[value='" + Left(dadosEmpenho.Despesa, 6) + "']").attr('selected', true);
        $('#DescricaoLocalEntregaSiafem').val(dadosEmpenho.Local);
        $('#NumeroProcessoNE').val(dadosEmpenho.NumeroProcesso);
        $('#CodigoNaturezaItem').val(Right(dadosEmpenho.Despesa, 2));
        $('#CodigoUO').val(Left(dadosEmpenho.Uo, 5));

        var cont = 0;
        consulta.ProgramaList.forEach(function (programa) {
            if (programa.Ptres === dadosEmpenho.Ptres && programa.Ano === $('#AnoExercicio').val()) {
                $("#Ptres option[value='" + programa.Ptres + "']").attr('selected', true);
                cont += 1;
            }
        });

        SelecioarComboCfp();

        var msg = '';
        if (cont === 0) {
            msg = 'Ptres ' + dadosEmpenho.Ptres + ' não Cadastrado no SIDS';
        }

        consulta.dropDownNatureza();

        var contN = 0;

        var natureza = Left(dadosEmpenho.Despesa, 6);
        consulta.EstruturaList.forEach(function (estrutura) {
            if (estrutura.Natureza === natureza && estrutura.Fonte === dadosEmpenho.Fonte.substr(1, 2)) {
                $("#Natureza option[value='" + estrutura.Codigo + "']").attr('selected', true);
                contN += 1;
            }
        });


        if (contN === 0) {
            msg = (msg.length === 0 ? '\n' : '') + 'CED ' + natureza + ' não Cadastrada no SIDS';
        }

        if (msg.length > 0) {
            AbrirModal(msg);

        }
    }

    consulta.carregarDadosRapSaldo = function () {
        $('#NumeroOriginalProdesp').val(dados.NumeroOriginalProdesp);
        $('#NumeroContrato').val(dados.NumeroContrato);
        $('#Valor').val(dados.Valor);
   }

    consulta.carregarDadosAnulacaoRap = function () {
        //$('#NumeroOriginalProdesp').val(dados.NumeroOriginalProdesp);
       
        liquidacao.scenaryFactoryForPesquisaAnulacaoRequisicaoRap();
    }

    consulta.carregarDadosInscritoRap = function () {
        //$('#NumeroOriginalProdesp').val(dados.NumeroOriginalProdesp);
        //$('#NumeroRecibo').val(dados.NumeroRecibo);

        liquidacao.scenaryFactoryForPesquisaSubempenhoInscritoRap();
    }

    consulta.formatarNatureza = function (entity) {
        return entity.OutCed1.concat('.', entity.OutCed2, '.', entity.OutCed3, '.', entity.OutCed4, '.', entity.OutCed5);
    }

    consulta.formatarPrograma = function (entity) {
        return entity.OutCfp1.concat('.', entity.OutCfp2, '.', entity.OutCfp3, '.', entity.OutCfp4, '.', entity.OutCfp5);
    }

    consulta.formatarEspecificacaoDespesa = function (entity) {
        return entity.OutEspecDespesa1.concat('\n', entity.OutEspecDespesa2, '\n', entity.OutEspecDespesa3, '\n', entity.OutEspecDespesa4, '\n', entity.OutEspecDespesa5, '\n', entity.OutEspecDespesa5, '\n', entity.OutEspecDespesa7, '\n', entity.OutEspecDespesa8, '\n', entity.OutEspecDespesa9);
    }

    consulta.formatarCfp = function (entity) {
        return consulta.formatarPrograma(entity).concat(' / ', consulta.formatarNatureza(entity));
    }

    consulta.carregarDadosConsulta = function (entity, type) {
        if (type === 1) {
            $('#modalConsultaContrato').modal('toggle');
        }

        $('#modalDadosEmpenho').modal();

        $('#txtNumEmpenho').val(entity.NumEmpenho);
        $('#txtNeSiafem').val(entity.OutNeSiafem);
        $('#txtFonteSiafem').val(entity.OutFonteSiafem);
        $('#txtNumProcesso').val(entity.OutNumProcesso);
        $('#txtCfp').val(consulta.formatarCfp(entity));
        $('#txtNumAplicacao').val(entity.OutCodAplicacao);
        $('#txtDataEmissao').val(entity.OutDataEmissao);
        $('#txtPrevisaoInicio').val(entity.OutPrevInic);
        $('#txtOrigem').val(entity.OutOrigemRecurso);
        $('#txtDestino').val(entity.OutDestRecurso);
        $('#txtCodObra').val(entity.OutCodObra);
        $('#txtNumContrato').val(entity.OutNumContrato);
        $('#txtValor').val(entity.OutValorEmpenho);
        $('#txtValorReforco').val(entity.OutValorReforco);
        $('#txtValorAnulado').val(entity.OutValorAnulado);
        $('#txtValorSubmpenhado').val(entity.OutValorSubEmpenhado);
        $('#txtSaldoEmpenho').val(entity.OutSaldoEmpenho);
        $('#txtSeqTam').val(entity.OutSeqTam);
        $('#txtQuota1').val(entity.OutSaldoQ1);
        $('#txtQuota2').val(entity.OutSaldoQ2);
        $('#txtQuota3').val(entity.OutSaldoQ3);
        $('#txtQuota4').val(entity.OutSaldoQ4);
        $('#txtEspecificacao').text(consulta.formatarEspecificacaoDespesa(entity));
        $('#txtCNPJ').val(entity.OutCnpjCpf);
        $('#txtCredor').val(entity.OutCredor);
        $('#txtEndereco').val(entity.OutEndereco);
        $('#txtBairro').val(entity.OutBairro);
        $('#txtMunicipio').val(entity.OutMunicipio);
        $('#txtEstado').val(entity.OutEstado);
        $('#txtCEP').val(entity.OutCep);
    }

    consulta.buttonConsultaProvider = function (action, value) {
        return "<button class='btn btn-xs btn-primary'".concat(action.length > 0
            ? " onclick='".concat(action, '(', value, ")'")
            : ''
        , "><i class='fa fa-search'></i></button>");
    }

    consulta.serviceConsultaDetalheProvider = function (entity) {
        switch (entity[0].OutEvento) {
            case 'RESERVA': //1
                return consulta.buttonConsultaProvider('consulta.consultarReserva', entity[0].OutNumero);
            case 'EMPENHO': //2
                return consulta.buttonConsultaProvider('consulta.consultarEmpenho', entity[0].OutNumero);
            case 'SUBEMPENHO': //3
                return consulta.buttonConsultaProvider('consulta.consultarSubempenho', entity[0].OutNumero);
            case 'REQUISICAO DE RAP': //4
            case 'DESP.EXTRA ORCAMENTARIA': //5
            case 'ORDEM DE PAGAMENTO': //6
            case 'CAUCAO': //7
            case 'RECEITA': //8
            default:
                return consulta.buttonConsultaProvider('', '');
        }
    }

    consulta.datatableAddRowForEstrutura = function (values) {

        $('#tblListaEstrutura > thead').remove();
        $('#tblListaEstrutura').append('<thead></thead>');

        if (area === 'Reserva') {
            $('#tblListaEstrutura > thead').append('<tr>' +
                "    <th class='text-center'>Nº Res.</th>" +
                "    <th class='text-center'>CED</th>" +
                "    <th class='text-right'>Cod. Apl.</th>" +
                "    <th class='text-center'>Dt. Inicial</th>" +
                "    <th class='text-right'>Valor Atual</th>" +
                "    <th class='text-right'>Valor Empenhado</th>" +
                "    <th class='text-right'>Disp. A Empenhar</th>" +
                "    <th class='text-center'></th>" +
                '</tr>'
            );

            if (!$('#tblListaEstrutura').hasClass('tbDataTables')) {
                $('#tblListaEstrutura').addClass('_tbDataTables');
                BuildDataTables.ById('#tblListaEstrutura');
            }

            $('#tblListaEstrutura').DataTable().clear().draw(true);
            $.each(values,
                function (index, x) {
                    $('#tblListaEstrutura')
                        .DataTable()
                        .row.add([
                            x.OutNrReserva,
                            x.OutCED,
                            x.OutCodAplicacao,
                            x.OutDataInic,
                            x.OutValorAtual,
                            x.OutValorEmpenhado,
                            x.OutDispEmpenhar,
                            "<button class='btn btn-xs btn-primary' onclick='consultarReserva(" + x.OutNrReserva + ", 0)'><i class='fa fa-search'></i></button>"
                        ]).draw(true);
                });
        }
        else {
            $('#tblListaEstrutura > thead').append('<tr>' +
                "    <th class='text-center'>Nº Empenho.</th>" +
                "    <th class='text-right'>Cod. Apl.</th>" +
                "    <th class='text-right'>Valor Atual</th>" +
                "    <th class='text-right'>Valor Sub-Empenhado</th>" +
                "    <th class='text-right'>Disp. A Empenhar</th>" +
                "    <th class='text-center'></th>" +
                '</tr>'
            );

            if (!$('#tblListaEstrutura').hasClass('_tbDataTables')) {
                $('#tblListaEstrutura').addClass('_tbDataTables');
                BuildDataTables.ById('#tblListaEstrutura');
            }

            $('#tblListaEstrutura').DataTable().clear().draw(true);
            $.each(values,
                function (index, x) {
                    $('#tblListaEstrutura')
                        .DataTable()
                        .row.add([
                            x.OutNrEmpenho,
                            x.OutCodAplicacao,
                            x.OutValorAtual,
                            x.OutValorSubEmpenhado,
                            x.OutDispSubEmpenhar,
                            "<button class='btn btn-xs btn-primary' onclick='consulta.consultarEmpenho(" + x.OutNrEmpenho + ",0)'><i class='fa fa-search'></i></button>"
                        ])
                        .draw(true);
                });
        }
    }

    consulta.datatableAddRowForContrato = function (values) {
        $('#tblListaContrato').DataTable().clear().draw(true);

        $.each(values, function (index, x) {
            $('#tblListaContrato').DataTable().row.add([
                x.OutEvento,
                x.OutNumero,
                x.OutData,
                x.OutValor,
                consulta.serviceConsultaDetalheProvider([x])
            ]).draw(true);
        });
    }

    consulta.datatableAddRowForEmpenhos = function (values) {
        $('#tblListaEmpenhos').DataTable().clear().draw(true);

        $.each(values, function (index, x) {
            $('#tblListaEmpenhos').DataTable().row.add([
                x.numerone,
                x.evento,
                x.natureza,
                x.credor,
                x.valor,
                "<button class='btn btn-xs btn-primary' onclick='consulta.consultarNe(\"" + x.numerone + "\")'><i class='fa fa-search'></i></button>"
            ]).draw(true);
        });
    }

    consulta.limparAssinaturasPorTipo = function (tipo) {
        switch (tipo) {
            case 1:
                $('#txtAutorizadoGrupo').val('');
                $('#txtAutorizadoOrgao').val('');
                $('#txtAutorizadoNome').val('');
                $('#txtAutorizadoCargo').val('');
                break;
            case 2:
                $('#txtExaminadoGrupo').val('');
                $('#txtExaminadoOrgao').val('');
                $('#txtExaminadoNome').val('');
                $('#txtExaminadoCargo').val('');
                break;
            case 3:
                $('#txtResponsavelGrupo').val('');
                $('#txtResponsavelOrgao').val('');
                $('#txtResponsavelCargo').val('');
                $('#txtResponsavelNome').val('');
                break;
            default:
                break;
        }
    }

    consulta.dropDownPrograma = function () {
        consulta.Programa = 0;
        $("#Programa").empty();
        $("#Programa").append("<option value='' >Selecione</option>");

        consulta.ProgramaList = consulta.ProgramaList.sort(dynamicSort("Cfp"));
        consulta.ProgramaList.forEach(function (programa) {
            var ano = $("#AnoExercicio").val() != ""
                ? programa.Ano == $("#AnoExercicio").val()
                : programa.Ano == new Date().getFullYear() || (programa.Ano == new Date().getFullYear() - 1);

            if (ano) {
                var cfp = programa.Cfp.substring(0, 2) + "." +
                          programa.Cfp.substring(2, 5) + "." +
                          programa.Cfp.substring(5, 9) + "." +
                          programa.Cfp.substring(9, 13) + " ";

                consulta.Programa = programa.Ptres == $("#Ptres").val() ? programa.Codigo : 0;

                $("#Programa").append("<option value='" + programa.Codigo + "' >" + cfp + programa.Descricao + "</option>");
            }
        });

        ValicarCampos($("#Programa"));
    }

    consulta.dropDownNatureza = function () {
        $("#Natureza").empty();
        $("#Natureza").append("<option value='' >Selecione</option>");

        consulta.EstruturaList = consulta.EstruturaList.sort(dynamicSort("Natureza"));

        var anoProg;
        consulta.EstruturaList.forEach(function (estrutura) {
            consulta.ProgramaList.forEach(function (programa) {
                if (programa.Codigo == estrutura.Programa)
                    anoProg = programa.Ano;
            });

            var ano = (anoProg == $("#AnoExercicio").val() || $("#AnoExercicio").val() == "");
            var cfp = (estrutura.Programa == $("#Programa").val() || $("#Programa").val() == "");

            if (cfp && ano) {
                consulta.Programa = estrutura.Codigo;
                $("#Natureza").append("<option value='" + estrutura.Codigo + "' >" + estrutura.Natureza.replace(/(\d{1})(\d{1})(\d{2})(\d{2})$/, "$1.$2.$3.$4") + " - " + estrutura.Fonte + "</option>");
            }
        });

        ValicarCampos($("#Natureza"));
    }

    consulta.confirmarContrato = function () {
        var msg = '';
        consulta.validateConnectionIsOpen();

        $('#Obra').val(consulta.EntityContrato.OutCodObra.replace(/[\.-]/g, '').replace(/(\d)(\d{1})$/, '$1-$2'));
        $('#Processo').val(consulta.EntityContrato.OutProcesSiafem);

        consulta.dropDownPrograma();

        var prog = consulta.EntityContrato.OutPrograma.replace(/[\.-]/g, "");

        var cont = 0;
        consulta.ProgramaList.forEach(function (programa) {
            if (programa.Cfp === prog.substr(0, 13)) {
                $("#Programa option[value='" + programa.Codigo + "']").attr('selected', true);
                cont += 1;
            }
        });

        var natureza = consulta.EntityContrato.OutCED.replace(/[\.-]/g, '');

        if (cont === 0) {
            msg = 'Programa ' + prog.substr(0, 13) + ' não Cadastrado no SIDS';
        }

        consulta.dropDownNatureza();

        var contN = 0;
        consulta.EstruturaList.forEach(function (estrutura) {
            if (estrutura.Natureza === natureza && estrutura.Fonte === prog.substr(13, 2)) {
                $("#Natureza option[value='" + estrutura.Codigo + "']").attr('selected', true);
                contN += 1;
            }
        });

        if (contN === 0) {
            msg = (msg.length === 0 ? '\n' : '') + 'CED ' + natureza + '-' + prog.substr(13, 2) + ' não Cadastrada no SIDS';
        }

        if (msg.length > 0)
            AbrirModal(msg);

        consulta.selecionarDropDownPrograma();
        ValicarCampos('campo');
    }

    consulta.confirmarReserva = function () {
        var msg = '';
        consulta.validateConnectionIsOpen();

        $('#Reserva').val(dadosReserva.OutNumReserva);
        $('#Processo').val(dadosReserva.OutNumProcesso);

        var obra = dadosReserva.OutCodObra === ''
            ? dadosReserva.OutCodAplicacao.replace(/[\.-]/g, '')
            : dadosReserva.OutCodObra.replace(/[\.-]/g, '');

        $('#Obra').val(obra.replace(/(\d)(\d{1})$/, '$1-$2'));

        $("#Fontes option[value='" + dadosReserva.OutDestRecurso + "']").attr('selected', true);


        if (dadosReserva.OutNumContrato.length > 0)
            $('input#Contrato').val(dadosReserva.OutNumContrato);

        if (dadosDatabase !== null) {
            //obtido no Sids
            $("#Fonte option[value='" + dadosDatabase.Fonte + "']").attr('selected', true);
            $('#Oc').val(dadosDatabase.Oc);
            $('#Ugo').val(dadosDatabase.Ugo);
            $('#Uo').val(dadosDatabase.Uo);
            if (dadosDatabase.Contrato.length > 0) {
                $('#Contrato').val(dadosDatabase.Contrato);
            }
        }

        var prog = cfp.replace(/[\.-]/g, '');
        consulta.dropDownPrograma();

        var cont = 0;
        consulta.ProgramaList.forEach(function (programa) {
            if (programa.Cfp === prog.substr(0, 13)) {
                $("#Programa option[value='" + programa.Codigo + "']").attr('selected', true);
                cont += 1;
            }
        });

        consulta.selecionarDropDownPrograma();

        if (cont === 0) {
            msg = 'Programa ' + prog.substr(0, 13) + ' não Cadastrado no SIDS';
        }

        var natureza = CED.replace(/[\.-]/g, '');
        consulta.dropDownNatureza();

        var contN = 0;
        consulta.EstruturaList.forEach(function (estrutura) {
            if (estrutura.Natureza === natureza && estrutura.Fonte === prog.substr(13, 2)) {
                $("#Natureza option[value='" + estrutura.Codigo + "']").attr('selected', true);
                contN += 1;
            }
        });

        if (contN === 0) {
            msg = (msg.length === 0 ? '\n' : '') + 'CED ' + natureza + '-' + prog.substr(13, 2) + ' não Cadastrada no SIDS';
        }

        if (msg.length > 0) {
            AbrirModal(msg);
        }

        $('#DescEspecificacaoDespesa > .Proximo')[0].value = removerAcentos(dadosReserva.OutEspecDespesa1);
        $('#DescEspecificacaoDespesa > .Proximo')[1].value = removerAcentos(dadosReserva.OutEspecDespesa2);
        $('#DescEspecificacaoDespesa > .Proximo')[2].value = removerAcentos(dadosReserva.OutEspecDespesa3);
        $('#DescEspecificacaoDespesa > .Proximo')[3].value = removerAcentos(dadosReserva.OutEspecDespesa4);
        $('#DescEspecificacaoDespesa > .Proximo')[4].value = removerAcentos(dadosReserva.OutEspecDespesa5);
        $('#DescEspecificacaoDespesa > .Proximo')[5].value = removerAcentos(dadosReserva.OutEspecDespesa6);
        $('#DescEspecificacaoDespesa > .Proximo')[6].value = removerAcentos(dadosReserva.OutEspecDespesa7);
        $('#DescEspecificacaoDespesa > .Proximo')[7].value = removerAcentos(dadosReserva.OutEspecDespesa8);
        $('#DescEspecificacaoDespesa > .Proximo')[8].value = removerAcentos(dadosReserva.OutEspecDespesa9);

        $('#EspecDespesa').val('000');

        if ($('#modalConsultaPorEstrutura').is(':visible')) {
            $('#modalConsultaPorEstrutura').modal('toggle');
        }
        $('#resultadoConsulta').show();

        ValicarCampos('campo');
    }

    consulta.confirmarEmpenho = function () {
        consulta.validateConnectionIsOpen();

        $('#CodigoEmpenho').val(dadosEmpenho.NumEmpenho);
        $('#Processo').val(dadosEmpenho.OutNumProcesso);
        $('#CodigoEmpenhoOriginal').val(dadosEmpenho.OutNeSiafem);
        $("#CodigoFonteSiafisico option[value='" + dadosEmpenho.OutFonteSiafem + "']").attr('selected', true);
        $('#Obra').val(dadosEmpenho.OutCodAplicacao);
        $('#DataEmissao').val(dadosEmpenho.OutDataEmissao);
        $('#DataEntregaMaterialSiafisico').val(dadosEmpenho.OutPrevInic);
        $("#Fonte option:contains('" + dadosEmpenho.OutOrigemRecurso + "')");
        $("#DestinoId option[value='" + dadosEmpenho.OutDestRecurso + "']").attr('selected', true);
        $('#Obra').val(dadosEmpenho.OutCodObra);

        if (dadosEmpenho.OutNumContrato.length > 0) {
            $('input#Contrato').val(dadosEmpenho.OutNumContrato);
        }

        $('#DescricaoLogradouroEntrega').val(dadosEmpenho.OutEndereco);
        $('#DescricaoBairroEntrega').val(dadosEmpenho.OutBairro);
        $('#CodigoMunicipio').val(dadosEmpenho.OutMunicipio);
        $('#NumeroCEPEntrega').val(dadosEmpenho.OutCep);

        var obra = dadosEmpenho.OutCodObra === '' ? dadosEmpenho.OutCodAplicacao.replace(/[\.-]/g, '') : dadosEmpenho.OutCodObra.replace(/[\.-]/g, '');

        $('input#Obra').val(obra.replace(/(\d)(\d{1})$/, '$1-$2'));

        $("#Fontes option[value='" + dadosEmpenho.OutDestRecurso + "']").attr('selected', true);

        var prog = dadosEmpenho.OutCfp1 + dadosEmpenho.OutCfp2 + dadosEmpenho.OutCfp3 + dadosEmpenho.OutCfp4;
        SelecioarComboCfp();

        var cont = 0;
        consulta.ProgramaList.forEach(function (programa) {
            if (programa.Cfp === prog) {
                $("#Programa option[value='" + programa.Codigo + "']").attr('selected', true);
                cont += 1;
            }
        });

        if (cont === 0) {
            msg = "Cfp " + prog + " não Cadastrado no SIDS";
        }

        consulta.dropDownNatureza();
        var natureza = CED.replace(/[\.-]/g, "");

        var msg = '';

        if (cont === 0) {
            msg = 'Programa ' + prog.substr(0, 13) + ' não Cadastrado no SIDS';
        }

        var contN = 0;
        consulta.EstruturaList.forEach(function (estrutura) {
            if (estrutura.Natureza === natureza && estrutura.Fonte === dadosEmpenho.OutFonteSiafem.substr(1, 2)) {
                $("#Natureza option[value='" + estrutura.Codigo + "']").attr('selected', true);
                contN += 1;
            }
        });


        if (contN === 0) {
            msg = (msg.length === 0 ? '\n' : '') + 'CED ' + natureza + '-' + dadosEmpenho.OutFonteSiafem.substr(1, 2) + ' não Cadastrada no SIDS';
        }

        if (msg.length > 0) {
            AbrirModal(msg);
        }

        consulta.selecionarDropDownPrograma();

        $('#DescEspecificacaoDespesa > .Proximo')[0].value = removerAcentos(dadosEmpenho.OutEspecDespesa1);
        $('#DescEspecificacaoDespesa > .Proximo')[1].value = removerAcentos(dadosEmpenho.OutEspecDespesa2);
        $('#DescEspecificacaoDespesa > .Proximo')[2].value = removerAcentos(dadosEmpenho.OutEspecDespesa3);
        $('#DescEspecificacaoDespesa > .Proximo')[3].value = removerAcentos(dadosEmpenho.OutEspecDespesa4);
        $('#DescEspecificacaoDespesa > .Proximo')[4].value = removerAcentos(dadosEmpenho.OutEspecDespesa5);
        $('#DescEspecificacaoDespesa > .Proximo')[5].value = removerAcentos(dadosEmpenho.OutEspecDespesa6);
        $('#DescEspecificacaoDespesa > .Proximo')[6].value = removerAcentos(dadosEmpenho.OutEspecDespesa7);
        $('#DescEspecificacaoDespesa > .Proximo')[7].value = removerAcentos(dadosEmpenho.OutEspecDespesa8);
        $('#DescEspecificacaoDespesa > .Proximo')[8].value = removerAcentos(dadosEmpenho.OutEspecDespesa9);

        $('#EspecDespesa').val('000');


        if ($('#modalConsultaPorEstrutura').is(':visible')) {
            $('#modalConsultaPorEstrutura').modal('toggle');
        }

        ValicarCampos('campo');
    }

    consulta.selecionarDropDownPrograma = function () {
        consulta.Programa = 0;

        $("#Ptres").empty(); // remove all options bar first
        $("#Ptres").append("<option value='' >Selecione</option>");
        //var ptres;

        consulta.ProgramaList = consulta.ProgramaList.sort(dynamicSort("Ptres"));
        consulta.ProgramaList.forEach(function (programa) {
            var ano = $("#AnoExercicio").val() != ""
               ? programa.Ano == $("#AnoExercicio").val()
               : programa.Ano == new Date().getFullYear() || (programa.Ano == new Date().getFullYear() - 1);

            if (ano) {
                //ptres = programa.Ptres;
                $("#Ptres").append("<option value='" + programa.Ptres + "' >" + programa.Ptres + "</option>");
                consulta.Programa = programa.Codigo == $("#Programa").val() ? programa.Ptres : 0;
                $('#Ptres option[value="' + consulta.Programa + '"]').attr('selected', true);

            }
        });

        ValicarCampos($("#Ptres"));
    }

    consulta.validateContrato = function (entity) {
        if (entity.length === 0) {
            AbrirModal('Favor informar número do contrato!');
            return false;
        }
    }

    consulta.validateEntity = function (entity, message) {
        if (entity === undefined || entity === null || entity === 0) {
            AbrirModal(message);
            return false;
        }
    }

    consulta.consultarContrato = function (tipoId) {
        var contrato = {};

        consulta.validateConnectionIsOpen();
        
        var numero = $('#Contrato').val().replace(/[\s*\.-]/g, '');
        consulta.validateContrato(numero);

        var numcontrato = consulta.EntityContrato
            ? consulta.EntityContrato.OutContrato.replace(/\s/g, '').replace(/[\.-]/g, '')
            : '';

        if (numero === numcontrato) {
            waitingDialog.show('Consultando');

            $('#modalConsultaContrato').modal('toggle');

            var contratoValue = contrato.OutContrato.replace(/\s/g, "").replace(/[\.-]/g, "").replace(/(\d{2})(\d{2})(\d{5})(\d{1})/g, "\$1.\$2.\$3\-\$4");

            $('input#Contrato').val(contratoValue);
            $('input#txtCNPJ').val(consulta.EntityContrato.OutCpfcnpj.replace(/(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})/g, '\$1.\$2.\$3\/\$4\-\$5'));
            $('input#Obra').val(consulta.EntityContrato.OutCodObra);
            $('input#txtContratada').val(consulta.EntityContrato.OutContratada);
            $('input#txtObjeto').val(consulta.EntityContrato.OutObjeto);
            $('input#ProcessoSiafem').val(consulta.EntityContrato.OutProcesSiafem);
            $('input#txtPragmatica').val(consulta.EntityContrato.OutPrograma);
            $('input#txtCED').val(consulta.EntityContrato.OutCED);

            consulta.datatableAddRowForContrato(consulta.EntityContratoList);

            waitingDialog.hide();
        }
        else {
            consulta.EntityContratoList = [];

            contrato = {
                NumContrato: numero,
                Type: tipoId
            }


            $.ajax({
                datatype: 'JSON',
                type: 'POST',
                url: "/Reserva/Reserva/ConsultarContrato", 
                data: JSON.stringify({ contrato: contrato }),
                contentType: 'application/json; charset=utf-8',
                beforeSend: function () {
                    waitingDialog.show('Consultando');
                },
                success: function (dados) {
                    if (dados.Status === 'Sucesso') {
                        waitingDialog.hide();

                        consulta.EntityContrato = dados.Contrato;
                        consulta.EntityContratoList = dados.Contrato.ListConsultaContrato;

                        $('#modalConsultaContrato').modal();
                        $('.classContrato').val(consulta.EntityContrato.OutContrato.replace(/\s/g, '').replace(/[\.-]/g, '').replace(/(\d{2})(\d{2})(\d{5})(\d{1})/g, '\$1.\$2.\$3\-\$4'));
                        $('#txtCNPJ').val(consulta.EntityContrato.OutCpfcnpj.replace(/(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})/g, '\$1.\$2.\$3\/\$4\-\$5'));
                        $('#Obra').val(consulta.EntityContrato.OutCodObra);
                        $('#txtContratada').val(consulta.EntityContrato.OutContratada);
                        $('#txtObjeto').val(consulta.EntityContrato.OutObjeto);
                        $('#ProcessoSiafem').val(consulta.EntityContrato.OutProcesSiafem);
                        $('#txtPragmatica').val(consulta.EntityContrato.OutPrograma);
                        $('#DivtxtCED').hide();

                        consulta.datatableAddRowForContrato(consulta.EntityContratoList);

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
    }

    consulta.consultarReserva = function (id, type) {
        consulta.validateConnectionIsOpen();
        consulta.validateEntity(id, 'Favor informar o número da Reserva!');

        $.ajax({
            datatype: 'JSON',
            type: 'POST',
            url: '/ConsutasBase/ConsultarReserva',
            data: JSON.stringify({ reserva: id }),
            contentType: 'application/json; charset=utf-8',
            beforeSend: function () {
                waitingDialog.show('Consultando');
            },
            success: function (dados) {
                if (dados.Status === 'Sucesso') {
                    consulta.carregarDadosConsulta(dados.Reserva, type);
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
    };

    consulta.consultarEmpenho = function (id, type) {
        consulta.validateConnectionIsOpen();
        consulta.validateEntity(id, 'Favor informar o número da Empenho!');

        $.ajax({
            datatype: 'JSON',
            type: 'POST',
            url: '/ConsutasBase/ConsultarEmpenho',
            data: JSON.stringify({ empenho: id }),
            contentType: 'application/json; charset=utf-8',
            beforeSend: function () {
                waitingDialog.show('Consultando');
            },
            success: function (dados) {
                if (dados.Status === 'Sucesso') {
                    consulta.carregarDadosConsulta(dados.Empenho, type);
                }
                else {
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
    };

    consulta.consultarSubempenho = function (id, type) {
        consulta.validateConnectionIsOpen();
        consulta.validateEntity(id, 'Favor informar o número do Subempenho!');

        $.ajax({
            datatype: 'JSON',
            type: 'POST',
            url: '/ConsutasBase/ConsultarSubempenho',
            data: JSON.stringify({ subempenho: id }),
            contentType: 'application/json; charset=utf-8',
            beforeSend: function () {
                waitingDialog.show('Consultando');
            },
            success: function (dados) {
                if (dados.Status === 'Sucesso') {
                    consulta.carregarDadosConsulta(dados.Subempenho, type);
                }
                else {
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
    };

    consulta.consultarEstrutura = function () {
        consulta.validateConnectionIsOpen();

        var valorCfp = obterEstruturaProgramaInfo();
        var valorNatureza = obterEstruturaNaturezaInfo();
        var valorFonte = obterEstruturaFonteInfo();
        var valorOrgao = obterEstruturaRegionalInfo();

        if (valorOrgao === null) {
            AbrirModal('Favor informar número do orgão!');
            return false;
        }
        else if ($('#AnoExercicio').val() === '') {
            AbrirModal('Favor informar o Ano!');
            return false;
        }
        else if (valorCfp === null) {
            AbrirModal('Favor informar o CFP!');
            return false;
        }
        else if (valorFonte === null) {
            AbrirModal('Favor informar a Origem do Recurso!');
            return false;
        }

        var estruturaFiltrosDto = {
            AnoExercicio: $('#AnoExercicio').val(),
            RegionalId: $('#Regional').val(),
            Cfp: valorCfp,
            Natureza: valorNatureza,
            Programa: $('#Ptres').val(),
            OrigemRecurso: valorFonte,
            Processo: $('#Processo').val(),
            Tipo: area
        };


        $.ajax({
            datatype: 'JSON',
            type: 'POST',
            url: '/ConsutasBase/ConsultarPorEstrutura',
            data: JSON.stringify({ estrutura: estruturaFiltrosDto }),
            contentType: 'application/json; charset=utf-8',
            beforeSend: function () {
                waitingDialog.show('Consultando');
            },
            success: function (dados) {
                if (dados.Status === 'Sucesso') {
                    $('#modalConsultaPorEstrutura').modal('toggle');
                    $('#txtAno').val($("#AnoExercicio").val());
                    $('#txtOrgao').val(valorOrgao);
                    $('#txtEstrutura').val(valorCfp);

                    datatableAddRowForEstrutura(dados.Estrutura.ListConsultaEstrutura); //propriedade List da classe : "ConsultaReservaEstrutura"
                }
                else {
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

    consulta.consultarEspecificacao = function (valor) {
        consulta.validateConnectionIsOpen();
        console.log(valor.length);
        if (valor.length === 3) {
            $.ajax({
                datatype: 'JSON',
                type: 'POST',
                url: '/ConsutasBase/ConsultarEspecificacao',
                data: JSON.stringify({ codigo: valor }),
                contentType: 'application/json; charset=utf-8',
                beforeSend: function () {
                    waitingDialog.show('Consultando');
                },
                success: function (dados) {
                    if (dados.Status === 'Sucesso') {
                        var dadosEspecificacao = dados.Especificacao;

                        waitingDialog.hide();

                        $('#DescEspecificacaoDespesa > .Proximo')[0].value = removerAcentos(dadosEspecificacao.outEspecDespesa);
                        $('#DescEspecificacaoDespesa > .Proximo')[1].value = removerAcentos(dadosEspecificacao.outEspecDespesa_02);
                        $('#DescEspecificacaoDespesa > .Proximo')[2].value = removerAcentos(dadosEspecificacao.outEspecDespesa_03);
                        $('#DescEspecificacaoDespesa > .Proximo')[3].value = removerAcentos(dadosEspecificacao.outEspecDespesa_04);
                        $('#DescEspecificacaoDespesa > .Proximo')[4].value = removerAcentos(dadosEspecificacao.outEspecDespesa_05);
                        $('#DescEspecificacaoDespesa > .Proximo')[5].value = removerAcentos(dadosEspecificacao.outEspecDespesa_06);
                        $('#DescEspecificacaoDespesa > .Proximo')[6].value = removerAcentos(dadosEspecificacao.outEspecDespesa_07);
                        $('#DescEspecificacaoDespesa > .Proximo')[7].value = removerAcentos(dadosEspecificacao.outEspecDespesa_08);
                        $('#DescEspecificacaoDespesa > .Proximo')[8].value = removerAcentos(dadosEspecificacao.outEspecDespesa_09);
                        $('#DescEspecificacaoDespesa > .Proximo').trigger('input');
                    }
                    else {
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


        ValicarCampos('campo');
    };

    consulta.consultarAssinatura = function (valor, tipo) {

        consulta.validateConnectionIsOpen();

        $.ajax({
            datatype: 'JSON',
            type: 'POST',
            url: '/ConsutasBase/ConsultarAssinatura',
            data: JSON.stringify({ codigo: valor, tipo: tipo }),
            contentType: 'application/json; charset=utf-8',
            beforeSend: function () {
                waitingDialog.show('Consultando');
            },
            success: function (dados) {
                if (dados.Status === 'Sucesso') {
                    switch (tipo) {
                        case 1:
                            $('#txtAutorizadoGrupo').val(dados.Assinatura.outGrupoAutorizador);
                            $('#txtAutorizadoOrgao').val(dados.Assinatura.outOrgaoAutorizador);
                            $('#txtAutorizadoNome').val(dados.Assinatura.outNomeAutorizador);
                            $('#txtAutorizadoCargo').val(dados.Assinatura.outCargoAutorizador);
                            $('#CodAssAutorizado').trigger('input');
                            break;
                        case 2:
                            $('#txtExaminadoGrupo').val(dados.Assinatura.outGrupoExaminador);
                            $('#txtExaminadoOrgao').val(dados.Assinatura.outOrgaoExaminador);
                            $('#txtExaminadoNome').val(dados.Assinatura.outNomeExaminador);
                            $('#txtExaminadoCargo').val(dados.Assinatura.outCargoExaminador);
                            $('#CodAssExaminado').trigger('input');
                            break;
                        case 3:
                            $('#txtResponsavelGrupo').val(dados.Assinatura.outGrupoResponsavel);
                            $('#txtResponsavelOrgao').val(dados.Assinatura.outOrgaoResponsavel);
                            $('#txtResponsavelNome').val(dados.Assinatura.outNomeResponsavel);
                            $('#txtResponsavelCargo').val(dados.Assinatura.outCargoResponsavel);
                            $('#CodAssResponsavel').trigger('input');
                            break;
                        default:
                            break;
                    }
                }
                else {
                    switch (tipo) {
                        case 1:
                            $('div >#CodAssAutorizado').val('');
                            break;
                        case 2:
                            $('div >#CodAssExaminado').val('');
                            break;
                        case 3:
                            $('div >#CodAssResponsavel').val('');
                            break;
                        default:
                            break;
                    }

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
    };

    consulta.LimparCombo = function (tipo) {
        if ((tipo == 1)) {

            document.getElementById('txtAutorizadoGrupo').value = "";
            document.getElementById('txtAutorizadoOrgao').value = "";
            document.getElementById('txtAutorizadoNome').value = "";
            document.getElementById('txtAutorizadoCargo').value = "";
        }

        else if ((tipo == 2)) {
            document.getElementById('txtExaminadoGrupo').value = "";
            document.getElementById('txtExaminadoOrgao').value = "";
            document.getElementById('txtExaminadoNome').value = "";
            document.getElementById('txtExaminadoCargo').value = "";

        }

        else if ((tipo == 3)) {
            document.getElementById('txtResponsavelGrupo').value = "";
            document.getElementById('txtResponsavelOrgao').value = "";
            document.getElementById('txtResponsavelCargo').value = "";
            document.getElementById('txtResponsavelNome').value = "";

        }
    }


    consulta.consultarOc = function () {
        consulta.validateConnectionIsOpen();

        if ($('#Oc').val().length === 0 || $('#Oc').val().length < 11 || $('#Oc').val() === '' || numOc === $('#Oc').val()) {
            return false;
        }

        var mydate = new Date();
        var year = mydate.getYear();
        var month = mydate.getMonth();
        if (year < 1000) {
            year += 1900;
        }

        numOc = $('#Oc').val();

        var dtoOc = {
            Oc: $('#Oc').val(),
            Ugo: $('#Ugo').val(),
            Ug: $('#Uo').val()
        };

        $.ajax({
            datatype: 'JSON',
            type: 'POST',
            url: "/ConsutasBase/ConsultarOc",
            data: JSON.stringify({ dtoOc: dtoOc }),
            contentType: 'application/json; charset=utf-8',
            beforeSend: function () {
                waitingDialog.show('Consultando');
            },
            success: function (dados) {
                if (dados.Status === 'Sucesso') {
                    carregarDadosOc(dados.Oc);
                }
                else {
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
    };

    consulta.consultarNe = function (empenho) {
        consulta.validateConnectionIsOpen();

        $.ajax({
            datatype: 'JSON',
            type: 'POST',
            url: '/ConsutasBase/ConsultarNe',
            data: JSON.stringify({ numNe: Left($('#CodigoEmpenhoOriginal').val(), 4) + empenho }),
            contentType: 'application/json; charset=utf-8',
            beforeSend: function () {
                waitingDialog.show('Consultando');
            },
            success: function (dados) {
                if (dados.Status === 'Sucesso') {
                    dadosEmpenho = dados.Empenho;

                    $('#modalConsultaEmpenhos').modal('toggle');

                    $('#modalDadosEmpenhoSIAFEM').modal();

                    $('#txtCnpjCpfCredor').val(dadosEmpenho.CgcCpf);
                    $('#txtDataEmissao').val(dadosEmpenho.DataEmissao);
                    $('#txtDataELancamento').val(dadosEmpenho.DataLancamento);
                    $('#txtNaturezaDespesa').val(dadosEmpenho.Despesa);
                    $('#txtEmpenhoOriginal').val(dadosEmpenho.EmpenhoOriginal);
                    $('#txtEvento').val(dadosEmpenho.Evento);
                    $('#txtFonte').val(dadosEmpenho.Fonte);
                    $('#txtGestao').val(dadosEmpenho.Gestao);
                    $('#txtGestaoCredor').val(dadosEmpenho.GestaoCredor);
                    $('#txtObra').val(dadosEmpenho.IdentificadorObra);
                    $('#txtLicitacao').val(dadosEmpenho.Licitacao);
                    $('#txtLocalEntrega').val(dadosEmpenho.Local);
                    $('#txtModalidade').val(dadosEmpenho.Modalidade);
                    $('#txtContrato').val(dadosEmpenho.NumeroContrato);
                    $('#txtNumEmpenho').val(dadosEmpenho.NumeroNe);
                    $('#txtProcesso').val(dadosEmpenho.NumeroProcesso);
                    $('#txtOc').val(dadosEmpenho.Oc);
                    $('#txtOrigemMarterial').val(dadosEmpenho.OrigemMaterial);
                    $('#txtPlanoInterno').val(dadosEmpenho.PlanoInterno);
                    $('#txtPrograma').val(dadosEmpenho.Pt);
                    $('#txtPtres').val(dadosEmpenho.Ptres);
                    $('#txtRefLegal').val(dadosEmpenho.ReferenciaLegal);
                    $('#txtTipo').val(dadosEmpenho.TipoEmpenho);
                    $('#txtUgo').val(dadosEmpenho.Ugo);
                    $('#txtUnidadeGestora').val(dadosEmpenho.UnidadeGestora);
                    $('#txtUo').val(dadosEmpenho.Uo);
                    $('#txtValor').val(dadosEmpenho.Valor);

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

    };

    consulta.consultarCt = function (campoCt) {
        consulta.validateConnectionIsOpen();

        if ($(campoCt).val().length === 0 || $(campoCt).val().length < 11 || $(campoCt).val() === '') {
            return false;
        }

        var mydate = new Date();
        var year = mydate.getYear();
        var month = mydate.getMonth();
        if (year < 1000) {
            year += 1900;
        }

        numCt = $(campoCt).val();

        $.ajax({
            datatype: 'JSON',
            type: 'POST',
            url: "/ConsutasBase/ConsultarCt",
            data: JSON.stringify({ NumCt: numCt }),
            contentType: "application/json; charset=utf-8",
            beforeSend: function () {
                waitingDialog.show('Consultando');
            },
            success: function (dados) {
                if (dados.Status === 'Sucesso') {
                    carregarDadosCtSiafisico(dados.Ct);
                }
                else {
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
    };

    consulta.consultarEmpenhos = function () {
        consulta.validateConnectionIsOpen();

        var unidadeGestora = $('#CodigoUnidadeGestora').val();
        var gestao = $('#CodigoGestao').val();
        var codigoEmpenho = $('#CodigoEmpenhoOriginal').val();
        var data = $('#DataEmissao').val();
        var cnpjCpf = $('#NumeroCNPJCPFUGCredor').val();
        var gestaoCredor = $('#CodigoGestaoCredor').val();
        var natureza = '';
        var modalidade = $('#ModalidadeId').val();
        var licitacao = $('#LicitacaoId').val();
        var fonte = $('#CodigoFonteSiafisico').val() > 0 ? $('#CodigoFonteSiafisico :selected').text() : null;
        var processo = $('#NumeroProcessoNE').val();

        if (gestao === '') {
            AbrirModal('Favor informar Gestão!');
            return false;
        }
        else if (unidadeGestora === '') {
            AbrirModal('Favor informar o Unidade Gestora!');
            return false;
        }
        else if (codigoEmpenho === '') {
            AbrirModal('Favor informar o Nº do Empenho!');
            return false;
        }

        var dtoEmpenhos =
        {
            CgcCpf: cnpjCpf,
            Data: data,
            Fonte: fonte,
            GestaoCredor: gestaoCredor,
            Licitacao: licitacao,
            Modalidade: modalidade,
            Natureza: natureza,
            NumEmpenho: codigoEmpenho,
            Processo: processo,
            Gestao: gestao,
            UnidadeGestora: unidadeGestora
        };

        var descNatureza = $('#Natureza').is(':selected') ? $('#Natureza :selected').text() : 'Todos';
        var descModalidade = $('#ModalidadeId').is(':selected') ? $('#ModalidadeId :selected').text() : 'Todos';
        var descLicitacao = $('#LicitacaoId').is(':selected') ? $('#LicitacaoId :selected').text() : 'Todos';
        var descFonte = $('#CodigoFonteSiafisico').is(':selected') ? $('#CodigoFonteSiafisico :selected').text() : 'Todos';

        $.ajax({
            datatype: 'JSON',
            type: 'POST',
            url: "/ConsutasBase/ConsultarEmpenhos",
            data: JSON.stringify({ dtoEmpenhos: dtoEmpenhos }),
            contentType: 'application/json; charset=utf-8',
            beforeSend: function () {
                waitingDialog.show('Consultando');
            },
            success: function (dados) {
                if (dados.Status === 'Sucesso') {
                    $('#modalConsultaEmpenhos').modal('toggle');
                    $('#txtUnidadeGestora').val(unidadeGestora);
                    $('#txtGestao').val(gestao);
                    $('#txtLicitacao').val(descLicitacao);
                    $('#txtFonte').val(descFonte);
                    $('#txtModalidade').val(descModalidade);
                    $('#txtNatureza').val(descNatureza);

                    datatableAddRowForEmpenhos(dados.Empenhos);
                }
                else {
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

    consulta.consultarAnulacaoApoio = function () {
        consulta.validateConnectionIsOpen();

        var anulacao = {
            NumeroOriginalProdesp: $('#NumeroSubempenhoProdesp').val().replace("/", ""),
            Valor: $('#ValorAnular').val().replace("R$ 0,00", "").replace(/[.,]/g, "")
        }

        if (anulacao.NumeroOriginalProdesp.length === 0 || anulacao.Valor.length === 0) {
            AbrirModal('Os Campos Nº do Subempenho e o Valor a Anular devem ser preenchidos.');
            return false;
        }

        $.ajax({
            datatype: 'JSON',
            type: 'POST',
            url: '/ConsutasBase/ConsultarAnulacaoApoio',
            data: JSON.stringify({ anulacao: anulacao }),
            contentType: 'application/json; charset=utf-8',
            beforeSend: function () {
                waitingDialog.show('Consultando');
            },
            success: function (dados) {
                if (dados.Status === 'Sucesso') {
                    $('#DataRealizado').val(dados.SubempenhoCancelamento.outDataRealizacao);
                    $('#NumeroSubempenhoProdesp').val(dados.SubempenhoCancelamento.outNumeroAnulacao);
                    //  $('#').val(dados.SubempenhoCancelamento.outSaldoAnteriorAnul);
                    //  $('#').val(dados.SubempenhoCancelamento.outVencimento);
                    $('#ValorRealizado').val(dados.SubempenhoCancelamento.outValorRealizado);
                    if (dados.SubempenhoCancelamento.outInfoTransacao === 'SubEmpenhoContrato') {
                        $('#CenarioProdesp').val('SubEmpenhoCotrato');
                    }
                    if (dados.SubempenhoCancelamento.outInfoTransacao === 'SemContrato') {
                        $('#CenarioProdesp').val('SubEmpenho');
                    }

                    liquidacao.displayHandlerButton();
                }
                else {
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

    consulta.consultarEmpenhoSaldoRap = function () {
        consulta.validateConnectionIsOpen();

        var ano = $('#AnoExercicio').val();
        var orgao = $('#Orgao').val();

        var dtoEmpenhoSaldoRap =
        {
            NumeroAnoExercicio: ano,
            RegionalId: orgao,
        };

        $.ajax({
            datatype: 'JSON',
            type: 'POST',
            url: "/ConsutasBase/ConsultaEmpenhoSaldoRAP",
            data: JSON.stringify({ entity: dtoEmpenhoSaldoRap }),
            contentType: "application/json; charset=utf-8",
            beforeSend: function () {
                waitingDialog.show('Consultando');
            },
            success: function (dados) {
                if (dados.Status === 'Sucesso') {
                    carregarDadosRapSaldo(dados);
                }
                else {
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

    consulta.subempenhoInscricaoRap = function () {
        consulta.validateConnectionIsOpen();
        var subempenhoInscricaoRap = $('#NumeroSubempenho').val();
        var recibo = $('#NumeroRecibo').val();

        var dtoSubempenhoInscricaoRap =
        {
            NumeroSubempenho: subempenhoInscricaoRap,
            NumeroRecibo: recibo,
        };
                

        $.ajax({
            datatype: 'JSON',
            type: 'POST',
            url: "/ConsutasBase/ConsultarRapRequisicaoApoio",
            data: JSON.stringify({ entity: dtoSubempenhoInscricaoRap }),
            contentType: "application/json; charset=utf-8",
            beforeSend: function () {
                waitingDialog.show('Consultando');
            },
            success: function (dados) {
                if (dados.Status === 'Sucesso') {
                    consulta.carregarDadosInscritoRap(dados);
                }
                else {
                    AbrirModal(dados.Msg);
                    consulta.carregarDadosInscritoRap(); //excluir aqui devera ser no sucesso
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
    
    consulta.requisicaoAnulacaoRap = function ()
    {
        consulta.validateConnectionIsOpen();

        var requisicaoAnulacaoRap = $('#NumeroRequisicaoRap').val();
       
        var dtorequisicaoAnulacaoRap =
        {
            NumeroRequisicaoRap: requisicaoAnulacaoRap,
        };

        $.ajax({
            datatype: 'JSON',
            type: 'POST',
            url: "/ConsutasBase/ConsultaRequisicaoRAP",
            data: JSON.stringify({ entity: dtorequisicaoAnulacaoRap }),
            contentType: "application/json; charset=utf-8",
            beforeSend: function () {
                waitingDialog.show('Consultando');
            },
            success: function (dados) {
                if (dados.Status === 'Sucesso') {
                    consulta.carregarDadosAnulacaoRap(dados);
                }
                else {
                    
                    
                    AbrirModal(dados.Msg);
                    consulta.carregarDadosAnulacaoRap(); //excluir aqui . Deverá ser no sucesso.
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

    consulta.provider = function () {
        consulta.body
            .on('click', '#btnConsultarContratoTodos', consulta.resumoEventHandler)
            .on('click', '#btnConfirmarContratoTodos', consulta.detalheEventHandler);
    }


    $(document).on('ready', consulta.init);

})(window, document, jQuery);