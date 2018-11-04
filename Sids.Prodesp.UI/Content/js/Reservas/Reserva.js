
var obj = "/Reserva/Reserva";
var area = "Reserva";
var quebras = 0;
var tans = "";
var isLoad;
var listaMes = [];
var tipoEstrutura = "Reserva";
var tipoContrato = "0";

$(document).ready(function () {
    ShowLoading();
    isLoad = true;
    var year = new Date().getYear();
    var month = new Date().getMonth() + 1;
    if (year < 1000)
        year += 1900;


    if (ModelItem.Tipo == "4" && ModelItem.TransmitidoSiafisico != true) {
        $("#Oc").removeAttr("ReadOnly");
    } else {
        $("#Oc").attr("ReadOnly", "True");
    }

    if (ModelItem.Codigo > 0) {
        MakMoeda();
        $("#Obra").val($("#Obra").val().replace(/(\d)(\d{1})$/, "$1-$2"));
    }

    $("#Regional").attr("ReadOnly", usuario.RegionalId != 1);
    $('#Regional > option:not(:selected)').attr('disabled', usuario.RegionalId != 1);

    if (ModelItem.TransmitidoProdesp != true && ModelItem.TransmitidoSiafem != true && ModelItem.TransmitidoSiafisico != true)
        $("#Oc").blur(function () {
            ConsultaOc();
        });

    $("#TipoReserva").change(function () {
        if ($(this).val() == "4") {
            $("#Oc").removeAttr("ReadOnly");
        } else {
            $("#Oc").attr("ReadOnly", "True");
        }
    });

    if (ModelItem.TransmitidoProdesp == true)
        $(".prodesp").attr("disabled", "disabled");

    if (ModelItem.TransmitidoSiafem == true)
        $(".siafem").attr("disabled", "disabled");

    if (ModelItem.TransmitidoSiafisico == true)
        $(".siafisico").attr("disabled", "disabled");

    $("#transmitirSIAFEM").click(function () {
        $("#transmitirSIAFISICO").attr("checked", false);
        $(this).attr("checked", true);
    });


    $("#transmitirSIAFISICO").click(function () {
        $("#transmitirSIAFEM").attr("checked", false);
        $(this).attr("checked", true);
    });

    if (month > 2) {
        $("#AnoExercicio").attr("ReadOnly", true);
        $('#AnoExercicio > option:not(:selected)').attr('disabled', true);
    }


    //criar a partir de
    if (ModelItem.AnoExercicio < year) {

        $("#AnoExercicio").removeAttr("ReadOnly");
        $("select[name='AnoExercicio'] option").removeAttr("disabled");

    }


    $(".real").maskMoney({ prefix: "R$ ", allowNegative: false, thousands: ".", decimal: ",", affixesStay: true, precision: 2, allowZero: false });
    //$("#Total").maskMoney({ prefix: "R$ ", allowNegative: false, thousands: ".", decimal: ",", affixesStay: true });
    $(".real").maskMoney('mask');
    SomarTotal();


    $("#btnSalvar").click(function () {
        tans = "S";
        $("#frmPainelCadastrar").data("bootstrapValidator").resetForm();
        $("#frmPainelCadastrar").submit();
    });

    $("#AnoExercicio").change(function () {
        selecionado = "0";
        GerarComboPtres();
        GerarComboCfp();
        GerarComboNatureza();
    });

    if (!isLoad) {
        GerarComboPtres();
        GerarComboCfp();
        GerarComboNatureza();
    }

    $("#btnTransmitir").click(function () {
        tans = "T";
        $("#frmPainelCadastrar").data("bootstrapValidator").resetForm();
        $("#frmPainelCadastrar").submit();
    });

    $("#Ptres").change(function () {

        if (!isLoad || ModelItem.Codigo == 0) {
            selecionado = "0";
            SelecioarComboCfp();
            GerarComboNatureza();
        }
    });

    $("#Programa").change(function () {
        if (!isLoad) {
            SelecionarComboPtres();
            GerarComboNatureza();
        }
    });

    $("#Natureza").change(function () {
        if (!isLoad) {
            GerarAplicacao("Obra");
        }
    });

    $("#Regional").change(function () {
        GerarUGO();
    });

    CarregarTextArea("DescEspecificacaoDespesa", ModelItem.DescEspecificacaoDespesa);
    CarregarTextArea("Observacao", ModelItem.Observacao);

    GerarUGO();

    $("div > #Valor").change(function () { SomarTotal(); });

    IniciarCreateEdit("Reserva");

    var tipoInvalido = "Tipo de dados invalido";
    var campoVazio = "Campo obrigatório não preenchido";

    $("#frmPainelCadastrar").bootstrapValidator({
        // To use feedback icons, ensure that you use Bootstrap v3.1.0 or later
        feedbackIcons: {
            valid: "",//'glyphicon glyphicon-ok',
            invalid: "",//'glyphicon glyphicon-remove',
            validating: ""//'glyphicon glyphicon-refresh'
        },
        fields: {
            Codigo: {
                validators: {
                    required: false
                }
            },
            //Contrato: {
            //    validators: {
            //        callback: {
            //            message: campoVazio,
            //            callback: function (value) {
            //                if ($("#transmitirProdesp").is(":checked")) {
            //                    return value.length > 0;
            //                } else {
            //                    return true;
            //                }
            //            }
            //        },
            //        regexp: {
            //            regexp: /^[a-zA-Z0-9 .]+$/,
            //            message: tipoInvalido
            //        },
            //        stringLength: {
            //            min: 12,
            //            message: 'Contrato Inválido.'
            //        }

            //    }
            //}
            //,
            TipoReserva: {
                validators: {
                    required: false
                }
            },
            Processo: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#transmitirProdesp").is(":checked") || $("#transmitirSIAFEM").is(":checked")) {
                                return value.length > 0;
                            } else {
                                return true;
                            }
                        }
                    }
                    //,
                    //regexp: {
                    //    regexp: /^[a-zA-Z0-9 /]+$/i,
                    //    message: 'Tipo de caracteres invalidos'
                    //}
                }
            },
            Ptres: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#transmitirSIAFEM").is(":checked")) {
                                return value.length > 0;
                            } else {
                                return true;
                            }
                        }
                    },
                    regexp: {
                        regexp: /^[0-9]{1,}$/,
                        message: "Campo somente números"
                    }
                }
            },
            Oc: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#transmitirSIAFISICO").is(":checked")) {
                                return value.length > 0;
                            } else {
                                return true;
                            }
                        }
                    },
                    regexp: {
                        regexp: /^[0-9OC0-9]{1,}$/,
                        message: tipoInvalido
                    },
                    stringLength: {
                        min: 11,
                        message: "OC Inválido."
                    }
                }
            },
            Regional: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#transmitirProdesp").is(":checked")) {
                                return value.length > 0;
                            } else {
                                return true;
                            }
                        }
                    }
                }
            },
            Programa: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#transmitirProdesp").is(":checked") || $("#transmitirSIAFEM").is(":checked")) {
                                return value.length > 0;
                            } else {
                                return true;
                            }
                        }
                    }
                }
            },
            Natureza: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#transmitirProdesp").is(":checked") || $("#transmitirSIAFEM").is(":checked")) {
                                return value.length > 0;
                            } else {
                                return true;
                            }
                        }
                    }
                }
            },
            Obra: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#transmitirProdesp").is(":checked")) {
                                return value.length > 0;
                            } else {
                                return true;
                            }
                        }
                    },
                    stringLength: {
                        min: 8,
                        message: "Cod. Aplicação Inválido."
                    }
                }
            },
            Ugo: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#transmitirSIAFISICO").is(":checked") || $("#transmitirSIAFEM").is(":checked")) {
                                return value.length > 0;
                            } else {
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
                            if ($("#transmitirSIAFEM").is(":checked")) {
                                return value.length > 0;
                            } else {
                                return true;
                            }
                        }
                    }
                }
            },
            Evento: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#transmitirSIAFISICO").is(":checked") || $("#transmitirSIAFEM").is(":checked")) {
                                return value.length > 0;
                            } else {
                                return true;
                            }
                        }
                    }
                }
            },
            DataEmissao: {
                validators: {
                    notEmpty: {
                        message: campoVazio
                    }
                }
            },
            AnoExercicio: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#transmitirProdesp").is(":checked")) {
                                return value.length > 0;
                            } else {
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
                            var valor = $("#Total").val().replace("R$ ", "").replace(",", ".");
                            valor = parseFloat(valor);
                            return valor > 0;
                        }
                    },
                    notEmpty: {
                        message: campoVazio
                    }
                }
            },
            AutorizadoSupraFolha: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#transmitirProdesp").is(":checked")) {
                                return value.length > 0;
                            } else {
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
                            if ($("#transmitirProdesp").is(":checked") || $("#transmitirSIAFEM").is(":checked")) {
                                return value.length > 0;
                            } else {
                                return true;
                            }
                        }
                    }
                }
            },
            Fontes: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#transmitirProdesp").is(":checked")) {
                                return value.length > 0;
                            } else {
                                return true;
                            }
                        }
                    }
                }
            },
            EspecificacaoDespesa: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#transmitirProdesp").is(":checked")) {

                                return value.length > 0;
                            } else {
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
                            if ($("#transmitirProdesp").is(":checked")) {
                                var cont = 0;
                                $("#DescEspecificacaoDespesa input").each(function () {
                                    if ($(this).val() != "") {
                                        cont++;
                                    }
                                });
                                return cont > 0;
                            } else {
                                return true;
                            }
                        }
                    }
                }
            },
            Observacao: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#transmitirSIAFISICO").is(":checked") || $("#transmitirSIAFEM").is(":checked")) {
                                var cont = 0;
                                $("#Observacao input").each(function () {
                                    if ($(this).val() != "") {
                                        cont++;
                                    }
                                });
                                return cont > 0;
                            } else {
                                return true;
                            }
                        }
                    }
                }
            },
            ExaminadoAssinatura: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#transmitirProdesp").is(":checked")) {
                                return value.length > 0;
                            } else {
                                return true;
                            }
                        }
                    }
                }
            },
            ResponsavelAssinatura: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#transmitirProdesp").is(":checked")) {
                                return value.length > 0;
                            } else {
                                return true;
                            }
                        }
                    }
                }
            },
            AutorizadoAssinatura: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#transmitirProdesp").is(":checked")) {
                                return value.length > 0;
                            } else {
                                return true;
                            }
                        }
                    }
                }
            },
            transmitirSIAFEM: {
                validators: {
                    notEmpty: {
                        message: campoVazio
                    }
                }
            }
        }
    }).
        on("submit", function (e) {

            var hasError = false;

            if ($(".has-error").length) {
                hasError = true;
            }

            if (hasError) {
                if (tans == "T") {
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
                    window.location.href = '/Reserva/Reserva/Edit/' + $("#Codigo").val() + '?tipo=i';
                }
            }

            e.submit;
        });


    isLoad = false;
    HideLoading();
});



