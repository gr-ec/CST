using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CST
{
  public class ExpiredCategory : ITradeCategory
  {
    public void SetTradeCategory(DateTime refDate, Trade trade)
    {
      if (refDate.Subtract(trade.NextPaymentDate).TotalDays > 30)
      {
        trade.SetCategory("EXPIRED");
      }
    }
  }
}
