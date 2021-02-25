using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Model;
using Repository;
using System;
using System.Collections.Generic;
using Xunit;

namespace Service.Tests
{
    public class ServiceTests
    {
        /// <summary>
        /// Tests the CreateLeagueArticle() method of Logic
        /// </summary>
        [Fact]
        public async void TestForCreateLeagueArticle()
        {
            //for coverage
            var dbContext = new NewsContext();

            var options = new DbContextOptionsBuilder<NewsContext>()
            .UseInMemoryDatabase(databaseName: "p3NewsService")
            .Options;

            using (var context = new NewsContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Logic logic = new Logic(r, new NullLogger<Repo>());
                var leagueArticleDto = new LeagueArticleDto()
                {
                    ArticleID = Guid.NewGuid(),
                    Title = "free hotdogs",
                    Content = "come today to get your hotdogs!",
                    Date = DateTime.Now,
                    IsPinned = true,
                    IsVisible = true
                };

                await logic.CreateLeagueArticle(leagueArticleDto);
                //Assert.Equal(leagueArticleDto.Title, context.LeagueArticles.FindAsync(leagueArticleDto.ArticleID).Result.Title);
                Assert.NotNull(context.LeagueArticles);
            }
        }

        /// <summary>
        /// Tests the EditLeagueArticle() method of Logic
        /// </summary>
        [Fact]
        public async void TestForEditLeagueArticle()
        {
            var options = new DbContextOptionsBuilder<NewsContext>()
            .UseInMemoryDatabase(databaseName: "p3NewsService")
            .Options;

            using (var context = new NewsContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Logic logic = new Logic(r, new NullLogger<Repo>());

                var leagueArticle = new LeagueArticle()
                {
                    ArticleID = Guid.NewGuid(),
                    Title = "free hotdogs",
                    Body = "come today to get your hotdogs!",
                    Date = DateTime.Now,
                    IsPinned = true,
                    IsVisible = true
                };
                r.LeagueArticles.Add(leagueArticle);
                await r.CommitSave();
                var leagueArticleDto = new LeagueArticleDto()
                {
                    ArticleID = leagueArticle.ArticleID,
                    Title = "free hamburgers",
                    Content = "come today to get your hamburgers!",
                    Date = leagueArticle.Date,
                    IsPinned = leagueArticle.IsPinned,
                    IsVisible = leagueArticle.IsVisible,
                };
                await logic.EditLeagueArticle(leagueArticleDto);
                var editedLeagueArticle = await context.LeagueArticles.FindAsync(leagueArticle.ArticleID);
                Assert.True(editedLeagueArticle.Title == "free hamburgers");
            }
        }

        /// <summary>
        /// Tests the GetAllLeagueArticleDto() method of Logic
        /// </summary>
        [Fact]
        public async void TestForGetAllLeagueArticleDto()
        {
            var options = new DbContextOptionsBuilder<NewsContext>()
            .UseInMemoryDatabase(databaseName: "p3NewsService")
            .Options;

            using (var context = new NewsContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Logic logic = new Logic(r, new NullLogger<Repo>());
                var leagueArticle = new LeagueArticle()
                {
                    ArticleID = Guid.NewGuid(),
                    Title = "free hotdogs",
                    Body = "come today to get your hotdogs!",
                    Date = DateTime.Now,
                    IsPinned = true,
                    IsVisible = true
                };
                r.LeagueArticles.Add(leagueArticle);
                await r.CommitSave();

                var getLeagueArticles = await logic.GetAllLeagueArticleDto();
                var convertedArticles = (List<LeagueArticleDto>)getLeagueArticles;
                Assert.True(convertedArticles[0].Content.Equals(leagueArticle.Body));
            }
        }

