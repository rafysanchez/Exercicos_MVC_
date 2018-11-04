var obj = "ReservaCancelamento";
var area = "Reserva";
var quebras = 0;
var tans;
var isLoad;
var listMes = [];
var tipoEstrutura = "Reserva";
var tipoContrato = "0";

$(document).ready(function () {
    ShowLoading();
    isLoad = true;
    var year = new Date().getYear();
    var month = new Date().getMonth() + 1;
    if (year < 1000)
        year += 1900;

    if (ModelItem.Codigo > 0) {
        MakMoeda();
    }

    $('#Obra').val($("#Obra").val().replace(/(\d)(\d{1})$/, "$1-$2"));


    $("#Regional").attr("ReadOnly", usuario.RegionalId != 1);
    $('#Regional > option:not(:selected)').attr('disabled', usuario.RegionalId != 1);

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

    CarregarTextArea("DescEspecificacaoDespesa", ModelItem.DescEspecificacaoDespesa);
    CarregarTextArea("Observacao", ModelItem.Observacao);

    if (month > 2) {
        $("#AnoExercicio").attr("ReadOnly", true);
        $('#AnoExercicio > option:not(:selected)').attr('disabled', true);
    }


    $(".real").maskMoney({ prefix: "R$ ", allowNegative: false, thousands: ".", decimal: ",", affixesStay: true, precision: 2, allowZero: false });
    //$("#Total").maskMoney({ prefix: "R$ ", allowNegative: false, thousands: ".", decimal: ",", affixesStay: true });
    $(".real").maskMoney('mask');
    SomarTotal();
    $("input:text:first:visible").focus();

    $('#btnSalvar').click(function () {
        tans = "S";
        $('#frmPainelCadastrar').data('bootstrapValidator').resetForm();
        $('#frmPainelCadastrar').submit();
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

    $('#btnTransmitir').click(function () {
        tans = "T";
        $('#frmPainelCadastrar').data('bootstrapValidator').resetForm();
        $('#frmPainelCadastrar').submit();
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



    $("div > #Valor").change(function () { SomarTotal(); });

    IniciarCreateEdit("Cancelamento");

    var tipoInvalido = 'Tipo de dados invalido';
    var tipoCaracterInvalido = 'Tipo de caracteres invalidos';
    var campoVazio = 'Campo obrigatório não preenchido';
    var campoNumerico = 'Campo somente números';
    var campoOcInvalido = "OC Inválido.";
    var campoObraInvalido = "Cod. Aplicação Inválido.";

    $('#frmPainelCadastrar').bootstrapValidator({
        feedbackIcons: {
            valid: '',
            invalid: '',
            validating: ''
        },
        fields: {
            Codigo: {
                validators: {
                    required: false
                }
            },

            Reserva: {
                validators: {
                    required: true
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
                        message: campoNumerico
                    }
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
                        message: campoOcInvalido
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
                        message: campoObraInvalido
                    }
                }
            },

            AnoExercicio: {
                validators: {
                    callback: {
                        message: campoVazio,
                        callback: function (value) {
                            if ($("#transmitirProdesp").is(":checked") || $("#transmitirSIAFISICO").is(":checked") || $("#transmitirSIAFEM").is(":checked")) {
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

            transmitirSIAFEM: {
                validators: {
                    notEmpty: {
                        message: campoVazio
                    }
                }
            }
        }
    })
        .on('submit', function (e) {
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

                    window.location.href = '/Reserva/ReservaCancelamento/Edit/' + $("#Codigo").val() + '?tipo=i';
                }
            }
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

    CarregarObjeto();
    CarregarMes();

    var dtoReservaCancelamentoSalvar = { Cancelamento: ModelItem, CancelamentoMes: listMes };
    var object = JSON.stringify(dtoReservaCancelamentoSalvar);

    waitingDialog.show('Salvando');

    $.ajax({
        datatype: 'json',
        type: 'Post',
        url: "/Reserva/ReservaCancelamento/Save",
        cache: false,
        async: false,
        data: object,
        contentType: "application/json; charset=utf-8",
        success: function (dados) {
            if (dados.Status == "Sucesso") {
                waitingDialog.hide();
                $("#Codigo").val(dados.Id);
                $('#frmPainelCadastrar').data('bootstrapValidator').resetForm();
                $.confirm({
                    text: 'Cancelamento ' + AcaoRealizada(tipoAcao, "Cancelamento") + ' com sucesso!',
                    title: "Confirmação",
                    cancel: function () {
                        window.location.href = '/Reserva/ReservaCancelamento/Edit/' + dados.Id + '?tipo=a';
                    },
                    cancelButton: "Fechar",
                    confirmButton: ""
                });
            } else {
                $('#frmPainelCadastrar').data('bootstrapValidator').resetForm();
                waitingDialog.hide();
                AbrirModal(dados.Msg);
            }
        },
        error: function (dados) {
            $('#frmPainelCadastrar').data('bootstrapValidator').resetForm();
            waitingDialog.hide();
            AbrirModal(dados);
        }
    });
}

function Transmitir() {
    var online = navigator.onLine;

    if (online != true) {
        AbrirModal("Erro de conexão");
        return false;
    }

    CarregarObjeto();
    CarregarMes();

    var dtoReservaCancelamentoSalvar = { Cancelamento: ModelItem, CancelamentoMes: listMes };
    var object = JSON.stringify(dtoReservaCancelamentoSalvar);

    Transmissao(object, "ReservaCancelamento");
}

function CarregarMes() {
    listMes = [];

    $.each($("div > #Valor"), function (index, value) {
        var valor = value.value;
        valor = valor.replace(/[\.,R$ ]/g, "");
        if (value.value != "" && valor > 0) {

            var cancelamentoMes = {
                Codigo: 0,
                Descricao: value.name.replace("Mes", ""),
                ValorMes: parseFloat(valor),
                Id: ModelItem.Codigo
            };

            listMes[listMes.length] = cancelamentoMes;
        }
    });
}

function CarregarObjeto() {
    ModelItem.Codigo = $("#Codigo").val();
    ModelItem.NumProdesp = $("#NumProdesp").val();
    ModelItem.NumSiafemSiafisico = $("#NumSiafemSiafisico").val();

    ModelItem.Reserva = $("#Reserva").val();

    ModelItem.AnoExercicio = $("#AnoExercicio").val();
    ModelItem.Regional = $("#Regional").val();
    ModelItem.Programa = $("#Programa").val();
    ModelItem.Estrutura = $("#Natureza").val();
    ModelItem.Ptres = $("#Ptres").val();
    ModelItem.Fonte = $("#Fonte").val();
    ModelItem.OrigemRecurso = $("#Fonte :selected").text();
    ModelItem.Processo = $("#Processo").val();

    ModelItem.Oc = $("#Oc").val();
    ModelItem.Ugo = $("#Ugo").val();
    ModelItem.Uo = $("#Uo").val();
    ModelItem.DestinoRecurso = $("#Fontes").val();
    ModelItem.Evento = $("#Evento").val();
    ModelItem.Contrato = $("#Contrato").val().replace(/[\.-]/g, "");
    ModelItem.Obra = $("#Obra").val().replace(/[\.-]/g, "");
    ModelItem.DataEmissao = $("#DataEmissao").val();

    ModelItem.AutorizadoSupraFolha = $("#AutorizadoSupraFolha").val();
    ModelItem.EspecificacaoDespesa = $("#EspecDespesa").val();
    ModelItem.Observacao = Concatenar("Observacao");
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

    ModelItem.TransmitirProdesp = $("#transmitirProdesp").is(":checked");
    ModelItem.TransmitirSiafem = $("#transmitirSIAFEM").is(":checked");
    ModelItem.TransmitirSiafisico = $("#transmitirSIAFISICO").is(":checked");


    ModelItem.DataCadastro = $("#DataCadastro").val();
    ModelItem.DataTransmissaoSiafemSiafisico = $("#DataTransmissaoSiafemSiafisico").val();
    ModelItem.DataTransmissaoProdesp = $("#DataTransmissaoProdesp").val();
}
