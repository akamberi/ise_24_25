class Program
{
    enum FOUND_ON_HIT
    {
        FIRST = 1,
        SECOND = 2,
        OTHER = 3
    }
    static int NO_LIFES = 3;
    static Random randomGenerator = new Random();
    static Dictionary<FOUND_ON_HIT, int> scores = new Dictionary<FOUND_ON_HIT, int>();

    static void DrawMenu()
    {
        Console.WriteLine("1. Luaj");
        Console.WriteLine("2. Shiko historikun");
        Console.WriteLine("0. Dil");
    }

    static bool Evaluate(int userSelection, int randomNumber, int attemptNo)
    {
        if (userSelection == randomNumber)
        {
            if (attemptNo == 1)
            {
                scores[FOUND_ON_HIT.FIRST]++;
                NO_LIFES += 2;
            }
            else if (attemptNo == 2)
            {
                scores[FOUND_ON_HIT.SECOND]++;
                NO_LIFES++;
            }
            else
            {
                scores[FOUND_ON_HIT.OTHER]++;
                NO_LIFES--;
            }
            return true;
        }
        else
        {
            Console.WriteLine("Gabim! Tentativa juaj eshte " + attemptNo);
            return false;
        }
    }

    static void DrawScores()
    {
        Console.WriteLine("===SCORES===");
        foreach (var score in scores)
        {
            Console.WriteLine($"{score.Key}: {score.Value}");
        }
    }

    static void Main(string[] args)
    {
        scores.Add(FOUND_ON_HIT.FIRST, 0);
        scores.Add(FOUND_ON_HIT.SECOND, 0);
        scores.Add(FOUND_ON_HIT.OTHER, 0);
        while (true)
        {
            DrawMenu();
            int userSelection = int.Parse(Console.ReadLine());
            switch (userSelection)
            {
                case 1:
                    if (NO_LIFES <= 0)
                    {
                        Console.WriteLine("Ju nuk keni me jete te mbetura!");
                        break;
                    }
                    NO_LIFES--;
                    int randomNumber = randomGenerator.Next(1, 100);
                    int attemptNo = 0;
                    int userNumber;
                    do
                    {
                        Console.WriteLine("Jepni nje numer nga 1 deri ne 100");
                        userNumber = int.Parse(Console.ReadLine());
                        attemptNo++;
                    } while (!Evaluate(userNumber, randomNumber, attemptNo));
                    Console.WriteLine("Urime! Ju e keni gjetur!");
                    break;
                case 2:
                    DrawScores();
                    break;
                case 0:
                    return;
                default:
                    Console.WriteLine("Ju lutem jepni nje vlere valide");
                    break;
            }
        }
    }
}
