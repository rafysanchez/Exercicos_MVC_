$(document).ready(function () {
    $('#tblVisualizarDetalhes').DataTable({
        "processing": true,
        "serverSide": true,
        "orderMulti": false,

        "dom": '<"top"i>rt<"bottom"lp><"clear">',
        "columnDefs": [{
            "targets": 5,
            "data": "download_link",
            "className": "dt-center",
        }],
        "ajax": {
            "url": "/ConfirmacaoPagamento/ModalVisualizarDetalhes",
            "type": "POST",
            "datatype": "json"
        },
        "columns": [
                { "data": "Orgao", "name": "Orgao", "className": "dt-center", "autoWidth": true },
                { "data": "TipoDespesa", "name": "TipoDespesa", "className": "dt-center", "autoWidth": true },
                { "data": "DataVencimento", "name": "DataVencimento", "className": "dt-center", "autoWidth": true },
                { "data": "DataPreparacao", "name": "DataPreparacao", "className": "dt-center", "autoWidth": true },
                { "data": "TipoDocumento", "name": "TipoDocumento", "className": "dt-center", "autoWidth": true },
                { "data": "NumeroDocumento", "name": "NumeroDocumento", "autoWidth": true },
                { "data": "NumeroNLDocumento", "name": "NumeroNLDocumento", "autoWidth": true },
                { "data": "NumeroContrato", "name": "NumeroContrato", "autoWidth": true },
                { "data": "CodigoObra", "name": "CodigoObra", "autoWidth": true },
                { "data": "NumeroOP", "name": "NumeroOP", "autoWidth": true },
                { "data": "BancoPagamento", "name": "BancoPagamento", "autoWidth": true },
                { "data": "AgenciaPagamento", "name": "AgenciaPagamento", "autoWidth": true },
                { "data": "ContaPagamentoSiafem", "name": "ContaPagamentoSiafem", "autoWidth": true },
                { "data": "NumeroEmpenho", "name": "NumeroEmpenho", "autoWidth": true },
                { "data": "NumeroProcesso", "name": "NumeroProcesso", "autoWidth": true },
                { "data": "NotaFiscal", "name": "NotaFiscal", "autoWidth": true },
                { "data": "ValorDocumento", "name": "ValorDocumento", "autoWidth": true },
                { "data": "NaturezaDespesa", "name": "NaturezaDespesa", "autoWidth": true },
                { "data": "CredorOrganizacaoCredorOriginal", "name": "CredorOrganizacaoCredorOriginal", "autoWidth": true },
                { "data": "CPF_CNPJ_CredorOriginal", "name": "CPF_CNPJ_CredorOriginal", "autoWidth": true },
                { "data": "CredorOriginalReferencia", "name": "CredorOriginalReferencia", "autoWidth": true },
                { "data": "CPF_CNPJ", "name": "CPF_CNPJ", "autoWidth": true },
                { "data": "NomeReduzidoCredor", "name": "NomeReduzidoCredor", "autoWidth": true },
                { "data": "BancoFavorecido", "name": "BancoFavorecido", "autoWidth": true },
                { "data": "AgenciaFavorecido", "name": "AgenciaFavorecido", "autoWidth": true },
                { "data": "ContaFavorecido", "name": "ContaFavorecido", "autoWidth": true },
                { "data": "StatusTransmissaoConfirmacao", "name": "StatusTransmissaoConfirmacao", "autoWidth": true },
                { "data": "MensagemErroRetornadaTransmissaoConfirmacaoPagamento", "name": "MensagemErroRetornadaTransmissaoConfirmacaoPagamento", "autoWidth": true },
        ]
    });
    oTable = util.getDataTable('table.tblVisualizarDetalhes', 'body');
})