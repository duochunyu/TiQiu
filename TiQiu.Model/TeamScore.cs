using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TiQiu.Model
{
    public class TeamScore
    {
        public int OrderId { get; set; }
        public int TeamAId { get; set; }
        public int? TeamBId { get; set; }
        public int TeamAScore { get; set; }
        public string TeamAName { get; set; }
        public string TeamBName { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string SCORE { get; set; }
        public int? Win { get; set; }
        public int? Lose { get; set; }
        public string TeamAColor { get; set; }
        public string TeamBColor { get; set; }
        public DateTime OrderDate { get; set; }        
    }
}