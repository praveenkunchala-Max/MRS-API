using System;
using System.Collections.Generic;
using System.Text;

namespace Augadh.SecurityMonitoring.Common.Entities
{
    

    //public class ObjectDto
    //{
    //    public long ObjectId { get; set; }
    //    public string ObjectName { get; set; }
    //    public string Action { get; set; }
    //    public List<Column> Items { get; set; }
    //    public string Query { get; set; }
    //    public List<ObjectAction> ObjectActions { get; set; }        
    //    public string TypeScriptServiceObjectName { get; set; }


    //}

    public class AppSystemObject
    {
        public string ObjectName { get; set; }
        
        public string Action { get; set; }
        public string ActionType { get; set; }
        public bool SendMail { get; set; }
        public string ParamsJson { get; set; }


    }
    
}
