using Cricket.Domain;
using JOIEnergy.Domain;

namespace Cricket.Services;

public interface IBallOutcomeService
{
    public List<IMatch> Matches { get; }    
    public ShotOutcome? Create(BallParameter ballParam, Innings innings);
}