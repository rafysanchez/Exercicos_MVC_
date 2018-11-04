
var programacaoDesembolsoList = [];
var selectedsDocumentoGerador = [];


function ConsultarDocumentoGerador() {

    selectedsDocumentoGerador = [];

    var TIPO_ID = $("#ProgramacaoDesembolsoTipoId").val();

    var entity = {};


    switch (TIPO_ID) {
        case "1":
            entity = {
                ProgramacaoDesembolsoTipoId: $("#ProgramacaoDesembolsoTipoId").val(),
                DocumentoTipoId: $("#DocumentoTipoId").val(),
                NumeroDocumento: $("#NumeroDocumento").val(),
                RegionalId: null,
                DataVencimento: null,
                CodigoDespesa: null
            }
            break;
        case "2":
            entity = {
                ProgramacaoDesembolsoTipoId: $("#ProgramacaoDesembolsoTipoId").val(),
                DocumentoTipoId: $("#DocumentoTipo").val(),
                NumeroDocumento: $("#Documento").val(),
                RegionalId: $("#Orgao").val(),
                DataVencimento: $("#DTVencimento").val(),
                CodigoDespesa: $("#TipoDespesa").val()
            }
            break;
        case "3":
            entity = {
                ProgramacaoDesembolsoTipoId: $("#ProgramacaoDesembolsoTipoId").val(),
                DocumentoTipoId: $("#DocumentoTipoId").val(),
                NumeroDocumento: $("#NumeroDocumento").val(),
                RegionalId: null,
                DataVencimento: null,
                CodigoDespesa: null
            }
            break;
        default: break;
    }

    var campo1 = 0;
    var campo2 = 0;



    if (entity.RegionalId != "") {
        campo1 += 1;
    }

    if (entity.DataVencimento != "") {
        campo1 += 1;
    }

    if (entity.CodigoDespesa != "") {
        campo1 += 1;
    }

    if (entity.DocumentoTipoId != "") {
        campo2 += 1;
    }

    if (entity.NumeroDocumento != "") {
        campo2 += 1;
    }



    if (TIPO_ID == 2 && (campo1 < 3 && campo2 < 2)) {
        AbrirModal("Preencha os campos Órgão (Regional), Data de Vencimento e Tipo de Despesa ou Tipo Documento e N° Documento");
        return false;
    }


    if (TIPO_ID == 2 && (campo2 == 2)) {

        entity.RegionalId = null;
    }


    $.ajax({
        datatype: 'JSON',
        type: 'POST',
        url: "/PagamentoContaUnica/ProgramacaoDesembolso/ConsultarDocumentoGerador",
        data: JSON.stringify({ entity: entity }),
        contentType: "application/json; charset=utf-8",
        beforeSend: function () {
            waitingDialog.show('Consultando');
        },
        success: function (dados) {
            if (dados.Status === 'Sucesso') {
                programacaoDesembolsoList = dados.ProgramacaoDesembolso;

                $.each(programacaoDesembolsoList,
                    function (index, x) {
                        programacaoDesembolsoList[index].DataEmissao = ConvertDateCSharp(x.DataEmissao);
                        programacaoDesembolsoList[index].DataVencimento = ConvertDateCSharp(x.DataVencimento);
                        programacaoDesembolsoList[index].ValorUnitario = MaskMonetario(x.Valor);

                    });

                if (programacaoDesembolsoList.length > 0) {


                    if (dados.mensagem != "") {
                        $.confirm({
                            text: dados.mensagem,
                            title: "Informação",
                            cancel: function () {
                                //ShowLoading();
                                //$("#form_filtro").submit();
                            },
                            cancelButton: "Ok",
                            confirmButton: "",
                            post: true,
                            cancelButtonClass: "btn-default",
                            modalOptionsBackdrop: true
                        });
                    };
                }
                else {

                    if (dados.mensagem == "") {

                        $.confirm({
                            text: "Não foram encontrados dados para solicitação",
                            title: "Informação",
                            cancel: function () {
                                //ShowLoading();
                                //$("#form_filtro").submit();
                            },
                            cancelButton: "Ok",
                            confirmButton: "",
                            post: true,
                            cancelButtonClass: "btn-default",
                            modalOptionsBackdrop: true
                        });
                    }
                    else {
                        $.confirm({
                            text: dados.mensagem,
                            title: "Informação",
                            cancel: function () {
                                //ShowLoading();
                                //$("#form_filtro").submit();
                            },
                            cancelButton: "Ok",
                            confirmButton: "",
                            post: true,
                            cancelButtonClass: "btn-default",
                            modalOptionsBackdrop: true
                        });

                    }


                }



                GerarTabelaDocGerador($("#ProgramacaoDesembolsoTipoId").val());

            }
            else {
                AbrirModal(dados.Msg);
            }
        },
        error: function (dados) {
            waitingDialog.hide();
            AbrirModal(dados);
        },
        complete: function () {
            waitingDialog.hide();
        }
    });
}

