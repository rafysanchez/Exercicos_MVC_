var tans = "";
var tipoContrato = "0304";

(function (window, document, $) {
    'use strict';

    window.listaDeBoletosValidator = {};

    listaDeBoletosValidator.init = function () {
        listaDeBoletosValidator.cacheSelectors();
        listaDeBoletosValidator.provider();
    }

    window.Imprimir = function (id, controller, form) {
        window.location.href = '/PagamentoContaUnica/ListaDeBoletos/Edit/' + listaDeBoletosValidator.EntityId.val() + '?tans=a';
    }

    listaDeBoletosValidator.cacheSelectors = function () {
        listaDeBoletosValidator.body = $('body');
        listaDeBoletosValidator.formPainelCadastrar = $("#frmPainelCadastrar");

        listaDeBoletosValidator.buttonSalvar = $("#btnSalvar");
        listaDeBoletosValidator.buttonTransmitir = $("#btnTransmitir");
        listaDeBoletosValidator.Entity = window.Entity;

        listaDeBoletosValidator.controller = window.controller;
        listaDeBoletosValidator.EntityId = $("#Codigo");

        listaDeBoletosValidator.validatorHandler = function (e) {
            listaDeBoletosValidator.hasError = false;
            listaDeBoletosValidator.classHasError = $(".has-error");
            if (listaDeBoletosValidator.classHasError.length) {
                listaDeBoletosValidator.hasError = true;
            }

            if (listaDeBoletosValidator.hasError == true) {
                switch (tans) {
                    case "T":
                        e.preventDefault();
                        $(".has-error:first input").focus();
                        break;
                    default:
                        e.preventDefault();
                        listaDeBoletosValidator.Entity.CadastroCompleto = false;
                        listaDeBoletosValidator.salvar();
                        break;
                }
            }
            else {
                switch (tans) {
                    case "T":
                        e.preventDefault();
                        listaDeBoletosValidator.Entity.CadastroCompleto = true;
                        listaDeBoletosValidator.transmitir();
                        break;
                    case "S":
                        e.preventDefault();
                        listaDeBoletosValidator.Entity.CadastroCompleto = true;
                        listaDeBoletosValidator.salvar();
                        break;
                    default:
                        e.preventDefault();
                        window.location.href = 'ListaDeBoletos/Edit/' + listaDeBoletosValidator.EntityId.val() + '?tipo=a';
                        break;
                }
            }
            e.submit;
        }

        listaDeBoletosValidator.saveHandler = function () {
            tans = 'S';
            listaDeBoletosValidator.formPainelCadastrar.data("bootstrapValidator").resetForm();
            listaDeBoletosValidator.formPainelCadastrar.submit();
        };

        listaDeBoletosValidator.sendHandler = function () {
            tans = 'T';
            listaDeBoletosValidator.formPainelCadastrar.data("bootstrapValidator").resetForm();
            listaDeBoletosValidator.formPainelCadastrar.submit();
        };
    }

    listaDeBoletosValidator.jsonValidate = {
        feedbackIcons: {
            valid: "",
            invalid: "",
            validating: ""
        },
        fields: {

            CodigoUnidadeGestora: {
                validators: {
                    notEmpty: {
                        message: "Campo obrigatório não preenchido"
                    }
                }
            },
            CodigoGestao: {
                validators: {
                    notEmpty: {
                        message: "Campo obrigatório não preenchido"
                    }
                }
            },
            DataEmissao: {
                validators: {
                    notEmpty: {
                        message: "Campo obrigatório não preenchido"
                    }
                }
            },
            NumeroCnpjcpfFavorecido: {
                validators: {
                    notEmpty: {
                        message: "Campo obrigatório não preenchido"
                    }
                }
            },
            NomeLista: {
                validators: {
                    notEmpty: {
                        message: "Campo obrigatório não preenchido"
                    }
                }
            },
            tblCodigoBarras: {
                validators: {
                    callback: {
                        message: "Codigo de Barras obrigatório não inserido",
                        callback: function (value) {
                            return listaDeBoletosCodicoBarras.EntityList.length > 0;
                        }
                    }
                }
            }
        }
    };

    listaDeBoletosValidator.salvar = function () {
        if (navigator.onLine !== true) {
            AbrirModal("Erro de conexão");
            return false;
        }

        listaDeBoletosValidator.new();

        $.ajax({
            datatype: "json",
            type: "Post",
            url: "/PagamentoContaUnica/ListaDeBoletos/Save",
            data: JSON.stringify(listaDeBoletosValidator.Entity),
            contentType: "application/json; charset=utf-8",
            beforeSend: function () {
                waitingDialog.show("Salvando");
            },
            success: function (dados) {
                if (dados.Status === "Sucesso") {
                    listaDeBoletosValidator.EntityId.val(dados.Id);
                    $.confirm({
                        text: "Lista de Boletos " + AcaoRealizada(tipoAcao, listaDeBoletosValidator.controller) + " com sucesso!",
                        title: "Confirmação",
                        cancel: function () {
                            window.location.href = "/" + area + "/" + window.controller + "/Edit/" + dados.Id + "?tipo=a";
                        },
                        cancelButton: "Fechar",
                        confirmButton: false
                    });
                } else {
                    AbrirModal(dados.Msg);
                }
            },
            error: function (dados) {
                AbrirModal(dados.statusText);
            },
            complete: function () {
                listaDeBoletosValidator.formPainelCadastrar.data("bootstrapValidator").resetForm();
                waitingDialog.hide();
            }
        });
    }

    listaDeBoletosValidator.transmitir = function () {
        if (navigator.onLine !== true) {
            AbrirModal("Erro de conexão");
            return false;
        }

        listaDeBoletosValidator.new();
        ModelItem = listaDeBoletosValidator.Entity;
        Transmissao(JSON.stringify(listaDeBoletosValidator.Entity), listaDeBoletosValidator.controller);
    }

    listaDeBoletosValidator.new = function () {

        for (var prop in ModelItem) {
            if (ModelItem.hasOwnProperty(prop)) {
                if ($("#" + prop).length > 0)
                    if ($("#" + prop).val().indexOf("R$") >= 0)
                        listaDeBoletosValidator.Entity[prop] = $("#" + prop).val().replace(/[\.R$ ]/g, "");
                    else
                        listaDeBoletosValidator.Entity[prop] = $("#" + prop).val();
            }
        }

        listaDeBoletosValidator.Entity.ListaCodigoBarras = listaDeBoletosCodicoBarras.EntityList;

        listaDeBoletosValidator.Entity.TransmitirSiafem = true;


    }


    listaDeBoletosValidator.provider = function () {
        listaDeBoletosValidator.formPainelCadastrar
            .bootstrapValidator(listaDeBoletosValidator.jsonValidate)
            .on("submit", listaDeBoletosValidator.validatorHandler);

        listaDeBoletosValidator.buttonSalvar
       .on('click', listaDeBoletosValidator.saveHandler);

        listaDeBoletosValidator.buttonTransmitir
        .on('click', listaDeBoletosValidator.sendHandler);

    }


    $(document).on('ready', listaDeBoletosValidator.init);

})(window, document, jQuery);