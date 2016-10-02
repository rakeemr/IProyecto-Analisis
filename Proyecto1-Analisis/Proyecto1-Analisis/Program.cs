using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1_Analisis
{
    class Program
    {
        public static int vertices = 4;
        public static int[,] antecesors = new int[vertices,vertices];
        public static int[,] distance = {   { 0, 5, 999, 999 }, { 50, 0, 15, 5 },
                                            { 30, 999, 0, 15 }, { 15, 999, 5, 0 } };

        public static int[,] fillMatrix ()
        {
            int[,] graph = new int[vertices, vertices];
            for (int i = 0; i < vertices; i++)
            {
                for (int j = 0; j < vertices; j++)
                {
                    Console.WriteLine("Type de distance for the path {0},{1}: ", i,j);
                    int d = Convert.ToInt32(Console.ReadLine());
                    graph[i, j] = d;
                }
            }
            return graph;
        }

        private static void Print(int[,] distance, int verticesCount)
        {
            for (int a = 0; a < verticesCount; ++a)
                Console.Write("\t" + a.ToString());
            Console.WriteLine();
            for (int b = 0; b < 4; ++b)
                Console.Write(" ");
            for (int c = 0; c < 31; ++c)
                Console.Write("-");
            for (int i = 0; i < verticesCount; ++i)
            {
                Console.Write("\n " + i + " |\t");
                for (int j = 0; j < verticesCount; ++j)
                {
                    Console.Write(distance[i, j].ToString() + "\t");
                }
                Console.WriteLine();
            }
            Console.WriteLine("\n\n");
        }

        public static void FloydWarshall(int[,] graph, int verticesCount)
        {
            int[,] distance = new int[verticesCount, verticesCount];

            for (int i = 0; i < verticesCount; ++i)
                for (int j = 0; j < verticesCount; ++j)
                    distance[i, j] = graph[i, j];

            for (int k = 0; k < verticesCount; ++k)
            {
                for (int i = 0; i < verticesCount; ++i)
                {
                    for (int j = 0; j < verticesCount; ++j)
                    {
                        if (distance[i, k] + distance[k, j] < distance[i, j])
                        {
                            distance[i, j] = distance[i, k] + distance[k, j];
                            antecesors[i, j] = k;
                        }
                    }
                }
            }

            Print(distance, verticesCount);
        }

        private static void dataRequest()
        {
            Console.WriteLine("\t\t*********************");
            Console.WriteLine("\t\t*    BIENVENIDO     *");
            Console.WriteLine("\t\t*********************");
            string op;
            do
            {
                Console.WriteLine("\n1)Fill the matrix of distances.\n2)Modify the distance from one path.");
                Console.WriteLine("3)Find the short distance graph and they're antecesors.\n4)Find the short distance by jumps.");
                Console.WriteLine("5)Press 0 to Exit.");
                Console.Write("Type the option you want: ");
                op = Console.ReadLine();
                if (op == "1")
                {
                    Console.Write("Insert the amount of vertices: ");
                    vertices = Convert.ToInt32(Console.ReadLine());
                    int[,] temp = fillMatrix();
                    Print(temp,vertices);
                }
                if (op == "2")
                {
                    Console.WriteLine();
                    Console.Write("Type the initial vertex: ");
                    int i = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Type the ending vertex: ");
                    int e = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Type the new distance: ");
                    int m = Convert.ToInt32(Console.ReadLine());
                    distance[i, e] = m;
                    Print(distance, vertices);
                }
                if (op == "3")
                {
                    FloydWarshall(distance, vertices);
                    Print(antecesors, vertices);
                }
                if (op == "4")
                {
                    Console.WriteLine("Building...");
                }
            } while (op != "0");
        }

        static void Main(string[] args)
        {
            dataRequest();
            

            /*Console.WriteLine("Matrix to find the shortest path of:");
            Print(distance,4);
            Console.WriteLine("Shortest distances between every pair of vertices:");
            FloydWarshall(distance, 4);
            Console.WriteLine("Path Matrix:");
            Print(P, 4);*/


        }
    }
}
