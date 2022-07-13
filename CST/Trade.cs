using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CST
{
  public class Trade : ITrade
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
    public void SetCategory(string category)
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
  }
}
