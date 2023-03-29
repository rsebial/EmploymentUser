using EmployeeUserApp.Api.Dto;
using EmployeeUserApp.Api.Validations.Extensions;
using EmployeeUserApp.Domain.Commands;
using EmployeeUserApp.Domain.Queries;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EmployeeUserApp.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {        
        public UserController(
            IMediator mediator, 
            ILogger<UserController> logger, 
            IValidator<UserCreateDto> UserCreateValidator,
            IValidator<UserUpdateDto> UserUpdateValidator)
        {
            _mediator = mediator;
            _logger = logger;
            _UserCreateValidator = UserCreateValidator;
            _UserUpdateValidator = UserUpdateValidator;
        }

        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        private readonly IValidator<UserCreateDto> _UserCreateValidator;
        private readonly IValidator<UserUpdateDto> _UserUpdateValidator;

        


        /// <summary>
        /// Gets single User
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Ok when found, NotFound when not found</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute] long id)
        {
            try
            {
                var query = new UserGet.Query(id);
                var result = await _mediator.Send(query);
                if (result != null)
                {
                    return Ok(result.Adapt<UserGetDto>());
                }
                return NotFound();
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, exc.Message);
                return Problem(exc.Message);
            }
        }

        /// <summary>
        /// Gets List of All Users
        /// </summary>
        /// <param></param>
        /// <returns>Ok when found, NotFound when not found</returns>
        [HttpGet]
        public async Task<IActionResult> GetUsersAsync()
        {
            try
            {
                var query = new UserAllGet.Query();
                var result = await _mediator.Send(query);
                if (result != null)
                {
                    return Ok(result.Adapt<UserGetAllDto>());
                }
                return NotFound();
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, exc.Message);
                return Problem(exc.Message);
            }
        }


        /// <summary>
        /// Creates a new User
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>Created when success, BadRequest when not valid</returns>
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] UserCreateDto? dto)
        {
            try
            {
                if (dto == null) return BadRequest();
                var query = new UserByEmailSearch.Query(dto.Email);
                if(query != null)
                {
                    return BadRequest("Email must be unique!");
                }

                var employments = dto.Employments;
                var hasInvalidEmployment = false;
                if(employments != null)
                {
                    foreach(var e in employments)
                    {
                        if(e.EndDate < e.StartDate)
                        {
                            hasInvalidEmployment = true;
                        }
                    }
                }
                if (hasInvalidEmployment)
                {
                    return BadRequest("One or more employments having an invalid Start and End Dates.");
                }

                var validationResult = await _UserCreateValidator.ValidateAsync(dto);
                if (validationResult.IsValid)
                {
                    var command = dto.Adapt<UserCreate.Command>();
                    var result = await _mediator.Send(command);
                    return Created(new Uri($"/User/{result}", UriKind.Relative), result);
                }
                else
                {
                    validationResult.AddToModelState(this.ModelState);
                    return BadRequest(ModelState);
                }
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, exc.Message);
                return Problem(exc.Message);
            }
        }

        
        /// <summary>
        /// Updates an existing User
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>Accepted when success, BadRequest when Invalid, NotFound when not found</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UserUpdateDto? dto)
        {
            try
            {
                if (dto == null) return BadRequest();
                var query = new UserByEmailSearch.Query(dto.Email);
                if (query != null)
                {
                    return BadRequest("Email must be unique!");
                }

                var employments = dto.Employments;
                var hasInvalidEmployment = false;
                if (employments != null)
                {
                    foreach (var e in employments)
                    {
                        if (e.EndDate < e.StartDate)
                        {
                            hasInvalidEmployment = true;
                        }
                    }
                }
                if (hasInvalidEmployment)
                {
                    return BadRequest("One or more employments having an invalid Start and End Dates.");
                }

                var validationResult = await _UserUpdateValidator.ValidateAsync(dto);
                if (validationResult.IsValid)
                {
                    var command = dto.Adapt<UserUpdate.Command>();
                    var result = await _mediator.Send(command);
                    return result switch
                    {
                        UpdateResult.NotFound => NotFound(),
                        _ => Accepted(new Uri($"/User/{dto.Id}", UriKind.Relative), result)
                    };
                } 
                else
                {
                    validationResult.AddToModelState(this.ModelState);
                    return BadRequest(this.ModelState);
                }               
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, exc.Message);
                return Problem(exc.Message);
            }
        }

        /// <summary>
        /// Removes a User
        /// </summary>
        /// <param name="id"></param>
        /// <returns>NoContent when success, Forbidden when not allowed</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            try
            {                
                var result = await _mediator.Send(new UserDelete.Command(id));
                return result switch
                {
                    DeleteResult.Forbidden => StatusCode((int)HttpStatusCode.Forbidden),
                    DeleteResult.NotFound => NotFound(),
                    _ => NoContent()
                };

            }
            catch (Exception exc)
            {
                _logger.LogError(exc, exc.Message);
                return Problem(exc.Message);
            }
        }
    }
}