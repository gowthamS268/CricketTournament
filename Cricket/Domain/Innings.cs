using System.Text.Json.Serialization;

namespace Cricket.Domain;

public class Innings
{
    private readonly int _maxBalls;
    
    public long Id { get; }
    public bool IsSuperOver { get; }
    public string Name { get; set; }
    public long BattingId { get; set; }
    public long BowlingId { get; set; }
    
    [JsonIgnore]
    public int TotalWickets { get; }
    
    [JsonIgnore]
    public List<BallDetail> BallDetails{ get; set; }
    
    public Innings(long id,bool isSuperOver = false)
    {
        Id = id;
        BallDetails = new List<BallDetail>();
        IsSuperOver = isSuperOver;
        if (isSuperOver)
        {
            TotalWickets = 2;
            _maxBalls = 6;
        }
        else
        {
            TotalWickets =  10;
            _maxBalls = 20;
        }
    }
    
    public int Score
    {
        get
        {
            return BallDetails.Sum(x => x.ShotOutcome.Run);
        }
    }
    
    public int Wickets
    {
        get
        {
            return BallDetails.Where(x => x.ShotOutcome.IsWicket).Count();
        }
    }

    [JsonIgnore]
    public int WicketsRemaining
    {
        get
        {
            return TotalWickets - Wickets;
        }
    }
    
    [JsonIgnore]
    public bool IsCompleted
    {
        get
        {
            return BallDetails.Count() == _maxBalls || WicketsRemaining == 0;
        }
    }

    [JsonIgnore]
    public string InningsSummary
    {
        get
        {
            return $"{Score.ToString()}-{Wickets.ToString()}({BallDetails.Count().ToString()})";
        }
    }
    
    public BallDetail? CreateBallDetail(string ballType, string shotType, string timing)
    {
        var ballCount = BallDetails.Count();
        if (ballCount >= _maxBalls || WicketsRemaining ==0) return null;
        
        var ballDetail = new BallDetail(ballCount+1, ballType, shotType, timing);
        BallDetails.Add(ballDetail);
        return ballDetail;
    }
}