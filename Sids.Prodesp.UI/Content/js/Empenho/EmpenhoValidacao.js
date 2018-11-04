
var Municipios;
var ListaMunicipios;
$(document).ready(function () {

    var tipoInvalido = "Tipo de dados invalido";
    var campoVazio = "Campo obrigatório não preenchido";
    var valorDiferente = "O valor total e o preço total do item não conferem";
    var validacao = "Campo obrigatório não preenchido";
    $("#frmPainelCadastrar").bootstrapValidator({
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
            
            NumeroCT: {
                validators: {
                    stringLength: {
                        min: 11,
                        message: 'A CT Original deve ser preenchida por completo'
                    },
                    required: false
                }
            },
          
            NumeroOriginalCT: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if (value !== undefined && value !== null && value !== "")
                                if (value.length < 11)
                                    return false;
                            return true;
                            //if ($("#transmitirProdesp").is(":checked")) { return value.length > 0; }
                            //else { return true; }
                        }
                    }
                }
            },
            CodigoEmpenho: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#transmitirProdesp").is(":checked") && tans == "T") { return value.length > 0; }
                            else { return true; }
                        }
                    }
                }
            },
            CodigoUnidadeGestora: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#transmitirSIAFISICO").is(":checked")) { return value.length > 0; }
                            else { return true; }
                        }
                    }
                }
            },
            CodigoUGO: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#transmitirSIAFISICO").is(":checked")) { return value.length > 0; }
                            else { return true; }
                        }
                    }
                }
            },
            CodigoGestao: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#transmitirSIAFISICO").is(":checked")) { return value.length > 0; }
                            else { return true; }
                        }
                    }
                }
            },
            CodigoUnidadeGestoraFornecedora: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#transmitirSIAFISICO").is(":checked")) {
                                if ($("#credor").length > 0)
                                    if ($("#credor").val().length == 0)
                                        return value.length > 0;
                                    else { return true; }
                                else { return value.length > 0; }
                            }
                            else { return true; }
                        }
                    }
                }
            },
            RegionalId: {
                validators: {
                    notEmpty: {
                        message: campoVazio
                    }
                }
            },
            NumeroAnoExercicio: {
                validators: {
                    notEmpty: {
                        message: campoVazio
                    }
                }
            },
            DataEntregaMaterialSiafisico: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#transmitirSIAFISICO").is(":checked") && tans == "T") { return value.length > 0; }
                            else { return true; }
                        },
                        callback1: {
                            message: "Data de Entrega deve ser maior ou igual que Data de Emissão",
                            callback1: function (value) {
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
                }
            },
            CodigoGestaoFornecedora: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#transmitirSIAFISICO").is(":checked")) {
                                if ($("#credor").length > 0)
                                    if ($("#credor").val().length == 0)
                                        return value.length > 0;
                                    else { return true; }
                                else { return value.length > 0; }
                            }
                            else { return true; }
                        }
                    }
                }
            },
            ProgramaId: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#transmitirSIAFEM").is(":checked") || $("#transmitirSIAFISICO").is(":checked")) {
                                return value.length > 0;
                            }
                            else { return true; }
                        }
                    }
                }
            },
            NaturezaId: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#transmitirSIAFEM").is(":checked")) { return value.length > 0; }
                            else { return true; }
                        }
                    }
                }
            },

            TipoAquisicaoId: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#transmitirSIAFISICO").is(":checked")) { return value == 1 || value == 2; }
                            else { return true; }
                        }
                    }
                }
            },
            CodigoNaturezaItem: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#transmitirProdesp").is(":checked") || $("#transmitirSIAFISICO").is(":checked")) {
                                return value.length > 0;
                            }
                            else { return true; }
                        }
                    }
                }
            },
            fornecedor: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#transmitirProdesp").is(":checked") || $("#transmitirSIAFISICO").is(":checked")) {
                                return value.length > 0;
                            }
                            else { return true; }
                        }
                    }
                }
            },
            Total: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#transmitirProdesp").is(":checked") || $("#transmitirSIAFEM").is(":checked") || $("#transmitirSIAFISICO").is(":checked")) {
                                return value.replace(/[\.,R$ ]/g, "") > 0;
                            }
                            else { return true; }
                        }
                    }
                }
            },
            ValorGeral: {
                validators: {
                    identical: {
                        field: "Total",
                        message: valorDiferente
                    },
                    callback: {
                        message: "Deve ser cadastrado ao menos um item.",
                        callback: function (value) {
                            if ($("#transmitirSIAFISICO").is(":checked")) {
                                return value.replace(/[\.,R$ ]/g, "") > 0;
                            }
                            else { return true; }
                        }
                    }
                }
            },
            ValorTotal: {
                validators: {
                    callback: {
                        message: valorDiferente,
                        callback: function (value) {
                            if ($("#transmitirSIAFEM").is(":checked")) {
                                return $("#Total").val().replace(/[\.,R$ ]/g, "") == value.replace(/[\.,R$ ]/g, "");
                            }
                            else { return true; }
                        }
                    }
                }
            },
            FonteId: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#transmitirProdesp").is(":checked") || $("#transmitirSIAFEM").is(":checked")) {
                                return value.length > 0;
                            }
                            else { return true; }
                        }
                    }
                }
            },
            CodigoEvento: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#transmitirSIAFEM").is(":checked") || $("#transmitirSIAFISICO").is(":checked")) { return value.length > 0; }
                            else { return true; }
                        }
                    }
                }
            },
            credor: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#transmitirSIAFEM").is(":checked")) { return value.length > 0; }
                            else { return true; }
                        }
                    }
                }
            },
            DataEntregaMaterial: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#transmitirSIAFISICO").is(":checked") && tans == "T" && obj !== "EmpenhoCancelamento") {
                                return value.length > 0;
                            } else {
                                if ($("#transmitirSIAFEM").is(":checked") && tans == "T") {
                                    if ($("#ModalidadeId").val() != 3 || $("#DescricaoLocalEntregaSiafem").val().trim().length > 0)
                                        return value.length > 0;
                                    else { return true; }
                                }
                                else { return true; }
                            }
                        }
                    },
                    callback1: {
                        message: "Data de Entrega deve ser maior ou igual que Data de Emissão",
                        callback1: function (value) {
                            if (value.length > 0 && value.length < 10)
                                return false;

                            if (value.length == 0) {
                                var data_1 = new Date(value.split('/').reverse().join('/'));
                                var data_2 = new Date($("#DataEmissao").val().split('/').reverse().join('/'));

                                if (data_1 < data_2) {
                                    return false;
                                } else {
                                    return true;
                                }
                            } else {
                                return true;
                            }
                        }
                    }
                }
            },
            DescricaoLocalEntregaSiafem: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#transmitirSIAFISICO").is(":checked") && tans == "T" && obj !== "EmpenhoCancelamento") {
                                return value.length > 0;
                            } else {
                                if ($("#transmitirSIAFEM").is(":checked") && tans == "T" && obj !== "EmpenhoCancelamento") {
                                    if ($("#ModalidadeId").val() != 3 || ($("#DataEntregaMaterial").val() !== undefined && $("#DataEntregaMaterial").val().length > 0))
                                        return value.trim().length > 0;
                                    else { return true; }
                                }
                                else { return true; }
                            }
                        }
                    }
                }
            },
            DescricaoUnidadeMedida: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#transmitirSIAFEM").is(":checked")) { return value.length > 0; }
                            else { return true; }
                        }
                    }
                }
            },
            DescricaoLogradouroEntrega: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#transmitirSIAFISICO").is(":checked")) { return value.length > 0; }
                            else { return true; }
                        }
                    }
                }
            },
            DescricaoBairroEntrega: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#transmitirSIAFISICO").is(":checked")) { return value.length > 0; }
                            else { return true; }
                        }
                    }
                }
            },
            DescricaoCidadeEntrega: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#transmitirSIAFISICO").is(":checked")) { return value.length > 0; }
                            else { return true; }
                        }
                    }
                }
            },
            NumeroCEPEntrega: {
                validators: {
                    stringLength: {
                        min: 9,
                        message: 'Cep invalido'
                    },
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#transmitirSIAFISICO").is(":checked")) { return value.length > 0; }
                            else { return true; }
                        }
                    }
                }
            },
            NumeroCNPJCPFUGCredor: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#transmitirSIAFEM").is(":checked")) {
                                return value.length > 0;
                            } else if ($("#transmitirSIAFISICO").is(":checked")) {
                                if ($("#CodigoUnidadeGestoraFornecedora").length > 0 && $("#CodigoGestaoFornecedora").length > 0)
                                    if ($("#CodigoUnidadeGestoraFornecedora").val().length == 6 && $("#CodigoGestaoFornecedora").val().length == 5)
                                        return true;
                                    else
                                        return value.length > 0;
                                else
                                    return true;
                            } else {
                                return true;
                            }
                        }
                    }
                }
            },
            NumeroCNPJCPFFornecedor: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#transmitirProdesp").is(":checked")) { return value.length > 0; }
                            else { return true; }
                        }
                    }
                }
            },

            Uo: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#transmitirProdesp").is(":checked") || $("#transmitirSIAFISICO").is(":checked")) {
                                return value.length > 0;
                            }
                            else { return true; }
                        }
                    }
                }
            },
            CodigoGestaoCredor: {
                validators: {

                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#transmitirSIAFISICO").is(":checked")) {
                                if ($("#NumeroCNPJCPFUGCredor").length > 0)
                                    if ($("#NumeroCNPJCPFUGCredor").val().length <= 6)
                                        return value.length > 0;
                                    else { return true; }
                                else { return value.length > 0; }
                            }
                            else { return true; }
                        }
                    }
                }
            },
            CodigoCredorOrganizacao: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#transmitirProdesp").is(":checked")) { return value.length > 0; }
                            else { return true; }
                        }
                    }
                }
            },
            CodigoPtres: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#transmitirSIAFEM").is(":checked") || $("#transmitirSIAFISICO").is(":checked")) {
                                return value.length > 0;
                            }
                            else { return true; }
                        }
                    }
                }
            },
            DataEmissao: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if (($("#transmitirSIAFEM").is(":checked") || $("#transmitirSIAFISICO").is(":checked")) && tans == "T") {
                                return value.length > 0;
                            }
                            else { return true; }
                        }
                    }
                }
            },
            Ugo: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#transmitirSIAFEM").is(":checked") || $("#transmitirSIAFISICO").is(":checked")) {
                                return value.length > 0;
                            }
                            else { return true; }
                        }
                    }
                }
            },
            CodigoMunicipio: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if (($("#transmitirSIAFEM").is(":checked") || $("#transmitirSIAFISICO").is(":checked")) && $('#EmpenhoCancelamentoTipoId').val() != undefined) {
                                return true;
                            }
                            else {
                                return value.length > 0;
                            }

                        }
                    }
                }
            },
            ModalidadeId: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if (($("#transmitirSIAFEM").is(":checked") || $("#transmitirSIAFISICO").is(":checked")) && obj != "EmpenhoCancelamento" && obj != "EmpenhoReforco") {
                                return value.length > 0;
                            }
                            else { return true; }
                        }
                    }
                }
            },
            LicitacaoId: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if (($("#transmitirSIAFEM").is(":checked") || $("#transmitirSIAFISICO").is(":checked")) && obj != "EmpenhoCancelamento" && obj != "EmpenhoReforco") {
                                return value.length > 0;
                            }
                            else { return true; }
                        }
                    }
                }
            },
            DescricaoReferenciaLegal: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#transmitirSIAFEM").is(":checked") || $("#transmitirSIAFISICO").is(":checked")) {
                                return value.length > 0;
                            }
                            else { return true; }
                        }
                    }
                }
            },

            //conforme solicitado em bug,foi retirada a obrigatoriedade da OrigemMaterialId  para todos os cenários
            DestinoId: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#transmitirProdesp").is(":checked")) {
                                return value.length > 0;
                            }
                            else { return true; }
                        }
                    }
                }
            },
            EmpenhoTipoId: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#transmitirProdesp").is(":checked") || $("#transmitirSIAFEM").is(":checked") || $("#transmitirSIAFISICO").is(":checked")) {
                                return value.length > 0;
                            }
                            else { return true; }
                        }
                    }
                }
            },
            NumeroProcesso: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#transmitirProdesp").is(":checked") || $("#transmitirSIAFEM").is(":checked") || $("#transmitirSIAFISICO").is(":checked")) {
                                return value.length > 0;
                            }
                            else { return true; }
                        }
                    }
                }
            },
            NumeroProcessoNE: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#transmitirProdesp").is(":checked") || $("#transmitirSIAFISICO").is(":checked")) {
                                return value.length > 0;
                            }
                            else { return true; }
                        }
                    }
                }
            },
            DescEspecDespesa: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#transmitirProdesp").is(":checked")) {
                                var cont = 0;
                                $("#DescEspecificacaoDespesa input").each(function () {
                                    if ($(this).val().length > 0) { cont++; }
                                });
                                return cont > 0;
                            } else {
                                return true;
                            }
                        }
                    }
                }
            },
            DescricaoAutorizadoSupraFolha: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#transmitirProdesp").is(":checked")) {
                                return value.length > 0;
                            }
                            else { return true; }
                        }
                    }
                }
            },
            CodigoEspecificacaoDespesa: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#transmitirProdesp").is(":checked")) {
                                return value.length > 0;
                            }
                            else { return true; }
                        }
                    }
                }
            },
            CodigoAutorizadoAssinatura: {
                validators: {
                    stringLength: {
                        min: 5,
                        message: 'O código da assinatura deve possuir 5 caracteres'
                    },
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#transmitirProdesp").is(":checked")) { return value > 0; }
                            else { return true; }
                        }
                    }
                }
            },
            CodigoExaminadoAssinatura: {
                validators: {
                    stringLength: {
                        min: 5,
                        message: 'O código da assinatura deve possuir 5 caracteres'
                    },
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#transmitirProdesp").is(":checked")) { return value > 0; }
                            else { return true; }
                        }
                    }
                }
            },
            CodigoResponsavelAssinatura: {
                validators: {
                    stringLength: {
                        min: 5,
                        message: 'O código da assinatura deve possuir 5 caracteres'
                    },
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#transmitirProdesp").is(":checked")) { return value > 0; }
                            else { return true; }
                        }
                    }
                }
            },
            DescricaoItemSiafem: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#transmitirSIAFEM").is(":checked")) {
                                var cont = 0;
                                $("#DescricaoItemSiafem input").each(function () {
                                    if ($(this).val().length > 0) { cont++; }
                                });
                                return cont > 0;
                            } else {
                                return true;
                            }
                        }
                    }
                }
            },
            NumeroContrato: {
                validators: {
                    stringLength: {
                        min: 13,
                        message: 'O contrato deve ser preenchido por completo'
                    },
                    required: false
                }
            },
            CodigoFonteSiafisico: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#transmitirProdesp").is(":checked") || $("#transmitirSIAFEM").is(":checked")) {
                                return value.length > 0;
                            }
                            else { return true; }
                        }
                    }
                }
            },
            EmpenhoCancelamentoTipoId: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#transmitirSIAFEM").is(":checked")) {
                                return value.length > 0;
                            }
                            else { return true; }
                        }
                    }
                }
            }
        }
    }).on("submit", function (e) {
        var hasError = false;

        if ($(".has-error").length) {
            hasError = true;
        }

        if (hasError == true) {
            if (tans == "T") {
                e.preventDefault();
                $(".has-error:first input").focus();
            }
            else {
                e.preventDefault();
                ModelItem.CadastroCompleto = false;
                Salvar();
            }
        }
        else {

            if (tans == "T") {
                e.preventDefault();
                ModelItem.CadastroCompleto = true;
                Transmitir();
            } else if (tans == "S") {
                e.preventDefault();
                ModelItem.CadastroCompleto = true;
                Salvar();
            } else if (tans == "I") {
                e.preventDefault();
                window.location.href = '/Empenho/' + (obj == "/Empenho/Empenho" ? "Empenho" : obj) + '/Edit/' + ModelItem.Id + '?tipo=i';
            }
        }

        e.submit;
    });


    $("#CodigoMunicipio").blur(function () {
        validateAutoComplete();
    });

    $("#CodigoMunicipio").autocomplete({
        source: Municipios
    });

});

