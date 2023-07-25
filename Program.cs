public class Program
{
    static int[,] matrix;
    static int rowCount;
    static int colCount;

    public static void Main()
    {
        while (true)
        {
            try
            {
                Console.WriteLine("Enter the matrix:");
                var matrixValue = Console.ReadLine();

                matrix = ValidateAndConvertToArray(matrixValue);

                Console.WriteLine(CountAreasOfOne() + "\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n");
            }
        }
    }

    static int[,] ValidateAndConvertToArray(string matrixValue)
    {
        if (string.IsNullOrWhiteSpace(matrixValue))
        {
            throw new Exception("Matrix cannot be null!");
        }

        string[] rows = matrixValue.Split(';');
        rowCount = rows.Length;
        colCount = rows[0].Split(',').Length;

        if (rowCount > 100)
            throw new Exception("The number of rows should be a maximum of 100");

        if (colCount > 100)
            throw new Exception("The number of columns should be a maximum of 100");

        int[,] intArray = new int[rowCount, colCount];

        for (int i = 0; i < rowCount; i++)
        {
            string[] numbers = rows[i].Split(',');

            if (colCount != numbers.Length)
                throw new Exception("Not all rows contain an equal number of columns");

            for (int j = 0; j < colCount; j++)
            {
                if (numbers[j] != "1" && numbers[j] != "0")
                {
                    throw new Exception("The matrix should consist only of 1 and 0 values");
                }

                intArray[i, j] = int.Parse(numbers[j]);
            }
        }

        return intArray;
    }

    static int CountAreasOfOne()
    {
        int count = 0;

        for (int row = 0; row < rowCount; row++)
        {
            for (int col = 0; col < colCount; col++)
            {
                if (matrix[row, col] == 1)
                {
                    count++;
                    DepthFirstSearch(row, col);
                }
            }
        }

        return count;
    }

    static void DepthFirstSearch(int row, int col)
    {
        if (row < 0 || row >= rowCount || col < 0 || col >= colCount || matrix[row, col] != 1)
            return;

        matrix[row, col] = -1;

        DepthFirstSearch(row + 1, col);
        DepthFirstSearch(row - 1, col);
        DepthFirstSearch(row, col + 1);
        DepthFirstSearch(row, col - 1);
    }
}