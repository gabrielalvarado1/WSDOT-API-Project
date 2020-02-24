using System;
using System.Net;
using Newtonsoft.Json;
using System.Web;
using WSDOT_API_Project.Utilities;
using System.Net.Http;
using System.Threading.Tasks;
using WSDOT_API_Project.Models;
using System.Collections.Generic;
using System.ComponentModel;

namespace WSDOT_API_Project
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Program prog = new Program();
            //populate PassInfo, RestrictionOne and RestrictionTwo models with Json information pass info
            // convert Json to populate POCO classes using NEWTONSOFT JSON tool. HAVE ALREADY INSTALLED THE NUGET PACKAGE;
            // maybe find way to put accessCode in JSON file and pull the access code from the JSON config file. This is okay for now.
            string accessCode = "f350ce65-496d-4283-8c2d-2b7d2a37b864";
            var populate = await prog.CallAPI(accessCode);

            foreach (var pass in populate)
            {
                Console.WriteLine(pass.MountainPassId + ":" + pass.MountainPassName);
            }
            while (true)
            {
                Console.WriteLine("");
                Console.Write("Select pass by inputting its corresponding ID: ");
                var selection = Console.ReadLine();
                //prog.FindPass(populate, selection);
                var checkSelection = Utilities.Utilities.ToInt(selection);
                if (checkSelection == true)
                {
                    // TODO: REFACTOR THIS PART, MAYBE PUT THE SEARCH AND RETRIEVE FUNCTION INTO A SEPERATE METHOD
                    var search = populate.Find(x => x.MountainPassId == Convert.ToInt32(selection));
                    if (search != null)
                    {
                        Console.WriteLine("");
                        Console.WriteLine("Information for {0}", search.MountainPassName);

                        // how to print each property in object: https://stackoverflow.com/questions/31770508/how-to-print-class-properties-on-the-console

                        foreach (PropertyDescriptor item in TypeDescriptor.GetProperties(search))
                        {
                            string name = item.Name;
                            object value = item.GetValue(search);
                            Console.WriteLine("{0} : {1}", name, value);
                            Console.WriteLine("");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Entered mountain pass ID not found. Try again");
                    }
                }
                else
                {
                    Console.WriteLine("Please enter a valid mountain pass ID. Try again");
                }
                Console.Write("Retrieve another pass's information? (y/n)");
                if (Console.ReadLine().ToLower() == "n") 
                {
                    break;
                }
            }
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

        /*public void DisplayPassInfo(var search) 
        {
            Console.WriteLine("");
            Console.WriteLine("Information for {0}", search.MountainPassName);

            // how to print each property in object: https://stackoverflow.com/questions/31770508/how-to-print-class-properties-on-the-console

            foreach (PropertyDescriptor item in TypeDescriptor.GetProperties(search))
            {
                string name = item.Name;
                object value = item.GetValue(search);
                Console.WriteLine("{0} : {1}", name, value);
                Console.WriteLine("");
            }
        }*/
    }
}