var editor;

function GerarTabelaDocGerador(tipo) {
    $("#MarcarTodos").prop("checked", false);
    var values = programacaoDesembolsoList;
    var table = "#tblPagamentosPreparar";


    if (!$(table).hasClass("_tbDataTables")) {
        $(table).addClass("_tbDataTables");
        BuildDataTables.ById(table);
    }

    $(table).DataTable().clear().draw(true);

    $(table).dataTable.ext.errMode = 'throw';

    $.each(values, function (index, x) {
        $(table).DataTable().row.add([
                   x.Sequencia.toString(),//0
                   x.NumeroDocumentoGerador,//1
                   (x.Regional != null) ? x.Regional : x.RegionalId,//2
                   x.CodigoUnidadeGestora,//3
                   x.CodigoGestao,//4
                   x.NumeroNLReferencia,//5
                   x.NumeroCnpjcpfCredor,//6
                   x.GestaoCredor,//7
                   x.NumeroBancoCredor,//8
                   x.NumeroAgenciaCredor,//9
                   x.NumeroContaCredor,//10
                   x.NumeroCnpjcpfPagto,//11
                   x.NomeCredorReduzido,//12
                   x.GestaoPagto,//13
                   x.NumeroBancoPagto,//14
                   x.NumeroAgenciaPagto,//15
                   x.NumeroContaPagto,//16
                   x.NumeroProcesso,//17
                   x.Finalidade,//18
                   x.NumeroEvento,//19
                   x.InscricaoEvento,//20
                   x.RecDespesa,//21 
                   x.Classificacao,//22
                   x.Fonte,//23
                   x.DataEmissao,//24
                   x.DataVencimento,//25
                   x.NumeroListaAnexo,//26
                   "R$ ".concat(MaskMonetario(x.Valor)),//27
                   x.NumeroDocumento,//28
                   x.MensagemServicoSiafem,//29
                   (tipo == 1 || tipo == 3) ?
                    "<button class='btn btn-xs btn-warning' onclick='ConfirmarPD(\"" + x.NumeroDocumentoGerador + "\")'><i>Confirmar</i></button>" :
                   (x.Valor == 0) ?
                   '<div class="checkbox MarcarTodos"><label><input type="checkbox" disabled="true"  id="checkbox' + x.Sequencia + '" name="checkbox1" onclick="SelectDocumentoGerador(this)" value=\"' + x.NumeroDocumentoGerador + '\"></label></div>' :
                    '<div class="checkbox MarcarTodos"><label><input type="checkbox"  id="checkbox' + x.Sequencia + '" name="checkbox1" onclick="SelectDocumentoGerador(this)" value=\"' + x.NumeroDocumentoGerador + '\"></label></div>',
                   '<button class="btn btn-xs btn-info editar" title="Editar" id="botaoEditar' + x.Sequencia + '" onclick="editarLinha(this,' + x.Sequencia + ')"><i class="fa fa-edit"></i></button>' +
                   '<button class="btn btn-xs btn-success salvar" title="Salvar" id="botaoSalvar' + x.Sequencia + '" onclick="salvarLinha(this,' + x.Sequencia + ')" style="display:none;"><i class="fa fa-floppy-o"></i></button>'

        ]).draw(true);

        if (x.Valor == 0) {
            $("#botaoEditar" + x.Sequencia).hide();

        }

    });

    configurarTabela(tipo);

    //if (tipo == 1 || tipo == 3) {
    //    $("#MarcarTodos").hide();
    //    $("#divbtnConfirmarAgrupamento").hide();
    //    $(".msgErro").hide();
    //    $(".acao").hide();
    //    $(".editar").hide();
    //} else if (tipo == 2) {
    //    $("#MarcarTodos").show();
    //    $("#divbtnConfirmarAgrupamento").show();
    //    $(".msgErro").hide();
    //    $(".salvar").hide();
    //    if (Entity.StatusSiafem == "E") { $(".editar").show(); } else { $(".editar").hide(); } 
    //} else {
    //    $(".MarcarTodos").hide();
    //    $("#divbtnConfirmarAgrupamento").show();
    //    $(".msgErro").show();
    //    $(".editar").show();
    //    $(".salvar").hide();
    //}

    $("#modalPagamentosPreparar").modal();
}

