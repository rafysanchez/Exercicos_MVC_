var button;
var tipoAcao;
var nomeClicado;
var acoes = "";
var sort = 0;
var order = "asc";


var mydate = new Date();
var year = mydate.getYear();
var month = mydate.getMonth();
//
$(document).ready(function () {
    $.ajaxSetup({ cache: false });

    if (year < 1000)
        year += 1900;
    $(".conteudo").css("min-height", (screen.height - 350) + "px");


    //ShowLoading();

    $("a:not(.dropdown-toggle)").click(function () { ShowLoading(); });

    $("html").keyup(function (event) {

        if (event.which == 13 && $("#modalConfirmaExclusao").length > 0)
            return false;

        if (event.which == 13 && $("#modalMessage").attr("style") == "display: block;")
            FecharModal("#modalMessage");
        else if (event.which == 13 && $("#modalMessage").attr("style") != "display: block;")
            $("#btnPesquisar").click();
    });

    $(":input:text").keyup(function (event) {

        if (event.which == 13 && $("#modalConfirmaExclusao").length > 0)
            return false;

        if (event.which == 13 && $("#modalMessage").attr("style") != "display: block;") {
            if ($("#btnPesquisar").length > 0)
                $("#btnPesquisar").click();
        }
    });

    $(".quantidade").keyup(function () {
        if (liquidacaoItem.quantidadeMaterialServico.val().length > 0) {
            var item = liquidacaoItem.quantidadeMaterialServico.val();
            item = String(item).replace(/\D/g, ""); //Remove tudo o que não é dígito
            item = item.replace(/(\d{3})$/, ",$1"); //Coloca a virgula
            liquidacaoItem.quantidadeMaterialServico.val(item);
        }
    });

    $(":input:text").on({
        change: function () {
            ValicarCampos();
        }
    });

    $("#GerarEstrutura").click(function () {
        GerarEstruturaAnoAtual();
    });

    $(":button[data-button='Cadastrar']").click(function () {
        ShowLoading();
    });


    $("#Create").click(function () {
        ShowLoading();
    });

    $(".NoEspecial").change("input", function (button) {
        button.target.value = removerAcentos(button.target.value);
    });


    //Máscaras de formulários
    $(".rg").mask("00.000.000-00");
    $(".cpf").mask("000.000.000-00", { placeholder: "___.___.___-__" });
    $(".cnpj").mask("00.000.000/0000-00", { placeholder: "__.___.___/____-__" });
    $(".ie").mask("000.000.000.000", { placeholder: "___.___.___.___" });
    $(".tel").mask("(00) 0000-0000");
    $(".cel").mask("(00) 00000-0000");
    //$(".real").mask("R$ 000.000.000.000.000,00", { reverse: true });
    $(".realOB").mask("R$ 000.000.000.000.000,00", { reverse: true });
    $(".data").mask("00/00/0000", { placeholder: "__/__/____" });
    $(".cfp").mask("00.000.0000.0000", { placeholder: "__.___.____.____" });
    $(".ced").mask("0.0.00.00.00", { placeholder: "_._.__.__.__" });
    $(".apl").mask("000000-0", { placeholder: "______-_" });
    $(".obra").mask("000000-0", { placeholder: "" });

    $(".qtTESTE").mask("0.000", { reverse: true });

    $(".gestaoCredor").mask("000000", { placeholder: "" });
    $(".unidGest").mask("000000", { placeholder: "" });
    $(".gestao").mask("00000", { placeholder: "" });
    $(".unidGestObra").mask("00000000000000000", { placeholder: "" });


    $(".credorOrgPro").mask("0", { placeholder: "" });
    $(".cnpjcpfProdesp").mask("000000000000000", { placeholder: "" });
    $(".anoExercicio").mask("0000", { placeholder: "______" });
    $(".cnpjcpfUgCredor").mask("00000000000000", { placeholder: "" });

    $(".numeroDocumento").mask("0000000000000000000", { placeholder: "" });

    $(".gestaoCredor").mask("000000", { placeholder: "" });
    $(".contrato").mask("00.00.00000-0", { placeholder: "__.__._____-_" });
    $(".natureza").mask("0.0.00.00", { placeholder: "_._.__.__" });
    $(".oc").mask("0000OC00000", { placeholder: year + "OC_____" });
    $(".ct").mask("0000CT00000", { placeholder: year + "CT_____" });
    $(".ne").mask("0000NE00000", { placeholder: year + "NE_____" });
    $(".nr").mask("0000NR00000", { placeholder: year + "NR_____" });
    $(".nl").mask("0000NL00000", { placeholder: year + "NL_____" });
    $(".pd").mask("0000PD00000", { placeholder: year + "PD_____" });
    $(".ob").mask("0000OB00000", { placeholder: year + "OB_____" });
    $(".re").mask("0000RE00000", { placeholder: year + "RE_____" });
    $(".rt").mask("0000RT00000", { placeholder: year + "RT_____" });
    $(".submpho").mask("000000000/000", { placeholder: "________/___" });
    $(".subEmpProdesp").mask("000000000", { placeholder: "          " });
    $(".prod").mask("000000000", { placeholder: "_________" });
    $(".prodr").mask("000000000/000", { placeholder: "_________/___" });
    $(".macro").mask("0000-0", { placeholder: "____-_" });
    $(".number").mask("00000", { placeholder: "_____" });
    $(".numeric").mask("000000", { placeholder: "" });
    $(".expec").mask("000", { placeholder: "" });
    $(".Ug").mask("000000", { placeholder: "" });
    $(".gestao").mask("00000", { placeholder: "" });
    $(".evento").mask("000000", { placeholder: "" });

    $(".textoNumero").mask("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA", { placeholder: "" });


    $(".maskSubempenho").mask("000000000/000", { placeholder: "________/___" });
    $(".maskRap").mask("000000000/000/000", { placeholder: "________/___/___" });

    //$(".eventoAnulacao").mask("005000", { placeholder: "" });
    $(".eventoAnulacao").keyup(function () {
        var val = $(this).val().replace(/(\d{2})(\d{1})(\d{3})/g, "\$1 5\$3").replace(" ", "");

        $(this).val(val);
    });


    $(".eventClass").mask("000000000", { placeholder: "" });
    $(".eventFonte").mask("000000000", { placeholder: "" });

    $(".cep").mask("00000-000", { placeholder: "_____-___" });
    $(".mesAno").mask("00/0000", { placeholder: "__/____" });
    $(".mes").mask("00", { placeholder: "__" });
    $(".ano").mask("0000", { placeholder: "____" });
    $(".item").mask("00000000-0", { placeholder: "________-_" });
    $(".quantidade").mask("000000,000", { placeholder: "______,___" });
    $(".percentual").mask("#.##0,00", { reverse: true });

    $(".percentualSub").mask("000");

    $(".empenho").mask("000000000", { placeholder: "_________" });
    $(".empenhoRefCan").mask("000000000/000", { placeholder: "_________/___" });
    $(".cfpCredor").mask("000000000000000", { placeholder: "" });
    $(".cfpCnpjUgCredor").mask("00000000000000", { placeholder: "" });

    $(".credorOrgPro").mask("0", { placeholder: "" });

    // função criada para validar campo para aceitar somente virgula
    $('.qtdeMaterialServico').keyup(function (e) {
        var teclaqtde = e.key;
        validacaoMaterialServico(teclaqtde);
    });



    $(".Subempenho").mask("000000000/000", { placeholder: "_________/___" });
    $(".SubempenhoCancelamento").mask("000000000/000/000", { placeholder: "_________/___/___" });
    $(".RapInscricao").mask("000000000/000", { placeholder: "_________/___" });
    $(".RapRequisicao").mask("000000000/000/000", { placeholder: "_________/___/___" });
    $(".RapAnulacao").mask("000000000/000/000", { placeholder: "_________/___/___" });
    $(".numeros").mask("0000000000000000000000000000000000000000000000000000000000000000", { placeholder: "" });

    $(".percentual").mask("###.##000,00", { reverse: true, placeholder: "" });

    $(".codConta").mask("000");
    $(".banco").mask("000");
    $(".agencia").mask("00000");
    $(".numDcto").mask("000000000000000");
    $(".numeroOp").mask("00-00-0-000000-000");
    $(".tpDespesa").mask("00");
    $(".numAgrupamento").mask("0000000000");



    $("#modalMessage button").click(function () {
        FecharModal("#modalMessage");
    });

    $(".eventoSubempenho").keyup(function () {
        var val = $(this).val().replace(/(\d{2})(\d{1})(\d{3})/g, "\$1 0\$3").replace(" ", "");

        $(this).val(val);
    });

    $(".datepicker").datepicker({
        format: "dd/mm/yyyy",
        language: "pt-BR",
        autoclose: true,
        todayHighlight: true
    });


    $(".monthpicker").datepicker({
        format: "mm",
        startView: "months",
        minViewMode: "months",
        language: "pt-BR",
        autoclose: true,
        todayHighlight: true
    });


    $(".yearExPicker").datepicker({
        format: "yyyy",
        startView: "years",
        minViewMode: "years",
        language: "pt-BR",
        autoclose: true,
        todayHighlight: true
    });

    MascaraFoneCel();

    $(".dropdown").on({
        mouseover: function () {
            $(this).addClass("open");
        },
        mouseleave: function () {
            $(this).removeClass("open");
        }
    });

    $(":button").click(function () {
        button = this;
    });

    if (obj != undefined && obj != "Home") ValidarPermissoes();

    if (obj != undefined && obj != "Log")
        $("input:text:first:visible").focus();

    $("#btnCancelar").click(function () {

    });

    $("#btnCancelar").confirm({
        text: "Deseja Cancelar Operação?",
        title: "Confirmar",
        confirm: function (button) {
            CancelarOperacao();
        },
        cancel: function (button) {
        },
        confirmButton: "Sim",
        cancelButton: "Não",
        post: true,
        confirmButtonClass: "btn-danger",
        cancelButtonClass: "btn-default",
        dialogClass: "modal-dialog modal-sm" // Bootstrap classes for large modal
    });

    $('.Proximo').keydown(function (e) {


        var key = e.which;

        if ((e.metaKey || e.ctrlKey) && ((String.fromCharCode(e.which).toLowerCase() === 'c') || (String.fromCharCode(e.which).toLowerCase() === 'v') || (String.fromCharCode(e.which).toLowerCase() === 'x'))) {
            key = 0;
        }


        if (key != 0)
            if (key == 13)
                $(this).next('.Proximo').focus();
            else if (key == 38)
                $(this).prev('.Proximo').focus();
            else if (key == 40)
                $(this).next('.Proximo').focus();
            else if ($(this).val().length == $(this).attr('maxlength') && (key >= 33 && key <= 126) && key != 46)
                $(this).next('.Proximo').focus();
            else if ($(this).val().length == 0 && key == 8)
                $(this).prev('.Proximo').focus();
    });

    //CustomValidators();
    Mudapah();

});

