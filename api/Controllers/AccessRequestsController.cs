using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using api.Models;
using System.Linq;

#region AccessRequestsController
namespace api.Controllers
{
    [Route("api/[controller]")]
    public class AccessRequestsController : Controller
    {
        private readonly AccessRequestContext _context;
#endregion

        public AccessRequestsController(AccessRequestContext context)
        {
            _context = context;

            if (_context.AccessRequests.Count() == 0)
            {
                _context.AccessRequests.Add(new AccessRequest { Sample_Field_1 = "Request1" });
                _context.SaveChanges();
            }
        }

        #region snippet_GetAll
        [HttpGet]
        public IEnumerable<AccessRequest> GetAll()
        {
            return _context.AccessRequests.ToList();
        }

        [HttpGet("{id}", Name = "GetAccessRequest")]
        public IActionResult GetById(long id)
        {
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
            if (request == null)
            {
                return BadRequest();
            }

            _context.AccessRequests.Add(request);
            _context.SaveChanges();

            return CreatedAtRoute("GetAccessRequest", new { id = request.Id }, request);
        }
        #endregion

        #region snippet_Update
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] AccessRequest request)
        {
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