function configurarTabela(tipo) {
    //var naoTransmitidoSiafem = Entity.StatusSiafem === "E" || Entity.StatusSiafem === "N";

    if (tipo == 1 || tipo == 3) {
        $("#MarcarTodos").hide();
        $("#divbtnConfirmarAgrupamento").hide();
        $(".msgErro").hide();
        $(".acao").hide();

        $(".editar").hide();
    } else if (tipo == 2) {
        $("#MarcarTodos").show();
        $("#divbtnConfirmarAgrupamento").show();
        $(".msgErro").hide();
        $(".salvar").hide();

        //if (naoTransmitidoSiafem) {
        //    $(".editar").show();
        //}
        //else {
        //    $(".editar").hide();
        //}
    } else {
        $(".MarcarTodos").hide();
        $("#divbtnConfirmarAgrupamento").show();
        $(".msgErro").show();
        $(".editar").show();
        $(".salvar").hide();
    }
}



function TogleCampos() {
    var $values = programacaoDesembolsoList;

    var item;
    $.each(programacaoDesembolsoList, function (index, x) {
        item = programacaoDesembolsoList[index];
        if (item.StatusSiafem == 'N' || item.StatusSiafem == 'E' || item.StatusSiafem == null) {
            $("#botaoEditar" + item.Sequencia).show();
        }
        else {
            $("#botaoEditar" + item.Sequencia).hide();
        }

        var fromConsulta = $("#fromConsulta").val();

        if (item.Valor <= 0 && fromConsulta === "1") {
            $("#checkbox" + item.Sequencia).attr("disabled", true);
            //$("#checkbox" + item.Sequencia).hide();
        }

    });
}

var selectedRow;