function findGetParameter(parameterName) {
    var result = null,
        tmp = [];
    location.search
        .substr(1)
        .split("&")
        .forEach(function (item) {
            tmp = item.split("=");
            if (tmp[0] === parameterName) result = decodeURIComponent(tmp[1]);
        });
    return result;
}

//function CustomValidators() {
//    $.validator.addMethod("notEqualTo", function (value, element, param) {
//        return param !== value;
//    }, "Please enter a different value, values must not be the same.");
//    $.validator.methods.date = function (value, element) {
//        return this.optional(element) || moment(value, "DD/MM/YYYY", true).isValid();
//    }
//}

function Mudapah() {
    $('#ddlMudapah').val(localStorage.getItem('ddlMudapah'));
}

function MudapahOnChange(sender) {
    localStorage.setItem('ddlMudapah', $('#ddlMudapah').val());
}

function ValidarPermissoes() {
    var url = "/" + obj + "/PermissoesAcao/";
    if (obj == "/Reserva/Reserva" || obj == "/Empenho/Empenho")
        url = obj + "/PermissoesAcao/";

    if ( obj == "Movimentacao")
        url = "/Movimentacao" + url;

    $("#Create").toggle();
    $(":button[data-button='Cadastrar']").toggle();
    $(":button[data-button='Editar']").toggle();
    $(":button[data-button='Visualizar']").toggle();
    $(":button[data-button='Excluir']").toggle();
    $(":button[data-button='AtivarDesativar']").toggle();
    $(":button[data-button='Transmitir']").toggle();
    $(":button[data-button='Imprimir']").toggle();
    if (acoes == "")
        $.ajax({
            type: "Post",
            url: url,
            cache: false,
            async: false,
            contentType: "application/json; charset=utf-8",
            success: function (dados) {
                acoes = dados;
                $.each(acoes, function (index, value) {
                    BotaoAcoes(value.Id);
                });
            },
            error: function () {
                status = false;
            }
        });
    else
        $.each(acoes, function (index, value) {
            BotaoAcoes(value.Id);
        });
}

