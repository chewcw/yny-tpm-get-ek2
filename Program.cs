using Microsoft.Azure.Devices.Provisioning.Client;
using Microsoft.Azure.Devices.Provisioning.Client.Transport;
using Microsoft.Azure.Devices.Provisioning.Security;
using Microsoft.Azure.Devices.Shared;
using System;

namespace Microsoft.Azure.Devices.Provisioning.Client.Samples
{
    public static class Program
    {
        private const string RegistrationId = "";
        public static int Main(string[] args)
        {
            try
            {
                using (var security = new SecurityProviderTpmHsm(RegistrationId))
                using (var transport = new ProvisioningTransportHandlerHttp())
                {
                    string base64EK = Convert.ToBase64String(security.GetEndorsementKey());
                    Console.WriteLine(base64EK);
                }

            }
            catch (Exception err)
            {
                Console.WriteLine($@"Error: {err}");
            }

            return 0;
        }
    }
}