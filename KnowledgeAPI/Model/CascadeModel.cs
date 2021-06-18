using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnowledgeAPI.Model
{
    public class CascadeModel
    {
        public string ID { get; set; }

        public string Name { get; set; }

        public int Value { get; set; }

        public string FullName { get; set; }

        public IEnumerable<CascadeModel> Children { get; set; }
    }
}