function BotaoAcoes(acao) {
    switch (acao) {
        case 1:
            $("#Create").toggle();
            $(":button[data-button='Cadastrar']").toggle();
            break;
        case 2:
            $(":button[data-button='Editar']").toggle();
            break;
        case 3:
            $(":button[data-button='Excluir']").toggle();
            break;
        case 4:
            $(":button[data-button='AtivarDesativar']").toggle();
            break;
        case 5:
            $(":button[data-button='Visualizar']").toggle();
            break;
        case 7:
            $(":button[data-button='Transmitir']").toggle();
            break;
        case 8:
            $(":button[data-button='Imprimir']").toggle();
            break;
    }
}

function MascaraFoneCel() {

    var masks = ["(00) 00000-0000", "(00) 0000-00009"],
        maskBehavior = function (val, e, field, options) {
            return val.length > 14 ? masks[0] : masks[1];
        };

    $(".fonecel").mask(maskBehavior, {
        onKeyPress:
                    function (val, e, field, options) {
                        field.mask(maskBehavior(val, e, field, options), options);
                    }
    });
}


function TestaCPF(cpf) {
    cpf = cpf.replace(".", "").replace(".", "").replace("-", "");
    var soma = 0;
    for (var i = 0; i < 9; i++) {
        soma += (cpf.charAt(i) * (10 - i));
    }

    var resto = parseInt(soma % 11);

    var digito = ((11 - resto) > 9 ? 0 : (11 - resto));

    if (digito != parseInt(cpf.charAt(9)))
        return false;

    soma = 0;
    for (var i = 0; i < 10; i++) {
        soma += (cpf.charAt(i) * (11 - i));
    }

    resto = parseInt(soma % 11);

    digito = ((11 - resto) > 9 ? 0 : (11 - resto));

    if (digito != parseInt(cpf.charAt(10)))
        return false;

    return true;
}

function validacaoEmail(email) {

    //var filtro = /^(([\w\-]+\.)*[\w\- ]+){8,}@([\w\- ]+\.)+([\w\-]{2,3})$/;
    var filtro = /^(([\w\-]+\.)*[\w\- ]+)@([\w\- ]+\.)+([\w\-]{2,3})$/;
    if (filtro.test(email)) {
        return true;
    } else {
        return false;
    }
}

function ModalAlterarSenha(boolean, id) {
    $("html").css("overflow-y", boolean == true ? "hidden" : "scroll");
    $(id).modal(boolean == true ? 'show' : 'hide');
    return false;
}

function ModalSistema(boolean, id, acao, obj, nome, btn) {
    button = btn;
    nomeClicado = nome;
    acao = $(button).attr("title") == null ? acao : $(button).attr("title");
    var ob = obj == "Usuario" ? "Usuário" : obj;
    value = acao;

    $.confirm({
        text: "Deseja " + acao + " " + ob + " " + nome + "?",
        title: "Confirmação",
        confirm: function () {
            ShowLoading();
            ModalExclusaoEfetuada(id, ob);
            HideLoading();
        },
        cancel: function () {
        },
        cancelButton: "Não",
        confirmButton: "Sim",
        post: true,
        confirmButtonClass: "btn-danger",
        cancelButtonClass: "btn-default",
        dialogClass: "modal-dialog modal-sm", // Bootstrap classes for large modal
        modalOptionsBackdrop: true
    });
    return false;
}

function CancelarOperacao() {
    ShowLoading();
    window.location.href = urlVoltar;
    return false;
}

function ModalExclusaoEfetuada(id, obj) {
    if (AlterarBotaoStatus(button) == true) {
        $.confirm({
            text: obj.replace("Usuario", "Usuário") + " " + AcaoRealizada(id, obj) + " com sucesso!",
            title: "Confirmação",
            cancel: function (button) {
            },
            cancelButton: "Ok",
            confirmButton: "",
            modalOptionsBackdrop: true,
            dialogClass: "modal-dialog modal-sm" // Bootstrap classes for large modal,
        });
    }
}

function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return "";
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}

function MostraTabelaPesquisa(id) {
    $(id + " tbody").toggle(true);
}

function AlterarBotaoStatus(botao) {

    if ($(botao).attr("title") == "Desativar") {

        if (AlterarStatus(botao.id) == true) {
            botaoAtivar(botao);
            return true;
        } else {
            return false;
        }
    } else if ($(botao).attr("title") == "Ativar") {

        if (AlterarStatus(botao.id) == true) {
            botaoDesativar(botao);
            return true;
        } else {
            return false;
        }
    } else {
        return Excluir(botao.id) == true;
    }
}

function botaoAtivar(botao) {
    nomeClicado = botao.name == null ? botao.attr("name") : botao.name;
    $(botao).addClass("btn-success").removeClass("btn-warning").removeAttr("title").attr("title", "Ativar");
    $(botao).children().addClass("fa-check-square-o").removeClass("fa-ban");
}

function botaoDesativar(botao) {
    nomeClicado = botao.name == null ? botao.attr("name") : botao.name;
    $(botao).addClass("btn-warning").removeClass("btn-success").removeAttr("title").attr("title", "Desativar");
    $(botao).children().addClass("fa-ban").removeClass("fa-check-square-o");
}



function botaoDesbloquear(botao) {
    nomeClicado = botao.name == null ? botao.attr("name") : botao.name;
    $(botao).addClass("btn-success").removeClass("btn-danger").removeAttr("title").attr("title", "Ativar");
    $(botao).children().addClass("fa-unlock").removeClass("fa-lock");
}

function botaoBloquear(botao) {
    nomeClicado = botao.name == null ? botao.attr("name") : botao.name;
    $(botao).addClass("btn-danger").removeClass("btn-success").removeAttr("title").attr("title", "Desativar");
    $(botao).children().addClass("fa-lock").removeClass("fa-unlock");
}

function BloqueiaCampos(idForm) {
    $("#" + idForm).find("input, textarea, select").attr("ReadOnly", "true");
    $("#" + idForm).find(".input-sm").removeAttr("ReadOnly");
    $("input:radio").attr('disabled', "true");
    $('option:not(:selected)').attr('disabled', "true");
    $('option[value="10"]').removeAttr("disabled");
    $('option[value="15"]').removeAttr("disabled");
    $('option[value="-1"]').removeAttr("disabled");
    $(".datepicker").datepicker('destroy');
    $(".monthpicker").datepicker('destroy');
    $(".real").attr("disabled", "true");
    $("[type='checkbox']").attr("disabled", "true");
}

