(function (window, document, $) {
    'use strict';
    var mensagemValidacao;
    var controlador = window.controller;


    window.liquidacaoValidator = {};

    liquidacaoValidator.init = function () {
        liquidacaoValidator.cacheSelectors();
        liquidacaoValidator.provider();

        ValidarPermissoes();
    }

    liquidacaoValidator.cacheSelectors = function () {
        liquidacaoValidator.body = $('body');
        liquidacaoValidator.controller = window.controller;
        liquidacaoValidator.area = window.area;

        liquidacaoValidator.controllerInclusao = 'Subempenho';
        liquidacaoValidator.controllerAnulacao = 'SubempenhoCancelamento';
        liquidacaoValidator.controllerRapInscricao = 'RapInscricao';
        liquidacaoValidator.controllerRapRequisicao = 'RapRequisicao';
        liquidacaoValidator.controllerRapAnulacao = 'RapAnulacao';

        liquidacaoValidator.route = '/' + liquidacaoValidator.area + '/' + liquidacaoValidator.controller;

        liquidacaoValidator.Entity = window.Entity;

        liquidacaoValidator.formPainelCadastrar = $("#frmPainelCadastrar");

        liquidacaoValidator.valueSelecaoSiafem = $('#transmitirSIAFEM');
        liquidacaoValidator.valueSelecaoSiafisico = $('#transmitirSIAFISICO');
        liquidacaoValidator.valueSelecaoProdesp = $('#transmitirProdesp');
        liquidacaoValidator.valueCenarioSiafemSiafisico = $('#CenarioSiafemSiafisico');
        liquidacaoValidator.valueCenarioProdesp = $('#CenarioProdesp');

        liquidacaoValidator.buttonSalvar = $("#btnSalvar");
        liquidacaoValidator.buttonTransmitir = $("#btnTransmitir");
        liquidacaoValidator.valueDescricaoObservacao2 = $("#DescricaoObservacao2");
        liquidacaoValidator.valueDescricaoObservacao3 = $("#DescricaoObservacao3");

        liquidacaoValidator.valueDescricaoEspecificacaoDespesa1 = $("#DescricaoEspecificacaoDespesa1").val();

        liquidacaoValidator.EntityId = $("#Codigo");

        liquidacaoValidator.validatorHandler = function (e) {
            liquidacaoValidator.hasError = false;
            liquidacaoValidator.classHasError = $(".has-error");
            if (liquidacaoValidator.classHasError.length) {
                liquidacaoValidator.hasError = true;
            }

            if (liquidacaoValidator.hasError == true) {
                switch (tans) {
                    case "T":
                        e.preventDefault();
                        $(".has-error:first input").focus();
                        break;
                    default:
                        e.preventDefault();
                        liquidacaoValidator.Entity.CadastroCompleto = false;
                        liquidacaoValidator.salvar();
                        break;
                }
            }
            else {
                switch (tans) {
                    case "T":
                        e.preventDefault();
                        liquidacaoValidator.Entity.CadastroCompleto = true;
                        liquidacaoValidator.transmitir();
                        break;
                    case "S":
                        e.preventDefault();
                        liquidacaoValidator.Entity.CadastroCompleto = true;
                        liquidacaoValidator.salvar();
                        break;
                    case "I":
                        e.preventDefault();
                        window.location.href = liquidacaoValidator.route + '/Edit/' + liquidacaoValidator.EntityId.val() + '?tipo=i';
                        break;
                }
            }

            e.submit;
        }

        liquidacaoValidator.saveHandler = function () {
            tans = 'S';
            liquidacaoValidator.formPainelCadastrar.data("bootstrapValidator").resetForm();
            liquidacaoValidator.formPainelCadastrar.submit();
        };

        liquidacaoValidator.sendHandler = function () {

            tans = 'T';
            liquidacaoValidator.formPainelCadastrar.data("bootstrapValidator").resetForm();
            liquidacaoValidator.formPainelCadastrar.submit();
        };
    }

    liquidacaoValidator.isProdesp = function () {
        return liquidacaoValidator.valueSelecaoProdesp.is(":checked");
    }
    liquidacaoValidator.isSiafem = function () {
        return liquidacaoValidator.valueSelecaoSiafem.is(":checked");
    }
    liquidacaoValidator.isSiafisico = function () {
        return liquidacaoValidator.valueSelecaoSiafisico.is(":checked");
    }
    liquidacaoValidator.isAnulacao = function () {
        var resultado = false;

        if (liquidacaoValidator.controller === undefined) {
            liquidacaoValidator.controller = window.controller;
        }

        if (liquidacaoValidator.controllerAnulacao === undefined) {
            liquidacaoValidator.controllerAnulacao = 'SubempenhoCancelamento';
        }

        resultado = liquidacaoValidator.controller === liquidacaoValidator.controllerAnulacao;

        return resultado;
    }

    liquidacaoValidator.isRapRequisicao = function () {
        return liquidacao.controller === liquidacao.controllerRapRequisicao;
    }

    liquidacaoValidator.jsonValidate = {
        feedbackIcons: {
            valid: "",
            invalid: "",
            validating: "",
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

            NumeroOriginalProdesp: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            if (liquidacaoValidator.valueSelecaoProdesp.is(":checked") && $("#CenarioProdesp").val() != "") {
                                return value.length > 0;
                            }
                            return true;
                        }
                    }
                }
            },

            NumeroSubempenhoProdesp: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            if (liquidacaoValidator.valueSelecaoProdesp.is(":checked") && tans == "T") {
                                return value.length > 0;
                            }
                            return true;
                        }
                    }
                }
            },


            CodigoTarefa: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            if (liquidacaoValidator.valueSelecaoProdesp.is(":checked") ||
                                liquidacaoValidator.valueSelecaoSiafisico.is(":checked") ||
                                liquidacaoValidator.valueSelecaoSiafem.is(":checked")) {
                                return value.length > 0;
                            }
                            return true;
                        }
                    }
                }
            },

            Tarefa: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            if (liquidacaoValidator.valueSelecaoProdesp.is(":checked") ||
                                liquidacaoValidator.valueSelecaoSiafisico.is(":checked") ||
                                liquidacaoValidator.valueSelecaoSiafem.is(":checked")) {
                                return value.length > 0;
                            }
                            return true;
                        }
                    }
                }
            },

            CodigoDespesa: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            if (liquidacaoValidator.valueSelecaoProdesp.is(":checked") ||
                                liquidacaoValidator.valueSelecaoSiafisico.is(":checked") ||
                                liquidacaoValidator.valueSelecaoSiafem.is(":checked")) {
                                return value.length > 0;
                            }
                            return true;
                        }
                    }
                }
            },

            ValorRealizado: {
                validators: {
                    identical: {
                        field: 'Valor',
                        message: controlador === "O Valor Realizado deve ser igual ao campo Valor"
                    },
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            var isSiafemSiafisico = liquidacaoValidator.isSiafem() || liquidacaoValidator.isSiafisico();
                            var isProdesp = liquidacaoValidator.isProdesp();

                            if (isProdesp || isSiafemSiafisico) {
                                var possuiValor = value.replace(/[\.,R$ ]/g, "") > 0;

                                var campo = $('#Valor');
                                var valorComparar = campo.val();

                                if (campo.is(":hidden")) {
                                    return true;
                                }
                                else {
                                    return value === valorComparar && possuiValor;
                                }
                            }
                            return true;
                        }
                    }
                }
            },


            //Total: {
            //    validators: {
            //        callback: {
            //            message: "Campo obrigatório não preenchido",
            //            callback: function (value) {
            //                if (liquidacaoValidator.valueSelecaoSiafem.is(":checked") || liquidacaoValidator.valueSelecaoSiafisico.is(":checked")) {
            //                    return value.replace(/[\.,R$ ]/g, "") > 0;
            //                }
            //                return true;
            //            },
            //            identical: {
            //                field: 'ValorRealizado',
            //                message: 'campos iguais'
            //            }
            //        },
            //    }
            //},

            //Total: {
            //    validators: {
            //        identical: {
            //            field: 'ValorRealizado',
            //            message: controlador === "SubempenhoCancelamento" ? "Valor total deve ser igual ao valor a Anular" : "Valor total deve ser igual ao valor realizado"
            //        },
            //    }
            //},

            Total: {
                validators: {
                    callback: {
                        message: liquidacaoValidator.isAnulacao() ? "Valor total deve ser igual ao valor a anular" : "Valor total deve ser igual ao valor realizado",
                        callback: function (value, validator, $field) {
                            value = util.removerCifra(value);
                            var valorComparar;
                            var campo;

                            if (liquidacaoValidator.isAnulacao()) {
                                campo = $('#ValorAnular');
                                valorComparar = util.removerCifra(campo.val());
                            }
                            else {
                                campo = $('#ValorRealizado');
                                valorComparar = util.removerCifra(campo.val());
                            }
                            if (campo.is(":hidden")) {
                                return true;
                            }
                            else {
                                return value === valorComparar;
                            }
                        }
                    },
                }
            },

            ValorAnular: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            if (liquidacaoValidator.valueSelecaoProdesp.is(":checked")) {
                                return value.replace(/[\.,R$ ]/g, "") > 0;
                            }
                            return true;
                        }
                    }
                }
            },
            NumeroCT: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            value = value === undefined || value === null ? "" : value;
                            var valueNE = $("#NumeroOriginalSiafemSiafisico").length > 0 ? $("#NumeroOriginalSiafemSiafisico").val() : '';

                            if (liquidacaoValidator.isSiafem()) {
                                return true;
                            }

                            if (liquidacaoValidator.isSiafisico()) {
                                var umPreenchido = value.length === 11 || valueNE.length === 11;

                                if (liquidacao.cenarioContem([1, 2, 3]) || liquidacao.cenarioContem([9, 10, 11])) {
                                    return umPreenchido;
                                }
                                else {
                                    return true;
                                }

                                //if (liquidacao.cenarioContem([1, 2, 3])) {
                                //    if (valueNE.length === 0) {
                                //        return value.length === 11;
                                //    } else {
                                //        return true;
                                //    }
                                //} else if (liquidacao.cenarioContem([9, 10, 11])) {
                                //    if (value.length === 0 && valueNE.length === 0) {
                                //        return false;
                                //    } else {
                                //        return true;
                                //    }
                                //} else {
                                //    return (value.length === 11);
                                //}
                            }
                        }
                    }
                }
            },
            NumeroOriginalSiafemSiafisico: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            value = value === undefined || value === null ? "" : value;
                            var valueCT = $("#NumeroCT").length > 0 ? $("#NumeroCT").val() : '';

                            //if (liquidacaoValidator.isAnulacao()) {
                            //    return value.length === 11;
                            //}

                            //if (liquidacao.cenarioContem([1, 2, 3])) {
                            //    return (value.length === 11) || (valueCT.length === 11);
                            //} else if (liquidacao.cenarioContem([9, 10, 11])) {
                            //    if (value.length === 0 && valueCT.length === 0) {
                            //        return false;
                            //    } else {
                            //        return value.length === 11;
                            //    }
                            //} else if (liquidacao.cenarioContem([8, 5])) {
                            //    return true;
                            //}
                            var umPreenchido = value.length === 11 || valueCT.length === 11;

                            if (liquidacao.cenarioContem([1, 2, 3]) || liquidacao.cenarioContem([9, 10, 11])) {
                                return umPreenchido;
                            }
                            else {
                                return true;
                            }


                        }
                    }
                }
            },
            NumeroCNPJCPFCredor: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            var isBec = liquidacao.verificarCenarioBec();
                            var isSiafemSiafisico = liquidacaoValidator.isSiafem() || liquidacaoValidator.isSiafisico();
                            var isNotRap = liquidacao.controller != liquidacao.controllerRapRequisicao;

                            if (!isBec && isSiafemSiafisico && isNotRap) {
                                return value.length > 0;
                            }

                            return true;
                        }
                    }
                }
            },

            CodigoGestaoCredor: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            var isBec = liquidacao.verificarCenarioBec();
                            var isSiafemSiafisico = liquidacaoValidator.isSiafem() || liquidacaoValidator.isSiafisico();
                            var isNotRap = liquidacao.controller != liquidacao.controllerRapRequisicao;
                            var credorTamanhoCorreto = $("#NumeroCNPJCPFCredor").val().length <= 6;

                            if (!isBec && isSiafemSiafisico && isNotRap && credorTamanhoCorreto) {
                                return value.length > 0;
                            }

                            return true;
                        }
                    }
                }
            },

            CodigoUnidadeGestora: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            if (liquidacaoValidator.valueSelecaoSiafem.is(":checked") ||
                                liquidacaoValidator.valueSelecaoSiafisico.is(":checked")) {
                                return value.length > 0;
                            }
                            return true;
                        }
                    }
                }
            },

            CodigoGestao: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            if (liquidacaoValidator.valueSelecaoSiafem.is(":checked") ||
                                liquidacaoValidator.valueSelecaoSiafisico.is(":checked")) {
                                return value.length > 0;
                            }
                            return true;
                        }
                    }
                }
            },
            TipoEventoId: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            return value.length > 0;
                        }
                    }
                }
            },
            NumeroMedicao: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            if ((liquidacaoValidator.valueSelecaoSiafem.is(":checked") &&
                                liquidacaoValidator.valueSelecaoProdesp.is(":checked")) && ($('#Contrato').val() != null && $('#Contrato').val() != "")) {
                                return value.length > 0;
                            }
                            return true;
                        }
                    }
                }
            },

            Valor: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            var isSiafemSiafisico = liquidacaoValidator.isSiafem() || liquidacaoValidator.isSiafisico();
                            var isProdesp = liquidacaoValidator.isProdesp();
                            var isRapRequisicao = liquidacaoValidator.isRapRequisicao();
                            var isNotNlPregaoBec = !liquidacao.verificarCenarioNlPregaoBec();


                            if (isSiafemSiafisico || (isProdesp && isRapRequisicao)) {
                                return value.replace(/[\.,R$ ]/g, "") > 0;
                            }
                            return true;
                        }
                    }
                }
            },

            CodigoEvento: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            if (liquidacaoValidator.valueSelecaoSiafem.is(":checked") ||
                                liquidacaoValidator.valueSelecaoSiafisico.is(":checked")) {
                                return value.length > 0;
                            }
                            return true;
                        }
                    }
                }
            },

            MesMedicao: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            if (liquidacaoValidator.valueSelecaoSiafem.is(":checked") ||
                                liquidacaoValidator.valueSelecaoSiafisico.is(":checked")) {
                                return value.length > 0;
                            }
                            return true;
                        }
                    }
                }
            },

            AnoMedicao: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            if (liquidacaoValidator.valueSelecaoSiafem.is(":checked") ||
                                liquidacaoValidator.valueSelecaoSiafisico.is(":checked")) {
                                return value.length > 0;
                            }
                            return true;
                        }
                    }
                }
            },

            ProgramaId: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            if (liquidacaoValidator.valueSelecaoSiafem.is(":checked")) {
                                return value.length > 0;
                            }
                            return true;
                        }
                    }
                }
            },

            Percentual: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            if (liquidacaoValidator.valueSelecaoSiafem.is(":checked")) {
                                return value.length > 0;
                            }
                            return true;
                        }
                    }
                }
            },

            ValorCaucionado: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            if (liquidacao.controller == liquidacao.controllerRapRequisicao) {
                                if (liquidacaoValidator.valueSelecaoProdesp.is(":checked")) {
                                    if ($("#NumeroGuia").val().length > 0) {
                                        return value.replace(/[\.,R$ ]/g, "") > 0;
                                    }
                                    return true;
                                }
                                return true;
                            } else {

                                return true;
                            }
                        }
                    }
                }
            },

            NumeroGuia: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            if (liquidacao.controller == liquidacao.controllerRapRequisicao) {
                                if (liquidacaoValidator.valueSelecaoProdesp.is(":checked")) {
                                    if ($("#ValorCaucionado").val().replace(/[\.,R$ ]/g, "") > 0) {
                                        return value.length > 0;
                                    }
                                    return true;
                                }
                                return true;
                            } else {

                                return true;
                            }
                        }
                    }
                }
            },



            DescricaoObservacao1: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            if ((liquidacaoValidator.valueSelecaoSiafem.is(":checked") ||
                                liquidacaoValidator.valueSelecaoSiafisico.is(":checked")) && (
                                liquidacaoValidator.valueDescricaoObservacao2.val() === "" &&
                                liquidacaoValidator.valueDescricaoObservacao3.val() === "")) {
                                return value.length > 0;
                            }
                            return true;
                        }
                    }
                }
            },

            Nota01: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            if (liquidacaoValidator.valueSelecaoSiafem.is(":checked") ||
                                liquidacaoValidator.valueSelecaoSiafisico.is(":checked")) {
                                return value.length > 0;
                            }
                            return true;
                        }
                    }
                }
            },


            CenarioSiafemSiafisico: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            if (liquidacaoValidator.valueSelecaoSiafem.is(":checked")) {
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
                        message: 'Campo obrigatório não preenchido'
                    }
                }
            },
            DataEntrega: {
                validators: {
                    callback: {
                        message: 'Campo obrigatório não preenchido',
                        callback: function (value) {
                            if ($("#transmitirSIAFISICO").is(":checked") && tans == "T") { return value.length > 0; }
                            else { return true; }
                        },
                        callback1: {
                            message: "Data de Entrega deve ser maior ou igual que Data de Emissão",
                            callback1: function (value) {
                                if (value.length < 10)
                                    return true;

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
                }
            },
            NaturezaSubempenhoId: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            if (liquidacaoValidator.valueSelecaoProdesp.is(":checked") && ($('#Contrato').val() != null && $('#Contrato').val() != "")) {
                                return value.length > 0;
                            }
                            return true;
                        }
                    }
                }
            },

            Referencia: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            if (liquidacaoValidator.valueSelecaoSiafem.is(":checked") && liquidacaoValidator.valueSelecaoProdesp.is(":checked") || liquidacaoValidator.valueSelecaoSiafisico.is(":checked")) {
                                return value.length > 0;
                            }
                            return true;
                        }
                    }
                }
            },

            NlReferencia: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            if (liquidacaoValidator.controller == liquidacaoValidator.controllerAnulacao) {
                                return value.length > 0;
                            }
                            return true;
                        }
                    }
                }
            },

            NumeroProcesso: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            if (liquidacaoValidator.valueSelecaoProdesp.is(":checked")) {
                                return value.length > 0;
                            }
                            return true;
                        }
                    }
                }
            },

            DescricaoAutorizadoSupraFolha: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            if (liquidacaoValidator.valueSelecaoProdesp.is(":checked")) {
                                return value.length > 0;
                            }
                            return true;
                        }
                    }
                }
            },

            CodigoEspecificacaoDespesa: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            if (liquidacaoValidator.valueSelecaoProdesp.is(":checked")) {
                                return value.length > 0;
                            }
                            return true;
                        }
                    }
                }
            },

            DescricaoEspecificacaoDespesa1: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            var algoPreenchido = liquidacaoValidator.valueDescricaoEspecificacaoDespesa1 !== ""
                            || liquidacaoValidator.valueDescricaoEspecificacaoDespesa2 !== ""
                            || liquidacaoValidator.valueDescricaoEspecificacaoDespesa3 !== ""
                            || liquidacaoValidator.valueDescricaoEspecificacaoDespesa4 !== ""
                            || liquidacaoValidator.valueDescricaoEspecificacaoDespesa5 !== ""
                            || liquidacaoValidator.valueDescricaoEspecificacaoDespesa6 !== ""
                            || liquidacaoValidator.valueDescricaoEspecificacaoDespesa7 !== ""
                            || liquidacaoValidator.valueDescricaoEspecificacaoDespesa8 !== "";


                            if (liquidacaoValidator.valueSelecaoProdesp.is(":checked")) {
                                return algoPreenchido;
                            }
                            return true;
                        }
                    }
                }
            },

            CodigoAutorizadoAssinatura: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            if (liquidacaoValidator.valueSelecaoProdesp.is(":checked")) {
                                return value.length > 0;
                            }
                            return true;
                        }
                    }
                }
            },

            CodigoExaminadoAssinatura: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            if (liquidacaoValidator.valueSelecaoProdesp.is(":checked")) {
                                return value.length > 0;
                            }
                            return true;
                        }
                    }
                }
            },

            CodigoResponsavelAssinatura: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            if (liquidacaoValidator.valueSelecaoProdesp.is(":checked")) {
                                return value.length > 0;
                            }
                            return true;
                        }
                    }
                }
            },

            TipoObraId: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            if ((liquidacaoValidator.valueSelecaoSiafem.is(":checked") ||
                                liquidacaoValidator.valueSelecaoProdesp.is(":checked")) &&
                                liquidacaoValidator.valueCenarioSiafemSiafisico.val() === "7" ||
                                liquidacaoValidator.valueCenarioSiafemSiafisico.val() === "8") {
                                return value.length > 0;
                            }
                            return true;
                        }
                    }
                }
            },
            CodigoTipoDeObra: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            if ((liquidacaoValidator.valueSelecaoSiafem.is(":checked"))) {
                                return value.length > 0;
                            }
                            return true;
                        }
                    }
                }
            },


            TipoServicoId: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            if (liquidacaoValidator.valueSelecaoSiafem.is(":checked")) {
                                return value.length > 0;
                            }
                            return true;
                        }
                    }
                }
            },

            NumeroObra: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            if (liquidacaoValidator.valueSelecaoSiafem.is(":checked") &&
                                liquidacaoValidator.valueCenarioSiafemSiafisico.val() === "7" ||
                                liquidacaoValidator.valueCenarioSiafemSiafisico.val() === "8") {
                                return value.length > 0;
                            }
                            return true;
                        }
                    }
                }
            },
            CodigoUnidadeGestoraObra: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            if ((liquidacaoValidator.valueSelecaoSiafem.is(":checked")) &&
                                liquidacaoValidator.valueCenarioSiafemSiafisico.val() === "7" ||
                                liquidacaoValidator.valueCenarioSiafemSiafisico.val() === "8") {
                                return value.length > 0;
                            }
                            return true;
                        }
                    }
                }
            },

            CodigoAplicacaoObra: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            if (liquidacaoValidator.valueSelecaoSiafem.is(":checked") ||
                                liquidacaoValidator.valueSelecaoProdesp.is(":checked")) {
                                return value.length > 0;
                            }
                            return true;
                        }
                    }
                }
            },

            DataRealizado: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            if (liquidacaoValidator.valueSelecaoProdesp.is(":checked") && $("#NumeroRecibo").val().length == 0 && $("#CenarioProdesp").val() != "") {
                                return value.length > 0;
                            }
                            return true;
                        }
                    }
                }
            },

            ValorSubempenhar: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            if (liquidacaoValidator.valueSelecaoSiafem.is(":checked") ||
                                liquidacaoValidator.valueSelecaoSiafisico.is(":checked")) {
                                return value.replace(/[., R$]/g, "").length > 0;
                            }
                            return true;
                        }
                    }
                }
            },

            DescricaoPrazoPagamento: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            if (liquidacaoValidator.valueSelecaoProdesp.is(":checked") && $("#NumeroRecibo").val().length == 0) {
                                return value.length > 0;
                            }
                            return true;
                        }
                    }
                }
            },

            NumeroRequisicaoRap: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            if (liquidacaoValidator.valueSelecaoProdesp.is(":checked") || liquidacaoValidator.valueSelecaoSiafem.is(":checked")) {
                                return value.length > 0;
                            }
                            return true;
                        }
                    }
                }
            },

            ValorSaldoAnteriorSubempenho: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            if (liquidacaoValidator.valueSelecaoSiafem.is(":checked") || liquidacaoValidator.valueSelecaoProdesp.is(":checked")) {
                                if (liquidacaoValidator.controller !== liquidacaoValidator.controllerRapAnulacao) {
                                    return value.replace(/[., R$]/g, "").length > 0;
                                }
                            }
                            return true;
                        }
                    }
                }
            },

            ValorAnulado: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {
                            if (liquidacaoValidator.valueSelecaoSiafem.is(":checked") || liquidacaoValidator.valueSelecaoProdesp.is(":checked")) {
                                return value.replace(/[., R$]/g, "").length > 0;
                            }
                            return true;
                        }
                    }
                }
            },

            CodigoCredorOrganizacao: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {

                            if (liquidacaoValidator.valueCenarioProdesp.val() === "RAPOraganizao7" && liquidacaoValidator.valueSelecaoProdesp.is(":checked") && liquidacao.controller === liquidacao.controllerRapRequisicao) {
                                return value.length > 0;
                            }
                            return true;
                        }
                    }
                }
            },


            NumeroCNPJCPFFornecedor: {
                validators: {
                    callback: {
                        message: "Campo obrigatório não preenchido",
                        callback: function (value) {

                            if (liquidacaoValidator.valueCenarioProdesp.val() === "RAPOraganizao7" && liquidacaoValidator.valueSelecaoProdesp.is(":checked") && liquidacao.controller === liquidacao.controllerRapRequisicao) {
                                return value.length > 0;
                            }
                            return true;
                        }
                    }
                }
            }

        }
    };

    liquidacaoValidator.salvar = function () {
        if (navigator.onLine !== true) {
            AbrirModal("Erro de conexão");
            return false;
        }

        liquidacaoValidator.new();

        $.ajax({
            datatype: "json",
            type: "Post",
            url: liquidacaoValidator.route + "/Save",
            data: JSON.stringify(liquidacaoValidator.Entity),
            contentType: "application/json; charset=utf-8",
            beforeSend: function () {
                waitingDialog.show("Salvando");
            },
            success: function (dados) {
                if (dados.Status === "Sucesso") {
                    liquidacaoValidator.EntityId.val(dados.Id);
                    $.confirm({
                        text: liquidacaoValidator.controller + " " + AcaoRealizada(tipoAcao, liquidacaoValidator.controller) + " com sucesso!",
                        title: "Confirmação",
                        cancel: function () {
                            window.location.href = liquidacaoValidator.route + "/Edit/" + dados.Id + "?tipo=a";
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
                liquidacaoValidator.formPainelCadastrar.data("bootstrapValidator").resetForm();
                waitingDialog.hide();
            }
        });
    }

    liquidacaoValidator.transmitir = function () {
        if (navigator.onLine !== true) {
            AbrirModal("Erro de conexão");
            return false;
        }

        liquidacaoValidator.new();

        ModelItem = liquidacaoValidator.Entity;

        Transmissao(JSON.stringify(liquidacaoValidator.Entity), liquidacaoValidator.controller);

    }

    liquidacaoValidator.new = function () {

        // PROPERTIES : COMMON
        liquidacaoValidator.Entity.Id = $("#Codigo").val();

        //pesquisa saldo rap

        if (liquidacaoValidator.controller === liquidacao.controllerRapInscricao || liquidacaoValidator.controller === liquidacao.controllerRapRequisicao) {
            liquidacaoValidator.Entity.NumeroAnoExercicio = $("#AnoExercicio").val();
        }
        else {
            liquidacaoValidator.Entity.AnoExcercicio = $("#AnoExercicio").val();
        }


        liquidacaoValidator.Entity.DataCadastro = $("#DataCadastro").val();
        liquidacaoValidator.Entity.DataTransmitidoProdesp = $("#DataTransmitidoProdesp").val();
        liquidacaoValidator.Entity.DataTransmitidoSiafemSiafisico = $("#DataTransmitidoSiafemSiafisico").val();

        //pesquisa tipo apropriacao
        liquidacaoValidator.Entity.NumeroContrato = $("#Contrato").val().replace(/[\.-]/g, "");
        liquidacaoValidator.Entity.TransmitirProdesp = liquidacaoValidator.valueSelecaoProdesp.is(":checked");
        liquidacaoValidator.Entity.TransmitirSiafem = liquidacaoValidator.valueSelecaoSiafem.is(":checked");
        liquidacaoValidator.Entity.TransmitirSiafisico = liquidacaoValidator.valueSelecaoSiafisico.is(":checked");
        liquidacaoValidator.Entity.NumeroProdesp = $("#NumeroProdesp").val();
        liquidacaoValidator.Entity.NumeroSiafemSiafisico = $("#NumeroSiafemSiafisico").val();
        liquidacaoValidator.Entity.CenarioSiafemSiafisico = $("#CenarioSiafemSiafisico").val();

        liquidacaoValidator.Entity.DataTransmitidoProdesp = $("#DataTransmitidoProdesp").val();
        liquidacaoValidator.Entity.DataTransmitidoSiafemSiafisico = $("#DataTransmitidoSiafemSiafisico").val();
        liquidacaoValidator.Entity.NomeCredor = $("#NomeCredor").val();

        if (liquidacaoValidator.controller !== liquidacaoValidator.controllerAnulacao) {
            liquidacaoValidator.Entity.ValorRealizado = $("#Valor").val().replace("R$ ", "").replace(/[.,]/g, "");

        } else if (liquidacaoValidator.valueSelecaoProdesp.is(":checked")) {
            liquidacaoValidator.Entity.ValorAnular = $("#ValorAnular").val().replace("R$ ", "").replace(/[.,]/g, "");
        }

        if (liquidacaoValidator.controller === liquidacaoValidator.controllerAnulacao) {
            //if ($("#ValorRealizado").val().length > 0 ){ //Todo : verificar a existencia desse campo na tela de cancelamento de subempenho, não existe na documentacao
            //    liquidacaoValidator.Entity.Valor = $("#ValorRealizado").val().replace("R$ ", "").replace(/[.,]/g, "");
            //}
            if ($("#ValorAnular").val().length > 0) { // valor anular aparece duas vezes na tela porém outra como valor 
                liquidacaoValidator.Entity.ValorAnular = $("#ValorAnular").val().replace("R$ ", "").replace(/[.,]/g, "");
            }
        }

        liquidacaoValidator.Entity.NumeroOriginalProdesp = $("#NumeroEmpenhoProdesp").val();

        liquidacaoValidator.Entity.NumeroSubempenhoProdesp = $("#NumeroSubempenhoProdesp").val();

        liquidacaoValidator.Entity.CenarioProdesp = $("#CenarioProdesp").val();

        //inscrição
        liquidacaoValidator.Entity.TipoServicoId = $('#TipoServicoId').val();

        //apropriacao
        liquidacaoValidator.Entity.NlReferencia = $('#NlReferencia').val();
        liquidacaoValidator.Entity.NumeroOriginalSiafemSiafisico = $("#NumeroOriginalSiafemSiafisico").val();
        liquidacaoValidator.Entity.NumeroCT = $("#NumeroCT").val();
        liquidacaoValidator.Entity.CodigoUnidadeGestora = $("#CodigoUnidadeGestora").val();
        liquidacaoValidator.Entity.CodigoGestao = $("#CodigoGestao").val();

        if ($("#Valor").length > 0)
            liquidacaoValidator.Entity.Valor = $("#Valor").val().replace("R$ ", "").replace(/[.,]/g, "");

        liquidacaoValidator.Entity.CodigoAplicacaoObra = $("#CodigoAplicacaoObra").val();
        liquidacaoValidator.Entity.DataEmissao = $("#DataEmissao").val();
        liquidacaoValidator.Entity.TipoEventoId = $("#TipoEventoId").val();

        if (liquidacao.valueCenarioSiafemSiafisico.val() === "1" || liquidacao.valueCenarioSiafemSiafisico.val() === "4") {
            liquidacaoValidator.Entity.CodigoEvento = $("#CodigoEvento").val();
        } else {
            liquidacaoValidator.Entity.CodigoEvento = $("#NumeroEvento").val();
        }

        liquidacaoValidator.Entity.NumeroCNPJCPFCredor = $("#NumeroCNPJCPFCredor").val();
        liquidacaoValidator.Entity.CodigoGestaoCredor = $("#CodigoGestaoCredor").val();
        liquidacaoValidator.Entity.Classificacao = $("#Classificacao").val();

        liquidacaoValidator.Entity.MesMedicao = $("#MesMedicao").val();
        liquidacaoValidator.Entity.AnoMedicao = $("#AnoMedicao").val();

        //liquidacaoValidator.Entity.Percentual = $("#Percentual").val();
        if ($("#Percentual").length > 0)
            liquidacaoValidator.Entity.Percentual = $("#Percentual").val().replace(/[.,]/g, "");

        liquidacaoValidator.Entity.TipoObraId = $("#TipoObraId").val();
        liquidacaoValidator.Entity.NumeroObra = $("#NumeroObra").val();
        liquidacaoValidator.Entity.CodigoUnidadeGestoraObra = $("#CodigoUnidadeGestoraObra").val();

        if (controlador === "RapInscricao" || controlador === "RapRequisicao") {
            liquidacaoValidator.Entity.RegionalId = $("#Orgao").val();
        } else {
            liquidacaoValidator.Entity.RegionalId = $("#RegionalId").val();
        }

        //observacao
        liquidacaoValidator.Entity.DescricaoObservacao1 = $('#DescricaoObservacao1').val() != "" ? $('#DescricaoObservacao1').val() : " ";
        liquidacaoValidator.Entity.DescricaoObservacao2 = $('#DescricaoObservacao2').val() != "" ? $('#DescricaoObservacao2').val() : " ";
        liquidacaoValidator.Entity.DescricaoObservacao3 = $('#DescricaoObservacao3').val() != "" ? $('#DescricaoObservacao3').val() : " ";
        //despesa
        liquidacaoValidator.Entity.NumeroProcesso = $("#NumeroProcesso").val();
        liquidacaoValidator.Entity.DescricaoAutorizadoSupraFolha = $("#DescricaoAutorizadoSupraFolha").val();
        liquidacaoValidator.Entity.CodigoEspecificacaoDespesa = $("#EspecDespesa").val();
        liquidacaoValidator.Entity.DescricaoEspecificacaoDespesa1 = $("#DescricaoEspecificacaoDespesa1").val();
        liquidacaoValidator.Entity.DescricaoEspecificacaoDespesa2 = $("#DescricaoEspecificacaoDespesa2").val();
        liquidacaoValidator.Entity.DescricaoEspecificacaoDespesa3 = $("#DescricaoEspecificacaoDespesa3").val();
        liquidacaoValidator.Entity.DescricaoEspecificacaoDespesa4 = $("#DescricaoEspecificacaoDespesa4").val();
        liquidacaoValidator.Entity.DescricaoEspecificacaoDespesa5 = $("#DescricaoEspecificacaoDespesa5").val();
        liquidacaoValidator.Entity.DescricaoEspecificacaoDespesa6 = $("#DescricaoEspecificacaoDespesa6").val();
        liquidacaoValidator.Entity.DescricaoEspecificacaoDespesa7 = $("#DescricaoEspecificacaoDespesa7").val();
        liquidacaoValidator.Entity.DescricaoEspecificacaoDespesa8 = $("#DescricaoEspecificacaoDespesa8").val();
        liquidacaoValidator.Entity.DescricaoEspecificacaoDespesa9 = $("#DescricaoEspecificacaoDespesa9").val();
        liquidacaoValidator.Entity.NlRetencaoInss = $("#NlRetencaoInss").val();

        liquidacaoValidator.Entity.DescricaoPrazoPagamento = $("#PrazoPagamento").val();

        if (liquidacaoValidator.controller === liquidacao.controllerRapRequisicao) {
            liquidacaoValidator.Entity.NumeroSubempenho = $("#NumeroSubempenho").val();
            liquidacaoValidator.Entity.DescricaoPrazoPagamento = $("#DescricaoPrazoPagamento").val();
        }

        liquidacaoValidator.Entity.Lista = $("#Lista").val();
        //referência
        liquidacaoValidator.Entity.Referencia = $("#Referencia").val();
        liquidacaoValidator.Entity.ReferenciaDigitada = $("#ReferenciaDigitada").val();

        //
        liquidacaoValidator.Entity.CodigoAutorizadoAssinatura = $("#CodAssAutorizado").val();
        liquidacaoValidator.Entity.CodigoAutorizadoGrupo = $("#txtAutorizadoGrupo").val();
        liquidacaoValidator.Entity.CodigoAutorizadoOrgao = $("#txtAutorizadoOrgao").val();
        liquidacaoValidator.Entity.DescricaoAutorizadoCargo = $("#txtAutorizadoCargo").val();
        liquidacaoValidator.Entity.NomeAutorizadoAssinatura = $("#txtAutorizadoNome").val();
        liquidacaoValidator.Entity.CodigoExaminadoAssinatura = $("#CodAssExaminado").val();
        liquidacaoValidator.Entity.CodigoExaminadoGrupo = $("#txtExaminadoGrupo").val();
        liquidacaoValidator.Entity.CodigoExaminadoOrgao = $("#txtExaminadoOrgao").val();
        liquidacaoValidator.Entity.DescricaoExaminadoCargo = $("#txtExaminadoCargo").val();
        liquidacaoValidator.Entity.NomeExaminadoAssinatura = $("#txtExaminadoNome").val();
        liquidacaoValidator.Entity.CodigoResponsavelAssinatura = $("#CodAssResponsavel").val();
        liquidacaoValidator.Entity.CodigoResponsavelGrupo = $("#txtResponsavelGrupo").val();
        liquidacaoValidator.Entity.CodigoResponsavelOrgao = $("#txtResponsavelOrgao").val();
        liquidacaoValidator.Entity.DescricaoResponsavelCargo = $("#txtResponsavelCargo").val();
        liquidacaoValidator.Entity.NomeResponsavelAssinatura = $("#txtResponsavelNome").val();


        liquidacaoValidator.Entity.Eventos = liquidacaoEvento.EntityList;
        liquidacaoValidator.Entity.Notas = liquidacaoValidator.convertNotasToList();
        liquidacaoValidator.Entity.Itens = liquidacaoItem.EntityList;
        liquidacaoValidator.Entity.CodigoTipoDeObra = $("#CodigoTipoDeObra").val();
        // PROPERTIES : EXCLUSIVE

        liquidacaoValidator.Entity.CodigoTarefa = $("#CodigoTarefa").val();
        liquidacaoValidator.Entity.Tarefa = $("#Tarefa").val();
        liquidacaoValidator.Entity.CodigoDespesa = $("#CodigoDespesa").val();
        liquidacaoValidator.Entity.NumeroRecibo = $("#NumeroRecibo").val();
        liquidacaoValidator.Entity.PrazoPagamento = $("#PrazoPagamento").val();
        liquidacaoValidator.Entity.DataRealizado = $("#DataRealizado").val();
        liquidacaoValidator.Entity.CodigoNotaFiscalProdesp = $("#CodigoNotaFiscalProdesp").val();
        liquidacaoValidator.Entity.NumeroMedicao = $("#NumeroMedicao").val();
        liquidacaoValidator.Entity.NaturezaSubempenhoId = $("#NaturezaSubempenhoId").val();
        liquidacaoValidator.Entity.NumeroEmpenho = $("#NumeroEmpenho").val();

        if (liquidacaoValidator.controller === liquidacao.controllerRapInscricao || liquidacaoValidator.controller === liquidacao.controllerRapRequisicao || liquidacaoValidator.controller === liquidacao.controllerRapAnulacao) {
            liquidacaoValidator.Entity.NaturezaId = $("#Natureza").val();
            if ($("#Natureza").val() !== null && $("#Natureza").val() !== undefined) {
                liquidacaoValidator.Entity.CEDId = $('#Natureza option:selected').text().charAt(0);
            }
            //liquidacaoValidator.Entity.CodigoCredorOrganizacao = $("#CodigoCredorOrganizacaoId").val();
            liquidacaoValidator.Entity.CodigoCredorOrganizacao = $("#CodigoCredorOrganizacao").val();
            liquidacaoValidator.Entity.ProgramaId = $("#Programa").val();
            liquidacaoValidator.Entity.NumeroCNPJCPFFornecedor = $("#NumeroCNPJCPFFornecedor").val();
        } else {
            liquidacaoValidator.Entity.NaturezaId = $("#NaturezaId").val();
            liquidacaoValidator.Entity.CodigoCredorOrganizacao = $("#CodigoCredorOrganizacaoId").val();
            liquidacaoValidator.Entity.ProgramaId = $("#ProgramaId").val();
            liquidacaoValidator.Entity.NumeroCNPJCPFFornecedor = $("#NumeroCNPJCPFFornecedorId").val();
        }

        liquidacaoValidator.Entity.FonteId = $("#FonteId").val();


        if (liquidacaoValidator.controller !== liquidacao.controllerRapAnulacao) {
            liquidacaoValidator.Entity.NumeroGuia = $("#NumeroGuia").val();
            liquidacaoValidator.Entity.QuotaGeralAutorizadaPor = $("#QuotaGeralAutorizadaPor").val();
            if ($("#ValorCaucionado").length > 0)
                liquidacaoValidator.Entity.ValorCaucionado = $("#ValorCaucionado").val().replace(/[., R$]/g, "");
            if ($("#ValorRealizado").length > 0)
                liquidacaoValidator.Entity.ValorRealizado = $("#ValorRealizado").val().replace(/[., R$]/g, "");
            liquidacaoValidator.Entity.NumeroOriginalProdesp = $("#NumeroOriginalProdesp").val();
        } else {
            liquidacaoValidator.Entity.NumeroRequisicaoRap = $("#NumeroRequisicaoRap").val();
            liquidacaoValidator.Entity.ValorSaldoAnteriorSubempenho = $("#ValorSaldoAnteriorSubempenho").val().replace(/[., R$]/g, "");
            liquidacaoValidator.Entity.ValorAnulado = $("#ValorAnulado").val().replace(/[., R$]/g, "");
            liquidacaoValidator.Entity.ValorSaldoAposAnulacao = $("#ValorSaldoAposAnulacao").val().replace(/[., R$]/g, "");

        }
    }

    liquidacaoValidator.convertNotasToList = function () {
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

    liquidacaoValidator.provider = function () {
        liquidacaoValidator.formPainelCadastrar
            .bootstrapValidator(liquidacaoValidator.jsonValidate)
            .on("submit", liquidacaoValidator.validatorHandler);

        liquidacaoValidator.buttonSalvar
            .on('click', liquidacaoValidator.saveHandler);

        liquidacaoValidator.buttonTransmitir
            .on('click', liquidacaoValidator.sendHandler);
    }

    $(document).on('ready', liquidacaoValidator.init);

})(window, document, jQuery);