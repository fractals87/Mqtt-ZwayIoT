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
using MQTTService;
using Newtonsoft.Json;

namespace CoWorkOfficeWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class GatewaysController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly MqttServiceApi _mqtt;

        public GatewaysController(ApplicationDbContext context, MqttServiceApi mqtt)
        {
            _context = context;
            _mqtt = mqtt;
        }

        // GET: api/Gateways
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GatewayDTO>))]
        public async Task<ActionResult<IEnumerable<GatewayDTO>>> GetGateways()
        {
            //return await _context.Gateways.ToListAsync();

            var listGateways = await _context.Gateways.Select(s =>
                   new GatewayDTO
                   {
                       id = s.Id.ToString(),
                       description = s.Description,
                       protocol = s.Protocol,
                       ip = s.IP,
                       port = s.Port,
                       user = s.User,
                       password = s.Password
                   }).ToListAsync();
            return Ok(listGateways);
        }

        //// GET: api/Gateways/Configure
        [HttpGet, Route("Configure")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<ApiMsgDTO> Configure()
        {
            _mqtt.Publish("Configuration/IoTDevice/", JsonConvert.SerializeObject(_context.IoTDevices.ToList()));
            _mqtt.Publish("Configuration/Gateway/", JsonConvert.SerializeObject(_context.Gateways.ToList()));
            return Ok(new ApiMsgDTO() { code = "OK", message = "Configurazione inviata con successo" });
        }
    }
}
