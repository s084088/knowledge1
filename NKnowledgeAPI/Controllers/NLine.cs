using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using NKnowledge;
using NKnowledge.Tables;
using NKnowledgeAPI.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NKnowledgeAPI.Controllers
{
    [Route("Api/NLine")]
    public class NLine : BaseController
    {
        private readonly NKnowledgeDB nKnowledgeDB = new();

        [HttpPost("GetLines")]
        public Package GetLines()
        {
            var lines = nKnowledgeDB.Set<Line>().Select(l => new
            {
                l.SourceID,
                l.TargetID,
            }).ToList();
            return Pack(lines);
        }

        [HttpPost("GetNodeLines")]
        public Package GetNodeLines([FromBody] JObject json)
        {
            int ID = json.GetApiValue<int>("ID", true);

            var req1 = nKnowledgeDB.Set<Line>().Where(l => l.SourceID == ID).Select(l => new
            {
                l.ID,
                l.Target.Name,
            }).ToList();

            var req2 = nKnowledgeDB.Set<Line>().Where(l => l.TargetID == ID).Select(l => new
            {
                l.ID,
                l.Source.Name,
            }).ToList();

            req1.AddRange(req2);

            return Pack(req1);
        }

        [HttpPost("AddNodeLine")]
        public Package AddNodeLine([FromBody] JObject json)
        {
            int ID = json.GetApiValue<int>("ID", true);
            int TargetID = json.GetApiValue<int>("TargetID", true);

            if (ID > TargetID)
            {
                int tempID = TargetID;
                TargetID = ID;
                ID = tempID;
            }

            if (!nKnowledgeDB.Set<Line>().Any(l => l.SourceID == ID && l.TargetID == TargetID))
            {
                _ = nKnowledgeDB.Add(new Line
                {
                    SourceID = ID,
                    TargetID = TargetID,
                });
                _ = nKnowledgeDB.SaveChanges();
            }

            return Pack();
        }

        public Package DeleteNodeLine([FromBody] JObject json)
        {
            int ID = json.GetApiValue<int>("ID", true);
            Line line = nKnowledgeDB.Set<Line>().FirstOrDefault(l => l.ID == ID);
            if (line != null)
            {
                _ = nKnowledgeDB.Remove(line);
                _ = nKnowledgeDB.SaveChanges();
            }
            return Pack();
        }
    }
}
