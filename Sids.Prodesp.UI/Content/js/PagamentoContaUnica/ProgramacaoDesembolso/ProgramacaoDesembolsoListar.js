var obj = "ProgramacaoDesembolso";
var modalDocumentoGerador;
var modalBloqueioPagamento;

$(document).on('ready', function () {

    modalDocumentoGerador = $("#modalDocumentoGerador");
    modalBloqueioPagamento = $("#modalBloqueioPagamento");

    $("#modalDocumentoGerador").remove();
    $("#modalBloqueioPagamento").remove();

    $('#btnReTransmitir').click(function () {
        tans = true;
        ObterId(controller);
    });

    $('#idSelecionar').click(function () {
        MarcaTodos();
    });


    $("#Regional").attr("ReadOnly", usuario.RegionalId != 1);
    $('#Regional > option:not(:selected)').attr('disabled', usuario.RegionalId != 1);

    $('#DocumentoTipoId').change(function () {
        filterHandler();
    });

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

function ExcluirPd(id, editar, tipoId, agrupamentoId, btn) {
    button = btn;
    var unica;
    if (navigator.onLine != true) {
        AbrirModal("Erro de conexão");
        return false;
    }

    urld = '/PagamentoContaUnica/ProgramacaoDesembolso/Delete?tipo=' + tipoId;

    if (tipoId == 2) {
        $.confirm({
            text: "Deseja excluir a programação de desembolso selecionada ou a lista de PD’s Agrupadas?",
            title: "Confirmação",
            confirm: function () {
                ShowLoading();
                unica = "nao";
                ExcluirListaPd(id, editar, agrupamentoId, unica);
            },
            cancel: function () {
                ShowLoading();
                //ModalExclusaoEfetuada("Excluir", "Programação Desembolso");
                //HideLoading();
                unica = "sim";
                ExcluirUmaPd(id, editar,agrupamentoId,unica);
            },
            cancelButton: "Excluir esta programação",
            confirmButton: "Excluir Lista de PD’s",
            post: true,
            confirmButtonClass: "btn-danger",
            cancelButtonClass: "btn-default",
            dialogClass: "modal-dialog" // Bootstrap classes for large modal

        });
    } else {
        ShowLoading();
        ModalSistema(true, '#modalConfirmaExclusao', 'Excluir', "Programação Desembolso", agrupamentoId, button);
        HideLoading();
    }

}




function ExcluirUmaPd(id, editar, agrupamentoId,unica) {
    if (navigator.onLine != true) {
        AbrirModal("Erro de conexão");
        return false;
    }

    if (editar) {
        AbrirModal("Não é permitido excluir lista de PD, existem PD’s já transmitidas");
        return false;
    }


    var status;
    $.ajax({
        type: "Post",
        url: urld,
        cache: false,
        async: false,
        data: JSON.stringify({ Id: id ,agrupamentoId : agrupamentoId , unica: unica}),
        contentType: "application/json; charset=utf-8",
        success: function (dados) {
            if (dados == "Sucesso") {
                HideLoading();
                $.confirm({
                    text: "Programação de Desembolso excluída com sucesso",
                    title: "Confirmação",
                    cancel: function () {
                        ShowLoading();
                        $("#form_filtro").submit();
                    },
                    cancelButton: "Ok",
                    confirmButton: "",
                    post: true,
                    cancelButtonClass: "btn-default",
                    modalOptionsBackdrop: true
                });
            } else {
                HideLoading();
                AbrirModal(dados);
                status = false;
                return false;

            }
        },
        error: function (dados, exception) {
            ErrorAjax(dados, exception);
        }
    });
    return status;

}



function ExcluirListaPd(id, editar, agrupamentoId,unica) {


    if (navigator.onLine != true) {
        AbrirModal("Erro de conexão");
        return false;
    }

    if (editar) {
        AbrirModal("Não é permitido excluir lista de PD, existem PD’s já transmitidas");
        return false;
    }

    var status;
    $.ajax({
        type: "Post",
        url: urld,
        cache: false,
        async: false,
        data: JSON.stringify({ Id: id, agrupamentoId: agrupamentoId, unica: unica }),
        contentType: "application/json; charset=utf-8",
        success: function (dados) {
            if (dados == "Sucesso") {
                HideLoading();
                $.confirm({
                    text: "Lista de Programação de Desembolso excluída com sucesso",
                    title: "Confirmação",
                    cancel: function () {
                        ShowLoading();
                        $("#form_filtro").submit();
                    },
                    cancelButton: "Ok",
                    confirmButton: "",
                    post: true,
                    cancelButtonClass: "btn-default",
                    modalOptionsBackdrop: true
                });
            } else {
                HideLoading();
                AbrirModal(dados);
                status = false;    
                return false;
  
            }
        },
        error: function (dados, exception) {
            ErrorAjax(dados, exception);
        }
    });
    return status;
}



function CancelarPd(id, tipoId) {
    if (navigator.onLine != true) {
        AbrirModal("Erro de conexão");
        return false;
    }

    $.confirm({
        text: "Se deseja cancelar a Programação de Desembolso, preencha o campo abaixo:" + '<br/><label class="control-label label-form-DER" for="causa">Causa:</label><textarea rows="2" columns="60" maxlength="120" id="causa" class="form-control" height="100%"/>',
        title: "Confirmação",
        confirm: function () {
            TransmitirCancelamento(id, tipoId);
        },
        cancel: function () {
        },
        cancelButton: "Não",
        confirmButton: "Sim",
        post: true,
        confirmButtonClass: "btn-success",
        cancelButtonClass: "btn-danger",
        dialogClass: "modal-dialog", // Bootstrap classes for large modal,
        datakeyboard: true
    });

}

function TransmitirCancelamento(id, tipoId) {

    var programacaoDesembolso = {
        Id: id,
        CausaCancelamento: $("#causa").val(),
        ProgramacaoDesembolsoTipoId: tipoId,
        TransmitirSiafem: true
    }

    $.ajax({
        type: "Post",
        url: '/PagamentoContaUnica/ProgramacaoDesembolso/TransmitirCancelamento',
        cache: false,
        async: false,
        data: JSON.stringify({ programacaoDesembolso: programacaoDesembolso }),
        contentType: "application/json; charset=utf-8",
        success: function (dados) {
            if (dados.Status == "Sucesso") {
                $.confirm({
                    text: "Programação de Desembolso cancelada com sucesso",
                    title: "Confirmação",
                    cancel: function () {
                        ShowLoading();
                        $("#form_filtro").submit();
                    },
                    cancelButton: "Ok",
                    confirmButton: "",
                    post: true,
                    cancelButtonClass: "btn-default",
                    modalOptionsBackdrop: true
                });
            } else {
                AbrirModal(dados.Msg);
                return false;
            }
        },
        error: function (dados, exception) {
            ErrorAjax(dados, exception);
        }
    });
}

function BloqueioPagamento(numDoc, tipoDoc, id, tipo, btn) {
    button = btn;

    var programacaoDesembolso = {
        Id: id,
        ProgramacaoDesembolsoTipoId: tipo,
        DocumentoTipoId: tipoDoc,
        NumeroDocumento: numDoc
    }

    $.ajax({
        type: "Post",
        url: "/PagamentoContaUnica/ProgramacaoDesembolso/ConsultarBloqueioOpApoio",
        cache: false,
        async: false,
        data: JSON.stringify({ programacaoDesembolso: programacaoDesembolso }),
        contentType: "application/json; charset=utf-8",
        beforeSend: function () {
            ShowLoading();
        },
        success: function (dados) {
            if (dados.Status == "Sucesso") {

                modalBloqueioPagamento.remove();
                modalDocumentoGerador.remove();
                $("body").append(modalBloqueioPagamento);
                modalBloqueioPagamento.modal('show');

                AtualizarFormularioById(dados.objModel);
                $("#BloOPId").val(id);
                $("#BloOPTipoId").val(tipo);
                $("#transmitirBloqueio").click(Bloquear);
            } else {
                AbrirModal(dados.Msg);
                return false;
            }
            return false;
        },
        complete: function () {
            HideLoading();
        },
        error: function (dados, exception) {
            ErrorAjax(dados, exception);
        }
    });

}

function Bloquear() {
    if ($("#BloOPTipoBloqueio").val() == "") {
        FecharModal('#modalBloqueioPagamento');
        AbrirModal("Campo Tipo de Bloqueio deve ser selecionado");
        return false;
    }
    var entity = {
        Id: $("#BloOPId").val(),
        DocumentoTipoId: $("#OutTipoDoc").val(),
        NumeroDocumento: $("#OutNumeroDoc").val(),
        TipoBloqueio: $("#BloOPTipoBloqueio").val(),
        ProgramacaoDesembolsoTipoId: $("#BloOPTipoId").val()
    }

    $.ajax({
        type: "Post",
        url: "/PagamentoContaUnica/ProgramacaoDesembolso/TransmitirBoqureioOp",
        cache: false,
        async: false,
        data: JSON.stringify({ programacaoDesembolso: entity }),
        contentType: "application/json; charset=utf-8",
        beforeSend: function () {
            ShowLoading();
        },
        success: function (dados) {
            if (dados.Status == "Sucesso") {
                $.confirm({
                    text: "Operação realizada com sucesso",
                    title: "Confirmação",
                    cancel: function () {
                        ShowLoading();
                        $("#form_filtro").submit();
                    },
                    cancelButton: "Ok",
                    confirmButton: "",
                    post: true,
                    cancelButtonClass: "btn-default",
                    modalOptionsBackdrop: true
                });
            } else {
                AbrirModal(dados.Msg);
                return false;
            }
            return false;
        },
        complete: function () {
            FecharModal("#modalBloqueioPagamento");
            modalBloqueioPagamento.remove();
            HideLoading();
        },
        error: function (dados, exception) {
            ErrorAjax(dados, exception);
        }
    });
}


function DesbloquearPagamento(numDoc, tipoDoc, id, tipo, btn) {
    button = btn;
    var entity = {
        Id: id,
        DocumentoTipoId: tipoDoc,
        NumeroDocumento: numDoc,
        ProgramacaoDesembolsoTipoId: tipo
    }

    $.ajax({
        type: "Post",
        url: "/PagamentoContaUnica/ProgramacaoDesembolso/TransmitirDesbloqueioOp",
        cache: false,
        async: false,
        data: JSON.stringify({ programacaoDesembolso: entity }),
        contentType: "application/json; charset=utf-8",
        beforeSend: function () {
            ShowLoading();
        },
        success: function (dados) {
            if (dados.Status == "Sucesso") {
                $.confirm({
                    text: "Operação realizada com sucesso",
                    title: "Confirmação",
                    cancel: function () {
                        ShowLoading();
                        $("#form_filtro").submit();
                    },
                    cancelButton: "Ok",
                    confirmButton: "",
                    post: true,
                    cancelButtonClass: "btn-default",
                    modalOptionsBackdrop: true
                });
            } else {
                AbrirModal(dados.Msg);
                return false;
            }
            return false;
        },
        complete: function () {
            FecharModal('#modalBloqueioPagamento');
            modalBloqueioPagamento.remove();
            HideLoading();
        },
        error: function (dados, exception) {
            ErrorAjax(dados, exception);
        }
    });
}

function CancelarOpApoio(numDoc, tipoDoc, id, tipo) {

    var entity = {
        Id: id,
        ProgramacaoDesembolsoTipoId: tipo,
        DocumentoTipoId: tipoDoc,
        NumeroDocumento: numDoc
    }

    $.ajax({
        datatype: 'JSON',
        type: 'POST',
        url: "/PagamentoContaUnica/ProgramacaoDesembolso/ConsultarCancelamentoOpApoio",
        data: JSON.stringify({ programacaoDesembolso: entity }),
        contentType: "application/json; charset=utf-8",
        beforeSend: function () {
            waitingDialog.show('Consultando');
        },
        success: function (dados) {
            if (dados.Status === 'Sucesso') {
                modalBloqueioPagamento.remove();
                modalDocumentoGerador.remove();
                $("body").append(modalDocumentoGerador);
                modalDocumentoGerador.modal('show');

                AtualizarFormularioById(dados.objModel);
                $("#CanOPId").val(id);
                $("#CanOPTipoId").val(tipo);
                $("#transmitirCancelamento").click(CancelarOp);

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

function CancelarOp() {

    if ($("#CanOPBloqueio").is(':checked')) 
        if ($("#CanOPTipoBloqueio").val() == "") {
            FecharModal('#modalDocumentoGerador');
            AbrirModal("Campo Tipo de Bloqueio deve ser selecionado");
            return false;
        }

    var entity = {
        Id: $("#CanOPId").val(),
        DocumentoTipoId: $("#outTipoDoc").val(),
        NumeroDocumento: $("#outNumDoc").val(),
        TipoBloqueio: $("#CanOPTipoBloqueio").val(),
        Bloqueio: $("#CanOPBloqueio").is(':checked'), 
        ProgramacaoDesembolsoTipoId: $("#CanOPTipoId").val()
    }

    $.ajax({
        datatype: 'JSON',
        type: 'POST',
        url: "/PagamentoContaUnica/ProgramacaoDesembolso/TransmitirCancelamentoOp",
        data: JSON.stringify({ programacaoDesembolso: entity }),
        contentType: "application/json; charset=utf-8",
        beforeSend: function () {
            waitingDialog.show('Consultando');
        },
        success: function (dados) {
            if (dados.Status === 'Sucesso') {
                $.confirm({
                    text: "Operação realizada com sucesso",
                    title: "Confirmação",
                    cancel: function () {
                        ShowLoading();
                        $("#form_filtro").submit();
                    },
                    cancelButton: "Ok",
                    confirmButton: "",
                    post: true,
                    cancelButtonClass: "btn-default",
                    modalOptionsBackdrop: true
                });
            }
            else {
                AbrirModal(dados.Msg);
            }
        },
        error: function (dados) {
            AbrirModal(dados);
        },
        complete: function () {
            FecharModal("#modalDocumentoGerador");
            modalDocumentoGerador.remove();
            waitingDialog.hide();
        }
    });
}

function ImprimirPd(id, tipo) {

    if (tipo == 1 || tipo == 3) {
        Imprimir(id, "ProgramacaoDesembolso", "#frmExport");
    } else {

        $.confirm({
            text: "Deseja imprimir todas as programações cadastradas por robô?",
            title: "Confirmação",
            confirm: function () {
                ImprimiLista(id, tipo, true);
            },
            cancel: function () {
                ImprimiLista(id, tipo, false);
            },
            cancelButton: "Não",
            confirmButton: "Sim",
            post: true,
            cancelButtonClass: "btn-default",
            modalOptionsBackdrop: true
        });

    }
}

function ImprimiLista(id, tipo, list) {
    waitingDialog.show('Gerando impressão');
    $.ajax({
        datatype: 'json',
        type: 'Post',
        url: "/PagamentoContaUnica/ProgramacaoDesembolso/ImprimirLista",
        cache: false,
        async: true,
        data: JSON.stringify({ id: id, tipo: tipo, list: list }),
        contentType: "application/json;",
        success: function (dados) {
            if (dados.Status == "Sucesso") {
                //window.location = "/" + area + "/" + controller + "/DownloadFile";
                $("#frmExport").submit();
                waitingDialog.hide();
            } else {
                waitingDialog.hide();
                AbrirModal(dados.Msg);
            }
        }
    });
}