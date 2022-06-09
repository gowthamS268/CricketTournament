using Cricket.Domain;
using Cricket.Enums;

namespace Cricket.Generator;

public class TeamGenerator
{
    private string[] TeamNames = { "Team A", "Team B" };
    public List<ITeam> Generate()
    {
        var teams = new List<ITeam>();
        var index = 1;
        foreach (var teamName  in TeamNames)
        {
            var team = new Team(index++);
            team.Name = teamName;
            AddPlayers(team);
            teams.Add(team);
        }

        return teams;
    }

    private void AddPlayers(ITeam team)
    {
        for (var i = 1; i <= 11; i++)
        {
            team.AddPlayer(i, $"Player_{i.ToString()}", BattingRole.LeftArmBat, BowlingRole.LeftArmSpin);
        }
    }
    
}