using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using PostIt.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
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

        public static void CreatePostIt(string text, Category category, Color color)
        {
            if (category == null)
                throw new Exception("Please add a category");
            using (PostItContext context = CreateContext())
            {
                Model.PostIt postIt = new Model.PostIt()
                {
                    Text = text,
                    Category = category,
                    CategoryId = category.Id,
                    ColorArgb = color.ToArgb()
                };
                var dbCategory = context.Categories.Include(c => c.PostIts).First(c => c.Id == category.Id);
                dbCategory.PostIts.Add(postIt);
                context.SaveChanges();
                postIt.Category = category;
                category.PostIts.Add(postIt);
            }
        }

        public static void ChangePostItCategory(Model.PostIt model, Category category)
        {
            using (PostItContext context = CreateContext())
            {
                var postIt = context.PostIts.FirstOrDefault(p => p.Id == model.Id);
                
                int indexPostit = model.Category.PostIts.IndexOf(model);
                model.Category.PostIts.RemoveAt(indexPostit);
                
                var newCategory = context.Categories.FirstOrDefault(c => c.Id == category.Id);
                model.Category = category;
                model.CategoryId = category.Id;
                postIt.Category = category;
                postIt.CategoryId = category.Id;
                context.PostIts.Update(postIt);
                category.PostIts.Add(model);
                context.SaveChanges();
            }
        }

        public static void UpdatePostIt(Model.PostIt postIt)
        {
            using (PostItContext context = CreateContext())
            {
                context.PostIts.Update(postIt);
                context.SaveChanges();
            }
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
