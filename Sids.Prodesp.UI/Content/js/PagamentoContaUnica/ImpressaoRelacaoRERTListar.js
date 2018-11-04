var obj = "ImpressaoRelacaoRERT";


function ValidaFormulario() {
    var online = navigator.onLine;

    if (online !== true) {
        AbrirModal("Erro de conexão");
        return false;
    }
    ShowLoading();
    var cont = 0;
    $("#form_filtro input").each(function () {
        if ($(this).val() !== "") {
            cont++;
        }
    });

    $("#form_filtro select").each(function () {
        if ($(this).val() !== "") {
            cont++;
        }
    });

    if (cont === 0) {
        HideLoading();
        AbrirModal("Informe ao menos um campo para filtro!");
        return false;
    } else {
        var msg = "";
        var data1;
        var data2;

        var dtInicial = $("#DataCadastramentoDe").val();
        var dtFinal = $("#DataCadastramentoAte").val();

        if ($("#DataCadastramentoDe").length > 0) {
            var dia;
            var mes;
            var ano;
            if (dtInicial.length > 0 && isValidDate(dtInicial) === false) {
                msg = "A data inicial é invalida!";
            } else {

                dia = dtInicial.substr(0, 2);
                mes = dtInicial.substr(3, 2);
                ano = dtInicial.substr(6, 4);

                data1 = new Date(mes + "/" + dia + "/" + ano);
            }

            if (dtFinal.length > 0 && isValidDate(dtFinal) === false) {
                msg = "A data final é invalida!";
            } else {
                dia = dtFinal.substr(0, 2);
                mes = dtFinal.substr(3, 2);
                ano = dtFinal.substr(6, 4);

                data2 = new Date(mes + "/" + dia + "/" + ano);
            }

            if (data1 && data2 && isValidDate(dtFinal) && isValidDate(dtInicial))
                if (data1.getTime() > data2.getTime() && dtFinal.length > 0) {
                    msg = "A data De deve ser menor ou igual a data Até!";
                }
        }

        if ($("#DataCadastramentoAte").length > 0) {
            if (dtFinal.length > 0 && isValidDate(dtFinal) === false) {
                msg = "A data final é invalida!";
            }
        }

        if (msg !== "") {
            HideLoading();
            AbrirModal(msg);
            return false;
        }
    }

    $("#form_filtro").submit();
}

function ValidaFormularioCadastrar() {
    var online = navigator.onLine;
    var msg;

    if (online !== true) {
        AbrirModal("Erro de conexão");
        return false;
    }
    waitingDialog.show('Transmitindo');
    var cont = 0;
    $("#form_impressao_relacao_re_rt input").each(function () {
        if ($(this).val() !== "") {
            cont++;
        }
    });

    if (cont === 0) {
        waitingDialog.hide();
        AbrirModal("Campos obrigatórios não preenchidos!");
        return false;
    } else {
        var codigoUnidadeGestora = $('#form_impressao_relacao_re_rt input[id="CodigoUnidadeGestora"]').val();
        var codigoGestao = $('#form_impressao_relacao_re_rt input[id="CodigoGestao"]').val();

        if (codigoUnidadeGestora.length === 0) {
            msg = "O código unidade gestora é de preenchimento obrigatório!";
        }

        if (codigoGestao.length === 0) {
            msg = "O código gestão é de preenchimento obrigatório!";
        }

        if (msg !== "" && msg !== undefined) {
            waitingDialog.hide();
            AbrirModal(msg);
            return false;
        }
    }

    return true;
}

