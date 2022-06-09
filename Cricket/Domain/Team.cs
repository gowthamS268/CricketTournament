using Cricket.Enums;

namespace Cricket.Domain;

public class Team: ITeam
{
    public long Id { get; }
    public string Name { get; set; }
    public List<IPlayer> Players { get; set; }

    public Team(int id)
    {
        Id = id;
        Players = new List<IPlayer>();
    }

    public void AddPlayer(int id, string name, BattingRole battingRole, BowlingRole bowlingRole)
    {
        var player = new Player(id);
        player.Name = name;
        player.Batting = BattingRole.LeftArmBat;
        player.Bowling = BowlingRole.LeftArmSpin;
        Players.Add(player);
    }
}
