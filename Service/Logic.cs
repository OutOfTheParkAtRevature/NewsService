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
            newArticle.IsPinned = leagueArticleDto.IsPinned;
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
            articleToEdit.IsPinned = leagueArticleDto.IsPinned;
            _repo.LeagueArticles.Update(articleToEdit);
            await _repo.CommitSave();
        }
        /// <summary>
        /// returns a list of league articles that are pinned
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<LeagueArticleDto>> GetPinnedLeagueArticleDto()
        {
            List<LeagueArticle> pinnedLeagueArticles = (List<LeagueArticle>)await _repo.GetPinnedLeagueArticles();
            List<LeagueArticleDto> dtos = new List<LeagueArticleDto>();
            foreach (var item in pinnedLeagueArticles)
            {
                LeagueArticleDto newDto = new LeagueArticleDto();
                newDto.ArticleID = item.ArticleID;
                newDto.Title = item.Title;
                newDto.Content = item.Body;
                newDto.Date = item.Date;
                newDto.IsVisible = item.IsVisible;
                newDto.IsPinned = item.IsPinned;
                dtos.Add(newDto);
            }
            return dtos;
        }
        /// <summary>
        /// deletes a league article by the id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteLeagueArticleById(Guid id)
        {
            LeagueArticle articleToDelete = await _repo.GetLeagueArticleById(id);
            _repo.LeagueArticles.Remove(articleToDelete);
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
            newArticle.IsPinned = teamArticleDto.IsPinned;
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

        /// <summary>
        /// returns a list of teat articles that are pinned
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<TeamArticleDto>> GetPinnedTeamArticleDto()
        {
            List<TeamArticle> pinnedTeamArticles = (List<TeamArticle>)await _repo.GetPinnedTeamArticles();
            List<TeamArticleDto> dtos = new List<TeamArticleDto>();
            foreach (var item in pinnedTeamArticles)
            {
                TeamArticleDto newDto = new TeamArticleDto();
                newDto.ArticleID = item.ArticleID;
                newDto.Title = item.Title;
                newDto.Content = item.Body;
                newDto.Date = item.Date;
                newDto.TeamID = item.TeamID;
                newDto.IsVisible = item.IsVisible;
                newDto.IsPinned = item.IsPinned;
                dtos.Add(newDto);
            }
            return dtos;
        }

        /// <summary>
        /// deletes a team article by the id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteTeamArticleById(Guid id)
        {
            TeamArticle articleToDelete = await _repo.GetTeamArticleById(id);
            _repo.TeamArticles.Remove(articleToDelete);
            await _repo.CommitSave();
        }
    }
}
