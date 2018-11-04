
namespace Sids.Prodesp.Model.Interface.Service
{
    public interface ICommon
    {
        string GetAddressByZipCode(string cep);
        string GetCaptcha();
    }
}
