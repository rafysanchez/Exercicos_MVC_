var PROGRAMACAO_DESEMBOLSO_EXECUCAO = (function () {
    var intervalSalvar = false;
    var intervalRetransmitir = false;
    var intervalTransmitir = false;
    var intervalConsultarPD = false;

    var TIPO_EXECUCAO_PD = {
        POR_LISTA: 1,
        POR_NUMERO: 2
    };

    var TIPO_EXCLUSAO_EXECUCAO_PD = {
        POR_NUMERO: 1,
        POR_AGRUPAMENTO: 2
    };

    function init() {
        Iniciar();
        bindAll();
        CustomValidators();

        if (findGetParameter('tipo') === 'c') {
            BloqueiaCampos('form_execucao_pd');
            $('#form_execucao_pd h2').html('Visualizar Execução de PD');
            $('#btnCancelar').hide();
            $('#btnSalvar').hide();
            $('#btnTransmitir').hide();
            $('#btnVoltar').show();
        }

        $('#filtroPD').each(function () {
            var title = $(this).text();
            $(this).html('<input type="text" placeholder="' + title + '" style="width:75px" />');
        });

        $('#tblGrid').DataTable().destroy();

        var table = $('#tblGrid').DataTable({
            "pageLength": -1
        });

        // Apply the search
        table.columns().every(function () {
            var that = this;

            $('input', this.header()).on('keyup change', function () {
                if (that.search() !== this.value) {
                    that
                        .search(this.value)
                        .draw();
                }
            });
        });

    }

    function initGrid() {
        Grid.Bind();

        setGlobalDataTables();
    }

    var bindAll = function () {
        BindTipoExecucaoPD();
        BindConfirmacao();
        BindConsultarPD();
        BindAdicionarPD();
        BindGridTodos();
        BindCancelar();
        BindTrasmitir();
        BindSalvar();
        BindPaginacaoEventos();
        BindTravarRadio();
    };

    var Iniciar = function () {
        $('div.ListaPD').hide();
        $('div.AdicionarPD').hide();
    };

    var BindNumOP = function () {
        $('div.ConfirmacaoPagamento').hide();
        var numOP = $('form#form_execucao_pd input[name="numOP"]').val();
        if (numOP !== "" && numOP !== undefined) {
            $('div.ConfirmacaoPagamento').show();
        }

    };

    var BindTravarRadio = function () {
        $('#tblGrid tbody tr', 'div#grid').each(function () {

            var linha = $(this);

            Grid.RegistroSelecionado = Grid.ItemPorLinha(linha);
            $(linha).find('input[type="radio"][name*="NouP"]').each(function () {
                if (Grid.RegistroSelecionado.StatusSiafem === 'S' || Grid.RegistroSelecionado.StatusProdesp === 'S') {
                    if ($(this).checked !== true) {
                        $(this).attr('disabled', true);
                    }
                }
            });

        });
    }

    var BindPaginacaoEventos = function () {
        $('#tblGrid').on('draw.dt', function () {
            BindTravarRadio();
        }).DataTable();
    }

    var BindTipoExecucaoPD = function () {
        $('form#form_execucao_pd select[name="TipoExecucao"]').off('change');
        $('form#form_execucao_pd select[name="TipoExecucao"]').on('change', function () {
            LimparListaItems();
            $('span.field-validation-error span').html('');
            switch (parseInt($(this).val())) {

                case TIPO_EXECUCAO_PD.POR_LISTA: $('div.ListaPD').show(); $('div.AdicionarPD').hide(); $('div.AdicionarPD input').prop('disabled', true); $('div.ListaPD input').removeAttr('disabled'); break;
                case TIPO_EXECUCAO_PD.POR_NUMERO: $('div.ListaPD').hide(); $('div.AdicionarPD').show(); $('div.ListaPD input').prop('disabled', true); $('div.AdicionarPD input').removeAttr('disabled'); break;

                default:
                    $('div.ListaPD').hide();
                    $('div.AdicionarPD').hide();
            }
        });
    };

    var BindConfirmacao = function () {
        //confirmacao

        $('form#form_execucao_pd select[name="opcoesTipoPagamento"]').attr('disabled', 'disabled');
        $('form#form_execucao_pd input[name="dataPreparacao"]').attr('disabled', 'disabled');

        $('form#form_execucao_pd select[name="confirmacao"]').off('change');
        $('form#form_execucao_pd select[name="confirmacao"]').on('change', function () {

            $('form#form_execucao_pd select[name="opcoesTipoPagamento"]').val('');
            $('form#form_execucao_pd input[name="dataPreparacao"]').val('');

            if ($(this).val() === "S") {
                $('form#form_execucao_pd select[name="opcoesTipoPagamento"]').removeAttr('disabled');
                $('form#form_execucao_pd input[name="dataPreparacao"]').removeAttr('disabled');
            } else {
                $('form#form_execucao_pd select[name="opcoesTipoPagamento"]').attr('disabled', 'disabled');
                $('form#form_execucao_pd input[name="dataPreparacao"]').attr('disabled', 'disabled');
            }
        });
    };


    var BindConsultarPD = function () {

        if (intervalConsultarPD) {
            clearInterval(intervalConsultarPD);
            intervalConsultarPD = false;
        }

        intervalConsultarPD = setInterval(function () {
            if ($('#EfetuouConsulta').val() === "true") {
                //if ($("table#tblGrid thead th input[name='SiafemStatus']").val() === "S" && $("table#tblGrid thead th input[name='ProdespStatus']").val() === "S"){
                $('button[data-button="ConsultarPD"]').attr('disabled', 'disabled');
            } else {
                $('button[data-button="ConsultarPD"]').removeAttr('disabled');
            }
        }, 50);

        $('button[data-button="ConsultarPD"]:not(.disabled)').off('click');
        $('button[data-button="ConsultarPD"]:not(.disabled)').on('click', function () {

            //$('#btnConsultarPD').off('click');
            //$('#btnConsultarPD').on('click', function () {

            if (!ValidarListaPD()) return false;

            //LimparListaItems();

            $.ajax({
                type: "POST",
                url: "/PagamentoContaUnica/ExecucaoPD/ConsultarListaPD",
                data: $('form#form_execucao_pd').serialize(),// serializes the form's elements.
                beforeSend: function (hrx) {
                    waitingDialog.show('Consultando Siafem...');
                }
            }).done(function (xhrResponse) {
                FillGrid(xhrResponse)
                BindGridTodos();

            }).fail(function (jqXHR, textStatus) {
                waitingDialog.hide();
                AbrirModal(jqXHR.statusText, function () { });
                LimparListaItems();
            }).always(function () {
                waitingDialog.hide();
            });

            return false;
        });
    };

    var FillGrid = function (html) {
        $('div#grid').html(html);
        $('#tblGrid', 'div#grid').dataTable({
            "pagingType": "full_numbers",
            "pageLength": -1,
            "orderable": false
        });

        BindPaginacaoEventos();
        BindTravarRadio();

        // DataTable
        var table = util.getDataTable('table#tblGrid', 'div#grid');

        //$('#filtro thead th').each(function () {
        $('#filtroPD', 'div#grid').each(function () {
            var title = $(this).text();
            $(this).html('<input type="text" placeholder="' + title + '" style="width:75px" />');
        });

        // Apply the search
        table.columns().every(function () {
            var that = this;
            //console.log(that);

            //$('#filtroPD').on('keyup change', function () {
            $('input', this.header()).on('keyup change', function () {
                //console.log(that.search() + '  ' + this.value);
                if (that.search() !== this.value) {
                    that
                        .search(this.value)
                        .draw();
                }
            });
        });
    };


    var BindAdicionarPD = function () {
        $('#btnAdicionarPD').off('click');
        $('#btnAdicionarPD').on('click', function () {

            if (!ValidarAdicionarPD()) return false;

            var $form = $("form#form_execucao_pd");
            $form.validate();

            if ($form.valid()) {
                $.ajax({
                    type: "POST",
                    url: "/PagamentoContaUnica/ExecucaoPD/AdicionarPD",
                    data: $('form#form_execucao_pd').serialize(),
                    content: 'application/x-www-form-urlencoded; charset=UTF-8',
                    beforeSend: function (hrx) {
                        waitingDialog.show('Consultando Siafem...');
                    }
                }).done(function (xhrResponse) {
                    FillGrid(xhrResponse)
                    BindGridTodos();
                }).fail(function (jqXHR, textStatus) {
                    AbrirModal(jqXHR.statusText, function () { });
                }).always(function () {
                    waitingDialog.hide();
                });
            }

            return false;
        });
    };

    var BindGridTodos = function () {

        $("table#tblGrid thead th input[name='TodosPrioridade']").off('change');
        $("table#tblGrid thead th input[name='TodosPrioridade']").on('change', function () {
            var checked = $(this).val();

            $('#tblGrid tbody tr', 'div#grid').each(function () {
                var linha = $(this);

                if (checked === "P") {
                    $(linha).find('input[type="radio"][name*="Prioritario"][value="True"]').each(function () {
                        if (!$(this).is(':disabled')) {
                            $(this).prop('checked', true);
                        }
                    });
                } else {
                    $(linha).find('input[type="radio"][name*="Prioritario"][value="False"]').each(function () {
                        if (!$(this).is(':disabled')) {
                            $(this).prop('checked', true);
                        }
                    });
                }
            });
        });

        $('input[type="radio"][name*="NouP"]').off('change');
        $('input[type="radio"][name*="NouP"]').on('change', function () {
            $("table#tblGrid thead th input[name='TodosPrioridade'][value='P']").prop('checked', false);
            $("table#tblGrid thead th input[name='TodosPrioridade'][value='N']").prop('checked', false);
        });

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

        function getTotal() {

            let result = 0;

            $('input[type="input"][name*="Valor"]').each(function () {
                alert(this.val());
            })

            return result;
        }

        $('#grid').off('click', 'button[data-button="ExcluirItem"]:not(.disabled)');
        $('#grid').on('click', 'button[data-button="ExcluirItem"]:not(.disabled)', function () {
            var linha = $(this).parent().parent();
            var table = $(this).closest('table');

            RemoverLinhaDataTable(linha);
            SomarTotal();
            reordenarTabela(table);
        });

        function reordenarTabela(table) {
            var count = 0;
            $(table).find('tbody tr:not(.tr-placeholder)').each(function (index, element) {
                var tr = $(this);
                var pattern = /[(\d+)]/i;
                var hiddens = tr.find('input[type=hidden]');

                hiddens.each(function (i, e) {
                    var hdn = $(this);
                    var name = hdn.prop('name');
                    name = name.replace(pattern, count);
                    hdn.prop('name', name);
                });

                tr.attr('data-index', count);
                tr.data('index', count);

                count++;
            });
        }

        $("table#tblGrid").off('click', "tbody tr td button[data-button='Visualizar']");
        $("table#tblGrid").on('click', "tbody tr td button[data-button='Visualizar']", function () {

            var linha = $(this).parent().parent();
            var obj = ItemPorLinha(linha);

            $.ajax({
                type: "POST",
                url: "/PagamentoContaUnica/ExecucaoPD/PDPorNumero",
                data: { "filtroAdicionarPd.NumeroPD": obj.NumPD, "GestaoLiquidante": obj.GestaoLiquidante, "UGLiquidante": obj.UGLiquidante },
                content: 'application/x-www-form-urlencoded; charset=UTF-8',
                beforeSend: function (hrx) {
                    waitingDialog.show('Visualizar detalhes');
                }
            }).done(function (xhrResponse) {

                for (var property in xhrResponse.data) {
                    if (xhrResponse.data.hasOwnProperty(property)) {
                        $('#_modalVisualizarDetalhesPD input[data-id="' + property + '"]').val(xhrResponse.data[property]);
                        $('#_modalVisualizarDetalhesPD td[data-id="' + property + '"]').html(xhrResponse.data[property]);
                        $('#_modalVisualizarDetalhesPD [data-id="' + property + '"]').prop('title', xhrResponse.data[property]);
                    }
                }

                if (xhrResponse.data.Fonte2 === null || xhrResponse.data.Fonte2 === "")
                    $('#_modalVisualizarDetalhesPD [data-id="Fonte2"]').parent().remove();


                if (xhrResponse.data.Fonte3 === null || xhrResponse.data.Fonte3 === "")
                    $('#_modalVisualizarDetalhesPD [data-id="Fonte3"]').parent().remove();

                $('#_modalVisualizarDetalhesPD').modal('show');

            }).fail(function (jqXHR, textStatus) {
                AbrirModal(jqXHR.statusText, function () { });
            }).always(function () {
                waitingDialog.hide();
            });


        });

        $('[data-toggle="popover"]').popover();
    };

    var BindCancelar = function () {
        //$('button#btnCancelar').off('click');
        //$('button#btnCancelar').on('click', function () {
        //    alert('Cancelar...');
        //});
    };

    var BindTrasmitir = function () {

        if (intervalTransmitir) {
            clearInterval(intervalTransmitir);
            intervalTransmitir = false;
        }

        intervalTransmitir = setInterval(function () {
            if ($('table#tblGrid tbody tr td input[type="checkbox"]:checked').length === 0) {
                $('button[data-button="TransmitirPD"]').attr('disabled', 'disabled');
            } else {
                $('button[data-button="TransmitirPD"]').removeAttr('disabled');
            }
        }, 50);

        $('button[data-button="TransmitirPD"]:not(.disabled)').off('click');
        $('button[data-button="TransmitirPD"]:not(.disabled)').on('click', function () {

            if (!ValidarExecutarPD()) return false;

            $('#EfetuouConsulta').val("true")

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

            var $form = $("form#form_execucao_pd");

            $.ajax({
                type: "POST",
                url: "/PagamentoContaUnica/ExecucaoPD/Transmitir",
                data: $form.serialize(),
                content: 'application/x-www-form-urlencoded; charset=UTF-8',
                beforeSend: function (hrx) {
                    waitingDialog.show('Transmitindo');
                }
            }).done(function (xhrResponse) {
                //BindTrasmitirProdesp();
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

    var BindTrasmitirProdesp = function () {

        if ($("select#confirmacao").val() !== "S") return false;

        if (!ValidarTransmitirConfirmacao()) return false;

        var $form = $("form#form_execucao_pd");

        $.ajax({
            type: "POST",
            url: "/PagamentoContaUnica/ExecucaoPD/TransmitirConfirmacao",
            data: $form.serialize(),
            content: 'application/x-www-form-urlencoded; charset=UTF-8',
            beforeSend: function (hrx) {
                waitingDialog.show('Transmitindo');
            }
        }).done(function (xhrResponse) {
            //FillGrid(xhrResponse.grid);
            //BindGridTodos();
        }).fail(function (jqXHR, textStatus) {
            AbrirModal(jqXHR.statusText, function () { });
        }).always(function () {
            waitingDialog.hide();
        });

        return false;
    };

    var BindSalvar = function () {

        if (intervalSalvar) {
            clearInterval(intervalSalvar);
            intervalSalvar = false;
        }

        intervalSalvar = setInterval(function () {
            if ($('table#tblGrid tbody tr td input[type="checkbox"]:checked').length === 0) {
                $('button[data-button="SalvarPD"]').attr('disabled', 'disabled');
            } else {
                $('button[data-button="SalvarPD"]').removeAttr('disabled');
            }
        }, 50);

        $('button[data-button="SalvarPD"]:not(.disabled)').off('click');
        $('button[data-button="SalvarPD"]:not(.disabled)').on('click', function () {

            var table = util.getDataTable('table#tblGrid', 'div#grid');
            var data = table.rows().data();
            var allInputs = [];
            var formInputs = $('form#form_execucao_pd :input:not(.hiddenrow)').serializeArray()

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

            if (!ValidarAdicionarPD()) return false;

            $('#EfetuouConsulta').val("true")

            $.ajax({
                type: "POST",
                url: "/PagamentoContaUnica/ExecucaoPD/Save",
                data: allInputs,//$('form#form_execucao_pd').serialize(),// serializes the form's elements.
                content: 'application/x-www-form-urlencoded; charset=UTF-8',
                beforeSend: function (hrx) {
                    waitingDialog.show('Salvando');
                }
            }).done(function (jqXHR) {
                $.confirm({
                    text: "Programação de Desembolso Execução salva com sucesso!",
                    title: "Confirmação",
                    cancel: function () {
                        console.log(jqXHR);
                        window.location = "/PagamentoContaUnica/ExecucaoPD/Edit/" + jqXHR.AgrupamentoItemPD;
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
    };

    var ItemPorLinha = function (linha) {
        return {
            IdExecucaoPD: linha.find('input[name*="IdExecucaoPD"]').val(),
            Codigo: linha.find('input[name*="Id"]').val(),
            AgrupamentoItemPD: linha.find('input[name*="AgrupamentoItemPD"]').val(),
            NumPD: linha.find('input[name*="NumPD"]').val(),
            UGPagadora: linha.find('input[name*="UGPagadora"]').val(),
            GestaoPagadora: linha.find('input[name*="GestaoPagadora"]').val(),
            UGLiquidante: linha.find('input[name*="UGLiquidante"]').val(),
            GestaoLiquidante: linha.find('input[name*="GestaoLiquidante"]').val(),
            AnoAserpaga: linha.find('input[name*="AnoAserpaga"]').val(),
            NouP: linha.find('input[name*="NouP"]:checked').val(),
            StatusSiafem: linha.find('input[name*="SiafemStatus"]').val(),
            StatusProdesp: linha.find('input[name*="ProdespStatus"]').val()
        };
    };
    var LimparListaItems = function () {
        $.ajax({
            type: "POST",
            url: "/PagamentoContaUnica/ExecucaoPD/LimparListaItems",
            data: $('form#form_execucao_pd').serialize(),// serializes the form's elements.
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
    var ValidarListaPD = function () {
        var $form = $("form#form_execucao_pd");
        $("form#form_execucao_pd").removeData("validator").removeData("unobtrusiveValidation");
        $.validator.unobtrusive.parse("form#form_execucao_pd");

        $("input#UGLiquidante").rules("add", { required: true, messages: { required: "Campo Obrigatório" } });
        $("input#GestaoLiquidante").rules("add", { required: true, messages: { required: "Campo Obrigatório" } });
        $("input#UGPagadora").rules("add", { required: true, messages: { required: "Campo Obrigatório" } });
        $("input#GestaoPagadora").rules("add", { required: true, messages: { required: "Campo Obrigatório" } });
        $("input#filtroListaPd_DataInicial").rules("add", { required: true, messages: { required: "Campo Obrigatório" } });
        $("input#filtroListaPd_DataFinal").rules("add", { required: true, messages: { required: "Campo Obrigatório" } });

        $form.validate();

        return $form.valid();
    };
    var ValidarAdicionarPD = function () {

        var $form = $("form#form_execucao_pd");
        $("form#form_execucao_pd").removeData("validator").removeData("unobtrusiveValidation");
        $.validator.unobtrusive.parse("form#form_execucao_pd");

        $('select#TipoExecucao').rules("add", { required: true, messages: { required: "Campo Obrigatório" } });
        $("input#UGPagadora").rules("add", { required: true, messages: { required: "Campo Obrigatório" } });
        $("input#GestaoPagadora").rules("add", { required: true, messages: { required: "Campo Obrigatório" } });
        $("input#UGLiquidante").rules("add", { required: true, messages: { required: "Campo Obrigatório" } });
        $("input#GestaoLiquidante").rules("add", { required: true, messages: { required: "Campo Obrigatório" } });
        $("input#filtroAdicionarPd_NumeroPD").rules("add", { required: true, messages: { required: "Campo Obrigatório" } });

        $form.validate({ ignore: [] });

        return $form.valid();

    };
    var ValidarExecutarPD = function () {

        $.validator.setDefaults({ ignore: [] });

        var $form = $("form#form_execucao_pd");
        $form.validate();

        $("form#form_execucao_pd").removeData("validator").removeData("unobtrusiveValidation");
        $.validator.unobtrusive.parse("form#form_execucao_pd");

        CodigoMudapah().rules("add", { required: true, messages: { required: "Campo Obrigatório" } });
        CodigoMudapah().rules("add", { notEqualTo: "0", messages: { notEqualTo: "Informe a pd para executar." } });

        return $form.valid();
    };

    var ValidarTransmitirConfirmacao = function () {

        //$.validator.setDefaults({ ignore: [] });

        var $form = $("form#form_execucao_pd");
        //$form.validate();

        $("form#form_execucao_pd").removeData("validator").removeData("unobtrusiveValidation");
        $.validator.unobtrusive.parse("form#form_execucao_pd");

        $("select#TipoPagamento").rules("add", { required: true, messages: { required: "Campo Obrigatório" } });
        //$("input#Dt_confirmacao").rules("add", { required: true, messages: { required: "Campo Obrigatório" } });
        $('form#form_execucao_pd input[name="DataConfirmacao"]').rules("add", { required: true, messages: { required: "Campo Obrigatório" } });

        $form.validate({ ignore: [] });

        return $form.valid();
    };

    var ValidarImpressaoOB = function () {

        $.validator.setDefaults({
            ignore: []
        });

        var $form = "form#frm-imprimir-pd";
        $($form).validate();

        $($form).removeData("validator").removeData("unobtrusiveValidation");
        $.validator.unobtrusive.parse($form);

        $($form).find("input[name='tipo']").rules("add", { required: true, messages: { required: "Selecione uma opção para efetuar a impressão." } });

        return $($form).valid();
    };
    var ValidarExclurExecPD = function () {
        $.validator.setDefaults({
            ignore: []
        });

        var $form = "form#frm-excluir-exec-pd";
        $($form).validate();

        $($form).removeData("validator").removeData("unobtrusiveValidation");
        $.validator.unobtrusive.parse($form);

        $($form).find("input[name='tipo']").rules("add", { required: true, messages: { required: "Selecione uma opção para efetuar a exclusão." } });

        return $($form).valid();
    };
    var ValidarCancelarOB = function () {
        $.validator.setDefaults({
            ignore: []
        });

        var $form = "form#frm-cancelar-ob";
        $($form).validate();

        $($form).removeData("validator").removeData("unobtrusiveValidation");
        $.validator.unobtrusive.parse($form);

        $($form).find("input[name='mudapah-text']").rules("add", { required: true, messages: { required: "Campo obrigatório." } });
        $($form).find("input[name='gestao']").rules("add", { required: true, messages: { required: "Campo obrigatório." } });
        $($form).find("input[name='OB']").rules("add", { required: true, messages: { required: "Campo obrigatório." } });
        $($form).find("textarea[name='causa']").rules("add", { required: true, messages: { required: "Campo obrigatório." } });

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
            Codigo: linha.find('input[name*="Id"]').val(),
            Agrupamento: linha.find('input[name*="Agrupamento"]').val(),
            NumPD: linha.find('input[name*="NumPD"]').val(),
            NumOB: linha.find('input[name*="NumOB"]').val(),
            UGPagadora: linha.find('input[name*="UGPagadora"]').val(),
            GestaoPagadora: linha.find('input[name*="GestaoPagadora"]').val(),
            UGLiquidante: linha.find('input[name*="UGLiquidante"]').val(),
            GestaoLiquidante: linha.find('input[name*="GestaoLiquidante"]').val(),
            AnoAserpaga: linha.find('input[name*="AnoAserpaga"]').val(),
            NouP: linha.find('input[name*="NouP"]:checked').val(),
            StatusSiafem: linha.find('input[name*="SiafemStatus"]').val()
        };
    };
    Grid.Bind = function () {
        Grid.BindRetrasmitir();
        Grid.BindImprimir();
        Grid.BindTransmitirCheckbox();
        Grid.BindCancelar();
        Grid.Datatables.BindEvents();
        Grid.BindExcluir();
        Grid.BindTransmitirSelecionarTodos();

        $('[data-toggle="popover"]').popover();
        $('[data-toggle="tooltip"]').tooltip();

    };
    Grid.BindTransmitirCheckbox = function () {
        $('input[type="checkbox"][name*=".trasmitir"]').off('change');
        $('input[type="checkbox"][name*=".trasmitir"]').on('change', function () {

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
            $('#_modalImpressaoPD').modal('show');

            $('#_modalImpressaoPD button[data-button="btnImprimirConfirmar"]').off('click');
            $('#_modalImpressaoPD button[data-button="btnImprimirConfirmar"]').on('click', function () {
                if (!ValidarImpressaoOB()) return false;
                var params = jQuery.param({
                    Id: Grid.RegistroSelecionado.Codigo,
                    tipo: $('#_modalImpressaoPD input[name="tipo"]:checked').val(),
                    filetype: 'PDF',
                    filtromudapah: Grid.RegistroSelecionado.UGPagadora, //CodigoMudapah().val(),
                    contenttype: 'application/pdf'
                });
                window.open("/PagamentoContaUnica/ExecucaoPD/ImprimirOB/?" + params, "_blank");
                $('#_modalImpressaoPD').modal('hide');
            });

            return false;
        });
    };
    Grid.BindCancelar = function () {
        $('button[data-button="Cancelar1"]:not(.disabled)').off('click');
        $('button[data-button="Cancelar1"]:not(.disabled)').on('click', function () {

            var linha = $(this).parent().parent();
            Grid.RegistroSelecionado = Grid.ItemPorLinha(linha);
            console.log(Grid);

            //<!-- string mudapah, string OB, string gestao, string causa -->

            $('#_modalCancelarOB input[name="mudapah-text"]').val(CodigoMudapah().val());
            $('#_modalCancelarOB input[name="OB-text"]').val(Grid.RegistroSelecionado.NumOB);
            $('#_modalCancelarOB input[name="gestao-text"]').val(Grid.RegistroSelecionado.GestaoLiquidante);
            $('#_modalCancelarOB input[name="mudapah-text"]').val(Grid.RegistroSelecionado.UGPagadora);//CodigoMudapah().val());
            $('#_modalCancelarOB input[name="mudapah"]').val(Grid.RegistroSelecionado.UGPagadora);
            $('#_modalCancelarOB input[name="OB"]').val(Grid.RegistroSelecionado.NumOB);
            $('#_modalCancelarOB input[name="gestao"]').val(Grid.RegistroSelecionado.GestaoLiquidante);

            $('#_modalCancelarOB').modal('show');

            $('#_modalCancelarOB button#btn-cancelar-ob').off('click');
            $('#_modalCancelarOB button#btn-cancelar-ob').on('click', function () {

                if (!ValidarCancelarOB()) return false;

                $.ajax({
                    type: "POST",
                    url: "/PagamentoContaUnica/ExecucaoPD/CancelarOB",
                    data: $('#frm-cancelar-ob').serialize(),
                    beforeSend: function (hrx) {
                        waitingDialog.show('Transmitindo');
                    }
                }).done(function (xhrResponse) {
                    console.log("done!");
                    $.confirm({
                        text: "Ordem Bancária cancelada com sucesso",
                        title: "Confirmação",
                        cancel: function () {
                            ShowLoading();
                            $("#form_filtro").submit();
                        },
                        cancelButton: "Ok",
                        confirmButton: "",
                        post: true,
                        cancelButtonClass: "btn-default",
                        modalOptionsBackdrop: true
                    });


                }).fail(function (jqXHR, textStatus) {
                    AbrirModal(jqXHR.statusText, function () { });
                }).always(function () {
                    $('#_modalCancelarOB').modal('hide');
                    waitingDialog.hide();
                });

            });

            return false;
        });
    };
    Grid.BindTransmitirSelecionarTodos = function () {
        $('table#tblPesquisa thead tr th input[name="trasmitirTodos"]', 'div#grid').off('click');
        $('table#tblPesquisa thead tr th input[name="trasmitirTodos"]', 'div#grid').on('click', function () {
            var checked = $(this).is(':checked');
            $('table#tblPesquisa tbody tr td input[type="checkbox"]', 'div#grid').each(function () {
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

            $('#_modalExclusaoPD').modal('show');

            $('#_modalExclusaoPD button#btn-confirmar-exclusao').off('click');
            $('#_modalExclusaoPD button#btn-confirmar-exclusao').on('click', function () {

                if (!ValidarExclurExecPD()) return false;

                var Id = 0;
                var tipo = $('#_modalExclusaoPD input[name="tipo"]:checked').val();

                if (tipo === TIPO_EXCLUSAO_EXECUCAO_PD.POR_NUMERO) {
                    Id = Grid.RegistroSelecionado.Codigo;
                } else {
                    Id = Grid.RegistroSelecionado.Agrupamento;
                }

                $.ajax({
                    type: "POST",
                    url: "/PagamentoContaUnica/ExecucaoPD/Delete",
                    data: { 'Id': Id, 'tipo': tipo },
                    beforeSend: function (hrx) {
                        waitingDialog.show('Retrasmitindo itens selecionados...');
                    }
                }).done(function (xhrResponse) {
                    console.log("done!");
                    AbrirModal(xhrResponse, function () { $('#btnPesquisar').trigger('click'); });
                }).fail(function (jqXHR, textStatus) {
                    AbrirModal(jqXHR.statusText, function () { });
                }).always(function () {
                    $('#_modalExclusaoPD').modal('hide');
                    waitingDialog.hide();
                });

            });

            return false;
        });
    };

    //Grid.BindExcluirItem = function () {

    //    $('button[data-button="ExcluirItem"]:not(.disabled)').off('click');
    //    $('button[data-button="ExcluirItem"]:not(.disabled)').on('click', function () {

    //        $('#_modalExclusaoPD_2').modal('show');

    //    });
    //}

    Grid.BindRetrasmitir = function () {

        if (intervalRetransmitir) {
            clearInterval(intervalRetransmitir);
            intervalRetransmitir = false;
        }

        intervalRetransmitir = setInterval(function () {
            if ($('table#tblPesquisa tbody tr td input[type="checkbox"]:checked').length === 0) {
                $('button[data-button="Retrasmitir"]').attr('disabled', 'disabled');
            } else {
                $('button[data-button="Retrasmitir"]').removeAttr('disabled');
            }
        }, 50);

        $('button[data-button="Retrasmitir"]:not(.disabled)').off('click');
        $('button[data-button="Retrasmitir"]:not(.disabled)').on('click', function () {

            var items = [];

            var itemsSelecionados = $('table#tblPesquisa tbody tr td input[type="checkbox"]:checked');
            for (var i = 0; i < itemsSelecionados.length; i++) {
                var linha = $(itemsSelecionados[i]).parent().parent();
                Grid.RegistroSelecionado = Grid.ItemPorLinha(linha);
                items.push(Grid.RegistroSelecionado.Codigo);
            }

            $.ajax({
                type: "POST",
                url: "/PagamentoContaUnica/ExecucaoPD/Retrasmitir",
                data: { 'Codigos': items, 'filtroMudapah': Grid.RegistroSelecionado.UGPagadora },
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

    return {
        Init: function () {
            init();
        },
        InitGrid: function () {
            initGrid();
        }
    };


}());


$(function () {
    PROGRAMACAO_DESEMBOLSO_EXECUCAO.Init();

    if ($("._tbDataTables")) {
        PROGRAMACAO_DESEMBOLSO_EXECUCAO.InitGrid();
    }

});
