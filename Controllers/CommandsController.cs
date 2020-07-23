using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CmdApi.Models;
using Microsoft.EntityFrameworkCore;


/*

https://www.youtube.com/watch?v=mUAZ-EbGBOg
This is where it comes from.

https://www.youtube.com/watch?v=fmvcAzHpsk8&t=3235s
This is updated version for .net core 3.1
You should check this out to make sure you are up to date as a version.



    To start program:
-Go to terminal
-Go to C:\Users\Efe\VSCode_Projects\CmdApi>
- >dotnet build (which builds and checks if there are any mistakes)
- >dotnet run (it will give you are localhost url.)
-You can see the database changes from MSSMS (database name is : CmdAPI or CmdApi)
*/


namespace CmdApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly CommandContext _context;

        public CommandsController(CommandContext context) => _context = context;


        //GET:          api/commands
        [HttpGet]
        public ActionResult<IEnumerable<Command>> GetCommands()
        {
            return _context.CommandItems;
        }

        //GET:          api/commands/<id>
        [HttpGet("{id}")]
        public ActionResult<Command> GetCommandItem(int id)
        {
            var commandItem = _context.CommandItems.Find(id);

            if (commandItem == null)
            {
                return NotFound();
            }

            return commandItem;
        }

        //POST:           api/commands     --------------- It adds item to db. You don't have to give id to body(postman). Because it creates id automatically.
        [HttpPost]
        public ActionResult<Command> PostCommandItem(Command command)
        {
            _context.CommandItems.Add(command);
            _context.SaveChanges();

            return CreatedAtAction("GetCommandItem", new Command{Id = command.Id}, command);
        }


        //PUT:          api/commands/<id>    ----------- It changes item. You need to give id as a part of the URL and also give to body (postman) as a Raw - JSON.
        [HttpPut("{id}")]
        public ActionResult<Command> PutCommandItem(int id, Command command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            _context.Entry(command).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }


        //DELETE:       api/commands/<id>   -------- It deletes item based on what you gave from URL.
        [HttpDelete("{id}")]
        public ActionResult<Command> DeleteCommandItem(int id)
        {
            var commandItem = _context.CommandItems.Find(id);
             if (commandItem == null)
            {
                return NotFound();
            }

            _context.CommandItems.Remove(commandItem);
            _context.SaveChanges();

            return commandItem;

        }

    }
}