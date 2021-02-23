using System;

namespace Model
{
    public class LeagueArticleDto
    {
        public Guid ArticleID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public bool IsVisible { get; set; }
        public bool IsPinned { get; set; }


        public LeagueArticleDto() { }
        
    }

    
}
