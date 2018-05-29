using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IChan.Data
{
    public class Data
    {
        public int UnspentIdeaId { get; set; }
        public HashSet<int> EnableIdea;

        public Data()
        {
            UnspentIdeaId = 0;
            EnableIdea = new HashSet<int>();
        }
    }
}
