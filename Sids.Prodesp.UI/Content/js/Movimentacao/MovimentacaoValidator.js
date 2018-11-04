var tans = "";

var Municipios;
var ListaMunicipios;
//var meses = [];
var itensCR = [];
var itensDS = [];
var itensNC = [];
var itensR = [];
var itensS = [];
var itensC = [];
var itensD = [];
var mesesC = [];
var mesesR = [];
var mesesD = [];
var mesesS = [];
var selectedRow = "";
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

            IdTipoMovimentacao: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if (($("#TransmitirProdesp").is(":checked") || $("#TransmitirSiafem").is(":checked")) && tans == "A") {
                                return value.length > 0;
                            }
                            else {
                                return true;
                            }
                        }
                    }
                }
            },

            ProgramaId: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if (($("#TransmitirProdesp").is(":checked") || $("#TransmitirSiafem").is(":checked")) && tans == "A") {
                                return value.length > 0;
                            }
                            else {
                                return true;
                            }
                        }
                    }
                }
            },
            NaturezaId: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if (($("#TransmitirProdesp").is(":checked") || $("#TransmitirSiafem").is(":checked")) && tans == "A") {
                                return value.length > 0;
                            }
                            else {
                                return true;
                            }
                        }
                    }
                }
            },
            AnoExercicio: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#TransmitirProdesp").is(":checked") && tans == "A") {
                                return value.length > 0;
                            }
                            else {
                                return true;
                            }
                        }
                    }
                }
            },

            NrProcesso: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#TransmitirProdesp").is(":checked") && tans == "A") {
                                return value.length > 0;
                            }
                            else {
                                return true;
                            }
                        }
                    }
                }
            },
            OrigemRecurso: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#TransmitirProdesp").is(":checked") && tans == "A") {
                                return value.length > 0;
                            }
                            else {
                                return true;
                            }
                        }
                    }
                }
            },
            DestinoRecurso: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#TransmitirProdesp").is(":checked") && tans == "A") {
                                return value.length > 0;
                            }
                            else {
                                return true;
                            }
                        }
                    }
                }
            },
            FlProc: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#TransmitirProdesp").is(":checked") && tans == "A") {
                                return value.length > 0;
                            }
                            else {
                                return true;
                            }
                        }
                    }
                }
            },

            IdTipoDocumento: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if (($("#TransmitirProdesp").is(":checked") || $("#TransmitirSiafem").is(":checked")) && tans == "A") {
                                return value.length > 0;
                            }
                            else {
                                return true;
                            }
                        }
                    }
                }
            },

            UnidadeGestoraEmitente: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#TransmitirSiafem").is(":checked") && tans == "A") {
                                return value.length > 0;
                            }
                            else {
                                return true;
                            }
                        }
                    }
                }
            },
            IdGestaoEmitente: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#TransmitirSiafem").is(":checked") && tans == "A") {
                                return value.length > 0;
                            }
                            else {
                                return true;
                            }
                        }
                    }
                }
            },
            UnidadeGestoraFavorecida: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#TransmitirSiafem").is(":checked") && tans == "A") {
                                return value.length > 0;
                            }
                            else {
                                return true;
                            }
                        }
                    }
                }
            },
            IdGestaoFavorecida: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#TransmitirSiafem").is(":checked") && tans == "A") {
                                return value.length > 0;
                            }
                            else {
                                return true;
                            }
                        }
                    }
                }
            },

            NrOrgao: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#TransmitirProdesp").is(":checked") && tans == "A") {
                                return value.length > 0;
                            }
                            else {
                                return true;
                            }
                        }
                    }
                }
            },

            NrObra: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#TransmitirProdesp").is(":checked") && tans == "A") {
                                return value.length > 0;
                            }
                            else {
                                return true;
                            }
                        }
                    }
                }
            },

            CancDist: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#TransmitirSiafem").is(":checked") && tans == "A") {
                                return value.length > 0;
                            }
                            else {
                                return true;
                            }
                        }
                    }
                }
            },

            Fonte: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#TransmitirSiafem").is(":checked") && tans == "A") {
                                return value.length > 0;
                            }
                            else {
                                return true;
                            }
                        }
                    }
                }
            },

            CategoriaGasto: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#TransmitirSiafem").is(":checked") && tans == "A") {
                                return value.length > 0;
                            }
                            else {
                                return true;
                            }
                        }
                    }
                }
            },

            EventoNC: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#TransmitirSiafem").is(":checked") && tans == "A") {
                                return value.length > 0;
                            }
                            else {
                                return true;
                            }
                        }
                    }
                }
            },
            Uo: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#TransmitirSiafem").is(":checked") && $("#IdTipoDocumento").val() === "2" && tans == "A") {
                                return value.length > 0;
                            }
                            else {
                                return true;
                            }
                        }
                    }
                }
            },
            FonteRecurso: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            var transmitirSiafem = $("#TransmitirSiafem").is(":checked");
                            var isRedistribuicao = $("#IdTipoMovimentacao").val() === "3";                            
                            var isDistribuicao = $("#IdTipoDocumento").val() === "2";

                            if (transmitirSiafem &&  !isRedistribuicao && isDistribuicao && tans == "A") {
                                return value.length > 0;
                            }
                            else {
                                return true;
                            }
                        }
                    }
                }
            },
            NatDespesa: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#TransmitirSiafem").is(":checked") && tans == "A" && $('#IdTipoMovimentacao').val().toString() !== "3") {
                                return value.length > 0;
                            }
                            else {
                                return true;
                            }
                        }
                    }
                }
            },
            UGO: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#TransmitirSiafem").is(":checked") && tans == "A") {
                                return value.length > 0;
                            }
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
                        message: campoVazio,
                        callback: function (value) {
                            if (($("#TransmitirProdesp").is(":checked") || $("#TransmitirSiafem").is(":checked")) && tans == "A") {
                                return value.replace(/[\.,R$ ]/g, "") > 0;
                            }
                            else {
                                return true;
                            }
                        }
                    }
                }
            },
            EspecDespesa: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#TransmitirProdesp").is(":checked") && tans == "A") {
                                return value.length === 3;
                            }
                            else {
                                return true;
                            }
                        }
                    }
                }
            },
            DescEspecificacaoDespesa: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#TransmitirProdesp").is(":checked") && tans == "A") {
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

            ObservacaoCancelamento: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#TransmitirSiafem").is(":checked") && ($("#IdTipoDocumento").val() === "1" || $("#IdTipoDocumento").val() === "6" || $("#IdTipoDocumento").val() === "7") && tans == "A") {
                                return value.length > 0;
                            }
                            else {
                                return true;
                            }
                        }
                    }
                }
            },
            ObservacaoNC: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            var transmitirSiafem = $("#TransmitirSiafem").is(":checked");
                            var isDistribuicaoSuplementacao = $("#IdTipoDocumento").val() === "2";
                            var isNotRedistribuicaoSemNc = $('#IdTipoMovimentacao').val() !== "3";

                            if (transmitirSiafem && isDistribuicaoSuplementacao && isNotRedistribuicaoSemNc && tans == "A") {
                                return value.length > 0;
                            }
                            else {
                                return true;
                            }
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
                            if ($("#TransmitirProdesp").is(":checked") && tans == "T") {
                                return value > 0;
                            }
                            else {
                                return true;
                            }
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
                            if ($("#TransmitirProdesp").is(":checked") && tans == "T") {
                                return value > 0;
                            }
                            else {
                                return true;
                            }
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
                            if ($("#TransmitirProdesp").is(":checked") && tans == "T") {
                                return value > 0;
                            }
                            else {
                                return true;
                            }
                        }
                    }
                }
            },


        }
    }).on("submit", function (e) {
        var hasError = false;

        if ($(".has-error").length) {
            hasError = true;
        }

        if (hasError == true) {
            if (tans == "A" || tans == "T") {
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
                window.location.href = '/Movimentacao/' + '/Edit/' + ModelItem.Id + '?tipo=i';
            } else if (tans == "A") {
                e.preventDefault();
            }
        }

        e.submit;
    });
});


