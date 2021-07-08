using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoWorkOffice.Data;
using CoWorkOfficeModel.Models;
using CoWorkOfficeModel.Models.DTO;
using CoWorkOfficeWebApi.Services;
using Microsoft.AspNetCore.Authorization;

namespace CoWorkOfficeWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,User")]
    public class ReservationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly RoomService _roomService;

        public ReservationsController(ApplicationDbContext context, RoomService roomService)
        {
            _context = context;
            _roomService = roomService;
        }

        // GET: api/Reservations
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ReservationDTO>))]
        public async Task<ActionResult<IEnumerable<ReservationDTO>>> GetReservations()
        {
            var user = await _context.Users.Where(f => f.Email == User.Identity.Name).FirstOrDefaultAsync();
            return Ok(await _context.Reservations
                    .Include(f=>f.Office)
                    .Where(f=>f.UserId == user.Id)
                    .Select(f=> new ReservationDTO()
                    {
                        id = f.Id,
                        da = f.DateTimeStart.ToString("dd/MM/yyyy HH:mm"),
                        a = f.DateTimeEnd.ToString("dd/MM/yyyy HH:mm"),
                        room = f.Office.Description,
                        clienti = f.NCustomerExpected
                    })
                    .ToListAsync());
        }

        // GET: api/Reservations/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReservationDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiMsgDTO))]
        public async Task<ActionResult<ReservationDTO>> GetReservation(int id)
        {

            var res = await _context.Reservations
                    .Include(f => f.Office)
                    .Where(f=>f.Id == id)
                    .Select(f => new ReservationDTO()
                    {
                        id = f.Id,
                        da = f.DateTimeStart.ToString("dd/MM/yyyy HH:mm"),
                        a = f.DateTimeEnd.ToString("dd/MM/yyyy HH:mm"),
                        idroom = f.OfficeId,
                        clienti = f.NCustomerExpected
                    })
                    .FirstOrDefaultAsync();
            if (res != null)
                return Ok(res);
            else
                return BadRequest(new ApiMsgDTO() { code = "KO", message = "Prenotazione non trovata" });
        }

        // PUT: api/Reservations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiMsgDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiMsgDTO))]
        public async Task<ActionResult<ApiMsgDTO>> PutReservation(ReservationDTO resDTO)
        {
            var reservation = new Reservation() { };
            try
            {
                var user = await _context.Users.Where(f => f.Email == User.Identity.Name).FirstOrDefaultAsync();
                reservation = new Reservation()
                {
                    DateTimeStart = Convert.ToDateTime(resDTO.da),
                    DateTimeEnd = Convert.ToDateTime(resDTO.a),
                    NCustomerExpected = resDTO.clienti,
                    OfficeId = resDTO.idroom,
                    UserId = user.Id, 
                    Id = resDTO.id
                };

                if (reservation.OfficeId == 0)
                    return BadRequest(new ApiMsgDTO() { code = "KO", message = "Ufficio non selezionato" });

                if (!_roomService.IsWaitingRoomFreeForNewRes(reservation.OfficeId, reservation.NCustomerExpected, reservation.DateTimeStart, reservation.DateTimeEnd, reservation.Id))
                    return BadRequest(new ApiMsgDTO() { code = "KO", message = "Sala d'attesa piena nella fascia indicata, ridurre il numero di clienti o scelgliere altra fascia" });

                _context.Entry(reservation).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiMsgDTO() { code = "KO", message = ex.Message });
            }
            return Ok(new ApiMsgDTO() { code = "OK", message = "Modifica avvenuta con successo" });

        }

        // POST: api/Reservations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiMsgDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiMsgDTO))]
        public async Task<ActionResult<ApiMsgDTO>> PostReservation(ReservationDTO resDTO)
        {
            var reservation = new Reservation() { };
            try
            {   var user = await _context.Users.Where(f => f.Email == User.Identity.Name).FirstOrDefaultAsync();
                reservation = new Reservation()
                {
                    DateTimeStart = Convert.ToDateTime(resDTO.da),
                    DateTimeEnd = Convert.ToDateTime(resDTO.a),
                    NCustomerExpected = resDTO.clienti,
                    OfficeId = resDTO.idroom,
                    UserId = user.Id
                };

                if(reservation.OfficeId == 0)
                    return BadRequest(new ApiMsgDTO() { code = "KO", message = "Ufficio non selezionato" });

                if (_roomService.IsOfficeBusy(reservation.OfficeId, reservation.DateTimeStart, reservation.DateTimeEnd))
                    return BadRequest(new ApiMsgDTO() { code = "KO", message = "Ufficio scelto è occupato nella fascia indicata" });

                if(!_roomService.IsWaitingRoomFreeForNewRes(reservation.OfficeId, reservation.NCustomerExpected, reservation.DateTimeStart, reservation.DateTimeEnd,null))
                    return BadRequest(new ApiMsgDTO() { code = "KO", message = "Sala d'attesa piena nella fascia indicata, ridurre il numero di clienti o scelgliere altra fascia" });

                _context.Reservations.Add(reservation);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiMsgDTO() { code = "KO", message = ex.Message });
            }
            return Ok(new ApiMsgDTO() { code = reservation.Id.ToString(), message = "Inserimento avvenuto con successo" });
        }

        // DELETE: api/Reservations/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiMsgDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiMsgDTO))]
        public async Task<ActionResult<ApiMsgDTO>> DeleteReservation(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return BadRequest(new ApiMsgDTO() { code = "KO", message = "Prenotazione non trovata" });
            }

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();

            return Ok(new ApiMsgDTO() { code = "OK", message = "Cancellazione avvenuta con successo" });
        }

    }
}
