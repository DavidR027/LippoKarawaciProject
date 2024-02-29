# Jawaban Algoritma dan Database Query
-Algoritma #1-
```
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
```
-Algoritma #2 A-
```
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
```
-Algoritma #2 B-
```
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
```
-Algoritma #2 C-
```
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
```
-Algoritma #2 D-
```
using System;

public class Program
{
    public static void Main()
    {
        Console.Write("Masukkan nilai n: ");
        int nilai = Convert.ToInt32(Console.ReadLine());

        int[,] matrix = new int[nilai, nilai];
        int currentNumber = 1;
        bool isAscending = true;
        int row = 0;

        for (int i = 0; i < nilai; i++)
        {
            if (isAscending)
            {
                for (int j = 0; j < nilai; j++)
                {
                    matrix[j, row] = currentNumber++;
                }
                isAscending = false;
            }
            else
            {
                for (int j = nilai - 1; j >= 0; j--)
                {
                    matrix[j, row] = currentNumber++;
                }
                isAscending = true;
            }
            row++;
        }

        
        for (int i = 0; i < nilai; i++)
        {
            for (int j = 0; j < nilai; j++)
            {
                Console.Write(matrix[i, j] + "\t");
            }
            Console.WriteLine();
        }
    }
}
```
-Database Query #1-
```
SELECT COUNT(Field1), Field2
FROM Table1
GROUP BY Field2
HAVING COUNT(Field1) > 1
ORDER BY COUNT(Field1) ASC;
```
-Database Query #2-
```
SELECT TableB.field
FROM TableA
Right JOIN TableB ON TableA.id = TableB.id
WHERE TableA.field IS NULL;
```
