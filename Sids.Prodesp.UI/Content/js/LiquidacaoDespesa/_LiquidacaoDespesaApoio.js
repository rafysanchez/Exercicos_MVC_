var ConsultarEmpenhoRapList = [];


function consultarSubempenhoApoio() {

    var online = navigator.onLine;

    if (online != true) {
        AbrirModal("Erro de conexão");
        return false;
    }

    var subempenho = {
        CenarioSiafemSiafisico: $('#CenarioSiafemSiafisico').val(),
        NumeroOriginalProdesp: $('#NumeroOriginalProdesp').val(),
        CodigoTarefa: $('#CodigoTarefa').val(),
        CodigoDespesa: $('#CodigoDespesa').val(),
        ValorRealizado: $('#ValorRealizado').val().replace("R$ ", "").replace(/[.,]/g, ""),
        NumeroRecibo: $('#NumeroRecibo').val(),
        PrazoPagamento: $('#PrazoPagamento').val(),
        DataRealizado: $('#DataRealizado').val()
    }

    if (subempenho.NumeroOriginalProdesp.length === 0 ||
        subempenho.CodigoTarefa.length === 0 ||
        subempenho.CodigoDespesa.length === 0 ||
        subempenho.ValorRealizado <= 0 ||
        subempenho.CenarioSiafemSiafisico === "" || (
        subempenho.NumeroRecibo.length === 0 && (
        subempenho.PrazoPagamento.length === 0 ||
        subempenho.DataRealizado.length === 0))) {
        AbrirModal('Os Campos: Tipo de Apropriação / Subempenho, Nº do Empenho, Código da Tarefa, Código da Despesa, Valor Realizado e Nº do Recibo ou Prazo de Pagamento e Data da Realização devem ser todos preenchidos.');
        return false;
    }

    if (subempenho.NumeroRecibo.length > 0) {
        if (subempenho.PrazoPagamento.length > 0 || subempenho.DataRealizado.length > 0) {
            AbrirModal('Favor Preencher o Campo Nº do Recibo ou Prazo de Pagamento e Data da Realização.');
            return false;
        }
    }


    DadosEmpenho(subempenho.NumeroOriginalProdesp, 3);

    setTimeout(function () {
        $.ajax({
            datatype: 'JSON',
            type: 'POST',
            url: '/ConsutasBase/ConsultarSubempenhoApoio',
            data: JSON.stringify({ subempenho: subempenho }),
            contentType: 'application/json; charset=utf-8',
            async: true,
            beforeSend: function () {
                waitingDialog.show('Consultando');
            },
            success: function (dados) {
                if (dados.Status === 'Sucesso') {


                    $('#CodigoAplicacaoObra').val(dados.SubempenhoApoio.outAplicObra.replace(/(\d)(\d{1})$/, "$1-$2"));

                    if (dados.SubempenhoApoio.outAplicObra == null)
                        $('#CodigoAplicacaoObra').removeAttr("disabled");

                    $('#NaturezaId').val(dados.SubempenhoApoio.outCED);
                    $("#FonteId option").text(dados.SubempenhoApoio.outOrigemRecurso);
                    $('#NumeroCNPJCPFFornecedorId').val(dados.SubempenhoApoio.outCGC);
                    $('#ProgramaId').val(dados.SubempenhoApoio.outCFP);
                    $('#CodigoCredorOrganizacaoId').val(dados.SubempenhoApoio.outOrganiz);
                    $('#RegionalId').val(dados.SubempenhoApoio.outOrgao);
                    $('#CenarioProdesp').val(dados.SubempenhoApoio.outInfoTransacao.replace("Cotrato", "Contrato"));
                    $('#NaturezaSubempenhoId').val(dados.SubempenhoApoio.outNatureza);
                    $('#NumeroMedicao').val(dados.SubempenhoApoio.outNumMedicao);
                    $('#Contrato').val(dados.SubempenhoApoio.outContrato.replace(" ", ".").replace(" ", "."));

                    if (dados.SubempenhoApoio.outInfoTransacao === "SubEmpenhoContrato" ||
                        dados.SubempenhoApoio.outInfoTransacao === "SubEmpenhoRecibo") {
                        $("#DescricaoEspecificacaoDespesa1")
                            .val("PARA ATENDER AS DESPESAS DE SERVICO DA MEDICAO " +
                                dados.SubempenhoApoio
                                .outNumMedicao);
                        $("#DescricaoEspecificacaoDespesa2").val("DO CONTRATO " + dados.SubempenhoApoio.outContrato);
                        $("#DescricaoEspecificacaoDespesa3").val("NOTA FISCAL:" + dados.SubempenhoApoio.outNotaFiscal);
                        $("#CodigoNotaFiscalProdesp").val(dados.SubempenhoApoio.outNotaFiscal);
                    }

                    liquidacao.displayHandlerButton();
                } else {
                    AbrirModal(dados.Msg);
                }
            },
            error: function (dados) {
                AbrirModal(dados);
            },
            complete: function () {
                waitingDialog.hide();
            }
        });
    }, 2000);

}

