using Cricket.Commons;
using Cricket.Domain;

namespace Cricket.Generator;

public class ShotOutcomeGenerator
{
       private static readonly string[] BattingCards = {"Straight", "Flick", "LegLance", "LongOn", "SquareCut", "Sweep", "CoverDrive", "Pull", "Scoop", "UpperCut"};
    private static readonly string[] ShotTimings = { "Early", "Good", "Perfect", "Late" };

    public Dictionary<string, ShotOutcome> Generate()
    {
        var shotOutcomes = new Dictionary<string, ShotOutcome>();
        var index = 0;
        foreach (var bowlingCard in Constants.BowlingCards)
        {
            foreach (var battingCard in BattingCards)
            {
                foreach (var shotTiming in ShotTimings)
                {
                    index++;
                    shotOutcomes.Add($"{bowlingCard}_{battingCard}_{shotTiming}", new ShotOutcome(index));
                }
            }
        }

        return shotOutcomes;
    }
 }