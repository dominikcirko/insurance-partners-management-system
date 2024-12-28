using Microsoft.AspNetCore.Mvc;
using insurance_partners_management_system.Models;
using insurance_partners_management_system.Repository.Interface;

namespace insurance_partners_management_system.Controllers
{

    namespace insurance_partners_management_system.Controllers
    {
        [ApiController]
        [Route("api/[controller]")]
        public class PartnersController : ControllerBase
        {
            private readonly IPartnerRepository _partnerRepository;

            public PartnersController(IPartnerRepository partnerRepository)
            {
                _partnerRepository = partnerRepository;
            }

            [HttpGet]
            public IActionResult GetAll()
            {
                var partners = _partnerRepository.GetAll()
                    .Select(p => new
                    {
                        p.IdPartner,
                        FullName = $"{p.FirstName} {p.LastName}",
                        p.IsForeign,
                        p.PartnerTypeId,
                        p.PartnerNumber,
                        p.CreatedAtUtc
                    });

                return Ok(partners);
            }

            [HttpGet("{id}")]
            public IActionResult GetById(int id)
            {
                var partner = _partnerRepository.GetById(id);
                if (partner == null)
                {
                    return NotFound(new { Message = "Partner not found" });
                }

                var partnerDetails = new
                {
                    partner.IdPartner,
                    FullName = $"{partner.FirstName} {partner.LastName}",
                    partner.Address,
                    partner.PartnerNumber,
                    partner.CroatianPin,
                    partner.PartnerTypeId,
                    partner.CreatedAtUtc,
                    partner.CreateByUser,
                    partner.IsForeign,
                    partner.ExternalCode,
                    partner.Gender
                };

                return Ok(partnerDetails);
            }

            [HttpPost]
            public IActionResult Add([FromBody] Partner partner)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _partnerRepository.Add(partner);
                return CreatedAtAction(nameof(GetById), new { id = partner.IdPartner }, partner);
            }

            [HttpPut("{id}")]
            public IActionResult Update(int id, [FromBody] Partner partner)
            {
                if (id != partner.IdPartner)
                {
                    return BadRequest(new { Message = "ID mismatch" });
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var existingPartner = _partnerRepository.GetById(id);
                if (existingPartner == null)
                {
                    return NotFound(new { Message = "Partner not found" });
                }

                _partnerRepository.Update(partner);
                return NoContent();
            }

            [HttpDelete("{id}")]
            public IActionResult Delete(int id)
            {
                var existingPartner = _partnerRepository.GetById(id);
                if (existingPartner == null)
                {
                    return NotFound(new { Message = "Partner not found" });
                }

                _partnerRepository.Delete(id);
                return NoContent();
            }
        }
    }
}
