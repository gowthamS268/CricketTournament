namespace JOIEnergy.Domain;

public class MatchParameter
{
    public string Name { get; set; }
    public long TeamOneId { get; set; }
    public long TeamTwoId { get; set; }
}

public class InningsParameter
{
    public string Name { get; set; }
    public long BattingTeamId { get; set; }
    public long BowlingTeamId { get; set; }
}

public class ShotParameter
{
    public string BattingType { get; set; }
    public string ShotTiming { get; set; }
}

public class BallParameter: ShotParameter
{
    public string BowlingType { get; set; }
}


public class SuperOverParameter : InningsParameter
{
    public int Target { get; set; }
    public ShotParameter[] Shots { get; set; }
}