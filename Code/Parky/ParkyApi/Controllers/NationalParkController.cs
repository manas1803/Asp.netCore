using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkyApi.Mapper;
using ParkyApi.Model;
using ParkyApi.Model.Dtos;
using ParkyApi.Repository.IRepository;

namespace ParkyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NationalParkController : ControllerBase
    {
        private readonly INationalPark _npRepo;
        private readonly IMapper _mapper;

        public NationalParkController(INationalPark npRepo, IMapper mapper)
        {
            _npRepo = npRepo;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetNationalParks()
        {
            var objList = _npRepo.GetNationalParks();
            var objDto = new List<NationalParkDto>();
            if (objList == null)
            {
                return NotFound("Object List Not Found");
            }
            foreach(var obj in objList)
            {
                objDto.Add(_mapper.Map<NationalParkDto>(obj));
            }

            return Ok(objDto);
        }
        
        [HttpGet("{nationalParkId:int}",Name="GetNationalPark")]
        public IActionResult GetNationalPark(int nationalParkId)
        {
            var obj = _npRepo.GetNationalPark(nationalParkId);
            if (obj == null)
            {
                return NotFound("Object Cannot be Found");
            }
            var objDto = _mapper.Map<NationalParkDto>(obj);
            return Ok(objDto);
        }
        
        [HttpPost]
        public IActionResult CreateNationalPark([FromBody] NationalParkDto nationalParkDto)
        {
            if (nationalParkDto == null)
            {
                ModelState.AddModelError("NullException", "Object is Null");
                return StatusCode(StatusCodes.Status400BadRequest,ModelState);
            }
            if (_npRepo.NationalParkExists(nationalParkDto.Name))
            {
                ModelState.AddModelError("DuplicateException", "National Park already Exists");
                return StatusCode(StatusCodes.Status406NotAcceptable, ModelState);
            }
            var nationalParkObj = _mapper.Map<NationalPark>(nationalParkDto);
            if (!_npRepo.CreateNationalPark(nationalParkObj))
            {
                ModelState.AddModelError("CreationException",$"Some error occured while adding the national park {nationalParkObj.Name}");
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }
            return CreatedAtRoute("GetNationalPark",new { nationalParkId = nationalParkObj.Id},nationalParkObj);
        }

        [HttpPatch("{nationalParkId:int}", Name = "UpdateNationalPark")]
        public IActionResult UpdateNationalPark([FromBody]NationalParkDto nationalParkDto,int nationalParkId)
        {

            if (nationalParkDto == null || nationalParkDto.Id!=nationalParkId)
            {
                ModelState.AddModelError("NullException", "Object is Null or id doesn't match");
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }
            var nationalParkObj = _mapper.Map<NationalPark>(nationalParkDto);
            if (!_npRepo.UpdateNationalPark(nationalParkObj))
            {
                ModelState.AddModelError("UpdateException", $"Some error occured while updating the national park {nationalParkObj.Name}");
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }
            return NoContent();
        }
        [HttpDelete("{nationalParkId:int}", Name = "DeleteNationalPark")]
        public IActionResult DeleteNationalPark(int nationalParkId)
        {

            if (!_npRepo.NationalParkExists(nationalParkId))
            {
                ModelState.AddModelError("NullException", "National Park does not exists");
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }
            var nationalParkObj = _npRepo.GetNationalPark(nationalParkId);
            if (!_npRepo.DeleteNationalPark(nationalParkObj))
            {
                ModelState.AddModelError("DeletionException", $"Some error occured while deleting the national park {nationalParkObj.Name}");
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }
            return NoContent();
        }
    }
}
