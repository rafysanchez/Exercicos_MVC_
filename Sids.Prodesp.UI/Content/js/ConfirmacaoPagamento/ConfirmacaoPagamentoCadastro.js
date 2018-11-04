var CONFIRMACAOPAGAMENTOCADASTRO = (function () {

    function init() {
        oldCode();
        bindAll();
    }

    function bindAll() {
        bindBtnConsultarPagamentosConfirmar();
        bindTipoDocumento();
    }

    

    function bindBtnConsultarPagamentosConfirmar() {
        
        $('#divPainelCadastrarConfirmacaoPagamento').on('click', '#btnConsultarPagamentosConfirmar', function () {            
            waitingDialog.show('Consultando');

            $('#divResultadosBusca').html('');
            var data = $(this).closest('.dadosBuscaPagamentosConfirmar').find(':input').serialize();
            $.ajax({
                type: 'POST',
                url: "/PagamentoContaDer/ConfirmacaoPagamento/ListarPagamentosConfirmar",
                cache: false,
                async: true,
                data: data,
                beforeSend: function (hrx) {
                    waitingDialog.show('Consultando');
                }
            }).done(function (jqXHR) {
                $('#divResultadosBusca').html(jqXHR.Html);
            }).fail(function (jqXHR, textStatus) {
                AbrirModal(jqXHR.statusText, function () { });
            }).always(function () {
                waitingDialog.hide();
            });
        });
    }

    function bindTipoDocumento() {
        $('#divFiltroPagamentosConfirmar').on('change', '#IdTipoDocumento', function () {
            var opcao = $(this).val();
            if (opcao == '5') {
                $('#NumeroDocumento').removeClass('maskRap').addClass('maskSubempenho');
            }
            else if (opcao == '11') {
                $('#NumeroDocumento').removeClass('maskSubempenho').addClass('maskRap');
            }
            else {
                $('#NumeroDocumento').removeClass('maskSubempenho').removeClass('maskRap');
            }
        })
    }

    function oldCode() {
        $('#select-all_Autorizacao').click(function (event) {
            if (this.checked) {
                // Iterate each checkbox
                $('.checkTodosAutorizacao').each(function () {
                    this.checked = true;
                });
            }
            else {
                $('.checkTodosAutorizacao').each(function () {
                    this.checked = false;
                });
            }
        });

        $(".checkTodosAutorizacao").on("change", function () {
            if (!$(this).is(":checked")) {
                $("#select-all_Autorizacao").prop('checked', false);
            }
            if ($(".checkTodosAutorizacao:checked").length == $(".checkTodosAutorizacao").length) {
                $("#select-all_Autorizacao").prop('checked', true);
            }
        });

        $("input:checkbox").on('click', function () {
            var $box = $(this);
            if ($box.is(":checked")) {
                var group = "input:checkbox[name='" + $box.attr("name") + "']";
                $(group).prop("checked", false);
                $box.prop("checked", true);
            } else {
                $box.prop("checked", false);
            }
        });

        $('.botao').click(function (e) {
            var isValid = true;
            $('select[required],input[required=required]').each(function () {
                if ($.trim($(this).val()) == '') {
                    isValid = false;
                    $('#modalAlerta').modal();
                    $('#modalAlerta #value').html('Campo obrigatório não preenchido');
                    $(this).addClass('campoVazio');
                }
                else {
                    $(this).removeClass('campoVazio');
                }
            });
            if (isValid == false) {
                e.preventDefault();
            }

            else {
                $('#modalAlerta').modal();
                $('#modalAlerta #value').html('Transmissão realizada com sucesso');
            }
        });

        $('select[name=opcoesConfirmacao]').change(function () {
            var opcao = $(this).val();
            if (opcao == '1') {
                $('.pagamentoPorLoteC').hide();
                $('.pagamentoPorDocumentoC').show();
            }

            else if (opcao == '2') {
                $('.pagamentoPorDocumentoC').hide();
                $('.pagamentoPorLoteC').show();
            }
            else {
                $('.pagamentoPorLoteC').hide();
                $('.pagamentoPorDocumentoC').hide();
            }

            $('#divFiltroPagamentosConfirmar').show();
        })

        $(document).ready(function () {
            $("input").blur(function () {
                if ($(this).val() == "") {
                    $(this).css({ "border": "1px solid #F00", "padding": "2px" });
                }
            });
            $("select").blur(function () {
                if ($(this).val() == "") {
                    $(this).css({ "border": "1px solid #F00", "padding": "2px" });
                }
            });
            $(".consulta1").click(function () {
                var cont = 0;
                $("#consultaPagamentoPorDocumento input").each(function () {
                    if ($(this).val() == "") {
                        $(this).css({ "border": "1px solid #F00", "padding": "2px" });
                        cont++;
                    }
                });
                $("#consultaPagamentoPorDocumento select").each(function () {
                    if ($(this).val() == "") {
                        $(this).css({ "border": "1px solid #F00", "padding": "2px" });
                        cont++;
                    }
                });
                if (cont == 0) {
                    $("#consultaPagamentoPorDocumento").submit();
                }
            });
            $(".consulta2").click(function () {
                var cont = 0;
                $("#consultaPagamentoPorLote input").each(function () {
                    if ($(this).val() == "") {
                        $(this).css({ "border": "1px solid #F00", "padding": "2px" });
                        cont++;
                    }
                });
                $("#consultaPagamentoPorLote select").each(function () {
                    if ($(this).val() == "") {
                        $(this).css({ "border": "1px solid #F00", "padding": "2px" });
                        cont++;
                    }
                });
                if (cont == 0) {
                    $("#consultaPagamentoPorLote").submit();
                }
            });
        });

        //botão transmitir Impressao de Relação de RE e RT
        $('#btnTransmitirImpressao').click(function (e) {
            var isValid = true;
            $('input.required').each(function () {
                if ($.trim($(this).val()) == '') {
                    isValid = false;
                    $('#modalAlerta').modal();
                    $('#modalAlerta #value').html('Campo obrigatório não preenchido');
                    $(this).addClass('campoVazio');
                }
                else {
                    $(this).removeClass('campoVazio');
                }
            });
            if (isValid == false) {
                e.preventDefault();
            }

            else {
                MostraModal('#modalTransmissaoImpressao');
            }
        });
    }

    function mostrarModal(id) {
        $(id).modal();
        return false;
    }

    function fecharModal(id) {
        $(id).modal('hide');
        return false;
    }

    return {
        Init: init,
        Modal: {
            Mostrar: mostrarModal,
            Fechar: fecharModal
        }
    }
}());

