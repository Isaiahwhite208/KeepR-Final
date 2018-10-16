using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
 public class KeepRepository
 {
  private IDbConnection _db;

  public KeepRepository(IDbConnection db)
  {
   _db = db;
  }
  internal IEnumerable<Keep> GetPublicKeeps()
  {
   return _db.Query<Keep>(@"
        SELECT * FROM keeps WHERE isPrivate = 0;
      ");
  }
  public IEnumerable<Keep> GetByUserId(string userId)
  {
   return _db.Query<Keep>(@"
        SELECT * FROM keeps WHERE userId = @userId;
      ", new { userId });
  }
  public Keep Create(Keep keep)
  {
   int id = _db.ExecuteScalar<int>(@"
        INSERT INTO keeps (name, img, description, isPrivate, userId)
        VALUES (@Name, @Img, @Description, @IsPrivate, @UserId);
        SELECT LAST_INSERT_ID();", keep
   );
   keep.Id = id;
   return keep;
  }
  public Keep Update(Keep keep)
  {
   _db.Execute(@"
      UPDATE keeps SET 
        name = @Name, 
        img = @Img, 
        description = @Description, 
        isPrivate = @IsPrivate, 
        views = @views
        WHERE id = @Id
      ", keep);
   return keep;
  }
  public void Delete(int Id)
  {
   _db.Execute("DELETE FROM keeps WHERE id = @Id", new { Id });
   return;
  }
 }
}