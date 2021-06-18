using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Knowledge.Tables
{
    [Table("KL_Level")]
    public class KLLevel : DbBase
    {
        public int Key { get; set; }

        [MaxLength(2047)]
        public string Name { get; set; }

        public int Value { get; set; }

        public int Final { get; set; }

        public virtual KLLevel Parent { get; set; }
        public string ParentID { get; set; }

        public virtual ICollection<KLLevel> Children { get; set; }


        public KLLevel AddChildren(string name, int value, int final = 0)
        {
            if (Children == null) Children = new List<KLLevel>();
            KLLevel kLLevel = new KLLevel
            {
                Name = name,
                Value = value,
                Final = final,
            };
            Children.Add(kLLevel);
            return kLLevel;
        }
    }
}
