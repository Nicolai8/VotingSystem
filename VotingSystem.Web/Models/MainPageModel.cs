using System.Collections.Generic;

namespace VotingSystem.Web.Models
{
    public class MainPageModel
    {
        public List<VotingModel> Votings { get; set; }
        public int Total { get; set; }
        public int Page { get; set; }
    }
}