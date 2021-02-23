using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class LeagueArticle
    {
        [Key]
        [DisplayName("Article ID")]
        public Guid ArticleID { get; set; }
        [DisplayName("Title")]
        public string Title { get; set; }
        [DisplayName("Body")]
        public string Body { get; set; }
        [DisplayName("Is Visible")]
        public bool IsVisible { get; set; }
        [DisplayName("Is Pinned")]
        public bool IsPinned { get; set; }
        public DateTime Date { get; set; }
    }
}
