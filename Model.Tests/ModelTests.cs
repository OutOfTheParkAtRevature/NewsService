using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;
using Model;

namespace Model.Tests
{
    public class ModelTests
    {
        /// <summary>
        /// Checks the data annotations of Models to make sure they aren't being violated
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private IList<ValidationResult> ValidateModel(object model)
        {
            var result = new List<ValidationResult>();
            var validationContext = new ValidationContext(model);
            Validator.TryValidateObject(model, validationContext, result, true);
            // if (model is IValidatableObject) (model as IValidatableObject).Validate(validationContext);

            return result;
        }

        /// <summary>
        /// Makes sure LeagueArticle Model works with valid data
        /// </summary>
        [Fact]
        public void ValidateLeagueArticle()
        {
            var leagueArticle = new LeagueArticle
            {
                ArticleID = Guid.NewGuid(),
                Title = "goodnews!",
                Body = "wewon!",
                IsPinned = true,
                IsVisible = true
            };

            var results = ValidateModel(leagueArticle);
            Assert.True(results.Count == 0);
        }

        /// <summary>
        /// Makes sure TeamArticle Model works with valid data
        /// </summary>
        [Fact]
        public void ValidateTeamArticle()
        {
            var teamArticle = new TeamArticle
            {
                ArticleID = Guid.NewGuid(),
                TeamID = Guid.NewGuid(),
                Title = "Start of season",
                Body = "Let's win them all!",
                IsVisible = true,
                IsPinned = true
            };

            var results = ValidateModel(teamArticle);
            Assert.True(results.Count == 0);
        }
    }
}
