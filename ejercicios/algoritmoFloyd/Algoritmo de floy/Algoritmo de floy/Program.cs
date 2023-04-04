using System;

class FloydAlgorithm
{
    static void Main()
    {
        int[,] matrix = {
            {0, 5, int.MaxValue, 10},
            {int.MaxValue, 0, 3, int.MaxValue},
            {int.MaxValue, int.MaxValue, 0, 1},
            {int.MaxValue, int.MaxValue, int.MaxValue, 0}
        };

        int n = matrix.GetLength(0);

        // Inicializar la matriz de distancias
        int[,] distances = new int[n, n];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                distances[i, j] = matrix[i, j];
            }
        }

        // Ejecutar el algoritmo de Floyd
        for (int k = 0; k < n; k++)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (distances[i, k] != int.MaxValue && distances[k, j] != int.MaxValue
                        && distances[i, k] + distances[k, j] < distances[i, j])
                    {
                        distances[i, j] = distances[i, k] + distances[k, j];
                    }
                }
            }
        }

        // Imprimir la matriz de distancias resultante
        Console.WriteLine("Matriz de distancias resultante:");
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (distances[i, j] == int.MaxValue)
                {
                    Console.Write("inf".PadLeft(5));
                }
                else
                {
                    Console.Write(distances[i, j].ToString().PadLeft(5));
                }
            }
            Console.WriteLine();
        }
    }
}
