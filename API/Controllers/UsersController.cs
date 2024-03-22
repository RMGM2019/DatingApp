using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API
{
    [Authorize]
    public class UsersController : BaseAPIController
    {
        private readonly DataContext _context;
        public UsersController(DataContext context)
        {
            _context = context;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }

        [HttpGet("{id}")] // /api/usres/2
        public async Task<ActionResult<AppUser>> GetUser(int id)
        {

            return await _context.Users.FindAsync(id);
        }

        // [HttpPost("{nombre}")]
        // public int Test(string nombre)
        // {
        //     var newInfo = new TestEF();
        //     newInfo.name = nombre;
        //     _context.Test.Add(newInfo);
        //     var inserted = _context.SaveChanges();
        //     Console.WriteLine(inserted);
        //     return 1;
        // }

        // [HttpDelete]
        // public async Task<ActionResult<IEnumerable<TestEF>>> Test2()
        // {
        //    var result = await  _context.Test.ToListAsync();
        //    Console.WriteLine("result: "+result);
        //    var filtered = result.Where(s => s.name.ToLower() == "pepe").Select(e => e).ToList();
        //    //Console.WriteLine("filtered: "+filtered.Count());

        //    return filtered;

        // }
    }
}