function AcaoRealizada(acao, tipoAcao) {
    if (tipoAcao == "Funcionalidade" || tipoAcao == "Estrutura" || tipoAcao == "Fonte" || tipoAcao == "Reserva" || tipoAcao == "ListaDeBoletos" || tipoAcao == "ReclassificacaoRetencao" || tipoAcao == "Programação Desembolso" || tipoAcao == "Preparacao De Arquivo de Remessa" || tipoAcao == "Movimentação Orçamentária") {

        switch (acao) {
            case "Salvar":
                return "Salva";
                break;
            case "Alterar":
                return "Alterada";
                break;
            case "Ativar":
                return "Ativada";
                break;
            case "Desativar":
                return "Desativada";
                break;
            case "Cadastrar":
                return "Cadastrada";
                break;
            case "Excluir":
                return "Excluída";
                break;
        }
    } else {
        switch (acao) {
            case "Salvar":
                return "Salvo";
                break;
            case "Alterar":
                return "Alterado";
                break;
            case "Ativar":
                return "Ativado";
                break;
            case "Desativar":
                return "Desativado";
                break;
            case "Cadastrar":
                return "Cadastrado";
                break;
            case "Excluir":
                return "Excluído";
                break;
        }
    }
}

function ValidaFormulario() {
    var online = navigator.onLine;

    if (online != true) {
        AbrirModal("Erro de conexão");
        return false;
    }
    ShowLoading();
    var cont = 0;
    $("#form_filtro input").each(function () {
        if ($(this).val() != "") {
            cont++;
        }
    });

    $("#form_filtro select").each(function () {
        if ($(this).val() != "") {
            cont++;
        }
    });

    if (cont == 0) {
        HideLoading();
        AbrirModal("Informe ao menos um campo para filtro!!");
        return false;
    } else {
        var msg = "";
        var data1;
        var data2;

        var dtInicial = $("#DataInicial").val();
        var dtFinal = $("#DataFinal").val();

        if (dtInicial === undefined) {
            dtInicial = '';
        }
        if (dtFinal === undefined) {
            dtFinal = '';
        }

        if ($("#DataInicial").length > 0) {
            var dia;
            var mes;
            var ano;
            if (dtInicial.length > 0 && isValidDate(dtInicial) == false) {
                msg = "A data inicial é invalida!";
            } else {

                dia = dtInicial.substr(0, 2);
                mes = dtInicial.substr(3, 2);
                ano = dtInicial.substr(6, 4);

                data1 = new Date(mes + "/" + dia + "/" + ano);
            }

            if (dtFinal.length > 0 && isValidDate(dtFinal) == false) {
                msg = "A data final é invalida!";
            } else {
                dia = dtFinal.substr(0, 2);
                mes = dtFinal.substr(3, 2);
                ano = dtFinal.substr(6, 4);

                data2 = new Date(mes + "/" + dia + "/" + ano);
            }

            if (isValidDate(dtFinal) && isValidDate(dtInicial))
                if (data1 && data2 &&  data1.getTime() > data2.getTime() && dtFinal.length > 0) {
                    msg = "A data De deve ser menor ou igual a data Até!";
                }
        }

        if ($("#DataFinal").length > 0) {
            if (dtFinal.length > 0 && isValidDate(dtFinal) == false) {
                msg = "A data final é invalida!";
            }
        }

        var cpf = $("#txtCPF").val();
        if (cpf != "" && $("#txtCPF").length > 0) {
            var testeCpf = TestaCPF(cpf);

            if (testeCpf == false) {
                msg = msg + "\nCPF Invalido";
            }
        }


        var email = $("#txtEmail").val();
        if (email != "" && $("#txtEmail").length > 0) {

            var testeEmail = validacaoEmail(email);

            if (testeEmail == false) {
                msg = msg + "\nEmail Invalido";
            }
        }
        if (msg != "") {
            HideLoading();
            AbrirModal(msg);
            return false;
        }
    }

    $("#form_filtro").submit();
    //HideLoading();
}

function isValidDate(Data) {

    if (Data.length < 10)
        return false;

    Data = Data.substr(0, 10);

    var dma = -1;
    var data = Array(3);
    var ch = Data.charAt(0);
    for (i = 0; i < Data.length && ((ch >= "0" && ch <= "9") || (ch == "/" && i != 0)) ;) {
        data[++dma] = "";
        if (ch != "/" && i != 0) return false;
        if (i != 0) ch = Data.charAt(++i);
        if (ch == "0") ch = Data.charAt(++i);
        while (ch >= "0" && ch <= "9") {
            data[dma] += ch;
            ch = Data.charAt(++i);
        }
    }
    if (ch != "") return false;
    if (data[0] == "" || isNaN(data[0]) || parseInt(data[0]) < 1) return false;
    if (data[1] == "" || isNaN(data[1]) || parseInt(data[1]) < 1 || parseInt(data[1]) > 12) return false;
    if (data[2] == "" || isNaN(data[2]) || ((parseInt(data[2]) < 0 || parseInt(data[2]) > 99) && (parseInt(data[2]) < 1900 || parseInt(data[2]) > 9999))) return false;
    if (data[2] < 50) data[2] = parseInt(data[2]) + 2000;
    else if (data[2] < 100) data[2] = parseInt(data[2]) + 1900;
    switch (parseInt(data[1])) {
        case 2: { if (((parseInt(data[2]) % 4 != 0 || (parseInt(data[2]) % 100 == 0 && parseInt(data[2]) % 400 != 0)) && parseInt(data[0]) > 28) || parseInt(data[0]) > 29) return false; break; }
        case 4: case 6: case 9: case 11: { if (parseInt(data[0]) > 30) return false; break; }
        default: { if (parseInt(data[0]) > 31) return false; }
    }
    return true;

}

function AbrirModal(msg, func) {
    $("html").css("overflow-y", "hidden");
    $("#modalMessage").modal();
    $("#modalMessage #Message").html("Erro!");
    $("#modalMessage #value").html(msg);
    $("#modalMessage .close").click(func);
}

function FecharModal(id) {
    $("html").css("overflow-y", "scroll");
    $(id).modal("toggle");
}

function AbrirDetalhe(url) {
    var online = navigator.onLine;

    if (online == true) {
    } else {
        AbrirModal("Erro de conexão");
        return false;
    }
    ShowLoading();
    window.location = url;
}