function validaItemsSiafisico() {
    var contador = 0;

    itens.forEach(function () {

        var valorUnitarioItem = String(itens[contador].ValorTotal);

        if (valorUnitarioItem.indexOf(".") > 0) {

            valorUnitarioItem = valorUnitarioItem.split(".")
            if (valorUnitarioItem[1].length === 1) {
                itens[contador].ValorUnitario = String(itens[contador].ValorUnitario).replace(/[\.R$ ]/g, "") + 0;
                itens[contador].ValorTotal = String(itens[contador].ValorTotal).replace(/[\.R$ ]/g, "") + 0;
            }
            itens[contador].ValorTotal = String(itens[contador].ValorTotal).replace(/[\.R$ ]/g, "");
            itens[contador].ValorUnitario = String(itens[contador].ValorUnitario).replace(/[\.R$ ]/g, "");
        }
        else {
            itens[contador].ValorTotal = String(itens[contador].ValorTotal).replace(/[\.R$ ]/g, "");
            //itens[contador].ValorUnitario = String(itens[contador].ValorUnitario).replace(/[\.,R$ ]/g, "");
            itens[contador].ValorUnitario = String(itens[contador].ValorUnitario);
        }
        contador = contador + 1;
    });
}

function validateAutoComplete() {
    var nomeMunicipio = $("#CodigoMunicipio").val();

    var nomes = ListaMunicipios.filter(function (municipio) {
        if (municipio.Nome === nomeMunicipio || municipio.Codigo == nomeMunicipio) {
            $("#CodigoMunicipio").val(municipio.Codigo);
            return municipio.Codigo;
        }

    });

    if (nomes.length == 0 && nomeMunicipio.length > 0) {
        if ($("#CodigoMunicipio").parent().parent().hasClass("has-error") == false) {
            $("#CodigoMunicipio").parent().parent().addClass("has-error");
            $("#CodigoMunicipio").parent().append('<small data-bv-validator="callback" id="smallCodigoMunicipio" data-bv-validator-for="CodigoMunicipio" class="help-block" style="">Código inválido</small>');
            $('small [data-bv-validator-for="CodigoMunicipio"]').show();
        }
    } else {
        if (nomeMunicipio.length > 0)
            $("#CodigoMunicipio").parent().parent().removeClass("has-error");

        $('#smallCodigoMunicipio').remove();
    }
}