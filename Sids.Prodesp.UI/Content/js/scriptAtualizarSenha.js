var obj = "Usuario";
var area = "Seguranca";
var cont = 0;

$(document).ready(function () {

    IniciarCreateEdit(obj);

    $('#frmPainelAlterarSenha').bootstrapValidator({
        // To use feedback icons, ensure that you use Bootstrap v3.1.0 or later
        feedbackIcons: {
            valid: '',//'glyphicon glyphicon-ok',
            invalid: '',//'glyphicon glyphicon-remove',
            validating: ''//'glyphicon glyphicon-refresh'
        },
        fields: {
            txtSenhaAtual: {
                validators: {
                    notEmpty: {
                        message: 'Campo obrigatório não preenchido'
                    },
                    stringLength: {
                        min: 6,
                        message: 'A senha deve possuir 6 ou mais caracteres'
                    }
                }
            },
            txtNovaSenha: {
                validators: {
                    notEmpty: {
                        message: 'Campo obrigatório não preenchido'
                    },
                    stringLength: {
                        min: 6,
                        message: 'A senha deve possuir 6 ou mais caracteres'
                    },
                    callback: {
                        message: 'Senha não confere',
                        callback: function (value) {
                            if (value.length >= 6 && value != $("#txtConfirmarSenha").val() && $("#txtConfirmarSenha").val().length > 0) {
                                return false;
                            } else {
                                return true;
                            }
                        }
                    }
                    //identical: {
                    //    field: 'txtConfirmarSenha',
                    //    message: 'Senha não confere'
                    //}
                }
            },
            txtConfirmarSenha: {
                validators: {
                    notEmpty: {
                        message: 'Campo obrigatório não preenchido'
                    },
                    stringLength: {
                        min: 6,
                        message: 'A senha deve possuir 6 ou mais caracteres'
                    },
                    callback: {
                        message: 'Senha não confere',
                        callback: function (value) {
                            if (value.length >= 6 && value != $("#txtNovaSenha").val()) {
                                return false;
                            } else {
                                return true;
                            }
                        }
                    }
                }
            }
        }
    }).on('submit', function (e) {

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


function Salvar() {
    var online = navigator.onLine;

    if (online == true) {
    } else {
        AbrirModal("Erro de conexão");
        return false;
    }

    var dtoSalvarSenha = {
        Senha: $("#txtSenhaAtual").val(),
        NovaSenha: $("#txtNovaSenha").val()
    };

    var dados = JSON.stringify(dtoSalvarSenha);
    $.ajax({
        datatype: 'json',
        type: 'Post',
        url: "/Home/AtualizarSenha",
        cache: false,
        async: true,
        data: dtoSalvarSenha = dados,
        contentType: 'application/json; charset=utf-8',
        success: function (dados) {
            if (dados == "Sucesso") {
                FecharModal('#modalAlteraSenhaSiafem');
            } else {
                AbrirModal(dados);
            }
        },
        error: function (dados) {
            AbrirModal(dados);
        }
    });

}
