var obj = "Desdobramento";

window.boletosListar = {};

$(document).on('ready', function () {
    $('#btnReTransmitir').click(function () {
        tans = true;
        ObterId(controller);
    });

    $('#idSelecionar').click(function () {
        MarcaTodos();
    });

    cacheSelectors();
    reset();
    displayHandlerChange();

    function cacheSelectors() {
        boletosListar.body = $('body');
        boletosListar.CenarioBoleto = $('.taxa');
        boletosListar.CenarioTaxa = $('.boleto');
        boletosListar.CenarioLeitor = $('.leitor');
        boletosListar.valueCenarioTipoBoleto = $('#TipoDeBoletoId');
    }
   
    function displayHandlerChange(e) {
        cacheSelectors();
        if (boletosListar.valueCenarioTipoBoleto.val() === "") {
            reset();
        }
        if (boletosListar.valueCenarioTipoBoleto.val() !== "") {
            reset();
            scenaryFactory();
        }
    }
    
    function scenaryFactory() {

        if (boletosListar.valueCenarioTipoBoleto.val() === "1") {
            CenarioBoleto.show();
            CenarioLeitor.hide();
            $("#NumeroTaxa1").focus();
        }
        if (boletosListar.valueCenarioTipoBoleto.val() === "2") {
            CenarioTaxa.show();
            CenarioLeitor.hide();
            $("#NumeroBoleto1").focus();
        }

    }
    
    var inputs = $('.barras').keyup(function (e) {
        if ($(this).val().length == $(this).attr('maxlength')) {
            e.preventDefault();
            var nextInput = inputs.get(inputs.index(this) + 1);
            if (nextInput) {
                nextInput.focus();
            }
        }
    });


    $('#TipoDeBoletoId').change(function () {
        displayHandlerChange();
    });
        

    $('#DocumentoTipoId').change(function () {
        filterHandler();
    });

    function filterHandler() {
        $('#NumeroDocumento').removeClass("Subempenho");
        $('#NumeroDocumento').removeClass("RapRequisicao");

        if ($("#DocumentoTipoId").val() == 11) {
            $('#NumeroDocumento').addClass("RapRequisicao");
        } else if ($("#DocumentoTipoId").val() == 5) {
            $('#NumeroDocumento').addClass("Subempenho");
        }
    }

    function UtilizarLeitor() {
        reset();
        $("#NumeroDoCodigoDebarras").focus();
    }


    function reset() {
        boletosListar.CenarioBoleto.hide();
        boletosListar.CenarioTaxa.hide();
        boletosListar.CenarioLeitor.show();
    }


});



function ValidaFormularioBoleto() {
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
        AbrirModal("Informe ao menos um campo para filtro!");
        return false;
    } else {
        var msg = "";
        var data1;
        var data2;

        var dtInicial = $("#DataInicial").val();
        var dtFinal = $("#DataFinal").val();
        var dtEmissao = $("#DataEmissao").val();

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
                if (data1 && data2 && data1.getTime() > data2.getTime() && dtFinal.length > 0) {
                    msg = "A data De deve ser menor ou igual a data Até!";
                }
        }

        if ($("#DataFinal").length > 0) {
            if (dtFinal.length > 0 && isValidDate(dtFinal) == false) {
                msg = "A data final é invalida!";
            }
        }

        if ($("#DataEmissao").length > 0) {
            if (dtEmissao.length > 0 && isValidDate(dtEmissao) == false) {
                msg = "A data de Emissão é invalida!";
            }
        }

        var cnpj = $("#NumeroCnpjcpfFavorecido").val();
        if (cnpj != "" && $("#NumeroCnpjcpfFavorecido").length > 0) {
            cnpj = cnpj.replace(/[-\/\\^$*+?.()|[\]{}]/g, '');
            $("#NumeroCnpjcpfFavorecido").val(cnpj);
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


