using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class NewsContext : DbContext
    {
        public DbSet<TeamArticle> TeamArticles;
        public DbSet<LeagueArticle> LeagueArticles;

        public NewsContext() { }

        public NewsContext(DbContextOptions<NewsContext> options) : base(options) { }
    }
}
