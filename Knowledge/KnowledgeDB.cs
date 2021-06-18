using Knowledge.Tables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knowledge
{
    public class KnowledgeDB : DbContext
    {
        /// <summary>
        /// 取表中非删除的数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IQueryable<T> Sets<T>() where T : DbBase => Set<T>().Where(l => l.DeleteFlag == 0);

        /// <summary>
        /// 数据库配置
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder
                .UseMySql("Server=127.0.0.1;Port=3306;Database=knowledge; User=root;Password=123456;CharSet=utf8;Convert Zero Datetime=True;Allow Zero Datetime=True");
        }

        /// <summary>
        /// 模型建立
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<NKLine>();
            modelBuilder.Entity<KLPoint>();
            modelBuilder.Entity<KLLevel>(); 
        }
    }
}
