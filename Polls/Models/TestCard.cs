using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polls.Models
{
    public class TestCard
    {
        public string testID { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public float rating { get; set; }
        public string url { get; set; }
        public List<string> tagNames { get; set; }
        public Author author { get; set; } = null;

        public bool isPublished { get; set; }
        public bool isActive { get; set; }
    }
}
