using pdf.Models;
using Microsoft.EntityFrameworkCore;

namespace pdf.Context
{
    public class BaseContext : DbContext
    {
        public BaseContext(DbContextOptions<BaseContext> options) : base(options){

        }
        public DbSet<Stadistics> Stadistics { get; set; }
    }
}