using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace HairSalon.Models
{
  public class Client
  {
    public int ClientId { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Phone { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }
    public string Description { get; set; }
    public int StylistId { get; set; }
    public Stylist Stylist { get; set; }
  }
}