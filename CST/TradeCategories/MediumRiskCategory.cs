using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CST
{
  public class MediumRiskCategory :ITradeCategory
  {
    public void SetTradeCategory(DateTime refDate, Trade trade)
    {
      if (trade.Value > 1000000 && trade.MyClientSectorType() == Trade.ClientSectorTypes.publicSector)
      {
        trade.SetCategory("MEDIUMRISK");
      }
    }
  }
}
