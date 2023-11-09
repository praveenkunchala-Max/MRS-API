using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Augadh.SecurityMonitoring.API.Entities;
using Augadh.SecurityMonitoring.API.Helpers;
using Augadh.SecurityMonitoring.Common.BusinessLayer;
using Augadh.SecurityMonitoring.Common.DataLayer;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.SqlClient;

namespace Augadh.SecurityMonitoring.API.Services
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        User AuthenticateGK(string username, string password, string product);
        IEnumerable<User> GetAll();
        User GetById(int id);
    }

    public class UserService : DisposeLogic, IUserService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<User> _users = new List<User>
        {
            new User { Id = 1, FirstName = "Admin", LastName = "User", Username = "admin", Password = "admin33", Role = Role.Admin },
            new User { Id = 1, FirstName = "Vendor", LastName = "Admin", Username = "vendoradmin", Password = "admin33", Role = Role.VendorAdmin },
            new User { Id = 1, FirstName = "Location", LastName = "Admin", Username = "locationadmin", Password = "admin33", Role = Role.LocationAdmin },
            new User { Id = 2, FirstName = "Location", LastName = "User", Username = "locationuser", Password = "user33", Role = Role.LocationUser }            
        };

        private readonly AppSettings _appSettings;
       // private ObjectDataConnection objDB;

        private ObjectDataConnection _objDB;


        private CommonDL _commonDL;
        private Database dbConnection;

        public UserService(IOptions<AppSettings> appSettings)//, ObjectDataConnection objDB, CommonDL commonDL)
        {
            _appSettings = appSettings.Value;
            //_objDB = objDB;
            //_commonDL = commonDL;
        }

        private Database GetSecurityMonitoringDBInstance(string product)
        {
            return _objDB.GetSecurityMonitoringDBConnection(product);
        }

        private User GetUser(string username, string password, string product)
        {
            User user = new User();
            string constring = "";
            if(product == "maxtrans")
            {
                //#TODO
                //add new connection string
                //constring = @"Server=tcp:nri-tdp.database.windows.net,1433;Initial Catalog=nri-tdp;Persist Security Info=False;User ID=tdp-admin;Password=Party_telugu@$99;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                constring = @"Data Source=MaxTrans;Initial Catalog=SecurityAreaMonitoring;Integrated Security=True;MultipleActiveResultSets=True";
            }
            else
            constring = @"Data Source=MaxTrans;Initial Catalog=SecurityAreaMonitoring;Integrated Security=True;MultipleActiveResultSets=True";
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand(@"select * from [user] where username = @userName and password = @password", con))
                {
                    cmd.CommandType = CommandType.Text;
                    // 2. define parameters used in command object
                    SqlParameter paramUserName = new SqlParameter();
                    paramUserName.ParameterName = "@userName";
                    paramUserName.Value = username;

                    SqlParameter paramPassword = new SqlParameter();
                    paramPassword.ParameterName = "@password";
                    paramPassword.Value = password;

                    // 3. add new parameter to command object
                    cmd.Parameters.Add(paramUserName);
                    cmd.Parameters.Add(paramPassword);

                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            //dataGridView1.DataSource = dt;
                            if(dt.Rows.Count >0)
                            {
                                DataRow dr = dt.Rows[0];
                                
                                user.FirstName = dt.Rows[0][4].ToString();
                                user.LastName = dt.Rows[0][6].ToString();
                                user.Role = dt.Rows[0][9].ToString();
                                user.Username = dt.Rows[0][2].ToString();

                            }
                        }
                    }
                }
            }
            return user;
        }

        public User Authenticate(string username, string password)
        {
            User dbUser = new User();

            //var commonDL = new CommonDL(new ObjectDataConnection());

            //AugadhSecuritySystemObject obj = new AugadhSecuritySystemObject();

            //dbConnection = GetSecurityMonitoringDBInstance("monitoring");
            //if (obj.ParamsJson == null)
            //    obj.ParamsJson = string.Empty;

            //DataSet res = dbConnection.ExecuteDataSet(ObjectConstants.StoredProcedures.AugadhSecuritySystemManageObjects.ToString(),
            //                obj.ObjectName, obj.Action, obj.ActionType, obj.ParamsJson.Trim());

            // var result = _commonDL.ManageAugadhSecuritySystemObjects(obj, "monitoring");

           // BindData();

            dbUser = _users[0];

            //var user = _users.SingleOrDefault(x => x.Username == username && x.Password == password);
            var user = GetUser(username, password, "");

            // return null if user not found
            if (user == null || user.Username == "")
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user.WithoutPassword();
        }

        public User AuthenticateGK(string username, string password, string product)
        {
            User dbUser = new User();

            //var commonDL = new CommonDL(new ObjectDataConnection());

            //AugadhSecuritySystemObject obj = new AugadhSecuritySystemObject();

            //dbConnection = GetSecurityMonitoringDBInstance("monitoring");
            //if (obj.ParamsJson == null)
            //    obj.ParamsJson = string.Empty;

            //DataSet res = dbConnection.ExecuteDataSet(ObjectConstants.StoredProcedures.AugadhSecuritySystemManageObjects.ToString(),
            //                obj.ObjectName, obj.Action, obj.ActionType, obj.ParamsJson.Trim());

            // var result = _commonDL.ManageAugadhSecuritySystemObjects(obj, "monitoring");

            // BindData();

            dbUser = _users[0];

            //var user = _users.SingleOrDefault(x => x.Username == username && x.Password == password);
            var user = GetUser(username, password, product);

            // return null if user not found
            if (user == null || user.Username == "")
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user.WithoutPassword();
        }

        public IEnumerable<User> GetAll()
        {
            return _users.WithoutPasswords();
        }

        public User GetById(int id)
        {
            var user = _users.FirstOrDefault(x => x.Id == id);
            return user.WithoutPassword();
        }
    }
}
