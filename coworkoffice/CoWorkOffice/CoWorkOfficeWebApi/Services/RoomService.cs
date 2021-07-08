using CoWorkOffice.Data;
using CoWorkOfficeModel.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoWorkOfficeWebApi.Services
{
    public class RoomService
    {
        private readonly ApplicationDbContext _context;

        public RoomService(ApplicationDbContext context)
        {
            _context = context;

        }

        public bool IsBusy(Room room, DateTime dateTime )
        {
            if (room is WaitingRoom)
            {
                return _context.Reservations.Where(f => f.NCustomerExpected > 0 && dateTime > f.DateTimeStart && dateTime < f.DateTimeEnd ).Any();
            }
            else
            {
                return _context.Reservations.Where(f => f.OfficeId == room.Id && dateTime > f.DateTimeStart && dateTime < f.DateTimeEnd).Any();
            }
        }

        public bool IsOfficeBusy(int id, DateTime dateTimeStart, DateTime dateTimeEnd)
        {
            return _context.Reservations.Where(f => ( (f.DateTimeStart <= dateTimeStart && f.DateTimeEnd >= dateTimeStart) || (f.DateTimeStart <= dateTimeEnd && f.DateTimeEnd >= dateTimeEnd)) && f.OfficeId == id).Any();
        }

        public bool IsWaitingRoomFreeForNewRes(int id, int nCustomerExpected, DateTime dateTimeStart, DateTime dateTimeEnd, int? idReservationExluded)
        {
            var room = _context.Rooms.Where(f => f.Id == id).FirstOrDefault();
            if (room is Office office)
            {
                var waitingRoom = (WaitingRoom)(_context.Rooms.Where(f => f.Id == office.WaitingRoomId).First());
                var nCustomer = _context.Reservations.Where(f => ((f.DateTimeStart <= dateTimeStart && f.DateTimeEnd >= dateTimeStart) || (f.DateTimeStart <= dateTimeEnd && f.DateTimeEnd >= dateTimeEnd)) && f.Office.WaitingRoomId == waitingRoom.Id).Sum(f => f.NCustomerExpected);
                if (idReservationExluded != null)
                {
                    var res = _context.Reservations.Where(f => f.Id == idReservationExluded).AsNoTracking().FirstOrDefault();
                    nCustomer -= res.NCustomerExpected;
                }
                if (nCustomer + nCustomerExpected < waitingRoom.NMaxCustomer)
                    return true;
            }
            return false;
        }
    }
}
