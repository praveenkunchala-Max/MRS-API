using System;
using System.Collections.Generic;
using System.Text;

namespace Augadh.SecurityMonitoring.Common.BusinessLayer
{
    public static class ObjectConstants
    {
        #region String Constants

        public static string InActiveUser = "InActiveUser";
        public static string ROOT = "ROOT";
        public static string Features = "Features";
        public static string FeatureId = "FeatureId";
        public static string Clients = "Clients";
        public static string ClientId = "ClientId";
        public static string Properties = "Properties";
        public static string PropertyId = "PropertyId";

        public static string CreateUser = "CreateUser";
        public static string AssignClientsToUser = "AssignClientsToUser";
        public static string UpdateClientsToUser = "UpdateClientsToUser";
        public static string AssignPropertiesToUser = "AssignPropertiesToUser";
        public static string UpdatePropertiesToUser = "UpdatePropertiesToUser";

        public static char Cap = '^';

        #endregion


        #region Int Constants

        public static int Zero = 0;
        public static int One = 1;
        public static int Two = 2;
        public static int Three = 3;
        public static int Four = 4;
        public static int Five = 5;
        public static int Six = 6;
        public static int Seven = 7;


        #endregion

        public enum StoredProcedures
        {
            ManageObjects

        }

       

    }
}