$(function () {
    CONFIRMACAOPAGAMENTOCADASTRO.Init();
})

$('button[data-button="btnSalvarConfirmacao"]:not(.disabled)').on('click', function () {
        waitingDialog.show('Salvando');

        $('#divResultadosBusca').html('');
        var data = $('.dadosBuscaPagamentosConfirmar').find(':input').serialize();
        var tipo = "Salvar";
        $.ajax({
            type: 'POST',
            url: "/PagamentoContaDer/ConfirmacaoPagamento/ListarPagamentosConfirmarSalvar",
            cache: false,
            async: true,
            data: data,
            beforeSend: function (hrx) {
                waitingDialog.show('Salvando');
            }
        }).done(function (jqXHR) {
            $('#divResultadosBusca').html(jqXHR.Status);
        }).fail(function (jqXHR, textStatus) {
            AbrirModal(jqXHR.statusText, function () { });
        }).always(function () {
            waitingDialog.hide();
        });
});


$('button[data-button="btnTransmitirProdesp"]:not(.disabled)').on('click', function () {
        waitingDialog.show('Transmitindo');

        $('#divResultadosBusca').html('');
        var data = $('.dadosBuscaPagamentosConfirmar').find(':input').serialize();
        var tipo = "Salvar";
        $.ajax({
            type: 'POST',
            url: "/PagamentoContaDer/ConfirmacaoPagamento/ListarPagamentosTransmitir",
            cache: false,
            async: true,
            data: data,
            beforeSend: function (hrx) {
                waitingDialog.show('Transmitindo');
            }
        }).done(function (jqXHR) {
            $('#divResultadosBusca').html(jqXHR.Html);
        }).fail(function (jqXHR, textStatus) {
            AbrirModal(jqXHR.statusText, function () { });
        }).always(function () {
            waitingDialog.hide();
        });
    });

    