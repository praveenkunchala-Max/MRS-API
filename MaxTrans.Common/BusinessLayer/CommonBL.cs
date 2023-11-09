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

namespace Augadh.SecurityMonitoring.Common.BusinessLayer
{
    public class CommonBL : DisposeLogic
    {
        private ObjectDataConnection objDB;
        public CommonBL(ObjectDataConnection objConn)
        {
            this.objDB = objConn;
        }



        //public ObjectResponse<object> ManageAugadhSecurityMonitoringObjects(AugadhSecuritySystemObject obj, string product)
        //{
        //    using (var commonDL = new CommonDL(objDB))
        //    {
        //        var result = commonDL.ManageAugadhSecuritySystemObjects(obj, product);
        //        if (DataValidationLayer.isDataSetNotNull(result))
        //        {
        //            return new ObjectResponse<object>() { ResponseData = result };
        //        }
        //        else
        //        {
        //            return new ObjectResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };

        //        }
        //    }
        //}

        public ObjectResponse<object> ManageObject(AppSystemObject obj, string product)
        {
            using (var commonDL = new CommonDL(objDB))
            {
                var result = commonDL.ManageObject(obj);

                if(obj.SendMail)
                {
                    
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

        public bool SendEmail(string to, string subject, string body)
        {
            try
            {
                SmtpClient mailClient = new SmtpClient("smtp.ionos.com", 587);
                mailClient.Credentials = new NetworkCredential("admin@maxtranssystems.com", "MRSadmin@2022");
                mailClient.Port = 587;

                MailMessage message = new MailMessage();
                message.From = new MailAddress("admin@maxtranssystems.com", "Max App");
                message.To.Add(new MailAddress(to));
                message.Bcc.Add(new MailAddress("admin@maxtranssystems.com"));

                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;

                mailClient.EnableSsl = true;
                mailClient.Send(message);

                return true;
            }

            catch (Exception ex)
            {
                return false;
            }

        }

        public AppUser ManageAuth(AppSystemObject obj, string product)
        {
            AppUser appUser = new AppUser();
            using (var commonDL = new CommonDL(objDB))
            {
                var result = commonDL.ManageObject(obj);
                if(result != null)
                {
                    if(result.Tables.Count>0)
                    {                
                        var dt = result.Tables[0];
                        object response = new object();
                       
                        if (dt.Rows.Count > 0 && dt.Rows[0][0] != DBNull.Value)
                        {
                            var tt = dt.Rows[0][0].ToString();
                            List<AppUser> lstUsers = new List<AppUser>();
                            lstUsers = JsonSerializer.Deserialize<List<AppUser>>(tt);
                            appUser = lstUsers[0];
                        }
                    }
                }

                //if (DataValidationLayer.isDataSetNotNull(result))
                //{
                //    return new ObjectResponse<object>() { ResponseData = result };
                //}
                //else
                //{
                //    return new ObjectResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };

                //}
                return appUser;
            }
        }

        public SMTPSettings GetSMTPSettings(AppSystemObject obj, string product)
        {
            SMTPSettings smtp = new SMTPSettings();
            using (var commonDL = new CommonDL(objDB))
            {
                var result = commonDL.GetSMTPSettings(obj);
                if (result != null)
                {
                    //if (result.Tables.Count > 0)
                    //{
                    //    var dt = result.Tables[0];
                    //    object response = new object();

                    //    if (dt.Rows.Count > 0 && dt.Rows[0][0] != DBNull.Value)
                    //    {
                    //        var tt = dt.Rows[0][0].ToString();
                    //        List<SMTPSettings> lstSMTP = new List<SMTPSettings>();
                    //        lstSMTP = JsonSerializer.Deserialize<List<SMTPSettings>>(tt);
                    //        smtp = lstSMTP[0];
                    //    }
                    //}
                    while (result.Read())
                    {
                        // ReadSingleRow((IDataRecord)reader);
                        smtp.FromEmail = result.GetString(0);

                    }

                    // Call Close when done reading.
                    result.Close();
                }

                //if (DataValidationLayer.isDataSetNotNull(result))
                //{
                //    return new ObjectResponse<object>() { ResponseData = result };
                //}
                //else
                //{
                //    return new ObjectResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };

                //}
                return smtp;
            }
        }

        public AppUser ManageRoles(AppSystemObject obj, string product)
        {
            using (var commonDL = new CommonDL(objDB))
            {
                var result = commonDL.ManageObject(obj);
                var dt = result.Tables[0];
                object response = new object();
                AppUser appUser = new AppUser();
                if (dt.Rows.Count > 0 && dt.Rows[0][0] != DBNull.Value)
                {
                    var tt = dt.Rows[0][0].ToString();
                    List<AppUser> lstUsers = new List<AppUser>();
                    lstUsers = JsonSerializer.Deserialize<List<AppUser>>(tt);
                    appUser = lstUsers[0];


                }

                //if (DataValidationLayer.isDataSetNotNull(result))
                //{
                //    return new ObjectResponse<object>() { ResponseData = result };
                //}
                //else
                //{
                //    return new ObjectResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };

                //}
                return appUser;
            }
        }

        public ObjectResponse<object> Authenticate(AppSystemObject obj, string product)
        {
            using (var commonDL = new CommonDL(objDB))
            {
                var result = commonDL.ManageObject(obj);
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
