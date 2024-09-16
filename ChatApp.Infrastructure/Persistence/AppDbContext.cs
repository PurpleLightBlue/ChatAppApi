using ChatApp.Domain.Aggregates;
using ChatApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Infrastructure.Persistence
{

    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<ChatRoom> ChatRooms { get; set; }
        public DbSet<Message> Messages { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Specify the SQLite database file path here
                optionsBuilder.UseSqlite("Data Source=localchat.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasKey(x => x.Username);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany()  // No collection of messages on User, so leave it empty
                .HasForeignKey(m => m.SenderName)  // SenderId is a foreign key
                .OnDelete(DeleteBehavior.Restrict);

            // Configuring the relationship between Message and ChatRoom
            modelBuilder.Entity<Message>()
                .HasOne(m => m.ChatRoom)
                .WithMany(c => c.Messages)  // A ChatRoom can have many messages
                .HasForeignKey(m => m.ChatRoomName)
                .OnDelete(DeleteBehavior.Restrict);// ChatRoomId is a foreign key

            // Configure the primary key for ChatRoom
            modelBuilder.Entity<ChatRoom>()
                .HasKey(c => c.RoomName);


            // Configure the relationship between ChatRoom and Message
            modelBuilder.Entity<ChatRoom>()
                .HasMany(c => c.Messages)
                .WithOne()
                .OnDelete(DeleteBehavior.Restrict);

            // Configure the relationship between ChatRoom and User (Members)
            modelBuilder.Entity<ChatRoom>()
                .HasMany(c => c.Members)
                .WithMany();
        }
    }
}
