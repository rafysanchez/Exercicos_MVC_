var obj = "Funcionalidade";
var area = "Seguranca";
var cont = 0;

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
            Nome: {
                validators: {
                    stringLength: {
                        max: 100,
                        message: 'Campo não pode ultrapassar 100 caracteres'
                    },
                    notEmpty: {
                        message: 'Campo obrigatório não preenchido'
                    },
                    regexp: {
                        regexp: /[a-zA-Z\u00C0-\u00FF ]+/i,///[^0-9!#$%&'()*+,-./:;?@[\\\]_`{|}~]/i,
                        message: 'Campo não pode começar com números'
                    }
                }
            },
            MenuUrlId: {
                validators: {
                    notEmpty: {
                        message: 'Campo obrigatório não preenchido'
                    }
                }
            },
            Descricao: {
                validators: {
                    stringLength: {
                        min: 0,
                        max: 100,
                        message: 'Campo não pode ultrapassar 200 caracteres'
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
            MenuId: {
                validators: {
                    notEmpty: {
                        message: 'Campo obrigatório não selecionado'
                    }
                }
            },
            acao: {
                validators: {
                    notEmpty: {
                        message: 'Selecione ao menos uma acão'
                    }
                }
            }
        }
    }).on('submit', function (e) {

        if (e.isDefaultPrevented()) {
            $(".has-error:first input").focus();
        } else {
            e.preventDefault();
            Salvar();
        }

    });

});


function Salvar() {

    var ListAcao = [];

    var ModelItem = {
        Codigo: $("#Codigo").val(),
        Nome: $("#Nome").val(),
        Descricao: $("#Descricao").val(),
        MenuUrlId: $("#MenuUrlId").val(),
        MenuId: $("#cbxMenu").val()
    };

    var online = navigator.onLine;

    if (online == true) {
    } else {
        AbrirModal("Erro de conexão");
        return false;
    }

    
    $.each($("input[type='checkbox']"), function (id, val) {
        if ($(val).is(":checked")) {

            var Acao = {
                Acao: $(val).attr("id"),
                Funcionalidade: ModelItem.Codigo
            };

            ListAcao[ListAcao.length] = Acao;
        };
    });


    var dtoSalvarFuncionalidade = {
        Funcionalidade: ModelItem,
        Acoes: ListAcao
    };
    var dados = JSON.stringify(dtoSalvarFuncionalidade);
    $.ajax({
        datatype: 'json',
        type: 'Post',
        url: "/Funcionalidade/Save",
        cache: false,
        async: true,
        data: dtoSalvarFuncionalidade = dados,
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