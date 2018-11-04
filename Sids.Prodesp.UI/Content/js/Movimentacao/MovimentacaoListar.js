var obj = "Movimentacao";
var modalEstornar;
var modalImprimir;
var entity;

var intervalRetransmitir = false;

$(document).on('ready', function () {
    $('#btnReTransmitir').click(function () {
        tans = true;
        ObterId(controller);
    });

    $('#idSelecionar').click(function () {
        MarcaTodos();
    });

    // Desabilita assim que é carregado a model Imprimir
    document.getElementById("nl").disabled = true;
    document.getElementById("nc").disabled = true;

    bindEventos();
});

function HabilitarOpcaoImpressao() {
    document.getElementById("nl").disabled = true;
    document.getElementById("nc").disabled = true;
    document.getElementById("nl").checked = false;
    document.getElementById("nc").checked = false;
}

function DesabilitarOpcaoImpressao() {
    document.getElementById("nl").disabled = false;
    document.getElementById("nc").disabled = false;
}

function bindEventos() {
    $('body').on('click', '#btnEstornar', Movimentacao.Estornar);
}

function ImprimirOp(id) {

    if (navigator.onLine !== true) {
        AbrirModal("Erro de conexão");
        return false;
    }

    $.ajax({
        datatype: 'JSON',
        type: 'POST',
        url: "/PagamentoContaDer/ArquivoRemessa/ConsultaImpressaoOpApoio",
        data: JSON.stringify({ id: id }),
        contentType: "application/json; charset=utf-8",
        beforeSend: function () {
            waitingDialog.show('Consultando');
        },
        success: function (dados) {
            if (dados.Status === 'Sucesso') {
                modalImprimir.remove();
                $("body").append(modalImprimir);
                modalImprimir.modal('show');


                $("#CodAssAutorizado").change(function BuscarAssinatura() {
                    var valor = $("div >#CodAssAutorizado").val();

                    tipo = 1;
                    LimparCombo(tipo);

                    if (valor.length !== 0 & valor.length === 5 & valor !== "") {
                        ConsultarAssinatura(valor, tipo);
                    }
                });

                $("#CodAssExaminado").change(function BuscarAssinatura() {
                    var valor = $("div >#CodAssExaminado").val();
                    tipo = 2;
                    LimparCombo(tipo);
                    if (valor.length !== 0 & valor.length === 5 & valor !== "") {
                        ConsultarAssinatura(valor, tipo);
                    }
                });



                entity = dados.objModel;
                $("#DataPreparacao").val(ConvertDateCSharp(entity.DataTransmitido));

                $("#CodAssAutorizado").val(entity.CodigoAssinatura);

                $("#txtAutorizadoGrupo").val(entity.CodigoGrupoAssinatura);
                $("#txtAutorizadoOrgao").val(entity.CodigoOrgaoAssinatura);
                $("#txtAutorizadoNome").val(entity.NomeAssinatura);
                $("#txtAutorizadoCargo").val(entity.DesCargo);

                $("#CodAssExaminado").val(entity.CodigoContraAssinatura);

                $("#txtExaminadoGrupo").val(entity.CodigoContraGrupoAssinatura);
                $("#txtExaminadoOrgao").val(entity.CodigoContraOrgaoAssinatura);
                $("#txtExaminadoNome").val(entity.NomeContraAssinatura);
                $("#txtExaminadoCargo").val(entity.DesContraCargo);

                $("#CodConta").val(entity.CodigoConta);
                $("#NumeroConta").val(entity.NumeroConta);
                $("#NumGeracao").val(entity.NumeroGeracao);
                $("#QtOpArquivo").val(entity.QtOpArquivo);
                $("#ValorTotal").val(entity.ValorTotal);
                //AtualizarFormulario(dados.objModel);
            }
            else {
                AbrirModal(dados.Msg);
            }
        },
        error: function (dados) {
            waitingDialog.hide();
            AbrirModal(dados);
        },
        complete: function () {
            waitingDialog.hide();
        }
    });
}


