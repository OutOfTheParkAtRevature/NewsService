using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
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

        [HttpGet("league")]
        public async Task<IEnumerable<LeagueArticleDto>> GetLeagueArticles()
        {
            return await _logic.GetAllLeagueArticleDto();
        }
        [HttpGet("league/pinned")]
        public async Task<IEnumerable<LeagueArticleDto>> GetPinnedLeagueArticles()
        {
            return await _logic.GetPinnedLeagueArticleDto();
        }
        [HttpPost("league")]
        public async Task CreateLeagueArticle(LeagueArticleDto leagueArticleDto)
        {
            await _logic.CreateLeagueArticle(leagueArticleDto);
        }
        [HttpPut("league")]
        public async Task EditLeagueArticle(LeagueArticleDto leagueArticleDto)
        {
            await _logic.EditLeagueArticle(leagueArticleDto);
        }
        [HttpDelete("league/{id}")]
        public async Task DeleteLeagueArticleById(Guid id)
        {
            await _logic.DeleteLeagueArticleById(id);
        }
        [HttpGet("team")]
        public async Task<IEnumerable<TeamArticleDto>> GetTeamArticles()
        {
            // add logic to get team information
            var token = await HttpContext.GetTokenAsync("access_token");
            return await _logic.GetAllTeamArticleDto(token);
        }
        [HttpGet("team/pinned")]
        public async Task<IEnumerable<TeamArticleDto>> GetPinnedTeamArticles()
        {
            // add logic to get team information
            var token = await HttpContext.GetTokenAsync("access_token");
            return await _logic.GetPinnedTeamArticleDto(token);
        }
        [HttpPost("team")]
        public async Task CreateTeamArticle(TeamArticleDto teamArticleDto)
        {
            await _logic.CreateTeamArticle(teamArticleDto);
        }
        [HttpPut("team")]
        public async Task EditTeamArticle(TeamArticleDto teamArticleDto)
        {
            await _logic.EditTeamArticle(teamArticleDto);
        }
        [HttpDelete("team/{id}")]
        public async Task DeleteTeamArticle(Guid id)
        {
            await _logic.DeleteTeamArticleById(id);
        }

    }
}