function consultarAnulacaoApoio() {
    var online = navigator.onLine;

    if (online != true) {
        AbrirModal("Erro de conexão");
        return false;
    }

    var anulacao = {
        NumeroOriginalProdesp: $('#NumeroSubempenhoProdesp').val().replace("/", ""),
        //Valor: $('#ValorAnular').val().replace("R$", "").replace(/[.,]/g, "")
        Valor: $('#ValorAnular').val().replace("R$", "").replace("0,00", "").replace(",", "").trim()
    }

    if (anulacao.NumeroOriginalProdesp.length === 0 || anulacao.Valor.length === 0) {
        AbrirModal('Os Campos Nº do Subempenho e o Valor a Anular devem ser preenchidos.');
        return false;
    }

    $.ajax({
        datatype: 'JSON',
        type: 'POST',
        url: '/ConsutasBase/ConsultarAnulacaoApoio',
        data: JSON.stringify({ anulacao: anulacao }),
        contentType: 'application/json; charset=utf-8',
        beforeSend: function () {
            waitingDialog.show('Consultando');
        },
        success: function (dados) {
            if (dados.Status === 'Sucesso') {
                $('#DataRealizado').val(ConverterData(dados.SubempenhoCancelamento.outDataRealizacao));

                //$('#ValorRealizado').val(dados.SubempenhoCancelamento.outValorRealizado);


                $("#ValorAnulado").val(dados.SubempenhoCancelamento.outValorRealizado);


                $("#ValorAnulado").attr("disabled", true);

                $(".real").maskMoney('mask');

                if (dados.SubempenhoCancelamento.outInfoTransacao === 'ComContrato') {
                    $('#CenarioProdesp').val('SubEmpenhoContrato');
                }
                if (dados.SubempenhoCancelamento.outInfoTransacao === 'SemContrato') {
                    $('#CenarioProdesp').val('SubEmpenho');
                }

                DadosSubempenho(anulacao.NumeroOriginalProdesp, 0);

                DadosEmpenho(anulacao.NumeroOriginalProdesp, 3, function () {
                    if (!liquidacao.cenarioContem([7])) {
                        $("#Valor").val(dados.SubempenhoCancelamento.outValorRealizado);
                        $("#Valor").attr("disabled", true);
                    }
                    else {
                        liquidacao.displayHandlerValorAnular();
                        $("#Valor").attr("disabled", false);
                    }
                });


                liquidacao.displayHandlerButton();
            }
            else {
                AbrirModal(dados.Msg);
            }
        },
        error: function (dados) {
            AbrirModal(dados);
        },
        complete: function () {
            waitingDialog.hide();
        }
    });
}

function carregarDadosAnulacaoRap(dados) {

    $('#CenarioProdesp').val(dados.RapAnulacao.outInfoTransacao);

    $("#DescricaoEspecificacaoDespesa1").val('');
    $("#DescricaoEspecificacaoDespesa2").val('');
    $("#DescricaoEspecificacaoDespesa3").val('');
    $("#DescricaoEspecificacaoDespesa4").val('');
    $("#DescricaoEspecificacaoDespesa5").val('');
    $("#DescricaoEspecificacaoDespesa6").val('');
    $("#DescricaoEspecificacaoDespesa7").val('');
    $("#DescricaoEspecificacaoDespesa8").val('');
    $("#DescricaoEspecificacaoDespesa9").val('');
    $("#CodigoNotaFiscalProdesp").val('');

    if (dados.RapAnulacao.outInfoTransacao === "ComContrato") {
        $("#DescricaoEspecificacaoDespesa1").val(dados.RapAnulacao.outEspecificacao1);

        $("#DescricaoEspecificacaoDespesa2").val(dados.RapAnulacao.outEspeficicacao2);

        $("#DescricaoEspecificacaoDespesa3").val(dados.RapAnulacao.outEspeficicacao3);
    } else {
        $("#EspecDespesa").val('');
    }

    //if ($("#EspecDespesa").val() === "") {
    //    $("#EspecDespesa").val("000");
    //}

    liquidacao.displayHandlerButton();
}

