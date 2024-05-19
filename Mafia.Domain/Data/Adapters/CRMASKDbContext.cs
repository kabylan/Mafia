using Mafia.Application.AutoAudit;
using Mafia.Domain.AutoAudit;
using Mafia.Domain.Entities;
using Mafia.Domain.Entities.Privilegios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using ActionForUser = Mafia.Domain.Entities.Privilegios.ActionForUser;

namespace Mafia.Domain.Data.Adapters
{
    public class MafiaDbContext : IdentityDbContext
    {
        //-->https://trailheadtechnology.com/aspnetcore-multi-tenant-tips-and-tricks/
        private readonly IUserSession _userSession;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private IDbContextTransaction _currentTransaction;

        public MafiaDbContext(DbContextOptions<MafiaDbContext> options, IUserSession userSession, IHttpContextAccessor httpContextAccessor)
          : base(options)
        {
            _userSession = userSession;
            _httpContextAccessor = httpContextAccessor;
        }

        #region Transaction

        public async Task BeginTransactionAsync()
        {
            if (_currentTransaction != null)
                return;

            _currentTransaction = await base.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted).ConfigureAwait(false);
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await SaveChangesAsync().ConfigureAwait(false);

                await _currentTransaction?.CommitAsync();
            }
            catch
            {
                await RollbackTransactionAsync();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public async Task RollbackTransactionAsync()
        {
            try
            {
                await _currentTransaction?.RollbackAsync();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        #endregion Transaction

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ShadowProperties();
            SetGlobalQueryFilters(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        #region Global Query Filters

        private void SetGlobalQueryFilters(ModelBuilder modelBuilder)
        {
            foreach (var tp in modelBuilder.Model.GetEntityTypes())
            {
                var t = tp.ClrType;
                // set global filters
                if (typeof(ISoftDelete).IsAssignableFrom(t))
                {
                    if (typeof(ITenantEntity).IsAssignableFrom(t))
                    {
                        // softdeletable and tenant (note do not filter just ITenant - too much filtering!
                        // just top level classes that have ITenantEntity
                        var method = SetGlobalQueryForSoftDeleteAndTenantMethodInfo.MakeGenericMethod(t);
                        method.Invoke(this, new object[] { modelBuilder });
                    }
                    else
                    {
                        // softdeletable
                        var method = SetGlobalQueryForSoftDeleteMethodInfo.MakeGenericMethod(t);
                        method.Invoke(this, new object[] { modelBuilder });
                    }
                }
            }
        }

        private static readonly MethodInfo SetGlobalQueryForSoftDeleteMethodInfo = typeof(MafiaDbContext).GetMethods(BindingFlags.Public | BindingFlags.Instance)
            .Single(t => t.IsGenericMethod && t.Name == "SetGlobalQueryForSoftDelete");

        private static readonly MethodInfo SetGlobalQueryForSoftDeleteAndTenantMethodInfo = typeof(MafiaDbContext).GetMethods(BindingFlags.Public | BindingFlags.Instance)
            .Single(t => t.IsGenericMethod && t.Name == "SetGlobalQueryForSoftDeleteAndTenant");

        public void SetGlobalQueryForSoftDelete<T>(ModelBuilder builder) where T : class, ISoftDelete
        {
            builder.Entity<T>().HasQueryFilter(item => !EF.Property<bool>(item, "IsDeleted"));
        }

        public void SetGlobalQueryForSoftDeleteAndTenant<T>(ModelBuilder builder) where T : class, ISoftDelete, ITenant
        {
            builder.Entity<T>().HasQueryFilter(
                item => !EF.Property<bool>(item, "IsDeleted") &&
                        (_userSession.DisableTenantFilter || EF.Property<int>(item, "TenantId") == _userSession.TenantId));
        }

        public override int SaveChanges()
        {

            ChangeTracker.SetShadowProperties(_userSession, _httpContextAccessor);
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            string userId = _httpContextAccessor.HttpContext?.User?.Identity.Name;

            ChangeTracker.SetShadowProperties(_userSession, _httpContextAccessor);
            return await base.SaveChangesAsync(true, cancellationToken);
        }

        public void SetEntityState(object entity, EntityState entityState)
        {
            base.Entry(entity).State = entityState;
        }

        #endregion Global Query Filters

        #region DBset

        

        #endregion 

        public DbSet<Country> Countries { get; set; }
        public DbSet<Organisation> Organisations { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Position> Positions { get; set; }

        #region Roles and functions

        public DbSet<ActionForUser> ActionForUsers { get; set; }
        public DbSet<ControllerAndRoles> ControllerAndRoles { get; set; }
        public DbSet<ActionCategoryForUser> ActionCategoryForUsers { get; set; }

        #endregion 

        public DbSet<Log> Logs { get; set; }
        public DbSet<AuditRecord> AuditRecords { get; set; }



    }
}