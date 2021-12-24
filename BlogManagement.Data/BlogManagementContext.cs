using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BlogManagement.Data.Configuration.EntityConfiguration;
using BlogManagement.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogManagement.Data
{
    /// <summary>
    /// This class when initialized represents a session with the database and
    /// can be used to query and save instances of this app entities.
    /// </summary>
    public class BlogManagementContext : IdentityDbContext<User, IdentityRole<long>, long>
    {
        /// <summary>
        /// Public constructor lets context configuration from AddDbContext pass to the DbContext
        /// </summary>
        /// <param name="options"></param>
        public BlogManagementContext(DbContextOptions<BlogManagementContext> options) : base(options) { }

        public override DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostMeta> PostMetas { get; set; }
        public DbSet<PostComment> PostComments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }

        /// <summary>
        /// Configure the entities using Fluent API
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new PostConfiguration());
            modelBuilder.ApplyConfiguration(new PostMetaConfiguration());
            modelBuilder.ApplyConfiguration(new PostCommentConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryPostConfiguration());
            modelBuilder.ApplyConfiguration(new TagConfiguration());
            modelBuilder.ApplyConfiguration(new PostTagConfiguration());
            modelBuilder.ApplyConfiguration(new IdentityRoleConfiguration());
            modelBuilder.ApplyConfiguration(new IdentityUserRoleConfiguration());
        }

        /*
     * Auto filled the DateCreated and DateModified Dates before saving to the DB.
     */
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var addedEntities =
                base.ChangeTracker
                    .Entries<BaseEntity>()
                    .Where(e =>
                        e.State is EntityState.Added or EntityState.Modified);

            foreach (var addedEntity in addedEntities)
            {
                addedEntity.Entity.UpdatedAt = DateTime.Now;

                if (addedEntity.State is EntityState.Added)
                    addedEntity.Entity.CreatedAt = DateTime.Now;
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
