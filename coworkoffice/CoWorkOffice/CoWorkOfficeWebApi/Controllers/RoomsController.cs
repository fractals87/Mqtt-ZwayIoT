using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoWorkOffice.Data;
using CoWorkOfficeModel.Models;
using Microsoft.AspNetCore.Authorization;
using CoWorkOfficeModel.Models.DTO;

namespace CoWorkOfficeWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,User")]
    public class RoomsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RoomsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Rooms
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OfficeDTO>))]
        public async Task<ActionResult<IEnumerable<OfficeDTO>>> GetRooms()
        {
            var listRoom = await _context.Rooms.OfType<Office>().Select(s =>
               new OfficeDTO
               {
                   Id = s.Id.ToString(),
                   Description = s.Description
               }).ToListAsync();
            return Ok(listRoom);
        }

    }
}
