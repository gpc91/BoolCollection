using System.Runtime.InteropServices;
using Gpc91;

public class Program
{
    public static void Main()
    {
        MemoryUsage();
    }
    
    public static void MemoryUsage()
    {
        bool[] values = {true, false, false, true, false, true, true, true, false, true, false, false, true, false, true, true, false};

        BoolCollection collection = new BoolCollection();
        collection.Build(values);

        Console.WriteLine(sizeof(bool) * values.Length);
        Console.WriteLine(Marshal.SizeOf(collection.Collection));
        
        Console.WriteLine(collection);
    }

}