using Microsoft.Extensions.Configuration;
using System.IO;

namespace USSDTELCOCharge.Service
{
    public class USSDChargeConfigurations
    {

        public string SelectQueryFromDBToQueue { get; set; }
        public string RabbitUserName { get; set; }
        public string RabbitPassword { get; set; }

        public string RabbitServer { get; set; }

        public string queuename { get; set; }
    }
    public class ConnectionStrings  
    {
        public string purchasesconn { get; set; }
    }
   
}
