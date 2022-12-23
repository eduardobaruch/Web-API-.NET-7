namespace Web_API_.NET_7.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        // Making the Characters created at databse
        public DbSet<Character> Characters => Set<Character>();
        public DbSet<User> Users => Set<User>();
    }
}
