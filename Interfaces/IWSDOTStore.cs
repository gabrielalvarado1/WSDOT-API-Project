using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WSDOT_API_Project.Models;

namespace WSDOT_API_Project.Interfaces
{
    public interface IWSDOTStore
    {
        public Task<List<PassInfo>> CallAPI(string accessCode);

        public void FindPass(List<PassInfo> passInfo, string selection);
    }
}
