

//$(document).ready(function() {
//    $('#credorTable_filter').hide();
//});
function PesquisarCredor(nome) {


    if (nome == "") {
        waitingDialog.hide();
        AbrirModal("Informe ao menos um campo para filtro!");
        return false;
    }
    

    $.ajax({
        datatype: 'JSON',
        type: 'POST',
        url: "/PagamentoContaUnica/Credor/PesquisarCredor",
        data: JSON.stringify({ nome: nome }),
        contentType: "application/json; charset=utf-8",
        beforeSend: function () {
            waitingDialog.show('Consultando');
        },
        success: function (dados) {
            if (dados.Status === 'Sucesso') {
                AtualizarLista(dados.Credores);
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

function AtualizarLista(credores) {

    var table = util.getDataTable('table._tbDataTables', 'form#frmPainelCadastrar');
    table.clear().draw();

    credores.forEach(function (credor) {

        var objectToCreate = [
            credor.Id,
            credor.CpfcnpjugCredor,
            credor.Prefeitura,
            credor.Conveniado,
            credor.BaseCalculo,
            credor.NomeReduzidoCredor
        ];

        $('#credorTable').dataTable().fnAddData(objectToCreate);

    });


}