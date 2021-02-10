using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class Repo
    {
        private readonly NewsContext _newsContext;
        private readonly ILogger _logger;
        public DbSet<TeamArticle> TeamArticles;
        public DbSet<LeagueArticle> LeagueArticles;

        public Repo(NewsContext newsContext, ILogger<Repo> logger)
        {
            _newsContext = newsContext;
            _logger = logger;
            this.TeamArticles = _newsContext.TeamArticles;
            this.LeagueArticles = _newsContext.LeagueArticles;
        }

        public async Task CommitSave()
        {
            await _newsContext.SaveChangesAsync();
        }

        public async Task<TeamArticle> GetTeamArticleById(Guid id)
        {
            return await TeamArticles.FindAsync(id);
        }

        public async Task<IEnumerable<TeamArticle>> GetTeamArticles()
        {
            return await TeamArticles.ToListAsync();
        }

        public async Task<IEnumerable<TeamArticle>> GetPinnedTeamArticles()
        {
            return await TeamArticles.Where(x => x.IsPinned == true).ToListAsync();
        }

        public async Task<IEnumerable<TeamArticle>> GetVisibleTeamArticles()
        {
            return await TeamArticles.Where(x => x.IsVisible == true).ToListAsync();
        }

        public async Task<LeagueArticle> GetLeagueArticleById(Guid id)
        {
            return await LeagueArticles.FindAsync(id);
        }

        public async Task<IEnumerable<LeagueArticle>> GetLeagueArticles()
        {
            return await LeagueArticles.ToListAsync();
        }

        public async Task<IEnumerable<LeagueArticle>> GetPinnedLeagueArticles()
        {
            return await LeagueArticles.Where(x => x.IsPinned == true).ToListAsync();
        }

        public async Task<IEnumerable<LeagueArticle>> GetVisibleLeagueArticles()
        {
            return await LeagueArticles.Where(x => x.IsVisible == true).ToListAsync();
        }
    }
}
