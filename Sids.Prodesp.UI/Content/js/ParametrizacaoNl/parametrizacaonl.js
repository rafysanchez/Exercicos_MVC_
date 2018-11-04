var PARAMETRIZACAONL = (function () {
    function init() {
        bindAll();

        var form = $('#frmAtualizarParametrizacaoNl');
        form.validate();
    }

    function bindAll() {
        bindTipoNl();
        bindAdicionarDespesa();
        bindAdicionarEvento();
        bindExcluirLinhaDespesa();
        bindTipoDocumento();
        bindExcluirLinhaEvento();
        bindEditarEvento();
        bindAtualizar();
        bindCancelar();
    }

    //3.1	RDN1 – Consultar Parametrização
    //Para a consulta de parametrização é necessário escolher o campo “Tipo de NL”.
    //Ao selecionar um registro deve ser executada a consulta e preenchimento dos demais campos.
    function bindTipoNl() {
        $('#frmAtualizarParametrizacaoNl').on('change', '#IdTipoNL', function () {
            var tipo = $(this).val();

            limparCamposTela();

            if (tipo) {
                selecionarTipoNl(tipo);
            }
        });
    }

    function bindExcluirLinhaDespesa() {
        $('#frmAtualizarParametrizacaoNl').on('click', ' table#tblDespesas tr td .btn-excluir-linha', function () {
            var tr = $(this).closest('tr');
            deletarLinha(tr, function () {
                var table = document.getElementById("tblDespesas");
                reordenarTabela(table);
            });
        });
    }

    function bindExcluirLinhaEvento() {
        $('#frmAtualizarParametrizacaoNl').on('click', ' table#tblEventos tr td .btn-excluir-linha', function () {
            var tr = $(this).closest('tr');
            deletarLinha(tr, function () {
                var table = document.getElementById("tblEventos");
                reordenarTabela(table);
            });
        });
    }

    function bindEditarEvento() {
        $('#frmAtualizarParametrizacaoNl').on('click', ' table#tblEventos tr td .btn-editar-evento', function () {
            debugger
            editarLinhaEvento(this);
        });
    }

    function bindAdicionarDespesa() {
        $('#frmAtualizarParametrizacaoNl').on('click', '#btnAdicionarDespesa', function () {
            var tipo = $('#IdTipoDespesa option:selected');
            var tipoId = tipo.val();
            var tipoCodigo = tipo.text().split(" - ")[0];
            var textoCodigo = tipo.text().split(" - ")[1];

            if (tipo.val()) {
                adicionarTipoDespesa(true, 0, tipoId, tipoCodigo, textoCodigo);
            }
            else {
                AbrirModal("Selecione uma despesa para adicionar.");
            }
        });
    }

    //3.5	RDN5 – Visualização do campo “Tipo RAP”
    //O campo “Tipo RAP” é visível apenas para o “Tipo Documento” = 11- Requisição de RAP.
    function bindTipoDocumento() {
        $('#frmAtualizarParametrizacaoNl').on('change', '#ddlIdTipoDocumento', function () {
            var tipo = $(this).val();

            mostraOcultaTipoRap(tipo);
        });
    }

    function bindAdicionarEvento() {
        $('#frmAtualizarParametrizacaoNl').on('click', '#btnAdicionarEvento', function () {
            debugger
            var id = document.getElementById("hdnEventoId").value;
            var tipoDocumento = document.getElementById("ddlIdTipoDocumento").options[document.getElementById("ddlIdTipoDocumento").selectedIndex];
            var tipoDocumentoValue = tipoDocumento.value;
            var tipoDocumentoText = tipoDocumento.text;
            var tipoRAP = document.getElementById("ddlIdTipoRap").options[document.getElementById("ddlIdTipoRap").selectedIndex];

            var tipoRAPValue = tipoRAP.value;
            var tipoRAPText = tipoRAP.value.length > 0 ? tipoRAP.text : '';

            var evento = document.getElementById("txtNumeroEvento").value;
            var classificacao = document.getElementById("txtNumeroClassificacao").value;
            var fonte = document.getElementById("txtNumeroFonte").value;
            var entradaSaida = $('input[name=chkEntradaSaida]:checked').val() == undefined ? '' : $('input[name=chkEntradaSaida]:checked').val();

            adicionarEvento(id, tipoDocumentoValue, tipoDocumentoText, tipoRAPValue, tipoRAPText, evento, classificacao, fonte, entradaSaida, function () {
                limparCamposEvento();
            });
        });
    }

    //3.2	RDN2 – Campos Obrigatórios Apenas em Tela
    //Para salvar as informações, o campo “Tipo de NL” é obrigatório apenas na tela.
    function bindAtualizar() {
        $('#frmAtualizarParametrizacaoNl').on('click', '#btnAtualizar', function () {
            if (!ValidarAtualizacaoParametrizacaoNL()) return false;

            atualizarParametrizacaoNl(function () {
                //limparCamposTela(true);
            });
            return false;
        });
    }

    function bindCancelar() {
        //$('#frmAtualizarParametrizacaoNl').off('click', '#btnCancelar');
        //$('#frmAtualizarParametrizacaoNl').on('click', '#btnCancelar', function () {
        //    ModalSistema(true, '#modalConfirmaExclusao', 'cancelar', obj, 'meh');
        //});
    }

    function selecionarTipoNl(tipo) {
        var url = $('#frmAtualizarParametrizacaoNl #IdTipoNL').data('urlVerificacao');

        $.ajax({
            url: url,
            data: { idTipoNL: tipo },
            contentType: 'json',
            method: 'GET',
            success: function (response) {
                if (response.Sucesso) {
                    $('#frmAtualizarParametrizacaoNl #IdParametrizacao').val(response.Objeto.IdParametrizacao);
                    $('#frmAtualizarParametrizacaoNl #Observacao').val(response.Objeto.Observacao);

                    $('#frmAtualizarParametrizacaoNl input[type=radio][name=Transmitir]').prop('checked', false);
                    $('#frmAtualizarParametrizacaoNl input[type=radio][name=Transmitir][data-valor=' + response.Objeto.Transmitir + ']').prop('checked', true);

                    $('#frmAtualizarParametrizacaoNl #UnidadeGestora').val(response.Objeto.UnidadeGestora);
                    $('#frmAtualizarParametrizacaoNl #FavorecidaCnpjCpfUg').val(response.Objeto.FavorecidaCnpjCpfUg);
                    $('#frmAtualizarParametrizacaoNl #FavorecidaNumeroGestao').val(response.Objeto.FavorecidaNumeroGestao);
                    debugger
                    $('#frmAtualizarParametrizacaoNl #IdFormaGerarNl').val(response.Objeto.IdFormaGerarNl);

                    carregarDespesas(response.Objeto.Despesas);
                    carregarEventos(response.Objeto.Eventos);
                }
                else {
                    AbrirModal(response.Mensagem);
                }
            },
            error: function (xhr, err, status) {
                console.log(xhr);
                console.log(err);
                console.log(status);
                AbrirModal(xhr);
            }
        });
    }

    function carregarDespesas(despesasSelecionadas) {

        var despesas = parseDropdownDespesa($('#IdTipoDespesa'));

        for (var i = 0; i < despesasSelecionadas.length; i++) {
            var despesaAtual = despesasSelecionadas[i];
            var despesaSelecionada = selecionarDaColecao(despesas, despesaAtual.IdTipo);

            if (despesaSelecionada) {
                adicionarTipoDespesa(false, despesaAtual.Id, despesaAtual.IdTipo, despesaSelecionada.codigo, despesaSelecionada.texto);
            }
        }
    }

    function carregarEventos(eventosSelecionados) {

        var tiposDocumento = parseDropdown($('#ddlIdTipoDocumento'));
        var tiposRap = parseDropdown($('#ddlIdTipoRap'));

        for (var i = 0; i < eventosSelecionados.length; i++) {
            var eventoAtual = eventosSelecionados[i];

            var tipoDocumentoAtual = selecionarDaColecao(tiposDocumento, eventoAtual.IdTipoDocumento);
            var tipoRapAtual = selecionarDaColecao(tiposRap, eventoAtual.IdTipoRap);

            var rapId = tipoRapAtual !== undefined ? tipoRapAtual.id : '';
            var rapTexto = tipoRapAtual !== undefined ? tipoRapAtual.texto : '';
            debugger
            eventoAtual.EntradaSaidaDescricao = eventoAtual.EntradaSaidaDescricao == null ? '' : eventoAtual.EntradaSaidaDescricao;

            adicionarEvento(eventoAtual.Id, tipoDocumentoAtual.id, tipoDocumentoAtual.texto, rapId, rapTexto, eventoAtual.NumeroEvento, eventoAtual.NumeroClassificacao, eventoAtual.NumeroFonte, eventoAtual.EntradaSaidaDescricao);
        }
    }

    function adicionarTipoDespesa(validarNoBackend, id, tipoId, tipoCodigo, textoCodigo) {
        //RDN6 “Tipo de Despesa” deve ser utilizado em apenas um único “tipo de NL”.
        //Caso seja escolhido exibir a mensagem: “Tipo de Despesa utilizada no tipo de NL XXXX (nome do tipo de NL)”. OK

        //RDN7Na lista de “Tipo de Despesa” não deve conter tipos de despesa repetidos.
        //Caso seja escolhido um tipo de despesa mais de uma vez exibir a mensagem: “Tipo de Despesa já escolhido”.

        var table = document.getElementById("tblDespesas");
        var table_len = (table.rows.length) - 2;

        if (validarNoBackend) {
            verificarTipoDespesa(tipoId, tipoCodigo, function () {
                inserirLinhaDespesa(table, table_len, id, tipoId, tipoCodigo, textoCodigo);
            });
        }
        else {
            inserirLinhaDespesa(table, table_len, id, tipoId, tipoCodigo, textoCodigo);
        }
    }

    function inserirLinhaDespesa(table, table_len, id, tipoId, tipoCodigo, textoCodigo) {

        var htmlInputs = gerarInputsDespesa(table_len, id, tipoId);

        table.insertRow(table.rows.length).outerHTML = "<tr id='row_" + table_len + "'><td>" + htmlInputs + tipoCodigo + "</td><td id='descricaoTipoDespesa" + table_len + "'>" + textoCodigo + "</td><td></button>&nbsp;<button type='button' title='Deletar' class='btn btn-xs btn-danger btn-excluir-linha'><span class='glyphicon glyphicon-trash'></span></button></td></tr>";
        verificarPlaceholder(table);

        $('#IdTipoDespesa').val('');
    }

    //3.6	RDN6 – Utilização do “Tipo de Despesa” em um Único Tipo de NL
    //“Tipo de Despesa” deve ser utilizado em apenas um único “tipo de NL”.
    //Caso seja escolhido exibir a mensagem: “Tipo de Despesa utilizada no tipo de NL XXXX (nome do tipo de NL)”.

    //3.7	RDN7 – Utilização do “Tipo de Despesa” em um Único Registro
    //Na lista de “Tipo de Despesa” não deve conter tipos de despesa repetidos.
    //Caso seja escolhido um tipo de despesa mais de uma vez exibir a mensagem: “Tipo de Despesa já escolhido”.
    function verificarTipoDespesa(idTipo, codigo, callbackSucesso, callbackFalha) {
        var url = $('#btnAdicionarDespesa').data('urlVerificacao');
        var tipoNl = $('#IdTipoNL').val();

        if (tipoNl.length === 0) {
            AbrirModal("É preciso selecionar um um tipo de NL.");
        }
        else {
            var jaExiste = $('#tblDespesas tbody tr input.dados-item').filter('[value=' + idTipo + ']').length > 0;

            if (jaExiste) {
                AbrirModal("Tipo de Despesa já escolhido.");
            }
            else {
                $.ajax({
                    url: url,
                    data: { tiponl: tipoNl, codigo: codigo },
                    contentType: 'json',
                    method: 'GET',
                    success: function (response) {
                        if (response.Sucesso) {
                            if (callbackSucesso) {
                                callbackSucesso();
                            }
                        }
                        else {
                            AbrirModal(response.Mensagem);
                            if (callbackFalha) {
                                callbackFalha();
                            }
                        }
                    },
                    error: function (xhr, err, status) {
                        console.log(xhr);
                        console.log(err);
                        console.log(status);
                        AbrirModal(xhr);
                    }
                });
            }
        }
    }

    function gerarInputsDespesa(arrayPos, id, tipoId) {
        var inputId = "<input type='hidden' class='dados-item' name='Despesas[" + arrayPos + "].Id' value='" + id + "'>";
        var inputTipoId = "<input type='hidden' class='dados-item' name='Despesas[" + arrayPos + "].IdTipo' value='" + tipoId + "'>";

        var htmlInputs = inputId + inputTipoId;

        return htmlInputs;
    }

    function adicionarEvento(id, tipoDocumentoValue, tipoDocumentoText, tipoRAPValue, tipoRAPText, evento, classificacao, fonte, entradaSaida, callback) {
        var table = document.getElementById("tblEventos");
        var table_len = (table.rows.length) - 2;
        var trExistente = $(table).find('tr').filter("[data-id= " + id + "]");
        debugger

        if ((id > 0 || $('.Editar-Item').length > 0) && trExistente.length > 0) {

            if ($('.Editar-Item').length > 0) {
                trExistente = $(table).find('tr').filter('.Editar-Item');
            }
            var trExistenteTipoDocumento = trExistente.find('input:hidden[name$=IdTipoDocumento]').val();
            var trExistenteTipoRap = trExistente.find('input:hidden[name$=IdTipoRap]').val();
            var trExistenteNumeroEvento = trExistente.find('input:hidden[name$=NumeroEvento]').val();
            var trExistenteNumeroClassificacao = trExistente.find('input:hidden[name$=NumeroClassificacao]').val();
            var trExistenteNumeroFonte = trExistente.find('input:hidden[name$=NumeroFonte]').val();

            var mesmoTipoDocumento = trExistenteTipoDocumento.toString() === tipoDocumentoValue.toString();
            var mesmoTipoRap = trExistenteTipoRap.toString() === tipoRAPValue.toString();
            var mesmoNumeroEvento = trExistenteNumeroEvento.toString() === evento.toString();
            var mesmoNumeroClassificacao = trExistenteNumeroClassificacao.toString() === classificacao.toString();
            var mesmoNumeroFonte = trExistenteNumeroFonte.toString() === fonte.toString();


            $(".btn-editar-evento").removeClass("Editar-Item");

            trExistente.remove();
        }
        debugger

        var validacao = validarPreenchimentoEvento(id, tipoDocumentoValue, tipoRAPValue, evento, classificacao, fonte, entradaSaida);

        if (validacao === true) {
            var htmlInputs = gerarInputsEvento(table_len, id, tipoDocumentoValue, tipoRAPValue, evento, classificacao, fonte, entradaSaida);

            table.insertRow(table.rows.length).outerHTML = "<tr id='row_" + table_len + "' data-id=" + id + " ><td style='text-align:center;'>" + htmlInputs + tipoDocumentoText + "</td><td style='text-align:center;'>" + tipoRAPText + "</td><td style='text-align:center;'>" + evento + "</td><td style='text-align:center;'>" + classificacao + "</td><td style='text-align:center;'>" + fonte + "</td><td style='text-align:center;'>" + entradaSaida + "</td><td><button type='button' title='Editar' class='btn btn-xs btn-info btn-editar-evento'><span class='glyphicon glyphicon-edit'></span></button>&nbsp;<button type='button' title='Deletar' class='btn btn-xs btn-danger btn-excluir-linha'><span class='glyphicon glyphicon-trash'></span></button><br/></td></tr>";

            verificarPlaceholder(table);
            reordenarTabela(table);

            if (callback) {
                callback();
            }
        }
        else {
            AbrirModal(validacao);
        }
    }

    //3.3	RDN3 – Adicionar na Lista Com Campos Preenchidos
    //Para adicionar as informações na lista ao menos um dos campos abaixo deve estar preenchido:
    //•	Tipo Documento;
    //•	Evento;
    //•	Classificação;
    //•	Fonte;
    //•	Tipo RAP
    //•	Entrada;
    //•	Saída;

    //3.8	RDN8 – “Tipo de RAP” Obrigatório
    //O “Tipo de RAP” é obrigatório quando em “Tipo de Documento” for escolhido o tipo “11- Requisição de RAP”.
    //Caso o “Tipo de RAP” não for escolhido exibir a mensagem: “Tipo de RAP deve ser escolhido”.
    function validarPreenchimentoEvento(id, tipoDocumentoValue, tipoRAPValue, evento, classificacao, fonte, entradaSaida) {
        var tipoDoc = parseInt(tipoDocumentoValue);
        var rapValido = tipoDoc !== 11 || (tipoDoc === 11 && tipoDocumentoValue.length > 0 && tipoRAPValue.length > 0);

        //if (rapValido && tipoDocumentoValue.length > 0 && evento > 0 && classificacao && classificacao !== 0 && fonte && fonte.length !== 0 && entradaSaida && entradaSaida.length !== 0) {
        //    return true;
        //}

        if (rapValido && tipoDocumentoValue.length > 0) {
            return true;
        }

        if (!rapValido) {
            return "Tipo de RAP deve ser escolhido.";
        }

        return 'Os dados do evento devem ser prenchidos corretamente.';
    }

    function gerarInputsEvento(arrayPos, id, tipoDocumentoValue, tipoRAPValue, evento, classificacao, fonte, entradaSaida) {
        var inputId = "<input type='hidden' class='dados-item' name='Eventos[" + arrayPos + "].Id' value='" + id + "'>";
        var inputTipoDocumento = "<input type='hidden' class='dados-item' name='Eventos[" + arrayPos + "].IdTipoDocumento' value='" + tipoDocumentoValue+ "'>";
        var inputTipoRap = "<input type='hidden' class='dados-item' name='Eventos[" + arrayPos + "].IdTipoRap' value='" + tipoRAPValue + "'>";
        var inputNumeroEvento = "<input type='hidden' class='dados-item' name='Eventos[" + arrayPos + "].NumeroEvento' value='" + evento + "'>";
        var inputNumeroClassificacao = "<input type='hidden' class='dados-item' name='Eventos[" + arrayPos + "].NumeroClassificacao' value='" + classificacao + "'>";
        var inputNumeroFonte = "<input type='hidden' class='dados-item' class='dados-item' name='Eventos[" + arrayPos + "].NumeroFonte' value='" + fonte + "'>";
            var inputEntradaSaida = "<input type='hidden' class='dados-item' name='Eventos[" + arrayPos + "].EntradaSaidaDescricao' value='" + entradaSaida + "'>";

            var htmlInputs = inputId +  inputTipoDocumento + inputTipoRap + inputNumeroEvento + inputNumeroClassificacao + inputNumeroFonte + inputEntradaSaida;

        return htmlInputs;
    }

    function editarLinhaEvento(btn) {

        $("#tblEventos tr").removeClass("Editar-Item");

        $(btn).closest('tr').addClass('Editar-Item');

        var tr = $(btn).closest('tr');
        var inputs = tr.find('td input[type=hidden].dados-item');
        debugger
        //RECEBER VALORES
        var id = inputs.filter('[name$=Id]').val();
        var tipoDocumento = inputs.filter('[name$=IdTipoDocumento]').val();
        var tipoRAP = inputs.filter('[name$=IdTipoRap]').val();
        var numeroEvento = inputs.filter('[name$=NumeroEvento]').val();
        var numeroClassificacao = inputs.filter('[name$=NumeroClassificacao]').val();
        var numeroFonte = inputs.filter('[name$=NumeroFonte]').val();
        var entradaSaidaDescricao = inputs.filter('[name$=EntradaSaidaDescricao]').val();

        //SETAR VALORES
        document.getElementById("hdnEventoId").value = id;
        document.getElementById("ddlIdTipoDocumento").value = tipoDocumento;


        mostraOcultaTipoRap(tipoDocumento);

        document.getElementById("ddlIdTipoRap").value = tipoRAP;

        document.getElementById("txtNumeroEvento").value = numeroEvento;
        document.getElementById("txtNumeroClassificacao").value = numeroClassificacao;
        document.getElementById("txtNumeroFonte").value = numeroFonte;

        $('[name=chkEntradaSaida]').prop('checked', false);
        $('[name=chkEntradaSaida][value=' + entradaSaidaDescricao + ']').prop('checked', true);
    }

    function mostraOcultaTipoRap(tipoDocumento) {
        if (parseInt(tipoDocumento) === 11) {
            $('#ddlIdTipoRap').parent('div').removeClass('hidden');
        }
        else {
            if (!$('#ddlIdTipoRap').hasClass('hidden')) {
                $('#ddlIdTipoRap').parent('div').addClass('hidden');
                $('#ddlIdTipoRap').val('');
            }
        }
    }

    function limparCamposEvento() {
        document.getElementById("hdnEventoId").value = "0";
        document.getElementById("ddlIdTipoDocumento").value = "";
        document.getElementById("ddlIdTipoRap").value = "";
        document.getElementById("txtNumeroEvento").value = "";
        document.getElementById("txtNumeroClassificacao").value = "";
        document.getElementById("txtNumeroFonte").value = "";
        $('input[name=chkEntradaSaida]:checked').prop('checked', false);
        document.getElementById("btnAtualizar").value = "";
        
    }

    //3.4	RDN4 – Botão Atualizar
    //Ao acionar o botão “Atualizar”:
    //•	devem ser salvos os registros marcados como incluídos;
    //•	devem ser salvas as alterações dos registros marcados como alterados;
    //•	devem ser excluídos os registros marcados como excluídos;
    function atualizarParametrizacaoNl(callbackSucesso, callbackFalha) {
        var form = $('#frmAtualizarParametrizacaoNl');
        var url = form.attr('action');
        var method = form.attr('method');
        var data = form.serializeObject();
        $.ajax({
            url: url,
            data: data,
            method: method,
            success: function (response) {
                if (response.Sucesso) {
                    limparCamposEvento();

                    $.confirm({
                        text: "Atualizado com Sucesso!",
                        title: "Parametrização de NL",
                        confirm: function (button) {
                        },
                        confirmButton: "Fechar",
                        cancelButton: false,
                        post: true,
                        dialogClass: "modal-dialog modal-sm" // Bootstrap classes for large modal
                    });


                    if (callbackSucesso) {
                        callbackSucesso();
                    }
                }
                else {
                    AbrirModal(response.Mensagem);
                    if (callbackFalha) {
                        callbackFalha();
                    }
                }
            },
            error: function (xhr, err, status) {
                console.log(xhr);
                console.log(err);
                console.log(status);
                AbrirModal(xhr);
            }
        });

    }

    function deletarLinha(tr, callback) {
        var tabela = $(tr).closest('table');
        $(tr).remove();

        verificarPlaceholder(tabela);

        if (callback) {
            callback();
        }
    }

    function verificarPlaceholder(table) {
        var tabela = $(table);
        var placeholder = tabela.find('tbody tr.tr-placeholder');
        var outras = tabela.find('tbody tr').not('.tr-placeholder');

        if (outras.length === 0) {
            placeholder.show();
        }
        else {
            placeholder.hide();
        }
    }

    function limparTabela(table) {
        var tabela = $(table);
        var placeholder = tabela.find('tbody tr.tr-placeholder');
        var outras = tabela.find('tbody tr').not('.tr-placeholder');

        outras.each(function (index, element) {
            $(this).remove();
        });

        placeholder.show();
    }

    function reordenarTabela(table) {
        var count = 0;
        $(table).find('tbody tr:not(.tr-placeholder)').each(function (index, element) {
            var tr = $(this);
            var pattern = /[(\d+)]/i;
            var hiddens = tr.find('input[type=hidden].dados-item');

            hiddens.each(function (i, e) {
                var hdn = $(this);
                var name = hdn.prop('name');
                name = name.replace(pattern, count);
                hdn.prop('name', name);
            });

            count++;
        });
    }

    function limparCamposTela(resetar) {
        $('#IdTipoDespesa').val('');

        var tabelaDespesas = document.getElementById("tblDespesas");
        limparTabela(tabelaDespesas);
        var tabelaEventos = document.getElementById("tblEventos");
        limparTabela(tabelaEventos);

        if (resetar) {
            $('#frmAtualizarParametrizacaoNl #IdTipoNL').val('');
        }

        $('#frmAtualizarParametrizacaoNl #IdParametrizacao').val('');
        $('#frmAtualizarParametrizacaoNl #Observacao').val('');
        $('#frmAtualizarParametrizacaoNl input[type=radio][name=Transmitir]').prop('checked', false);
        $('#frmAtualizarParametrizacaoNl #UnidadeGestora').val('');
        $('#frmAtualizarParametrizacaoNl #FavorecidaCnpjCpfUg').val('');
        $('#frmAtualizarParametrizacaoNl #FavorecidaNumeroGestao').val('');
        $('#frmAtualizarParametrizacaoNl #IdFormaGerarNl').val('');
        
    }

    function parseDropdownDespesa(ddl) {
        var options = $(ddl).find('option');
        var colecao = [];
        options.each(function (index, element) {
            var textoInteiro = element.text.split(" - ");
            var codigo = textoInteiro[0];
            var texto = textoInteiro[1];

            if (element.value.length > 0) {
                colecao.push({ id: element.value, codigo: codigo, texto: texto });
            }
        });

        return colecao;
    }

    function parseDropdown(ddl) {
        var options = $(ddl).find('option');
        var colecao = [];
        options.each(function (index, element) {
            if (element.value.length > 0) {
                colecao.push({ id: element.value, texto: element.text });
            }
        });

        return colecao;
    }

    function selecionarDaColecao(colecao, idSelecionado) {
        for (var i = 0; i < colecao.length; i++) {
            var obj = colecao[i];
            var id = parseInt(obj.id);

            if (isNaN(id)) {
                id = obj.id;
            }

            if (id === idSelecionado) {
                return obj;
            }
        }
    }

    return {
        Init: init
    }
}());

