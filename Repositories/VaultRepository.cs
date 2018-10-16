using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
 public class VaultRepository
 {
  private IDbConnection _db;
  public VaultRepository(IDbConnection db)
  {
   _db = db;
  }
  public IEnumerable<Vault> GetUserVaults(string userId)
  {
   return _db.Query<Vault>(@"
   SELECT * FROM vaults WHERE userId = @userId;
   ", new { userId });
  }
  public Vault Create(Vault vault)
  {
   int id = _db.ExecuteScalar<int>(@"
   INSERT INTO vaults (name, description, userId)
   VALUES (@Name, @Description, @UserId);
   SELECT LAST_INSERT_ID();", vault
   );
   vault.Id = id;
   return vault;
  }
  public void Delete(int Id)
  {
   _db.Execute("DELETE FROM vaults WHERE id = @Id", new { Id });
   return;
  }

  internal void AddKeepToVault(VaultKeep vk)
  {
   _db.Execute(@"
    INSERT INTO vaultkeeps (vaultId, keepId, userId)
    VALUES (@VaultId, @KeepId, @UserId);
    ", vk);
  }

  internal IEnumerable<Keep> GetKeepsByVaultId(int vaultId)
  {
   return _db.Query<Keep>(@"
    SELECT * FROM vaultkeeps vk
    INNER JOIN keeps k ON k.id = vk.keepId 
    WHERE (vaultId = @vaultId) 
    ", new { vaultId });
  }

  internal void RemoveKeepFromVault(VaultKeep vk)
  {
   _db.Execute(@"
    DELETE FROM vaultkeeps WHERE keepId = @KeepId AND vaultId = @VaultId
   ", vk);
  }
 }
}