using AutoMapper;
using HotelListing.DTOs;
using HotelListing.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelListing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<HotelController> _logger;
        private readonly IMapper _iMapper;

        public HotelController(IUnitOfWork unitOfWork, ILogger<HotelController> logger, IMapper iMapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _iMapper = iMapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetHotels()
        {

            try
            {
                var results = await _unitOfWork.Hotels.GetAll();
                var mappedDTOResult = _iMapper.Map<IList<HotelDTO>>(results);

                return Ok(mappedDTOResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(GetHotels)}");
                return StatusCode(500, "Internal Server Error. Please try again later.");
            }


        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetHotelById(int id)
        {

            try
            {
                var results = await _unitOfWork.Hotels.Get(hotel => hotel.HotelId == id, new List<string> {"Country"});
                var mappedDTOResult = _iMapper.Map<HotelDTO>(results);

                return Ok(mappedDTOResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(GetHotels)}");
                return StatusCode(500, "Internal Server Error. Please try again later.");
            }


        }


    }
}
