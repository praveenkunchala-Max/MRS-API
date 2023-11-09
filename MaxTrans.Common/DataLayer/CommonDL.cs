using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Augadh.SecurityMonitoring.Common.BusinessLayer;
using Augadh.SecurityMonitoring.Common.DataLayer;
using Augadh.SecurityMonitoring.Common.Entities;

namespace Augadh.SecurityMonitoring.Common.DataLayer
{
    public interface ICommonDL
    {
        //User Authenticate(string username, string password);
        //IEnumerable<User> GetAll();
        //User GetById(int id);
        //Database GetSecurityMonitoringDBInstance(string product);
        //DataSet ManageAugadhSecuritySystemObjects(AugadhSecuritySystemObject obj, string product);
        DataSet ManageObject(AppSystemObject obj);
    }

    public class CommonDL : DisposeLogic, ICommonDL
    {
        private Database dbConnection;
        private ObjectDataConnection _objDB;

        DataSet dsData = new DataSet();

        public CommonDL(ObjectDataConnection objDB)
        {
            this._objDB = objDB;
        }

        public CommonDL()
        {
        }

        private Database GetSecurityMonitoringDBInstance(string product)
        {
            return _objDB.GetSecurityMonitoringDBConnection(product);
        }

        private Database GetObjectDBInstance()
        {
            return _objDB.GetObjectDBConnection();
        }

        //public DataSet ManageAugadhSecuritySystemObjects(AugadhSecuritySystemObject obj, string product)
        //{

        //    dbConnection = GetSecurityMonitoringDBInstance(product);
        //    if (obj.ParamsJson == null)
        //        obj.ParamsJson = string.Empty;

        //    DataSet res = dbConnection.ExecuteDataSet(ObjectConstants.StoredProcedures.AugadhSecuritySystemManageObjects.ToString(),
        //                    obj.ObjectName, obj.Action, obj.ActionType, obj.ParamsJson.Trim());
        //    return res;
        //}

        public DataSet ManageObject(AppSystemObject obj)
        {

            dbConnection = GetObjectDBInstance();
            if (obj.ParamsJson == null)
                obj.ParamsJson = string.Empty;

            DataSet res = dbConnection.ExecuteDataSet(ObjectConstants.StoredProcedures.ManageObjects.ToString(),
                            obj.ObjectName, obj.Action, obj.ActionType, obj.ParamsJson.Trim());
            return res;
        }
        public IDataReader GetSMTPSettings(AppSystemObject obj)
        {

            dbConnection = GetObjectDBInstance();
            if (obj.ParamsJson == null)
                obj.ParamsJson = string.Empty;

            IDataReader res = dbConnection.ExecuteReader(ObjectConstants.StoredProcedures.ManageObjects.ToString(),
                            obj.ObjectName, obj.Action, obj.ActionType, obj.ParamsJson.Trim());

            //try
            //{
            //    //sql connection object
                //using (SqlConnection conn = new SqlConnection(connString))
                //{

                    //set stored procedure name
                    //string spName = @"dbo.[uspEmployeeInfo]";

                    //define the SqlCommand object
                   // SqlCommand cmd = new SqlCommand(spName, conn);

                    //Set SqlParameter - the employee id parameter value will be set from the command line
                   // SqlParameter param1 = new SqlParameter();
                   // param1.ParameterName = "@employeeID";
                   // param1.SqlDbType = SqlDbType.Int;
                   // param1.Value = int.Parse(args[0].ToString());

                    //add the parameter to the SqlCommand object
                   // cmd.Parameters.Add(param1);

                    //open connection
                    //conn.Open();

                    //set the SqlCommand type to stored procedure and execute
                   // cmd.CommandType = CommandType.StoredProcedure;
                   // SqlDataReader dr = cmd.ExecuteReader();

                   // Console.WriteLine(Environment.NewLine + "Retrieving data from database..." + Environment.NewLine);
                    //Console.WriteLine("Retrieved records:");

                    //check if there are records
                    //if (res.Read())
                    //{
                        //while (res.Read())
                        //{
                        //    empID = dr.GetInt32(0);
                        //    empCode = dr.GetString(1);
                        //    empFirstName = dr.GetString(2);
                        //    empLastName = dr.GetString(3);
                        //    locationCode = dr.GetString(4);
                        //    locationDescr = dr.GetString(5);

                        //    //display retrieved record
                        //    //Console.WriteLine("{0},{1},{2},{3},{4},{5}", empID.ToString(), empCode, empFirstName, empLastName, locationCode, locationDescr);
                        //}
                    //}
                    //else
                    //{
                    //    Console.WriteLine("No data found.");
                    //}

                    //close data reader
                    //dr.Close();

                    //close connection
                    //conn.Close();
                //}
            //}
            //catch (Exception ex)
            //{
            //    //display error message
            //    Console.WriteLine("Exception: " + ex.Message);
            //}

            return res;
        }

    }
}
