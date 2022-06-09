using Cricket.Enums;

namespace Cricket.Domain;

public interface IPlayer
{
    public long Id { get; }
    public string Name { get; }
    public short Age { get; }
    public BattingRole Batting { get;}
    public BowlingRole Bowling { get; }
}
