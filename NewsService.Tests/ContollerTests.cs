using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Model;
using NewsService.Controllers;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using Xunit;

namespace NewsService.Tests
{
    public class ContollerTests
    {
        /// <summary>
        /// Tests the GetLeagueArticles() method of NewsController
        /// </summary>
        [Fact]
        public async void TestForGetLeagueArticles()
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
                NewsController newsController = new NewsController(logic);
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

                var getLeagueArticles = await newsController.GetLeagueArticles();
                var convertedArticles = (List<LeagueArticleDto>)getLeagueArticles;
                Assert.True(convertedArticles[0].Content.Equals(leagueArticle.Body));
            }
        }

        /// <summary>
        /// Tests the GetPinnedLeagueArticles() method of NewsController
        /// </summary>
        [Fact]
        public async void TestForGetPinnedLeagueArticles()
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
                NewsController newsController = new NewsController(logic);
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

                var pinnedLeagueArticle = await newsController.GetPinnedLeagueArticles();
                var convertedArticle = (List<LeagueArticleDto>)pinnedLeagueArticle;
                Assert.True(convertedArticle[0].Content.Equals(leagueArticle.Body));
            }
        }

        /// <summary>
        /// Tests the CreateLeagueArticle() method of NewsController
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
                NewsController newsController = new NewsController(logic);
                var leagueArticleDto = new LeagueArticleDto()
                {
                    ArticleID = Guid.NewGuid(),
                    Title = "free hotdogs",
                    Content = "come today to get your hotdogs!",
                    Date = DateTime.Now,
                    IsPinned = true,
                    IsVisible = true
                };

                await newsController.CreateLeagueArticle(leagueArticleDto);
                Assert.NotNull(context.LeagueArticles);
            }
        }

        /// <summary>
        /// Tests the EditLeagueArticle() method of NewsController
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
                NewsController newsController = new NewsController(logic);

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
                await newsController.EditLeagueArticle(leagueArticleDto);
                var editedLeagueArticle = await context.LeagueArticles.FindAsync(leagueArticle.ArticleID);
                Assert.True(editedLeagueArticle.Title == "free hamburgers");
            }
        }

        /// <summary>
        /// Tests the DeleteLeagueArticleById() method of NewsController
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
                NewsController newsController = new NewsController(logic);
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

                await newsController.DeleteLeagueArticleById(leagueArticle.ArticleID);
                Assert.Null(context.LeagueArticles.Find(leagueArticle.ArticleID));
            }
        }

        /// <summary>
        /// Tests the GetTeamArticles() method of NewsController
        /// </summary>
        //[Fact]
        //public async void TestForGetTeamArticles()
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
        //        NewsController newsController = new NewsController(logic);
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

        //        var getTeamArticles = await newsController.GetTeamArticles();
        //        var convertedArticles = (List<TeamArticleDto>)getTeamArticles;
        //        Assert.True(convertedArticles[0].Content.Equals(teamArticle.Body));
        //    }
        //}

        /// <summary>
        /// Tests the GetPinnedTeamArticles() method of NewsController
        /// </summary>
        [Fact]
        //public async void TestForGetPinnedTeamArticles()
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
        //        NewsController newsController = new NewsController(logic);
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

        //        var pinnedTeamArticle = await newsController.GetPinnedTeamArticles();
        //        var convertedArticle = (List<TeamArticleDto>)pinnedTeamArticle;
        //        Assert.True(convertedArticle[0].Content.Equals(teamArticle.Body));
        //    }
        //}

        /// <summary>
        /// Tests the CreateTeamArticle() method of NewsController
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
                NewsController newsController = new NewsController(logic);
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

                await newsController.CreateTeamArticle(teamArticleDto);
                Assert.NotNull(context.LeagueArticles);
            }
        }

        /// <summary>
        /// Tests the EditTeamArticle() method of NewsController
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
                NewsController newsController = new NewsController(logic);

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
                await newsController.EditTeamArticle(teamArticleDto);
                var editedTeamArticle = await context.TeamArticles.FindAsync(teamArticle.ArticleID);
                Assert.True(editedTeamArticle.Title == "free hamburgers");
            }
        }

        /// <summary>
        /// Tests the DeleteTeamArticle() method of NewsController
        /// </summary>
        [Fact]
        public async void TestForDeleteTeamArticle()
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
                NewsController newsController = new NewsController(logic);
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

                await newsController.DeleteTeamArticle(teamArticle.ArticleID);
                Assert.Null(context.TeamArticles.Find(teamArticle.ArticleID));
            }
        }
    }
}