function editarLinha(nRow, id) {
    $("#botaoEditar" + id).hide();
    $("#botaoSalvar" + id).show();
    selectedRow = $(nRow).parent();
    var aData = $("#tblPagamentosPreparar").dataTable().fnGetData(selectedRow);

    for (var i = 2; i < 28; i++) {
        aData[i] = aData[i] == null ? "" : aData[i];
        var addClass = i == 24 || i == 25 ? "data datepicker" : "";
        addClass = addClass + (i == 27 ? "real" : "");
        addClass = addClass + (i == 5 ? "nl" : ""); // NL

        var maxlength = "";
        var varId = "";
        switch (i) {
            case 2:
                maxlength = "maxlength = " + 2; //Orgão(Regional)
                break;
            case 3:
                maxlength = "maxlength = " + 6; //Unidade Gestora
                break;
            case 4:
                maxlength = "maxlength = " + 5; //Gestão
                break;
                //case 5:
                //    maxlength = "" // NL, já configurado acima através da classe nl
                //    break;
            case 6:
                maxlength = "maxlength = " + 6; //Unidade Gestora Pagadora
                break;
            case 7:
                maxlength = "maxlength = " + 5; //Gestão Pagadora
                break;
            case 8:
                maxlength = "maxlength = " + 3; //Banco
                varId = "id = 'banco" + id + "'";
                break;
            case 9:
                maxlength = "maxlength = " + 5; //Agência
                varId = "id = 'agencia" + id + "'";
                break;
            case 10:
                maxlength = "maxlength = " + 10; //Conta
                varId = "id = 'conta" + id + "'";
                break;
            case 11:
                maxlength = "maxlength = " + 14; //CPF/CNPJ Credor
                break;
            case 12:
                maxlength = "maxlength = " + 20; //Nome Reduzido
                break;
            case 13:
                maxlength = "maxlength = " + 5; //Gestão Favorecido
                break;
            case 14:
                maxlength = "maxlength = " + 3; //Banco Favorecido
                break;
            case 15:
                maxlength = "maxlength = " + 5; //Agência Favorecido
                break;
            case 16:
                maxlength = "maxlength = " + 10; //Conta Favorecido
                break;
            case 17:
                maxlength = "maxlength = " + 15; //Processo
                break;
            case 18:
                maxlength = "maxlength = " + 40; //Finalidade
                break;
            case 19:
                maxlength = "maxlength = " + 6; //Evento
                break;
            case 20:
                maxlength = "maxlength = " + 22; //Inscrição
                break;
            case 21:
                maxlength = "maxlength = " + 10; //Rec. Despesa, não consta no documento
                break;
            case 22:
                maxlength = "maxlength = " + 9; //Classificação
                break;
            case 23:
                maxlength = "maxlength = " + 9; //Fonte
                break;
                //case 24:
                //    maxlength = "" //Data de Emissão, já tratado acima com a class datepicker
                //    break;
                //case 25:
                //    maxlength = "" //Data de Vencto, já tratado acima com a class datepicker
                //    break;
            case 26:
                maxlength = "maxlength = " + 11; //Lista Anexo
                break;
            case 27:
                maxlength = "maxlength = " + 20 //Valor, já tratado acima com a class real
                varId = "id = 'valor" + id + "'";
                break;
        }

        var newElement = "<input " + varId + " type='text' " + maxlength + " class='form-control " + addClass + "' value='" + aData[i] + "'>";
        $($(selectedRow.parent()).find("> *")[i]).html(newElement);
    }

    $(".real").maskMoney({ prefix: "R$ ", allowNegative: false, thousands: ".", decimal: ",", affixesStay: true });
    $(".real").maskMoney('mask');
}


function salvarLinha(nRow, id) {
    $("#botaoEditar" + id).show();
    $("#botaoSalvar" + id).hide();
    selectedRow = $(nRow).parent();

    if ($("#conta" + id).val() === "") {
        AbrirModal("Campo Conta Corrente(pagador) obrigatório");
        $("#conta" + id).focus();
        $("#botaoEditar" + id).hide();
        $("#botaoSalvar" + id).show();
        return false;
    }

    if ($("#valor" + id).val() === "") {
        AbrirModal("Campo Valor obrigatório");
        $("#valor" + id).focus();
        $("#botaoEditar" + id).hide();
        $("#botaoSalvar" + id).show();
        return false;
    }


    var $tabela = $("#tblPagamentosPreparar");
    var oTable = $tabela.dataTable();
    var allPages = $tabela.dataTable().fnGetNodes();
    var $checkTodos = $("#MarcarTodos", $tabela).is(':checked');
    var $checks = $('input[name="checkbox1"]', allPages);



    var aData = $("#tblPagamentosPreparar").dataTable().fnGetData(selectedRow);
    for (var i = 2; i < 28; i++) {
        aData[i] = $($(selectedRow.parent()).find("> td > input")[0]).val();
        $($(selectedRow.parent()).find("> *")[i]).html(aData[i]);
    }

    aData[29] = "";
    $($(selectedRow.parent()).find("> *")[29]).html(aData[29]);

    desembolsoAgrupamento.save(aData);


    ToogleMarcarTeste($checks, $checkTodos);

}