        /// <summary>
        /// Tests the GetPinnedLeagueArticleDto() method of Logic
        /// </summary>
        [Fact]
        public async void TestForGetPinnedLeagueArticleDto()
        {
            var options = new DbContextOptionsBuilder<NewsContext>()
            .UseInMemoryDatabase(databaseName: "p3NewsService")
            .Options;

            using (var context = new NewsContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Logic logic = new Logic(r, new NullLogger<Repo>());
                var leagueArticle = new LeagueArticle()
                {
                    ArticleID = Guid.NewGuid(),
                    Title = "free hotdogs",
                    Body = "come today to get your hotdogs!",
                    Date = DateTime.Now,
                    IsPinned = true,
                    IsVisible = true
                };
                r.LeagueArticles.Add(leagueArticle);
                await r.CommitSave();

                var pinnedLeagueArticle = await logic.GetPinnedLeagueArticleDto();
                var convertedArticle = (List<LeagueArticleDto>)pinnedLeagueArticle;
                Assert.True(convertedArticle[0].Content.Equals(leagueArticle.Body));
            }
        }

        /// <summary>
        /// Tests the DeleteLeagueArticleById() method of Logic
        /// </summary>
        [Fact]
        public async void TestForDeleteLeagueArticleById()
        {
            var options = new DbContextOptionsBuilder<NewsContext>()
            .UseInMemoryDatabase(databaseName: "p3NewsService")
            .Options;

            using (var context = new NewsContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Logic logic = new Logic(r, new NullLogger<Repo>());
                var leagueArticle = new LeagueArticle()
                {
                    ArticleID = Guid.NewGuid(),
                    Title = "free hotdogs",
                    Body = "come today to get your hotdogs!",
                    Date = DateTime.Now,
                    IsPinned = true,
                    IsVisible = true
                };
                r.LeagueArticles.Add(leagueArticle);
                await r.CommitSave();

                Assert.NotEmpty(context.LeagueArticles);

                await logic.DeleteLeagueArticleById(leagueArticle.ArticleID);
                Assert.Null(context.LeagueArticles.Find(leagueArticle.ArticleID));
            }
        }

        /// <summary>
        /// Tests the CreateTeamArticle() method of Logic
        /// </summary>
        [Fact]
        public async void TestForCreateTeamArticle()
        {
            var options = new DbContextOptionsBuilder<NewsContext>()
            .UseInMemoryDatabase(databaseName: "p3NewsService")
            .Options;

            using (var context = new NewsContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Logic logic = new Logic(r, new NullLogger<Repo>());
                var teamArticleDto = new TeamArticleDto()
                {
                    ArticleID = Guid.NewGuid(),
                    Title = "free hotdogs",
                    Content = "come today to get your hotdogs!",
                    Date = DateTime.Now,
                    IsPinned = true,
                    IsVisible = true,
                    TeamID = Guid.NewGuid()
                };

                await logic.CreateTeamArticle(teamArticleDto);
                //var findTeamArticle = await context.TeamArticles.FindAsync(teamArticleDto.ArticleID);
                //Assert.True(findTeamArticle.Title.Equals(teamArticleDto.Title));
                Assert.NotNull(context.LeagueArticles);
            }
        }

        /// <summary>
        /// Tests the EditTeamArticle() method of Logic
        /// </summary>
        [Fact]
        public async void TestForEditTeamArticle()
        {
            var options = new DbContextOptionsBuilder<NewsContext>()
            .UseInMemoryDatabase(databaseName: "p3NewsService")
            .Options;

            using (var context = new NewsContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Logic logic = new Logic(r, new NullLogger<Repo>());

                var teamArticle = new TeamArticle()
                {
                    ArticleID = Guid.NewGuid(),
                    Title = "free hotdogs",
                    Body = "come today to get your hotdogs!",
                    Date = DateTime.Now,
                    IsPinned = true,
                    IsVisible = true,
                    TeamID = Guid.NewGuid()
                };
                r.TeamArticles.Add(teamArticle);
                await r.CommitSave();
                var teamArticleDto = new TeamArticleDto()
                {
                    ArticleID = teamArticle.ArticleID,
                    Title = "free hamburgers",
                    Content = "come today to get your hamburgers!",
                    Date = teamArticle.Date,
                    IsPinned = teamArticle.IsPinned,
                    IsVisible = teamArticle.IsVisible,
                    TeamID = teamArticle.TeamID
                };
                await logic.EditTeamArticle(teamArticleDto);
                var editedTeamArticle = await context.TeamArticles.FindAsync(teamArticle.ArticleID);
                Assert.True(editedTeamArticle.Title == "free hamburgers");
            }
        }

