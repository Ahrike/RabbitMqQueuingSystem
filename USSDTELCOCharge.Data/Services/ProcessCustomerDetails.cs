using DataAccessor;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using USSDTELCOCharge.Data.DataTransferObjects;
using USSDTELCOCharge.Service;

namespace USSDTELCOCharge.Data.Services
{
    public class ProcessCustomerDetails : IProcessCustomerDetails
    {
        private readonly IOptions<ConnectionStrings> _conn;
        private readonly IOptions<USSDChargeConfigurations> _ussd;

        private readonly IDbInterfacing _db;

        private readonly ILogger<UssdTelCoChargeProcessor> _logger;
        public ProcessCustomerDetails( IOptions<ConnectionStrings> conn,  ILogger<UssdTelCoChargeProcessor> logger)
        {
            _conn = conn;
            _logger = logger;
        }

     


        public List<UssdGroupTransaction> dbSelectTransactionFromDBToQueue()
        {
            List<UssdGroupTransaction> ussdGroupTransaction = new List<UssdGroupTransaction>();

            var selectQuery = _ussd.Value.SelectQueryFromDBToQueue;
            try
            {
                using (SqlConnection conn = new SqlConnection(_conn.Value.purchasesconn))
                {
                    using (SqlCommand comm = new SqlCommand(selectQuery, conn))
                    {
                        if (conn.State != ConnectionState.Open)
                        {
                            conn.Open();
                        }

                        string temp = string.Empty;

                        using (SqlDataReader dtadapt = comm.ExecuteReader())
                        {
                            while (dtadapt.Read())
                            {
                                var telcoModel = new UssdGroupTransaction()
                                {
                                    CustomerNumber = (DBNull.Value == dtadapt["CustomerNumber"]) ? "" : dtadapt["CustomerNumber"].ToString(),

                                    TelcoName = (DBNull.Value == dtadapt["TelcoName"]) ? "" : dtadapt["TelcoName"].ToString(),

                                    TelcoTimeStamp = (DBNull.Value == dtadapt["TelcoTimeStamp"]) ? "" : dtadapt["TelcoTimeStamp"].ToString()

                                  
                                };
                                ussdGroupTransaction.Add(telcoModel);
                            }
                        }

                    }
                }
            }

            catch (Exception ex)
            {

            }
            return ussdGroupTransaction;

        }
    }
}




