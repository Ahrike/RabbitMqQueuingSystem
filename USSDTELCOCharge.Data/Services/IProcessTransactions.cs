using System.Collections.Generic;
using System.Threading.Tasks;
using USSDTELCOCharge.Data.DataTransferObjects;

namespace USSDTELCOCharge.Data.Services
{
    public interface IProcessTransactions
    {
        Task<bool> ProcessTransaction(); //(IEnumerable<UssdTransaction> customerDetails);
    }
}