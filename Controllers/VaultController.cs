using System;
using System.Collections.Generic;
using keepr.Models;
using keepr.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{
 [Route("api/[controller]")]
 [ApiController]
 public class VaultsController : Controller
 {
  VaultRepository _repo;
  public VaultsController(VaultRepository repo)
  {
   _repo = repo;
  }
  [Authorize]
  [HttpGet("myVaults")]
  public IEnumerable<Vault> Get()
  {
   var userId = HttpContext.User.Identity.Name;
   return _repo.GetUserVaults(userId);
  }
  [Authorize]
  [HttpPost]
  public Vault Post([FromBody] Vault vault)
  {
   if (ModelState.IsValid)
   {
    vault.UserId = HttpContext.User.Identity.Name;
    return _repo.Create(vault);
   }
   throw new Exception("Invalid Vault");
  }
  [Authorize]
  [HttpDelete("{vaultId}")]
  public void Delete(int vaultId)
  {
   _repo.Delete(vaultId);
   return;
  }


  [Authorize]
  [HttpPost("keeps")]
  public void AddKeepToVault([FromBody]VaultKeep vk)
  {
   vk.UserId = HttpContext.User.Identity.Name;
   _repo.AddKeepToVault(vk);
   return;
  }

  [Authorize]
  [HttpGet("keeps/{id}")]
  public IEnumerable<Keep> GetKeepsByVaultId(int id)
  {
   return _repo.GetKeepsByVaultId(id);
  }
  [Authorize]
  [HttpPut("keeps")]
  public void RemoveKeepFromVault([FromBody]VaultKeep vk)
  {
   _repo.RemoveKeepFromVault(vk);
   return;
  }
 }
}