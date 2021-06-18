using Knowledge;
using Knowledge.Tables;
using KnowledgeAPI.Comm;
using KnowledgeAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Util;

namespace KnowledgeAPI.Controllers
{
    [Route("Api/Knowledge")]
    [ApiExplorerSettings(GroupName = "V2")]
    public class Knowledge : BaseController
    {
        KnowledgeDB knowledgeDB = new KnowledgeDB();


        /// <summary>
        /// 获取知识点
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("GetKnowledgeList")]
        public Package GetKnowledgeList()
        {
            var req = knowledgeDB.Sets<KLPoint>().Include(l => l.Nodes).ToList();
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
            string Explain = json.GetApiValue<string>("Explain");
            string TypeID = json.GetLastValue<string>("TypeID");
            string ProcessID = json.GetLastValue<string>("ProcessID");
            string LevelID = json.GetLastValue<string>("LevelID");
            KLPoint point = new KLPoint();
            point.Name = Name;
            point.Explain = Explain;
            point.LevelID = LevelID;
            point.ProcessID = ProcessID;
            point.TypeID = TypeID;
            point.Nodes = new List<KLNode>();
            JArray jArray = json.Value<JArray>("Nodes");
            if (jArray != null)
            {
                foreach (JObject jObject in jArray)
                {
                    string TypeID1 = jObject.GetLastValue<string>("TypeID");
                    string ProcessID1 = jObject.GetLastValue<string>("ProcessID");
                    string LevelID1 = jObject.GetLastValue<string>("LevelID");
                    point.Nodes.Add(new KLNode { TypeID = TypeID1, ProcessID = ProcessID1, LevelID = LevelID1 });
                }
            }
            knowledgeDB.Add(point);
            knowledgeDB.SaveChanges();

            return Pack(point);
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
            string Explain = json.GetApiValue<string>("Explain");
            string TypeID = json.GetLastValue<string>("TypeID");
            string ProcessID = json.GetLastValue<string>("ProcessID");
            string LevelID = json.GetLastValue<string>("LevelID");

            KLPoint point = knowledgeDB.Sets<KLPoint>().Include(l => l.Nodes).FirstOrDefault(l => l.ID == ID);
            if (point == null) ApiError("知识点未找到");

            point.Name = Name;
            point.Explain = Explain;
            point.LevelID = LevelID;
            point.ProcessID = ProcessID;
            point.TypeID = TypeID;
            point.Nodes.ForEach(l => knowledgeDB.Remove(l));
            JArray jArray = json.Value<JArray>("Nodes");
            if (jArray != null)
            {
                foreach (JObject jObject in jArray)
                {
                    string TypeID1 = jObject.GetLastValue<string>("TypeID");
                    string ProcessID1 = jObject.GetLastValue<string>("ProcessID");
                    string LevelID1 = jObject.GetLastValue<string>("LevelID");
                    point.Nodes.Add(new KLNode { TypeID = TypeID1, ProcessID = ProcessID1, LevelID = LevelID1 });
                }
            }

            //point1.Nodes.ForEach(node =>
            //{
            //    node.IsNew();
            //    point.Nodes.Add(node);
            //});
            knowledgeDB.SaveChanges();

            return Pack(point);
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
            KLPoint point = knowledgeDB.Sets<KLPoint>().Include(l => l.Nodes).FirstOrDefault(l => l.ID == ID);
            if (point == null) ApiError("知识点未找到");
            point.Nodes.ForEach(l => knowledgeDB.Remove(l));
            knowledgeDB.Remove(point);
            knowledgeDB.SaveChanges();
            return Pack(null);
        }

        /// <summary>
        /// 获取知识类别
        /// </summary>
        /// <returns></returns>
        [HttpPost, HttpGet, Route("GetType")]
        public new Package GetType()
        {
            return Pack(Res.Types);
        }

        /// <summary>
        /// 获取知识类别
        /// </summary>
        /// <returns></returns>
        [HttpPost, HttpGet, Route("GetProcess")]
        public Package GetProcess()
        {
            return Pack(Res.Processes);
        }

        /// <summary>
        /// 获取知识类别
        /// </summary>
        /// <returns></returns>
        [HttpPost, HttpGet, Route("GetGrade")]
        public Package GetGrade()
        {
            return Pack(Res.Grades);
        }

        /// <summary>
        /// 获取图谱
        /// </summary>
        /// <returns></returns>
        [HttpPost, HttpGet, Route("GetGraph")]
        public Package GetGraph()
        {
            List<CascadeModel> types = Res.Types.SelectMany(l => l.Children).ToList();
            List<CascadeModel> process = Res.Processes.SelectMany(l => l.Children).ToList();
            List<CascadeModel> grades = Res.Grades.SelectMany(l => l.Children.SelectMany(k => k.Children.SelectMany(j => j.Children))).ToList();

            return Pack(new { types, process, grades });
        }