function Transmitir() {
    if (navigator.onLine != true) {
        AbrirModal("Erro de conexão");
        return false;
    }

    CreateInstanceModel();
    //CreateInstanceModelMes();
    //CreateInstanceModelItem();

    //if ($("#transmitirSIAFISICO").is(":checked")) {
    //    validaItemsSiafisico();
    //}

    //var modelSalvar = { movimentacao: ModelItem, CancelamentoReducao: itensCR, NotasCreditos: itensNC, DistribuicaoSuplementacao: itensDS, Reducao: itensR, Suplementacao: itensS, Cancelamento: itensC, Distribuicao: itensD, MesesC: mesesC, MesesR: mesesR, MesesD: mesesD, MesesS: mesesS };
    var modelSalvar = { Model: ModelItem };


    Transmissao(JSON.stringify(modelSalvar), "Movimentacao");
}




function Salvar() {
    if (navigator.onLine != true) {
        AbrirModal("Erro de conexão");
        return false;
    }

    //CreateInstanceModel();

    var modelSalvar = { Model: ModelItem };

    $.ajax({
        datatype: "json",
        type: "Post",
        url: "/Movimentacao/Movimentacao/Save",
        cache: false,
        data: JSON.stringify(modelSalvar),
        contentType: "application/json; charset=utf-8",
        beforeSend: function () {
            waitingDialog.show("Salvando");
        },
        success: function (dados) {
            if (dados.Status == "Sucesso") {
                $("#Id").val(dados.Id);
                $.confirm({
                    text: "Movimentação Orçamentária " + AcaoRealizada(tipoAcao, "Movimentacao") + " com sucesso!",
                    title: "Confirmação",
                    cancel: function () {
                        window.location.href = "/Movimentacao/Movimentacao/Edit/" + dados.Id + "?tipo=a";
                    },
                    cancelButton: "Fechar",
                    confirmButton: ""
                });
            } else {
                AbrirModal(dados.Msg);
            }
        },
        error: function (dados) {
            AbrirModal(dados);
        },
        complete: function () {
            $("#frmPainelCadastrar").data("bootstrapValidator").resetForm();
            waitingDialog.hide();
        }
    });
}



