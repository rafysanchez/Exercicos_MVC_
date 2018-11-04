var obj = "Home";
var resp = "";
var cont = 0;
var ip = "";



$(document).ready(function () {


    $("#Enviar").click(function () {
        ValidadarCaptcha();
    });

    $("#Novo").click(function () {
        FecharCaptcha();
        GerarCaptcha();
    });

    $("#loginForm").bootstrapValidator({
        // To use feedback icons, ensure that you use Bootstrap v3.1.0 or later
        feedbackIcons: {
            valid: "",//'glyphicon glyphicon-ok',
            invalid: "",//'glyphicon glyphicon-remove',
            validating: ""//'glyphicon glyphicon-refresh'
        },
        fields: {
            txtLogin: {
                validators: {
                    notEmpty: {
                        message: "Campo obrigatório não preenchido"
                    }
                }
            },
            txtSenha: {
                validators: {
                    notEmpty: {
                        message: "Campo obrigatório não preenchido"
                    }
                }
            }
        }
    }).on("submit", function (e) {
        
        if (e.isDefaultPrevented()) {
            $(".has-error:first input").focus();
            // handle the invalid form...
        } else {
            // everything looks good!
            e.preventDefault();
            Salvar();
        }
    });
});


function Salvar() {
    var online = navigator.onLine;

    if (online == true) {
    } else {
        AbrirModal("Erro de conexão");
        return false;
    }

    var modelItem = {
        Login: $("#txtLogin").val(),
        Senha: $("#txtSenha").val()
    };

    var dados = JSON.stringify(modelItem);

    $.ajax({
        datatype: "json",
        type: "Post",
        url: "/Login/Acessar",
        cache: false,
        async: true,
        data: dtoLogin = dados,
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            if (result.message == "true") {
                window.location.href = "/Home/Index/";
            } else if (result.status == "Ok") {
                Voltar(result.message, "Mensagem SIAFEM!");
                cont = cont + 1;
            } else if (result.status == "Erro") {
                cont = cont + 1;
                if (cont >= 3) {
                    GerarCaptcha();
                } else {
                    AbrirModal(result.message);
                    return false;
                }

            }
        },
        error: function (result) {
            AbrirModal(result);
            return false;
        }
    });
    return false;
}

function GerarCaptcha() {
    $("#divCaptcha").toggle();
    $("#divLogin").remove();
}


function ValidadarCaptcha() {
    FecharCaptcha();
}

function FecharCaptcha() {
    document.location.reload();
}