using System;

namespace HangmanSharp
{
    internal class Program
    {
        static public void PrintGame(int[,] spaces)
        {
            for (int y = 0; y < spaces.GetLength(1); y++)
            {
                for (int x = 0; x < spaces.GetLength(0); x++)
                {
                    if (spaces[y, x] == 0)
                    {
                        Console.Write("| ");
                    }
                    else
                    {
                        string PrintPiece = spaces[y, x] % 2 == 0 ? "O" : "X";
                        Console.Write("|" + PrintPiece);
                    }
                }
                Console.Write("| \n");
            }
        }
        static public int CheckPlace(int[,] spaces, int y, bool isY)
        {
            int ToChange;
            bool validNumber = int.TryParse(Console.ReadLine(), out ToChange);

            if (!validNumber || ToChange > 3 || ToChange < 1)
            {
                Console.WriteLine("Invalid option!");
                ToChange = CheckPlace(spaces, y, isY);
            }
            if(!isY && spaces[y-1,ToChange-1]!= 0)
            {
                Console.WriteLine("Already selected, choose another!");
                ToChange = CheckPlace(spaces, y, isY);
            }
            if(isY) {
                int[] YArr = { spaces[ToChange-1, 0], spaces[ToChange-1, 1],spaces[ToChange-1, 2] };
                bool AlreadyUsed = YArr.ToList().All(x => x != 0);
                if (AlreadyUsed)
                {
                    Console.WriteLine("No avaliable spaces, choose another!");
                    ToChange = CheckPlace(spaces, y, isY);
                }
            }
            return ToChange;
        }
        static public int[,] MovePiece(int[,] spaces, int Player){
            int XToChange, YToChange;
            Console.WriteLine("Select Y Coordenate:");
            YToChange = CheckPlace(spaces, 0, true);
            Console.WriteLine("Select X Coordenate:");
            XToChange = CheckPlace(spaces, YToChange, false);

            spaces[YToChange - 1, XToChange - 1] = Player;
            return spaces;
        }

        static public bool CheckWinner(int[,] spaces, int player)
        {
            for (int y = 0; y < spaces.GetLength(1); y++)
            {
                int[] horizontalArr = { spaces[y,0], spaces[y, 1],spaces[y,2] };
                bool HorizontalWin = horizontalArr.ToList().All(x => x == 1);
                if (HorizontalWin)
                {
                    return true;
                }

                int[] VeticalArr = { spaces[0, y], spaces[1, y], spaces[2, y] };
                bool VeticalWin = VeticalArr.ToList().All(x => x == 1);
                if (VeticalWin)
                {
                    return true;
                }
            }
            int[] CrossedArr1 = { spaces[0,0], spaces[1,1], spaces[2,2] };
            bool CrossedWin1 = CrossedArr1.ToList().All(x => x == 1);
            if (CrossedWin1)
            {
                return true;
            }

            int[] CrossedArr2 = { spaces[0,2], spaces[1,1], spaces[2,0] };
            bool CrossedWin2 = CrossedArr2.ToList().All(x => x == 1);
            if (CrossedWin2)
            {
                return true;
            }

            return false;
        }
        static void Main(string[] args)
        {
            int[,] space = new int[,] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
            for(int x = 0; x < 9;)
            {
                int player = x % 2 == 0 ? 1 : 2;
                Console.WriteLine($"Player {player}'s Turn");
                PrintGame(space);
                space = MovePiece(space, player);
                if (x > 3)
                {
                    if (CheckWinner(space, player))
                    {
                        Console.WriteLine($"Player {player} Win!");
                        break;
                    }

                }
                if(x==8)
                {
                    Console.WriteLine("Draw");
                }
                x++;
            }
            PrintGame(space);
        }
    }
}