var obj = "Usuario";
var area = "Seguranca";

var cont = 0;

var chk = 0;

$(document).ready(function () {

    IniciarCreateEdit(obj);

    $("li > input[type='checkbox']").click(function () {

        if ($(this).is(":checkd")) {
            chk = chk + 1;
        } else {
            chk = chk - 1;
        }
    });


    $('#frmPainelCadastrar').bootstrapValidator({
        // To use feedback icons, ensure that you use Bootstrap v3.1.0 or later
        feedbackIcons: {
            valid: '',//'glyphicon glyphicon-ok',
            invalid: '',//'glyphicon glyphicon-remove',
            validating: ''//'glyphicon glyphicon-refresh'
        },
        fields: {
            Nome: {
                validators: {
                    notEmpty: {
                        message: 'Campo obrigatório não preenchido'
                    },
                    regexp: {
                        regexp: /^[A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ ]+$/,
                        message: 'Campo Deve Conter Somente Letras'
                    }
                }
            },
            CPF: {
                validators: {
                    notEmpty: {
                        message: 'Campo obrigatório não preenchido'
                    },
                    callback: {
                        message: "O CPF inválido, preencha corretamente o campo CPF",
                        callback: function (value) {
                            if (value.length === 14) {
                                return TestaCPF(value);
                            } else {
                                return false;
                            }
                        }
                    }
                }
            },
            Email: {
                validators: {
                    notEmpty: {
                        message: 'Campo obrigatório não preenchido'
                    },
                    regexp: {
                        regexp: /^(([\w\-]+\.)*[\w\- ]+)@([\w\- ]+\.)+([\w\-]{2,3})$/,
                        message: 'E-mail invalido, preencha corretamente o campo e-mail'
                    }
                    //callback: {
                    //    message: 'E-mail invalido, preencha corretamente o campo e-mail',
                    //    callback: function (value) {
                    //        if (value.length > 0) {
                    //            return validacaoEmail(value);
                    //        } else {
                    //            return true;
                    //        }
                    //    }
                    //}
                }
            },
            Senha: {
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
            ChaveDeAcesso: {
                validators: {
                    notEmpty: {
                        message: 'Campo obrigatório não preenchido'
                    }
                }
            },
            ddlRegional: {
                validators: {
                    notEmpty: {
                        message: 'Campo obrigatório não selecionado'
                    }
                }
            },
            perfil: {
                validators: {
                    notEmpty: {
                        message: 'Selecione ao menos um perfil'
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

//var ModelItem = {
//    Codigo: 0,
//    Nome: "",
//    ChaveDeAcesso: "",
//    CPF: "",
//    Email: "",
//    RegionalId: 0,
//    AreaId: 0,
//    SistemaId: 0,
//    Senha: "",
//    SenhaSiafem: 0
//};

$(function () {
    $('.tree li:has(ul)').addClass('parent_li').find(' > span').attr('title', 'Recolher');
    $('.tree li.parent_li > span').on('click', function (e) {
        var children = $(this).parent('li.parent_li').find(' > ul > li');
        if (children.is(":visible")) {
            children.hide('fast');
            $(this).attr('title', 'Expandir').find(' > i').addClass('fa-plus').removeClass('fa-minus');
        } else {
            children.show('fast');
            $(this).attr('title', 'Recolher').find(' > i').addClass('fa-minus').removeClass('fa-plus');
        }
        e.stopPropagation();
    });
});

var ListPerfilUsuario = [];

function Salvar() {
    var online = navigator.onLine;

    if (online == true) {
    } else {
        AbrirModal("Erro de conexão");
        return false;
    }


    ListPerfilUsuario = [];

    ModelItem.Codigo = $("#Codigo").val();
    ModelItem.Nome = $("#Nome").val();
    ModelItem.ChaveDeAcesso = $("#ChaveDeAcesso").val();
    ModelItem.CPF = $("#CPF").val().replace(".", "").replace(".", "").replace("-", "");
    ModelItem.Email = $("#Email").val();
    ModelItem.RegionalId = $("#ddlRegional").val();
    ModelItem.AreaId = $("#ddlArea").val();
    ModelItem.SistemaId = $("#ddlSistema").val();
    ModelItem.Senha = $("#Senha").val();
    ModelItem.AcessaSiafem = $("#txtSiafen").is(":checked");
    $.each($("td > :input[type='checkbox']"), function (id, val) {
        if ($(val).is(":checked")) {

            var PerfilUsuario = {
                Usuario: ModelItem.Codigo,
                Perfil: 0
            };

            PerfilUsuario.Perfil = $(val).val();
            ListPerfilUsuario[ListPerfilUsuario.length] = PerfilUsuario;
        };
    });


    var dtoSalvarUsuario = {
        Usuario: ModelItem,
        perfisUsuario: ListPerfilUsuario
    };
    var dados = JSON.stringify(dtoSalvarUsuario);

    $.ajax({
        datatype: 'json',
        type: 'Post',
        url: "/Usuario/Save",
        cache: false,
        async: true,
        data: dtoSalvarUsuario = dados,
        contentType: 'application/json; charset=utf-8',
        success: function (dados) {
            if (dados == "Sucesso") {
                Voltar(obj + ' ' + AcaoRealizada(tipoAcao, "Reserva") + ' com sucesso!', "Confirmação");
            } else {
                AbrirModal(dados);
            }
        },

        error: function (dados) {
            AbrirModal(dados);
        }
    });

}