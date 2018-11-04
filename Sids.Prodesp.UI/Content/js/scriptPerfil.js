var obj = "Perfil";
var area = "Seguranca";
var cont = 0;
$("input").change(function () {
    if ($(this).val() != "") {
        $(this).removeClass("Invalid");
        cont++;
    }
});

$("#Acessar").click(function () {
    var login = $("#username").val();
    var password = $("#password").val();

    var cont = 0;
    $("form input").each(function () {
        if ($(this).val() == "") {
            $(this).first().focus();
            $(this).addClass("Invalid");
            cont++;
        }
    });
    if (cont > 0) {
        return false;
    }

    $.ajax({
        type: "Post",
        url: '/Login/Acesso/',
        cache: false,
        async: true,
        data: { login: login, password: password },
        success: function (data) {
            if (data == "Ok") {
                $("#loginForm").submit();
            }
            else {
                $("#loginErrorMsg").removeClass("hide");
            }
        },
        error: function () {

        }
    });

});

$(document).ready(function () {

    IniciarCreateEdit(obj);

    $('#frmPainelCadastrar').bootstrapValidator({
        // To use feedback icons, ensure that you use Bootstrap v3.1.0 or later
        feedbackIcons: {
            valid: '',//'glyphicon glyphicon-ok',
            invalid: '',//'glyphicon glyphicon-remove',
            validating: ''//'glyphicon glyphicon-refresh'
        },
        fields: {
            Descricao: {
                validators: {
                    stringLength: {
                        max: 100,
                        message: 'Campo não pode ultrapassar 100 caracteres'
                    },
                    notEmpty: {
                        message: 'Campo obrigatório não preenchido'
                    },
                    regexp: {
                        regexp: /^[^0-9]/i,
                        message: 'Campo não pode começar com números'
                    }
                }
            },
            Detalhe: {
                validators: {
                    stringLength: {
                        min: 0,
                        max: 100,
                        message: 'Campo não pode ultrapassar 200 caracteres'
                    },
                    requerid: false,
                    regexp: {
                        regexp: /^[^0-9]/i,
                        message: 'Campo não pode começar com números'
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

//var ModelItem = {
//    Codigo: 0,
//    Descricao: "",
//    Detalhe: ""
//};

$(function () {
    $('.tree li:has(ul)').addClass('parent_li').find(' > span').attr('title', 'Expandir');
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

var ListPerfilAcao = [];

function Salvar() {
    var online = navigator.onLine;

    if (online == true) {
    } else {
        AbrirModal("Erro de conexão");
        return false;
    }
    ListPerfilAcao = [];
    ModelItem.Codigo = $("#Codigo").val();

    $.each($("input[type='checkbox']"), function (id, val) {
        if ($(val).is(":checked")) {

            var PerfilAcao = {
                Perfil: ModelItem.Codigo,
                Funcionalidade: 0,
                Acao: 0
            };

            PerfilAcao.Funcionalidade = $(val).val();
            PerfilAcao.Acao = $(val).attr("data-acaoId");
            ListPerfilAcao[ListPerfilAcao.length] = PerfilAcao;
        };
    });

    ModelItem.Descricao = $("#Descricao").val();
    ModelItem.Detalhe = $("#Detalhe").val();

    var dtoSalvarPerfil = {
        perfil: ModelItem,
        perfilAcao: ListPerfilAcao
    };
    var dados = JSON.stringify(dtoSalvarPerfil);
    $.ajax({
        datatype: 'json',
        type: 'Post',
        url: "/Perfil/Save",
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


function GerarCreateEdit(id) {

    var div = $("#Index");
    var disp = div.css("display");
    div.css("display", disp == 'none' ? 'block' : 'none');


    var div2 = $("#CreateEditi");
    var disp2 = div2.css("display");
    div2.css("display", disp2 == 'none' ? 'block' : 'none');

    $.ajax({
        datatype: 'json',
        type: 'Post',
        url: "/Perfil/Editar",
        cache: false,
        async: true,
        data: JSON.stringify({ Id: id }),
        contentType: 'application/json; charset=utf-8',
        success: function (dados) {

            $("#txtDescricao").val() = dados.Perfil.Descricao;
            $("#txtDetalhe").val() = dados.Perfil.Detalhe;
            $("#Codigo").val() = dados.Perfil.Codigo;

            var lista = $("#lista");
            $.each(dados.Funcionalidades, function (id, val) {
                var funcLi = $("<li><span><i class='fa fa-plus'></i> " + val.Funcionalidade + "</span></li>");

                var acaoLu = $("<ul id='" + val.Funcionalidade + "'></ul>");
                $.each(val.Acoes, function (id, acao) {
                    var acaoLi = $("<li style='display: none;'></li>");
                    var check = $("<input type='checkbox' value='" + acao.FuncionalidadeAcaoId + "' data-acaoId='" + acao.Id + "' />" + acao.Descricao);
                    check.attr("checked", acao.Associado);
                    acaoLi.append(check);
                    acaoLu.append(acaoLi);
                });
                funcLi.append(acaoLu);
                lista.append(funcLi);
            });

            $(function () {
                $('.tree li:has(ul)').addClass('parent_li').find(' > span').attr('title', 'Expandir');
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
        },
        error: function (dados) {
            AbrirModal(dados);
        }
    });
}