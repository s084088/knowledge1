using Knowledge;
using Knowledge.Tables;
using KnowledgeAPI.Comm;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnowledgeAPI.Controllers
{
    [Route("Api/NKnowledge")]
    [ApiExplorerSettings(GroupName = "V2")]
    public class NKnowledge : BaseController
    {
        KnowledgeDB knowledgeDB = new KnowledgeDB();

        /// <summary>
        /// 获取知识点
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("GetKnowledgeList")]
        public Package GetKnowledgeList()
        {
            var req = knowledgeDB.Sets<NKNode>().ToList();
            return Pack(req);
        }

        /// <summary>
        /// 增加知识点
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("AddKnowledge")]
        public Package AddKnowledge([FromBody] JObject json)
        {
            if (json == null) ApiError("请输入知识点!");
            if (string.IsNullOrWhiteSpace(json.Value<string>("Name"))) ApiError("请输入知识点名称!");
            string Name = json.GetApiValue<string>("Name");
            int Type = json.GetApiValue<int>("Type");

            NKNode node = new NKNode();
            node.Name = Name;
            node.Type = Type;

            knowledgeDB.Add(node);
            knowledgeDB.SaveChanges();

            return Pack(node);
        }

        /// <summary>
        /// 修改知识点
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("EditKnowledge")]
        public Package EditKnowledge([FromBody] JObject json)
        {
            if (json == null) ApiError("请输入知识点!");
            string ID = json.GetApiValue<string>("ID", true);
            string Name = json.GetApiValue<string>("Name");
            int Type = json.GetApiValue<int>("Type");

            NKNode node = knowledgeDB.Sets<NKNode>().FirstOrDefault(l => l.ID == ID);
            if (node == null) ApiError("知识点未找到");

            node.Name = Name;
            node.Type = Type;

            knowledgeDB.SaveChanges();

            return Pack(node);
        }

        /// <summary>
        /// 删除知识点
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("DeleteKnowledge")]
        public Package DeleteKnowledge([FromBody] JObject json)
        {
            if (json == null) ApiError("请输入知识点!");
            string ID = json.Value<string>("ID");
            NKNode node = knowledgeDB.Sets<NKNode>().FirstOrDefault(l => l.ID == ID);
            if (node == null) ApiError("知识点未找到");
            List<NKLine> nKLines = knowledgeDB.Sets<NKLine>().Where(l => l.SourceID == ID).ToList();
            List<NKLine> nKLines1 = knowledgeDB.Sets<NKLine>().Where(l => l.TargetID == ID).ToList();

            knowledgeDB.Remove(nKLines);
            knowledgeDB.Remove(nKLines);
            knowledgeDB.Remove(node);

            knowledgeDB.SaveChanges();
            return Pack(null);
        }






    }
}
