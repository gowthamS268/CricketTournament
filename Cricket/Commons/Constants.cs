namespace Cricket.Commons;

public static class Constants {
    public static string[] runCommentryConfigs = new string[]{
        "just Over the filder", "Excellent Effort on the boundary" ,"Nice hit", "Excellent Line and Length", "Nice cover drive", "Over the ground"
    };

    public static string wicketCommentry = "Its a Wicket!!";
    
    public static string[] BowlingCards = {"Bouncer", "InSwinger", "OutSwinger", "OffCutter", "LegCutter", "DSlowerBall", "Pace", "Doosra", "Yorker","OffBreak"};

    private static long _matchId = 1;

    public static long GetMatchId()
    {
        return _matchId++;
    }
}