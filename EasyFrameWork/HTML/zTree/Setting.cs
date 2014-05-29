using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.HTML.zTree
{
    public class Setting
    {
        #region 类
        public class Data
        {
            public SimpleData simpleData { get; set; }
        }
        public class SimpleData
        {
            public bool enable { get; set; }
        }
        public class CallBack
        {
            public string beforeAsync { get; set; }
            public string beforeCheck { get; set; }
            public string beforeClick { get; set; }
            public string beforeCollapse { get; set; }
            public string beforeDblClick { get; set; }
            public string beforeDrag { get; set; }
            public string beforeDragOpen { get; set; }
            public string beforeDrop { get; set; }
            public string beforeEditName { get; set; }
            public string beforeExpand { get; set; }
            public string beforeMouseDown { get; set; }
            public string beforeMouseUp { get; set; }
            public string beforeRemove { get; set; }
            public string beforeRename { get; set; }
            public string beforeRightClick { get; set; }
            public string onAsyncError { get; set; }
            public string onAsyncSuccess { get; set; }
            public string onCheck { get; set; }
            public string onClick { get; set; }
            public string onCollapse { get; set; }
            public string onDblClick { get; set; }
            public string onDrag { get; set; }
            public string onDrop { get; set; }
            public string onExpand { get; set; }
            public string onMouseDown { get; set; }
            public string onMouseUp { get; set; }
            public string onNodeCreated { get; set; }
            public string onRemove { get; set; }
            public string onRename { get; set; }
            public string onRightClick { get; set; }
        }
        public class Async
        {
            public Async()
            {
                autoParam = new List<string>();
            }
            public bool enable { get; set; }
            public string url { get; set; }
            public List<string> autoParam { get; set; }
            public string otherParam { get; set; }
            public string dataFilter { get; set; }
        }
        public class Check
        {
            public Check()
            {
                this.chkboxType = new ChkboxType()
                {
                    Y = "ps",
                    N = "ps"
                };
            }
            public class ChkboxType
            {
                public string Y { get; set; }
                public string N { get; set; }
            }
            public bool autoCheckTrigger { get; set; }
            public ChkboxType chkboxType { get; set; }
            public string chkStyle { get; set; }
            public bool enable { get; set; }
            public bool nocheckInherit { get; set; }
            public bool chkDisabledInherit { get; set; }
            public string radioType { get; set; }
        }
        #endregion

        public Data data
        {
            get;
            set;
        }
        public CallBack callback
        {
            get;
            set;
        }
        public Async async
        {
            get;
            set;
        }
        public Check check
        {
            get;
            set;
        }
    }

}
