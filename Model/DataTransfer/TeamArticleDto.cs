using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DataTransfer;

namespace Model
{
    public class TeamArticleDto
    {
        public Guid ArticleID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public Guid TeamID { get; set; }
        public TeamDto Team { get; set; }
        public bool IsVisible { get; set; }
        public bool IsPinned { get; set; }
    }
}
