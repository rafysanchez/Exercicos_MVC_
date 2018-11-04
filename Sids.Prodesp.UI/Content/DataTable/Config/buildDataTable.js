// ********* Arquivo .js para configuração e atribuição do Plugin DataTables. *********
/** Como utilizar:
     Basicamente para utilizar é necessário que a Table(<table>) tenha a classe "_tbDataTables". Essa table tem que ter a separação de <thead> <tbody>!
     ** Forma de atribuir o Plugin:
      * Básica: 
          Não adicionando nenhum dos atribudos para sua table, o função (BuildDataTables.Load()) vai atribuir o plugin depois que a table já foi preechida.
            Ex.: 
            <table class="_tbDataTables">
                <thead>
                    <tr>
                        <th>IdCliente</th>
                        <th>Cliente</th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
      * Carregamento Ajax somente no Inicio:
          Adicionando na sua tabela (<table>) o atributo "ajaxsource" (WebService do Ajax) com o caminho do WebService, 
          o atributo "destroy" tem que ser "true" e colocar o atribulo processing (opcional).
          Na tag <thead> nos <th> o atributo "column" precisa ser preenchido com o nome dos atributos 
          do json que será resultado do WebService consumido.
              Ex.: <table class="_tbDataTables" ajaxsource="Aluno/ProcessarAoIniciar" destroy="true" processing="true">
                     <thead>
                        <tr>
                            <th column="idCliente">ID Cliente</th>
                            <th column="cliente">Nome do Cliente</th>
                        </tr>
                     </thead>
                     <tbody></tbody>
                   </table>
      * Processamento 100% Ajax:
          Adicionando na tabela (<table>) o atributo "ajaxsource" (WebService do Ajax) com o caminho do WebService, o atributo "serverside" tem que ser 
          colocodo como "true". Na tag <thead> nos <th> o atributo "column" precisa ser preenchido com o nome dos atributos do json que será resultado do 
          WebService consumido.
              Ex.: 
                <table class="_tbDataTables" ajaxsource="Aluno/ProcessarDataTables">
                    <thead>
                    <tr>
                        <th column="IdCliente">ID Cliente</th>
                        <th column="Cliente">Nome do Cliente</th>
                    </tr>
                    </thead>
                    <tbody></tbody>
                </table>
     ** Atributos:
       Como descrito acima, para utilizar esse .js é necessário colocar atributos na tabela (<table>), tentei pegar as principais features do DataTables 
       Jquery na função de carregar (BuildDataTables.Load()), mas nada impede de implementar mais no decorrer de novas necessidade!
       Os atributos que a função deste JS vai procura na tabela <table>:
          - ajaxSource: WebService do Ajax
          - serverSide: Processamento no servidor? (boolean)
          - jqueryUI: Utiliza o estilo (style) do JqueryUI? (boolean)
          - filter: Se vai ter o filtro do cabeçalho ou não (boolean)
          - lengthChange: Se vai ter o menu que contém quantidade de linhas por página (boolean)
          - serverMethod: Método do Servidor (Web Servicer) será "GET" ou "POST"
          - destroy: Se vai ser Destruído TUDO ao carregar (boolean)
          - processing: Se irá mostrar o Show Processando (boolean)
    
       Os atributos que a função deste JS vai procura no Head (<thead><tr><th>) da tabela (<table>):
          - column: Nome do atributo que vai vindo no json do WebService consumido.
          - sortable: Se a coluna que possui esse atributo vai ser ordenada ou não (boolean)
          - visible: Se a coluna que possui esse atributo vai ser visível ou não (boolean)
*/

// Carregar o DataTable após carregar tudo
$(document).ready(function () {
    BuildDataTables.Load();
});

var tables = [];

