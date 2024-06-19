class Point
{
    public int Player1Score { get; }
    public int Player1Sets { get; }
    public int Player2Score { get; }
    public int Player2Sets { get; }
    public Player Server { get; }

    public Point(int player1Score, int player1sets, int player2Score, int player2Sets, Player server)
    {
        Player1Score = player1Score;
        Player1Sets = player1sets;
        Player2Score = player2Score;
        Player2Sets = player2Sets;
        Server = server;
    }
}