using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace HairSalon.Models
{
  public class Stylist
  {
    public int StylistId { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Phone { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Specialty { get; set; }
    public string Description { get; set; }
    public List<Client> Clients { get; set; }
  }
}