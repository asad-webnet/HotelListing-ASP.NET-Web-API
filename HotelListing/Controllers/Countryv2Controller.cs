using AutoMapper;
using HotelListing.Data;
using HotelListing.DTOs;
using HotelListing.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelListing.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/{v2:apiVersion}/country")]
    [ApiController]
    public class Countryv2Controller : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CountryController> _logger;
        private readonly IMapper _mapper;

        public Countryv2Controller(IUnitOfWork unitOfWork, ILogger<CountryController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCountries([FromQuery] RequestParams reqParams)
        {
            throw new DivideByZeroException("API v2 not ready yet");

            var countries = await _unitOfWork.Countries.GetAll(requestParams: reqParams, null);
            var results = _mapper.Map<IList<CountryDTO>>(countries);
            return Ok(results);
        }
    }
}