function IniciarCreateEdit(tipo) {
    switch (getParameterByName("tipo")) {
        case "c":
            $("#divContent h2").html("Visualizar " + tipo);
            BloqueiaCampos("divPainelCadastrar");
            $("#btnSalvar").hide();
            $("#btnCancelar").hide();
            $("#btnTransmitir").hide();
            $("#btnAtualizarIndex").hide();
            $(".input-group-btn").hide();
            $(".btn-edit").hide();
            $(".btn-trash").hide();

            $("#btnVoltar").show();
            $("#btnVoltar").addClass("btn btn-sm btn-primary");
            $("#btnVoltar").html('<i class="fa fa-arrow-left"></i> Voltar');

            $(".text-danger").text("");
            $("#resultadoConsulta").show();
            break;
        case "a":
            $("#divContent h2").html("Alterar " + tipo);

            if (tipo == "Programa") {
                $("#AnoExercicio").attr("disabled", "disabled");
            }

            //$("#btnCancelar").addClass("btn btn-sm btn-danger");
            //$("#btnCancelar").html('<i class="fa fa-close"></i> Cancelar');
            $("#btnTransmitir").show();
            $("#btnAtualizarIndex").show();
            $("#btnVoltar").hide();
            $("#resultadoConsulta").show();

            tipoAcao = "Alterar";
            break;
        case "i":
            $("#divContent h2").html("Alterar " + tipo);

            if (tipo == "Programa") {
                $("#AnoExercicio").attr("disabled", "disabled");
            }

            //$("#btnCancelar").addClass("btn btn-sm btn-danger");
            //$("#btnCancelar").html('<i class="fa fa-close"></i> Cancelar');
            $("#btnTransmitir").show();
            $("#btnAtualizarIndex").show();
            $("#btnVoltar").hide();
            $("#resultadoConsulta").show();

            tipoAcao = "Alterar";

            var control = "";

            if (obj == "/Reserva/Reserva") {
                control = "Reserva";
            } else if (obj == "/Empenho/Empenho") {
                control = "Empenho";
            } else {
                control = obj;
            }

            window.location.href = "/" + area + "/" + control + "/DownloadFile";
            break;
        default:
            $("#divContent h2").html("Cadastrar " + tipo);
            $("#txtNome").val("");
            $("#txtDetalhes").val("");
            //$("#btnCancelar").addClass("btn btn-sm btn-danger");
            //$("#btnCancelar").html('<i class="fa fa-close"></i> Cancelar');
            $("#btnTransmitir").show();
            $("#btnVoltar").hide();
            //$("#retornoTransmissao").hide();
            $("#resultadoConsulta").hide();

            tipoAcao = "Cadastrar";
            break;
    }
}

function Excluir(id) {

    var online = navigator.onLine;

    if (online == true) {
    } else {
        AbrirModal("Erro de conexão");
        return false;
    }

    var status;
    $.ajax({
        type: "Post",
        url: urld,
        cache: false,
        async: false,
        data: JSON.stringify({ Id: id }),
        contentType: "application/json; charset=utf-8",
        success: function (dados) {
            if (dados == "Sucesso") {
                var par = $(button).parent().parent();
                RemoverLinhaTabela(par);
                status = true;
            } else {
                AbrirModal(dados);
                status = false;
                return false;
            }
        },
        error: function (dados, exception) {
            ErrorAjax(dados, exception);
            status = false;
            return false;
        }
    });
    return status;
}

function AlterarStatus(id) {

    var online = navigator.onLine;

    if (online == true) {
    } else {
        AbrirModal("Erro de conexão");
        return false;
    }

    var status = true;
    $.ajax({
        type: "Post",
        url: urla,
        cache: false,
        async: false,
        data: JSON.stringify({ Id: id }),
        contentType: "application/json; charset=utf-8",
        success: function (dados) {
            if (dados != "Sucesso") {
                AbrirModal(dados);
                status = false;
                return false;
            }
        },
        error: function (dados, exception) {
            ErrorAjax(dados, exception);
            status = false;
            return false;
        }
    });
    return status;
}

function GerarEstruturaAnoAtual() {

    var online = navigator.onLine;

    if (online == true) {
    } else {
        AbrirModal("Erro de conexão");
        return false;
    }

    var ano = parseInt(anoCadastro) + 1;

    $.confirm({
        text: "Deseja gerar lista de programas para o ano " + ano + "?",
        title: "Confirmar",
        confirm: function (button) {

            $.ajax({
                type: "Post",
                url: "/Configuracao/Programa/GerarEstruturaAnoAtual/",
                cache: false,
                async: false,
                contentType: "application/json; charset=utf-8",
                success: function (dados) {
                    if (dados.Status == "Sucesso") {
                        Confirmar(dados.Msg, "Confirmação");
                        $("#ddlAnos").append("<option value='" + ano + "' >" + ano + "</option>");

                    } else {
                        AbrirModal(dados.Msg);
                    }
                },
                error: function (dados) {
                    AbrirModal(dados);
                    status = false;
                    return false;
                }
            });
        },
        cancel: function (button) {
        },
        confirmButton: "Sim",
        cancelButton: "Não",
        post: true,
        confirmButtonClass: "btn-danger",
        cancelButtonClass: "btn-default",
        dialogClass: "modal-dialog modal-lg" // Bootstrap classes for large modal

    });
    return false;
}

function Voltar(texto, titulo) {

    $.confirm({
        text: texto,
        title: titulo,
        cancel: function () {
            AtualizarIndex();
        },
        cancelButton: "Fechar",
        confirmButton: ""
    });
}

function Confirmar(texto, titulo) {

    $.confirm({
        text: texto,
        title: titulo,
        cancel: function () {
        },
        cancelButton: "Fechar",
        confirmButton: ""
    });
}

function AtualizarIndex() {
    window.location.href = urlVoltar;
}

function ExisteReserva(id) {

    var qtd = null;
    $.ajax({
        type: "Post",
        url: "/" + obj + "/ObterQuatidadeReserva",
        cache: false,
        async: false,
        data: JSON.stringify({ id: id }),
        contentType: "application/json; charset=utf-8",
        success: function (dados) {
            qtd = dados;
        },
        error: function (dados) {
            AbrirModal(dados);
            return false;
        }
    });
    return qtd;
}

function RemoverLinhaTabela(row) {
    $("._tbDataTables").DataTable()
        .row(row)
        .remove()
        .draw();
}

function dynamicSort(property) {
    var sortOrder = 1;
    if (property[0] === "-") {
        sortOrder = -1;
        property = property.substr(1);
    }
    return function (a, b) {
        var result = (a[property] < b[property]) ? -1 : (a[property] > b[property]) ? 1 : 0;
        return result * sortOrder;
    };
}