function CreateInstanceModel() {
    //ModelItem.CancelamentoReducao = ModelItemCancelamentoReducaoList;
    //ModelItem.NotasCreditos = ModelItemNotaCreditoList;
    //ModelItem.DistribuicaoSuplementacao = ModelItemDistribuicaoSuplementacaoList;
    //ModelItem.Reducao = ModelItemReducaoList;
    //ModelItem.MesesC = mesesC;
    //ModelItem.MesesR = ModelItem.MesesR;
    //ModelItem.MesesD = mesesD;
    //ModelItem.MesesS = mesesS;
}


function CreateInstanceModelCRItem() {
    itensCR = [];

    if ($("#transmitirSIAFEM").is(":checked")) {
        if ($("#ValorTotal").val() != "") {
            var valor = $("#ValorTotal").val().replace(/[\.,R$ ]/g, "");

            itensCR[itensCR.length] = {
                Id: 0,
                IdMovimentacao: ModelItem.Id,
                UnidadeGestoraEmitente: $('#UnidadeGestoraEmitente').val(),
                //UnidadeGestoraFavorecido: $('#UnidadeGestoraFavorecida').val(),
                NrOrgao: $('#NrOrgao').val(),
                Fonte: $('#Fonte').val(),
                CategoriaGasto: $('#CategoriaGasto').val(),
                ValorCancelamentoReducao: $('#Total').val().replace(/[\.,R$ ]/g, ""),
                ObservacaoCancelamento: $('#ObservacaoCancelamento').val(),
                ObservacaoCancelamento2: $('#ObservacaoCancelamento2').val(),
                ObservacaoCancelamento3: $('#ObservacaoCancelamento3').val(),
                FlagRedistribuicao: "R",
                //StatusSiafemItem: StatusSiafisicoItemGrid !== "S" ? "N" : "S",
                NrSequencia: ModelItemCancelamentoReducaoList.length + 1,
                EventoNC: $('#CancDist').val(),
                // campo reducao
                ProgramaId: $('#Programa').val(),
                FlProc: $('#FlProc').val(),
                NrProcesso: $('#NrProcesso').val(),
                NrOrgao: $('#NrOrgao').val(),
                NrObra: $('#NrObra').val(),
                OrigemRecurso: $('#OrigemRecurso').val(),
                DestinoRecurso: $('#DestinoRecurso').val(),
                EspecDespesa: $('#EspecDespesa').val(),
                DescEspecDespesa: Concatenar("DescEspecificacaoDespesa"),
                CodigoAutorizadoAssinatura: $("#CodAssAutorizado").val(),
                CodigoAutorizadoGrupo: $("#txtAutorizadoGrupo").val(),
                CodigoAutorizadoOrgao: $("#txtAutorizadoOrgao").val(),
                DescricaoAutorizadoCargo: $("#txtAutorizadoCargo").val(),
                NomeAutorizadoAssinatura: $("#txtAutorizadoNome").val(),
                CodigoExaminadoAssinatura: $("#CodAssExaminado").val(),
                CodigoExaminadoGrupo: $("#txtExaminadoGrupo").val(),
                CodigoExaminadoOrgao: $("#txtExaminadoOrgao").val(),
                DescricaoExaminadoCargo: $("#txtExaminadoCargo").val(),
                NomeExaminadoAssinatura: $("#txtExaminadoNome").val(),
                CodigoResponsavelAssinatura: $("#CodAssResponsavel").val(),
                CodigoResponsavelGrupo: $("#txtResponsavelGrupo").val(),
                CodigoResponsavelOrgao: $("#txtResponsavelOrgao").val(),
                DescricaoResponsavelCargo: $("#txtResponsavelCargo").val(),
                NomeResponsavelAssinatura: $("#txtResponsavelNome").val()

            };
        }
    }
    else {

        var cont = 0;

        ModelItemCancelamentoReducaoList.forEach(function (e) {
            //if ($('#NumeroCT').val() === "")// quando não preencher a ct
            //    ModelItemCancelamentoReducaoList[cont].StatusSiafisicoItem = "N";

            cont = cont + 1;
        });

        itensCR = ModelItemCancelamentoReducaoList;
    }
}

