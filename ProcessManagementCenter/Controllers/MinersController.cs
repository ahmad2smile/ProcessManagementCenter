using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProcessManagementCenter.Domain;
using ProcessManagementCenter.Models;

namespace ProcessManagementCenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MinersController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public MinersController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/Miners
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Miner>>> GetMiners()
        {
            return await _context.Miners.ToListAsync();
        }

        // GET: api/Miners/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Miner>> GetMiner(int id)
        {
            var miner = await _context.Miners.FindAsync(id);

            if (miner == null)
            {
                return NotFound();
            }

            return miner;
        }

        // PUT: api/Miners/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMiner(int id, Miner miner)
        {
            if (id != miner.Id)
            {
                return BadRequest();
            }

            _context.Entry(miner).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MinerExists(id))
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

        // POST: api/Miners
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Miner>> PostMiner(Miner miner)
        {
            _context.Miners.Add(miner);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMiner", new { id = miner.Id }, miner);
        }

        // DELETE: api/Miners/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Miner>> DeleteMiner(int id)
        {
            var miner = await _context.Miners.FindAsync(id);
            if (miner == null)
            {
                return NotFound();
            }

            _context.Miners.Remove(miner);
            await _context.SaveChangesAsync();

            return miner;
        }

        private bool MinerExists(int id)
        {
            return _context.Miners.Any(e => e.Id == id);
        }
    }
}