function Salvar() {
    var online = navigator.onLine;

    if (online == true) {
    } else {
        AbrirModal("Erro de conexão");
        return false;
    }

    waitingDialog.show("Salvando");

    CarregarObjeto();

    CarregarMes();

    var dtoReservaSalvar = {
        Reserva: ModelItem,
        ReservaMes: listaMes
    };

    var object = JSON.stringify(dtoReservaSalvar);

    $.ajax({
        datatype: "json",
        type: "Post",
        url: "/Reserva/Reserva/Save",
        cache: false,
        async: false,
        data: object,
        contentType: "application/json; charset=utf-8",
        success: function (dados) {
            if (dados.Status == "Sucesso") {
                waitingDialog.hide();
                $("#Codigo").val(dados.Id);

                $("#frmPainelCadastrar").data("bootstrapValidator").resetForm();

                $.confirm({
                    text: "Reserva " + AcaoRealizada(tipoAcao, "Reserva") + " com sucesso!",
                    title: "Confirmação",
                    cancel: function () {
                        window.location.href = "/Reserva/Reserva/Edit/" + dados.Id + "?tipo=a";
                    },
                    cancelButton: "Fechar",
                    confirmButton: ""
                });

            } else {

                $("#frmPainelCadastrar").data("bootstrapValidator").resetForm();
                waitingDialog.hide();
                AbrirModal(dados.Msg);
            }
        },
        error: function (dados) {

            $("#frmPainelCadastrar").data("bootstrapValidator").resetForm();
            waitingDialog.hide();
            AbrirModal(dados);
        }
    });
}

