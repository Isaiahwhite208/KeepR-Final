using System.Data;

namespace keepr.Repositories
{
 public class VaultRepository
 {
  private IDbConnection _db;
  public VaultRepository(IDbConnection db)
  {
   _db = db;
  }
 }
}