// ************************************************* //
// ** Objecto para armazenar as functions criadas ** //
var BuildDataTables = {
    setEvent: function(evt, val){
        this[evt] = val;
    },
    Events_LenghtChange: null,
    Events_PageChange: null,
    Events_Order : null,
    // Função que Carrega o DataTables em todos as table's que tiver com o classe _tbDataTables
    Load: function () {

        var tablesAll = $('._tbDataTables');
        for (var i = 0; i < tablesAll.length; i++) {

            //$.fn.dataTable.moment('DD/MM/YYYY HH:mm:ss');
            $.fn.dataTable.moment('DD/MM/YYYY');


            var cabecalhos = $(tablesAll[i]).find('thead tr th').length;
            var linhas = $($(tablesAll[i]).find('tbody tr')[0]).find('td').length;

            if ($(tablesAll[i]).find('tr').find('td').html() === "Nenhum registro encontrado." || cabecalhos !== linhas)
                return;


            tables[i] = $(tablesAll[i]).dataTable(BuildDataTables.GetAtributos($(tablesAll[i])));

            $(tables[i]).on("length.dt", function () {

                if (BuildDataTables.Events_LenghtChange != null)
                    setTimeout(function () { BuildDataTables.Events_LenghtChange() }, 250);
            });
            $(tables[i]).on("order.dt", function () {

                if (BuildDataTables.Events_Order != null)
                    setTimeout(function () { BuildDataTables.Events_Order() }, 250);
            });

            $(tables[i]).on('page.dt', function () {

                var name = $(this).attr('id');
                var currentTable = null;

                for (var i = 0; i < tables.length; i++) {
                    if ($(tables[i]).attr('id') == name) {
                        currentTable = i;
                    }
                }
                
                if (currentTable == null) return false;

                var info = tables[currentTable].api().page.info();
                if (BuildDataTables.Events_PageChange != null)
                    setTimeout(function () { BuildDataTables.Events_PageChange(info)  }, 250);
            });
        }
    },
    ById: function (id, options, rebuild) {

        if (rebuild === true) {
            $(id).DataTable().destroy();

        }

        var tables = $(id);
        for (var i = 0; i < tables.length; i++) {

            //$.fn.dataTable.moment('DD/MM/YYYY HH:mm:ss');
            $.fn.dataTable.moment('DD/MM/YYYY');

            var $table = $(tables[i]);
            $table.dataTable(BuildDataTables.GetAtributos($table, options));
        }
    },

    // Pegar todos os atributos da table para montar Json que o plugin (DataTables) utiliza

    GetAtributos: function ($objTable, options) {

        if (obj == "Log") {
            sort = 4;
            order = "desc";
        }

        var atributos = {};
        //**********************************************************************************///
        /* Iniciando os atributos (Default) */
        atributos.aoColumns = [];                                           // Colunas
        atributos.sAjaxSource = null;                                       // WebService do Ajax
        atributos.bServerSide = false;                                      // Processamento no servidor? (boolean)
        atributos.sPaginationType = "full_numbers";                         // Tipo de Paginação
        atributos.aLengthRows = 10;                                         // Quantidade de Linhas ao carregar
        atributos.aLengthMenu = [[05, 10, 15, -1], [05, 10, 15, "Todos"]];  // Menu para mostrar quantidade de registros por página para o usuário
        atributos.oLanguage = BuildDataTables.GetInternationalisation();    // Tradução para pt-BR
        atributos.jQueryUI = false;                                         // Utiliza o estilo (style) do JqueryUI? (boolean)
        atributos.bFilter = true;                                           // Se vai ter o filtro do cabeçalho ou não (boolean)
        atributos.bLengthChange = true;                                     // Se vai ter o menu que contém quantidade de linhas por página  (boolean)
        atributos.sServerMethod = BuildDataTables.enumMethodServer.get;     // Método do Servidor (Web Servicer) será GET ou POST
        atributos.bDestroy = false;                                         // Se vai ser Destruído TUDO ao carregar (boolean)
        atributos.bProcessing = true;                                      // Se irá mostrar o Show Processando (boolean)
        atributos.aaSorting = [[sort, order]];                              //Orderanação inicial da tabela
        atributos.responsive = true;

        //*********************************************************************************///
        /* Pegando os atributos no Objeto Table (jquery) passado pelo parametro de entrada */

        if (options) { $.extend(atributos, options); }

        // Pegando o nome das atributos do Json para cada coluna na ordem colocado no thead ///
        var tableTh = $objTable.find('thead tr th');

        if (tableTh) {

            var jquerTH, dtColumn;
            var tableThLength = tableTh.length;
            for (var i = 0; i < tableThLength; i++) {
                var attrColumn = {};
                jquerTH = $(tableTh[i]);

                attrColumn.bSortable = (jquerTH.attr('sortable') !== 'false');
                attrColumn.bVisible = (jquerTH.attr('visible') !== 'false');
                dtColumn = jquerTH.attr('column');
                if (dtColumn) {
                    attrColumn.mData = dtColumn.toString();
                }

                atributos.aoColumns.push(attrColumn);
            }
        }

        // Pegando o caminho do Ajax 
        var ajaxSource = $objTable.attr('ajaxSource');
        if (ajaxSource)
            atributos.sAjaxSource = ajaxSource.toString();

        // Pegando se o processamento será no servidor
        atributos.bServerSide = $objTable.attr('serverSide') === 'true';

        // Pegando se será utilizado o estilo do JqueryUI
        atributos.jQueryUI = $objTable.attr('jqueryUI') === 'true';

        // Pegando se vai ter o filtro do cabeçalho
        atributos.bFilter = $objTable.attr('filter') !== 'false';

        // Pegando se vai poder alterar a quantidade de linhas por página
        atributos.bLengthChange = $objTable.attr('lengthChange') !== 'false';

        // Tipo do Método no Servidor (POST ou GET)
        var serverMethod = $objTable.attr('serverMethod');
        if (serverMethod)
            atributos.sServerMethod = (serverMethod === BuildDataTables.enumMethodServer.post ? BuildDataTables.enumMethodServer.post :
                BuildDataTables.enumMethodServer.get);

        // Se vai ser Destruído TUDO ao carregar
        atributos.bDestroy = $objTable.attr('destroy') === 'true';

        // Se vai ter o processamento ao carregar
        var processing = $objTable.attr('processing');
        if (processing)
            atributos.bProcessing = (processing === 'true');

        return atributos;
    },

    // Pegar a internalização do DataTables em Português (pt-BR)

    GetInternationalisation: function () {
        // Tradução do DataTables
        if (BuildDataTables.ConfigLanguage === undefined) {
            BuildDataTables.ConfigLanguage = {
                "oAria": {
                    "sSortAscending": ": ativar para classificar coluna ascendente",
                    "sSortDescending": ": ativar para classificar coluna descendente"
                },
                "oPaginate": {
                    "sFirst": "Primeiro",
                    "sLast": "&Uacute;ltimo",
                    "sNext": "Pr&oacute;ximo",
                    "sPrevious": "Anterior"
                },
                "sEmptyTable": "N&atilde;o h&aacute; dados dispon&iacute;veis na tabela",
                "sInfo": "Exibindo de _START_ a _END_ de _TOTAL_ registros",
                "sInfoEmpty": "Exibindo 0 a 0 de 0 registros",
                "sInfoFiltered": "(filtrado a partir de _MAX_ registros)",
                "sInfoPostFix": "",
                "sInfoThousands": "",
                "sDecimal": ",",
                "sLengthMenu": "Mostrar _MENU_",
                "sLoadingRecords": "Carregando...",
                "sProcessing": "Processando...",
                "sSearch": "Pesquisar:",
                "sZeroRecords": "Nenhum registro encontrado com o filtro utilizado",
                /**
                 * All of the language information can be stored in a file on the
                 * server-side, which DataTables will look up if this parameter is passed.
                 * It must store the URL of the language file, which is in a JSON format,
                 * and the object has the same properties as the oLanguage object in the
                 * initialiser object (i.e. the above parameters). Please refer to one of
                 * the example language files to see how this works in action.
                 *  @type string
                 *  @default <i>Empty string - i.e. disabled</i>
                 *  @dtopt Language
                 * 
                 *  @example
                 *    $(document).ready( function() {
                 *      $('#example').dataTable( {
                 *        "oLanguage": {
                 *          "sUrl": "http://www.sprymedia.co.uk/dataTables/lang.txt"
                 *        }
                 *      } );
                 *    } );
                 */
                "sUrl": ""
            };
        }

        return BuildDataTables.ConfigLanguage;
    },

    // Enum do tipo de Método no sevidor

    enumMethodServer: { get: "GET", post: "POST" }
};