using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Factory.Models
{
  public class Machine
  {
    public int MachineId { get; set; }
    [Required(ErrorMessage = "Add values for Title")]
    public string Title { get; set; }
    [Required(ErrorMessage = "Add values for Details")]
    public string Details { get; set; }
    public List<AuthorizationJoin> AuthorizationJoins { get; set; }
  }
}