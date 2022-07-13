using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CST
{
  public class HighRiskCategory : ITradeCategory
  {
    public void SetTradeCategory(DateTime refDate, Trade trade)
    {
      if (trade.Value > 1000000 && trade.MyClientSectorType() == Trade.ClientSectorTypes.privateSector)
      {
        trade.SetCategory("HIGHRISK");
      }
    }
  }
}
