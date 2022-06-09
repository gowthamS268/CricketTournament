using Cricket.Domain;
using Cricket.Services;
using JOIEnergy.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Cricket.Controllers;

[Route("matches")]
public class MatchController: Controller
{
    
    private readonly IMatchService _matchService;

    public MatchController(IMatchService matchService)
    {
        _matchService = matchService;
    }

    
    [HttpGet("read")]
    public ObjectResult GetMatches()
    {
        return new ObjectResult(_matchService.Matches);
    }
    

    
    [HttpGet("read/{matchId}")]
    public ObjectResult GetResult( long matchId)
    {
        var match = _matchService.Matches.FirstOrDefault(x => x.Id == matchId);
        return
            match != null ? 
                new ObjectResult(match) : 
                new NotFoundObjectResult(string.Format("Match ID ({0}) not found", matchId.ToString()));
    }

    
    [HttpPost("create")]
    public ObjectResult Create([FromBody]MatchParameter matchParam)
    {
        return CreateMatch(matchParam, null);
    }

    
    [HttpPut ("create/{matchId}")]
    public ObjectResult Create(long matchId, [FromBody]MatchParameter matchParam)
    {
        return CreateMatch(matchParam,matchId);
    }

    private ITeam? GetTeamById(long teamId)
    {
        return _matchService.Teams.FirstOrDefault(x => x.Id == teamId);
    }

    private ObjectResult CreateMatch(MatchParameter matchParam, long? matchId)
    {
        var teamOne = GetTeamById(matchParam.TeamOneId); 
        var teamTwo = GetTeamById(matchParam.TeamTwoId);

        if (teamOne == null || teamTwo == null)
        {
            return new BadRequestObjectResult(
                $"Invalid data for TeamOne: {matchParam.TeamOneId.ToString()} or TeamTwo: {matchParam.TeamTwoId.ToString()}");
        }
        
        var match = _matchService.Create(matchParam, matchId);
        return new OkObjectResult(match);
    }
   
}