using Knowledge;
using Knowledge.Tables;
using KnowledgeAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Util;

namespace KnowledgeAPI
{
    public static class Res
    {
        private static KnowledgeDB knowledgeDB = new KnowledgeDB();
        private static List<CascadeModel> types;
        private static List<CascadeModel> processes;
        private static List<CascadeModel> grades;

        public static List<CascadeModel> Types
        {
            get
            {
                if (types == null)
                {
                    types = knowledgeDB.Set<KLLevel>().Where(l => l.ParentID == "type").OrderBy(l => l.Value).Select(l => new CascadeModel
                    {
                        ID = l.ID,
                        Name = l.Name,
                        Value = l.Value,
                        Children = l.Children.OrderBy(l => l.Value).Select(k => new CascadeModel
                        {
                            ID = k.ID,
                            Name = k.Name,
                            Value = k.Value,
                        })
                    }).ToList();
                    types.ForEach(l => SetFullName(l));
                }
                return types;
            }
        }

        public static List<CascadeModel> Processes
        {
            get
            {
                if (processes == null)
                {
                    processes = knowledgeDB.Set<KLLevel>().Where(l => l.ParentID == "process").OrderBy(l => l.Value).Select(l => new CascadeModel
                    {
                        ID = l.ID,
                        Name = l.Name,
                        Value = l.Value,
                        Children = l.Children.OrderBy(l => l.Value).Select(k => new CascadeModel
                        {
                            ID = k.ID,
                            Name = k.Name,
                            Value = k.Value,
                        })
                    }).ToList();
                    processes.ForEach(l => SetFullName(l));
                }
                return processes;
            }
        }

        public static List<CascadeModel> Grades
        {
            get
            {
                if (grades == null)
                {
                    grades = knowledgeDB.Set<KLLevel>().Where(l => l.ParentID == "grade").OrderBy(l => l.Value).Select(l => new CascadeModel
                    {
                        ID = l.ID,
                        Name = l.Name,
                        Value = l.Value,
                        Children = l.Children.OrderBy(l => l.Value).Select(k => new CascadeModel
                        {
                            ID = k.ID,
                            Name = k.Name,
                            Value = k.Value,
                            Children = k.Children.OrderBy(k => k.Value).Select(j => new CascadeModel
                            {
                                ID = j.ID,
                                Name = j.Name,
                                Value = j.Value,
                                Children = j.Children.OrderBy(j => j.Value).Select(h => new CascadeModel
                                {
                                    ID = h.ID,
                                    Name = h.Name,
                                    Value = h.Value,
                                })
                            })
                        })
                    }).ToList();
                    grades.ForEach(l => SetFullName(l));
                }
                return grades;
            }
        }

        public static void SetFullName(CascadeModel cascadeModel, string lastName = "")
        {
            if (lastName != "") cascadeModel.FullName = lastName + Environment.NewLine + cascadeModel.Name;
            else cascadeModel.FullName = cascadeModel.Name;
            cascadeModel.Children?.ForEach(l => SetFullName(l, cascadeModel.FullName));
        }
    }
}