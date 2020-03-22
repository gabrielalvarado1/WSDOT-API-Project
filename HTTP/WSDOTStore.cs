using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WSDOT_API_Project.Interfaces;
using WSDOT_API_Project.Models;
using WSDOT_API_Project.Utilities;

namespace WSDOT_API_Project.HTTP
{
    public class WSDOTStore:IWSDOTStore
    {
        private readonly AppSettings _appSettings;
        public WSDOTStore(IOptions<AppSettings> appSettings) 
        {
            _appSettings = appSettings?.Value ?? throw new ArgumentException(nameof(appSettings));
        }

        public async Task<List<PassInfo>> CallAPI(string accessCode)
        {
            // how to call rest API
            //https://stackoverflow.com/questions/22627296/how-to-call-rest-api-from-a-console-application/22627481
            //https://docs.microsoft.com/en-us/aspnet/web-api/overview/advanced/calling-a-web-api-from-a-net-client
            // best method
            //https://blog.jayway.com/2012/03/13/httpclient-makes-get-and-post-very-simple/
            try
            {
                // calls url builder utility which builds the URL to send via http client

                var url = URLBuilder.BuildURL(accessCode);
                HttpClient client = new HttpClient();
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string content = await response.Content.ReadAsStringAsync();
                //Console.WriteLine(content);
                // this works!!!
                List<PassInfo> pass = JsonConvert.DeserializeObject<List<PassInfo>>(content);

                // how to order model object in-place (which is more efficient since you don't have to
                // create a whole new object): https://stackoverflow.com/questions/3309188/how-to-sort-a-listt-by-a-property-in-the-object

                pass.Sort((x, y) => x.MountainPassId.CompareTo(y.MountainPassId));
                return pass;
            }

            // catch block 
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException(ex.ToString());
            }
        }

        public void FindPass(List<PassInfo> passInfo, string selection)
        {
            var checkSelection = Utilities.Utilities.ToInt(selection);
            if (checkSelection == true)
            {
                // TODO: REFACTOR THIS PART, MAYBE PUT THE SEARCH AND RETRIEVE FUNCTION INTO A SEPERATE METHOD
                var search = passInfo.Find(x => x.MountainPassId == Convert.ToInt32(selection));
                //return search;
            }
            //return 0;
        }
    }
}
