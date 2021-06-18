using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NKnowledge.Tables
{
    public class DbBase
    {
        [Key]
        public int ID { get; set; } 
    }
}
