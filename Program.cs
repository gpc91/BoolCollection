using Gpc91;

public class Program
{
    public static void Main()
    {
        bool isHappy = true;
        bool isSad = false;
        bool isFast = true;
        bool isSlow = false;
        bool isAlive = true;
        bool isDead = false;
        bool canDance = false;
        bool canSwim = true;
        bool hasRhythm = false;
        bool hasArms = true;

        var col = BoolCollection.Create(isHappy, isSad, isFast, isSlow, isAlive, isDead, canDance, canSwim, hasRhythm, hasArms);
        
        Console.WriteLine(col);

        var enumerator = col.GetEnumerator();

        Console.WriteLine(col[5]);
        col[7] = true;
        Console.WriteLine(col[5]);
    }
}