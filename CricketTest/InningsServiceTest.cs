using System.Collections.Generic;
using Cricket.Domain;
using Cricket.Enums;
using Cricket.Generator;
using Cricket.Services;
using JOIEnergy.Domain;
using Xunit;

namespace CricketTest;

public class InningsServiceTest
{
    private Dictionary<string, ShotOutcome> ShotOutcomes { get; }
    private List<IMatch> Matches { get; }
    private List<ITeam> Teams { get; }
    private InningsParameter _inningsParameter;
    private SuperOverParameter _superOverParameter;
    
    public InningsServiceTest()
    {
        ShotOutcomes = new ShotOutcomeGenerator().Generate();
        Teams = new TeamGenerator().Generate();
        _inningsParameter =  new InningsParameter()
        {
            Name = "Innings 1",
            BattingTeamId = 1,
            BowlingTeamId = 2
        };
        _superOverParameter = new SuperOverParameter()
        {
            Name = "SuperOver",
            BattingTeamId = 1,
            BowlingTeamId = 2,
            Target = 5,
            Shots = GetShotParameters()
        };
        
        Matches = new List<IMatch>(){new Match(1,2, 1)};
    }

    private ShotParameter[] GetShotParameters()
    {
        var shotParams = new ShotParameter[6];
        for (var i = 0; i < 6; i++)
        {
            shotParams[i] = new ShotParameter() { BattingType = "CoverDrive", ShotTiming = "Good" };
        }

        return shotParams;
    }
    
    private Dictionary<string, ShotOutcome> GetShotOutcomes()
    {
        var shotOutcomes = new Dictionary<string, ShotOutcome>();
        shotOutcomes.Add("Bowl1_Bat_Time", new ShotOutcome(4));
        shotOutcomes.Add("Bowl2_Bat_Time", new ShotOutcome(5));
        return shotOutcomes;
    }

    
    [Fact]
    public void ShouldReturnFirstInningsWithId1()
    {
        var service = new InningsService(Matches, Teams, ShotOutcomes);
       var innings = service.Create(_inningsParameter, Matches[0]);
       Assert.Equal(1, innings.Id);
    }
    
    [Fact]
    public void ShouldReturnFirstInningsWithGivenName()
    {
        var service = new InningsService(Matches, Teams, ShotOutcomes);
        var innings = service.Create(_inningsParameter, Matches[0]);
        Assert.Equal( "Innings 1", innings.Name);
    }
    
    [Fact]
    public void ShouldReturnNormalInningsByDefault()
    {
        var service = new InningsService(Matches, Teams, ShotOutcomes);
        var innings = service.Create(_inningsParameter, Matches[0]);
        Assert.False(innings.IsSuperOver);
    }
    
    [Fact]
    public void ShouldReturnSecondInningsWithId2()
    {
        var service = new InningsService(Matches, Teams, ShotOutcomes);
        service.Create(_inningsParameter, Matches[0]);
        var innings = service.Create(_inningsParameter, Matches[0]);
        Assert.Equal(2, innings.Id);
    }
    
    [Fact]
    public void ShouldReturnNullWhenMoreThan2InningsCreated()
    {
        var service = new InningsService(Matches, Teams, ShotOutcomes);
        service.Create(_inningsParameter, Matches[0]);
        service.Create(_inningsParameter, Matches[0]);
        var innings = service.Create(_inningsParameter, Matches[0]);
        Assert.Null(innings);
    }
    
    [Fact]
    public void ShouldReturnValidInningsCount()
    {
        var service = new InningsService(Matches, Teams, ShotOutcomes);
        service.Create(_inningsParameter, Matches[0]);
        Assert.Equal(1, Matches[0].Innings.Count);
    }
    
    [Fact]
    public void ShouldReturnSuperOverInningsWhenCreateCallingSuperOver()
    {
        var service = new InningsService(Matches, Teams, ShotOutcomes);
        var innings = service.CreateSuperOver(_superOverParameter, Matches[0]);
        Assert.True(innings.IsSuperOver);
    }
    
    [Fact]
    public void ShouldReturnFirstSuperOverInningsWhenCreateCallingSuperOverWithId1()
    {
        var service = new InningsService(Matches, Teams, ShotOutcomes);
        var innings = service.CreateSuperOver(_superOverParameter, Matches[0]);
        Assert.Equal(1, innings.Id);
    }
    
