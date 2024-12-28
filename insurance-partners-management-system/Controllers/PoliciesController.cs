using Microsoft.AspNetCore.Mvc;
using insurance_partners_management_system.Models;
using insurance_partners_management_system.Repository.Interface;

namespace insurance_partners_management_system.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PoliciesController : ControllerBase
    {
        private readonly IPolicyRepository _policyRepository;
        private readonly IPartnerRepository _partnerRepository;

        public PoliciesController(IPolicyRepository policyRepository, IPartnerRepository partnerRepository)
        {
            _policyRepository = policyRepository;
            _partnerRepository = partnerRepository;
        }

        // GET: api/policies/partner/{partnerId}
        [HttpGet("partner/{partnerId}")]
        public IActionResult GetPoliciesByPartnerId(int partnerId)
        {
            var partner = _partnerRepository.GetById(partnerId);
            if (partner == null)
            {
                return NotFound(new { Message = "Partner not found" });
            }

            var policies = _policyRepository.GetById(partnerId);
            return Ok(policies);
        }

        // POST: api/policies
        [HttpPost]
        public IActionResult AddPolicy([FromBody] Policy policy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var partner = _partnerRepository.GetById(policy.PartnerId);
            if (partner == null)
            {
                return NotFound(new { Message = "Partner not found" });
            }

            _policyRepository.Add(policy);
            return CreatedAtAction(nameof(GetPoliciesByPartnerId), new { partnerId = policy.PartnerId }, policy);
        }

        // PUT: api/policies/{id}
        [HttpPut("{id}")]
        public IActionResult UpdatePolicy(int id, [FromBody] Policy policy)
        {
            if (id != policy.IdPolicy)
            {
                return BadRequest(new { Message = "ID mismatch" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingPolicy = _policyRepository.GetById(id);
            if (existingPolicy == null)
            {
                return NotFound(new { Message = "Policy not found" });
            }

            _policyRepository.Update(policy);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePolicy(int id)
        {
            var existingPolicy = _policyRepository.GetById(id);
            if (existingPolicy == null)
            {
                return NotFound(new { Message = "Policy not found" });
            }

            _policyRepository.Delete(id);
            return NoContent();
        }
    }
}