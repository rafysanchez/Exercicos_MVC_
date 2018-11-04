var obj = "Estrutura";
var area = "Configuracao";

var cont = 0;

$(document).ready(function () {

    $("#AnoExercicio").change(function () {
        selecionado = "0";
        GerarCombo();
    });
    GerarCombo();

    IniciarCreateEdit(obj);


    $('#frmPainelCadastrar').bootstrapValidator({
        // To use feedback icons, ensure that you use Bootstrap v3.1.0 or later
        feedbackIcons: {
            valid: '',//'glyphicon glyphicon-ok',
            invalid: '',//'glyphicon glyphicon-remove',
            validating: ''//'glyphicon glyphicon-refresh'
        },
        fields: {
            AnoExercicio: {
                validators: {
                    notEmpty: {
                        message: 'Campo obrigatório não preenchido'
                    }
                }
            },
            Programa: {
                validators: {
                    stringLength: {
                        min: 0,
                        max: 60,
                        message: 'Campo não pode ultrapassar 60 caracteres'
                    },
                    notEmpty: {
                        message: 'Campo obrigatório não preenchido'
                    }
                }
            },
            Nomenclatura: {
                validators: {
                    stringLength: {
                        min: 0,
                        max: 60,
                        message: 'Campo não pode ultrapassar 60 caracteres'
                    },
                    notEmpty: {
                        message: 'Campo obrigatório não preenchido'
                    }
                }
            },
            Natureza: {
                validators: {
                    stringLength: {
                        min: 9,
                        max: 9,
                        message: 'Tamanho de Campos inválido'
                    },
                    notEmpty: {
                        message: 'Campo obrigatório não preenchido'
                    },
                    regexp: {
                        regexp: /^[0-9 .]{1,}$/,
                        message: 'Campo somente números'
                    }
                }
            },
            Macro: {
                validators: {
                    stringLength: {
                        min: 6,
                        max: 60,
                        message: 'Tamanho de Campos inválido'
                    },
                    requerid: false
                }
            },
            Fonte: {
                validators: {
                    stringLength: {
                        min: 0,
                        max: 60,
                        message: 'Campo não pode ultrapassar 60 caracteres'
                    },
                    notEmpty: {
                        message: 'Campo obrigatório não preenchido'
                    }
                }
            },
            Aplicacao: {
                validators: {
                    stringLength: {
                        min: 8,
                        max: 8,
                        message: 'Tamanho de Campos inválido'
                    },
                    requerid: false
                }
            }
        }
    }).on('submit', function (e) {

        //// Prevent form submission  success.form.bv
        //e.preventDefault();


        ////// Get the form instance
        //var $form = $(e.target);

        ////// Get the BootstrapValidator instance
        //var bv = $form.data('bootstrapValidator');

        if (e.isDefaultPrevented()) {
            $(".has-error:first input").focus();
            // handle the invalid form...
        } else {
            // everything looks good!
            e.preventDefault();
            Salvar();
        }

        // Use Ajax to submit form data

    });

});

var ModelItem = {
    Codigo: 0,
    Nomenclatura: "",
    Natureza: 0,
    Macro: 0,
    Aplicacao: 0,
    Fonte: 0,
    Programa: 0
};


function Salvar() {
    var online = navigator.onLine;

    if (online == true) {
    } else {
        AbrirModal("Erro de conexão");
        return false;
    }

    ModelItem.Codigo = $("#Codigo").val();
    ModelItem.Nomenclatura = $("#Nomenclatura").val();
    ModelItem.Natureza = $("#Natureza").val().replace(/[\.-]/g, "");;
    ModelItem.Macro = $("#Macro").val().replace(/[\.-]/g, "");;
    ModelItem.Aplicacao = $("#Aplicacao").val().replace(/[\.-]/g, "");;
    ModelItem.Programa = $("#Programa").val();
    ModelItem.Fonte = $("#Fonte").val();

    var dados = JSON.stringify(ModelItem);

    $.ajax({
        datatype: 'json',
        type: 'Post',
        url: "/Estrutura/Save",
        cache: false,
        async: true,
        data: dados,
        contentType: 'application/json; charset=utf-8',
        success: function (dados) {
            if (dados == "Sucesso") {
                Voltar(obj + ' ' + AcaoRealizada(tipoAcao, obj) + ' com sucesso!', "Confirmação");
            } else {
                AbrirModal(dados);
            }
        },
        error: function (dados) {
            AbrirModal(dados);
        }
    });

}