$(function () {
    PARAMETRIZACAONL.Init();
});


//$(document).ready(function () {
//    //$("#atualizar").click(function () {
//    //    $('#frmAtualizarParametrizacaoNl').validate();
//    //    //var cont = 0;
//    //    //$("select[name=opcoesTipoNL]").each(function () {
//    //    //    if ($(this).val() == "" && $(this).prop('required')) {
//    //    //        $(this).css({ "border": "1px solid #F00", "padding": "2px" });
//    //    //        cont++;
//    //    //    }
//    //    //});
//    //    //$("select[name=opcoesTipoRap]").each(function () {
//    //    //    if ($(this).val() == "" && $(this).prop('required')) {
//    //    //        $(this).css({ "border": "1px solid #F00", "padding": "2px" });
//    //    //        cont++;
//    //    //    }
//    //    //});
//    //    //$("input[name=transmitir]").each(function () {
//    //    //    if ($(this).prop('required')) {
//    //    //        $('.requiredLabel').css({ "color": "#F00", "padding": "2px" });
//    //    //        cont++;
//    //    //    }
//    //    //});
//    //    //if (cont == 1) {
//    //    //    setTimeout(MostraModal('#modalAtualizarParametrizacao'), 2000);
//    //    //}
//    //});
//});


var ValidarAtualizacaoParametrizacaoNL = function () {

    $.validator.setDefaults({ ignore: [] });

    var $form = $("form#frmAtualizarParametrizacaoNl");
    //$form.validate();

    $("form#frmAtualizarParametrizacaoNl").removeData("validator").removeData("unobtrusiveValidation");
    $.validator.unobtrusive.parse("form#frmAtualizarParametrizacaoNl");

    $("select#IdTipoNL").rules("add", { required: true, messages: { required: "Campo Obrigatório" } });
    //$("#Transmitir").rules("add", { required: true, messages: { required: "Campo Obrigatório" } });
    $("#FavorecidaNumeroGestao").rules("add", { required: false });
    $("#Observacao").rules("add", { required: false });

    $("select#IdFormaGerarNl").rules("add", { required: true, messages: { required: "Campo Obrigatório" } });
    $("#IdParametrizacao").rules("add", { required: false });
    
    

    $form.validate();

    return $form.valid();
};