namespace keepr.Models
{

 public class VaultKeep
 {
  public int KeepId { get; set; }
  public int VaultId { get; set; }
  public string UserId { get; set; }
 }
 public class Keep
 {
  public Keep(string name, string img, string description, bool isPrivate)
  {
   Name = name;
   Img = img;
   Description = description;
   IsPrivate = isPrivate;
  }

  public Keep() { }
  public int Id { get; set; }
  public string Name { get; set; }
  public string Description { get; set; }
  public string UserId { get; set; }
  public string Img { get; set; }
  public bool IsPrivate { get; set; }

 }
}