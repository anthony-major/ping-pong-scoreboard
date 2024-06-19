using System.Collections;

class Game
{
    public Player Player1 { get; } = new(){IsServing = true, StartedSet = true};
    public Player Player2 { get; } = new();

    private Player Server
    {
        get
        {
            return Player1.IsServing ? Player1 : Player2;
        }
    }

    private Stack<Point> pointHistory = new();

    public void UpdateScore(Player player)
    {
        pointHistory.Push(new(Player1.Score, Player1.Sets, Player2.Score, Player2.Sets, Server));
        player.Score++;
        TryChangeServer();
        var winner = CheckForWin();
        if (winner != null)
        {
            winner.Sets++;
            Reset();
        }
    }

    public void UndoPoint()
    {
        if (pointHistory.Count == 0)
        {
            return;
        }

        var previousPoint = pointHistory.Pop();

        Player1.Score = previousPoint.Player1Score;
        Player1.Sets = previousPoint.Player1Sets;
        Player1.IsServing = previousPoint.Server == Player1;
        
        Player2.Score = previousPoint.Player2Score;
        Player2.Sets = previousPoint.Player2Sets;
        Player2.IsServing = previousPoint.Server == Player2;
    }

    public void ResetAll()
    {
        pointHistory.Push(new(Player1.Score, Player1.Sets, Player2.Score, Player2.Sets, Server));

        Player1.Score = 0;
        Player1.Sets = 0;
        Player1.IsServing = true;
        Player1.StartedSet = true;

        Player2.Score = 0;
        Player2.Sets = 0;
        Player2.IsServing = false;
        Player2.StartedSet = false;
    }

    private void Reset()
    {
        Player1.Score = 0;
        Player1.IsServing = !Player1.StartedSet;
        Player1.StartedSet = !Player1.StartedSet;

        Player2.Score = 0;
        Player2.IsServing = !Player2.StartedSet;
        Player2.StartedSet = !Player2.StartedSet;
    }

    private void SwitchServer()
    {
        Player1.IsServing = !Player1.IsServing;
        Player2.IsServing = !Player2.IsServing;   
    }

    private void TryChangeServer()
    {
        if ((Player1.Score + Player2.Score) >= 20)
        {
            SwitchServer();
        }
        else if ((Player1.Score + Player2.Score) % 2 == 0)
        {
            SwitchServer();
        }
    }

    private Player? CheckForWin()
    {
        if (Player1.Score < 11 && Player2.Score < 11)
        {
            return null;
        }
        if (Player1.Score >= Player2.Score + 2)
        {
            return Player1;
        }
        if (Player2.Score >= Player1.Score + 2)
        {
            return Player2;
        }

        return null;
    }
}