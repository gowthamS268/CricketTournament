using Cricket.Domain;
using JOIEnergy.Domain;

namespace Cricket.Services;

public interface IMatchService
{
    public List<IMatch> Matches { get; }
    public List<ITeam> Teams { get; }

    public IMatch Create(MatchParameter matchParam, long? matchId=null);
}