using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CST
{
  class Trade : ITrade
  {
    public Trade(double value, string clientSector, DateTime nextPaymentDate)
    {
      Value = value;
      ClientSector = clientSector;
      NextPaymentDate = nextPaymentDate;
    }

    /// <summary>
    /// Enum created to facilitate comparisons.
    /// </summary>
    public enum ClientSectorTypes
    {
      privateSector,
      publicSector,
      unknown
    }

    public double Value { get; private set; }
    public string ClientSector { get; private set; }
    public DateTime NextPaymentDate { get; private set; }
    public string Category { get; private set; }

    /// <summary>
    /// Category console print if category is not null or empty.
    /// </summary>
    public void PrintCategory()
    {
      Console.WriteLine(string.IsNullOrEmpty(this.Category) ? "Unknown category" : this.Category);
    }

    /// <summary>
    /// Set trade's category.
    /// </summary>
    private void SetCategory(string category)
    {
      if (string.IsNullOrEmpty(this.Category))
      {
        this.Category = category;
      }
    }

    /// <summary>
    /// Returns client sector type of the received sector.
    /// </summary>
    public static ClientSectorTypes ClientSectorType(string sector)
    {
      return
        sector == "Private" ?
          ClientSectorTypes.privateSector :
          sector == "Public" ?
            ClientSectorTypes.publicSector :
            ClientSectorTypes.unknown;
    }

    /// <summary>
    /// Returns client sector type of this trade.
    /// </summary>
    public ClientSectorTypes MyClientSectorType()
    {
      return ClientSectorType(this.ClientSector);
    }

    #region category discovery delegates
    public static void TradeExpired(DateTime refDate, Trade trade)
    {
      if (refDate.Subtract(trade.NextPaymentDate).TotalDays > 30)
      {
        trade.SetCategory("EXPIRED");
      }
    }
    public static void TradeHighRisk(DateTime refDate, Trade trade)
    {
      if (trade.Value > 1000000 && trade.MyClientSectorType() == ClientSectorTypes.privateSector)
      {
        trade.SetCategory("HIGHRISK");
      }
    }
    public static void TradeMediumRisk(DateTime refDate, Trade trade)
    {
      if (trade.Value > 1000000 && trade.MyClientSectorType() == ClientSectorTypes.publicSector)
      {
        trade.SetCategory("MEDIUMRISK");
      }
    }
    #endregion
  }
}
