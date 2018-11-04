var obj = "Programa";
var area = "Configuracao";

var cont = 0;

$(document).ready(function () {

    IniciarCreateEdit(obj);


    $(".ddlAnos").change(function () {
        $("#form_filtro").submit();
    });

    $('#frmPainelCadastrar').bootstrapValidator({
        // To use feedback icons, ensure that you use Bootstrap v3.1.0 or later
        feedbackIcons: {
            valid: '',//'glyphicon glyphicon-ok',
            invalid: '',//'glyphicon glyphicon-remove',
            validating: ''//'glyphicon glyphicon-refresh'
        },
        fields: {
            Ano: {
                validators: {
                    notEmpty: {
                        message: 'Campo obrigatório não preenchido'
                    }
                }
            },
            Ptres: {
                validators: {
                    stringLength: {
                        min: 6,
                        message: 'Campo deve ter 6 caracteres'
                    },
                    notEmpty: {
                        message: 'Campo obrigatório não preenchido'
                    },
                    regexp: {
                        regexp: /^[0-9]{1,}$/,
                        message: 'Campo somente números'
                    }
                }
            },
            Cfp: {
                validators: {
                    stringLength: {
                        min: 16,
                        message: 'Campo deve ter 16 caracteres'
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
            Descricao: {
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
    Descricao: "",
    Ptres: 0,
    Cfp: "",
    Ano: 0
};


function Salvar() {
    var online = navigator.onLine;

    if (online == true) {
    } else {
        AbrirModal("Erro de conexão");
        $('#frmPainelCadastrar').data('bootstrapValidator').resetForm();
        return false;
    }

    ModelItem.Codigo = $("#Codigo").val();
    ModelItem.Descricao = $("#Descricao").val();
    ModelItem.Ptres = $("#Ptres").val();
    ModelItem.Cfp = $("#Cfp").val();
    ModelItem.Ano = $("#Ano").val();

    var dados = JSON.stringify(ModelItem);

    $.ajax({
        datatype: 'json',
        type: 'Post',
        url: "/Programa/Save",
        cache: false,
        async: true,
        data: dtoSalvarPerfil = dados,
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
