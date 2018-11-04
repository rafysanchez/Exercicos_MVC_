var tans = "";
var tipoContrato = "0304";

(function (window, document, $) {
    'use strict';

    window.desdobramentoValidator = {};

    desdobramentoValidator.init = function () {
        desdobramentoValidator.cacheSelectors();
        desdobramentoValidator.provider();
    }

    desdobramentoValidator.cacheSelectors = function () {
        desdobramentoValidator.body = $('body');
        desdobramentoValidator.formPainelCadastrar = $("#frmPainelCadastrar");

        desdobramentoValidator.buttonSalvar = $("#btnSalvar");
        desdobramentoValidator.buttonTransmitir = $("#btnTransmitir");
        desdobramentoValidator.aceitarCredor = $("#AceitaCredor");
        desdobramentoValidator.Entity = window.Entity;
        desdobramentoValidator.controller = window.controller;
        desdobramentoValidator.area = window.area;

        desdobramentoValidator.route = '/' + desdobramentoValidator.area + '/' + desdobramentoValidator.controller;

        desdobramentoValidator.EntityId = $("#Codigo");
        desdobramentoValidator.TipoDesdobramento = $("#DesdobramentoTipoId");

        desdobramentoValidator.validatorHandler = function (e) {
            desdobramentoValidator.hasError = false;
            desdobramentoValidator.classHasError = $(".has-error");
            if (desdobramentoValidator.classHasError.length) {
                desdobramentoValidator.hasError = true;
            }

            if (desdobramentoValidator.hasError == true) {
                switch (tans) {
                    case "T":
                        e.preventDefault();
                        $(".has-error:first input").focus();
                        break;
                    default:
                        e.preventDefault();
                        desdobramentoValidator.Entity.CadastroCompleto = false;
                        desdobramentoValidator.salvar();
                        break;
                }
            }
            else {
                switch (tans) {
                    case "T":
                        e.preventDefault();
                        desdobramentoValidator.Entity.CadastroCompleto = true;
                        desdobramentoValidator.transmitir();
                        break;
                    case "S":
                        e.preventDefault();
                        desdobramentoValidator.Entity.CadastroCompleto = true;
                        desdobramentoValidator.salvar();
                        break;
                    case "I":
                        e.preventDefault();
                        window.location.href = desdobramentoValidator.route + '/Edit/' + desdobramentoValidator.EntityId.val() + '?tipo=i';
                        break;
                }
            }
            e.submit;
        }

        desdobramentoValidator.saveHandler = function () {
            tans = 'S';
            desdobramentoValidator.formPainelCadastrar.data("bootstrapValidator").resetForm();
            desdobramentoValidator.formPainelCadastrar.submit();
        };

        desdobramentoValidator.sendHandler = function () {
            tans = 'T';
            desdobramentoValidator.formPainelCadastrar.data("bootstrapValidator").resetForm();
            desdobramentoValidator.formPainelCadastrar.submit();
        };
    }

    desdobramentoValidator.jsonValidate = {
        feedbackIcons: {
            valid: "",
            invalid: "",
            validating: ""
        },
        fields: {
            Id: {
                validators: {
                    required: false
                }
            },
            NumeroContrato: {
                validators: {
                    stringLength: {
                        min: 13,
                        message: 'O contrato deve ser preenchido por completo'
                    }

                }
            },
            DocumentoTipoId: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            return value.length > 0;
                        }
                    }
                }
            },
            NumeroDocumento: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            return value.length > 0;
                        }
                    }
                }
            },
            CodigoServico: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            if (desdobramentoValidator.TipoDesdobramento.val() == 1)
                                return value.length > 0;
                            else
                                return true;
                        }
                    }
                }
            },
            ValorDistribuido: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            if (desdobramentoValidator.TipoDesdobramento.val() == 1) {
                                var valorLimpo = value.replace(/[\.,R$ ]/g, "");
                                var valorLimpoInteiro = parseInt(valorLimpo);

                                var numerico = isNumber(valorLimpo);

                                return valorLimpoInteiro > 0 && numerico;
                            }
                            else
                                return true;
                        }
                    }
                }
            },
            //DescricaoServico: {
            //    validators: {
            //        callback: {
            //            message: "Campo obrigatório não preenchido",
            //            callback: function (value) {
            //                if (desdobramentoValidator.TipoDesdobramento.val() == 1)
            //                    return value.length > 0;
            //                else
            //                    return true;
            //            }
            //        }
            //    }
            //},
            //DescricaoCredor: {
            //    validators: {
            //        callback: {
            //            message: "Campo obrigatório não preenchido",
            //            callback: function (value) {
            //                if (desdobramentoValidator.TipoDesdobramento.val() == 1)
            //                    return value.length > 0;
            //                else
            //                    return true;
            //            }
            //        }
            //    }
            //},
            //NomeReduzidoCredor: {
            //    validators: {
            //        callback: {
            //            message: "Campo obrigatório não preenchido",
            //            callback: function (value) {
            //                if (desdobramentoValidator.TipoDesdobramento.val() == 1)
            //                    return value.length > 0;
            //                else
            //                    return true;
            //            }
            //        }
            //    }
            //},
            tblPesquisaIssqn: {
                validators: {
                    callback: {
                        message: "DEVE SER INFORMADO PELO MENOS DOIS DESDOBRAMENTOS.",
                        callback: function (value) {
                            if (desdobramentoValidator.TipoDesdobramento.val() == 1)
                                return desdobramentoList.EntityList.length >= 2;
                            else
                                return true;
                        }
                    }
                }
            },
            tblPesquisaOutros: {
                validators: {
                    callback: {
                        message: "DEVE SER INFORMADO PELO MENOS UM DESDOBRAMENTOS.",
                        callback: function (value) {
                            if (desdobramentoValidator.TipoDesdobramento.val() == 2)
                                return desdobramentoList.EntityList.length >= 1;
                            else
                                return true;
                        }
                    }
                }
            },
            nmRetransmitirTodos: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            if (desdobramentoValidator.TipoDesdobramento.val() == 1) {
                                if (desdobramentoValidator.aceitarCredor.is(":checked")) {
                                    return value.length > 0;
                                } else
                                    return true;
                            }
                            else
                                return true;

                        }
                    }
                }
            },


        }
    };

    desdobramentoValidator.salvar = function () {
        if (navigator.onLine !== true) {
            AbrirModal("Erro de conexão");
            return false;
        }

        desdobramentoValidator.new();
       

        $.ajax({
            datatype: "json",
            type: "Post",
            url: "/PagamentoContaUnica/Desdobramento/Save",
            data: JSON.stringify(desdobramentoValidator.Entity),
            contentType: "application/json; charset=utf-8",
            beforeSend: function () {
                waitingDialog.show("Salvando");
            },
            success: function (dados) {
                if (dados.Status === "Sucesso") {
                    desdobramentoValidator.EntityId.val(dados.Id);
                    $.confirm({
                        text: desdobramentoValidator.controller + " " + AcaoRealizada(tipoAcao, desdobramentoValidator.controller) + " com sucesso!",
                        title: "Confirmação",
                        cancel: function () {
                            window.location.href = "/PagamentoContaUnica/Desdobramento/Edit/" + dados.Id + "?tipo=a";
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
                desdobramentoValidator.formPainelCadastrar.data("bootstrapValidator").resetForm();
                waitingDialog.hide();
            }
        });
    }

    desdobramentoValidator.transmitir = function () {
        if (navigator.onLine !== true) {
            AbrirModal("Erro de conexão");
            return false;
        }

        desdobramentoValidator.new();
        ModelItem = desdobramentoValidator.Entity;
        Transmissao(JSON.stringify(desdobramentoValidator.Entity), desdobramentoValidator.controller);
    }

    desdobramentoValidator.new = function () {

        desdobramentoValidator.Entity.DesdobramentoTipoId = $("#DesdobramentoTipoId").val();
        desdobramentoValidator.Entity.DocumentoTipoId = $("#DocumentoTipoId").val();
        desdobramentoValidator.Entity.NumeroDocumento = $("#NumeroDocumento").val();
        desdobramentoValidator.Entity.CodigoServico = $("#CodigoServico").val();

        if (desdobramentoValidator.Entity.DesdobramentoTipoId == 1) {
            desdobramentoValidator.Entity.ValorDistribuido = $("#ValorDistribuido").val().replace(/[R$ \-.]/g, "");
        } else {
            desdobramentoValidator.Entity.ValorDistribuido = $("#ValorDistribuidoOutros").val().replace(/[R$ \-.]/g, "");
        }

        desdobramentoValidator.Entity.DescricaoServico = $("#DescricaoServico").val();
        desdobramentoValidator.Entity.DescricaoCredor = $("#DescricaoCredor").val();
        desdobramentoValidator.Entity.NomeReduzidoCredor = $("#NomeReduzidoCredor").val();
        desdobramentoValidator.Entity.AceitaCredor = $("#AceitaCredor").is(":checked");
        desdobramentoValidator.Entity.TipoDespesa = $("#TipoDespesa").val();
        desdobramentoValidator.Entity.NumeroContrato = $("#NumeroContrato").val();
        desdobramentoValidator.Entity.CodigoAplicacaoObra = $("#CodigoAplicacaoObra").val();

        desdobramentoValidator.Entity.IdentificacaoDesdobramentos = desdobramentoList.EntityList;

        desdobramentoValidator.Entity.TransmitirProdesp = true;


    }


    desdobramentoValidator.provider = function () {
        desdobramentoValidator.formPainelCadastrar
            .bootstrapValidator(desdobramentoValidator.jsonValidate)
            .on("submit", desdobramentoValidator.validatorHandler);

        desdobramentoValidator.buttonSalvar
       .on('click', desdobramentoValidator.saveHandler);

        desdobramentoValidator.buttonTransmitir
        .on('click', desdobramentoValidator.sendHandler);

    }


    $(document).on('ready', desdobramentoValidator.init);

})(window, document, jQuery);