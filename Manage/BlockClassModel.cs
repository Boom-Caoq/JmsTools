using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jmsTools.Manage
{
    public class BlockchainData
    {
        public decimal Balance { get; set; }
        public int TransactionCount { get; set; }
        public List<TransactionRecord> Transactions { get; set; }
    }

    public class TransactionRecord
    {
        public string TxHash { get; set; }
        public DateTime Timestamp { get; set; }
        public decimal Value { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }
}
