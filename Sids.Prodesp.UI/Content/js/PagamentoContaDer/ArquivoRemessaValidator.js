var tans = "";
var tipoContrato = "0304";

(function (window, document, $) {
    'use strict';

    window.ArquivoRemessaValidator = {};

    ArquivoRemessaValidator.init = function () {
        ArquivoRemessaValidator.cacheSelectors();
        ArquivoRemessaValidator.provider();
    }

 

    ArquivoRemessaValidator.cacheSelectors = function () {
        ArquivoRemessaValidator.body = $('body');
        ArquivoRemessaValidator.formPainelCadastrar = $("#frmPainelCadastrar");

        ArquivoRemessaValidator.buttonSalvar = $("#btnSalvar");
        ArquivoRemessaValidator.buttonTransmitir = $("#btnTransmitir");
        ArquivoRemessaValidator.Entity = window.Entity;

        ArquivoRemessaValidator.controller = window.controller;
        ArquivoRemessaValidator.EntityId = $("#Codigo");

        ArquivoRemessaValidator.validatorHandler = function (e) {
            ArquivoRemessaValidator.hasError = false;
            ArquivoRemessaValidator.classHasError = $(".has-error");
            if (ArquivoRemessaValidator.classHasError.length) {
                ArquivoRemessaValidator.hasError = true;
            }

            if (ArquivoRemessaValidator.hasError == true) {
                switch (tans) {
                    case "T":
                        e.preventDefault();
                        $(".has-error:first input").focus();
                        break;
                    default:
                        e.preventDefault();
                        ArquivoRemessaValidator.Entity.CadastroCompleto = false;
                        ArquivoRemessaValidator.salvar();
                        break;
                }
            }
            else {
                switch (tans) {
                    case "T":
                        e.preventDefault();
                        ArquivoRemessaValidator.Entity.CadastroCompleto = true;
                        ArquivoRemessaValidator.transmitir();
                        break;
                    case "S":
                        e.preventDefault();
                        ArquivoRemessaValidator.Entity.CadastroCompleto = true;
                        ArquivoRemessaValidator.salvar();
                        break;
                    default:
                        e.preventDefault();

                        window.location.href = '/PagamentoContaDer/' + (obj == "/ArquivoRemessa/ArquivoRemessa" ? "ArquivoRemessa" : obj) + '/Edit/' + ModelItem.Id + '?tipo=a';
                        
                        break;
                }
            }
            e.submit;
        }

        ArquivoRemessaValidator.saveHandler = function () {
            tans = 'S';
            ArquivoRemessaValidator.formPainelCadastrar.data("bootstrapValidator").resetForm();
            ArquivoRemessaValidator.formPainelCadastrar.submit();
        };

        ArquivoRemessaValidator.sendHandler = function () {
            tans = 'T';
            ArquivoRemessaValidator.formPainelCadastrar.data("bootstrapValidator").resetForm();
            ArquivoRemessaValidator.formPainelCadastrar.submit();
        };
    }

    ArquivoRemessaValidator.jsonValidate = {
        feedbackIcons: {
            valid: "",
            invalid: "",
            validating: ""
        },
        fields: {


            CodigoConta: {
                validators: {
                    notEmpty: {
                        message: "Campo obrigatório não preenchido"
                    }
                }
            },
            DataPreparacao: {
                validators: {
                    notEmpty: {
                        message: "Campo obrigatório não preenchido"
                    }
                }
            },
            DataPagamento: {
                validators: {
                    notEmpty: {
                        message: "Campo obrigatório não preenchido"
                    }
                }
            },
            CodigoAutorizadoAssinatura: {
                validators: {
                    notEmpty: {
                        message: "Campo obrigatório não preenchido"
                    }
                }
            },
            CodigoExaminadoAssinatura: {
                validators: {
                    notEmpty: {
                        message: "Campo obrigatório não preenchido"
                    }
                }
            }

        }
    };

    ArquivoRemessaValidator.salvar = function () {
        if (navigator.onLine !== true) {
            AbrirModal("Erro de conexão");
            return false;
        }

        ArquivoRemessaValidator.new();

        $.ajax({
            datatype: "json",
            type: "Post",
            url: "/PagamentoContaDer/ArquivoRemessa/Save",
            data: JSON.stringify(ArquivoRemessaValidator.Entity),
            contentType: "application/json; charset=utf-8",
            beforeSend: function () {
                waitingDialog.show("Salvando");
            },
            success: function (dados) {
                if (dados.Status === "Sucesso") {
                    ArquivoRemessaValidator.EntityId.val(dados.Id);
                    $.confirm({
                        text: "Preparação de Arquivo de Remessa " + AcaoRealizada(tipoAcao, ArquivoRemessaValidator.controller) + " com sucesso!",
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
                ArquivoRemessaValidator.formPainelCadastrar.data("bootstrapValidator").resetForm();
                waitingDialog.hide();
            }
        });
    }

    ArquivoRemessaValidator.transmitir = function () {
        if (navigator.onLine !== true) {
            AbrirModal("Erro de conexão");
            return false;
        }

        ArquivoRemessaValidator.new();
        ModelItem = ArquivoRemessaValidator.Entity;

        var str = ModelItem.DataPreparacao;
        var date = new Date(str.split('/').reverse().join('/'));
        var novaData = new Date();
 
        if (date > novaData)
        {
            AbrirModal("A data da Preparação da OP deve ser menor ou igual à data do processamento.")
            return false;
        }




        Transmissao(JSON.stringify(ArquivoRemessaValidator.Entity), ArquivoRemessaValidator.controller);




    }

    ArquivoRemessaValidator.new = function () {

        //preparacaoPagamentoValidator.Entity.PreparacaoPagamentoTipoId = $("#PreparacaoPagamentoTipoId").val(),
        ArquivoRemessaValidator.Entity.CodigoConta = $("#CodigoConta").val(),


            ArquivoRemessaValidator.Entity.DataPreparacao= $("#DataPreparacao").val(),
           ArquivoRemessaValidator.Entity.DataPagamento = $("#DataPagamento").val(),

           ArquivoRemessaValidator.Entity.NomeAssinatura = $("#txtAutorizadoNome").val(),
           ArquivoRemessaValidator.Entity.CodigoAssinatura = $("#CodAssAutorizado").val(),
           ArquivoRemessaValidator.Entity.CodigoGrupoAssinatura = $("#txtAutorizadoGrupo").val(),
           ArquivoRemessaValidator.Entity.CodigoOrgaoAssinatura = $("#txtAutorizadoOrgao").val(),
           ArquivoRemessaValidator.Entity.DesCargo = $("#txtAutorizadoCargo").val(),



           ArquivoRemessaValidator.Entity.NomeContraAssinatura = $("#txtExaminadoNome").val(),
           ArquivoRemessaValidator.Entity.CodigoContraAssinatura = $("#CodAssExaminado").val(),
           ArquivoRemessaValidator.Entity.CodigoContraGrupoAssinatura = $("#txtExaminadoGrupo").val(),
           ArquivoRemessaValidator.Entity.CodigoContraOrgaoAssinatura = $("#txtExaminadoOrgao").val(),
           ArquivoRemessaValidator.Entity.DesContraCargo = $("#txtExaminadoCargo").val(),


                ArquivoRemessaValidator.Entity.Banco = $("#NumeroBancoPagto").val(),
                ArquivoRemessaValidator.Entity.Agencia = $("#NumeroAgenciaPagto").val(),
                ArquivoRemessaValidator.Entity.NumeroConta = $("#NumeroContaPagto").val()



    }


    ArquivoRemessaValidator.provider = function () {
        ArquivoRemessaValidator.formPainelCadastrar
            .bootstrapValidator(ArquivoRemessaValidator.jsonValidate)
            .on("submit", ArquivoRemessaValidator.validatorHandler);

        ArquivoRemessaValidator.buttonSalvar
       .on('click', ArquivoRemessaValidator.saveHandler);

        ArquivoRemessaValidator.buttonTransmitir
        .on('click', ArquivoRemessaValidator.sendHandler);

    }


    $(document).on('ready', ArquivoRemessaValidator.init);

})(window, document, jQuery);