function CreateInstanceModelDSItem() {
    itensDS = [];

    if ($("#transmitirSIAFEM").is(":checked")) {
        if ($("#ValorTotal").val() != "") {
            var valor = $("#ValorTotal").val().replace(/[\.,R$ ]/g, "");

            itensDS[itensDS.length] = {
                Id: 0,
                IdMovimentacao: ModelItem.Id,
                UnidadeGestoraFavorecida: $('#UnidadeGestoraFavorecida').val(),
                NrOrgao: $('#NrOrgao').val(),
                Fonte: $('#Fonte').val(),
                CategoriaGasto: $('#CategoriaGasto').val(),
                ObservacaoCancelamento: $('#ObservacaoCancelamento').val(),
                ObservacaoCancelamento2: $('#ObservacaoCancelamento2').val(),
                ObservacaoCancelamento3: $('#ObservacaoCancelamento3').val(),
                FlagRedistribuicao: "S",
                //StatusSiafemItem: StatusSiafisicoItemGrid !== "S" ? "N" : "S",
                NrSequencia: ModelItemDistribuicaoSuplementacaoList.length + 1,
                ValorDistribuicaoSuplementacao: $('#Total').val().replace(/[\.,R$ ]/g, ""),
                EventoNC: $('#CancDist').val(),
                //campos suplementacao
                // campo reducao
                ProgramaId: $('#Programa').val(),
                FlProc: $('#FlProc').val(),
                NrProcesso: $('#NrProcesso').val(),
                NrOrgao: $('#NrOrgao').val(),
                NrObra: $('#NrObra').val(),
                OrigemRecurso: $('#OrigemRecurso').val(),
                DestinoRecurso: $('#DestinoRecurso').val(),
                EspecDespesa: $('#EspecDespesa').val(),
                DescEspecDespesa: Concatenar("DescEspecificacaoDespesa"),
                CodigoAutorizadoAssinatura: $("#CodAssAutorizado").val(),
                CodigoAutorizadoGrupo: $("#txtAutorizadoGrupo").val(),
                CodigoAutorizadoOrgao: $("#txtAutorizadoOrgao").val(),
                DescricaoAutorizadoCargo: $("#txtAutorizadoCargo").val(),
                NomeAutorizadoAssinatura: $("#txtAutorizadoNome").val(),
                CodigoExaminadoAssinatura: $("#CodAssExaminado").val(),
                CodigoExaminadoGrupo: $("#txtExaminadoGrupo").val(),
                CodigoExaminadoOrgao: $("#txtExaminadoOrgao").val(),
                DescricaoExaminadoCargo: $("#txtExaminadoCargo").val(),
                NomeExaminadoAssinatura: $("#txtExaminadoNome").val(),
                CodigoResponsavelAssinatura: $("#CodAssResponsavel").val(),
                CodigoResponsavelGrupo: $("#txtResponsavelGrupo").val(),
                CodigoResponsavelOrgao: $("#txtResponsavelOrgao").val(),
                DescricaoResponsavelCargo: $("#txtResponsavelCargo").val(),
                NomeResponsavelAssinatura: $("#txtResponsavelNome").val()

            };
        }
    }
    else {

        var cont = 0;

        ModelItemDistribuicaoSuplementacaoList.forEach(function (e) {
            //if ($('#NumeroCT').val() === "")// quando não preencher a ct
            //    ModelItemCancelamentoReducaoList[cont].StatusSiafisicoItem = "N";

            cont = cont + 1;
        });

        itensDS = ModelItemDistribuicaoSuplementacaoList;
    }
}


