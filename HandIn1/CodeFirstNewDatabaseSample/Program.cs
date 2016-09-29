using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace CodeFirstNewDatabaseSample
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new BloggingContext())
            {
                Console.Write("Enter a name for a new Blog: ");
                var name = Console.ReadLine();

                var blog = new Blog { Name = name };
                db.Blogs.Add(blog);
                db.SaveChanges();

                var query = from b in db.Blogs
                    orderby b.Name
                    select b;

                Console.WriteLine("All blogs in the database:");
                foreach (var item in query)
                {
                    Console.WriteLine(item.Name);
                }

                Console.Write("Enter a name for a new Organization: ");
                var nameOrganization = Console.ReadLine();

                var organization = new Organization {OrganizationName = nameOrganization};
                db.Organizations.Add(organization);
                db.SaveChanges();

                Console.Write("Enter a username for a new User: ");
                var nameUser = Console.ReadLine();
                var user = new User {Username = nameUser, OrganizationId = organization.OrganizationId };
                db.Users.Add(user);
                db.SaveChanges();

                var queryUser = from b in db.Users
                            orderby b.Username
                            select b;

                Console.WriteLine("All users and assoiated organizations in the database:");
                foreach (var item in queryUser)
                {
                    Console.WriteLine(item.Username + "\t" + item.OrganizationId);
                }

                var queryOrganization = from b in db.Organizations
                                orderby b.OrganizationId
                                select b;

                Console.WriteLine("All organizations in the database:");
                foreach (var item in queryOrganization)
                {
                    Console.WriteLine(item.OrganizationId + "\t" + item.OrganizationName);
                }

                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }
    }

    public class Blog
    {
        public int BlogId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public virtual List<Post> Posts { get; set; }
    }

    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { get; set; }
        public virtual Blog Blog { get; set; }
    }

    public class User
    {
        [Key]
        public string Username { get; set; }
        public string DisplayName { get; set; }

        public int OrganizationId { get; set; }
        public virtual Organization Organization { get; set; }
    }

    public class Organization
    {
        public int OrganizationId { get; set; }
        public string OrganizationName { get; set; }

        public virtual List<User> Users { get; set; }
    }

    public class BloggingContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Organization> Organizations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.DisplayName)
                .HasColumnName("display_name");
        }
    }
}
