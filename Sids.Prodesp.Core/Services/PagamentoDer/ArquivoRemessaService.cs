using Sids.Prodesp.Infrastructure.DataBase.PagamentoDer;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PagamentoDer;
using Sids.Prodesp.Core.Services.WebService.PagamentoContaDer;
using Sids.Prodesp.Core.Services.WebService.PagamentoContaUnica;
using Sids.Prodesp.Model.Enum;
using Sids.Prodesp.Model.Interface.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using Sids.Prodesp.Model.Entity.Reserva;
using Sids.Prodesp.Core.Services.Reserva;
using Sids.Prodesp.Model.Interface.Reserva;
using Sids.Prodesp.Model.Interface.PagamentoContaDer;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ProgramacaoDesembolsoExecucao;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.AutorizacaoDeOB;
using Sids.Prodesp.Model.Interface.Service.PagamentoContaUnica;
using Sids.Prodesp.Infrastructure.Log;
using Sids.Prodesp.Infrastructure.Services.PagamentoContaUnica;
using Sids.Prodesp.Model.Exceptions;
using Sids.Prodesp.Model.Entity.Seguranca;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PreparacaoPagamento;
using Sids.Prodesp.Infrastructure.Services.PagamentoContaDer;
using Sids.Prodesp.Model.Interface.Service.PagamentoContaDer;

namespace Sids.Prodesp.Core.Services.PagamentoDer
{
    public class ArquivoRemessaService : Base.BaseService
    {
        private readonly ICrudArquivoRemessa _repository;
        private readonly ProdespPagamentoContaUnicaService _prodespContaUnica;
        private readonly ProdespPagamentoContaDerService _prodespContaDer;
        private readonly ChaveCicsmoService _chave;
       
        public ArquivoRemessaService(ILogError log, IChaveCicsmo chave, ICrudArquivoRemessa repository, IProdespPagamentoContaDer prodespContaDer)
            : base(log)
        {
            this._repository = repository;
            this._prodespContaUnica = new ProdespPagamentoContaUnicaService(new LogErrorDal(), new ProdespPagamentoContaUnicaWs());
            this._prodespContaDer = new ProdespPagamentoContaDerService(new LogErrorDal(), new ProdespPagamentoContaDerWs());
            _chave = new ChaveCicsmoService(log, chave);
        }



        public AcaoEfetuada Excluir(ArquivoRemessa entity, int recursoId, short action)
        {
            try
            {
                _repository.Remove(entity.Id);

                if (recursoId > 0) return LogSucesso(action, recursoId, $"Preparação de Arquivo Remessa : Codigo {entity.Id}");

                return AcaoEfetuada.Sucesso;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, actionId: action, functionalityId: recursoId);
            }
        }

