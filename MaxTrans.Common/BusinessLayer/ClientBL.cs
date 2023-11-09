using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.Json;
using Augadh.SecurityMonitoring.Common.BusinessLayer;
using Augadh.SecurityMonitoring.Common.DataLayer;
using Augadh.SecurityMonitoring.Common.Entities;
using Augadh.SecurityMonitoring.Common.Helpers;
using System.Net;
using System.Net.Mail;
using MaxTrans.Common.Entities;
using MaxTrans.Common.BusinessLayer;

namespace Augadh.SecurityMonitoring.Common.BusinessLayer
{
    public class ClientBL : DisposeLogic
    {
        private ObjectDataConnection objDB;
        public ClientBL(ObjectDataConnection objConn)
        {
            this.objDB = objConn;
        }

        public ObjectResponse<object> AddClient(AppSystemObject obj)
        {
            using (var commonDL = new CommonDL(objDB))
            {
                var result = commonDL.ManageObject(obj);
                //in the result retrieve client email and other details

                if (result != null && result.Tables.Count > 0 && result.Tables[0].Rows.Count > 0)
                {
                   
                    string clientId, clientName, clientDetails, clientType, email, toEmail, ccEmail = string.Empty;

                    foreach (DataRow dr in result.Tables[0].Rows)
                    {
                        clientId = dr[0].ToString();
                        clientName = dr[1].ToString();
                        clientDetails = dr[2].ToString();
                        clientType = dr[2].ToString();
                        email = dr[2].ToString();

                        //temporary
                        toEmail = "prasad.node@gmail.com";
                        ccEmail = "prasad.node@gmail.com";


                        MailBL.SendEmail(toEmail, ccEmail, "Client account created successfully -  " + clientName, "Account created successfully. <br> "
                            + "<br>Default password is -  maxtrans@123 "+ " <br>");
                    }
                }                

                if (DataValidationLayer.isDataSetNotNull(result))
                {
                    return new ObjectResponse<object>() { ResponseData = result };
                }
                else
                {
                    return new ObjectResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };

                }
            }
        }
     
    }
}
