using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using WSDOT_API_Project.Interfaces;

namespace WSDOT_API_Project
{
    class App
    {
        private readonly AppSettings _appSettings;
        private readonly IConfiguration _configuration;
        private IWSDOTStore _wsdotStore;
        public App(IOptions<AppSettings> appSettings, IConfiguration configuration, IWSDOTStore wsdotStore)
        {
            _appSettings = appSettings?.Value ?? throw new ArgumentException(nameof(appSettings));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _wsdotStore = wsdotStore ?? throw new ArgumentNullException(nameof(wsdotStore));
        }
        public async Task Run()
        {
            //populate PassInfo, RestrictionOne and RestrictionTwo models with Json information pass info
            // convert Json to populate POCO classes using NEWTONSOFT JSON tool. HAVE ALREADY INSTALLED THE NUGET PACKAGE;
            // maybe find way to put accessCode in JSON file and pull the access code from the JSON config file. This is okay for now.
            string accessCode = "f350ce65-496d-4283-8c2d-2b7d2a37b864";
            var populate = await _wsdotStore.CallAPI(accessCode);

            foreach (var pass in populate)
            {
                Console.WriteLine(pass.MountainPassId + ":" + pass.MountainPassName);
            }
            while (true)
            {
                Console.WriteLine("");
                Console.Write("Select pass by inputting its corresponding ID: ");
                var selection = Console.ReadLine();
                var checkSelection = Utilities.Utilities.ToInt(selection);
                if (checkSelection == true)
                {
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
                        // TODO: FIGURE OUT HOW TO PRINT OUT NESTED OBJECTS
                        Console.WriteLine(search.RestrictionOne.RestrictionText);
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
    }
}
        
   
