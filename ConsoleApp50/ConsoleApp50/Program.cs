namespace RPG
{
    public class Program
    {
        static void Main(string[] args)
        {
            Player player = new Knight("M");
            GameManager gameManager = new GameManager(player);
            gameManager.StartGame();
        }

  
    }
}
