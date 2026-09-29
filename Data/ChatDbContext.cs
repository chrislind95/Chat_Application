using Microsoft.EntityFrameworkCore;

namespace ChatStart.Data;

public class ChatDbContext : DbContext
{
    public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options)
    {

    }

    public DbSet<User> Users => Set<User>();
}