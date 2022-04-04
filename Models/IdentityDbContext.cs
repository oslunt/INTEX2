namespace INTEX2.Models
{
    public class IdentityDbContext<T>
    {
        private DbContextOptions options;

        public IdentityDbContext(DbContextOptions options)
        {
            this.options = options;
        }
    }
}