function carregarDadosInscritoRap(dados) {

    $('#CenarioProdesp').val(dados.RapRequisicao.outInfoTransacao);

    $("#DescricaoEspecificacaoDespesa1").val('');
    $("#DescricaoEspecificacaoDespesa2").val('');
    $("#DescricaoEspecificacaoDespesa3").val('');
    $("#DescricaoEspecificacaoDespesa4").val('');
    $("#DescricaoEspecificacaoDespesa5").val('');
    $("#DescricaoEspecificacaoDespesa6").val('');
    $("#DescricaoEspecificacaoDespesa7").val('');
    $("#DescricaoEspecificacaoDespesa8").val('');
    $("#DescricaoEspecificacaoDespesa9").val('');
    $("#CodigoNotaFiscalProdesp").val('');

    if (dados.RapRequisicao.outInfoTransacao === "RAPRecibo" || dados.RapRequisicao.outInfoTransacao === "RAPContrato") {
        $("#DescricaoEspecificacaoDespesa1")
            .val("PARA ATENDER AS DESPESAS DE SERVICO DA MEDICAO " +
                dados.RapRequisicao.outMedicao);
        $("#DescricaoEspecificacaoDespesa2").val("DO CONTRATO " + dados.RapRequisicao.outContrato);
        $("#DescricaoEspecificacaoDespesa3").val("NOTA FISCAL:" + dados.RapRequisicao.outNFF);
        $("#CodigoNotaFiscalProdesp").val(dados.RapRequisicao.outNFF);
    }

    if ($("#EspecDespesa").val() === ""){
        $("#EspecDespesa").val("000");
    }

    liquidacao.displayHandlerButton();
}

function requisicaoAnulacaoRap() {
    validateConnectionIsOpen();

    var numRequisicaoAnulacaoRap = $('#NumeroRequisicaoRap').val();
    var msg = "Favor informar um numero para Consulta requisição de RAP Prodesp";

    if (numRequisicaoAnulacaoRap.length <= 0) {
        AbrirModal(msg);
        return false;
    }

    $("#Valor").val('0,00');
    $("#ValorAnulado").val('0,00');
    $('#Classificacao').val('');
    $('#NumeroProcesso').val('');
    $('#DescricaoAutorizadoSupraFolha').val('')
    
    $.ajax({
        datatype: 'JSON',
        type: 'POST',
        url: "/ConsutasBase/ConsultarRapAnulacaoApoio",
        data: JSON.stringify({ requisicaoAnulacaoRap: numRequisicaoAnulacaoRap }),
        contentType: "application/json; charset=utf-8",
        beforeSend: function () {
            waitingDialog.show('Consultando');
        },
        success: function (dados) {
            if (dados.Status === 'Sucesso') {

                DadosRap(numRequisicaoAnulacaoRap);
                $('#CenarioProdesp').val(dados.RapAnulacao.outInfoTransacao);

                RefazerCombo("#Orgao");
                $('#Orgao  option[value="' + dados.RapAnulacao.outOrgao + '"]').attr("selected", true);

                $('#AnoExercicio').attr("disabled", true);

                RefazerCombo("#ProgramaId");
                $('#Programa  option[value="' + dados.RapAnulacao.outCFP + '"]').attr("selected", true);

                GerarComboNatureza();
                $('#Natureza  option[value="' + dados.RapAnulacao.outCED + '"]').attr("selected", true);

                $('#CodigoAplicacaoObra').val(dados.RapAnulacao.outAplicObra.replace(/[.,-/]/g, "").replace(/(\d)(\d{1})$/, "$1-$2"));
                IsAttr($('#CodigoAplicacaoObra'), dados.RapAnulacao.outAplicObra, "Readonly");
                $('#CodigoTarefa').val(dados.RapAnulacao.outCodTarefa);
                IsAttr($('#CodigoTarefa'), dados.RapAnulacao.outCodDespesa, "disabled");

                $('#CodigoDespesa').val(dados.RapAnulacao.outCodDespesa);
                IsAttr($('#CodigoDespesa'), dados.RapAnulacao.outCodDespesa, "Readonly");
                
                $('#DescricaoEspecificacaoDespesa1').val(dados.RapAnulacao.outEspecificacao1);
                $('#DescricaoEspecificacaoDespesa2').val(dados.RapAnulacao.outEspeficicacao2);
                $('#DescricaoEspecificacaoDespesa3').val(dados.RapAnulacao.outEspeficicacao3);
                $('#DescricaoEspecificacaoDespesa4').val(dados.RapAnulacao.outEspeficicacao4);
                $('#DescricaoEspecificacaoDespesa5').val(dados.RapAnulacao.outEspeficicacao5);
                $('#DescricaoEspecificacaoDespesa6').val(dados.RapAnulacao.outEspeficicacao6);
                $('#DescricaoEspecificacaoDespesa7').val(dados.RapAnulacao.outEspeficicacao7);

                //DadosRap(numRequisicaoAnulacaoRap);
                carregarDadosAnulacaoRap(dados);
            }
            else {
                AbrirModal(dados.Msg);
            }
        },
        error: function (dados) {
            AbrirModal(dados);
        },
        complete: function () {
            waitingDialog.hide();
        }
    });
}

