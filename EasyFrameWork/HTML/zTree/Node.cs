
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Easy.HTML.zTree
{
    public class Node
    {
        /// <summary>
        /// 值
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 父级ID
        /// </summary>
        public string pId { get; set; }
        /// <summary>
        /// 显示文本
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 是否展开
        /// </summary>
        public bool open { get; set; }

        public bool Checked { get; set; }

        /// <summary>
        /// 是否有子级
        /// </summary>
        public bool isParent { get; set; }
        /// <summary>
        /// 点击时是否选中
        /// </summary>
        public bool click { get; set; }
    }
}
