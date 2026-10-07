using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talaqah.Application.Common.Mediator;
using Talaqah.Application.Common.Results;
using Talaqah.Application.Features.ExamSchedule.DTOs.Responses;
using Talaqah.Application.Features.ExamSchedule.Queries.GetAllExams;
using Talaqah.Application.Common.Pagination;

namespace Talaqah.WebAPI.Controllers
{
    [Route("api/exam-schedules")]
    [ApiController]
    public class ExamSchedulesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExamSchedulesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Result<ExamSchedulesViewByAdminResponse>), StatusCodes.Status200OK)]
        public async Task <ActionResult<Result<ExamSchedulesViewByAdminResponse>>> GetAllExamSchedules(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var result = await _mediator.Send(new GetAllExamsQuery(new PaginationParameters(pageNumber, pageSize)));
            return result.IsSuccess? Ok(result) : BadRequest(result);
        }
    }
}