        /// <summary>
        /// 获取单个图谱
        /// </summary>
        /// <returns></returns>
        [HttpPost, HttpGet, Route("GetKnowledgeGraph")]
        public Package GetKnowledgeGraph([FromBody] JObject json)
        {
            string ID = json.GetApiValue<string>("ID");
            var nodes = knowledgeDB.Set<KLNode>().Where(l => l.PointID == ID).OrderBy(l => l.Process.Final).ToList();

            var req = nodes.Select(l => new
            {
                Process = Res.Processes.SelectMany(l => l.Children).FirstOrDefault(k => k.ID == l.ProcessID).FullName,
                Grade = Res.Grades.SelectMany(l => l.Children.SelectMany(k => k.Children.SelectMany(j => j.Children))).FirstOrDefault(k => k.ID == l.LevelID).FullName,
            }).ToList();

            var rew = Res.Processes.SelectMany(l => l.Children).Select(l =>
            {
                var levelid = nodes.FirstOrDefault(k => k.ProcessID == l.ID)?.LevelID;
                if (levelid == null) return string.Empty;
                return Res.Grades.SelectMany(l => l.Children.SelectMany(k => k.Children.SelectMany(j => j.Children))).FirstOrDefault(k => k.ID == levelid).FullName;
                ;
            }).ToList();

            return Pack(rew);
        }

        /// <summary>
        /// 设置种子数据
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("SetBaseLevel")]
        public Package SetSeed()
        {
            List<KLLevel> kLLevels = knowledgeDB.Set<KLLevel>().ToList();
            knowledgeDB.RemoveRange(kLLevels);

            int index = 1;
            KLLevel type = new KLLevel { ID = "type" };
            knowledgeDB.Add(type);

            KLLevel type1 = type.AddChildren("事实型知识", 1);
            type1.AddChildren("术语知识", 1, index++);
            type1.AddChildren("具体细节和要素知识", 2, index++);

            type1 = type.AddChildren("事实型知识", 2);
            type1.AddChildren("分类和类别的知识", 1, index++);
            type1.AddChildren("原理和通则的知识", 2, index++);
            type1.AddChildren("理论、模型和结构的知识", 3, index++);

            type1 = type.AddChildren("程序型知识", 3);
            type1.AddChildren("具体学科的技能和算法的知识", 1, index++);
            type1.AddChildren("具体学科的技术和方法的知识", 2, index++);
            type1.AddChildren("确定何时使用适当程序的准则知识", 3, index++);

            type1 = type.AddChildren("元认知知识", 4);
            type1.AddChildren("策略性知识", 1, index++);
            type1.AddChildren("关于认知任务的知识，包括适当的情境性知识和条件行知识", 2, index++);
            type1.AddChildren("关于自我的知识", 3, index++);

            index = 1;
            KLLevel process = new KLLevel { ID = "process" };
            knowledgeDB.Add(process);

            KLLevel process1 = process.AddChildren("记忆/回忆", 1);
            process1.AddChildren("识别", 1, index++);
            process1.AddChildren("回忆", 2, index++);

            process1 = process.AddChildren("理解", 2);
            process1.AddChildren("解释", 1, index++);
            process1.AddChildren("举例", 2, index++);
            process1.AddChildren("分类", 3, index++);
            process1.AddChildren("总结", 4, index++);
            process1.AddChildren("推断", 5, index++);
            process1.AddChildren("比较", 6, index++);
            process1.AddChildren("说明", 7, index++);

            process1 = process.AddChildren("应用", 3);
            process1.AddChildren("执行", 1, index++);
            process1.AddChildren("实施", 2, index++);

            process1 = process.AddChildren("分析", 4);
            process1.AddChildren("区别", 1, index++);
            process1.AddChildren("组织", 2, index++);
            process1.AddChildren("归因", 3, index++);

            process1 = process.AddChildren("评价", 5);
            process1.AddChildren("检查", 1, index++);
            process1.AddChildren("评论", 2, index++);

            process1 = process.AddChildren("创造", 6);
            process1.AddChildren("产生", 1, index++);
            process1.AddChildren("规划", 2, index++);
            process1.AddChildren("创作", 3, index++);

            List<string> grades = new List<string> { "一年级", "二年级", "三年级", "四年级", "五年级", "六年级", "七年级", "八年级", "九年级" };
            List<string> album = new List<string> { "上册", "下册" };
            List<string> unit = new List<string> { "一单元", "二单元", "三单元", "四单元", "五单元", "六单元", "七单元", "八单元" };
            List<string> lesson = new List<string> { "一课时", "二课时", "三课时", "四课时" };

            KLLevel grade = new KLLevel { ID = "grade" };
            knowledgeDB.Add(grade);
            index = 1;
            int gradeIndex = 1;
            grades.ForEach(g =>
            {
                KLLevel g1 = grade.AddChildren(g, gradeIndex++);
                int albumIndex = 1;
                album.ForEach(a =>
                {
                    KLLevel a1 = g1.AddChildren(a, albumIndex++);
                    int unitIndex = 1;
                    unit.ForEach(u =>
                    {
                        KLLevel u1 = a1.AddChildren(u, unitIndex++);
                        int lessonIndex = 1;
                        lesson.ForEach(l =>
                        {
                            u1.AddChildren(l, lessonIndex++, index++);
                        });
                    });
                });
            });
            knowledgeDB.SaveChanges();
            return Pack(null);
        }
    }
}
