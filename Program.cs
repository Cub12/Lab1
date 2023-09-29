namespace Lab1
{
    public class GameAccount
    {
        public string UserName { get; }
        public decimal CurrentRating { get; set; }
        private int GamesCount { get; set; }  
        // ReSharper disable once InconsistentNaming
        private readonly List<Game> allGames = new();

        public GameAccount(string name, decimal rating)
        {
            if (rating < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(rating), "Rating must be >= 1");
            }
            
            UserName = name;
            CurrentRating = rating;
        }

        public void WinGame(string opponentName, decimal changeOfRating)
        {
            if (changeOfRating < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(changeOfRating), "Rating for which playing cannot " 
                                                                              + "be negative");
            }

            var win = new Game(opponentName, true, changeOfRating);
            allGames.Add(win);
            CurrentRating += changeOfRating;
            GamesCount++;
        }
        
        public void LoseGame(string opponentName, decimal changeOfRating)
        {
            if (changeOfRating < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(changeOfRating), "Rating for which playing cannot " 
                                                                              + "be negative");
            }

            if (CurrentRating - changeOfRating < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(CurrentRating), "Rating must be >= 1");
            }

            var lose = new Game(opponentName, false, changeOfRating);
            allGames.Add(lose);
            CurrentRating -= changeOfRating;
            GamesCount++;
        }

        public string GetStats()
        {
            var report = "";
            for (var i = 0; i < allGames.Count; i++)
            {
                var game = allGames[i];
                var result = game.Won ? "Win" : "Lose";
                report += $"Opponent: {game.Opponent}, Result: {result}, Change of Rating: " +
                          $"{game.ChangeOfRating}, Game Index: {i + 1}";
            }
            
            return report;
        }
    }

    public class Game
    { 
        public string Opponent { get; }
        public bool Won { get; }
        public decimal ChangeOfRating { get; }

        public Game(string opponent, bool won, decimal changeOfRating)
        {
            Opponent = opponent;
            Won = won;
            ChangeOfRating = changeOfRating;
        }
    }
    
    public abstract class Program
    {
        public static void Main()
        {
            var player1 = new GameAccount("George", 115);
            var player2 = new GameAccount("Lewis", 190);
            
            player1.WinGame(player2.UserName, 8);
            player2.LoseGame(player1.UserName, 3);
            
            Console.WriteLine($"\n{player1.UserName}'s Rating: {player1.CurrentRating}");
            Console.WriteLine($"{player2.UserName}'s Rating: {player2.CurrentRating}\n");
            
            Console.WriteLine($"{player1.UserName}'s Stats:");
            Console.WriteLine(player1.GetStats());

            Console.WriteLine($"{player2.UserName}'s Stats:");
            Console.WriteLine(player2.GetStats());
        }
    }
}