using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Augadh.SecurityMonitoring.Common.BusinessLayer
{
   public static class DataValidationLayer
    {
        public static bool isDataSetNotNull(DataSet dsDataset,bool isDataExists = false)
        {
            bool isResult = false;
            if(dsDataset != null && dsDataset.Tables.Count > 0)
            {
                isResult =  true;
            }

            return isResult;
        }
    }
}
