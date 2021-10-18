﻿using System;
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
    public class RequestLinesController : ControllerBase
    {
        private readonly PRS_CapstoneDbContext _context;

        
         private async Task<IActionResult> RecalculateRequestTotal(int requestId)
         {
             var request = await _context.Requests.FindAsync(requestId);
             if(request==null) {
                 NotFound();
             }
             // adds up the totals from request for 
             request.Total = (from rl in _context.RequestLine
                              join prod in _context.Product
                              on rl.ProductId equals prod.Id
                              where rl.RequestId == requestId
                              select new
                              {
                                  LineTotal = prod.Price * rl.Quantity
                              }).Sum(x => x.LineTotal);
             await _context.SaveChangesAsync();
             return Ok();

         } 

        public RequestLinesController(PRS_CapstoneDbContext context)
        {
            _context = context;
        }

        // GET: api/RequestLines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RequestLine>>> GetRequestLine()
        {
            return await _context.RequestLine.ToListAsync();
        }

        // GET: api/RequestLines/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RequestLine>> GetRequestLine(int id)
        {
            var requestLine = await _context.RequestLine.FindAsync(id);

            if (requestLine == null)
            {
                return NotFound();
            }

            return requestLine;
        }

        // PUT: api/RequestLines/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequestLine(int id, RequestLine requestLine)
        {
            if (id != requestLine.Id)
            {
                return BadRequest();
            }

            _context.Entry(requestLine).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                await RecalculateRequestTotal(requestLine.RequestId);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestLineExists(id))
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

        // POST: api/RequestLines
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RequestLine>> PostRequestLine(RequestLine requestLine)
        {
            _context.RequestLine.Add(requestLine);
            await _context.SaveChangesAsync();
            await RecalculateRequestTotal(requestLine.RequestId); ;

            return CreatedAtAction("GetRequestLine", new { id = requestLine.Id }, requestLine);
        }

        // DELETE: api/RequestLines/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequestLine(int id)
        {
            var requestLine = await _context.RequestLine.FindAsync(id);
            if (requestLine == null)
            {
                return NotFound();
            }

            _context.RequestLine.Remove(requestLine);
            await _context.SaveChangesAsync();
            await RecalculateRequestTotal(requestLine.RequestId); ;

            return NoContent();
        }

        private bool RequestLineExists(int id)
        {
            return _context.RequestLine.Any(e => e.Id == id);
        }
    }
}
