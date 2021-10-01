
using ExcelDataReader;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using USSDTELCOCharge.Data.Helpers;
using USSDTELCOCharge.Service;

namespace USSDTELCOCharge.Data.Services
{
    public class UssdTelCoChargeProcessor : IUssdTelCoChargeProcessor
    {
        private readonly ILogger<UssdTelCoChargeProcessor> _logger;
        private readonly IProcessTransactions _processTransactions;

        public UssdTelCoChargeProcessor( ILogger<UssdTelCoChargeProcessor> logger)
        {
           
            _logger = logger;
        }
      


        public async Task UssdProcessor()
        {
            try
            {
               
                await _processTransactions.ProcessTransaction();
            }
            catch (Exception ex)
            {

                _logger.LogInformation(ex.Message, "UssdProcessor");
                _logger.LogDebug(ex.Message, "UssdProcessor");
            }

        }


    }
}
