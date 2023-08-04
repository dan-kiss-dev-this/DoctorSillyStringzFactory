using System.Collections.Generic;

namespace Factory.Models
{
  public class Machine
  {
    public int MachineId { get; set; }
    public string Title { get; set; }
    public List<AuthorizationJoin> AuthorizationJoins { get; set; }
  }
}