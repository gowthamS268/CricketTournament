using Cricket.Domain;
using JOIEnergy.Domain;

namespace Cricket.Services;

public class MatchService : IMatchService
{
    public List<IMatch> Matches { get; }
    public List<ITeam> Teams { get; }

    public MatchService(List<IMatch> matches, List<ITeam> teams)
    {
        Matches = matches;
        Teams = teams;
    }

    public IMatch Create(MatchParameter matchParam, long? matchId = null)
    {
        var match = new Match(matchParam.TeamOneId, matchParam.TeamTwoId, matchId);
        match.Name = matchParam.Name;
        Matches.Add(match);
        return match;
    }
}