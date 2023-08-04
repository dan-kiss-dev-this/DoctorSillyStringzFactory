using System.Collections.Generic;

namespace Factory.Models
{
  public class AuthorizationJoin
  {
    public int AuthorizationJoinId { get; set; }
    public int EngineerId { get; set; }
    public int MachineId { get; set; }

    // from EF Core  
    public Engineer Engineer { get; set; }
    public Machine Machine { get; set; }

  }
}