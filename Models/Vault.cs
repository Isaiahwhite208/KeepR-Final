namespace keepr.Models
{
 public class Vault
 {
  public Vault(string name, string description)
  {
   Name = name;
   Description = description;
  }
  public Vault() { }
  public int Id { get; set; }
  public string Name { get; set; }
  public string Description { get; set; }
  public string UserId { get; set; }
 }
}