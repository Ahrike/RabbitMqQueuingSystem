using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using USSDTELCOCharge.Data.DataTransferObjects;

namespace USSDTELCOCharge.Data.Services
{
    public interface IProcessCustomerDetails
    {
     
        public List<UssdGroupTransaction> dbSelectTransactionFromDBToQueue();
       
    }
}