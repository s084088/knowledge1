using Microsoft.EntityFrameworkCore;
using NKnowledge.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NKnowledge
{
    public class NKnowledgeDB : DbContext
    {

        /// <summary>
        /// 数据库配置
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            string connectionString = "Server=127.0.0.1;Port=3306;Database=nknowledge; User=root;Password=123456;CharSet=utf8;Convert Zero Datetime=True;Allow Zero Datetime=True";
            ServerVersion serverVersion = new MariaDbServerVersion(new Version(10, 5, 4));
            optionsBuilder.UseMySql(connectionString, serverVersion);
        }

        /// <summary>
        /// 模型建立
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Line>();
        }
    }
}
