using Microsoft.EntityFrameworkCore;
using Model;

namespace Repository
{
    public class NewsContext : DbContext
    {
        public DbSet<TeamArticle> TeamArticles { get; set; }
        public DbSet<LeagueArticle> LeagueArticles { get; set; }

        public NewsContext() { }

        public NewsContext(DbContextOptions<NewsContext> options) : base(options) { }
    }
}
