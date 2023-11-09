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
    public class JobsBL : DisposeLogic
    {
        private ObjectDataConnection objDB;
        public JobsBL(ObjectDataConnection objConn)
        {
            this.objDB = objConn;
        }

        public ObjectResponse<object> AddJob(AppSystemObject obj)
        {
            using (var commonDL = new CommonDL(objDB))
            {
                var result = commonDL.ManageObject(obj);

                if (result != null && result.Tables.Count > 0 && result.Tables[0].Rows.Count > 0)
                {
                   
                    string toEmail, ccEmail, jobName, jobId = string.Empty;

                    foreach (DataRow dr in result.Tables[0].Rows)
                    {
                        jobId = dr[0].ToString();
                        jobName = dr[1].ToString();
                        toEmail = dr[2].ToString();
                        ccEmail = dr[3].ToString();

                        //temporary
                        toEmail = "prasad.node@gmail.com";
                        ccEmail = "prasad.node@gmail.com";


                        MailBL.SendEmail(toEmail, ccEmail, "File uploaded successfully - Job Id " + jobId, "New job created. <br>Job name - "
                            + jobName + "<br>Job Id - " + jobId + "<br>");
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


        public ObjectResponse<object> AssignJob(AppSystemObject obj)
        {
            using (var commonDL = new CommonDL(objDB))
            {
                var result = commonDL.ManageObject(obj);

                if (result != null && result.Tables.Count > 0 && result.Tables[0].Rows.Count > 0)
                {

                    string toEmail, ccEmail, jobName, jobId = string.Empty, levelType = string.Empty;
                    //require manager, admin, employee, earlier employee email id's
                    foreach (DataRow dr in result.Tables[0].Rows)
                    {
                        jobId = dr[0].ToString();
                        jobName = dr[1].ToString();
                        toEmail = dr[2].ToString();
                        ccEmail = dr[3].ToString();

                        //temporary
                        toEmail = "prasad.node@gmail.com";
                        ccEmail = "prasad.node@gmail.com";


                        MailBL.SendEmail(toEmail, ccEmail, "New job assigned - Job Id " + jobId, "New job assigned. <br>Job name - "
                            + jobName + "<br>Job Id - " + jobId + "<br>Level Type - " + levelType + "<br>");
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

        //public bool SendEmail(string to, string subject, string body)
        //{
        //    try
        //    {
        //        SmtpClient mailClient = new SmtpClient("smtp.ionos.com", 587);
        //        mailClient.Credentials = new NetworkCredential("admin@maxtranssystems.com", "MRSadmin@2022");
        //        mailClient.Port = 587;

        //        MailMessage message = new MailMessage();
        //        message.From = new MailAddress("admin@maxtranssystems.com", "Max App");
        //        message.To.Add(new MailAddress(to));
        //        message.Bcc.Add(new MailAddress("admin@maxtranssystems.com"));

        //        message.Subject = subject;
        //        message.Body = body;
        //        message.IsBodyHtml = true;

        //        mailClient.EnableSsl = true;
        //        mailClient.Send(message);

        //        return true;
        //    }

        //    catch (Exception ex)
        //    {
        //        return false;
        //    }

        //}

        //public SMTPSettings GetSMTPSettings(AppSystemObject obj)
        //{
        //    SMTPSettings smtp = new SMTPSettings();
        //    using (var commonDL = new CommonDL(objDB))
        //    {
        //        var result = commonDL.GetSMTPSettings(obj);
        //        if (result != null)
        //        {
        //            //if (result.Tables.Count > 0)
        //            //{
        //            //    var dt = result.Tables[0];
        //            //    object response = new object();

        //            //    if (dt.Rows.Count > 0 && dt.Rows[0][0] != DBNull.Value)
        //            //    {
        //            //        var tt = dt.Rows[0][0].ToString();
        //            //        List<SMTPSettings> lstSMTP = new List<SMTPSettings>();
        //            //        lstSMTP = JsonSerializer.Deserialize<List<SMTPSettings>>(tt);
        //            //        smtp = lstSMTP[0];
        //            //    }
        //            //}
        //            while (result.Read())
        //            {
        //                // ReadSingleRow((IDataRecord)reader);
        //                smtp.FromEmail = result.GetString(0);

        //            }

        //            // Call Close when done reading.
        //            result.Close();
        //        }

        //        //if (DataValidationLayer.isDataSetNotNull(result))
        //        //{
        //        //    return new ObjectResponse<object>() { ResponseData = result };
        //        //}
        //        //else
        //        //{
        //        //    return new ObjectResponse<object>() { Error = new Error() { ErrorDescription = "No Data Available" } };

        //        //}
        //        return smtp;
        //    }
        //}


    }
}
