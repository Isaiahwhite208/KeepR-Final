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
 public class VaultController : Controller
 {
  VaultRepository _repo;
  public VaultController(VaultRepository repo)
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
   throw new Exception("INVALID VAULT");
  }

 }
}