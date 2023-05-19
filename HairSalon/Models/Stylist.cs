using System.Collections.Generic;

namespace HairSalon.Models
{
  public class Stylist
  {
    public int StylistId { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Specialty { get; set; }
    public string Description { get; set; }
    public List<Client> Clients { get; set; }
  }
}