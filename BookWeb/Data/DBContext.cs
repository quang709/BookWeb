using BookWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace BookWeb.Data
{
    public class DBContext :DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {

        }
        public DbSet<Category> categories { get; set; }
        public object Categories { get; internal set; }
    }
}
