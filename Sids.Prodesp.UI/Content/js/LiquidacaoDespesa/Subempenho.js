var obj = "Subempenho";
var area = "LiquidacaoDespesa";
var tans = "";
var isLoad;
var tipoContrato = "0";

$(document).ready(function () {
    isLoad = true;

    var campoVazio = "Campo obrigatório não preenchido";

    $("#btnSalvar").click(function () {
        tans = "S";
       
        $("#frmPainelCadastrar").data("bootstrapValidator").resetForm();
        $("#frmPainelCadastrar").submit();
    });

    $("#btnTransmitir").click(function () {
        tans = "T";
        $("#frmPainelCadastrar").data("bootstrapValidator").resetForm();
        $("#frmPainelCadastrar").submit();
    });
});


function Salvar() {
    if (navigator.onLine != true) {
        AbrirModal("Erro de conexão");
        return false;
    }

    createInstanceModel();
    createInstanceModelItem();
    createInstanceNotaItem();

    var modelSalvar = { Model: Entity, Nota: notas};

  
}

function Transmitir() {
    if (navigator.onLine != true) {
        AbrirModal("Erro de conexão");
        return false;
    }

    createInstanceModel();
    createInstanceModelItem();
    createInstanceNotaItem();


    var modelSalvar = { Model: ModelItem };

    Transmissao(JSON.stringify(modelSalvar), "Subempenho");
}


function createInstanceModel() {
        
    Entity.Contrato = $("#Contrato").val().replace(/[\.-]/g, "");
    //Pesquisa Tipo Apropriação:
    Entity.CenarioSiafemSiafisico = $("#CenarioSiafemSiafisico").val();
    Entity.NumeroEmpenhoProdesp  = $("#NumeroEmpenhoProdesp").val();
    Entity.ValorRealizado  = $("#ValorRealizado").val();
    Entity.NumeroEmpenhoProdesp = $("#NumeroEmpenhoProdesp").val();
    Entity.CodigoTarefa  = $("#CodigoTarefa").val();
    Entity.CodigoDespesa = $("#CodigoDespesa").val();
    Entity.NumeroRecibo  = $("#NumeroRecibo").val();
    Entity.PrazoPagamento = $("#PrazoPagamento").val();
    Entity.DataRealizado = $("#DataRealizado").val();
    Entity.NomeCredor = $("#NomeCredor").val();
    //apropriacao
    Entity.NumeroOriginalSiafemSiafisico = $("#NumeroOriginalSiafemSiafisico").val();
    Entity.NumeroCT = $("#NumeroCT").val();
    Entity.CodigoUnidadeGestora = $("#CodigoUnidadeGestora").val();
    Entity.CodigoGestao = $("#CodigoGestao").val();
    Entity.CodigoNotaFiscalProdesp = $("#CodigoNotaFiscalProdesp").val();
    Entity.Valor = $("#Valor").val();
    Entity.NumeroMedicao = $("#NumeroMedicao").val();
    Entity.TipoEventoId = $("#TipoEventoId").val();
    Entity.CodigoEvento = $("#CodigoEvento").val();
    Entity.NaturezaSubempenhoId = $("#NaturezaSubempenhoId").val();
    Entity.DataEmissao = $("#DataEmissao").val();
    Entity.AnoMedicao = $("#AnoMedicao").val();
    Entity.AnoMes = $("#AnoMes").val();
    Entity.Percentual = $("#Percentual").val();
    Entity.RegionalId = $("#RegionalId").val();
    Entity.ProgramaId = $("#ProgramaId").val();
    Entity.NaturezaId = $("#NaturezaId").val();
    Entity.FonteId = $("#FonteId").val();
    Entity.CodigoAplicacaoObra = $("#CodigoAplicacaoObra").val();
    Entity.NumeroCNPJCPFCredor = $("#NumeroCNPJCPFCredor").val();
    Entity.CodigoGestaoCredor = $("#CodigoGestaoCredor").val();
    Entity.CodigoCredorOrganizacao = $("#CodigoCredorOrganizacaoId").val();
    Entity.NumeroCNPJCPFFornecedor = $("#NumeroCNPJCPFFornecedorId").val();
    Entity.TipoObraId = $("#TipoObraId").val();
    Entity.CodigoUnidadeGestoraObra = $("#CodigoUnidadeGestoraObra").val();
    //observacao
    Entity.DescricaoObservacao1 = $('#DescricaoObservacao1').val();
    Entity.DescricaoObservacao2 = $('#DescricaoObservacao2').val();
    Entity.DescricaoObservacao3 = $('#DescricaoObservacao3').val();
    //caucao
    Entity.QuotaGeralAutorizadaPor = $("#QuotaGeralAutorizadaPor").val();
    Entity.NumeroGuia = $("#NumeroGuia").val();
    Entity.ValorCaucionado = $("#ValorCaucionado").val();
    //dado Despesa: 
    Entity.NumeroProcesso = $("#NumeroProcesso").val();
    Entity.DescricaoAutorizadoSupraFolha = $("#DescricaoAutorizadoSupraFolha").val();
    Entity.CodigoEspecificacaoDespesa = $("#CodigoEspecificacaoDespesa").val();
    Entity.NlRetencaoInss = $("#NlRetencaoInss").val();
    Entity.Lista = $("#Lista").val();
    Entity.DescricaoEspecificacaoDespesa1 = $("#DescricaoEspecificacaoDespesa1").val();
    Entity.DescricaoEspecificacaoDespesa2 = $("#DescricaoEspecificacaoDespesa2").val();
    Entity.DescricaoEspecificacaoDespesa3 = $("#DescricaoEspecificacaoDespesa3").val();
    Entity.DescricaoEspecificacaoDespesa4 = $("#DescricaoEspecificacaoDespesa4").val();
    Entity.DescricaoEspecificacaoDespesa5 = $("#DescricaoEspecificacaoDespesa5").val();
    Entity.DescricaoEspecificacaoDespesa6 = $("#DescricaoEspecificacaoDespesa6").val();
    Entity.DescricaoEspecificacaoDespesa7 = $("#DescricaoEspecificacaoDespesa7").val();
    Entity.DescricaoEspecificacaoDespesa8 = $("#DescricaoEspecificacaoDespesa8").val();
    Entity.DescricaoEspecificacaoDespesa9 = $("#DescricaoEspecificacaoDespesa9").val();
    //dado Observação   
    Entity.DescricaoObservacao1 = $("#DescricaoObservacao1").val();
    Entity.DescricaoObservacao2 = $("#DescricaoObservacao2").val();
    Entity.DescricaoObservacao3 = $("#DescricaoObservacao3").val();
    //dado referencia:
    Entity.Referencia = $("#Referencia").val();
    Entity.ReferenciaDigitada = $("#ReferenciaDigitada").val();
    
    Entity.Itens = liquidacaoItem.EntityList;
    Entity.Eventos = liquidacaoEvento.EntityList;
    Entity.Notas = createInstanceNotaItem();
}

function createInstanceNotaItem() {
    
    var notas = [];
    var cont = 0;
    $.each($("div > #Nota"), function (index, value) {
        cont = cont + 1;
        if (value.value !== "") {
            var valorNota = value.value;
            
            var nota = {
                Id: 0,
                CodigoNotaFiscal: valorNota,
                SubempenhoId: Entity.Id,
                Ordem: cont
            };
            notas[notas.length] = nota;
        }
    });

    return notas;
}