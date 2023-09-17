using AutoMapper;
using HotelListing.Data;
using HotelListing.DTOs;
using HotelListing.IRepository;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelListing.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CountryController> _logger;
        private readonly IMapper _mapper;

        public CountryController(IUnitOfWork unitOfWork, ILogger<CountryController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [HttpCacheExpiration(MaxAge = 69)]
        public async Task<IActionResult> GetCountries([FromQuery] RequestParams reqParams)
        {
            //throw new DivideByZeroException("Divide by zero occurred");

            var countries = await _unitOfWork.Countries.GetAll(requestParams: reqParams,null);
            var results = _mapper.Map<IList<CountryDTO>>(countries);
            return Ok(results);
        }

        [HttpGet("{id:int}",Name = "GetCountryByID")]
        public async Task<IActionResult> GetCountryById(int id)
        {
            try
            {
                var country = await _unitOfWork.Countries.Get(country => country.CountryId == id,
                                                        new List<string> { "Hotels" });
                var result = _mapper.Map<CountryDTO>(country);
                return Ok(country);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,$"Something Went Wrong in the {nameof(GetCountryById)}");
                return StatusCode(500,"Internal Server Error. Please try again later.");
            }
        }

        //[Authorize(Roles = "Administrator")]
        [HttpPost]
        //[Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCountry([FromBody] CreateCountryDTO countryDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST Attempt in {nameof(CreateCountry)}");
                return BadRequest(ModelState);
            }

            try
            {
                // Mapping from Source (CreateCountryDTO) to HotelDTO, returns Country Obj
                var countryObj = _mapper.Map<Country>(countryDTO);

                await _unitOfWork.Countries.Insert(countryObj);
                await _unitOfWork.Save();

                return CreatedAtRoute("GetCountryByID", new { id = countryObj.CountryId }, countryObj);

            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Something Went Wrong in the {nameof(CreateCountry)}");
                return StatusCode(500, "Internal Server Error. Please try again later.");
            }

        }

        [Authorize(Roles = "Administrator")]
        [HttpPut(template: "{id:int}")]
        public async Task<IActionResult> UpdateCountry(int id, [FromBody] UpdateCountryDTO countryDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST Attempt in {nameof(CreateCountry)}");
                return BadRequest(ModelState);
            }

            try
            {
                var country = await _unitOfWork.Countries.Get(country => country.CountryId == id);

                if (country == null)
                {
                    _logger.LogError($"Invalid Country ID from {nameof(UpdateCountry)}");
                    return BadRequest("Invalid Country ID");
                }

                country = _mapper.Map(countryDTO, country);
                _unitOfWork.Countries.Update(country);
                await _unitOfWork.Save();

                _logger.LogInformation(message:$"Country Of ID ${id} has been updated successfully");
                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Something Went Wrong in the {nameof(UpdateCountry)}");
                return StatusCode(500, "Internal Server Error. Please try again later.");
            }
        }

    }
}
