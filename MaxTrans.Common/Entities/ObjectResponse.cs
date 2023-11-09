using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using Augadh.SecurityMonitoring.Common.BusinessLayer;

namespace Augadh.SecurityMonitoring.Common.Entities
{
    public class ObjectResponse<T> : DisposeLogic
    {
        [DataMember(Order = 1)]
        public string ResponseType
        {
            get
            {
                return typeof(T).Name;
            }
            set { }
        }

        [DataMember(Order = 2)]
        public string Response { get; set; }
        

        [DataMember(Order = 3)]
        public string ResponseMessage { get; set; }

        [DataMember(Order = 4)]
        public string ResponseCode { get; set; }

        [DataMember(Order = 5)]
        public string ResponseValue { get; set; }

        [DataMember(Order = 3)]
        public T ResponseData { get; set; }

        [DataMember(Order = 6)]
        public Error Error { get; set; }
    }
}
