(function (window, document, $) {
    'use strict';

    window.programacaoDesembolso = {};

    programacaoDesembolso.init = function () {
        $(".real").maskMoney({ prefix: "R$ ", allowNegative: false, thousands: ".", decimal: ",", affixesStay: true });
        $(".real").maskMoney('mask');

        if (usuario.RegionalId == 1) {
            $("#Orgao option[value='16']").attr("selected", "selected");
            $("#Orgao").removeAttr('disabled');
        } else {
            $("#Orgao option[value='" + usuario.RegionalId + "']").attr("selected", "selected");
            $("#Orgao").attr('disabled', true);
        }


        if (ModelItem.TransmitidoSiafem == true)
            $(".lockSiafem").attr("disabled", true);

        if (ModelItem.TipoBloqueio == 1)
            $(".lock").attr("disabled", true);

        programacaoDesembolso.cacheSelectors();
        programacaoDesembolso.reset();
        programacaoDesembolso.provider();
        programacaoDesembolso.scenaryFactory();


        programacaoDesembolso.filterHandler();

        IniciarCreateEdit(programacaoDesembolso.controller);

    }

    programacaoDesembolso.cacheSelectors = function () {

        programacaoDesembolso.body = $('body');
        programacaoDesembolso.controller = window.controller;
        programacaoDesembolso.controllerProgramacaoDesembolso = 'ProgramacaoDesembolso';

        programacaoDesembolso.CenarioManual = $('.manual');
        programacaoDesembolso.CenarioManualPDBEC = $('.PDBEC');
        programacaoDesembolso.CenarioRobo = $('.robo');

        programacaoDesembolso.NumeroNE = $('#NumeroNE');
        programacaoDesembolso.NumeroCT = $('#NumeroCT');
        

        programacaoDesembolso.valueCenarioProgramacaoDesembolsoTipo = $('#ProgramacaoDesembolsoTipoId');
        programacaoDesembolso.valueProgramacaoDesembolsoDocumentoTipo = $('#DocumentoTipo');

        programacaoDesembolso.displayHandlerChange = function () {

            if (programacaoDesembolso.valueCenarioProgramacaoDesembolsoTipo.val() === "") {
                programacaoDesembolso.reset();
            }

            if (programacaoDesembolso.valueCenarioProgramacaoDesembolsoTipo.val() !== "") {
                programacaoDesembolso.reset();
                programacaoDesembolso.scenaryFactory();
            }

        }

        programacaoDesembolso.displayHandlerNumeroNEBlur = function () {
            //var value = $(this).val();
            var value = $('#NumeroNE').val();

            if (value) {
                $.ajax({
                    type: "POST",
                    url: "/PagamentoContaUnica/" + window.controller + "/ConsultarCtPorNe",
                    data: { "numeroNe": value, "origem": window.controller, "numDoc": $("#NumeroDocumento").val() },
                    dataType: "json",
                    success: function (response) {

                        if (response.Ct !== undefined && response.Ct !== null) {
                            //$('#NumeroNE').val(response.Ct.ContratoEmpenhado);
                            $("table#tbl-items-material tbody tr").remove();

                            if (response.Ct && response.baseItem) {
                                if (response.baseItem !== undefined && response.baseItem !== null) {
                                    var cont = 0;
                                    var item = response.baseItem;

                                    item.forEach(function () {

                                        
                                        linha = '<tr><td>' + item[cont].SequenciaItem + '</td> <td>' + item[cont].CodigoItemServico + '</td> <td>X</td> <td>' + MaskQuantidadeCorretaDoBackEnd(item[cont].QuantidadeMaterialServico) + '</td> <td>' + MaskMonetarioCorretoDoBackEnd(item[cont].ValorTotal) + '</td> </tr>';
                                        //$("table#tbl-items-material tbody").html(linha);
                                        $("table#tbl-items-material tbody").append(linha);
                                        cont = cont + 1;
                                    });

                                }
                                else {
                                    $("table#tbl-items-material tbody tr").remove();
                                    var linha = "<tr> <td colspan='5'> Nenhum item encontrado. </td> </tr>";
                                    $("table#tbl-items-material tbody").html(linha);
                                }
                            }

                        }
                    }
                });
            }
            else
            {
                $("table#tbl-items-material tbody tr").remove();
                var linha = "<tr> <td colspan='5'> Nenhum item encontrado. </td> </tr>";
                $("table#tbl-items-material tbody").html(linha);
            }

        }

        programacaoDesembolso.displayHandlerNumeroCTBlur = function () {
            var value = $(this).val();
            $.ajax({
                type: "POST",
                url: "/PagamentoContaUnica/" + window.controller + "/ConsultarCt",
                data: { "NumCt": value, "origem": 6, "numDoc": $("#NumeroDocumento").val() },
                dataType: "json",
                success: function (response) {
                    if (response.Ct !== undefined && response.Ct !== null) {
                        //$('#NumeroNE').val(response.Ct.ContratoEmpenhado);
                        $("table#tbl-items-material tbody tr").remove();



                        if (response.Ct && response.baseItem) {
                            if (response.baseItem !== undefined && response.baseItemo !== null) {
                             var cont = 0;
                             var item = response.baseItem;

                                item.forEach(function () {

                                    //var item = e;
                                    linha = '<tr><td>' + item[cont].SequenciaItem + '</td> <td>' + item[cont].CodigoItemServico + '</td> <td>X</td> <td>' + item[cont].QuantidadeMaterialServico + '</td> <td>' + item[cont].ValorTotal + '</td> </tr>';
                                    //$("table#tbl-items-material tbody").html(linha);
                                    $("table#tbl-items-material tbody").append(linha);

                                    cont = cont + 1;

                                });

                            }
                            else {
                                var linha = "<tr> <td colspan='5'> Nenhum item encontrado. </td> </tr>";
                                $("table#tbl-items-material tbody").html(linha);
                            }
                        }

                    }

                }
            });
        }

        programacaoDesembolso.filterHandler = function () {

            var field = "";
            var doc = "";
            if (programacaoDesembolso.valueCenarioProgramacaoDesembolsoTipo.val() == 2) {
                doc = "#Documento";
                field = "#DocumentoTipo";
            } else if (programacaoDesembolso.valueCenarioProgramacaoDesembolsoTipo.val() == 1 || programacaoDesembolso.valueCenarioProgramacaoDesembolsoTipo.val() == 3) {
                doc = "#NumeroDocumento";
                field = "#DocumentoTipoId";
            }

            $(doc).removeClass("Subempenho");
            $(doc).removeClass("RapRequisicao");

            if ($(field).val() == 11) {
                $(doc).addClass("RapRequisicao");
            } else if ($(field).val() == 5) {
                $(doc).addClass("Subempenho");
            }
        }
    };

    programacaoDesembolso.reset = function () {

        programacaoDesembolso.CenarioManual.hide();
        programacaoDesembolso.CenarioManualPDBEC.hide();
        programacaoDesembolso.CenarioRobo.hide();
    }

    programacaoDesembolso.scenaryFactory = function () {

        if (programacaoDesembolso.valueCenarioProgramacaoDesembolsoTipo.val() == 3) {
            programacaoDesembolso.CenarioManualPDBEC.show();
            //programacaoDesembolso.limpar();

            $('#GestaoCredor').attr("ReadOnly", true);
            $('#NumeroBancoCredor').attr("ReadOnly", true);
            $('#NumeroAgenciaCredor').attr("ReadOnly", true);
            $('#NumeroContaCredor').attr("ReadOnly", true);
            $('#NumeroCnpjcpfPagto').attr("ReadOnly", true);
            $('#GestaoPagto').attr("ReadOnly", true);
            $('#NumeroBancoPagto').attr("ReadOnly", true);
            $('#NumeroAgenciaPagto').attr("ReadOnly", true);
            $('#NumeroContaPagto').attr("ReadOnly", true);

            if (programacaoDesembolso.NumeroCT.val() !== '') {
                $('#NumeroCT').blur();
            }
            if (programacaoDesembolso.NumeroNE.val() !== '') {
                $('#NumeroNE').blur();
            }
        }

        if (programacaoDesembolso.valueCenarioProgramacaoDesembolsoTipo.val() == 2) {
            programacaoDesembolso.CenarioRobo.show();
        }

        if (programacaoDesembolso.valueCenarioProgramacaoDesembolsoTipo.val() == 1) {
            programacaoDesembolso.CenarioManual.show();
            //programacaoDesembolso.limpar();

            $('#GestaoCredor').attr("ReadOnly", false);
            $('#NumeroBancoCredor').attr("ReadOnly", false);
            $('#NumeroAgenciaCredor').attr("ReadOnly", false);
            $('#NumeroContaCredor').attr("ReadOnly", false);
            $('#NumeroCnpjcpfPagto').attr("ReadOnly", false);
            $('#GestaoPagto').attr("ReadOnly", false);
            $('#NumeroBancoPagto').attr("ReadOnly", false);
            $('#NumeroAgenciaPagto').attr("ReadOnly", false);
            $('#NumeroContaPagto').attr("ReadOnly", false);

        }
    }

    programacaoDesembolso.provider = function () {

        programacaoDesembolso.body
            .on('change', '#ProgramacaoDesembolsoTipoId', programacaoDesembolso.displayHandlerChange)
            .on('change', '.DocumentoTipo', programacaoDesembolso.filterHandler)
            .on('blur', '#NumeroNE', programacaoDesembolso.displayHandlerNumeroNEBlur)
            .on('blur', '#NumeroCT', programacaoDesembolso.displayHandlerNumeroCTBlur);

    }



    $(document).on('ready', programacaoDesembolso.init);

})(window, document, jQuery);