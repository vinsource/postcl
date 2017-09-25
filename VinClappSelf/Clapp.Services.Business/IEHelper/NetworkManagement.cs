using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Clapp.Services.Business.Model;

namespace Clapp.Services.Business.IEHelper
{
    public class NetworkManagement
    {
        /// <summary>
        /// Set's a new IP Address and it's Submask of the local machine
        /// </summary>
        /// <param name="ip_address">The IP Address</param>
        /// <param name="subnet_mask">The Submask IP Address</param>
        /// <remarks>Requires a reference to the System.Management namespace</remarks>
        public void SetIP(CraigsListEmailAccount account)
        {
            using (var networkConfigMng = new ManagementClass("Win32_NetworkAdapterConfiguration"))
            {
                using (var networkConfigs = networkConfigMng.GetInstances())
                {
                    foreach (
                        var managementObject in
                            networkConfigs.Cast<ManagementObject>()
                                          .Where(managementObject => (bool) managementObject["IPEnabled"]))
                    {
                        using (var newIP = managementObject.GetMethodParameters("EnableStatic"))
                        {
                            // Set new IP address and subnet if needed

                            if (!String.IsNullOrEmpty(account.Proxy))
                            {
                                newIP["IPAddress"] = new[] {account.Proxy};

                                newIP["SubnetMask"] = new[] {account.Dns};



                            }



                            managementObject.InvokeMethod("EnableStatic", newIP, null);


                            // Set mew gateway if needed

                            using (var newGateway = managementObject.GetMethodParameters("SetGateways"))
                            {
                                newGateway["DefaultIPGateway"] = new[] {account.DefaultGateWay};
                                newGateway["GatewayCostMetric"] = new[] {1};
                                managementObject.InvokeMethod("SetGateways", newGateway, null);
                            }

                        }
                    }
                }
            }
        }

        public void ListIP()
        {
            ManagementClass objMC = new ManagementClass(
                "Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection objMOC = objMC.GetInstances();

            foreach (ManagementObject objMO in objMOC)
            {
                if (!(bool) objMO["ipEnabled"])
                    continue;



                //Console.WriteLine(objMO["Caption"] + "," +
                //                  objMO["ServiceName"] + "," + objMO["MACAddress"]);
                string[] ipaddresses = (string[]) objMO["IPAddress"];
                string[] subnets = (string[]) objMO["IPSubnet"];
                string[] gateways = (string[]) objMO["DefaultIPGateway"];


                //Console.WriteLine("Printing Default Gateway Info:");
                //Console.WriteLine(objMO["DefaultIPGateway"].ToString());

                //Console.WriteLine("Printing IPGateway Info:");
                //foreach (string sGate in gateways)
                //    Console.WriteLine(sGate);


                Console.WriteLine("Printing Ipaddress Info:");

                //foreach (string sIP in ipaddresses)


                Console.WriteLine("Printing SubNet Info:");

                foreach (string sNet in subnets)
                    Console.WriteLine(sNet);
            }
        }


        /// <summary>
        /// Set's a new Gateway address of the local machine
        /// </summary>
        /// <param name="gateway">The Gateway IP Address</param>
        /// <remarks>Requires a reference to the System.Management namespace</remarks>
        public void setGateway(string gateway)
        {
            ManagementClass objMC = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection objMOC = objMC.GetInstances();

            foreach (ManagementObject objMO in objMOC)
            {
                if ((bool) objMO["IPEnabled"])
                {
                    try
                    {
                        ManagementBaseObject setGateway;
                        ManagementBaseObject newGateway =
                            objMO.GetMethodParameters("SetGateways");

                        newGateway["DefaultIPGateway"] = new string[] {gateway};
                        newGateway["GatewayCostMetric"] = new int[] {1};

                        setGateway = objMO.InvokeMethod("SetGateways", newGateway, null);
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// Set's the DNS Server of the local machine
        /// </summary>
        /// <param name="NIC">NIC address</param>
        /// <param name="DNS">DNS server address</param>
        /// <remarks>Requires a reference to the System.Management namespace</remarks>
        public void setDNS(string NIC, string DNS)
        {
            ManagementClass objMC = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection objMOC = objMC.GetInstances();

            foreach (ManagementObject objMO in objMOC)
            {
                if ((bool) objMO["IPEnabled"])
                {
                    // if you are using the System.Net.NetworkInformation.NetworkInterface you'll need to change this line to if (objMO["Caption"].ToString().Contains(NIC)) and pass in the Description property instead of the name 
                    if (objMO["Caption"].Equals(NIC))
                    {
                        try
                        {
                            ManagementBaseObject newDNS =
                                objMO.GetMethodParameters("SetDNSServerSearchOrder");
                            newDNS["DNSServerSearchOrder"] = DNS.Split(',');
                            ManagementBaseObject setDNS =
                                objMO.InvokeMethod("SetDNSServerSearchOrder", newDNS, null);
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Set's WINS of the local machine
        /// </summary>
        /// <param name="NIC">NIC Address</param>
        /// <param name="priWINS">Primary WINS server address</param>
        /// <param name="secWINS">Secondary WINS server address</param>
        /// <remarks>Requires a reference to the System.Management namespace</remarks>
        public void setWINS(string NIC, string priWINS, string secWINS)
        {
            ManagementClass objMC = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection objMOC = objMC.GetInstances();

            foreach (ManagementObject objMO in objMOC)
            {
                if ((bool) objMO["IPEnabled"])
                {
                    if (objMO["Caption"].Equals(NIC))
                    {
                        try
                        {
                            ManagementBaseObject setWINS;
                            ManagementBaseObject wins =
                                objMO.GetMethodParameters("SetWINSServer");
                            wins.SetPropertyValue("WINSPrimaryServer", priWINS);
                            wins.SetPropertyValue("WINSSecondaryServer", secWINS);

                            setWINS = objMO.InvokeMethod("SetWINSServer", wins, null);
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                }
            }
        }


        public static string GetIpAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            return
                (from ip in host.AddressList where ip.AddressFamily == AddressFamily.InterNetwork select ip.ToString())
                    .First();
        }

    }
}
