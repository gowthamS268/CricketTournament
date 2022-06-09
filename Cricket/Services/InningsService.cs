using Cricket.Commons;
using Cricket.Domain;
using JOIEnergy.Domain;

namespace Cricket.Services;

public class InningsService : IInningsService
{
    public List<IMatch> Matches { get; }
    private Dictionary<string, ShotOutcome> ShotOutcomes { get; }
    public List<ITeam> Teams { get; }
    public InningsService(List<IMatch> matches, List<ITeam> teams, Dictionary<string, ShotOutcome> shotOutcomes)
    {
        Matches = matches;
        Teams = teams;
        ShotOutcomes = shotOutcomes;
    }
    
    public Innings? Create(InningsParameter inningsParam, IMatch match)
    {
       var innings = match.CreateInnings(inningsParam, false);
       return innings;
    }

    public Innings? CreateSuperOver(InningsParameter superOverParams, IMatch match)
    {
        var innings = match.CreateInnings(superOverParams, true);
        return innings;
    }

    public string PlaySuperOver(SuperOverParameter superOverParams, Innings innings)
    {
        var index = 2;
        foreach (var shot in superOverParams.Shots)
        {
            var bowlingType = Constants.BowlingCards[index % 10];
            var ballDetail = innings.CreateBallDetail(bowlingType, shot.BattingType, shot.ShotTiming);
            ballDetail?.CreateShotOutCome(ShotOutcomes);

            if (IsSuperOverCompleted(innings, superOverParams.Target)) break;
            index++;
        }

        if (superOverParams.Target == innings.Score) return "Match Tied";
        var winningTeamId = superOverParams.Target > innings.Score
            ? superOverParams.BowlingTeamId
            : superOverParams.BattingTeamId;
        var winningTeam = Teams.First(x => x.Id == winningTeamId);
        return $"{winningTeam.Name} Won";
    }

    private bool IsSuperOverCompleted(Innings innings, int Target)
    {
        return  innings.Score>Target || innings.WicketsRemaining == 0;
    }
}