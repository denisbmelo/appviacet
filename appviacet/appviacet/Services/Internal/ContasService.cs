using appviacet.Context;
using appviacet.Context.Entitys;
using appviacet.Services.Internal.Interface;
using Microsoft.EntityFrameworkCore;
using System;

namespace appviacet.Services.Internal
{
    public class ContasService : IContasService
    {
        private readonly AppViaCetContext _appViaCetContext;

        public ContasService(AppViaCetContext context)
        {
            _appViaCetContext = context;
        }

        public virtual async Task<List<Conta>> GetContasAsync()
        {
            try
            {
                return await _appViaCetContext.Contas.ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public virtual async Task<Conta> GetContaAsync(int id)
        {
            try
            {
                return await _appViaCetContext.Contas.FindAsync(id);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public virtual async Task<Conta> CreateContaAsync(Conta conta)
        {
            try
            {
                _appViaCetContext.Contas.Add(conta);
                await _appViaCetContext.SaveChangesAsync();
                return conta;
            }
            catch (Exception)
            {

                throw;
            }

            
        }

        public virtual async Task<Conta> UpdateContaAsync(int id, Conta conta)
        {
            if (id != conta.Id)
            {
                throw new ArgumentException("Id nao encontrado");
            }
            try
            {
                _appViaCetContext.Entry(conta).State = EntityState.Modified;
                await _appViaCetContext.SaveChangesAsync();
                return conta;
            }
            catch (Exception)
            {

                throw new ArgumentException("Conexao nao encontrada");
            }
          
        }

        public virtual async Task<bool> DeleteContaAsync(int id)
        {
            var conta = await   _appViaCetContext.Contas.FindAsync(id);
            if (conta == null)
            {
                return false;
            }
            try
            {
                _appViaCetContext.Contas.Remove(conta);
                await _appViaCetContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                throw;
            }

           
        }

    }
}