function ConfirmarPD(numDoc) {

    pagamentoEvento.table.dataTable().fnDeleteRow();

    $.each(pagamentoEvento.EntityList, function () {

        pagamentoEvento.EntityList.shift();

    });


    FecharModal("#modalPagamentosPreparar");

    var selectedItem = programacaoDesembolsoList.filter(function (item) {
        if (item.NumeroDocumentoGerador === numDoc)
            return item;
    })[0];

    selectedItem.ProgramacaoDesembolsoTipoId = $('#ProgramacaoDesembolsoTipoId').val();

    AtualizarFormularioById(selectedItem, ['DataEmissao']);

    $('#NumeroNLReferencia').val(selectedItem.numNLRef);
    $('#NumeroContrato').val(selectedItem.NumeroContrato);
    $('#CodigoAplicacaoObra').val(selectedItem.CodigoAplicacaoObra);

    $('#NumeroNLReferenciaBec').val(selectedItem.numNLRef);
    $('#NumeroContratoBec').val(selectedItem.NumeroContrato);
    $('#CodigoAplicacaoObraBec').val(selectedItem.CodigoAplicacaoObra);
    $('#NumeroNE').val(selectedItem.NumeroNE);

    programacaoDesembolso.displayHandlerNumeroNEBlur();

    $(".real").maskMoney({ prefix: "R$ ", allowNegative: false, thousands: ".", decimal: ",", affixesStay: true });
    $(".real").maskMoney('mask');


    programacaoDesembolsoValidator.Entity.CodigoDespesa = selectedItem.CodigoDespesa;
    programacaoDesembolsoValidator.Entity.RecDespesa = selectedItem.RecDespesa;

    if (selectedItem.ProgramacaoDesembolsoTipoId == 1) {
        pagamentoEvento.add();
    }

}

function SelectDocumentoGerador(check) {
    var selectDoc = $(check).val();
    if ($(check).is(":checked")) {
        selectedsDocumentoGerador = selectedsDocumentoGerador.filter(function (item) {
            return item !== selectDoc;
        });
        selectedsDocumentoGerador.push(selectDoc);
    } else {
        selectedsDocumentoGerador = selectedsDocumentoGerador.filter(function (item) {
            return item !== selectDoc;
        });
    }
}

function toggleMarcarTodos(event) {

    // Se a sua coluna tiver ordenacao, tem que prevenir de ordenar ao clicar no checkbox! 
    event.stopPropagation ? event.stopPropagation() : event.returnValue = false;
    ToogleMarcarTodos();
}

$('#tblPagamentosPreparar').on('page.dt', function () {
    //setTimeout(ToogleMarcarTodos, 1);
}).on('draw.dt', function () {
    configurarTabela($("#ProgramacaoDesembolsoTipoId").val());

    var $tabela = $("#tblPagamentosPreparar");
    var oTable = $tabela.dataTable();
    var allPages = $tabela.dataTable().fnGetNodes();
    var $checkTodos = $("#MarcarTodos", $tabela).is(':checked');
    var $checks = $('input[name="checkbox1"]');

    TogleCampos();

    ToogleMarcarTeste($checks, $checkTodos);

    if ($checkTodos) {
        ToogleMarcarTodos();
    }

 




    

});

function ToogleMarcarTodos() {
    var $tabela = $("#tblPagamentosPreparar");

    var oTable = $tabela.dataTable();
    var allPages = $tabela.dataTable().fnGetNodes();

    var check = $("#MarcarTodos", $tabela).is(':checked');
    var $checks = $('input[name="checkbox1"]', allPages);
    selectedsDocumentoGerador = [];

    if (check) {
        $checks.each(function () {
            if ($(this).prop("disabled") !== true) {
                $(this).prop("checked", true);
                var selectDoc = $(this).val();
                selectedsDocumentoGerador.push(selectDoc);
            }
        });

    } else {
        $checks.each(function () {
            $(this).prop("checked", false);
        });

        selectedsDocumentoGerador = [];
    }
}


function ToogleMarcarTeste(selecionados, selTodos) {


    var $tabela = $("#tblPagamentosPreparar");
    var oTable = $tabela.dataTable();
    var allPages = $tabela.dataTable().fnGetNodes();


    var $check = $('input[name="MarcarTodos"]', $tabela);
    var $checks = $('input[name="checkbox1"]', allPages);
    //selectedsDocumentoGerador = [];

    $check.prop("checked", selTodos);

    var $Select = selecionados;

    $checks.each(function () {

        var $w = this;

        $Select.each(function () {

            if ($(this).prop("id") == $w.id) {

                if ($(this).prop("checked") == true) {
                    $w.checked = true;
                } else {
                    $w.checked = $(this).prop("checked");
                }

            }

        });

    });

}