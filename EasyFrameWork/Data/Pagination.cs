using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.Data
{
    /// <summary>
    /// 分页信息
    /// </summary>
    public class Pagination
    {
        public Pagination()
        {
            this.PageIndex = 0;
            this.PageSize = 20;
        }
        /// <summary>
        /// 当前页，索引从0开始。
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 分页大小
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int AllPage
        {
            get
            {
                long num = RecordCount / (long)PageSize;
                if (RecordCount % PageSize > 0)
                {
                    num++;
                }
                return (int)num;
            }
        }
        /// <summary>
        /// 总数据量
        /// </summary>
        public long RecordCount { get; set; }
    }
}
