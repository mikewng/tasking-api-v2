using Microsoft.EntityFrameworkCore;
using tasking_api.Main.Models;

namespace tasking_api.Infrastructure.Context
{
    public sealed class AppDbContext : DbContext
    {
        public DbSet<Board> board => Set<Board>();
        public DbSet<BoardTask> task => Set<BoardTask>();
        public DbSet<BoardTaskTag> taskTag => Set<BoardTaskTag>();
        public DbSet<User> user_account => Set<User>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Board configuration
            modelBuilder.Entity<Board>(entity =>
            {
                entity.HasKey(b => b.Id);
                entity.Property(b => b.Id).ValueGeneratedOnAdd();
                entity.Property(b => b.Name).IsRequired().HasMaxLength(200);
                entity.Property(b => b.Description).HasMaxLength(1000);
                entity.Property(b => b.OwnerId).IsRequired();
                
                entity.HasMany(b => b.Tasks)
                      .WithOne()
                      .HasForeignKey(t => t.BoardId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // BoardTask configuration  
            modelBuilder.Entity<BoardTask>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Id).ValueGeneratedOnAdd();
                entity.Property(t => t.Name).IsRequired().HasMaxLength(200);
                entity.Property(t => t.Description).HasMaxLength(1000);
                entity.Property(t => t.BoardId).IsRequired();
                entity.Property(t => t.Deadline);
                entity.Property(t => t.TaskStatus).IsRequired();
                entity.Property(t => t.CreatedAt).IsRequired();
                entity.Property(t => t.UpdatedAt);

                entity.HasMany(t => t.Tags)
                    .WithOne()
                    .HasForeignKey(t => t.TaskParent_Id)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // BoardTaskTag configuration
            modelBuilder.Entity<BoardTaskTag>(entity =>
            {
                entity.HasKey(t => t.IdTask_Tag);
                entity.Property(t => t.IdTask_Tag).ValueGeneratedOnAdd();
                entity.Property(t => t.TaskParent_Id).IsRequired();
                entity.Property(t => t.Tag_Value).IsRequired().HasMaxLength(200);
            });

            // User configuration
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Id).ValueGeneratedOnAdd();
                entity.Property(u => u.Username).IsRequired().HasMaxLength(50);
                entity.Property(u => u.Email).IsRequired().HasMaxLength(100);
                entity.Property(u => u.PasswordHash).IsRequired().HasMaxLength(255);
                entity.Property(u => u.CreatedAt).IsRequired();
                entity.Property(u => u.LastLoginAt);
                
                entity.HasIndex(u => u.Username).IsUnique();
                entity.HasIndex(u => u.Email).IsUnique();
            });

            // Apply any additional configurations from assembly
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }

        // IUnitOfWork pattern
        public new Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) 
            => base.SaveChangesAsync(cancellationToken);
    }
}