function Transmitir() {
    var online = navigator.onLine;

    if (online == true) {
    } else {
        AbrirModal("Erro de conexão");
        return false;
    }

    CarregarObjeto();

    CarregarMes();

    var dtoReservaSalvar = {
        Reserva: ModelItem,
        ReservaMes: listaMes
    };

    var object = JSON.stringify(dtoReservaSalvar);

    Transmissao(object, "Reserva");
}

function CarregarMes() {

    listaMes = [];

    $.each($("div > #Valor"), function (index, value) {
        var valor = value.value;
        valor = valor.replace(/[\.,R$ ]/g, "");

        if (valor != "" && valor > 0) {
            var reservaMes = {
                Codigo: 0,
                Descricao: value.name.replace("Mes", ""),
                ValorMes: parseFloat(valor),
                Id: ModelItem.Codigo
            };

            listaMes[listaMes.length] = reservaMes;
        }
    });
}

function CarregarObjeto() {
    ModelItem.Codigo = $("#Codigo").val();
    ModelItem.Fonte = $("#Fonte").val();
    ModelItem.Estrutura = $("#Natureza").val();
    ModelItem.Programa = $("#Programa").val();
    ModelItem.Tipo = $("#TipoReserva").val();
    ModelItem.Regional = $("#Regional").val();
    ModelItem.Contrato = $("#Contrato").val().replace(/[\.-]/g, "").replace(" ", "");
    ModelItem.Processo = $("#Processo").val();
    ModelItem.NumProdesp = $("#NumProdesp").val();
    ModelItem.NumSiafemSiafisico = $("#NumSiafemSiafisico").val();
    ModelItem.Obra = $("#Obra").val().replace(/[\.-]/g, "");
    ModelItem.Oc = $("#Oc").val();
    ModelItem.Ugo = $("#Ugo").val();
    ModelItem.Uo = $("#Uo").val();
    ModelItem.Evento = $("#Evento").val();
    ModelItem.AnoExercicio = $("#AnoExercicio").val();
    ModelItem.AnoReferencia = $("#AnoReferencia").val();
    ModelItem.OrigemRecurso = $("#Fonte :selected").text();
    ModelItem.DestinoRecurso = $("#Fontes").val();
    ModelItem.Observacao = Concatenar("Observacao");
    ModelItem.TransmitirProdesp = $("#transmitirProdesp").is(":checked");
    ModelItem.TransmitirSiafem = $("#transmitirSIAFEM").is(":checked");
    ModelItem.TransmitirSiafisico = $("#transmitirSIAFISICO").is(":checked");
    ModelItem.AutorizadoSupraFolha = $("#AutorizadoSupraFolha").val();
    ModelItem.EspecificacaoDespesa = $("#EspecDespesa").val();
    ModelItem.DescEspecificacaoDespesa = Concatenar("DescEspecificacaoDespesa");

    ModelItem.AutorizadoAssinatura = $("#CodAssAutorizado").val();
    ModelItem.AutorizadoGrupo = $("#txtAutorizadoGrupo").val();
    ModelItem.AutorizadoOrgao = $("#txtAutorizadoOrgao").val();
    ModelItem.NomeAutorizadoAssinatura = $("#txtAutorizadoNome").val();
    ModelItem.AutorizadoCargo = $("#txtAutorizadoCargo").val();

    ModelItem.ExaminadoAssinatura = $("#CodAssExaminado").val();
    ModelItem.ExaminadoGrupo = $("#txtExaminadoGrupo").val();
    ModelItem.ExaminadoOrgao = $("#txtExaminadoOrgao").val();
    ModelItem.NomeExaminadoAssinatura = $("#txtExaminadoNome").val();
    ModelItem.ExaminadoCargo = $("#txtExaminadoCargo").val();

    ModelItem.ResponsavelAssinatura = $("#CodAssResponsavel").val();
    ModelItem.ResponsavelGrupo = $("#txtResponsavelGrupo").val();
    ModelItem.ResponsavelOrgao = $("#txtResponsavelOrgao").val();
    ModelItem.NomeResponsavelAssinatura = $("#txtResponsavelNome").val();
    ModelItem.ResponsavelCargo = $("#txtResponsavelCargo").val();

    ModelItem.DataEmissao = $("#DataEmissao").val();
    ModelItem.DataCadastro = $("#DataCadastro").val();
    ModelItem.DataTransmissaoSiafemSiafisico = $("#DataTransmissaoSiafemSiafisico").val();
    ModelItem.DataTransmissaoProdesp = $("#DataTransmissaoProdesp").val();

}

function GerarUGO() {
    regionais.forEach(function (regional) {

        if (regional.Id == $("#Regional").val()) {
            $("#Ugo").val(regional.Uge);
        }
    });
}