function ImprimirOP() {

    if (navigator.onLine !== true) {
        AbrirModal("Erro de conexão");
        return false;
    }


    entity.DataPreparacao = $("#DataPreparacao").val();

    entity.CodigoAssinatura= $("#CodAssAutorizado").val();

    entity.CodigoGrupoAssinatura= $("#txtAutorizadoGrupo").val();
    entity.CodigoOrgaoAssinatura = $("#txtAutorizadoOrgao").val();
    entity.NomeAssinatura= $("#txtAutorizadoNome").val();
    entity.DesCargo=$("#txtAutorizadoCargo").val();

    entity.CodigoContraAssinatura=$("#CodAssExaminado").val();

    entity.CodigoContraGrupoAssinatura=$("#txtExaminadoGrupo").val();
    entity.CodigoContraOrgaoAssinatura=$("#txtExaminadoOrgao").val();
    entity.NomeContraAssinatura=$("#txtExaminadoNome").val();
    entity.DesContraCargo=$("#txtExaminadoCargo").val();

    entity.CodigoConta =$("#CodConta").val();
    entity.NumeroConta =$("#NumeroConta").val();
    entity.NumeroGeracao = $("#NumGeracao").val();
    entity.QtOpArquivo= $("#QtOpArquivo").val();
    entity.ValorTotal = $("#ValorTotal").val();
    entity.SelArquivo = $("#selArquivo").val();



    $.ajax({
        datatype: 'JSON',
        type: 'POST',
        url: "/PagamentoContaDer/ArquivoRemessa/ImprimirOP",
        data: JSON.stringify({ arquivoRemessa: entity }),
        contentType: "application/json; charset=utf-8",
        beforeSend: function () {
            waitingDialog.show('Imprimindo');
        },
        success: function (dados) {
            if (dados.Status === 'Sucesso') {
                $.confirm({
                    text: "Operação Concluída",
                    title: "Confirmação",
                    cancel: function () {
                        window.location.reload();
                    },
                    cancelButton: "Ok",
                    confirmButton: "",
                    post: true,
                    cancelButtonClass: "btn-default",
                    modalOptionsBackdrop: true
                });
            }
            else {
                modalImprimir.modal('hide');
                AbrirModal(dados.Msg);
            }
        },
        error: function (dados) {
            waitingDialog.hide();
            modalImprimir.modal('hide');
            AbrirModal(dados);
        },
        complete: function () {
            waitingDialog.hide();
        }
    });
}

