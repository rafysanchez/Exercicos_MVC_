
var obj = "Fonte";
var area = "Configuracao";
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
            Codigo: {
                validators: {
                    stringLength: {
                        min: 9,
                        message: 'Campo deve ter 9 caracteres'
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
            Descricao: {
                validators: {
                    stringLength: {
                        max: 45,
                        message: 'Campo não pode ultrapassar 45 caracteres'
                    },
                    notEmpty: {
                        message: 'Campo obrigatório não preenchido'
                    },
                },
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
var ModelItem = {
    Id:"",
    Codigo: "",
    Descricao: "",
};




function Salvar() {
    var online = navigator.onLine;
    if (online == true) {
    } else {
        AbrirModal("Erro de conexão");
        return false;
    }
    
    var ModelItem = {
        Id :$("#Id").val(),       
        Codigo: $("#Codigo").val(),
        Descricao: $("#Descricao").val()
   };


    var dtoSalvarFonte = {
        Fonte: ModelItem,
    };
    var dados = JSON.stringify(dtoSalvarFonte);
    $.ajax({
        datatype: 'json',
        type: 'Post',
        url: "/Fonte/Save",
        cache: false,
        async: true,
        data: dtoSalvarFonte = dados,
        contentType: 'application/json; charset=utf-8',
        success: function (dados) {
            if (dados == "Sucesso") {
                Voltar(obj + ' ' + AcaoRealizada(tipoAcao, obj) + ' com sucesso!', "Confirmação");
                //$('#modalSalvementoEfetuado').modal();
                //$('#modalSalvementoEfetuado #value').html(obj + ' ' + AcaoRealizada(tipoAcao, obj) + ' com sucesso!');
            } else {
                AbrirModal(dados);
            }
        },
        error: function (dados) {
            AbrirModal(dados);
        }
    });
}



function ReservaFonte(id) {

    var qtd = null;
    $.ajax({
        type: 'Post',
        url: '/Fonte/ObterQuatidadeReserva',
        cache: false,
        async: false,
        data: JSON.stringify({ id: id }),
        contentType: 'application/json; charset=utf-8',
        success: function (dados) {
            qtd = dados;
        },
        error: function (dados) {
            AbrirModal(dados);
        }
    });
    return qtd;
}
