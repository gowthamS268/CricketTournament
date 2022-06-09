using System.Text.Json.Serialization;
using Cricket.Commons;
using Cricket.Enums;
using JOIEnergy.Domain;


namespace Cricket.Domain;

public class Match : IMatch
{
    private List<Innings> _innings;
    
    public long Id { get;  }
    public string Name { get; set; }
   
    [JsonIgnore]
    public long TeamOne { get; }
    
    [JsonIgnore]
    public long TeamTwo { get; }
    
    [JsonIgnore]
    public MatchResult Result { get; set; }
    
    [JsonIgnore]
    public List<Innings> Innings => _innings;
    public Match( long teamOne, long teamTwo, long? matchId=null)
    {
        Id = matchId ?? Constants.GetMatchId();
        TeamOne = teamOne;
        TeamTwo = teamTwo;
        _innings = new List<Innings>();
        Result = MatchResult.InProgress;
    }

    [JsonIgnore]
    public string ResultSummary
    {
        get
        {
            var inniningsOne = GetInnings(1);
            var inniningsTwo = GetInnings(2);
            if (inniningsOne != null && inniningsTwo != null && inniningsOne.IsCompleted && inniningsTwo.IsCompleted)
            {
                if (inniningsOne.Score == inniningsTwo.Score)
                {
                    return "Match Tied with Super Over";
                }

                return inniningsOne.Score > inniningsTwo.Score
                    ? $"Match Won By Team ID({inniningsOne.BattingId})"
                    : $"Match Won By Team ID({inniningsTwo.BattingId})";
            }

            return "Match In Progress";
        }
    }

    public Innings? CreateInnings(InningsParameter inningsParam, bool isSuperOver = false)
    {
        var inningsCount = _innings.Count(x=> x.IsSuperOver == isSuperOver);
        if (inningsCount >= 2) return null;
        var innings = new Innings(inningsCount + 1, isSuperOver);
        innings.Name = inningsParam.Name;
        innings.BattingId = inningsParam.BattingTeamId;
        innings.BowlingId = inningsParam.BowlingTeamId;
        _innings.Add(innings);
        return innings;
    }
    
    private Innings? GetInnings(int id)
    {
        return Innings.FirstOrDefault(x => x.Id == id);
    }

}
