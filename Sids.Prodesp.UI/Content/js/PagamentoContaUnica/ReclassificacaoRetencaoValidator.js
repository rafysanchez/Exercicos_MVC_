var tans = "";
var tipoContrato = "0304";

(function (window, document, $) {
    'use strict';

    window.reclassificacaoRetencaoValidator = {};

    reclassificacaoRetencaoValidator.init = function () {
        reclassificacaoRetencaoValidator.cacheSelectors();
        reclassificacaoRetencaoValidator.provider();
    }

    reclassificacaoRetencaoValidator.cacheSelectors = function () {
        reclassificacaoRetencaoValidator.body = $('body');
        reclassificacaoRetencaoValidator.formPainelCadastrar = $("#frmPainelCadastrar");

        reclassificacaoRetencaoValidator.buttonSalvar = $("#btnSalvar");
        reclassificacaoRetencaoValidator.buttonTransmitir = $("#btnTransmitir");
        reclassificacaoRetencaoValidator.Entity = window.Entity;

        reclassificacaoRetencaoValidator.controller = window.controller;
        reclassificacaoRetencaoValidator.EntityId = $("#Codigo");

        reclassificacaoRetencaoValidator.valueReclassificacaoRetencaoTipoId = $("#ReclassificacaoRetencaoTipoId");

        reclassificacaoRetencaoValidator.validatorHandler = function (e) {
            reclassificacaoRetencaoValidator.hasError = false;
            reclassificacaoRetencaoValidator.classHasError = $(".has-error");
            if (reclassificacaoRetencaoValidator.classHasError.length) {
                reclassificacaoRetencaoValidator.hasError = true;
            }

            if (reclassificacaoRetencaoValidator.hasError == true) {
                switch (tans) {
                    case "T":
                        e.preventDefault();
                        $(".has-error:first input").focus();
                        break;
                    default:
                        e.preventDefault();
                        reclassificacaoRetencaoValidator.Entity.CadastroCompleto = false;
                        reclassificacaoRetencaoValidator.salvar();
                        break;
                }
            }
            else {
                switch (tans) {
                    case "T":
                        e.preventDefault();
                        reclassificacaoRetencaoValidator.Entity.CadastroCompleto = true;
                        reclassificacaoRetencaoValidator.transmitir();
                        break;
                    case "S":
                        e.preventDefault();
                        reclassificacaoRetencaoValidator.Entity.CadastroCompleto = true;
                        reclassificacaoRetencaoValidator.salvar();
                        break;
                    case "I":
                        e.preventDefault();
                        window.location.href = '/PagamentoContaUnica/' + reclassificacaoRetencaoValidator.controller + '/Edit/' + reclassificacaoRetencaoValidator.EntityId.val() + '?tipo=i';
                        break;
                }
            }
            e.submit;
        }

        reclassificacaoRetencaoValidator.saveHandler = function () {
            tans = 'S';
            reclassificacaoRetencaoValidator.formPainelCadastrar.data("bootstrapValidator").resetForm();
            reclassificacaoRetencaoValidator.formPainelCadastrar.submit();
        };

        reclassificacaoRetencaoValidator.sendHandler = function () {
            tans = 'T';
            reclassificacaoRetencaoValidator.formPainelCadastrar.data("bootstrapValidator").resetForm();
            reclassificacaoRetencaoValidator.formPainelCadastrar.submit();
        };
    }

    reclassificacaoRetencaoValidator.jsonValidate = {
        feedbackIcons: {
            valid: "",
            invalid: "",
            validating: ""
        },
        fields: {
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
                    notEmpty: {
                        message: "Campo obrigatório não preenchido"
                    }
                }
            },
            NumeroDocumento: {
                validators: {
                    notEmpty: {
                        message: "Campo obrigatório não preenchido"
                    }
                }
            },
            NormalEstorno: {
                validators: {
                    notEmpty: {
                        message: "Campo obrigatório não preenchido"
                    }
                }
            },
            ReclassificacaoRetencaoTipoId: {
                validators: {
                    notEmpty: {
                        message: "Campo obrigatório não preenchido"
                    }
                }
            },
            NumeroOriginalSiafemSiafisico: {
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
            CodigoAplicacaoObra: {
                validators: {
                    notEmpty: {
                        message: "Campo obrigatório não preenchido"
                    }
                }
            },
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


            NumeroCNPJCPFCredor: {
                validators: {
                    notEmpty: {
                        message: "Campo obrigatório não preenchido"
                    }
                }
            },
            CodigoGestaoCredor: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            if ($("#NumeroCNPJCPFCredor").val().length <= 6)
                                return value.length > 0;
                            else {
                                return true;
                            }
                        }
                    }
                }
            },
            Total: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            return value.replace(/[\.,R$ ]/g, "") > 0;
                        }
                    }
                }
            },
            DescricaoObservacao1: {
                validators: {
                    notEmpty: {
                        message: "Campo obrigatório não preenchido"
                    }
                }
            },
            Nota01: {
                validators: {
                    notEmpty: {
                        message: "Campo obrigatório não preenchido"
                    }
                }
            },
            MesMedicao: {
                validators: {
                    notEmpty: {
                        message: "Campo obrigatório não preenchido"
                    }
                }
            },
            AnoMedicao: {
                validators: {
                    notEmpty: {
                        message: "Campo obrigatório não preenchido"
                    }
                }
            },
            Valor: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            return value.replace(/[\.,R$ ]/g, "") > 0;
                        }
                    }
                }
            },
            CodigoEvento: {
                validators: {
                    notEmpty: {
                        message: "Campo obrigatório não preenchido"
                    }
                }
            },
            CodigoInscricao: {
                validators: {
                    notEmpty: {
                        message: "Campo obrigatório não preenchido"
                    }
                }
            },

            CodigoFonte: {
                validators: {
                    notEmpty: {
                        message: "Campo obrigatório não preenchido"
                    }
                }
            },
            NotaLancamenoMedicao: {
                validators: {
                    notEmpty: {
                        message: "Campo obrigatório não preenchido"
                    }
                }
            },

            NumeroCnpjPrefeitura: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            if (reclassificacaoRetencaoValidator.valueReclassificacaoRetencaoTipoId.val() === "4") {
                                if ($("#NumeroCnpjPrefeitura").val().trim().length == 0) {
                                    return false;
                                }
                            }
                            return true;
                        }
                    }
                }
            }
        },
    };

    reclassificacaoRetencaoValidator.salvar = function () {
        if (navigator.onLine !== true) {
            AbrirModal("Erro de conexão");
            return false;
        }

        reclassificacaoRetencaoValidator.new();

        $.ajax({
            datatype: "json",
            type: "Post",
            url: "/PagamentoContaUnica/ReclassificacaoRetencao/Save",
            data: JSON.stringify(reclassificacaoRetencaoValidator.Entity),
            contentType: "application/json; charset=utf-8",
            beforeSend: function () {
                waitingDialog.show("Salvando");
            },
            success: function (dados) {
                if (dados.Status === "Sucesso") {
                    reclassificacaoRetencaoValidator.EntityId.val(dados.Id);
                    $.confirm({
                        text: reclassificacaoRetencaoValidator.controller + " " + AcaoRealizada(tipoAcao, reclassificacaoRetencaoValidator.controller) + " com sucesso!",
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
                reclassificacaoRetencaoValidator.formPainelCadastrar.data("bootstrapValidator").resetForm();
                waitingDialog.hide();
            }
        });
    }

    reclassificacaoRetencaoValidator.transmitir = function () {
        if (navigator.onLine !== true) {
            AbrirModal("Erro de conexão");
            return false;
        }

        reclassificacaoRetencaoValidator.new();
        ModelItem = reclassificacaoRetencaoValidator.Entity;
        Transmissao(JSON.stringify(reclassificacaoRetencaoValidator.Entity), reclassificacaoRetencaoValidator.controller);
    }

    reclassificacaoRetencaoValidator.new = function () {

        for (var prop in ModelItem) {
            if (ModelItem.hasOwnProperty(prop)) {
                if ($("#" + prop).length > 0)
                    if ($("#" + prop).val().indexOf("R$") >= 0)
                        reclassificacaoRetencaoValidator.Entity[prop] = $("#" + prop).val().replace(/[\.,R$ ]/g, "");
                    else
                        reclassificacaoRetencaoValidator.Entity[prop] = $("#" + prop).val();
            }
        }

        reclassificacaoRetencaoValidator.Entity.Eventos = pagamentoEvento.EntityList;
        reclassificacaoRetencaoValidator.Entity.Notas = reclassificacaoRetencaoValidator.convertNotasToList();

        reclassificacaoRetencaoValidator.Entity.TransmitirSiafem = true;


    }


    reclassificacaoRetencaoValidator.provider = function () {
        reclassificacaoRetencaoValidator.formPainelCadastrar
            .bootstrapValidator(reclassificacaoRetencaoValidator.jsonValidate)
            .on("submit", reclassificacaoRetencaoValidator.validatorHandler);

        reclassificacaoRetencaoValidator.buttonSalvar
       .on('click', reclassificacaoRetencaoValidator.saveHandler);

        reclassificacaoRetencaoValidator.buttonTransmitir
        .on('click', reclassificacaoRetencaoValidator.sendHandler);

    }

    reclassificacaoRetencaoValidator.convertNotasToList = function () {
        var notas = [];
        var cont = 0;
        $.each($("div > #Nota"), function (index, value) {
            cont = cont + 1;
            if (value.value !== "") {
                var valorNota = value.value;

                var nota = {
                    Id: 0,
                    CodigoNotaFiscal: valorNota,
                    SubempenhoId: Entity.Id,
                    Ordem: cont
                };
                notas[notas.length] = nota;
            }
        });

        return notas;
    }

    $(document).on('ready', reclassificacaoRetencaoValidator.init);

})(window, document, jQuery);