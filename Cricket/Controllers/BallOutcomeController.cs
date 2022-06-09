using Cricket.Domain;
using Cricket.Services;
using JOIEnergy.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Cricket.Controllers;

[Route("matches")]
public class BallOutcomeController: Controller
{
    private  IBallOutcomeService _ballOutcomeService;
    public BallOutcomeController(IBallOutcomeService ballOutcomeService)
    {
        _ballOutcomeService = ballOutcomeService;
    }
    
    [HttpGet("{matchId}/innings/{inningsId}/balls/read")]
    public ObjectResult GetInnings(long matchId, long inningsId)
    {
        if (!IsValidMatchId(matchId))
        {
            return new NotFoundObjectResult(string.Format(" Match ID ({0}) not found", matchId.ToString()));
        }

        var match = _ballOutcomeService.Matches.First(x => x.Id == matchId);
        if (!IsValidInningsId(match, inningsId))
        {
            return new NotFoundObjectResult(string.Format(" Innings ID ({0}) not found", inningsId.ToString()));
        }
        var innings = match.Innings.First(x => x.Id == inningsId);
        return new OkObjectResult(innings.BallDetails);
    }

    
    [HttpGet("{matchId}/innings/{inningsId}/balls/read/{ballId}")]
    public ObjectResult GetResult(long matchId, long inningsId, int ballId)
    {
        if (!IsValidMatchId(matchId))
        {
            return new NotFoundObjectResult(string.Format(" Match ID ({0}) not found", matchId.ToString()));
        }

        var match = _ballOutcomeService.Matches.First(x => x.Id == matchId);
        if (!IsValidInningsId(match, inningsId))
        {
            return new NotFoundObjectResult(string.Format(" Innings ID ({0}) not found", inningsId.ToString()));
        }
        var ballDetail = match.Innings.First(x => x.Id == inningsId).BallDetails.FirstOrDefault(x=> x.Id == ballId);
        return
            ballDetail != null ? 
                new ObjectResult(ballDetail) : 
                new NotFoundObjectResult(string.Format("Ball Id ({0}) not found",ballId.ToString()));

    }

    
    [HttpPut ("{matchId}/innings/{inningsId}/balls/create/{ballId}")]
    public ObjectResult Create(long matchId, long inningsId, int ballId, [FromBody]BallParameter ballParam)
    {
        if (!IsValidMatchId(matchId))
        {
            return new NotFoundObjectResult(string.Format(" Match ID ({0}) not found", matchId.ToString()));
        }

        var match = _ballOutcomeService.Matches.First(x => x.Id == matchId);
        if (!IsValidInningsId(match, inningsId))
        {
            return new NotFoundObjectResult(string.Format(" Innings ID ({0}) not found", inningsId.ToString()));
        }
        var innings = match.Innings.First(x => x.Id == inningsId);

        if (innings.BallDetails.Any(x=> x.Id == ballId))
        {
            return new ConflictObjectResult(string.Format(" Ball Id ({0}) is already exists", ballId.ToString()));
        }
        var ballOutcome = _ballOutcomeService.Create(ballParam, innings);
        return ballOutcome != null ? 
            new CreatedResult("",ballOutcome) : 
            new BadRequestObjectResult("Invalid data ");
    }
    
    private bool IsValidMatchId(long matchId)
    {
        return _ballOutcomeService.Matches.Any(x => x.Id == matchId);
    }
    
    private bool IsValidInningsId(IMatch match, long inningsId)
    {
        return match.Innings.Any(x => x.Id == inningsId);
    }

}