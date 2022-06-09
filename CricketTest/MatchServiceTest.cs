using System.Collections.Generic;
using Cricket.Domain;
using Cricket.Generator;
using Cricket.Services;
using JOIEnergy.Domain;
using Xunit;

namespace CricketTest;

public class MatchServiceTest
{
    private List<IMatch> Matches { get; }
    private List<ITeam> Teams { get; }

    private MatchParameter _validMatchParam { get; }

    public MatchServiceTest()
    {
        Teams = new TeamGenerator().Generate();
        Matches = new List<IMatch>();
        _validMatchParam = new MatchParameter()
        {
            Name = "Match_Test",
            TeamOneId = 1,
            TeamTwoId = 2
        };
    }
    
    [Fact]
    public void ShouldReturnAMatchWithValidParameter()
    {
        var service = new MatchService(Matches, Teams);
        var match = service.Create(_validMatchParam);
        Assert.NotNull(match);
    }
    
    [Fact]
    public void ShouldReturnValidCountOnAddingMatch()
    {
        var service = new MatchService(Matches, Teams);
        service.Create(_validMatchParam);
        Assert.Equal(1, Matches.Count);
    }
    
    [Fact]
    public void ShouldAutoIncrementGeneratedMatchIdWhenMatchIdIsNull()
    { 
        var service = new MatchService(Matches, Teams);
       var match1 = service.Create(_validMatchParam);
       var match2 = service.Create(_validMatchParam);
       Assert.Equal(match1.Id + 1, match2.Id);
    }
    [Fact]
    public void ShouldReturnValidMatchIdWhenNotNull()
    {
        var service = new MatchService(Matches, Teams);
        var match = service.Create(_validMatchParam, 123);
        Assert.Equal(123, match.Id);
    }
    
}