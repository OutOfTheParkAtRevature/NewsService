using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Model;
using Repository;

namespace Service
{
    public class Logic
    {
        private readonly ILogger _logger;
        public Repo _repo;

        public Logic(Repo repo, ILogger<Repo> logger)
        {
            _logger = logger;
            this._repo = repo;
        }
        /// <summary>
        /// creates a leagueaArticle from the incoming leagueArticleDto
        /// </summary>
        /// <param name="leagueArticleDto"></param>
        /// <returns></returns>
        public async Task CreateLeagueArticle(LeagueArticleDto leagueArticleDto)
        {
            LeagueArticle newArticle = new LeagueArticle();
            newArticle.Title = leagueArticleDto.Title;
            newArticle.Body = leagueArticleDto.Content;
            newArticle.Date = leagueArticleDto.Date;
            newArticle.IsVisible = leagueArticleDto.IsVisible;
            _repo.LeagueArticles.Add(newArticle);
            await _repo.CommitSave();
        }
        /// <summary>
        /// edits a leagueArticle with the in coming leagueArticleDto
        /// </summary>
        /// <param name="leagueArticleDto"></param>
        /// <returns></returns>
        public async Task EditLeagueArticle(LeagueArticleDto leagueArticleDto)
        {
            
            LeagueArticle articleToEdit = await _repo.GetLeagueArticleById(leagueArticleDto.ArticleID);
            articleToEdit.Title = leagueArticleDto.Title;
            articleToEdit.Body = leagueArticleDto.Content;
            articleToEdit.Date = leagueArticleDto.Date;
            articleToEdit.IsVisible = leagueArticleDto.IsVisible;
            _repo.LeagueArticles.Update(articleToEdit);
            await _repo.CommitSave();
        }
        /// <summary>
        /// creates a teamArticle from the in coming teamArticleDto
        /// </summary>
        /// <param name="teamArticleDto"></param>
        /// <returns></returns>
        public async Task CreateTeamArticle(TeamArticleDto teamArticleDto)
        {
            TeamArticle newArticle = new TeamArticle();
            newArticle.Title = teamArticleDto.Title;
            newArticle.Body = teamArticleDto.Content;
            newArticle.Date = teamArticleDto.Date;
            newArticle.TeamID = teamArticleDto.TeamID;
            newArticle.IsVisible = teamArticleDto.IsVisible;
            _repo.TeamArticles.Add(newArticle);
            await _repo.CommitSave();
        }
        /// <summary>
        /// Edits a teamArticle from the in coming teamArticleDto
        /// </summary>
        /// <param name="teamArticleDto"></param>
        /// <returns></returns>
        public async Task EditTeamArticle(TeamArticleDto teamArticleDto)
        {
            TeamArticle articleToEdit = await _repo.GetTeamArticleById(teamArticleDto.ArticleID);
            articleToEdit.Title = teamArticleDto.Title;
            articleToEdit.Body = teamArticleDto.Content;
            articleToEdit.Date = teamArticleDto.Date;
            articleToEdit.TeamID = teamArticleDto.TeamID;
            articleToEdit.IsVisible = teamArticleDto.IsVisible;
            _repo.TeamArticles.Update(articleToEdit);
            await _repo.CommitSave();
        }
    }
}