function QuabrarLinha(campo) {
    var texto = campo.val();
    var max = campo.attr("cols");
    var row = campo.attr("rows");
    var textos = texto.split("\n");
    var valor = "";

    for (var index = 0; index < row; index++) {
        var value = textos[index];

        if (value != null && value.length > max) {

            var e = max - 1;

            while (value.charAt(e) !== " ") {
                e -= 1;
            }
            textos[index + 1] = textos[index + 1] != null && textos[index + 1] != undefined ? value.substr(e + 1, value.length - max) + textos[index + 1] : value.substr(e + 1, value.length - max);
            textos[index] = value.substr(0, max - 1);
        }
    };

    for (var i = 0; i < row; i++) {
        if (textos[i] != undefined)
            valor += textos[i + 1] != null ? textos[i] + "\n" : textos[i];
    };

    return valor;
}

function MakMoeda() {
    var inputs = $("div > #Valor");

    $.each(inputs, function (index, value) {
        if (value.value != "") {
            var valor = value.value;
            value.value = MaskMonetario(valor);
        }
    });

    var total = $("#Total").val();
    $("#Total").val(MaskMonetario(total));
}

function MaskMonetario(v) {
    if (v == null)
        return 0;
    //v = v.toString().indexOf(",") < 0 ? v + ",00" : v;  // causa erro em empenho verificar a necessessidade desta valição
    v = String(v).replace(/\D/g, ""); //Remove tudo o que não é dígito

    if (v.length == 2) {
        v = v.replace(/(\d{2})$/, "0,$1"); //Coloca a virgula e zero a esquerda
    }
    else if (v.length == 1) {
        v = String(v).replace(/(\d{1})$/, "0,0$1"); // coloca a vírgula , zero decimal e centesimal
    }
    else {
        v = v.replace(/(\d{2})$/, ",$1")
    }

    v = v.replace(/(\d+)(\d{3},\d{2})$/g, "$1.$2"); //Coloca o primeiro ponto

    var qtdLoop = (v.length - 3) / 3;
    var count = 0;

    while (qtdLoop > count) {
        count++;
        v = v.replace(/(\d+)(\d{3}.*)/, "$1.$2"); //Coloca o resto dos pontos
    }

    return v;
}

function MaskQuantidade(v) {

    v = String(v).replace(/\D/g, ""); //Remove tudo o que não é dígito

    if (v.length === 3) {
        v = v.replace(/(\d{3})$/, "0,$1"); //Coloca a virgula e zero a esquerda
    }
    else if (v.length === 1) {
        v = String(v).replace(/(\d{2})$/, "0,00$1"); // coloca a vírgula , zero decimal e centesimal
    }

    else {
        v = v.replace(/(\d{3})$/, ",$1")
    }
    v = v.replace(",00", ",000");
    v = v.replace(",000", ",00");

    v = v.replace(/(.+)(,\d{2})$/g, "$1$20"); //acrescenta zero a decimais de duas casas

    v = v.replace(/\B(?=(\d{3})+(?!\d))/g, "."); // acrescenta pontos separadores de milhar

    var possuiDecimal = v.toString().indexOf(',') > -1;

    if (!possuiDecimal) {
        v = v + ',000';
    }

    return v;
}

function MaskMonetarioCorretoDoBackEnd(v) {
    if (v == null)
        return 0;

    v = String(v);

    var possuiDecimal = v.toString().indexOf('.') > -1;

    if (!possuiDecimal) {
        v = v + ',00';
    }
    else {
        v = v.replace(".", ",");
        var arr = v.split(",");
        var inteiro = arr[0];
        var decimal = arr[1];
        if (decimal.length < 2) {
            decimal = decimal.padRight(2, "0");
        }
        v = inteiro + "," + decimal;
    }

    v = v.replace(/(\d+)(\d{3},\d{2})$/g, "$1.$2"); //Coloca o primeiro ponto

    var qtdLoop = (v.length - 3) / 3;
    var count = 0;

    while (qtdLoop > count) {
        count++;
        v = v.replace(/(\d+)(\d{3}.*)/, "$1.$2"); //Coloca o resto dos pontos
    }



    return v;
}

function MaskQuantidadeCorretaDoBackEnd(v) {

    v = v.toString();

    var possuiDecimal = v.indexOf('.') > -1;

    if (!possuiDecimal && v.indexOf(',') === -1) {
        v = v + ',000';
    }
    else {
        v = v.replace(".", ",");
    }

    return v;
}

String.prototype.padLeft = function (n, pad) {
    t = '';
    if (n > this.length) {
        for (i = 0; i < n - this.length; i++) {
            t += pad;
        }
    }
    return t + this;
}
String.prototype.padRight = function (n, pad) {
    t = this;
    if (n > this.length) {
        for (i = 0; i < n - this.length; i++) {
            t += pad;
        }
    }
    return t;
}

function SomarTotal() {
    var inputs = $("div > #Valor");
    var total = 0;

    $.each(inputs, function (index, value) {
        if (value.value != "") {
            var valor = value.value;
            valor = valor.replace(".", "").replace(".", "").replace(".", "").replace(".", "").replace(".", "");
            valor = valor.replace(",", ".").replace("R$ ", "");
            total += parseFloat(valor);
        }
    });
    total = parseFloat(total.toFixed(2));
    var valor = String(total).replace(".", ",");
    valor = valor.indexOf(',') == -1 ? valor + ",00" : valor;
    valor = valor.length - valor.indexOf(',') < 3 ? valor + "0" : valor;
    $("#Total").val("R$ " + MaskMonetario(valor));
    $("#ValorTotal").val("R$ " + MaskMonetario(valor));
    $("#Total").trigger("input");
    $("#ValorTotal").trigger("input");
}


function Concatenar(id) {
    var inputs = $("#" + id + " > .Proximo");
    var texto = "";

    $.each(inputs, function (index, value) {
        if (value.value != "") {
            var valor = InserirEspacos(value.value, value.maxLength);
            valor = valor.replace(/^[a-zA-Z0-9]+$/);
            texto += texto.length == 0 ? valor : ";" + valor;
        }
    });

    return texto;
}

function CarregarTextArea(id, valor) {
    var inputs = $("#" + id + " > .Proximo");
    var row = id == "Obsercacao" ? 3 : 9;
    if (valor != null && valor.length > 0) {
        var textos = valor.split(";");
        for (var index = 0; index < row; index++)
            if (textos.length > index)
                inputs[index].value = textos[index].trim();

    }

}

function InserirEspacos(valor, max) {
    var result = valor;

    while (max > result.length) {
        result += " ";
    }
    return result;
}

