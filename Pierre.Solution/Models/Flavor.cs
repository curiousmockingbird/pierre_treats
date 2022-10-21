using System.Collections.Generic;

namespace PierresBakery.Models 
{
  public class Flavor
  {
    public Flavor()
    {
      this.JoinEntities = new HashSet<TreatFlavor>();
    }
    public int FlavorId { get; set; }
    public string Flavor { get; set; }
    public virtual ApplicationUser User { get; set; }
    public virtual ICollection<TreatFlavor> JoinEntities { get; }
  }
}