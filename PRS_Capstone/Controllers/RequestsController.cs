using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRS_Capstone.Models;

namespace PRS_Capstone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly PRS_CapstoneDbContext _context;
        
        [HttpPut("review")]
        public async Task<IActionResult> Review(Request request)
        {
            const int preapproved_total = 50;
            // creates condition for pre-apporval, sets to review if over the preapproval amount
            /* if(request.Total <= preapproved_total)
             {
                 request.Status = "APPROVED";
             }
             else{
                 request.Status = "REVIEW";
             }*/
            request.Status = request.Total <= preapproved_total ? "APPROVED" : "REVIEW";

            return await PutRequest(request.Id, request);
        }

        [HttpPut("approve")]
        public async Task<IActionResult> Approve(Request request){
            request.Status = "APPROVED";
            return await PutRequest(request.Id, request);
        }

        [HttpPut("reject")]
        public async Task<IActionResult> Reject(Request request) {
            request.Status = "REJECTED";
            return await PutRequest(request.Id, request);
        }


        public RequestsController(PRS_CapstoneDbContext context)
        {
            _context = context;
        }

        // GET: api/Requests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequests()
        {
            return await _context.Requests
                                    .Include(x => x.User)
                                    .ToListAsync();
        }

        // GET: api/Requests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Request>> GetRequest(int id)
        {
            var request = await _context.Requests
                                    .Include(x => x.User)
                                    .Include(x => x.RequestLines)
                                    .SingleOrDefaultAsync(x => x.Id == id);

            if (request == null)
            {
                return NotFound();
            }

            return request;
        }

        // PUT: api/Requests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequest(int id, Request request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }

            _context.Entry(request).State = EntityState.Modified;

            try
            {
                /// need to verify this is the correct parameter
                
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Requests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Request>> PostRequest(Request request)
        {
            _context.Requests.Add(request);
            await _context.SaveChangesAsync();
            

            return CreatedAtAction("GetRequest", new { id = request.Id }, request);
        }

        // DELETE: api/Requests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequest(int id)
        {
            var request = await _context.Requests.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }

            _context.Requests.Remove(request);
            await _context.SaveChangesAsync();
            

            return NoContent();
        }

        private bool RequestExists(int id)
        {
            return _context.Requests.Any(e => e.Id == id);
        }
    }
}
