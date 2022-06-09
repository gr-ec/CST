using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CST
{
  class Program
  {
    /// <summary>
    /// Stores category discovery functions <see cref="Trade"/>.
    /// </summary>
    delegate void TradeCategory(DateTime refDate, Trade trade);
    static TradeCategory CategoryDiscoveryDelegates;

    /// <summary>
    /// Get input lines from user
    /// </summary>
    private static List<string> GetInputLines()
    {
      List<string> lines = new List<string>();

      Console.WriteLine("Paste your input (then press enter): ");

      string line;
      while (!string.IsNullOrEmpty(line = Console.ReadLine()))
      {
        lines.Add(line);
      }

      return lines;
    }

    /// <summary>
    /// Parse trade lines. Returns trades and possible errors (ignore trades if errors).
    /// </summary>
    private static Tuple<IList<Trade>, IList<string>> ParseTrades(List<string> lines)
    {
      var trades = new List<Trade>();
      var errors = new List<string>();

      if (lines.Count() < 3)
      {
        errors.Add("input must have at least three lines");
      }
      if (!DateTime.TryParseExact(lines[0].Trim(), "MM/dd/yyyy", new CultureInfo("en-US"), DateTimeStyles.None, out DateTime refDate))
      {
        errors.Add("Invalid format for reference date! (use format mm/dd/yyyy)");
      }
      if (!int.TryParse(lines[1].Trim(), out int tradesNo) || tradesNo < 1)
      {
        errors.Add("Invalid number of trades! (use integer numbers above 0)");
      }

      foreach (var l in lines.GetRange(2, tradesNo))
      {
        var tradeDataParts = l.Trim().Split(' ');
        if (tradeDataParts.Count() == 3)
        {
          if (double.TryParse(tradeDataParts[0], out double tradeValue))
          {
            if (Trade.ClientSectorType(tradeDataParts[1].Trim()) != Trade.ClientSectorTypes.unknown)
            {
              if (DateTime.TryParseExact(tradeDataParts[2], "MM/dd/yyyy", new CultureInfo("en-US"), DateTimeStyles.None, out DateTime nextPaymentDate))
              {
                var trade = new Trade(tradeValue, tradeDataParts[1].Trim(), nextPaymentDate);
                CategoryDiscoveryDelegates(refDate, trade);
                trades.Add(trade);
              }
              else
              {
                errors.Add("Invalid next payment date");
              }
            }
            else
            {
              errors.Add("Invalid client sector");
            }
          }
          else
          {
            errors.Add("First field must be a double value");
          }
        }
        else
        {
          errors.Add("You must inform three fields for trade fields");
        }
      }

      return new Tuple<IList<Trade>, IList<string>>(trades, errors);
    }

    
    static void Main(string[] args)
    {
      //subscribe all trade functions in order of precedence
      CategoryDiscoveryDelegates += Trade.TradeExpired;
      CategoryDiscoveryDelegates += Trade.TradeHighRisk;
      CategoryDiscoveryDelegates += Trade.TradeMediumRisk;

      //unsubscribe trade functions on demand

      var tradeData = ParseTrades(GetInputLines());

      if (tradeData.Item2.Any())
      {
        foreach(var e in tradeData.Item2.Distinct().ToList())
        {
          Console.WriteLine(e);
        }
      }
      else
      {
        foreach (var t in tradeData.Item1)
        {
          t.PrintCategory();
        }
      }

      Console.WriteLine();
      Console.WriteLine("Press any key to exit");
      Console.ReadLine();
    }
  }
}
