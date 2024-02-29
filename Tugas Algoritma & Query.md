# Jawaban Algoritma dan Database Query
-Algoritma #1-

using System;
					
public class Program
{
	public static void Main()
	{
		Console.Write("Enter numbers: ");
        string input = Console.ReadLine();

        string[] numberStrings = input.Split(',');
		
		var total = 0;
		for(int i = 0; i<numberStrings.Length; i++){
			if (Int32.Parse(numberStrings[i]) == 8){
				total = total + 5;
			}else if (Int32.Parse(numberStrings[i])%2 == 0){
				total = total + 1;
			} else {
				total = total + 3;					
			}
		}
		
		Console.Write("Total: "+total);
	}
}

-Algoritma #2 A-

public class Program
{
	public static void Main()
	{
	Console.Write("Masukkan nilai n: ");
        int nilai = Convert.ToInt32(Console.ReadLine());

        for (int i = 1; i <= nilai; i++)
        {
            for (int j = 1; j <= i; j++)
            {
                Console.Write(i);
            }

            Console.WriteLine();
        }
	}
}

-Algoritma #2 B-

public class Program
{
	public static void Main()
	{
	Console.Write("Masukkan nilai n: ");
        int nilai = Convert.ToInt32(Console.ReadLine());

        for (int i = 1; i <= nilai; i++)
        {
            for (int j = i; j >= 1; j--)
            {
                Console.Write(j);
            }

            Console.WriteLine();
        }
	}
}

-Algoritma #2 C-

using System;

public class Program
{
    public static void Main()
    {
        Console.Write("Masukkan nilai n: ");
        int nilai = Convert.ToInt32(Console.ReadLine());
        int k = 0;
        bool check = true;

        for (int i = 1; i <= nilai; i++)
        {
            for (int j = 1; j <= i; j++)
            {
                if (check && k < nilai)
                {
                    k++;
                }
                else if (!check && k >= 1)
                {
                    k--;
                }

                Console.Write(k);
		if (k == nilai)
		{
		    check = false;
		}
		    else if (k == 1)
		{
		    check = true;
		}
            }

            Console.WriteLine();
        }
    }
}