var ImpressaoRelacaoRERT = (function () {

    /****************************************************/
    /********************** Grid ************************/
    /****************************************************/
    var Grid = {};
    Grid.RegistroSelecionado = {};

    function initGrid() {
        Grid.Bind();
        setGlobalDataTables();
    }

    Grid.ItemPorLinha =  function(linha) {        
        return {
            Id: linha.find('input[name*="id"]').val(),
            CodigoAgrupamento: linha.find('input[name*="codigoAgrupamento"]').val(),
            CodigoUnidadeGestora: linha.find('input[name*="codigoUnidadeGestora"]').val(),
            CodigoGestao: linha.find('input[name*="codigoGestao"]').val(),
            CodigoBanco: linha.find('input[name*="codigoBanco"]').val(),
            DataSolicitacao: linha.find('input[name*="dataSolicitacao"]').val(),
            CodigoRE: linha.find('input[name*="codigoRE"]').val(),
            CodigoRT: linha.find('input[name*="codigoRT"]').val(),
            FlagCancelamento: linha.find('input[name*="flagCancelamento"]').val()
        };
    };

    Grid.Bind = function () {
        Grid.BindCancelar();
        Grid.BindExcluir();
        Grid.BindTransmitir();
        Grid.BindImprimir();
        Grid.BindImprimirAgrupamento();
        Grid.BindImprimirVisualizar();
        Grid.Datatables.BindEvents();

        $('[data-toggle="popover"]').popover();
        $('[data-toggle="tooltip"]').tooltip();
    };

    Grid.BindCancelar = function () {
        $('body').off('click', 'button[data-button="Cancelar"]:not(.disabled)');
        $('body').on('click', 'button[data-button="Cancelar"]:not(.disabled)', function () {
            var linha = $(this).parent().parent();
            Grid.RegistroSelecionado = Grid.ItemPorLinha(linha);
            console.log(Grid);

            var codigoRelacao, PrefixoREouRT, NumREouRT;

            if (Grid.RegistroSelecionado.CodigoRE !== '') {
                codigoRelacao = Grid.RegistroSelecionado.CodigoRE;
                PrefixoREouRT = Grid.RegistroSelecionado.CodigoRE.substr(4,2);
                NumREouRT = Grid.RegistroSelecionado.CodigoRE.substr(6,5);
            } else {
                codigoRelacao = Grid.RegistroSelecionado.CodigoRT;
                PrefixoREouRT = Grid.RegistroSelecionado.CodigoRT.substr(4,2);
                NumREouRT = Grid.RegistroSelecionado.CodigoRT.substr(6,5);
            }

            $('#_modalCancelarImpressaoRelacaoReRt input[name="unidadeGestora-text"]').val(Grid.RegistroSelecionado.CodigoUnidadeGestora);
            $('#_modalCancelarImpressaoRelacaoReRt input[name="gestao-text"]').val(Grid.RegistroSelecionado.CodigoGestao);
            $('#_modalCancelarImpressaoRelacaoReRt input[name="numeroRelacaoReRt-text"]').val(codigoRelacao);

            $('#_modalCancelarImpressaoRelacaoReRt input[name="unidadeGestora"]').val(parseInt(Grid.RegistroSelecionado.CodigoUnidadeGestora));
            $('#_modalCancelarImpressaoRelacaoReRt input[name="gestao"]').val(parseInt(Grid.RegistroSelecionado.CodigoGestao));
            $('#_modalCancelarImpressaoRelacaoReRt input[name="numeroRelacaoReRt"]').val(codigoRelacao);

            $('#_modalCancelarImpressaoRelacaoReRt').modal('show');

            $('#_modalCancelarImpressaoRelacaoReRt button#btn-transmitir-impressaoRelacaoReRt').off('click');
            $('#_modalCancelarImpressaoRelacaoReRt button#btn-transmitir-impressaoRelacaoReRt').on('click', function () {
                
                $.ajax({
                    type: "POST",
                    url: "/PagamentoContaUnica/ImpressaoRelacaoRERT/CancelarImpressaoRelacaoReRt",
                    data: {
                        'id': Grid.RegistroSelecionado.Id,
                        'unidadeGestora': Grid.RegistroSelecionado.CodigoUnidadeGestora, 
                        'gestao': Grid.RegistroSelecionado.CodigoGestao,
                        'prefixoREouRT': PrefixoREouRT,
                        'numREouRT': NumREouRT,
                        'flagCancelamento': Grid.RegistroSelecionado.FlagCancelamento
                    },
                    beforeSend: function (hrx) {
                        waitingDialog.show('Transmitindo');
                    }
                }).done(function (xhrResponse) {
                    console.log("done!");
                    $.confirm({
                        text: "Impressão Relação " + codigoRelacao + " cancelada com sucesso",
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
                    $('#_modalCancelarImpressaoRelacaoReRt').modal('hide');
                    waitingDialog.hide();
                });

            });

            return false;
        });
    };

    Grid.BindExcluir = function () {
        $('body').off('click', 'button[data-button="Excluir"]:not(.disabled)');
        $('body').on('click', 'button[data-button="Excluir"]:not(.disabled)', function () {
            var linha = $(this).parent().parent();
            Grid.RegistroSelecionado = Grid.ItemPorLinha(linha);
            console.log(Grid);

            //$('#_modalExclusaoImpressaoRelacaoReRt input[name="codigoReRt-text"]').val(codigoRelacao);
            //$('#_modalExclusaoImpressaoRelacaoReRt input[name="codigoReRt"]').val(codigoRelacao);

            $('#_modalExclusaoImpressaoRelacaoReRt').modal('show');

            $('#_modalExclusaoImpressaoRelacaoReRt button#btn-excluir-impressaoRelacaoReRt').off('click');
            $('#_modalExclusaoImpressaoRelacaoReRt button#btn-excluir-impressaoRelacaoReRt').on('click', function () {

                //if (!ValidarExcluirImpressaoRelacaoReRt()) return false;

                $.ajax({
                    type: "POST",
                    url: "/PagamentoContaUnica/ImpressaoRelacaoRERT/Delete",
                    data: {
                        'id': Grid.RegistroSelecionado.Id
                    },
                    beforeSend: function (hrx) {
                        waitingDialog.show('Transmitindo');
                    }
                }).done(function (xhrResponse) {
                    console.log("done!");
                    $.confirm({
                        text: "Impressão Relação RE RT <b>" + Grid.RegistroSelecionado.Id + "</b> excluída com sucesso",
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
                    $('#_modalExclusaoImpressaoRelacaoReRt').modal('hide');
                    waitingDialog.hide();
                });
            });

            return false;
        });
    };

    Grid.BindTransmitir = function () {
        $('body').off('click', 'button[data-button="Transmitir"]:not(.disabled)');
        $('body').on('click', 'button[data-button="Transmitir"]:not(.disabled)', function (e) {
            e.stopPropagation();
            var linha = $(this).parent().parent();

            var validaCampos = ValidaFormularioCadastrar();

            if (validaCampos) {
                Grid.RegistroSelecionado = Grid.ItemPorLinha(linha);
                console.log(Grid);

                var codigoUnidadeGestora = $('#form_impressao_relacao_re_rt input[id="CodigoUnidadeGestora"]').val();
                var codigoGestao = $('#form_impressao_relacao_re_rt input[id="CodigoGestao"]').val();
                var codigoBanco = $('#form_impressao_relacao_re_rt input[id="CodigoBanco"]').val();
                var dataSolicitacao = "";
                var numeroRelatorio = "";
                var mantemAgrupamento = $('#form_impressao_relacao_re_rt input[name="MantemAgrupamento"]').val();

                $.ajax({
                    type: "POST",
                    url: "/PagamentoContaUnica/ImpressaoRelacaoRERT/Transmitir",
                    data: {
                        'unidadeGestora': codigoUnidadeGestora,
                        'gestao': codigoGestao,
                        'banco': codigoBanco,
                        'dataSolicitacao': dataSolicitacao,
                        'numeroRelatorio': numeroRelatorio,
                        'mantemAgrupamento': mantemAgrupamento
                    },
                    content: 'application/x-www-form-urlencoded; charset=UTF-8',
                    beforeSend: function (hrx) {
                        waitingDialog.show('Consultando Siafem...');
                    },
                    success: function (dados) {
                        var Mensagem = dados.Mensagem;
                        var Titulo = dados.Titulo;
                        var Agrupamento = dados.Agrupamento;
                        var UnidadeGestora = dados.UnidadeGestora;
                        var Gestao = dados.Gestao;
                        var Banco = dados.Banco;

                        if (dados.Status === 'Sucesso' && Titulo === "Geração RE/RT") {
                            $.confirm({
                                text: Mensagem,
                                title: Titulo,
                                confirm: function (button) {
                                    $('#form_impressao_relacao_re_rt input[name="MantemAgrupamento"]').val(Agrupamento);
                                    $('button[data-button="Transmitir"]').click();
                                },
                                cancel: function (button) {
                                    $('#form_impressao_relacao_re_rt input[name="MantemAgrupamento"]').val(Agrupamento);
                                    waitingDialog.hide();
                                    CarregarReRt(Agrupamento, UnidadeGestora, Gestao, Banco);
                                },
                                confirmButton: "Sim",
                                cancelButton: "Não",
                                post: true,
                                confirmButtonClass: "btn-danger",
                                cancelButtonClass: "btn-default",
                                dialogClass: "modal-dialog modal-sm"
                            });
                        }
                        else if (Mensagem === "Não existe documento pendente para emissão de RE e RT" || Titulo === "Erro") {
                            $.confirm({
                                text: Mensagem,
                                title: Titulo,
                                cancel: function (button) {
                                },
                                confirmButton: "",
                                cancelButton: "Fechar",
                                post: true,
                                confirmButtonClass: "",
                                cancelButtonClass: "btn-default",
                                dialogClass: "modal-dialog modal-sm"
                            });
                        }
                        else {
                            $.confirm({
                                text: Mensagem,
                                title: Titulo,
                                cancel: function (button) {
                                    $('#form_impressao_relacao_re_rt input[name="MantemAgrupamento"]').val(Agrupamento);
                                    waitingDialog.hide();
                                    CarregarReRt(Agrupamento, UnidadeGestora, Gestao, Banco);
                                },
                                confirmButton: "",
                                cancelButton: "Fechar",
                                post: true,
                                confirmButtonClass: "",
                                cancelButtonClass: "btn-default",
                                dialogClass: "modal-dialog modal-sm"
                            });
                        }
                    },
                    error: function (result) {
                        abrirmodal(result.statustext, function () { });
                        waitingdialog.hide();
                    },
                    complete: function (result) {
                        waitingDialog.hide();
                    }
                });
            }

            return false;
        });
    };

    Grid.BindImprimir = function () {
        $('body').off('click', 'button[data-button="Imprimir"]:not(.disabled)');
        $('body').on('click', 'button[data-button="Imprimir"]:not(.disabled)', function (e) {
            e.stopPropagation();
            var linha = $(this).parent().parent();
            Grid.RegistroSelecionado = Grid.ItemPorLinha(linha);
            console.log(Grid);

            $('#_modalImprimirImpressaoRelacaoReRt').modal('show');
            $('#_modalImprimirImpressaoRelacaoReRt button[data-button="btnImprimirConfirmar"]').off('click');
            $('#_modalImprimirImpressaoRelacaoReRt button[data-button="btnImprimirConfirmar"]').on('click', function () {

                var numeroRelatorio, tipoImpressao;
                var res = [];

                if (Grid.RegistroSelecionado.CodigoRE !== '') {
                    numeroRelatorio = Grid.RegistroSelecionado.CodigoRE;
                } else {
                    numeroRelatorio = Grid.RegistroSelecionado.CodigoRT;
                }

                tipoImpressao = $('#_modalImprimirImpressaoRelacaoReRt input[name="tipo"]:checked').val();

                // Agrupamento pelo FiltroGrid
                if (tipoImpressao === "2") {
                    $.ajax({
                        url: '/PagamentoContaUnica/ImpressaoRelacaoRERT/ListarReRtPorAgrupamento/?codigoAgrupamento=' + Grid.RegistroSelecionado.CodigoAgrupamento,
                        async: false
                    }).done(function (response) {
                        res = response;
                    });

                    $('#_modalImprimirImpressaoRelacaoReRt').modal('hide');

                    for (var i = 0; i < res.length; i++) {
                        var paramsAgrupamento = jQuery.param({
                            codigoUnidadeGestora: Grid.RegistroSelecionado.CodigoUnidadeGestora,
                            codigoAgrupamento: Grid.RegistroSelecionado.CodigoAgrupamento,
                            codigoGestao: Grid.RegistroSelecionado.CodigoGestao,
                            codigoBanco: "",
                            dataSolicitacao: "",
                            numeroRelatorio: res[i],
                            filetype: 'PDF',
                            contenttype: 'application/pdf'
                        });
                        var newRel = window.open("/PagamentoContaUnica/ImpressaoRelacaoRERT/ImprimirImpressaoRelacao/?" + paramsAgrupamento, "_blank");
                        newRel.blur();
                    }
                }
                else {
                    var params = jQuery.param({
                        codigoUnidadeGestora: Grid.RegistroSelecionado.CodigoUnidadeGestora,
                        codigoGestao: Grid.RegistroSelecionado.CodigoGestao,
                        codigoBanco: "",
                        dataSolicitacao: "",
                        numeroRelatorio: numeroRelatorio,
                        filetype: 'PDF',
                        contenttype: 'application/pdf'
                    });

                    window.open("/PagamentoContaUnica/ImpressaoRelacaoRERT/ImprimirImpressaoRelacao/?" + params, "_blank");
                    $('#_modalImprimirImpressaoRelacaoReRt').modal('hide');
                }
            });

            return false;
        });
    };

    Grid.BindImprimirAgrupamento = function () {
        $('body').off('click', 'button[data-button="ImprimirAgrupamento"]:not(.disabled)');
        $('body').on('click', 'button[data-button="ImprimirAgrupamento"]:not(.disabled)', function (e) {
            e.stopPropagation();
            var linha = $(this).parent().parent();
            Grid.RegistroSelecionado = Grid.ItemPorLinha(linha);
            console.log(Grid);

            var codigoUnidadeGestora = $('#form_impressao_relacao_re_rt input[id="CodigoUnidadeGestora"]').val();
            var codigoAgrupamento = $('#form_impressao_relacao_re_rt input[id="MantemAgrupamento"]').val();
            var codigoGestao = $('#form_impressao_relacao_re_rt input[id="CodigoGestao"]').val();
            var res = [];

            $.ajax({
                url: '/PagamentoContaUnica/ImpressaoRelacaoRERT/ListarReRtPorAgrupamento/?codigoAgrupamento=' + codigoAgrupamento,
                async: false
            }).done(function (response) {
                res = response;               
            });

            for (var i = 0; i < res.length; i++) {
                var paramsAgrupamento = jQuery.param({
                    codigoUnidadeGestora: codigoUnidadeGestora,
                    codigoAgrupamento: codigoAgrupamento,
                    codigoGestao: codigoGestao,
                    codigoBanco: "",
                    dataSolicitacao: "",
                    numeroRelatorio: res[i],
                    filetype: 'PDF',
                    contenttype: 'application/pdf'
                });
                window.open("/PagamentoContaUnica/ImpressaoRelacaoRERT/ImprimirImpressaoRelacao/?" + paramsAgrupamento, "_blank");
            }

            return false;
        });
    };

    Grid.BindImprimirVisualizar = function () {
        $('body').off('click', 'button[data-button="ImprimirVisualizar"]:not(.disabled)');
        $('body').on('click', 'button[data-button="ImprimirVisualizar"]:not(.disabled)', function (e) {

            var codigoUnidadeGestora = $('#form_impressao_relacao_re_rt input[id="CodigoUnidadeGestora"]').val();
            var codigoAgrupamento = $('#form_impressao_relacao_re_rt input[id="MantemAgrupamento"]').val();
            var codigoGestao = $('#form_impressao_relacao_re_rt input[id="CodigoGestao"]').val();
            var numeroRE = $('#form_impressao_relacao_re_rt input[id="NumeroRE"]').val();
            var numeroRT = $('#form_impressao_relacao_re_rt input[id="NumeroRT"]').val();

            var numeroRelatorio;

            if (numeroRE !== '') {
                numeroRelatorio = numeroRE;
            } else {
                numeroRelatorio = numeroRT;
            }

            var params = jQuery.param({
                codigoUnidadeGestora: codigoUnidadeGestora,
                codigoGestao: codigoGestao,
                codigoBanco: "",
                dataSolicitacao: "",
                numeroRelatorio: numeroRelatorio,
                filetype: 'PDF',
                contenttype: 'application/pdf'
            });

            window.open("/PagamentoContaUnica/ImpressaoRelacaoRERT/ImprimirImpressaoRelacao/?" + params, "_blank");

            return false;
        });
    };

    function CarregarReRt(Agrupamento, UnidadeGestora, Gestao, Banco) {
        ShowLoading();
        window.location.href = "/PagamentoContaUnica/ImpressaoRelacaoRERT/CreateListar?agrupamento=" + Agrupamento + "&ug=" + UnidadeGestora + "&gestao=" + Gestao + "&banco=" + Banco;
        return false;
    }

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

    var FillGrid = function (html) {
        $('div#grid').html(html);
        $('#tblGrid', 'div#grid').dataTable({
            "pagingType": "full_numbers",
            "pageLength": -1,
            "orderable": false
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
        InitGrid: function () {
            initGrid();
        }
    };
}());

$(function () {
    if ($("._tbDataTables")) {
        ImpressaoRelacaoRERT.InitGrid();
    }
});