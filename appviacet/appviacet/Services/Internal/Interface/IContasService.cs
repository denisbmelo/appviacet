using appviacet.Context.Entitys;

namespace appviacet.Services.Internal.Interface
{
    public interface IContasService 
    {
        Task<List<Conta>> GetContasAsync();
        Task<Conta> GetContaAsync(int id);
        Task<Conta> CreateContaAsync(Conta conta);
        Task<Conta> UpdateContaAsync(int id, Conta conta);
        Task<bool> DeleteContaAsync(int id);
    }
}
