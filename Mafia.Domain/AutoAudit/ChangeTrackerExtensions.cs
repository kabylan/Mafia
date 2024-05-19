using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Mafia.Domain.Data.Adapters;
using System.Linq;
using System;

namespace Mafia.Domain.AutoAudit
{
    public static class ChangeTrackerExtensions
    {
        public static void SetShadowProperties(this ChangeTracker changeTracker, IUserSession userSession, IHttpContextAccessor httpContextAccessor)
        {
            changeTracker.DetectChanges();
            MafiaDbContext dbContext = (MafiaDbContext)changeTracker.Context;
            DateTime timestamp = DateTime.Now;

            string userId = httpContextAccessor.HttpContext?.User?.Identity.Name;

            foreach (EntityEntry entry in changeTracker.Entries().ToList())
            {

                //Auditable Entity Model
                if (entry.Entity is IAuditedEntityBase)
                {
                    if (entry.State == EntityState.Added)
                    {
                        entry.Property("CreatedOn").CurrentValue = timestamp;
                    }
                    if (httpContextAccessor.HttpContext != null)
                    {
                        string userName = httpContextAccessor.HttpContext.User.Identity.Name;
                        if (userName != null)
                        {
                            try
                            {
                                var user = dbContext.ApplicationUsers.FirstOrDefault(e => e.UserName == userName);
                                if (user != null)
                                {
                                    if (entry.State == EntityState.Added)
                                    {
                                        entry.Property("CreatedOn").CurrentValue = timestamp;
                                        entry.Property("CreatedById").CurrentValue = user.Id;
                                        entry.Property("ModifiedOn").CurrentValue = timestamp;
                                    }

                                    if (entry.State == EntityState.Deleted || entry.State == EntityState.Modified)
                                    {
                                        entry.Property("ModifiedOn").CurrentValue = timestamp;
                                        entry.Property("ModifiedById").CurrentValue = user.Id;
                                    }
                                }
                            }
                            catch 
                            {
                                var user = dbContext.ApplicationUsers.FirstOrDefault(e => e.UserName == userName);
                                if (user != null)
                                {
                                    if (entry.State == EntityState.Added)
                                    {
                                        entry.Property("CreatedById").CurrentValue = user.Id;
                                        entry.Property("ModifiedOn").CurrentValue = timestamp;
                                    }

                                    if (entry.State == EntityState.Deleted || entry.State == EntityState.Modified)
                                    {
                                        entry.Property("ModifiedOn").CurrentValue = timestamp;
                                        entry.Property("ModifiedById").CurrentValue = user.Id;
                                    }
                                }
                            }
                        }
                    }
                }


                //Add TenantId to Claims so we can store it in the future
                if (entry.Entity is ITenant)
                {
                    entry.Property("TenantId").CurrentValue = userSession.TenantId;
                }

                //Soft Delete Entity Model
                if (entry.State == EntityState.Deleted && entry.Entity is ISoftDelete)
                {
                    entry.State = EntityState.Modified;
                    entry.Property("IsDeleted").CurrentValue = true;
                }


            }

        }

    }
}
