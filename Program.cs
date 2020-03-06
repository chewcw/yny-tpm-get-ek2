using Microsoft.Azure.Devices.Provisioning.Client;
using Microsoft.Azure.Devices.Provisioning.Client.Transport;
using Microsoft.Azure.Devices.Provisioning.Security;
using Microsoft.Azure.Devices.Shared;
using System;
using McMaster.Extensions.CommandLineUtils;

namespace Microsoft.Azure.Devices.Provisioning.Client.Samples
{
    [Subcommand(typeof(GetTPM))]
    [HelpOption]
    class Program
    {
        public static void Main(string[] args)
        {
            CommandLineApplication.Execute<Program>(args);
        }

        private int OnExecute()
        {
            Console.Error.WriteLine("No command passed to the command line. Use --help option to check what arguments available.");
            return -1;
        }
    }

    [HelpOption]
    class GetTPM
    {
        [Option("-r", LongName = "Registration", Description = "Regisration ID")]
        private string RegistrationId { get; set; }

        public int OnExecute()
        {
            try
            {
                if (RegistrationId == "" || RegistrationId == null)
                {
                    RegistrationId = Prompt.GetString("Please type the RegistrationID: ", null, ConsoleColor.Red, ConsoleColor.Black);
                }


                using (var security = new SecurityProviderTpmHsm(RegistrationId))
                {
                    string base64EK = Convert.ToBase64String(security.GetEndorsementKey());
                    Console.WriteLine(base64EK);
                }

                return 0;

            }
            catch (Exception err)
            {
                Console.WriteLine($@"Error: {err}");
                return -1;
            }
        }
    }
}
