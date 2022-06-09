using Cricket.Domain;
using JOIEnergy.Domain;

namespace Cricket.Services;

public class BallOutcomeService: IBallOutcomeService
{
    private Dictionary<string, ShotOutcome> ShotOutcomes { get; }
    public List<IMatch> Matches { get; }

    public BallOutcomeService(List<IMatch> matches, Dictionary<string, ShotOutcome> shotOutcomes)
    {
        ShotOutcomes = shotOutcomes;
        Matches = matches;
    }

    public ShotOutcome? Create(BallParameter ballParam, Innings innings)
    {
        var ballDetail = innings.CreateBallDetail(ballParam.BowlingType, ballParam.BattingType, ballParam.ShotTiming);
        ballDetail?.CreateShotOutCome(ShotOutcomes);
        return ballDetail?.ShotOutcome;
    }
}