namespace RoushTech.EntityFrameworkCore
{
    using System.Threading;
    using System.Threading.Tasks;
    using DbContextHooks;
    using Microsoft.EntityFrameworkCore;

    public class ValidatingDbContext : DbContext
    {
        protected  IDbContextValidator DbContextValidator { get; }

        public ValidatingDbContext()
        {
            DbContextValidator = new ContextValidator();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            Validate();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = new CancellationToken())
        {
            Validate();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected void Validate()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                var entity = entry.Entity;
                foreach (var property in entry.Metadata.GetProperties())
                {
                    DbContextValidator.Validate(entity, property);
                }
            }
        }
    }
}