function carregarDadosRapSaldo(values) {

    $('#tblListaEmpenhoSaldo > thead').remove();
    $('#tblListaEmpenhoSaldo').append("<thead></thead>");

    $('#tblListaEmpenhoSaldo > thead').append('<tr>' +
        '<th class="text-center">Empenho</th>' +
        '<th class="text-center">C. Aplic.</th>' +
        '<th class="text-center">Valor Atual</th>' +
        '<th class="text-right">Valor SubEmpenhado</th>' +
        '<th class="text-center">Disp. a SubEmpenhar</th>' +
        '<th class="text-center"></th></tr>');

    if (!$("#tblListaEmpenhoSaldo").hasClass("_tbDataTables")) {
        $('#tblListaEmpenhoSaldo').addClass("_tbDataTables");
        BuildDataTables.ById('#tblListaEmpenhoSaldo');
    }

    $('#tblListaEmpenhoSaldo').DataTable().clear().draw(true);
    $.each(values,
        function (index, x) {
            $('#tblListaEmpenhoSaldo')
                .DataTable()
                .row.add([
                    x.outNrEmpenho,
                    x.outAplicacao,
                    x.outValorAtual,
                    x.outValorSubEmpenhado,
                    x.outDispSubEmpenhar,
                    "<button class='btn btn-xs btn-primary' onclick='DadosEmpenho(" +
                    x.outNrEmpenho +
                    ",0)'><i class='fa fa-search'></i></button>"
                ])
                .draw(true);
        });

}

function consultarEmpenhoSaldoRap() {
    validateConnectionIsOpen();

    var ano = $('#AnoExercicio').val();
    var orgao = $('#Orgao').val();

    var dtoEmpenhoSaldoRap =
    {
        NumeroAnoExercicio: ano,
        RegionalId: orgao
    };


    var msg = "O Ano e o Orgão devem ser preenchidos";
    if (dtoEmpenhoSaldoRap.NumeroAnoExercicio === "" || dtoEmpenhoSaldoRap.RegionalId === "") {
        AbrirModal(msg); return false;
    }


    $.ajax({
        datatype: 'JSON',
        type: 'POST',
        url: "/ConsutasBase/ConsultaEmpenhoSaldoRAP",
        data: JSON.stringify({ entity: dtoEmpenhoSaldoRap }),
        contentType: "application/json; charset=utf-8",
        beforeSend: function () {
            waitingDialog.show('Consultando');
        },
        success: function (dados) {
            if (dados.Status === 'Sucesso') {
                $('#modalConsultaEmpenhoSaldo').modal("toggle");
                $('#txtAnoSaldo').val(ano);
                $('#txtOrgaoSaldo').val(dados.EmpenhoSaldo[0].outOrgao);
                carregarDadosRapSaldo(dados.EmpenhoSaldo);
            }
            else {
                AbrirModal(dados.Msg);
            }
        },
        error: function (dados) {
            AbrirModal(dados);
        },
        complete: function () {
            waitingDialog.hide();
        }
    });
}

