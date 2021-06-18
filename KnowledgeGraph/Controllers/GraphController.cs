using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteEntity;
using LiteEntity.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Util;

namespace KnowledgeGraph.Controllers
{
    public class GraphController : Controller
    {
        private readonly LiteDB liteDB = new LiteDB();

        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Math_Yuan()
        {
            return View("Math_Yuan");
        }

        public IActionResult Math_Tree()
        {
            return View("Math_Tree");
        }

        public IActionResult Math_Tree_1()
        {
            return View("Math_Tree_1");
        }

        public IActionResult English()
        {
            return View("English");
        }

        public IActionResult English_Yuan()
        {
            return View("English_Yuan");
        }

        public ActionResult GetGraph()
        {
            var color = new string[][]
            {
                new string[]
                {
                    "#EA0000",
                    "#FF0000",
                    "#FF2D2D",
                    "#FF5151",
                    "#ff7575",
                    "#FF9797",
                    "#FFB5B5",
                    "#FFD2D2",
                    "#FFECEC",
                },
                new string[]
                {
                    "#2894FF",
                    "#46A3FF",
                    "#66B3FF",
                    "#84C1FF",
                    "#97CBFF",
                    "#ACD6FF",
                    "#C4E1FF",
                    "#D2E9FF",
                    "#ECF5FF",
                },
                new string[]
                {
                    "#F9F900",
                    "#FFFF37",
                    "#FFFF6F",
                    "#FFFF93",
                    "#FFFFAA",
                    "#FFFFB9",
                    "#FFFFCE",
                    "#FFFFDF",
                    "#FFFFF4",
                },
                new string[]
                {
                    "#28FF28",
                    "#53FF53",
                    "#79FF79",
                    "#93FF93",
                    "#A6FFA6",
                    "#BBFFBB",
                    "#CEFFCE",
                    "#DFFFDF",
                    "#F0FFF0",
                },

            };

            List<KP> kPs = liteDB.Set<KP>().Where(l => l.Subject == 1).ToList();
            List<KPR> kPRs = liteDB.Set<KPR>().Where(l => l.Subject == 1).ToList();

            var nodes = kPs.Select(l => new
            {
                id = l.ID,
                name = l.Name,
                symbolSize = (9 - l.Type) * (9 - l.Type) / 4D,
                category = l.Importance - 1,
                label = new
                {
                    show = true,
                    fontSize = 13 - l.Type
                },
                itemStyle = new
                {
                    normal = new
                    {
                        //borderColor = color[l.Importance - 1][l.Type - 1],
                        //borderWidth = 2,
                        shadowBlur = 15,
                        shadowColor = color[l.Importance - 1][l.Type - 1],
                        color = color[l.Importance - 1][l.Type + 1],
                    }
                }
            });

            var links = kPRs.Select(l => new
            {
                id = l.Text,
                source = l.OriginID,
                target = l.TargetID,
                name = l.Text,
            });

            ApiPakcage apiPakcage = new ApiPakcage { Result = new { nodes, links } };


            return Content(apiPakcage.ToJson());
        }

        public ActionResult GetEnglishGraph()
        {
            var color = new string[][]
            {
                new string[]
                {
                    "#EA0000",
                    "#FF0000",
                    "#FF2D2D",
                    "#FF5151",
                    "#ff7575",
                    "#FF9797",
                    "#FFB5B5",
                    "#FFD2D2",
                    "#FFECEC",
                },
                new string[]
                {
                    "#2894FF",
                    "#46A3FF",
                    "#66B3FF",
                    "#84C1FF",
                    "#97CBFF",
                    "#ACD6FF",
                    "#C4E1FF",
                    "#D2E9FF",
                    "#ECF5FF",
                },
                new string[]
                {
                    "#F9F900",
                    "#FFFF37",
                    "#FFFF6F",
                    "#FFFF93",
                    "#FFFFAA",
                    "#FFFFB9",
                    "#FFFFCE",
                    "#FFFFDF",
                    "#FFFFF4",
                },
                new string[]
                {
                    "#28FF28",
                    "#53FF53",
                    "#79FF79",
                    "#93FF93",
                    "#A6FFA6",
                    "#BBFFBB",
                    "#CEFFCE",
                    "#DFFFDF",
                    "#F0FFF0",
                },

            };

            List<KP> kPs = liteDB.Set<KP>().Where(l => l.Subject == 2).ToList();
            List<KPR> kPRs = liteDB.Set<KPR>().Where(l => l.Subject == 2).ToList();

            var nodes = kPs.Select(l => new
            {
                id = l.ID,
                name = l.Name,
                symbolSize = (9 - l.Type) * (9 - l.Type) / 4D,
                category = l.Importance - 1,
                label = new
                {
                    show = true,
                    fontSize = 13 - l.Type
                },
                itemStyle = new
                {
                    normal = new
                    {
                        //borderColor = color[l.Importance - 1][l.Type - 1],
                        //borderWidth = 2,
                        shadowBlur = 15,
                        shadowColor = color[l.Importance - 1][l.Type - 1],
                        color = color[l.Importance - 1][l.Type + 1],
                    }
                }
            });

            var links = kPRs.Select(l => new
            {
                id = l.Text,
                source = l.OriginID,
                target = l.TargetID,
                name = l.Text,
            });

            ApiPakcage apiPakcage = new ApiPakcage { Result = new { nodes, links } };


            return Content(apiPakcage.ToJson());
        }
    }
}
