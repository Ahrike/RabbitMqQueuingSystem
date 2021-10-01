using Dapper;
using DataAccessor;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using USSDTELCOCharge.Data.DataTransferObjects;
using USSDTELCOCharge.Service;

namespace USSDTELCOCharge.Data.Services
{
    public class ProcessTransactions : IProcessTransactions
    {
       

        private readonly IProcessCustomerDetails _process;

        private readonly IDbInterfacing _db;

        private readonly IOptions<ConnectionStrings> _conn;

        private readonly IOptions<USSDChargeConfigurations> _ussd;
        //USSDChargeConfigurations
        private readonly ILogger<UssdTelCoChargeProcessor> _logger;

        //public ProcessTransactions( IProcessCustomerDetails process, IDbInterfacing db, IOptions<ConnectionStrings> conn, ILogger<UssdTelCoChargeProcessor> logger, IOptions<USSDChargeConfigurations> ussd)
        //{
           
        //    _process = process;

        //    _db = db;

        //    _conn = conn;

        //    _logger = logger;

        //    _ussd = ussd;
        //}



        public async Task<bool> ProcessTransaction()    
        {
             bool success = false;

             string RabbitServer = _ussd.Value.RabbitServer;

             string RabbitUserName = _ussd.Value.RabbitUserName;

             string RabbitPassword = _ussd.Value.RabbitPassword;

             string queuename = _ussd.Value.queuename;

             int PublishedCounter;

             int RejectedCounter;


            ConnectionFactory factoryConnect = new ConnectionFactory()
            {
                HostName = RabbitServer,

                UserName = RabbitUserName,

                Password = RabbitPassword,

                AutomaticRecoveryEnabled = true,

                NetworkRecoveryInterval = TimeSpan.FromSeconds(10),

                TopologyRecoveryEnabled = true,

                UseBackgroundThreadsForIO = true,
                DispatchConsumersAsync = true
             };

            using var connection = factoryConnect.CreateConnection();

            using var channel = connection.CreateModel();

            channel.QueueDeclare(queuename,

                durable: true,

                exclusive: false,

                autoDelete: false,

                arguments: null);

            List<UssdGroupTransaction> ussdGroupTransactionsFromDb = _process.dbSelectTransactionFromDBToQueue();

            if (ussdGroupTransactionsFromDb.Count > 0)
            {
                string jsonString = string.Empty;

                foreach (var item in ussdGroupTransactionsFromDb)
                {
                    jsonString = JsonSerializer.Serialize(item);
                    channel.BasicPublish(string.Empty, queuename, null, Encoding.UTF8.GetBytes(jsonString));

                    DynamicParameters paras = new();

                    paras.Add("@customernumber", item.CustomerNumber);

                    paras.Add("@tranxstate", "ON QUEUE");

                    var result = await _db.ModifyDB(_conn.Value.purchasesconn, "UpdateTelcoChargeDetailsQueueDB", CommandType.StoredProcedure, paras);
                    if (result.Success) success = result.Success;
                }

            }




            return success;
        }
    

    }
}




