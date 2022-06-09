using System;
using System.Collections.Generic;
using System.Linq;
using Cricket.Controllers;
using Cricket.Domain;
using Cricket.Services;
using JOIEnergy.Domain;
using Xunit;

namespace CricketTest;

public class BallOutcomeServiceTest
{
    private Dictionary<string, ShotOutcome> ShotOutcomes { get; }
    public List<IMatch> Matches { get; }
    
    public BallOutcomeServiceTest()
    {
        ShotOutcomes = GetShotOutcomes();
        Matches = new List<IMatch>();
        var match = new Match(1,2,1);
        match.CreateInnings(new InningsParameter()
        {
            Name = "Tet innings",
            BattingTeamId = 1,
            BowlingTeamId = 2
        });
        match.CreateInnings(new SuperOverParameter()
        {
            Name = "SuperOver",
            Target = 10,
            BattingTeamId = 1,
            BowlingTeamId = 2
        }, true);
        Matches.Add(match);
      
    }

    private Dictionary<string, ShotOutcome> GetShotOutcomes()
    {
        var shotOutcomes = new Dictionary<string, ShotOutcome>();
        shotOutcomes.Add("Bowl1_Bat_Time", new ShotOutcome(4));
        shotOutcomes.Add("Bowl2_Bat_Time", new ShotOutcome(5));
        return shotOutcomes;
    }

    [Fact]
    public void ShouldReturnNullIfBallsAreMoreThan20InNotSuperOver()
    {
        var service = new BallOutcomeService(Matches, ShotOutcomes);
        var ballParameter = new BallParameter()
            { BattingType = "Bat", BowlingType = "Bowl1", ShotTiming = "Time" };
        for (var i = 0; i < 20; i++){
            service.Create(ballParameter,Matches[0].Innings[0]);
        }

        var shotOutcome = service.Create(ballParameter, Matches[0].Innings[0]);
        Assert.Null(shotOutcome);
    }
    [Fact]
    public void ShouldReturnNullIfBallsAreMoreThan6InSuperOver()
    {
        var service = new BallOutcomeService(Matches, ShotOutcomes);
        var ballParameter = new BallParameter()
            { BattingType = "Bat", BowlingType = "Bowl1", ShotTiming = "Time" };
        for (var i = 0; i < 7; i++){
            service.Create(ballParameter,Matches[0].Innings[1]);
        }

        var shotOutcome = service.Create(ballParameter, Matches[0].Innings[1]);
        Assert.Null(shotOutcome);
    }
    [Fact]
    public void ShouldReturn4Run()
    {
        var service = new BallOutcomeService(Matches, ShotOutcomes);
        var shotOutcome = service.Create(new BallParameter()
            { BattingType = "Bat", BowlingType = "Bowl1", ShotTiming = "Time" }, Matches[0].Innings[0]);
        Assert.NotNull(shotOutcome);
        Assert.Equal(4, shotOutcome.Run);
    }
    [Fact]
    public void ShouldReturnWicketIfItsWicket()
    {
        var service = new BallOutcomeService(Matches, ShotOutcomes);
        var shotOutcome = service.Create(new BallParameter()
            { BattingType = "Bat", BowlingType = "Bowl2", ShotTiming = "Time" }, Matches[0].Innings[0]);
        Assert.True(shotOutcome.IsWicket);
    }
    
    [Fact]
    public void ShouldEndMatchAfterTenWickets()
    {
        var service = new BallOutcomeService(Matches, ShotOutcomes);
        for (int i = 0; i < 20; i++)
        {
            service.Create(new BallParameter()
                { BattingType = "Bat", BowlingType = "Bowl2", ShotTiming = "Time" }, Matches[0].Innings[0]);
        }
        Assert.Equal(10, Matches[0].Innings[0].BallDetails.Count(x => x.ShotOutcome.IsWicket));
        Assert.Equal(10, Matches[0].Innings[0].BallDetails.Count);
    }

    [Fact]
    public void ShouldThrowExceptionWhnInvalidParameter()
    {
        var service = new BallOutcomeService(Matches, ShotOutcomes);
        Assert.Throws<KeyNotFoundException>(() =>
            {
                return service.Create(new BallParameter()
                    { BattingType = "Bat", BowlingType = "Bowl2", ShotTiming = "Time1" }, Matches[0].Innings[0]);
            }
        );
    }
}