using Cricket.Commons;

namespace Cricket.Domain;

public class ShotOutcome
{
    public string Commentry { get; }
    public int Run { get; set; }
    public bool IsWicket { get; }

    public ShotOutcome(int randomNumber)
    {
        IsWicket= randomNumber % 5 == 0;
        if (IsWicket)
        {
            Run = 0;
            Commentry = Constants.wicketCommentry;
        }
        else
        {
            Run = randomNumber%7;
            Commentry = Constants.runCommentryConfigs[randomNumber % 6];
        }
        
    }
}