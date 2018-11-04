var tans = "";
var tipoContrato = "0304";

(function (window, document, $) {
    'use strict';

    window.preparacaoPagamentoValidator = {};

    preparacaoPagamentoValidator.init = function () {
        preparacaoPagamentoValidator.cacheSelectors();
        preparacaoPagamentoValidator.provider();
    }

    preparacaoPagamentoValidator.cacheSelectors = function () {
        preparacaoPagamentoValidator.body = $('body');
        preparacaoPagamentoValidator.formPainelCadastrar = $("#frmPainelCadastrar");

        preparacaoPagamentoValidator.buttonSalvar = $("#btnSalvar");
        preparacaoPagamentoValidator.buttonTransmitir = $("#btnTransmitir");

        preparacaoPagamentoValidator.Entity = window.Entity;
        preparacaoPagamentoValidator.controller = window.controller;
        preparacaoPagamentoValidator.EntityId = $("#Codigo");

        preparacaoPagamentoValidator.validatorHandler = function (e) {
            preparacaoPagamentoValidator.hasError = false;
            preparacaoPagamentoValidator.classHasError = $(".has-error");
            if (preparacaoPagamentoValidator.classHasError.length) {
                preparacaoPagamentoValidator.hasError = true;
            }

            if (preparacaoPagamentoValidator.hasError == true) {
                switch (tans) {
                    case "T":
                        e.preventDefault();
                        $(".has-error:first input").focus();
                        break;
                    default:
                        e.preventDefault();
                        preparacaoPagamentoValidator.Entity.CadastroCompleto = false;
                        preparacaoPagamentoValidator.salvar();
                        break;
                }
            }
            else {
                switch (tans) {
                    case "T":
                        e.preventDefault();
                        preparacaoPagamentoValidator.Entity.CadastroCompleto = true;
                        preparacaoPagamentoValidator.transmitir();
                        break;
                    case "S":
                        e.preventDefault();
                        preparacaoPagamentoValidator.Entity.CadastroCompleto = true;
                        preparacaoPagamentoValidator.salvar();
                        break;
                    case "I":
                        e.preventDefault();
                        window.location.href = 'PreparacaoDePagamento/Edit/' + preparacaoPagamentoValidator.EntityId.val() + '?tipo=i';
                        break;
                }
            }
            e.submit;
        }

        preparacaoPagamentoValidator.saveHandler = function () {
            tans = 'S';
            preparacaoPagamentoValidator.formPainelCadastrar.data("bootstrapValidator").resetForm();
            preparacaoPagamentoValidator.formPainelCadastrar.submit();
        };

        preparacaoPagamentoValidator.sendHandler = function () {
            tans = 'T';
            preparacaoPagamentoValidator.formPainelCadastrar.data("bootstrapValidator").resetForm();
            preparacaoPagamentoValidator.formPainelCadastrar.submit();
        };
    }

    preparacaoPagamentoValidator.jsonValidate = {
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

            PreparacaoPagamentoTipoId: {
                validators: {
                    callback: {
                        message: "Selecione um Tipo de Preparaçao",
                        callback: function (value) {
                            return value.length > 0;
                        }
                    }
                }
            },
            AnoExercicio: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            return value.length > 0;
                        }
                    }
                }
            },
            CodigoConta: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            return value.length > 0;
                        }
                    }
                }
            },
            
            NumeroBanco: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido.",
                        callback: function (value) {
                            return value.length > 0;
                        }
                    }
                }
            },
            NumeroAgencia: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido..",
                        callback: function (value) {
                            return value.length > 0;
                        }
                    }
                }
            },
            NumeroConta: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido...",
                        callback: function (value) {
                            return value.length > 0;
                        }
                    }
                }
            },
            DocumentoTipoId: {
                validators: {
                    callback: {
                        message: "Selecione algum tipo de documento",
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
            ValorDocumento: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            return value.replace(/[\.,R$ ]/g, "") > 0;
                        }
                    }
                }
            },
            CodigoAutorizadoAssinatura: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            return value > 0;
                        }
                    }
                }
            },
            CodigoExaminadoAssinatura: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            return value > 0;
                        }
                    }
                }
            },
            DataVencimento: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            return value.replace("/","").replace("/","") > 0;
                        }
                    }
                }
            },
            TipoDespesa: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            return value > 0;
                        }
                    }
                }
            },
            Regional: {
                validators: {
                    callback: {
                        message: "Alguma Regional deve ser preenchida",
                        callback: function (value) {
                            return value > 0;
                        }
                    }
                }
            },

        }
    };

    preparacaoPagamentoValidator.salvar = function () {
        if (navigator.onLine !== true) {
            AbrirModal("Erro de conexão");
            return false;
        }

        preparacaoPagamentoValidator.new();

        $.ajax({
            datatype: "json",
            type: "Post",
            url: "/PagamentoContaUnica/PreparacaoDePagamento/Save",
            data: JSON.stringify(preparacaoPagamentoValidator.Entity),
            contentType: "application/json; charset=utf-8",
            beforeSend: function () {
                waitingDialog.show("Salvando");
            },
            success: function (dados) {
                if (dados.Status === "Sucesso") {
                    preparacaoPagamentoValidator.EntityId.val(dados.Id);
                    $.confirm({
                        text: "Preparação de Pagamento  " + " " + AcaoRealizada(tipoAcao, preparacaoPagamentoValidator.controller) + " com sucesso!",
                        title: "Confirmação",
                        cancel: function () {
                            window.location.href = "/PagamentoContaUnica/PreparacaoDePagamento/Edit/" + dados.Id + "?tipo=a";
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
                preparacaoPagamentoValidator.formPainelCadastrar.data("bootstrapValidator").resetForm();
                waitingDialog.hide();
            }
        });
    }

    preparacaoPagamentoValidator.transmitir = function () {
        if (navigator.onLine !== true) {
            AbrirModal("Erro de conexão");
            return false;
        }

        preparacaoPagamentoValidator.new();

        ModelItem = preparacaoPagamentoValidator.Entity;

        Transmissao(JSON.stringify(preparacaoPagamentoValidator.Entity), preparacaoPagamentoValidator.controller, "Transmissão realizada com sucesso, deseja cadastrar outra preparação de pagamento?");
    }

    preparacaoPagamentoValidator.new = function () {

        preparacaoPagamentoValidator.Entity.PreparacaoPagamentoTipoId = $("#PreparacaoPagamentoTipoId").val(),
        preparacaoPagamentoValidator.Entity.AnoExercicio = $("#AnoExercicio").val(),

        preparacaoPagamentoValidator.Entity.CodigoConta = $("#CodigoConta").val(),
        preparacaoPagamentoValidator.Entity.NumeroBanco = $("#NumeroBanco").val(),
        preparacaoPagamentoValidator.Entity.NumeroAgencia = $("#NumeroAgencia").val(),
        preparacaoPagamentoValidator.Entity.NumeroConta = $("#NumeroConta").val(),

        preparacaoPagamentoValidator.Entity.RegionalId = $("#Orgao").val(),
        preparacaoPagamentoValidator.Entity.DataVencimento = $("#DataVencimento").val(),
        preparacaoPagamentoValidator.Entity.CodigoDespesa = $("#TipoDespesa").val(),
      
        preparacaoPagamentoValidator.Entity.CodigoAutorizadoAssinatura = $("#CodAssAutorizado").val(),
        preparacaoPagamentoValidator.Entity.CodigoAutorizadoGrupo = $("#txtAutorizadoGrupo").val(),
        preparacaoPagamentoValidator.Entity.CodigoAutorizadoOrgao = $("#txtAutorizadoOrgao").val(),
        preparacaoPagamentoValidator.Entity.NomeAutorizadoAssinatura = $("#txtAutorizadoNome").val(),
        preparacaoPagamentoValidator.Entity.DescricaoAutorizadoCargo = $("#txtAutorizadoCargo").val(),
        preparacaoPagamentoValidator.Entity.CodigoExaminadoAssinatura = $("#CodAssExaminado").val(),
        preparacaoPagamentoValidator.Entity.CodigoExaminadoGrupo = $("#txtExaminadoGrupo").val(),
        preparacaoPagamentoValidator.Entity.CodigoExaminadoOrgao = $("#txtExaminadoOrgao").val(),
        preparacaoPagamentoValidator.Entity.NomeExaminadoAssinatura = $("#txtExaminadoNome").val(),
        preparacaoPagamentoValidator.Entity.DescricaoExaminadoCargo = $("#txtExaminadoCargo").val(),
        preparacaoPagamentoValidator.Entity.DocumentoTipoId = $("#DocumentoTipoId").val(),
        
        preparacaoPagamentoValidator.Entity.NumeroDocumento = $("#NumeroDocumento").val(),
        preparacaoPagamentoValidator.Entity.ValorDocumento = $("#ValorDocumento").val().replace(/[R$ \-.]/g, ""),
        preparacaoPagamentoValidator.Entity.Referencia = $("#Referencia").val(),
        
        preparacaoPagamentoValidator.Entity.CodigoCredorOrganizacaoId = $("#CodigoCredorOrganizacaoId").val(),
        preparacaoPagamentoValidator.Entity.NumeroCnpjcpfCredor = $("#NumeroCNPJCPFCredorId").val(),
        preparacaoPagamentoValidator.Entity.CodigoDespesaCredor = $("#TipoDespesaCredor").val(),
        preparacaoPagamentoValidator.Entity.NumeroContrato = $("#NumeroContrato").val(),

        preparacaoPagamentoValidator.Entity.Credor1 = $("#Credor1").val(),
        preparacaoPagamentoValidator.Entity.Credor2 = $("#Credor2").val(),
        preparacaoPagamentoValidator.Entity.Endereco = $("#DescricaoLogradouroEntrega").val(),
        preparacaoPagamentoValidator.Entity.Cep = $("#NumeroCEPEntrega").val(),
        
        preparacaoPagamentoValidator.Entity.NumeroBancoCredor = $("#NumeroBancoCredor").val(),
        preparacaoPagamentoValidator.Entity.NumeroAgenciaCredor = $("#NumeroAgenciaCredor").val(),
        preparacaoPagamentoValidator.Entity.NumeroContaCredor = $("#NumeroContaCredor").val(),
        preparacaoPagamentoValidator.Entity.NumeroBancoPagto = $("#NumeroBancoPagto").val(),
        preparacaoPagamentoValidator.Entity.NumeroAgenciaPagto = $("#NumeroAgenciaPagto").val(),
        preparacaoPagamentoValidator.Entity.NumeroContaPagto = $("#NumeroContaPagto").val(),
        preparacaoPagamentoValidator.Entity.QuantidadeOpPreparada = $("#QuantidadeOpPreparada").val(),
        preparacaoPagamentoValidator.Entity.ValorTotal = $("#ValorTotal").val().replace(/[R$ \-.]/g, ""),

        preparacaoPagamentoValidator.Entity.TransmitirProdesp = true;
    }


    preparacaoPagamentoValidator.provider = function () {
        preparacaoPagamentoValidator.formPainelCadastrar
         .bootstrapValidator(preparacaoPagamentoValidator.jsonValidate)
         .on("submit", preparacaoPagamentoValidator.validatorHandler)

        preparacaoPagamentoValidator.buttonSalvar
       .on('click', preparacaoPagamentoValidator.saveHandler);

        preparacaoPagamentoValidator.buttonTransmitir
        .on('click', preparacaoPagamentoValidator.sendHandler);
    }

    $(document).on('ready', preparacaoPagamentoValidator.init);

})(window, document, jQuery);