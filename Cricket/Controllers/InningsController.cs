using Cricket.Domain;
using Cricket.Enums;
using Cricket.Services;
using JOIEnergy.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Cricket.Controllers;

[Route("matches")]
public class InningsController: Controller
{
    private readonly IMatchService _matchService;
    private readonly IInningsService _inningsService;
    public InningsController(IMatchService matchService, IInningsService inningsService)
    {
        _matchService = matchService;
        _inningsService = inningsService;
    }

    
    [HttpGet("{matchId}/innings/read")]
    public ObjectResult GetInnings(long matchId)
    {
        var match = _matchService.Matches.FirstOrDefault(x => x.Id == matchId);
        return
            match != null ? 
                new ObjectResult(match.Innings) : 
                new NotFoundObjectResult(string.Format(" Match ID ({0}) not found", matchId.ToString()));
    }

    
    [HttpGet("{matchId}/innings/read/{inningsId}")]
    public ObjectResult GetInningsById(long matchId, long inningsId)
    {
        var match = _matchService.Matches.FirstOrDefault(x => x.Id == matchId)?.Innings.FirstOrDefault( y => y.Id == inningsId);
        return
            match != null ? 
                new ObjectResult(match) : 
                new NotFoundObjectResult(string.Format("Innings ID ({0}) in Match ID ({1}) not found",inningsId.ToString(), matchId.ToString()));
    }
    

    
    [HttpPost("{matchId}/innings/create")]
    public ObjectResult CreateInnings(long matchId, [FromBody] InningsParameter inningsParam)
    {
        if (!IsValidMatchId(matchId))
        {
           return new NotFoundObjectResult(string.Format(" Match ID ({0}) not found", matchId.ToString()));
        }

        var match = _matchService.Matches.First(x => x.Id == matchId);
        if (match.Innings.Count() >= 2 || match.Result != MatchResult.InProgress)
        {
            return new BadRequestObjectResult($"Invalid request for the Match Id ({matchId.ToString()})");
        }

        var innings = _inningsService.Create(inningsParam, match);
        return innings != null ? new CreatedResult("", innings) : new BadRequestObjectResult("Invalid Request");
    }
    
    [HttpPost ("{matchId}/super-over/play")]
    public ObjectResult CreateSuperOver(long matchId,[FromBody] SuperOverParameter superOverParam )
    {
        if (!IsValidMatchId(matchId))
        {
            return new NotFoundObjectResult(string.Format(" Match ID ({0}) not found", matchId.ToString()));
        }
        var match = _matchService.Matches.First(x => x.Id == matchId);
        var innings = _inningsService.CreateSuperOver(superOverParam, match);
        if (innings == null)
        {
            return new BadRequestObjectResult("Invalid parameters for super over");
        }
        var result = _inningsService.PlaySuperOver(superOverParam, innings);
        return new CreatedResult("", result);
    }
    
    private bool IsValidMatchId(long matchId)
    {
        return _matchService.Matches.Any(x => x.Id == matchId);
    }
}