function ExcluirListaPd(id, editar, agrupamentoId, unica) {


    if (navigator.onLine !== true) {
        AbrirModal("Erro de conexão");
        return false;
    }

    if (editar) {
        AbrirModal("Não é permitido excluir lista de PD, existem PD’s já transmitidas");
        return false;
    }

    var status;
    $.ajax({
        type: "Post",
        url: urld,
        cache: false,
        async: false,
        data: JSON.stringify({ Id: id, agrupamentoId: agrupamentoId, unica: unica }),
        contentType: "application/json; charset=utf-8",
        success: function (dados) {
            if (dados === "Sucesso") {
                HideLoading();
                $.confirm({
                    text: "Movimentação Orçamentária excluída com sucesso",
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
            } else {
                HideLoading();
                AbrirModal(dados);
                status = false;
                return false;

            }
        },
        error: function (dados, exception) {
            ErrorAjax(dados, exception);
        }
    });
    return status;
}

function CancelarOp() {
    var entity = {
        Id: $("#CanOPId").val()
    }

    $.ajax({
        datatype: 'JSON',
        type: 'POST',
        url: "/PagamentoContaDer/ArquivoRemessa/TransmitirCancelamentoOp",
        data: JSON.stringify({ arquivoremessa: entity }),
        contentType: "application/json; charset=utf-8",
        beforeSend: function () {
            waitingDialog.show('Consultando');
        },
        success: function (dados) {
            if (dados.Status === 'Sucesso') {
                $.confirm({
                    text: "Operação realizada com sucesso",
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
            }
            else {
                AbrirModal(dados.Msg);
            }
        },
        error: function (dados) {
            AbrirModal(dados);
        },
        complete: function () {
            FecharModal("#modalCancelarArquivo");
            modalCancelarArquivo.remove();
            waitingDialog.hide();
        }
    });
}

var Movimentacao = (function () {
    var Grid = {};
    Grid.RegistroSelecionado = {};

    function initGrid() {
        Grid.Bind();
        setGlobalDataTables();

        $('#tblPesquisa').DataTable().order(8, 'asc').draw();
    }

    Grid.ItemPorLinha = function (linha) {
        return {
            Id: linha.find('input[name*="Id"]').val(),
            IdMovimentacao: linha.find('input[name*="IdMovimentacao"]').val(),
            DescDocumento: linha.find('input[name*="DescDocumento"]').val(),
            NumSequencia: linha.find('input[name*="NumSequencia"]').val(),
            NumDocumento: linha.find('input[name*="NumDocumento"]').val(),
            NumAgrupamento: linha.find('input[name*="NumAgrupamento"]').val(),
            IdTransmitir: linha.find('input[name*="Transmitir"]').val(),
            IdFuncionalidadeAtual: linha.find('input[name*="IdFuncionalidadeAtual"]').val()
        };
    };

    Grid.Bind = function () {
        Grid.BindImprimir();
        Grid.BindEstornar();
        Grid.BindExcluir();
        Grid.BindTransmitirSelecionarTodos();
        Grid.BindRetransmitir();

        $('[data-toggle="popover"]').popover();
        $('[data-toggle="tooltip"]').tooltip();
    };

    Grid.BindExcluir = function () {
        $('body').off('click', 'button[data-button="Excluir"]:not(.disabled)');
        $('body').on('click', 'button[data-button="Excluir"]:not(.disabled)', function (e) {
            e.stopPropagation();
            var linha = $(this).parent().parent();
            Grid.RegistroSelecionado = Grid.ItemPorLinha(linha);
            var id = $(this).data("id-mov");

            if (navigator.onLine !== true) {
                AbrirModal("Erro de conexão");
                return false;
            }

            $.confirm({
                text: "Deseja excluir todo o agrupamento da Movimentação Orçamentária?",
                title: "Confirmação",
                confirm: function () {
                    $.ajax({
                        type: "POST",
                        url: '/Movimentacao/Movimentacao/Delete',
                        data: { id: id },
                        content: 'application/x-www-form-urlencoded; charset=UTF-8',
                        beforeSend: function (hrx) {
                            waitingDialog.show('Excluindo...');
                        }
                    }).done(function (xhrResponse) {
                        if (xhrResponse === "Sucesso") {
                            //window.open("/Movimentacao/Movimentacao/Index/" + Grid.RegistroSelecionado.IdFuncionalidadeAtual, "_parent");
                            $("#btnPesquisar").click();
                        }
                    }).fail(function (jqXHR, textStatus) {
                        AbrirModal(jqXHR.statusText, function () { });
                    }).always(function () {
                        waitingDialog.hide();
                    });
                },
                cancel: function () {
                },
                cancelButton: "Não",
                confirmButton: "Sim",
                post: true,
                confirmButtonClass: "btn-danger",
                cancelButtonClass: "btn-default",
                dialogClass: "modal-dialog modal-sm",
                modalOptionsBackdrop: true
            });

            

            return false;
        });
    }

    Grid.BindImprimir = function () {
        $('body').off('click', 'button[data-button="Imprimir"]:not(.disabled)');
        $('body').on('click', 'button[data-button="Imprimir"]:not(.disabled)', function (e) {
            e.stopPropagation();
            var linha = $(this).parent().parent();
            Grid.RegistroSelecionado = Grid.ItemPorLinha(linha);
            console.log(Grid);

            $('#_modalImprimir').modal('show');
            $('#_modalImprimir button[data-button="btnImprimirConfirmar"]').off('click');
            $('#_modalImprimir button[data-button="btnImprimirConfirmar"]').on('click', function () {

                var opcaoImpressao = $('#_modalImprimir input[name="tipo"]:checked').val();
                var nc = $('#_modalImprimir input[name="nc"]:checked').val();
                var nl = $('#_modalImprimir input[name="nl"]:checked').val();
                var opcaoAgrupamento;

                if (nc === "NC" && nl === "NL") { // NL e NC
                    opcaoAgrupamento = 3;
                } else if (nc === "NC" && nl === undefined) { // NC
                    opcaoAgrupamento = 2;
                } else { // NL
                    opcaoAgrupamento = 1;
                }

                if ((nc === "NC" || nl === "NL" || (nc === "NC" && nl === "NL")) || opcaoImpressao === "1") {
                    $.ajax({
                        type: "POST",
                        url: "/Movimentacao/Movimentacao/ImprimirMov",
                        data: {
                            "id": Grid.RegistroSelecionado.Id,
                            "idMovimentacao": Grid.RegistroSelecionado.IdMovimentacao,
                            "tipoDocumento": Grid.RegistroSelecionado.DescDocumento,
                            "numSequencia": Grid.RegistroSelecionado.NumSequencia,
                            "numAgrupamento": Grid.RegistroSelecionado.NumAgrupamento,
                            "numDocumento": Grid.RegistroSelecionado.NumDocumento,
                            "opcaoImpressao": opcaoImpressao,
                            "opcaoAgrupamento": opcaoAgrupamento
                        },
                        content: 'application/x-www-form-urlencoded; charset=UTF-8',
                        beforeSend: function (hrx) {
                            waitingDialog.show('Transmitindo Siafem...');
                        }
                    }).done(function (xhrResponse) {
                        $("#frm-imprimir-movimentacao").submit();
                    }).fail(function (jqXHR, textStatus) {
                        AbrirModal(jqXHR.statusText, function () { });
                    }).always(function () {
                        $('#_modalImprimir').modal('hide');
                        waitingDialog.hide();
                    });
                } else {
                    $.confirm({
                        text: "Favor selecionar uma das opções de agrupamento.",
                        title: "Confirmação",
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
            });

            return false;
        });
    }

    Grid.BindEstornar = function () {
        $('body').off('click', 'button[data-button="Estorno"]:not(.disabled)');
        $('body').on('click', 'button[data-button="Estorno"]:not(.disabled)', function (e) {
            e.stopPropagation();
            var linha = $(this).parent().parent();
            Grid.RegistroSelecionado = Grid.ItemPorLinha(linha);
            console.log(Grid);

            $('#modalEstorno').modal('show');
            $('#modalEstorno button[data-button="btnEstornar"]').off('click');
            $('#modalEstorno button[data-button="btnEstornar"]').on('click', function () { 

                var opcao = $('#modalEstorno input[name="estornar"]:checked').val();
                var id = Grid.RegistroSelecionado.Id;
                var idMovimentacao = Grid.RegistroSelecionado.IdMovimentacao;
                var descDocumento = Grid.RegistroSelecionado.DescDocumento;

                window.location = '/Movimentacao/Movimentacao/Estornar?idMovimentacao=' + idMovimentacao + '&id=' + id + '&descDocumento=' + descDocumento + '&opcao=' + opcao;

            });
            return false;
        });
    }

    Grid.BindTransmitirSelecionarTodos = function () {
        $("table#tblPesquisa thead tr th input[name='nmRetransmitirTodos']").off('click');
        $("table#tblPesquisa thead tr th input[name='nmRetransmitirTodos']").on('click', function () {
            var checked = $(this).is(':checked');
            $('table#tblPesquisa tbody tr td input[type="checkbox"]').each(function () {
                $(this).prop('checked', checked);
                $(this).trigger('change');
            });

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
                items.push(Grid.RegistroSelecionado.IdTransmitir);
            }

            //$.ajax({
            //    type: "POST",
            //    url: "/PagamentoContaUnica/AutorizacaoDeOB/Retransmitir",
            //    data: { 'ListaDeOB': items, 'filtroMudapah': Grid.RegistroSelecionado.UGPagadora },
            //    beforeSend: function (hrx) {
            //        waitingDialog.show('Retrasmitindo itens selecionados...');
            //    }
            //}).done(function (xhrResponse) {
            //    $('#btnPesquisar').trigger('click');
            //    console.log("done!");
            //}).fail(function (jqXHR, textStatus) {
            //    AbrirModal(jqXHR.statusText, function () { });
            //}).always(function () {
            //    waitingDialog.hide();
            //});

        });
    };

    return {
        Init: function () {
            init();
        },
        InitGrid: function () {
            initGrid();
        },
    };
}());

$(function () {
    if ($("._tbDataTables")) {
        Movimentacao.InitGrid();
    }
});