function CreateInstanceModelNCItem() {
    itensNC = [];

    if ($("#transmitirSIAFEM").is(":checked")) {
        if ($("#ValorTotal").val() != "") {
            var valor = $("#ValorTotal").val().replace(/[\.,R$ ]/g, "");

            itensNC[itensNC.length] = {
                Id: 0,
                IdMovimentacao: ModelItem.Id,
                ProgramaId: $('#Programa').val(),
                IdEstrutura: $('#Natureza').val(),
                Fonte: $('#Fonte').val(),
                UnidadeGestoraFavorecida: $('#UnidadeGestoraFavorecida').val(),
                IdGestaoFavorecida: $('#IdGestaoFavorecida').val(),
                Uo: $('#Uo').val(),
                EventoNC: $('#EventoNC').val(),
                plano_interno: $('#plano_interno').val(),
                CategoriaGasto: $('#CategoriaGasto').val(),
                ValorCredito: $('#Total').val().replace(/[\.,R$ ]/g, ""),
                ObservacaoNC: $('#ObservacaoNC').val(),
                ObservacaoNC2: $('#ObservacaoNC2').val(),
                ObservacaoNC3: $('#ObservacaoNC3').val(),
                //StatusSiafemItem: StatusSiafisicoItemGrid !== "S" ? "N" : "S",
                //NrSequencia: ModelItemCancelamentoReducaoList.length + 1

            };
        }
    }
    else {

        var cont = 0;

        ModelItemNotaCreditoList.forEach(function (e) {
            //if ($('#NumeroCT').val() === "")// quando não preencher a ct
            //    ModelItemCancelamentoReducaoList[cont].StatusSiafisicoItem = "N";

            cont = cont + 1;
        });

        itensNC = ModelItemNotaCreditoList;
    }
}


function CreateInstanceModelRItem() {
    itensR = [];

    if ($("#transmitirSIAFEM").is(":checked")) {
        if ($("#ValorTotal").val() != "") {
            var valor = $("#ValorTotal").val().replace(/[\.,R$ ]/g, "");

            itensR[itensR.length] = {
                Id: 0,
                IdMovimentacao: ModelItem.Id,
                NrOrgao: $('#NrOrgao').val(),
                ValorTotal: $('#Total').val(),
                //StatusSiafemItem: StatusSiafisicoItemGrid !== "S" ? "N" : "S",
                NrSequencia: ModelItemReducaoList.length + 1

            };
        }
    }
    else {

        var cont = 0;

        ModelItemReducaoList.forEach(function (e) {
            //if ($('#NumeroCT').val() === "")// quando não preencher a ct
            //    ModelItemCancelamentoReducaoList[cont].StatusSiafisicoItem = "N";

            cont = cont + 1;
        });

        itensR = ModelItemReducaoList;
    }
}

function CreateInstanceModelSItem() {
    itensS = [];

    if ($("#transmitirSIAFEM").is(":checked")) {
        if ($("#ValorTotal").val() != "") {
            var valor = $("#ValorTotal").val().replace(/[\.,R$ ]/g, "");

            itensS[itensS.length] = {
                Id: 0,
                IdMovimentacao: ModelItem.Id,
                NrOrgao: $('#NrOrgao').val(),
                ValorTotal: $('#Total').val(),
                //StatusSiafemItem: StatusSiafisicoItemGrid !== "S" ? "N" : "S",
                NrSequencia: ModelItemSuplementacaoList.length + 1

            };
        }
    }
    else {

        var cont = 0;

        ModelItemSuplementacaoList.forEach(function (e) {
            //if ($('#NumeroCT').val() === "")// quando não preencher a ct
            //    ModelItemCancelamentoReducaoList[cont].StatusSiafisicoItem = "N";

            cont = cont + 1;
        });

        itensS = ModelItemSuplementacaoList;
    }
}

