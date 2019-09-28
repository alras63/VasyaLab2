using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VasyaLab2
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();

            Console.WriteLine("Введите размерность матрицы");
            string stringN = Console.ReadLine();

            Console.WriteLine("Сколько шагов step вы хотите сделать?");
            string stepStr = Console.ReadLine();

            int N;
            int Step;
            Console.WriteLine();

       
            if (Int32.TryParse(stringN, out N) && Int32.TryParse(stepStr, out Step))
            {
                int[,] matr = new int[N, N];

                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < N; j++)
                    {
                        matr[i, j] = rnd.Next(-5, 5);
                        Console.Write("{0}\t", matr[i, j]);
                    }
                    Console.WriteLine();
                }

                int edge;
                int[] cur = new int[4*(N-1)]; //текущая маска, 4*(N-1) - количество элементов
                int[] shift = new int[4 * (N - 1)]; // маска для переставление
                for (int k = 0; k < N / 2; k++) // k < N/2 - количество элементов в строке, например N/2 для N=4 будет 2, k повторится три раза
                {
                    edge = N - 1 - k; 
                    //Снимем конкретный слой
                    int c = 0; 
                    for (int j = k; j < edge; j++)
                    {
                        cur[c] = matr[k, j];
                        c++;
                    }
                    for (int i = k; i < edge; i++)
                    {
                        cur[c] = matr[i, edge];
                        c++;
                    }
                    for (int j = edge; j >= k; j--)
                    {
                        cur[c] = matr[edge, j];
                        c++;
                    }
                    for (int i = edge - 1; i > k; i--)
                    {
                        cur[c] = matr[i, k];
                        c++;
                    }

                    //переставляем
                    for (int i = 0; i < 4 * (N - (2 * k) - 1); i++) // 4 * (N - (2 * k) - 1) - количество элементов  в слое
                    {
                        shift[(i + Step) % (4 * (N - (2 * k) - 1))] = cur[i];
                    }

                    //одеваем слой

                    int c1 = 0;
                    for (int j = k; j < edge; j++)
                    {
                        matr[k, j] = shift[c1];
                        c1++;
                    }
                    for (int i = k; i < edge; i++)
                    {
                        matr[i, edge] = shift[c1];
                        c1++;
                    }
                    for (int j = edge; j >= k; j--)
                    {
                        matr[edge, j] = shift[c1];
                        c1++;
                    }
                    for (int i = edge - 1; i > k; i--)
                    {
                        matr[i, k] = shift[c1];
                        c1++;
                    }
                } // переход на новый слой

                //Вывод результата
                Console.WriteLine("Результат");
                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < N; j++)
                       
                    {
                        Console.Write("{0}\t", matr[i, j]);
                    }
                    Console.WriteLine();
                }

            }
            else
            {
                Console.WriteLine("Введенное число некорректно");
            }
            Console.ReadLine();
        }
    }
}
