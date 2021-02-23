using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model;
using Service;

namespace NewsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly Logic _logic;

        public NewsController(Logic logic)
        {
            _logic = logic;
        }

        [HttpGet]
        public async Task<IEnumerable<LeagueArticleDto>> GetLeagueArticles()
        {
            return await _logic.GetAllLeagueArticleDto();
        }
        [HttpGet]
        public async Task<IEnumerable<LeagueArticleDto>> GetPinnedLeagueArticles()
        {
            return await _logic.GetPinnedLeagueArticleDto();
        }
        [HttpPost]
        public async Task CreateLeagueArticle(LeagueArticleDto leagueArticleDto)
        {
            await _logic.CreateLeagueArticle(leagueArticleDto);
        }
        [HttpPut]
        public async Task EditLeagueArticle(LeagueArticleDto leagueArticleDto)
        {
            await _logic.EditLeagueArticle(leagueArticleDto);
        }
        [HttpDelete("{id}")]
        public async Task DeleteLeagueArticleById(Guid id)
        {
            await _logic.DeleteLeagueArticleById(id);
        }
        [HttpGet]
        public async Task<IEnumerable<TeamArticleDto>> GetTeamArticles()
        {
            return await _logic.GetAllTeamArticleDto();
        }
        [HttpGet]
        public async Task<IEnumerable<TeamArticleDto>> GetPinnedTeamArticles()
        {
            return await _logic.GetPinnedTeamArticleDto();
        }
        [HttpPost]
        public async Task CreateTeamArticle(TeamArticleDto teamArticleDto)
        {
            await _logic.CreateTeamArticle(teamArticleDto);
        }
        [HttpPut]
        public async Task EditTeamArticle(TeamArticleDto teamArticleDto)
        {
            await _logic.EditTeamArticle(teamArticleDto);
        }
        [HttpDelete("{id}")]
        public async Task DeleteTeamArticle(Guid id)
        {
            await _logic.DeleteTeamArticleById(id);
        }

    }
}
