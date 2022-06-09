namespace Cricket.Domain;

public class BallDetail
{
    public int Id { get; }
    public string BallType { get; }
    public string ShotType { get; }
    public string Timing { get; }

    public ShotOutcome ShotOutcome { get; private set; }

    public BallDetail(int id, string ballType, string shotType, string timing)
    {
        Id = id;
        BallType = ballType;
        ShotType = shotType;
        Timing = timing;
    }

    public void CreateShotOutCome(Dictionary<string, ShotOutcome> shotOutcomes )
    {
        var key = $"{BallType}_{ShotType}_{Timing}";
        ShotOutcome = shotOutcomes[key];
    }
}