        public int SalvarOuAlterar(ArquivoRemessa entity, int recursoId, short action)
        {
            try
            {
                entity.Id = _repository.Save(entity);

                if (recursoId > 0 && entity.StatusProdesp == "S") LogSucesso(action, recursoId, $"Arquivo Preparação Remessa : Codigo {entity.Id}");

                return entity.Id;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, action, recursoId);
            }
        }



        public IEnumerable<ArquivoRemessa> Listar(ArquivoRemessa entity)
        {
            return _repository.Fetch(entity);
        }


        public ArquivoRemessa Selecionar(int id)
        {
            var entity = _repository.Get(id);
            
            return entity;
        }


        public IEnumerable<ArquivoRemessa> BuscarGrid(ArquivoRemessa entity, DateTime de = default(DateTime), DateTime ate = default(DateTime))
        {
            var result = _repository.FetchForGrid(entity, de, ate).ToList();
         
            return result;
        }


        public void Transmitir(int entityId, Usuario user, int recursoId)
        {

            var entity = Selecionar(entityId);
            try
            {
                Transmissao(user, entity, recursoId);
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, (short)EnumAcao.Transmitir, recursoId);
            }
            finally
            {
                SalvarOuAlterar(entity, recursoId, (short)EnumAcao.Transmitir);
            }
        }




        private void Transmissao(Usuario user, ArquivoRemessa entity, int recursoId)
        {
            try
            {
                if (entity.StatusProdesp == "N" || entity.StatusProdesp == "E") TransmitirProdesp(entity, recursoId);
            }
            catch
            {
                throw;
            }
        }










        private void TransmitirProdesp(ArquivoRemessa entity, int recursoId)
        {
            var cicsmo = new ChaveCicsmo();
            try
            {
                cicsmo = _chave.ObterChave(recursoId);
              

                //_prodespContaUnica.Inserir_PreparacaoPagamento(cicsmo.Chave, cicsmo.Senha, ref entity);
               var obj =  _prodespContaDer.PreparacaoArquivoRemessa(entity, cicsmo.Chave, cicsmo.Senha);


               


                entity.StatusProdesp = "S";
                entity.DataTrasmitido = DateTime.Now;
                entity.MensagemServicoProdesp = null;
                entity.DataCadastro = DateTime.Now;
                entity.TransmitidoProdesp = true;

                entity.NumeroConta = string.IsNullOrEmpty(entity.NumeroConta)  ? ((Sids.Prodesp.Infrastructure.ProdespPagamentoContaDer.Procedure_PreparacaoArquiRemessaRecordType)obj).outNumConta : entity.NumeroConta;
                entity.Agencia = string.IsNullOrEmpty(entity.Agencia) ? ((Sids.Prodesp.Infrastructure.ProdespPagamentoContaDer.Procedure_PreparacaoArquiRemessaRecordType)obj).outAgencia.Substring(0, 5) : entity.Agencia;
                entity.Banco = string.IsNullOrEmpty(entity.Banco) ? ((Sids.Prodesp.Infrastructure.ProdespPagamentoContaDer.Procedure_PreparacaoArquiRemessaRecordType)obj).outBanco.Substring(0, 3) : entity.Banco;

                entity.NumeroGeracao = Convert.ToInt32( ((Sids.Prodesp.Infrastructure.ProdespPagamentoContaDer.Procedure_PreparacaoArquiRemessaRecordType)obj).outNumGeracaoArquiv.Replace(",", "").Replace(".", ""));
                entity.QtDeposito = Convert.ToInt32( ((Sids.Prodesp.Infrastructure.ProdespPagamentoContaDer.Procedure_PreparacaoArquiRemessaRecordType)obj).outQtdeDepositos.Replace(",", "").Replace(".", ""));
                entity.QtOpArquivo = Convert.ToInt32(((Sids.Prodesp.Infrastructure.ProdespPagamentoContaDer.Procedure_PreparacaoArquiRemessaRecordType)obj).outQtdeOpArquivo.Replace(",", "").Replace(".", ""));
                entity.QtDocTed = Convert.ToInt32(((Sids.Prodesp.Infrastructure.ProdespPagamentoContaDer.Procedure_PreparacaoArquiRemessaRecordType)obj).outQtdeDocTed.Replace(",", "").Replace(".", ""));
                entity.ValorTotal = string.IsNullOrEmpty(((Sids.Prodesp.Infrastructure.ProdespPagamentoContaDer.Procedure_PreparacaoArquiRemessaRecordType)obj).outValorTotalPagto) ? 0 : Convert.ToInt32(((Sids.Prodesp.Infrastructure.ProdespPagamentoContaDer.Procedure_PreparacaoArquiRemessaRecordType)obj).outValorTotalPagto.Replace(",", "").Replace(".","")) ;
              


            }
            catch (Exception ex)
            {
                entity.StatusProdesp = "E";
                entity.MensagemServicoProdesp = ex.Message;
                throw;
            }
            finally
            {
                _chave.LiberarChave(cicsmo.Codigo);
                SalvarOuAlterar(entity, 0, (short)EnumAcao.Transmitir);
            }
        }



        public string Transmitir(IEnumerable<int> entityIdList, Usuario user, int recursoId)
        {
            var arquivos = new List<ArquivoRemessa>();
            var result = default(string);

            foreach (var arquivoId in entityIdList)
            {
                var entity = new ArquivoRemessa();
                try
                {
                    entity = Selecionar(arquivoId);

                    Retransmissao(user, entity, recursoId);
                    arquivos.Add(entity);
                }
                catch (Exception ex)
                {
                    throw SaveLog(ex, (short?)EnumAcao.Transmitir, recursoId);
                }
                finally
                {
                    SalvarOuAlterar(entity, recursoId, (short)EnumAcao.Transmitir);
                }
            }

            var arquivosErros = arquivos.Where(x => x.StatusProdesp == "E").ToList();

            if (arquivosErros.Count > 0)
                if (arquivos.Count == 1)
                    result += arquivos.FirstOrDefault()?.MensagemServicoProdesp;
                else
                    result += Environment.NewLine + "; / Alguns Arquivos de Remessa não puderam ser retransmitidos";


            return result;
        }



        private Error Retransmissao(Usuario user, ArquivoRemessa entity, int recursoId)
        {
            var error = new Error();
            var cicsmo = new ChaveCicsmo();
            try
            {
                try
                { if (entity.TransmitirProdesp && !entity.TransmitidoProdesp) TransmitirProdesp(entity, recursoId); }
                catch (Exception ex)
                { error.Prodesp = ex.Message; }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                _chave.LiberarChave(cicsmo.Codigo);
            }
            return error;
        }




















        public void TransmitirCancelamentoOp(ArquivoRemessa entity, int recursoId)
        {
            var cicsmo = new ChaveCicsmo();
            
             var   objModel = Selecionar(entity.Id);


            try
            {
                cicsmo = _chave.ObterChave(recursoId);
                objModel.Cancelado = entity.Cancelado;
                _prodespContaDer.CancelamentoArquivoRemessa(objModel, cicsmo.Chave, cicsmo.Senha);
               

                objModel.Cancelado = true;
                //objModel.NumeroGeracao = null;
                //objModel.StatusProdesp = "N";
                //objModel.MensagemServicoProdesp = null;
                //objModel.DataTrasmitido = null;
                //objModel.TransmitidoProdesp = false;
                //objModel.QtOpArquivo = null;
                //objModel.QtDocTed = null;
                //objModel.QtDeposito = null;
                //objModel.ValorTotal = null;

                SalvarOuAlterar(objModel, 0, (short)EnumAcao.Transmitir);

            }
            catch (Exception ex)
            {
                throw SaveLog(ex, (short?)EnumAcao.Transmitir, recursoId);
            }
            finally
            {
                _chave.LiberarChave(cicsmo.Codigo);

                //SalvarOuAlterar(objModel, 0, (short)EnumAcao.Transmitir);

            }
            
        }






        public object SelecionarPreparado(ArquivoRemessa entity, int recursoId)
        {

            var cicsmo = new ChaveCicsmo();

            var objModel = Selecionar(entity.Id);

           

            try
            {
                cicsmo = _chave.ObterChave(recursoId);
                var result = _prodespContaDer.ConsultaArquivoPreparado(objModel, cicsmo.Chave, cicsmo.Senha);



                return result;

            }
            catch (Exception ex)
            {
                throw SaveLog(ex, (short?)EnumAcao.Transmitir, recursoId);
            }
            finally
            {
                _chave.LiberarChave(cicsmo.Codigo);

           

            }

            
        }

























        public object ImprimirProdespOP(ArquivoRemessa entity)
        {
            var cicsmo = new ChaveCicsmo();
            try
            {

                cicsmo = _chave.ObterChave();

                var result = _prodespContaDer.ImpressaoReemissaoRelacaoOD(entity, cicsmo.Chave, cicsmo.Senha);

                return result;

            }
            catch (Exception ex)
            {
                throw SaveLog(ex);
            }
            finally
            {
                _chave.LiberarChave(cicsmo.Codigo);
            }
        }


        //public object ImprimirProdesp(ArquivoRemessa entity)
        //{
        //    var cicsmo = new ChaveCicsmo();
        //    try
        //    {

        //        cicsmo = _chave.ObterChave();

        //        var result = _prodespContaDer.ImpressaoRelacaoOD(entity, cicsmo.Chave, cicsmo.Senha);

        //        return result;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw SaveLog(ex);
        //    }
        //    finally
        //    {
        //        _chave.LiberarChave(cicsmo.Codigo);
        //    }
        //}



        internal class Error
        {
            public string Prodesp { get; set; }
        }





        //public AcaoEfetuada Salvar(ConfirmacaoPagamento entity, int? recursoId)
        //{
        //    try
        //    {
        //        if (entity.Id == 0)
        //        {
        //            var id = _confirmacaoPgtoRepository.Add(entity);
        //            entity.Id = id;
        //        }
        //        else
        //        {
        //            ExcluirItemsNaoListados(entity);
        //            _confirmacaoPgtoRepository.Edit(entity);
        //        }



        //        return AcaoEfetuada.Sucesso;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw SaveLog(ex, 1, recursoId);
        //    }
        //}


        //public ConfirmacaoPagamento Selecionar(int id)
        //{
        //    var filtro = new ConfirmacaoPagamento();
        //    filtro.Id = id;

        //    return Selecionar(filtro);
        //}
        //public ConfirmacaoPagamento Selecionar(ConfirmacaoPagamento filtro)
        //{
        //    var entity = this._confirmacaoPgtoRepository.Fetch(filtro).FirstOrDefault();

        //    if (entity == null)
        //        return null;

        //    var items = this._confirmacaoPgtoItemRepository.Fetch(new ConfirmacaoPagamentoItem() { IdConfirmacaoPagamento = entity.Id });
        //    entity.Items = items;

        //    return entity;
        //}

        //public ConfirmacaoPagamento Selecionar(int? id, int? idAutorizacaoOB)
        //{
        //    var entity = _confirmacaoPgtoRepository.Get(id, idAutorizacaoOB);

        //    if (entity == null)
        //        return null;

        //    var items = _confirmacaoPgtoItemRepository.Fetch(new ConfirmacaoPagamentoItem() { IdConfirmacaoPagamento = entity.Id });
        //    entity.Items = items;

        //    return entity;
        //}

        //public IEnumerable<ConfirmacaoPagamento> Consultar(ConfirmacaoPagamento filtro)
        //{
        //    var entities = this._confirmacaoPgtoRepository.FetchForGrid(filtro);

        //    if (entities == null)
        //        return null;

        //    foreach (var entity in entities)
        //    {
        //        var items = this._confirmacaoPgtoItemRepository.Fetch(new ConfirmacaoPagamentoItem() { IdConfirmacaoPagamento = entity.Id });
        //        entity.Items = items;
        //    }

        //    return entities;
        //}







        //public void TransmitirProdesp(PDExecucao entity, IEnumerable<PDExecucaoItem> entityItem, DateTime? dataConfirmacao, int? tipoPagamento, int recursoId)
        //{
        //    try
        //    {
        //        foreach (var item in entityItem)
        //        {
        //            if (item.cd_transmissao_status_siafem == "S")
        //            {
        //                TransmitirConfirmacaoPagamentoItem(recursoId, entity, item, dataConfirmacao, tipoPagamento);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        entity.StatusProdesp = "E";
        //        entity.MensagemServicoProdesp = ex.Message;
        //    }
        //}





    }
}