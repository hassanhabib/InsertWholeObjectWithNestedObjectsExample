using Microsoft.EntityFrameworkCore;

namespace InsertWholeObjectExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var storageBroker = new StorageBroker();
            storageBroker.Students.Add(new Student
            {
                Id = Guid.NewGuid(),
                Name = "Moamen",
                Homeworks = new List<Homework>
                {
                    new Homework
                    {
                        Id = Guid.NewGuid(),
                        Title = "C# Homework"
                    },

                    new Homework
                    {
                        Id = Guid.NewGuid(),
                        Title = "Java Homework"
                    }
                }
            });

            storageBroker.SaveChanges();
        }
    }

    public class Student
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Homework> Homeworks { get; set; }
    }

    public class Homework
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Student Student { get; set; }
    }

    internal class StorageBroker : DbContext
    {
        public StorageBroker() => this.Database.Migrate();

        public DbSet<Student> Students { get; set; }
        public DbSet<Homework> Homeworks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=OtripleSDb;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }
}
