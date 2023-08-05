using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Factory.Models
{
  public class Engineer
  {
    public int EngineerId { get; set; }
    [Required(ErrorMessage = "Add values for Name")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Add values for Details")]
    public string Details { get; set; }
    public List<AuthorizationJoin> AuthorizationJoins { get; set; }
  }
}