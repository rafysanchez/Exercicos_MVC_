var AUTORIZACAO_OB = (function () {
    var intervalSalvar = false;
    var intervalTransmitir = false;
    var intervalRetransmitir = false;

    function init() {
        Iniciar();
        bindAll();
        CustomValidators();

        if (findGetParameter('tipo') == 'c') {
            BloqueiaCampos('form_autorizacao_ob');
            $('#form_autorizacao_ob h2').html('Visualizar Autorização de OB');
            $('#btnCancelar').hide();
            //$('#btnSalvar').hide();
            $('#btnSalvarOB').hide();
            $('#btnTransmitirOB').hide();
            $('#btnConsultarOB').hide();
            $('#btnVoltar').show();
        }

        if (findGetParameter('tipo') == 'a') {
            $('#btnConsultarOB').hide();
        }

        //BuildDataTables.ById('._tbDataTables', { "pageLength": -1 }, true);
    }

    function initGrid() {
        Grid.Bind();
        BuildDataTables.ById('#tblGrid', { "pageLength": -1 }, true);
        //BuildDataTables.ById('#tblGrid', null, true);
    }

    function bindAll() {

        BindAutorizacaoOB();
        //BindConfirmacao();
        BindConsultarOB();
        //BindAdicionarPD();
        BindGridTodos();
        //BindCancelar();
        BindTransmitir();
        BindSalvar();
    };

    function Iniciar() {
        $('div.ListaOB').hide();
        //$('div.AdicionarPD').hide();
    };

    var BindAutorizacaoOB = function () {
        $('span.field-validation-error span').html('');
        $('div.ListaOB').show();
        $('div.ListaOB input').removeAttr('disabled');
    };

    function BindConfirmacao() {
        //confirmacao
        $('form#form_autorizacao_ob select[name="opcoesTipoPagamento"]').attr('disabled', 'disabled');
        $('form#form_autorizacao_ob input[name="dataPreparacao"]').attr('disabled', 'disabled');

        $('form#form_autorizacao_ob select[name="confirmacao"]').off('change');
        $('form#form_autorizacao_ob select[name="confirmacao"]').on('change', function () {

            $('form#form_autorizacao_ob select[name="opcoesTipoPagamento"]').val('');
            $('form#form_autorizacao_ob input[name="dataPreparacao"]').val('');

            if ($(this).val() === "S") {
                $('form#form_autorizacao_ob select[name="opcoesTipoPagamento"]').removeAttr('disabled');
                $('form#form_autorizacao_ob input[name="dataPreparacao"]').removeAttr('disabled');
            } else {
                $('form#form_autorizacao_ob select[name="opcoesTipoPagamento"]').attr('disabled', 'disabled');
                $('form#form_autorizacao_ob input[name="dataPreparacao"]').attr('disabled', 'disabled');
            }
        });
    };

    function BindConsultarOB() {
        $('#btnConsultarOB').off('click');
        $('#btnConsultarOB').on('click', function () {

            if (!ValidarListaOB()) return false;

            $.ajax({
                type: "POST",
                url: "/PagamentoContaUnica/AutorizacaoDeOB/ConsultarListaOB",
                data: $('form#form_autorizacao_ob').serialize(),// serializes the form's elements.
                beforeSend: function (hrx) {
                    waitingDialog.show('Consultando Siafem...');
                    //ShowLoading();
                }
            }).done(function (xhrResponse) {
                FillGrid(xhrResponse);
                BindGridTodos();

                BuildDataTables.ById('#tblGrid', { "pageLength": -1 });
                adicionarFiltroAgrupamento();

            }).fail(function (jqXHR, textStatus) {
                waitingDialog.hide();
                FillGrid('');
                AbrirModal(jqXHR.statusText, function () { });
            }).always(function () {
                waitingDialog.hide();
                //HideLoading();
            });

            return false;
        });
    };

    function FillGrid(html) {
        $('div#grid').html(html);
    };

    function BindGridTodos() {

        $("table#tblGrid thead th input[name='Todos']").off('change');
        $("table#tblGrid thead th input[name='Todos']").on('change', function () {

            $('input[type="checkbox"][name*="TransmitirCheckBox"]').each(function () {
                //alert('xxx ' + $(this).is(':disabled'))

                if ($('input[type="checkbox"][name*="Todos"]').is(':checked')) {
                    if (!$(this).is(':disabled')) {
                        $(this).prop('checked', true);
                    }
                } else {
                    if (!$(this).is(':disabled')) {
                        $(this).prop('checked', false);
                    }
                }

            });

        });

        $('input[type="checkbox"][name*="TransmitirCheckBox"]').off('change');
        $('input[type="checkbox"][name*="TransmitirCheckBox"]').on('change', function () {
            $("table#tblGrid thead th input[name='Todos']").prop('checked', false);
        });

        //$("table#tblGrid tbody tr td button[data-button='Visualizar']").off('click');
        //$("table#tblGrid tbody tr td button[data-button='Visualizar']").on('click', function () {

        //    var linha = $(this).parent().parent();
        //    var obj = ItemPorLinha(linha);

        //    $.ajax({
        //        type: "POST",
        //        url: "/PagamentoContaUnica/AutorizacaoDeOB/OBPorNumero",
        //        data: { "filtroAdicionarPd.NumeroPD": obj.NumPD, "GestaoLiquidante": obj.GestaoLiquidante, "UGLiquidante": obj.UGLiquidante },
        //        content: 'application/x-www-form-urlencoded; charset=UTF-8',
        //        beforeSend: function (hrx) {
        //            waitingDialog.show('Visualizar detalhes');
        //        }
        //    }).done(function (xhrResponse) {

        //        for (var property in xhrResponse.data) {
        //            if (xhrResponse.data.hasOwnProperty(property)) {
        //                $('#_modalVisualizarDetalhesPD input[data-id="' + property + '"]').val(xhrResponse.data[property]);
        //                $('#_modalVisualizarDetalhesPD td[data-id="' + property + '"]').html(xhrResponse.data[property]);
        //                $('#_modalVisualizarDetalhesPD [data-id="' + property + '"]').prop('title', xhrResponse.data[property]);
        //            }
        //        }

        //        if (xhrResponse.data.Fonte2 == null || xhrResponse.data.Fonte2 == "")
        //            $('#_modalVisualizarDetalhesPD [data-id="Fonte2"]').parent().remove();


        //        if (xhrResponse.data.Fonte3 == null || xhrResponse.data.Fonte3 == "")
        //            $('#_modalVisualizarDetalhesPD [data-id="Fonte3"]').parent().remove();

        //        $('#_modalVisualizarDetalhesPD').modal('show');

        //    }).fail(function (jqXHR, textStatus) {
        //        AbrirModal(jqXHR.statusText, function () { });
        //    }).always(function () {
        //        waitingDialog.hide();
        //    });

        //});

        $('[data-toggle="popover"]').popover();
    };

    function BindSalvar() {

        if (intervalSalvar) {
            clearInterval(intervalSalvar);
            intervalSalvar = false;
        }

        intervalSalvar = setInterval(function () {
            if ($('table#tblGrid tbody tr td input[type="checkbox"]:checked').length == 0) {
                $('button[data-button="SalvarOB"]').attr('disabled', 'disabled');
            } else {
                $('button[data-button="SalvarOB"]').removeAttr('disabled');
            }
        }, 50);

        $('button[data-button="SalvarOB"]:not(.disabled)').off('click');
        $('button[data-button="SalvarOB"]:not(.disabled)').on('click', function () {
            var table = util.getDataTable('table#tblGrid', 'div#grid');
            var data = table.rows().data();
            var allInputs = [];
            var formInputs = $('form#form_autorizacao_ob :input:not(.hiddenrow)').serializeArray()

            for (var i = 0; i < formInputs.length; i++) {
                allInputs.push(formInputs[i]);
            }

            for (var i = 0; i < data.length; i++) {
                for (var j = 0; j < data[i].length; j++) {
                    var xpto = $(data[i][j]);
                    xpto.each(function (index, element) {
                        var that = $(this);
                        var name = that.prop('name');
                        var valor = that.val();

                        var isRadio = that.is(":radio") === true;
                        var isCheckbox = that.is(":checkbox") === true;

                        if (isRadio) {
                            if ($('[name="' + name + '"]:checked').val() !== undefined && that.prop('checked') === true) {
                                valor = $('[name="' + name + '"]:checked').val().toString();
                            }
                        }
                        if (isCheckbox) {
                            valor = $('[name="' + name + '"]').is(':checked').toString();
                        }
                        valor = valor.charAt(0).toUpperCase() + valor.substr(1).toLowerCase();

                        if (name !== undefined && name.length > 0) {
                            allInputs.push({ name: name, value: valor });
                        }
                    });
                }
            }

            if (!ValidarAdicionarOB()) return false;

            $.ajax({
                type: "POST",
                url: "/PagamentoContaUnica/AutorizacaoDeOB/Save",
                data: allInputs,
                content: 'application/x-www-form-urlencoded; charset=UTF-8',
                beforeSend: function (hrx) {
                    waitingDialog.show('Salvando');
                }
            }).done(function (jqXHR) {

                $.confirm({
                    text: "Autorização de OB salva com sucesso!",
                    title: "Confirmação",
                    cancel: function () {
                        window.location = "/PagamentoContaUnica/AutorizacaoDeOB/Edit/" + jqXHR.IdAutorizacaoOB;
                    },
                    cancelButton: "Fechar",
                    confirmButton: false
                });

            }).fail(function (jqXHR, textStatus) {
                AbrirModal(jqXHR.statusText, function () { });
            }).always(function () {
                waitingDialog.hide();
            });
            return false;
        });
    }


    var BindTransmitir = function () {

        if (intervalTransmitir) {
            clearInterval(intervalTransmitir);
            intervalTransmitir = false;
        }

        intervalTransmitir = setInterval(function () {
            if ($('table#tblGrid tbody tr td input[type="checkbox"]:checked').length == 0) {
                $('button[data-button="TransmitirOB"]').attr('disabled', 'disabled');
            } else {
                $('button[data-button="TransmitirOB"]').removeAttr('disabled');
            }
        }, 50);

        $('button[data-button="TransmitirOB"]:not(.disabled)').off('click');
        $('button[data-button="TransmitirOB"]:not(.disabled)').on('click', function () {

            if (!ValidarAutorizarOB()) return false;

            if ($("select#EhConfirmacaoPagamento").val() === "1") {
                if (!ValidarTransmitirConfirmacao()) return false;
            }

            // reordenação de campos antes de transmitir
            var novoIndice = 0;
            $('table#tblGrid tbody tr').each(function (index, element) {
                var inputs = $(this).find(':input');
                inputs.each(function (index2, element2) {
                    var inputName = $(this).prop('name');
                    var novoNome = inputName.replace(/Items\[(\d+)\](.+)/, 'Items[' + novoIndice + ']$2');
                    $(this).prop('name', novoNome);
                });
                novoIndice++;
            });

            var $form = $("form#form_autorizacao_ob");

            $.ajax({
                type: "POST",
                url: "/PagamentoContaUnica/AutorizacaoDeOB/Transmitir",
                data: $form.serialize(),
                content: 'application/x-www-form-urlencoded; charset=UTF-8',
                beforeSend: function (hrx) {
                    waitingDialog.show('Transmitindo');
                }
            }).done(function (xhrResponse) {

                FillGrid(xhrResponse.grid);
                BindGridTodos();

            }).fail(function (jqXHR, textStatus) {
                AbrirModal(jqXHR.statusText, function () { });
            }).always(function () {
                waitingDialog.hide();
            });

            return false;
        });
    };

    function LimparListaItems() {
        $.ajax({
            type: "POST",
            url: "/PagamentoContaUnica/AutorizacaoDeOB/LimparListaItems",
            data: $('form#form_autorizacao_ob').serialize(),// serializes the form's elements.
            content: 'application/x-www-form-urlencoded; charset=UTF-8',
            beforeSend: function (hrx) {
                //waitingDialog.show('Consultando Siafem...');
            }
        }).done(function (xhrResponse) {

            FillGrid(xhrResponse.grid);
            BindGridTodos();

        }).fail(function (jqXHR, textStatus) {
            AbrirModal(jqXHR.statusText, function () { });
        }).always(function () {
            waitingDialog.hide();
        });
    };

    function ValidarListaOB() {
        var $form = $("form#form_autorizacao_ob");
        $("form#form_autorizacao_ob").removeData("validator").removeData("unobtrusiveValidation");
        $.validator.unobtrusive.parse("form#form_autorizacao_ob");

        $("input#UGPagadora").rules("add", { required: true, messages: { required: "Campo Obrigatório" } });
        $("input#GestaoPagadora").rules("add", { required: true, messages: { required: "Campo Obrigatório" } });

        $form.validate();

        return $form.valid();
    };

    function ValidarAdicionarOB() {

        var $form = $("form#form_autorizacao_ob");
        $("form#form_autorizacao_ob").removeData("validator").removeData("unobtrusiveValidation");
        $.validator.unobtrusive.parse("form#form_autorizacao_ob");

        $("input#UGPagadora").rules("add", { required: true, messages: { required: "Campo Obrigatório" } });
        $("input#GestaoPagadora").rules("add", { required: true, messages: { required: "Campo Obrigatório" } });

        $form.validate({ ignore: [] });

        return $form.valid();

    };

    function ValidarAutorizarOB() {

        $.validator.setDefaults({ ignore: [] });

        var $form = $("form#form_autorizacao_ob");
        $form.validate();

        $("form#form_autorizacao_ob").removeData("validator").removeData("unobtrusiveValidation");
        $.validator.unobtrusive.parse("form#form_autorizacao_ob");

        var items = [];

        var itemsSelecionados = $('table#tblGrid tbody tr td input[type="checkbox"]:checked');
        for (var i = 0; i < itemsSelecionados.length; i++) {
            var linha = $(itemsSelecionados[i]).parent().parent();
            Grid.RegistroSelecionado = Grid.ItemPorLinha(linha);
            items.push(Grid.RegistroSelecionado.Codigo);
        }

        if (itemsSelecionados.length === 0) { return false }

        return $form.valid();
    };

    var ValidarTransmitirConfirmacao = function () {

        //$.validator.setDefaults({ ignore: [] });

        var $form = $("form#form_autorizacao_ob");
        //$form.validate();

        $("form#form_autorizacao_ob").removeData("validator").removeData("unobtrusiveValidation");
        $.validator.unobtrusive.parse("form#form_autorizacao_ob");

        $("select#TipoPagamento").rules("add", { required: true, messages: { required: "Campo Obrigatório" } });
        $("input#Dt_Confirmacao").rules("add", { required: true, messages: { required: "Campo Obrigatório" } });
        //$('form#form_autorizacao_ob input[name="confirmacaoPagamento.Dt_confirmacao"]').rules("add", { required: true, messages: { required: "Campo Obrigatório" } });

        $form.validate({ ignore: [] });

        return $form.valid();
    };

    var ValidarImpressaoOB = function () {

        $.validator.setDefaults({
            ignore: []
        });

        var $form = "form#frm-imprimir-ob";
        $($form).validate();

        $($form).removeData("validator").removeData("unobtrusiveValidation");
        $.validator.unobtrusive.parse($form);

        $($form).find("input[name='tipo']").rules("add", { required: true, messages: { required: "Selecione uma opção para efetuar a impressão." } });

        return $($form).valid();
    };

    var ValidarExcluirAutorizacaoOB = function () {
        $.validator.setDefaults({
            ignore: []
        });

        var $form = "form#frm-excluir-autorizacao-ob";
        $($form).validate();

        $($form).removeData("validator").removeData("unobtrusiveValidation");
        $.validator.unobtrusive.parse($form);

        return $($form).valid();
    };

    var CodigoMudapah = function () {
        return $('#UGPagadora'); //$('#ddlMudapah');
    };

    /****************************************************/
    /********************** Grid ************************/
    /****************************************************/
    var Grid = {};
    Grid.RegistroSelecionado = {};
    Grid.ItemPorLinha = function (linha) {
        return {
            Codigo: linha.find('input[name*="AgrupamentoOB"]').val(),
            Agrupamento: linha.find('input[name*="AgrupamentoOB"]').val(),
            NumPD: linha.find('input[name*="NumPD"]').val(),
            NumOB: linha.find('input[name*="NumOB"]').val(),
            UGPagadora: linha.find('input[name*="UGPagadora"]').val(),
            GestaoPagadora: linha.find('input[name*="GestaoPagadora"]').val(),
            UnidadeGestora: linha.find('input[name*="UnidadeGestora"]').val(),
            Gestao: linha.find('input[name*="Gestao"]').val()
        };
    };

    Grid.Bind = function () {
        //Grid.BindTransmitir();
        Grid.BindRetransmitir();
        Grid.BindImprimir();
        Grid.BindTransmitirCheckbox();
        //Grid.BindCancelar();
        Grid.Datatables.BindEvents();
        Grid.BindExcluir();
        Grid.BindTransmitirSelecionarTodos();

        $('[data-toggle="popover"]').popover();
        $('[data-toggle="tooltip"]').tooltip();

        adicionarFiltroAgrupamento();
    };

    Grid.BindTransmitirCheckbox = function () {
        $('input[type="checkbox"][name*=".transmitir"]').off('change');
        $('input[type="checkbox"][name*=".transmitir"]').on('change', function () {

            var linha = $(this).parent().parent();

            if (this.checked) {
                $(linha).addClass('success');
            } else {
                $(linha).removeClass('success');
            }

        });
    };

    Grid.BindImprimir = function () {
        $('#frmExport').off('click', 'button[data-button="Imprimir1"]:not(.disabled)');
        $('#frmExport').on('click', 'button[data-button="Imprimir1"]:not(.disabled)', function () {

            var linha = $(this).parent().parent();
            Grid.RegistroSelecionado = Grid.ItemPorLinha(linha);
            console.log(Grid.RegistroSelecionado);

            $('#_modalImpressaoOB').modal('show');

            $('#_modalImpressaoOB button[data-button="btnImprimirConfirmar"]').off('click');
            $('#_modalImpressaoOB button[data-button="btnImprimirConfirmar"]').on('click', function () {
                if (!ValidarImpressaoOB()) return false;
                var params = jQuery.param({
                    Id: Grid.RegistroSelecionado.Codigo,
                    numOb: Grid.RegistroSelecionado.NumOB,
                    tipo: $('#_modalImpressaoOB input[name="tipo"]:checked').val(),
                    filetype: 'PDF',
                    filtromudapah: Grid.RegistroSelecionado.UGPagadora, //CodigoMudapah().val(),
                    contenttype: 'application/pdf'
                });
                window.open("/PagamentoContaUnica/AutorizacaoDeOB/ImprimirOB/?" + params, "_blank");
                $('#_modalImpressaoOB').modal('hide');
            });

            return false;
        });
    };

    Grid.BindTransmitirSelecionarTodos = function () {
        $("table#tblPesquisa thead tr th input[name='transmitirTodos']").off('click');
        $("table#tblPesquisa thead tr th input[name='transmitirTodos']").on('click', function () {
            var checked = $(this).is(':checked');
            $('table#tblPesquisa tbody tr td input[type="checkbox"]').each(function () {
                $(this).prop('checked', checked);
                $(this).trigger('change');
            });

        });
    };

    Grid.BindExcluir = function () {
        $('button[data-button="Excluir1"]:not(.disabled)').off('click');
        $('button[data-button="Excluir1"]:not(.disabled)').on('click', function () {

            var linha = $(this).parent().parent();
            Grid.RegistroSelecionado = Grid.ItemPorLinha(linha);

            $('#_modalExclusaoOB').modal('show');

            $('#_modalExclusaoOB button#btn-confirmar-exclusao').off('click');
            $('#_modalExclusaoOB button#btn-confirmar-exclusao').on('click', function () {

                if (!ValidarExcluirAutorizacaoOB()) return false;

                var Id = 0;
                Id = Grid.RegistroSelecionado.Codigo;

                $.ajax({
                    type: "POST",
                    url: "/PagamentoContaUnica/AutorizacaoDeOB/Delete",
                    data: { 'Id': Id },
                    beforeSend: function (hrx) {
                        waitingDialog.show('Excluindo a OB...');
                    }
                }).done(function (xhrResponse) {
                    console.log("done!");
                    //AbrirModal(xhrResponse, function () { $('#btnPesquisar').trigger('click'); });
                    $('#btnPesquisar').trigger('click');
                }).fail(function (jqXHR, textStatus) {
                    AbrirModal(jqXHR.statusText, function () { });
                }).always(function () {
                    $('#_modalAutorizacaoOB').modal('hide');
                    waitingDialog.hide();
                });

            });

            return false;
        });
    };

    Grid.BindRetransmitir = function () {

        if (intervalRetransmitir) {
            clearInterval(intervalRetransmitir);
            intervalRetransmitir = false;
        }

        intervalRetransmitir = setInterval(function () {
            if ($('table#tblPesquisa tbody tr td input[type="checkbox"]:checked').length == 0) {
                $('button[data-button="Retransmitir"]').attr('disabled', 'disabled');
            } else {
                $('button[data-button="Retransmitir"]').removeAttr('disabled');
            }
        }, 50);

        $('button[data-button="Retransmitir"]:not(.disabled)').off('click');
        $('button[data-button="Retransmitir"]:not(.disabled)').on('click', function () {

            var items = [];

            var itemsSelecionados = $('table#tblPesquisa tbody tr td input[type="checkbox"]:checked');
            for (var i = 0; i < itemsSelecionados.length; i++) {
                var linha = $(itemsSelecionados[i]).parent().parent();
                Grid.RegistroSelecionado = Grid.ItemPorLinha(linha);
                items.push(Grid.RegistroSelecionado.Codigo);
            }

            $.ajax({
                type: "POST",
                url: "/PagamentoContaUnica/AutorizacaoDeOB/Retransmitir",
                data: { 'ListaDeOB': items, 'filtroMudapah': Grid.RegistroSelecionado.UGPagadora },
                beforeSend: function (hrx) {
                    waitingDialog.show('Retrasmitindo itens selecionados...');
                }
            }).done(function (xhrResponse) {
                $('#btnPesquisar').trigger('click');
                console.log("done!");
            }).fail(function (jqXHR, textStatus) {
                AbrirModal(jqXHR.statusText, function () { });
            }).always(function () {
                waitingDialog.hide();
            });

        });


    };

    //Grid.BindTransmitir = function () {

    //    if (intervalTransmitir) {
    //        clearInterval(intervalTransmitir);
    //        intervalTransmitir = false;
    //    }

    //    intervalTransmitir = setInterval(function () {
    //        if ($('table#tblGrid tbody tr td input[type="checkbox"]:checked').length == 0) {
    //            $('button[data-button="TransmitirOB"]').attr('disabled', 'disabled');
    //        } else {
    //            $('button[data-button="TransmitirOB"]').removeAttr('disabled');
    //        }
    //    }, 50);

    //    $('button[data-button="TransmitirOB"]:not(.disabled)').off('click');
    //    $('button[data-button="TransmitirOB"]:not(.disabled)').on('click', function () {

    //        //var items = [];

    //        //var itemsSelecionados = $('table#tblGrid tbody tr td input[type="checkbox"]:checked');
    //        //for (var i = 0; i < itemsSelecionados.length; i++) {
    //        //    var linha = $(itemsSelecionados[i]).parent().parent();
    //        //    Grid.RegistroSelecionado = Grid.ItemPorLinha(linha);
    //        //    items.push(Grid.RegistroSelecionado.Codigo);
    //        //}

    //        if (!ValidarAutorizarOB()) return false;

    //        if ($("select#EhConfirmacaoPagamento").val() === "1") {
    //            if (!ValidarTransmitirConfirmacao()) return false;
    //        }

    //        var $form = $("form#form_autorizacao_ob");

    //        $.ajax({
    //            type: "POST",
    //            url: "/PagamentoContaUnica/AutorizacaoDeOB/TransmitirOB",
    //            data: $form.serialize(),
    //            content: 'application/x-www-form-urlencoded; charset=UTF-8',
    //            beforeSend: function (hrx) {
    //                waitingDialog.show('Transmitindo');
    //            }
    //        }).done(function (xhrResponse) {
    //            FillGrid(xhrResponse.grid);
    //            BindGridTodos();
    //        }).fail(function (jqXHR, textStatus) {
    //            AbrirModal(jqXHR.statusText, function () { });
    //        }).always(function () {
    //            waitingDialog.hide();
    //        });

    //        return false;

    //    });


    //};

    Grid.Datatables = {};
    Grid.Datatables.BindEvents = function () {
        BuildDataTables.setEvent('Events_LenghtChange', function () {
            Grid.Bind();
        });
        BuildDataTables.setEvent('Events_PageChange', function () {
            Grid.Bind();
        });
        BuildDataTables.setEvent('Events_Order', function () {
            $('.popover').popover('hide');
        });
    };

    function CustomValidators() {
        $.validator.addMethod("notEqualTo", function (value, element, param) {
            return param !== value;
        }, "Please enter a different value, values must not be the same.");
        $.validator.methods.date = function (value, element) {
            return this.optional(element) || moment(value, "DD/MM/YYYY", true).isValid();
        }
    }

    function adicionarFiltroAgrupamento() {
        $('#filtroAgrupOB').each(function () {
            var title = $(this).text();
            $(this).html('<input type="text" placeholder="' + title + '" style="width:75px"/>');
        });


        var cabecalhos = $('#tblGrid').find('thead tr th').length;
        var linhas = $($('#tblGrid').find('tbody tr')[0]).find('td').length;
        if (cabecalhos === linhas) {
            var table = util.getDataTable('table#tblGrid', 'div#grid');

            table.columns().every(function () {
                var col = this;

                $('input', this.header()).on('keyup blur', function (e) {
                    e.preventDefault();
                    var txtBox = this;

                    if ((e.type === 'keyup' && e.keyCode === 13) || e.type === 'blur') {
                        if (col.search() !== txtBox.value) {
                            table.page.len(-1);
                            col.search(txtBox.value).draw();
                        }
                    }
                });
            });
        }
    }

    return {
        Init: function () {
            init();
        },
        InitGrid: function () {
            var cabecalhos = $('#tblGrid').find('thead tr th').length;
            var linhas = $($('#tblGrid').find('tbody tr')[0]).find('td').length;

            if (cabecalhos === linhas && $("._tbDataTables")) {
                initGrid();
            }
        }
    };

}());


$(function () {
    AUTORIZACAO_OB.Init();
    AUTORIZACAO_OB.InitGrid();
});
