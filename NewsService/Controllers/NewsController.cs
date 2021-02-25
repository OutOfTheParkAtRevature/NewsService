using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using Service;

namespace NewsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, League Manager, Head Coach, Assistant Coach, Parent, Player")]
    public class NewsController : ControllerBase
    {
        private readonly Logic _logic;

        public NewsController(Logic logic)
        {
            _logic = logic;
        }

        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<LeagueArticleDto>> GetLeagueArticles()
        {
            return await _logic.GetAllLeagueArticleDto();
        }
        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<LeagueArticleDto>> GetPinnedLeagueArticles()
        {
            return await _logic.GetPinnedLeagueArticleDto();
        }
        [HttpPost]
        [Authorize(Roles ="Admin, League Manager")]
        public async Task CreateLeagueArticle(LeagueArticleDto leagueArticleDto)
        {
            await _logic.CreateLeagueArticle(leagueArticleDto);
        }
        [HttpPut]
        [Authorize(Roles = "Admin, League Manager")]
        public async Task EditLeagueArticle(LeagueArticleDto leagueArticleDto)
        {
            await _logic.EditLeagueArticle(leagueArticleDto);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, League Manager")]
        public async Task DeleteLeagueArticleById(Guid id)
        {
            await _logic.DeleteLeagueArticleById(id);
        }
        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<TeamArticleDto>> GetTeamArticles()
        {
            // add logic to get team information
            var token = await HttpContext.GetTokenAsync("access_token");
            return await _logic.GetAllTeamArticleDto(token);
        }
        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<TeamArticleDto>> GetPinnedTeamArticles()
        {
            // add logic to get team information
            var token = await HttpContext.GetTokenAsync("access_token");
            return await _logic.GetPinnedTeamArticleDto(token);
        }
        [HttpPost]
        [Authorize(Roles = "Admin, League Manager, Head Coach")]
        public async Task CreateTeamArticle(TeamArticleDto teamArticleDto)
        {
            await _logic.CreateTeamArticle(teamArticleDto);
        }
        [HttpPut]
        [Authorize(Roles = "Admin, League Manager, Head Coach")]
        public async Task EditTeamArticle(TeamArticleDto teamArticleDto)
        {
            await _logic.EditTeamArticle(teamArticleDto);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, League Manager, Head Coach")]
        public async Task DeleteTeamArticle(Guid id)
        {
            await _logic.DeleteTeamArticleById(id);
        }

    }
}