function removerAcentos(texto) {
    var comAcentos = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç<>;¹²³£¢¬ªº°§";
    var semAcentos = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc             ";

    for (var j = 0; j < texto.length; j++) {
        for (var i = 0; i < comAcentos.length; i++) {
            if (texto.charAt(j) == comAcentos[i])
                texto = texto.replace(texto.charAt(j), semAcentos[i]);
        }
    }
    return texto;
}

function ShowLoading() {
    $('html, body').scrollTop(0);
    $("#divLoading").modal();
}

function HideLoading() {
    $("#divLoading").modal("toggle");
}

function ValidarCampo(campo) {

    var prodesp = $(campo).hasClass("prodesp");
    var siafem = $(campo).hasClass("siafem");
    var siafisico = $(campo).hasClass("siafisico");

    if (($("#transmitirProdesp").is(":checked") && prodesp) || ($("#transmitirSIAFEM").is(":checked") && siafem) || ($("#transmitirSIAFISICO").is(":checked") && siafisico)) {
        return $(campo).val().length > 0;
    } else {
        return true;
    }
}

function Left(str, n) {
    if (n <= 0)
        return "";
    else if (n > String(str).length)
        return str;
    else
        return String(str).substring(0, n);
}

function Right(str, n) {
    if (n <= 0)
        return "";
    else if (n > String(str).length)
        return str;
    else {
        var iLen = String(str).length;
        return String(str).substring(iLen, iLen - n);
    }
}

function ConverterData(data) {
    var dia = Left(data, 2);
    var mes = data.length == 8 ? data.substring(3, 5) : data.substring(2, 5);
    var ano = data.length == 8 ? "20" + Right(data, 2) : Right(data, 4);

    return dia + "/" + (mes.length == 2 ? mes : ConverterMes(mes)) + "/" + ano;
}

function ConverterMes(mes) {
    switch (mes) {
        case "JAN":
            return "01";
            break;
        case "FEV":
            return "02";
            break;
        case "MAR":
            return "03";
            break;
        case "ABR":
            return "04";
            break;
        case "MAI":
            return "05";
            break;
        case "JUN":
            return "06";
            break;
        case "JUL":
            return "07";
            break;
        case "AGO":
            return "08";
            break;
        case "SET":
            return "09";
            break;
        case "OUT":
            return "10";
            break;
        case "NOV":
            return "11";
            break;
        case "DEZ":
            return "12";
            break;

    }
}

function limparFormulario($form) {

    $(':input', '#form_filtro')
       .not(':button, :submit, :reset, :hidden, option:not(:disabled), :input[readonly]')
       .val('')
       .removeAttr('checked')
       .removeAttr('selected');

}

function ValicarCampos(campo) {

    var inputs = $(":input").not(':button, :submit, :reset, :hidden, option:not(:disabled)');

    $.each(inputs, function (index, value) {
        if ($(value).parent().parent().hasClass("has-error")) {
            if ($(value).val() != "") {
                $(value).parent().parent().removeClass("has-error");
                $(value).parent().parent().addClass("has-success");
                $("small[data-bv-validator-for='" + $(value).attr("name") + "']").hide();
            }
        } else if ($(value).parent().hasClass("has-error")) {
            if ($(value).val() != "") {
                $(value).parent().removeClass("has-error");
                $(value).parent().addClass("has-success");
                $("small[data-bv-validator-for='" + $(value).attr("name") + "']").hide();
            }
        }
    });

}

function SomaTotalGeral() {
    var total = 0;
    $('#tblPesquisaItem tbody tr').each(function (index, value) {
        var precoTotal = $(this).find('td')[4];
        if (precoTotal != undefined) {
            var innerValue = precoTotal.innerText;
            total += parseInt(innerValue.replace(/[\.,R$ ]/g, ""));
        }
    });

    total = total / 100;
    var valor = String(total).replace(".", ",");
    valor = valor.indexOf(',') == -1 ? valor + ",00" : valor;
    valor = valor.length - valor.indexOf(',') < 3 ? valor + "0" : valor;

    $('#ValorGeral').val("R$ " + MaskMonetario(valor));
}


function MultValorTotal() {
    var valortotal = $('#QuantidadeMaterialServico').val().replace(",", ".") * $('#ValorUnitario').val().replace(/[\.,R$ ]/g, "") / 100;
    valortotal = parseFloat(valortotal.toFixed(2));
    var valor = String(valortotal).replace(".", ",");
    valor = valor.indexOf(',') == -1 ? valor + ",00" : valor;
    valor = valor.length - valor.indexOf(',') < 3 ? valor + "0" : valor;
    $("#ValorTotal").val("R$ " + MaskMonetario(valor));

    $('#ValorUnitario').maskMoney('mask');
    $("#ValorTotal").maskMoney('mask');
}

function validacaoMaterialServico(teclaqtde) {
    //var filtro = /^(([\w\-]+\.)*[\w\- ]+){8,}@([\w\- ]+\.)+([\w\-]{2,3})$/;
    var qtde = $("#QuantidadeMaterialServico").val().toString();
    if (qtde != "" && $("#QuantidadeMaterialServico").length > 0 && qtde != undefined && (teclaqtde.length === 1)) {

        var filtro = /[^0-9,]/;

        filtro.lastIndex = 0;
        if (filtro.test(qtde)) {
            var array = qtde.split(teclaqtde);
            $("#QuantidadeMaterialServico").val(array[0] + array[1]);
        } else {
            return true;
        }
    }
}

function convertDecimal(valorConverter) {
    var qtdeMaterial = valorConverter.replace('.', '');
    qtdeMaterial = parseFloat(qtdeMaterial.replace(',', '.')).toFixed(3).replace('.', ',');
    qtdeMaterial = qtdeMaterial.replace('.', ',');
    return qtdeMaterial;
};

function StringIsNull(param1, param2) {
    return param1 == null ? param2 : param1;
}

function validateConnectionIsOpen() {
    if (navigator.onLine !== true) {
        AbrirModal('Erro de conexão');
    }

    return false;
}

function IsAttr(field, value, attr) {
    if (StringIsNull(value, "").length > 0);
    $(field).attr(attr, true);
}

function ErrorAjax(dados, exception) {
    var msg = '';
    if (dados.status === 0) {
        msg = 'Não conectado \n Verificar rede.';
    } else if (dados.status == 404) {
        msg = 'Página não encontrada. [404]';
    } else if (dados.status == 500) {
        msg = 'Erro interno do servidor [500].';
    } else if (exception === 'parsererror') {
        msg = 'Falha na análise JSON solicitada.';
    } else if (exception === 'timeout') {
        msg = 'Erro de tempo limite.';
    } else if (exception === 'abort') {
        msg = 'Requisição ajax abortada.';
    } else {
        msg = 'Erro não detectado.\n' + dados.responseText;
    }

    AbrirModal(msg);
}

