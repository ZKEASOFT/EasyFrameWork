using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.Attribute;

namespace Easy.Models
{
    public class EditorEntity
    {
        /// <summary>
        /// 创建人ID
        /// </summary>
        public string CreateBy { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatebyName { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 修改人ID
        /// </summary>
        public string LastUpdateBy { get; set; }
        /// <summary>
        /// 最后修改人
        /// </summary>
        public string LastUpdateByName { get; set; }
        /// <summary>
        /// 最后更新日期
        /// </summary>
        public DateTime? LastUpdateDate { get; set; }
    }

}
