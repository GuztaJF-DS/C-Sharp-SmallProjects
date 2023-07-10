namespace Triangle
{
    internal class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 10; j > i; j--)
                {
                    Console.Write(' ');
                }
                for (int k = 0; k < 2; k++)
                {
                    for (int a = 0; a < i; a++)
                    {
                        Console.Write('.');
                    }
                }
                Console.WriteLine(".");
            }
        }
    }
}