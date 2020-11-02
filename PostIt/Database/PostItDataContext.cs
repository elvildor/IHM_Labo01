using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using PostIt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PostIt.Database
{
    public class PostItDataContextFactory : IDesignTimeDbContextFactory<PostItContext>
    {
        public PostItContext CreateDbContext(string[] args)
        {
            var optionBuilder = new DbContextOptionsBuilder<PostItContext>()
                .UseSqlite("Data Source=postit.db");
            return new PostItContext(optionBuilder.Options);
        }
    }

    public class PostItContext : DbContext
    {
        private static Type _contextType;

        public static PostItContext CreateContext()
        {
            if (_contextType == null)
            {
                Type type = typeof(IDesignTimeDbContextFactory<PostItContext>);
                Type contextType = AppDomain.CurrentDomain
                    .GetAssemblies()
                    .SelectMany(s => s.GetTypes())
                    .FirstOrDefault(p => type.IsAssignableFrom(p));
                if (contextType == null)
                    throw new Exception("Please implement IDesignTimeDbContextFactory<> in application");
                _contextType = contextType;
            }
            return ((IDesignTimeDbContextFactory<PostItContext>)Activator.CreateInstance(_contextType)).CreateDbContext(new[] { "" });
        }

        public static void Migrate()
        {
            using(PostItContext context = CreateContext())
            {
                context.Database.Migrate();
            }
        }

        public static IEnumerable<Category> GetCategories()
        {
            using(PostItContext context = CreateContext())
            {
                return context.Categories.Include(c => c.PostIts).ToListAsync().Result;
            }
        }

        public static void AddCategories(Category category)
        {
            using (PostItContext context = CreateContext())
            {
                context.Categories.Add(category);
                context.SaveChanges();
            }
        }

        public PostItContext(DbContextOptions options)
            : base(options)
        { }

        public DbSet<Model.PostIt> PostIts { get; set; }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Model.PostIt>()
                .HasOne(p => p.Category)
                .WithMany(c => c.PostIts)
                .HasForeignKey(p => p.CategoryId);
        }
    }
}
