using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Model;
using System;
using System.Collections.Generic;
using Xunit;

namespace Repository.Tests
{
    public class RepoTests
    {
        /// <summary>
        /// Tests the CommitSave() method of Repo
        /// </summary>
        [Fact]
        public async void TestForCommitSave()
        {
            var options = new DbContextOptionsBuilder<NewsContext>()
            .UseInMemoryDatabase(databaseName: "p3NewsService")
            .Options;

            using (var context = new NewsContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                var leagueArticle = new LeagueArticle
                {
                    ArticleID = Guid.NewGuid(),
                    Title = "goodnews!",
                    Body = "wewon!",
                    IsPinned = true,
                    IsVisible = true
                };

                r.LeagueArticles.Add(leagueArticle);
                await r.CommitSave();
                Assert.NotEmpty(context.LeagueArticles);
            }
        }

        /// <summary>
        /// Tests the GetLeagueArticles() method of Repo
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
                var leagueArticle = new LeagueArticle
                {
                    ArticleID = Guid.NewGuid(),
                    Title = "goodnews!",
                    Body = "wewon!",
                    IsPinned = true,
                    IsVisible = true
                };

                r.LeagueArticles.Add(leagueArticle);
                await r.CommitSave();
                var leagueArticleList = await r.GetLeagueArticles();
                Assert.NotEmpty(leagueArticleList);
            }
        }

        /// <summary>
        /// Tests the GetLeagueArticleById() method of Repo
        /// </summary>
        [Fact]
        public async void TestForGetLeagueArticleById()
        {
            var options = new DbContextOptionsBuilder<NewsContext>()
            .UseInMemoryDatabase(databaseName: "p3NewsService")
            .Options;

            using (var context = new NewsContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                var leagueArticle = new LeagueArticle
                {
                    ArticleID = Guid.NewGuid(),
                    Title = "goodnews!",
                    Body = "wewon!",
                    IsPinned = true,
                    IsVisible = true
                };

                r.LeagueArticles.Add(leagueArticle);
                await r.CommitSave();
                var leagueArticle2 = await r.GetLeagueArticleById(leagueArticle.ArticleID);
                Assert.True(leagueArticle2.Equals(leagueArticle));
            }
        }

        /// <summary>
        /// Tests the GetPinnedLeagueArticles() method of Repo
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
                var leagueArticle = new LeagueArticle
                {
                    ArticleID = Guid.NewGuid(),
                    Title = "goodnews!",
                    Body = "wewon!",
                    IsPinned = true,
                    IsVisible = true
                };

                r.LeagueArticles.Add(leagueArticle);
                await r.CommitSave();
                var leagueArticle2 = await r.GetPinnedLeagueArticles();
                var convertedList = (List<LeagueArticle>)leagueArticle2;
                Assert.True(convertedList[0].Equals(leagueArticle));
            }
        }

        /// <summary>
        /// Tests the GetVisibleLeagueArticles() method of Repo
        /// </summary>
        [Fact]
        public async void TestForGetVisibleLeagueArticles()
        {
            var options = new DbContextOptionsBuilder<NewsContext>()
            .UseInMemoryDatabase(databaseName: "p3NewsService")
            .Options;

            using (var context = new NewsContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                var leagueArticle = new LeagueArticle
                {
                    ArticleID = Guid.NewGuid(),
                    Title = "goodnews!",
                    Body = "wewon!",
                    IsPinned = true,
                    IsVisible = true
                };

                r.LeagueArticles.Add(leagueArticle);
                await r.CommitSave();
                var leagueArticle2 = await r.GetVisibleLeagueArticles();
                var convertedList = (List<LeagueArticle>)leagueArticle2;
                Assert.True(convertedList[0].Equals(leagueArticle));
            }
        }

        /// <summary>
        /// Tests the GetTeamArticles() method of Repo
        /// </summary>
        [Fact]
        public async void TestForGetTeamArticles()
        {
            var options = new DbContextOptionsBuilder<NewsContext>()
            .UseInMemoryDatabase(databaseName: "p3NewsService")
            .Options;

            using (var context = new NewsContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                var teamArticle = new TeamArticle
                {
                    ArticleID = Guid.NewGuid(),
                    TeamID = Guid.NewGuid(),
                    Title = "Start of season",
                    Body = "Let's win them all!",
                    IsVisible = true,
                    IsPinned = true
                };

                r.TeamArticles.Add(teamArticle);
                await r.CommitSave();
                var teamArticle2 = await r.GetTeamArticles();
                Assert.NotNull(teamArticle2);
            }
        }

        /// <summary>
        /// Tests the GetTeamArticleById() method of Repo
        /// </summary>
        [Fact]
        public async void TestForGetTeamArticleById()
        {
            var options = new DbContextOptionsBuilder<NewsContext>()
            .UseInMemoryDatabase(databaseName: "p3NewsService")
            .Options;

            using (var context = new NewsContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                var teamArticle = new TeamArticle
                {
                    ArticleID = Guid.NewGuid(),
                    TeamID = Guid.NewGuid(),
                    Title = "Start of season",
                    Body = "Let's win them all!",
                    IsVisible = true,
                    IsPinned = true
                };

                r.TeamArticles.Add(teamArticle);
                await r.CommitSave();
                var teamArticle2 = await r.GetTeamArticleById(teamArticle.ArticleID);
                Assert.True(teamArticle2.Equals(teamArticle));
            }
        }

        /// <summary>
        /// Tests the GetPinnedTeamArticles() method of Repo
        /// </summary>
        [Fact]
        public async void TestForGetPinnedTeamArticles()
        {
            var options = new DbContextOptionsBuilder<NewsContext>()
            .UseInMemoryDatabase(databaseName: "p3NewsService")
            .Options;

            using (var context = new NewsContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                var teamArticle = new TeamArticle
                {
                    ArticleID = Guid.NewGuid(),
                    TeamID = Guid.NewGuid(),
                    Title = "Start of season",
                    Body = "Let's win them all!",
                    IsVisible = true,
                    IsPinned = true
                };

                r.TeamArticles.Add(teamArticle);
                await r.CommitSave();
                var teamArticle2 = await r.GetPinnedTeamArticles();
                var convertedList = (List<TeamArticle>)teamArticle2;
                Assert.True(convertedList[0].Equals(teamArticle));
            }
        }

        /// <summary>
        /// Tests the GetVisibleTeamArticles() method of Repo
        /// </summary>
        [Fact]
        public async void TestForGetVisibleTeamArticles()
        {
            var options = new DbContextOptionsBuilder<NewsContext>()
            .UseInMemoryDatabase(databaseName: "p3NewsService")
            .Options;

            using (var context = new NewsContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                var teamArticle = new TeamArticle
                {
                    ArticleID = Guid.NewGuid(),
                    TeamID = Guid.NewGuid(),
                    Title = "Start of season",
                    Body = "Let's win them all!",
                    IsVisible = true,
                    IsPinned = true
                };

                r.TeamArticles.Add(teamArticle);
                await r.CommitSave();
                var teamArticle2 = await r.GetVisibleTeamArticles();
                var convertedList = (List<TeamArticle>)teamArticle2;
                Assert.True(convertedList[0].Equals(teamArticle));
            }
        }
    }
}
