using Address.Mvc.Models;

namespace Address.Mvc
{
    public interface IDadataService
    {
        Task<DadataResponse> CleanAddressAsync(string rawAddress);
    }
}
