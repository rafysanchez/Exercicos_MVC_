var obj = "SubempenhoCancelamento";
var area = "LiquidacaoDespesa";
var tans = "";
var isLoad;

//var quebras = 0;
//var itens = [];

$(document).ready(function () {
    isLoad = true;
      

    $("#btnSalvar").click(function () {
        tans = "S";
        $("#frmPainelCadastrar").data("bootstrapValidator").resetForm();
        $("#frmPainelCadastrar").submit();
    });
})

function Salvar() {
    if (navigator.onLine != true) {
        AbrirModal("Erro de conexão");
        return false;
    }

    CreateInstanceModel();

    var modelSalvar = { Model: ModelItem };

    $.ajax({
        datatype: "json",
        type: "Post",
        url: obj +"/Save",
        cache: false,
        data: JSON.stringify(modelSalvar),
        contentType: "application/json; charset=utf-8",
        beforeSend: function () {
            waitingDialog.show("Salvando");
        },
        success: function (dados) {
            if (dados.Status == "Sucesso") {
                $("#Id").val(dados.Id);
                $.confirm({
                    text: "Subempenho " + AcaoRealizada(tipoAcao, "SubempenhoCancelamento") + " com sucesso!",
                    title: "Confirmação",
                    cancel: function () {
                        window.location.href = obj + "/Edit/" + dados.Id + "?tipo=a";
                    },
                    cancelButton: "Fechar",
                    confirmButton: false
                });
            } else {
                AbrirModal(dados.Msg);
            }
        },
        error: function (dados) {
            AbrirModal(dados);
        },
        complete: function () {
            $("#frmPainelCadastrar").data("bootstrapValidator").resetForm();
            waitingDialog.hide();
        }
    });
}

function CreateInstanceModel() {
    ModelItem.QuotaGeralAutorizadaPor = $("#quotaId").val();
    ModelItem.NumeroGuia = $("#guiaId").val();
    ModelItem.ValorCaucionado = $("#valcaucionadoId").val();
}
