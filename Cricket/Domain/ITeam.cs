using Cricket.Enums;

namespace Cricket.Domain;

public interface ITeam
{
    public long Id { get; }
    public string Name { get; }
    public List<IPlayer> Players { get; }
    public void AddPlayer(int id, string name, BattingRole battingRole, BowlingRole bowlingRole);
}