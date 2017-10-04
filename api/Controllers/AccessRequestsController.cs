using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using api.Models;
using System;
using System.Linq;

#region AccessRequestsController
namespace api.Controllers
{
    [Route("ears/v0/[controller]")]
    public class AccessRequestsController : Controller
    {
        private readonly AccessRequestContext _context;
#endregion



        public AccessRequestsController(AccessRequestContext context)
        {
            //Console.WriteLine("AccessRequestsController: point 1");

            _context = context;

            //Console.WriteLine("AccessRequestsController: point 2");

            try
            {
              //  Console.WriteLine("AccessRequestsController: point 3");

               // Console.WriteLine(_context.AccessRequests.Count());

                /* if (_context.AccessRequests != null && _context.AccessRequests.Count() == 0)
                {
                   Console.WriteLine("AccessRequestsController: point 4");

                    _context.AccessRequests.Add(new AccessRequest { Sample_Field_1 = "Request1" });
                    _context.SaveChanges();

                    Console.WriteLine("AccessRequestsController: point 5");
                }*/
            } catch(Npgsql.PostgresException ex) {
                Console.WriteLine("AccessRequestsController: point 6");
                Console.WriteLine(ex.Detail);
            } catch(Microsoft.EntityFrameworkCore.DbUpdateException ex) {
                Console.WriteLine("AccessRequestsController: point 7");
                Console.WriteLine(ex.Message);

            } catch (Exception ex) {
                Console.WriteLine("AccessRequestController: point 21");
                Console.WriteLine(ex.ToString());
            }

			//Console.WriteLine("AccessRequestController: point 22");

		}

        #region snippet_GetAll
        [HttpGet]
        public IEnumerable<AccessRequest> GetAll()
        {
           // Console.WriteLine("AccessRequestsController: point 8");

            try {

            return _context.AccessRequests.ToList();

			}  catch (Exception ex) {
                Console.WriteLine("AccessRequestController: point 31");
                Console.WriteLine(ex.ToString());
                return null;
            }

        }

        [HttpGet("{id}", Name = "GetAccessRequest")]
        public IActionResult GetById(long id)
        {
          //  Console.WriteLine("AccessRequestsController: point 9");

            var request = _context.AccessRequests.FirstOrDefault(t => t.Id == id);
            if (request == null)
            {
                return NotFound();
            }
            return new ObjectResult(request);
        }
        #endregion
        #region snippet_Create
        [HttpPost]
        public IActionResult Create([FromBody] AccessRequest request)
        {

           // Console.WriteLine("AccessRequestsController: point 10");
            if (request == null)
            {
                return BadRequest();
            }
          //  Console.WriteLine("AccessRequestsController: point 101");

            try
            {
                _context.AccessRequests.Add(request);
                _context.SaveChanges();
			}
			catch (Microsoft.EntityFrameworkCore.DbUpdateException ex)
			{
				Console.WriteLine(ex.Message);
                return BadRequest(ex);
			
		} catch (Exception ex) {
                Console.WriteLine("AccessRequestController: point 41");
                Console.WriteLine(ex.ToString());
            }

            return CreatedAtRoute("GetAccessRequest", new { id = request.Id }, request);
        }
        #endregion

        #region snippet_Update
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] AccessRequest request)
        {

            //Console.WriteLine("AccessRequestsController: point 11");
            if (request == null || request.Id != id)
            {
                return BadRequest();
            }

            var AccessRequest = _context.AccessRequests.FirstOrDefault(t => t.Id == id);
            if (AccessRequest == null)
            {
                return NotFound();
            }

            AccessRequest.Sample_Field_1 = request.Sample_Field_1;
			AccessRequest.Sample_Field_2 = request.Sample_Field_2;

            _context.AccessRequests.Update(AccessRequest);
            _context.SaveChanges();
            return new NoContentResult();
        }
        #endregion

        #region snippet_Delete
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {

           // Console.WriteLine("AccessRequestsController: point 12");
            var AccessRequest = _context.AccessRequests.FirstOrDefault(t => t.Id == id);
            if (AccessRequest == null)
            {
                return NotFound();
            }

            _context.AccessRequests.Remove(AccessRequest);
            _context.SaveChanges();
            return new NoContentResult();
        }
        #endregion
    }
}