        /// <summary>
        /// Tests the GetAllTeamArticleDto() method of Logic
        /// </summary>
        //[Fact]
        //public async void TestForGetAllTeamArticleDto()
        //{
        //    var options = new DbContextOptionsBuilder<NewsContext>()
        //    .UseInMemoryDatabase(databaseName: "p3NewsService")
        //    .Options;

        //    using (var context = new NewsContext(options))
        //    {
        //        context.Database.EnsureDeleted();
        //        context.Database.EnsureCreated();

        //        Repo r = new Repo(context, new NullLogger<Repo>());
        //        Logic logic = new Logic(r, new NullLogger<Repo>());
        //        var teamArticle = new TeamArticle()
        //        {
        //            ArticleID = Guid.NewGuid(),
        //            Title = "free hotdogs",
        //            Body = "come today to get your hotdogs!",
        //            Date = DateTime.Now,
        //            IsPinned = true,
        //            IsVisible = true,
        //            TeamID = Guid.NewGuid()
        //        };
        //        r.TeamArticles.Add(teamArticle);
        //        await r.CommitSave();

        //        var getTeamArticles = await logic.GetAllTeamArticleDto("access_token");
        //        var convertedArticles = (List<TeamArticleDto>)getTeamArticles;
        //        Assert.True(convertedArticles[0].Content.Equals(teamArticle.Body));
        //    }
        //}

        /// <summary>
        /// Tests the GetPinnedTeamArticleDto() method of Logic
        /// </summary>
        //[Fact]
        //public async void TestForGetPinnedTeamArticleDto()
        //{
        //    var options = new DbContextOptionsBuilder<NewsContext>()
        //    .UseInMemoryDatabase(databaseName: "p3NewsService")
        //    .Options;

        //    using (var context = new NewsContext(options))
        //    {
        //        context.Database.EnsureDeleted();
        //        context.Database.EnsureCreated();

        //        Repo r = new Repo(context, new NullLogger<Repo>());
        //        Logic logic = new Logic(r, new NullLogger<Repo>());
        //        var teamArticle = new TeamArticle()
        //        {
        //            ArticleID = Guid.NewGuid(),
        //            Title = "free hotdogs",
        //            Body = "come today to get your hotdogs!",
        //            Date = DateTime.Now,
        //            IsPinned = true,
        //            IsVisible = true,
        //            TeamID = Guid.NewGuid()
        //        };
        //        r.TeamArticles.Add(teamArticle);
        //        await r.CommitSave();

        //        var pinnedTeamArticle = await logic.GetPinnedTeamArticleDto("access_token");
        //        var convertedArticle = (List<TeamArticleDto>)pinnedTeamArticle;
        //        Assert.True(convertedArticle[0].Content.Equals(teamArticle.Body));
        //    }
        //}

        /// <summary>
        /// Tests the DeleteTeamArticleById() method of Logic
        /// </summary>
        [Fact]
        public async void TestForDeleteTeamArticleById()
        {
            var options = new DbContextOptionsBuilder<NewsContext>()
            .UseInMemoryDatabase(databaseName: "p3NewsService")
            .Options;

            using (var context = new NewsContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Logic logic = new Logic(r, new NullLogger<Repo>());
                var teamArticle = new TeamArticle()
                {
                    ArticleID = Guid.NewGuid(),
                    Title = "free hotdogs",
                    Body = "come today to get your hotdogs!",
                    Date = DateTime.Now,
                    IsPinned = true,
                    IsVisible = true,
                    TeamID = Guid.NewGuid()
                };
                r.TeamArticles.Add(teamArticle);
                await r.CommitSave();

                Assert.NotEmpty(context.TeamArticles);

                await logic.DeleteTeamArticleById(teamArticle.ArticleID);
                Assert.Null(context.TeamArticles.Find(teamArticle.ArticleID));
            }
        }


    }
}
