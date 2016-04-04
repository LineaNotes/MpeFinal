using System;

namespace MpeFinal.Services
{
  public class DateToLong
  {
    private static readonly DateTime JanFirst1970 = new DateTime(1970, 1, 1);

    public static long getTime()
    {
      return (long)((DateTime.Now.ToUniversalTime() - JanFirst1970).TotalMilliseconds + 0.5);
    }

    public static long getTime(DateTime dateTime)
    {
      return (long)((dateTime.ToUniversalTime() - JanFirst1970).TotalMilliseconds + 0.5);
    }
  }
}