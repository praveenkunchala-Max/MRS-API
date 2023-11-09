using Augadh.SecurityMonitoring.Common.BusinessLayer;
using Augadh.SecurityMonitoring.Common.DataLayer;
using Augadh.SecurityMonitoring.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace MaxTrans.API
{
    public class RefreshTokenGenerator : DisposeLogic, IRefreshTokenGenerator
    {
        //private readonly Learn_DBContext context;

        public RefreshTokenGenerator()
        {
            //context = learn_DB;
        }
        public string GenerateToken(string username)
        {
            var randomnumber = new byte[32];
            using (var randomnumbergenerator = RandomNumberGenerator.Create())
            {
                randomnumbergenerator.GetBytes(randomnumber);
                string RefreshToken = Convert.ToBase64String(randomnumber);

                //DOING DOING
                var commonBL = new CommonBL(new ObjectDataConnection());
                AppSystemObject obj = new AppSystemObject{  Action="", ActionType = "", ObjectName = "", ParamsJson = ""};
                //var _pp = commonBL.ManageAuth(obj, "nritdp");

                //var _user = context.TblRefreshtoken.FirstOrDefault(o => o.UserId == username);
                //if (_user != null)
                //{
                //    _user.RefreshToken = RefreshToken;
                //    context.SaveChanges();
                //}
                //else
                //{
                //    TblRefreshtoken tblRefreshtoken = new TblRefreshtoken()
                //    {
                //        UserId = username,
                //        TokenId = new Random().Next().ToString(),
                //        RefreshToken = RefreshToken,
                //        IsActive = true
                //    };
                //}

                return RefreshToken;
            }
        }
    }
}
