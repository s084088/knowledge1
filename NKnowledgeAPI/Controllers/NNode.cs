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
    [Route("Api/NNode")]
    public class NNode : BaseController
    {
        NKnowledgeDB nKnowledgeDB = new NKnowledgeDB();

        /// <summary>
        /// 获取知识点
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetKnowledgeList")]
        public Package GetKnowledgeList()
        {
            var req = nKnowledgeDB.Set<Node>().ToList();
            return Pack(req);
        }

        /// <summary>
        /// 增加知识点
        /// </summary>
        /// <returns></returns>
        [HttpPost("AddKnowledge")]
        public Package AddKnowledge([FromBody] JObject json)
        {
            if (json == null) ApiError("请输入知识点!");
            if (string.IsNullOrWhiteSpace(json.Value<string>("Name"))) ApiError("请输入知识点名称!");
            string Name = json.GetApiValue<string>("Name");
            int Type = json.GetApiValue<int>("Type");

            Node node = new Node();
            node.Name = Name;
            node.Type = Type;

            nKnowledgeDB.Add(node);
            nKnowledgeDB.SaveChanges();

            return Pack(node);
        }

        /// <summary>
        /// 修改知识点
        /// </summary>
        /// <returns></returns>
        [HttpPost("EditKnowledge")]
        public Package EditKnowledge([FromBody] JObject json)
        {
            if (json == null) ApiError("请输入知识点!");
            int ID = json.GetApiValue<int>("ID", true);
            string Name = json.GetApiValue<string>("Name");
            int Type = json.GetApiValue<int>("Type");

            Node node = nKnowledgeDB.Set<Node>().FirstOrDefault(l => l.ID == ID);
            if (node == null) ApiError("知识点未找到");

            node.Name = Name;
            node.Type = Type;

            nKnowledgeDB.SaveChanges();

            return Pack(node);
        }

        /// <summary>
        /// 删除知识点
        /// </summary>
        /// <returns></returns>
        [HttpPost("DeleteKnowledge")]
        public Package DeleteKnowledge([FromBody] JObject json)
        {
            if (json == null) ApiError("请输入知识点!");
            int ID = json.GetApiValue<int>("ID", true);
            Node node = nKnowledgeDB.Set<Node>().FirstOrDefault(l => l.ID == ID);
            if (node == null) ApiError("知识点未找到");
            List<Line> nKLines = nKnowledgeDB.Set<Line>().Where(l => l.SourceID == ID).ToList();
            List<Line> nKLines1 = nKnowledgeDB.Set<Line>().Where(l => l.TargetID == ID).ToList();

            nKnowledgeDB.RemoveRange(nKLines);
            nKnowledgeDB.RemoveRange(nKLines);
            var v = nKnowledgeDB.Remove(node);

            nKnowledgeDB.SaveChanges();
            return Pack(null);
        }






    }
}
