using Microsoft.EntityFrameworkCore;

namespace appviacet.Context
{
    public class AppViaCetContext : DbContext
    {
        public AppViaCetContext(DbContextOptions<DbContext> options) : base (options) 
        {
        }
    }
}
