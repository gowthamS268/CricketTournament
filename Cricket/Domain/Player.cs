using System.Security.Cryptography;
using Cricket.Enums;
using JOIEnergy.Domain;

namespace Cricket.Domain;

public class Player : IPlayer
{
    public long Id { get; }
    public string Name { get; set; }
    public short Age { get; set; }
    public BattingRole Batting { get; set;}
    public BowlingRole Bowling { get; set; }

    public Player(int id)
    {
        Id = id;
    }
}






