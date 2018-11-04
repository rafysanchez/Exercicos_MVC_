var tans = "";
var tipoContrato = "0304";

(function (window, document, $) {
    'use strict';

    window.programacaoDesembolsoValidator = {};

    programacaoDesembolsoValidator.init = function () {
        programacaoDesembolsoValidator.cacheSelectors();
        programacaoDesembolsoValidator.provider();
    }

    programacaoDesembolsoValidator.cacheSelectors = function () {
        programacaoDesembolsoValidator.body = $('body');
        programacaoDesembolsoValidator.formPainelCadastrar = $("#frmPainelCadastrar");

        programacaoDesembolsoValidator.buttonSalvar = $("#btnSalvar");
        programacaoDesembolsoValidator.buttonTransmitir = $("#btnTransmitir");

        programacaoDesembolsoValidator.Entity = window.Entity;
        programacaoDesembolsoValidator.controller = window.controller;
        programacaoDesembolsoValidator.EntityId = $("#Codigo");

        programacaoDesembolsoValidator.validatorHandler = function (e) {
            programacaoDesembolsoValidator.hasError = false;
            programacaoDesembolsoValidator.classHasError = $(".has-error");
            if (programacaoDesembolsoValidator.classHasError.length) {
                programacaoDesembolsoValidator.hasError = true;
            }

            if (programacaoDesembolsoValidator.hasError == true) {
                switch (tans) {
                    case "T":
                        e.preventDefault();
                        $(".has-error:first input").focus();
                        break;
                    default:
                        e.preventDefault();
                        programacaoDesembolsoValidator.Entity.CadastroCompleto = false;
                        programacaoDesembolsoValidator.salvar();
                        break;
                }
            }
            else {
                switch (tans) {
                    case "T":
                        e.preventDefault();
                        programacaoDesembolsoValidator.Entity.CadastroCompleto = true;
                        programacaoDesembolsoValidator.transmitir();
                        break;
                    case "S":
                        e.preventDefault();
                        programacaoDesembolsoValidator.Entity.CadastroCompleto = true;
                        programacaoDesembolsoValidator.salvar();
                        break;
                    case "I":
                        e.preventDefault();
                        window.location.href = '/PagamentoContaUnica/' + programacaoDesembolsoValidator.controller + '/Edit/' + programacaoDesembolsoValidator.EntityId.val() + '?tipo=i';
                        break;
                }
            }
            e.submit;
        }

        programacaoDesembolsoValidator.saveHandler = function () {
            tans = 'S';

            //programacaoDesembolsoValidator.new();
            //console.log(programacaoDesembolsoValidator.Entity);
            //return false;

            programacaoDesembolsoValidator.formPainelCadastrar.data("bootstrapValidator").resetForm();
            programacaoDesembolsoValidator.formPainelCadastrar.submit();
        };

        programacaoDesembolsoValidator.sendHandler = function () {
            tans = 'T';
            programacaoDesembolsoValidator.formPainelCadastrar.data("bootstrapValidator").resetForm();
            programacaoDesembolsoValidator.formPainelCadastrar.submit();
        };
    }

    programacaoDesembolsoValidator.jsonValidate = {
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

            ProgramacaoDesembolsoTipoId: {
                validators: {
                    notEmpty: {
                        message: "Campo obrigatório não preenchido"
                    }
                }
            },
            CodigoUnidadeGestora: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            return value.length > 0;
                        }
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
            /*NumeroContrato: {
                validators: {
                    notEmpty: {
                        message: "Campo obrigatório não preenchido"
                    }
                }
            },*/
            /*CodigoAplicacaoObra: {
                validators: {
                    notEmpty: {
                        message: "Campo obrigatório não preenchido"
                    }
                }
            },*/
            NumeroNLReferencia: {
                validators: {
                    notEmpty: {
                        message: "Campo obrigatório não preenchido"
                    }
                }
            },
            NumeroProcesso: {
                validators: {
                    notEmpty: {
                        message: "Campo obrigatório não preenchido"
                    }
                }
            },
            Finalidade: {
                validators: {
                    notEmpty: {
                        message: "Campo obrigatório não preenchido"
                    }
                }
            },
            NumeroCnpjcpfCredor: {
                validators: {
                    notEmpty: {
                        message: "Campo obrigatório não preenchido"
                    }
                }
            },
            GestaoCredor: {
                validators: {
                    notEmpty: {
                        message: "Campo obrigatório não preenchido"
                    }
                }
            },
            /*NumeroBancoCredor: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            if ($("#NumeroCnpjcpfPagto").val().length <= 6) {
                                return value.length > 0;
                            }
                            return true;
                        }
                    }
                }
            },*/
            /*NumeroAgenciaCredor: {
                validators: {
                    notEmpty: {
                        message: "Campo obrigatório não preenchido"
                    }
                }
            },*/
            NumeroContaCredor: {
                validators: {
                    notEmpty: {
                        message: "Campo obrigatório não preenchido"
                    }
                }
            },
            NumeroCnpjcpfPagto: {
                validators: {
                    notEmpty: {
                        message: "Campo obrigatório não preenchido"
                    }
                }
            },
            GestaoPagto: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            if ($("#NumeroCnpjcpfPagto").val().length <= 6) {
                                return value.length > 0;
                            }
                            return true;
                        }
                    }
                }
            },
            NumeroBancoPagto: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            if ($("#NumeroContaPagto").val().toUpperCase() != "UNICA") {
                                return value.length > 0;
                            }
                            return true;
                        }
                    }

                }
            },
            NumeroAgenciaPagto: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            if ($("#NumeroContaPagto").val().toUpperCase() != "UNICA") {
                                return value.length > 0;
                            }
                            return true;
                        }
                    }
                }
            },
            NumeroContaPagto: {
                validators: {
                    notEmpty: {
                        message: "Campo obrigatório não preenchido"
                    }
                }
            },
            DocumentoTipoId: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            if ($("#ProgramacaoDesembolsoTipoId").val() == 1) {
                                return value.length > 0;
                            }
                            return true;
                        }
                    }
                }
            },
            NumeroDocumento: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            if ($("#ProgramacaoDesembolsoTipoId").val() == 1) {
                                return value.length > 0;
                            }
                            return true;
                        }
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
            DataVencimento: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            if ($("#ProgramacaoDesembolsoTipoId").val() == 1) {
                                return value.length > 0;
                            }
                            return true;
                        }
                    },
                    callback1: {
                        message: "Data de Vencimento deve ser maior ou igual que Data de Emissão",
                        callback1: function (value) {
                            if ($("#ProgramacaoDesembolsoTipoId").val() == 2)
                                return true;

                            if (value.length < 10)
                                return false;

                            var data_1 = new Date(value.split('/').reverse().join('/'));
                            var data_2 = new Date($("#DataEmissao").val().split('/').reverse().join('/'));

                            if (data_1 < data_2) {
                                return false;
                            } else {
                                return true;
                            }
                        }
                    }
                }
            },

            NumeroCT: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            if ($('#NumeroNE').val() == "") {
                                return value.length !== 0;
                            }
                            return true;
                        }
                    }
                }
            },

            NumeroNE: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            if ($('#NumeroCT').val() == "") {
                                return value.length !== 0;
                            }
                            return true;
                        }
                    }
                }
            },

            Obs: {
                validators: {
                    notEmpty: {
                        message: "Campo obrigatório não preenchido"
                    }
                }
            }

        }
    };

    programacaoDesembolsoValidator.salvar = function () {
        if (navigator.onLine !== true) {
            AbrirModal("Erro de conexão");
            return false;
        }

        programacaoDesembolsoValidator.new();

        $.ajax({
            datatype: "json",
            type: "Post",
            url: "/PagamentoContaUnica/" + programacaoDesembolsoValidator.controller + "/Save",
            data: JSON.stringify(programacaoDesembolsoValidator.Entity),
            contentType: "application/json; charset=utf-8",
            beforeSend: function () {
                waitingDialog.show("Salvando");
            },
            success: function (dados) {
                if (dados.Status === "Sucesso") {
                    programacaoDesembolsoValidator.EntityId.val(dados.Id);
                    $.confirm({
                        text: "Programação de Desembolso " + " " + AcaoRealizada(tipoAcao, programacaoDesembolsoValidator.controller) + " com sucesso!",
                        title: "Confirmação",
                        cancel: function () {
                            window.location.href = "/PagamentoContaUnica/" + programacaoDesembolsoValidator.controller + "/Edit/" + dados.Id + "?tipo=a";
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
                programacaoDesembolsoValidator.formPainelCadastrar.data("bootstrapValidator").resetForm();
                waitingDialog.hide();
            }
        });
    }

    programacaoDesembolsoValidator.transmitir = function () {
        if (navigator.onLine !== true) {
            AbrirModal("Erro de conexão");
            return false;
        }

        programacaoDesembolsoValidator.new();
        ModelItem = programacaoDesembolsoValidator.Entity;
        Transmissao(JSON.stringify(programacaoDesembolsoValidator.Entity), programacaoDesembolsoValidator.controller);
    }

    programacaoDesembolsoValidator.new = function () {
        for (var prop in ModelItem) {
            if (ModelItem.hasOwnProperty(prop)) {
                if ($("#" + prop).length > 0)
                    if ($("#" + prop).val().indexOf("R$") >= 0)
                        programacaoDesembolsoValidator.Entity[prop] = $("#" + prop).val().replace(/[\.,R$ ]/g, "");
                    else
                        programacaoDesembolsoValidator.Entity[prop] = $("#" + prop).val();
            }
        }

        if (programacaoDesembolsoValidator.Entity.ProgramacaoDesembolsoTipoId == 2)
        {
            programacaoDesembolsoValidator.Entity.NumeroDocumento = $("#Documento").val();
            programacaoDesembolsoValidator.Entity.DocumentoTipoId = $("#DocumentoTipo").val();
            programacaoDesembolsoValidator.Entity.CodigoDespesa = $("#TipoDespesa").val();
            programacaoDesembolsoValidator.Entity.DataVencimento = $("#DTVencimento").val();
            programacaoDesembolsoValidator.Entity.RegionalId = $("#Orgao").val();
        }

        programacaoDesembolsoValidator.Entity.Eventos = pagamentoEvento.EntityList;
        programacaoDesembolsoValidator.Entity.Agrupamentos = desembolsoAgrupamento.EntityList;

        programacaoDesembolsoValidator.Entity.TransmitirSiafem      = true;
        programacaoDesembolsoValidator.Entity.TransmitirSiafisico = false;



       
        if (programacaoDesembolsoValidator.Entity.ProgramacaoDesembolsoTipoId == 3) {
            programacaoDesembolsoValidator.Entity.CodigoUnidadeGestora = $("div.PDBEC input#CodigoUnidadeGestora").val();
            programacaoDesembolsoValidator.Entity.CodigoGestao = $("div.PDBEC input#CodigoGestao").val();
            programacaoDesembolsoValidator.Entity.NumeroNLReferencia = $("div.PDBEC input#NumeroNLReferenciaBec").val();
            programacaoDesembolsoValidator.Entity.TransmitirSiafem      = false;
            programacaoDesembolsoValidator.Entity.TransmitirSiafisico   = true;
            programacaoDesembolsoValidator.Entity.TransmitidoSiafisico  = false;
        }

    }


    programacaoDesembolsoValidator.provider = function () {
        programacaoDesembolsoValidator.formPainelCadastrar.bootstrapValidator(programacaoDesembolsoValidator.jsonValidate).on("submit", programacaoDesembolsoValidator.validatorHandler);
        programacaoDesembolsoValidator.buttonSalvar.on('click', programacaoDesembolsoValidator.saveHandler);
        programacaoDesembolsoValidator.buttonTransmitir.on('click', programacaoDesembolsoValidator.sendHandler);
    }

    $(document).on('ready', programacaoDesembolsoValidator.init);

})(window, document, jQuery);