    [Fact]
    public void ShouldReturnFirstSuperOverInningsWhenCreateCallingSuperOverWithGivenName()
    {
        var service = new InningsService(Matches, Teams, ShotOutcomes);
        var innings = service.CreateSuperOver(_superOverParameter, Matches[0]);
        Assert.Equal("SuperOver", innings.Name);
    }
    
    [Fact]
    public void ShouldReturnNullWhenMoreThan2SuperOverInningsCreated()
    {
        var service = new InningsService(Matches, Teams, ShotOutcomes);
        service.CreateSuperOver(_superOverParameter, Matches[0]);
        service.CreateSuperOver(_superOverParameter, Matches[0]);
        var innings = service.CreateSuperOver(_superOverParameter, Matches[0]);
        Assert.Null(innings);
    }
    
    [Fact]
    public void ShouldReturnWonResultWhenSuperOverIsPlayed()
    {
        var service = new InningsService(Matches, Teams, ShotOutcomes);
        var innings = service.CreateSuperOver(_superOverParameter, Matches[0]);
        Assert.NotNull(innings);
        if (innings != null)
        {
            var result = service.PlaySuperOver(_superOverParameter, innings);
            Assert.Equal("Team A Won", result);
        }
        
    }
    
    [Fact]
    public void ShouldReturnResultWith2BallsWhenSuperOver()
    {
        var service = new InningsService(Matches, Teams, ShotOutcomes);
        var innings = service.CreateSuperOver(_superOverParameter, Matches[0]);
        Assert.NotNull(innings);
        if (innings != null)
        {
            var result = service.PlaySuperOver(_superOverParameter, innings);
            Assert.Equal(2, innings.BallDetails.Count);
        }
        
    }
    
    [Fact]
    public void ShouldReturnTeamBWonResultWhenSuperOverIsPlayedWithHugeTotal()
    {
        var service = new InningsService(Matches, Teams, ShotOutcomes);
        _superOverParameter.Target = 25;
        var innings = service.CreateSuperOver(_superOverParameter, Matches[0]);
        Assert.NotNull(innings);
        if (innings != null)
        {
            var result = service.PlaySuperOver(_superOverParameter, innings);
            Assert.Equal("Team B Won", result);
        }
    }
    [Fact]
    public void ShouldBowlAll6BallWhenSuperOverTargetIsNotReached()
    {
        var service = new InningsService(Matches, Teams, ShotOutcomes);
        _superOverParameter.Target = 25;
        var innings = service.CreateSuperOver(_superOverParameter, Matches[0]);
        Assert.NotNull(innings);
        if (innings != null)
        {
            var result = service.PlaySuperOver(_superOverParameter, innings);
            Assert.Equal(6, innings.BallDetails.Count);
        }
    }
    
    [Fact]
    public void ShouldResulInTeamBWonWhen2WicketsGone()
    {
        var service = new InningsService(Matches, Teams, ShotOutcomes);
        _superOverParameter.Target = 25;
        var shorts = new ShotParameter[2];
        shorts[0] = new ShotParameter() { BattingType = "Flick", ShotTiming = "Early"};
        shorts[1] = new ShotParameter() { BattingType = "Flick", ShotTiming = "Early"};
        _superOverParameter.Shots = shorts;
        var innings = service.CreateSuperOver(_superOverParameter, Matches[0]);
        Assert.NotNull(innings);
        if (innings != null)
        {
            var result = service.PlaySuperOver(_superOverParameter, innings);
            Assert.Equal("Team B Won", result);
            Assert.Equal(2, innings.BallDetails.Count);
        }
    }
    
    [Fact]
    public void ShouldResultInTiedWhenBothTeamsScoreSameRun()
    {
        var service = new InningsService(Matches, Teams, ShotOutcomes);
        _superOverParameter.Target = 18;
        var innings = service.CreateSuperOver(_superOverParameter, Matches[0]);
        Assert.NotNull(innings);
        if (innings != null)
        {
            var result = service.PlaySuperOver(_superOverParameter, innings);
            Assert.Equal(18, innings.Score);
            Assert.Equal("Match Tied", result);
        }
    }
}