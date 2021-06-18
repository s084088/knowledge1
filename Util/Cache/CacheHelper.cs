using System;

namespace Util
{
    /// <summary>
    /// 缓存帮助类
    /// </summary>
    public class CacheHelper
    {
        /// <summary>
        /// 缓存类型,1系统缓存,2redis缓存
        /// </summary>
        public static int Type { get; } = 1;

        /// <summary>
        /// redis配置
        /// </summary>
        public static string RedisConfig { get; } = "r-uf6l5xvs08y1zxpomqpd.redis.rds.aliyuncs.com:6379,password=xTOdvTvclzMzvx8KCkbyJK1TcjYTBVbq" /*"localhost:6379,password=123456"*/;

        /// <summary>
        /// 静态构造函数，初始化缓存类型
        /// </summary>
        static CacheHelper()
        {
            SystemCache = new SystemCache();

            if (!RedisConfig.IsNullOrEmpty())
            {
                try
                {
                    RedisCache = new RedisCache(RedisConfig);
                }
                catch
                {
                }
            }

            switch (Type)
            {
                case 1: Cache = SystemCache; break;
                case 2: Cache = RedisCache; break;
                default: throw new Exception("请指定缓存类型！");
            }
        }

        /// <summary>
        /// 默认缓存
        /// </summary>
        public static ICache Cache { get; }

        /// <summary>
        /// 系统缓存
        /// </summary>
        public static ICache SystemCache { get; }

        /// <summary>
        /// Redis缓存
        /// </summary>
        public static ICache RedisCache { get; }
    }
}