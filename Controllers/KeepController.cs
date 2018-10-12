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
 public class KeepController : Controller
 {
  KeepRepository _repo;
  public KeepController(KeepRepository repo)
  {
   _repo = repo;
  }

  [HttpGet]
  public IEnumerable<Keep> GetAll()
  {
   return _repo.GetPublicKeeps();
  }
  [Authorize]
  [HttpGet("myKeeps")]
  public IEnumerable<Keep> Get()
  {
   var userId = HttpContext.User.Identity.Name;
   return _repo.GetByUserId(userId);
  }
  [Authorize]
  [HttpPost]
  public Keep Post([FromBody] Keep keep)
  {
   if (ModelState.IsValid)
   {
    keep.UserId = HttpContext.User.Identity.Name;
    return _repo.Create(keep);
   }
   throw new Exception("Invalid Keep");
  }
 }
}