using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IChan.Data
{
    public class Data
    {
        public int _unspentIdeaId;
        public int UnspentIdeaId
        {
            get { return _unspentIdeaId; }
            set { _unspentIdeaId = value; }
        }

        public HashSet<int> EnableIdea;

        public Data()
        {
            UnspentIdeaId = 0;
            EnableIdea = new HashSet<int>();
        }
    }
}