function SubempenhoInscricaoRap() {
    validateConnectionIsOpen();
    var subempenhoInscricaoRap = $('#NumeroSubempenho').val();
    var recibo = $('#NumeroRecibo').val();

    var dtoSubempenhoInscricaoRap =
    {
        NumeroSubempenho: subempenhoInscricaoRap,
        NumeroRecibo: recibo
    };


    $.ajax({
        datatype: 'JSON',
        type: 'POST',
        url: "/ConsutasBase/ConsultarRapRequisicaoApoio",
        data: JSON.stringify({ entity: dtoSubempenhoInscricaoRap }),
        contentType: "application/json; charset=utf-8",
        beforeSend: function () {
            waitingDialog.show('Consultando');
        },
        success: function (dados) {
            if (dados.Status === 'Sucesso') {

                $('#CenarioProdesp').val(dados.RapRequisicao.outInfoTransacao);
                $('#NumeroOriginalProdesp').val(Left(subempenhoInscricaoRap, 9));

                RefazerCombo("#Orgao");
                $('#Orgao  option[value="' + dados.RapRequisicao.outOrgao + '"]').attr("selected", true);
                IsAttr($('#Orgao'), dados.RapRequisicao.outOrgao, "disabled");
                $('#AnoExercicio').attr("disabled", true);

                RefazerCombo("#ProgramaId");
                $('#Programa  option[value="' + dados.RapRequisicao.outCFP + '"]').attr("selected", true);
                IsAttr($('#Programa'), dados.RapRequisicao.outCFP, "disabled");

                GerarComboNatureza();
                $('#Natureza  option[value="' + dados.RapRequisicao.outCED + '"]').attr("selected", true);
                IsAttr($('#Natureza'), dados.RapRequisicao.outCED, "disabled");

                $('#CodigoAplicacaoObra').val(dados.RapRequisicao.outAplicObra.replace(/[.,-/]/g, "").replace(/(\d)(\d{1})$/, "$1-$2"));
                IsAttr($('#CodigoAplicacaoObra'), dados.RapRequisicao.outAplicObra, "Readonly");

                $('#NumeroCNPJCPFFornecedor').val(dados.RapRequisicao.outCGC.replace(/[.,-/]/g, "")); 
                //IsAttr($('#NumeroCNPJCPFFornecedor'), dados.RapRequisicao.outCGC, "Readonly");
               
                $('#CodigoCredorOrganizacao').val(dados.RapRequisicao.outOrganiz); // utilizado na requisição de rap
                $('#CodigoCredorOrganizacaoId').val(dados.RapRequisicao.outOrganiz);

                IsAttr($('#CodigoCredorOrganizacaoId'), dados.RapRequisicao.outOrganiz, "Readonly");

                if (dados.RapRequisicao.outInfoTransacao === "RAPRecibo") {
                    RefazerCombo("#NaturezaSubempenhoId");
                    $('#NaturezaSubempenhoId  option[value="' + dados.RapRequisicao.outNatureza + '"]').attr("selected", true);
                    IsAttr($('#NaturezaSubempenhoId'), dados.RapRequisicao.outNatureza, "disabled");

                    $('#NumeroMedicao').val(dados.RapRequisicao.outMedicao);
                    IsAttr($('#NumeroMedicao'), dados.RapRequisicao.outMedicao, "Readonly");

                    $('#DataRealizado').val(ConverterData(dados.RapRequisicao.outDataRealizacao));
                    IsAttr($('#DataRealizado'), dados.RapRequisicao.outDataRealizacao, "disabled");

                    $('#DescricaoPrazoPagamento').val(dados.RapRequisicao.outPrazoPagto);
                    IsAttr($('#DescricaoPrazoPagamento'), dados.RapRequisicao.outPrazoPagto, "Readonly");

                    $('#CodigoNotaFiscalProdesp').val(dados.RapRequisicao.outNFF);
                    
                }
                DadosSubempenho(subempenhoInscricaoRap.replace("/", ""), 0);
                carregarDadosInscritoRap(dados);
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


function EmpenhoRap() {
    validateConnectionIsOpen();

    var empenhoRap = $('#NumeroEmpenho').val();


    var dtoEmpenhoRap =
    {
        NumeroProdesp: empenhoRap
    };


    waitingDialog.show('Consultando');

    $.ajax({
        datatype: 'JSON',
        type: 'POST',
        url: "/ConsutasBase/ConsultarEmpenhoRap",
        data: JSON.stringify({ entity: dtoEmpenhoRap }),
        contentType: "application/json; charset=utf-8",
        beforeSend: function () {
            waitingDialog.show('Consultando');
        },
        success: function (dados) {
            if (dados.Status === 'Sucesso') {

                ConsultarEmpenhoRapList = dados.RapRequisicao.ListConsultarEmpenhoRap;

                $('#modalConsultaEmpenhoRAP').modal();


                $('#NumEmpenho').val(dados.RapRequisicao.outNrEmpenho);
                $('#NomeCredor').val(dados.RapRequisicao.outCredor);
                $('#NumeroContrato').val(dados.RapRequisicao.outNrContrato);



                $('#valInicialEmp').val(dados.RapRequisicao.outValorIniRef_EMP);
                $('#valAnulacaoEmp').val(dados.RapRequisicao.outValorAnul_EMP);
                $('#valLiqEmpenhadoEmp').val(dados.RapRequisicao.outValorLiqEmp_EMP);


                $('#valPagoExercicioSub').val(dados.RapRequisicao.outValorpagoExerc_SUB);
                $('#valInscritoSub').val(dados.RapRequisicao.outValorInscRAP_SUB);
                $('#valPagoRapSub').val(dados.RapRequisicao.outValorReqRAP_SUB);
                $('#valAnuladoRapSub').val(dados.RapRequisicao.outValorAnulRAP_SUB);
                $('#valRequisitarSub').val(dados.RapRequisicao.outSaldoReq_SUB);
                $('#valTotalSub').val(dados.RapRequisicao.outTotalUtilizado_SUB);


                $('#valSubEmpenhoPag').val(dados.RapRequisicao.outValorSubEmp_PAG);
                $('#valRequisicaoPag').val(dados.RapRequisicao.outValorReq_PAG);
                $('#valPagarPag').val(dados.RapRequisicao.outValorPagar_PAG);


                GerarTabelaEmpenhoRap(ConsultarEmpenhoRapList);


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


function GerarTabelaEmpenhoRap(values) {


    $('#tblListaSubEmpenhoRap > thead').remove();
    $('#tblListaSubEmpenhoRap').append("<thead></thead>");

    $('#tblListaSubEmpenhoRap > thead').append("<tr>" +
        "    <th>Nº do SubEmpenho</th>" +
        "    <th>Cód. Despesa</th>" +
        "    <th>Data da Realização</th>" +
        "    <th>Nome Reduzido Credor da NSE</th>" +
        "    <th>Líquido Subempenhado</th>" +
        "    <th>Data de Vencimento</th>" +
        "    <th>OD</th>" +
        "    <th>Data de Pagamento</th>" +
        "    <th></th>" +
        "</tr>");

    if (!$("#tblListaSubEmpenhoRap").hasClass("_tbDataTables")) {
        $('#tblListaSubEmpenhoRap').addClass("_tbDataTables");
        BuildDataTables.ById('#tblListaSubEmpenhoRap');
    }

    $('#tblListaSubEmpenhoRap').DataTable().clear().draw(true);

    $.each(values,
        function (index, x) {
            $('#tblListaSubEmpenhoRap').DataTable().row.add([
                     x.outNrSubEmpenho,
                     x.outCodDespesa,
                     x.outDtRealizacao,
                     x.outNomeCredor,
                     x.outLiqSubEmpenhado,
                     x.outDtVencimento,
                     x.outOD,
                     x.outDtPagto,
                    "<button class='btn btn-xs btn-warning' id='botaoConfirmar" + x.outNrSubEmpenho + "'   onclick='ConfirmarEmpenho(\"" + x.outNrSubEmpenho + "\")'><i>Confirmar</i></button>"


            ])
                .draw(true);

            if (x.outDtRealizacao != "") {
                $("#botaoConfirmar" + x.outNrSubEmpenho).hide();

            }


        });
}




function ConfirmarEmpenho( nrSubEmpenho) {


    var numEmpenho = $('#NumEmpenho').val() + '/' + nrSubEmpenho;


    $('#NumeroSubempenho').val(numEmpenho);

    SubempenhoInscricaoRap();


    $('#NumeroSubempenho').val("") ;

    FecharModal("#modalConsultaEmpenhoRAP");




}




$('#tblListaSubEmpenhoRap').on('page.dt', function () {
    //setTimeout(ToogleMarcarTodos, 1);
}).on('draw.dt', function () {


    TogleCampos();


});



function TogleCampos() {
    var $values = ConsultarEmpenhoRapList;

    var item;
    $.each(ConsultarEmpenhoRapList, function (index, x) {
        item = ConsultarEmpenhoRapList[index];



        if (item.outDtRealizacao != "") {
            $("#botaoConfirmar" + item.outNrSubEmpenho).hide();
        }
        else {
            $("#botaoConfirmar" + item.outNrSubEmpenho).show();
        }

    });
}