function ConvertDateCSharp(dateScharp) {
    var jsonDate = dateScharp;  // returns "/Date(1245398693390)/"; 
    var re = /-?\d+/;
    var m = re.exec(jsonDate);

    if (m < 0) return null;

    var date = new Date(parseInt(m[0]));

    return ("0" + String(date.getDate())).slice(-2) + "/" + ("0" + String((date.getMonth() + 1))).slice(-2) + "/" + date.getFullYear();
}


function AtualizarFormulario(database) {
    $.each(Object.getOwnPropertyNames(database), function (index, value) {

        if ($('[name="' + value + '"]').length > 0)
            switch ($('[name="' + value + '"]').get(0).nodeName) {
                case "INPUT":
                    if (String(database[value]) != "0" && String(database[value]).search("/Date") < 0)
                        $('input[name="' + value + '"]').val(database[value]);

                    break;
                case "SELECT":
                    RefazerCombo('#' + value);
                    $("select[name='" + value + "'] option[value='" + database[value] + "']").attr('selected', true);
                    break;
                default:
                    break;
            }
    });
}

function objectWithoutProperties(obj, keys) { var target = {}; for (var i in obj) { if (keys.indexOf(i) >= 0) continue; if (!Object.prototype.hasOwnProperty.call(obj, i)) continue; target[i] = obj[i]; } return target; }
function objectWithoutKey(object, key) {
    var deletedKey = object[key],
        otherKeys = objectWithoutProperties(object, [key]);
    return otherKeys;
};

function AtualizarFormularioById(database, exclude) {


    if (exclude != undefined && exclude != null) {
        for (var i = 0; i < exclude.length; i++) {
            database = objectWithoutKey(database, exclude[i]);
        }
    }

    $.each(Object.getOwnPropertyNames(database), function (index, value) {

        if ($('#' + value).length > 0)
            switch ($('#' + value).get(0).nodeName) {
                case "INPUT":
                    if (String(database[value]) != "0" && String(database[value]).search("/Date") < 0)
                        $('#' + value).val(database[value]);

                    break;
                case "TD":
                    if (String(database[value]) != "0" && String(database[value]).search("/Date") < 0)
                        $('#' + value).text(database[value]);

                    break;
                case "SELECT":
                    RefazerCombo('#' + value);
                    $('#' + value + " option[value='" + database[value] + "']").attr('selected', true);
                    break;
                default:
                    break;
            }
    });
}

function validarCampo($campo, $link) {
    if ($("#" + $campo).val().length < 3) {

        $.confirm({
            text: "Campo obrigatório não preenchido",
            title: "Confirmação",
            cancel: function () {
                $("#" + $campo).focus();
            },
            cancelButton: "Ok",
            confirmButton: "",
            post: true,
            cancelButtonClass: "btn-default",
            modalOptionsBackdrop: true
        });
        return false;
    } else {
        $('#' + $campo + '-error').attr('class', 'col-lg-3 col-md-3 form-group-sm form-group has-feedback');
        if ($link != null) {
            window.open($link, '_blank');
        } else {
            return true;
        };
    };
};

function validarCampoR($campo) {
    if (document.getElementById($campo).value.length >= 3) {
        $('#' + $campo + '-error').attr('class', 'col-lg-3 col-md-3 form-group-sm form-group has-feedback');
        return true;
    };
};

function validarCampoS() {


    var imp132 = $('#impressora132').val();
    var imp80 = $('#impressora80').val();

    var valid = imp132.length < 3 && imp80.length < 3 ? true : false;

    if (valid) {
        $.confirm({
            text: "Campo obrigatório não preenchido",
            title: "Confirmação",
            cancel: function () {
                if (imp80.length < 3) {
                    $('#impressora80').focus();
                }
                if (imp132.length < 3) {
                    $('#impressora132').focus();
                }
            },
            cancelButton: "Ok",
            confirmButton: "",
            post: true,
            cancelButtonClass: "btn-default",
            modalOptionsBackdrop: true
        });
    } else {
        SalvarImpressora(imp132, imp80);
    }


};

function SalvarImpressora(imp132, imp80) {
    var online = navigator.onLine;

    if (online == true) {
    } else {
        AbrirModal("Erro de conexão");
        return false;
    }

    $.ajax({
        type: "Post",
        url: "/Seguranca/Usuario/SalvarImpressora",
        cache: false,
        async: false,
        data: JSON.stringify({ imp132: imp132, imp80: imp80 }),
        contentType: "application/json; charset=utf-8",
        success: function (dados) {
            if (dados == "Sucesso") {
                $.confirm({
                    text: "Impressora Salva Com Sucesso",
                    title: "Confirmação",
                    cancel: function () {
                        window.location.reload();
                    },
                    cancelButton: "Ok",
                    confirmButton: "",
                    post: true,
                    cancelButtonClass: "btn-default",
                    modalOptionsBackdrop: true
                });
            }
        },
        error: function (dados, exception) {
            ErrorAjax(dados, exception);
        }
    });
}

function modalImpressora() {
    $('#impressora132').val($('#132').text());
    $('#impressora80').val($('#80').text());
}

function isNumber(n) {
    return !isNaN(parseFloat(n)) && isFinite(n);
}

function setGlobalDataTables() {
    $.extend(true, $.fn.dataTable.defaults, {
        "language": {
            "paginate": {
                "first": "Primeiro",
                "last": "Último"
            },
            "search": "Procurar",
            "zeroRecords": "Nenhum registro encontrado."
        }
    });
}


(function (window, document, $) {
    'use strict';

    window.util = {};

    util.removerCifra = function(value) {
        return value.replace(/[\R$ ]/g, "");
    }

    util.valorToDecimal = function (value) {
        if (value === undefined || value === null || value === '') {
            return value;
        }

        var valorStr = value.replace(/[R$ ]/g, "").replace(/[\.]+/g, '').replace(',', '.');

        return parseFloat(valorStr);
    }

    util.isJson = function(xhr) {
        var ct = xhr.getResponseHeader("content-type") || "";

        if (ct.indexOf('json') > -1) {
            return true;
        }
        return false;
    }

    util.getDataTable = function (seletor, contexto) {
        var table = $(seletor, contexto).DataTable();
        return table;
    }
})(window, document, jQuery);