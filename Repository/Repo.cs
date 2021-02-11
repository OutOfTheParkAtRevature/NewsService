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
        /// <summary>
        /// saves changes to the database
        /// </summary>
        /// <returns></returns>
        public async Task CommitSave()
        {
            await _newsContext.SaveChangesAsync();
        }
        /// <summary>
        /// returns a teamArticle based on the ArticleID parameter
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TeamArticle> GetTeamArticleById(Guid id)
        {
            return await TeamArticles.FindAsync(id);
        }
        /// <summary>
        /// returns all teamArticles
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<TeamArticle>> GetTeamArticles()
        {
            return await TeamArticles.ToListAsync();
        }
        /// <summary>
        /// returns all teamArticles where ispinned is true
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<TeamArticle>> GetPinnedTeamArticles()
        {
            return await TeamArticles.Where(x => x.IsPinned == true).ToListAsync();
        }
        /// <summary>
        /// returns all teamArticles where isvisible is true
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<TeamArticle>> GetVisibleTeamArticles()
        {
            return await TeamArticles.Where(x => x.IsVisible == true).ToListAsync();
        }
        /// <summary>
        /// returns the LeagueArticle by the article id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<LeagueArticle> GetLeagueArticleById(Guid id)
        {
            return await LeagueArticles.FindAsync(id);
        }
        /// <summary>
        /// returns all leagueArticles
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<LeagueArticle>> GetLeagueArticles()
        {
            return await LeagueArticles.ToListAsync();
        }
        /// <summary>
        /// returns all leagueArticles where IsPinned is true
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<LeagueArticle>> GetPinnedLeagueArticles()
        {
            return await LeagueArticles.Where(x => x.IsPinned == true).ToListAsync();
        }
        /// <summary>
        /// returns all leagueArticles where IsVisible is true
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<LeagueArticle>> GetVisibleLeagueArticles()
        {
            return await LeagueArticles.Where(x => x.IsVisible == true).ToListAsync();
        }
    }
}
