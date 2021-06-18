namespace Util
{
    /// <summary>
    /// 数据表格分页
    /// </summary>
    public class Pagination
    {
        #region 构造函数

        public Pagination()
        {
            SortField = "ID";
            SortType = "asc";
            PageNumber = 1;
            PageSize = int.MaxValue;
        }

        #endregion 构造函数

        #region 私有成员

        /// <summary>
        /// 总页数
        /// </summary>
        private int _pageCount
        {
            get
            {
                int pages = Total / PageSize;
                int pageCount = Total % PageSize == 0 ? pages : pages + 1;
                return pageCount;
            }
        }

        #endregion 私有成员

        #region 默认方案

        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// 每页行数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 排序列
        /// </summary>
        public string SortField { get; set; }

        /// <summary>
        /// 排序类型
        /// </summary>
        public string SortType { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int Pages { get => _pageCount; }

        /// <summary>
        /// 是否首页
        /// </summary>
        public bool IsFirstPage { get => PageNumber == 1; }

        /// <summary>
        /// 是否尾页
        /// </summary>
        public bool IsLastPage { get => PageNumber == Pages; }

        #endregion 默认方案
    }
}