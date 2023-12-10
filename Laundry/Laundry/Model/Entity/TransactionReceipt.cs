using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laundry.Model.Entity
{
    public class TransactionReceipt
    {
        public string Header { get; set; }
        public string TransactionInfo { get; set; }
        public string ItemsInfo { get; set; }
        public string SummaryInfo { get; set; }
        public string ClosingInfo { get; set; }
        public string Footer { get; set; }

        public string GenerateReceiptText()
        {
            StringBuilder receiptText = new StringBuilder();

            receiptText.AppendLine(Header);
            receiptText.AppendLine(TransactionInfo);
            receiptText.AppendLine(ItemsInfo);
            receiptText.AppendLine(SummaryInfo);
            receiptText.AppendLine(ClosingInfo);
            receiptText.AppendLine(Footer);

            return receiptText.ToString();
        }
    }
}