function CreateInstanceModelCItem() {
    itensC = [];

    if ($("#transmitirSIAFEM").is(":checked")) {
        if ($("#ValorTotal").val() != "") {
            var valor = $("#ValorTotal").val().replace(/[\.,R$ ]/g, "");

            itensC[itensC.length] = {
                Id: 0,
                IdMovimentacao: ModelItem.Id,
                UnidadeGestoraEmitente: $('#UnidadeGestoraEmitente').val(),
                UnidadeGestoraFavorecido: $('#UnidadeGestoraFavorecida').val(),
                NrOrgao: $('#NrOrgao').val(),
                Fonte: $('#Fonte').val(),
                CategoriaGasto: $('#CategoriaGasto').val(),
                ValorTotal: $('#Total').val(),
                //StatusSiafemItem: StatusSiafisicoItemGrid !== "S" ? "N" : "S",
                //NrSequencia: ModelItemCancelamentoReducaoList.length + 1

            };
        }
    }
    else {

        var cont = 0;

        ModelItemCancelamentoList.forEach(function (e) {
            //if ($('#NumeroCT').val() === "")// quando não preencher a ct
            //    ModelItemCancelamentoReducaoList[cont].StatusSiafisicoItem = "N";

            cont = cont + 1;
        });

        itensC = ModelItemCancelamentoList;
    }
}


function CreateInstanceModelDItem() {
    itensD = [];

    if ($("#transmitirSIAFEM").is(":checked")) {
        if ($("#ValorTotal").val() != "") {
            var valor = $("#ValorTotal").val().replace(/[\.,R$ ]/g, "");

            itensD[itensD.length] = {
                Id: 0,
                IdMovimentacao: ModelItem.Id,
                UnidadeGestoraEmitente: $('#UnidadeGestoraEmitente').val(),
                UnidadeGestoraFavorecido: $('#UnidadeGestoraFavorecida').val(),
                NrOrgao: $('#NrOrgao').val(),
                Fonte: $('#Fonte').val(),
                CategoriaGasto: $('#CategoriaGasto').val(),
                ValorTotal: $('#Total').val(),
                //StatusSiafemItem: StatusSiafisicoItemGrid !== "S" ? "N" : "S",
                //NrSequencia: ModelItemCancelamentoReducaoList.length + 1

            };
        }
    }
    else {

        var cont = 0;

        ModelItemDistribuicaoList.forEach(function (e) {
            //if ($('#NumeroCT').val() === "")// quando não preencher a ct
            //    ModelItemCancelamentoReducaoList[cont].StatusSiafisicoItem = "N";

            cont = cont + 1;
        });

        itensD = ModelItemDistribuicaoList;
    }
}






function CreateInstanceModelMesC() {

    mesesC = [];

    $.each($("div > #Valor"), function (index, value) {

        if (value.value != "") {

            var valorC = value.value.replace(/[\.,R$ ]/g, "");

            var mesC = {
                Codigo: 0,
                Descricao: value.name.replace("Mes", ""),
                ValorMes: parseFloat(valorC),
                Id: ModelItem.Codigo
            };

            mesesC[mesesC.length] = mesC;
        }
    });
}


function CreateInstanceModelMesR() {

    mesesR = [];

    $.each($("div > #Valor"), function (index, value) {

        if (value.value != "") {

            var valorR = value.value.replace(/[\.,R$ ]/g, "");

            var mesR = {
                Codigo: 0,
                Descricao: value.name.replace("Mes", ""),
                ValorMes: parseFloat(valorR),
                Id: ModelItem.Codigo,
                NrSequencia: ModelItemReducaoList.length
            };

            mesesR[mesesR.length] = mesR;
        }
    });
}

function CreateInstanceModelMesD() {

    mesesD = [];

    $.each($("div > #Valor"), function (index, value) {

        if (value.value != "") {

            var valorD = value.value.replace(/[\.,R$ ]/g, "");

            var mesD = {
                Codigo: 0,
                Descricao: value.name.replace("Mes", ""),
                ValorMes: parseFloat(valorD),
                Id: ModelItem.Codigo
            };

            mesesD[mesesD.length] = mesD;
        }
    });
}

function CreateInstanceModelMesS() {

    mesesS = [];

    $.each($("div > #Valor"), function (index, value) {

        if (value.value != "") {

            var valorS = value.value.replace(/[\.,R$ ]/g, "");

            var mesS = {
                Codigo: 0,
                Descricao: value.name.replace("Mes", ""),
                ValorMes: parseFloat(valorS),
                Id: ModelItem.Codigo
            };

            mesesS[mesesS.length] = mesS;
        }
    });
}






