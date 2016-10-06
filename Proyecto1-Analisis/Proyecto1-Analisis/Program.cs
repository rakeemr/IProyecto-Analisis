using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1_Analisis
{
    class Program
    {
        public static bool aux, aux2;
        public static int jumps;
        public static int comparisons, assignments;
        public static int vertices = 4;

        /*Keeps the predecessors matrix*/
        public static int[,] antecesors = new int[vertices,vertices];

        /*4x4 Graph*/
        public static int[,] distanceMatrix = {     { 0, 5, 999, 999 }, 
                                                    { 50, 0, 15, 5 },
                                                    { 30, 999, 0, 15 }, 
                                                    { 15, 999, 5, 0 } };

        /*8x8 Graph*/
        public static int[,] distanceMatrix1 = {    { 0, 322, 947, 904, 532, 951, 639, 127 }, 
                                                    { 550, 0, 379, 672, 394, 827, 780, 197 },
                                                    { 781, 644, 0, 52, 963, 141, 216, 433 },
                                                    { 616, 907, 854, 0, 384, 986, 432, 555 },
                                                    { 34, 660, 717, 3, 0, 969, 563, 379 },
                                                    { 330, 326, 326, 529, 756, 0, 927, 669 },
                                                    { 883, 806, 721, 921, 270, 989, 0, 280 },
                                                    { 818, 51, 385, 629, 606, 187, 949, 0 } };

        /*10x10 Graph*/
        public static int[,] distanceMatrix2 = {    { 0, 94, 397, 86, 865, 304, 237, 737, 192, 374 },
                                                    { 702, 0, 324, 278, 614, 738, 312, 200, 439, 593 },
                                                    { 668, 922, 0, 62, 963, 426, 336, 199, 50, 105 },
                                                    { 537, 693, 880, 0, 65, 701, 101, 862, 945, 833 },
                                                    { 626, 87, 769, 169, 0, 612, 306, 502, 223, 194 },
                                                    { 351, 43, 82, 152, 168, 0, 639, 335, 123, 438 },
                                                    { 967, 37, 687, 86, 836, 8, 0, 952, 212, 912 },
                                                    { 637, 450, 255, 606, 966, 581, 0, 284, 631, 350 },
                                                    { 120, 833, 975, 856, 753, 494, 611, 0, 203, 767 },
                                                    { 61, 765, 738, 506, 866, 588, 400, 682, 0, 422 },
                                                    { 719, 353, 290, 310, 556, 901, 788, 476, 185, 0 } };

        /*20x20 Graph*/
        public static int[,] distanceMatrix3 = {    { 0, 593, 283, 326, 125, 189, 642, 582, 265, 497, 510, 363, 438, 366, 232, 700, 497, 38, 200, 143 },
                                                    { 837, 0, 819, 963, 158, 519, 387, 476, 77, 365, 590, 787, 921, 794, 513, 184, 97, 122, 259, 80 },
                                                    { 969, 264, 0, 259, 892, 562, 570, 648, 936, 654, 341, 793, 130, 739, 519, 583, 773, 319, 167, 605 },
                                                    { 802, 165, 504, 0, 906, 723, 442, 644, 853, 48, 603, 375, 778, 120, 173, 573, 919, 560, 71, 595 },
                                                    { 949, 738, 539, 423, 0, 797, 657, 181, 274, 929, 411, 777, 955, 653, 278, 803, 759, 158, 353, 169 },
                                                    { 120, 926, 794, 887, 50, 0, 14, 10, 566, 946, 664, 213, 248, 571, 656, 63, 626, 80, 875, 109 },
                                                    { 65, 261, 369, 924, 637, 826, 0, 125, 841, 369, 813, 760, 207, 901, 475, 23, 944, 651, 372, 58 },
                                                    { 782, 646, 614, 328, 265, 197, 529, 0, 996, 214, 177, 679, 283, 243, 103, 858, 556, 870, 250, 224 },
                                                    { 545, 889, 168, 196, 132, 903, 6, 346, 0, 633, 119, 974, 707, 817, 327, 419, 646, 41, 658, 439 },
                                                    { 297, 36, 128, 627, 192, 134, 477, 963, 798, 0, 466, 73, 401, 148, 512, 892, 478, 418, 195, 362 },
                                                    { 191, 182, 287, 363, 95, 203, 971, 466, 916, 684, 0, 515, 211, 811, 927, 508, 761, 541, 4, 998 },
                                                    { 425, 42, 547, 479, 166, 45, 573, 559, 304, 434, 940, 0, 845, 296, 247, 114, 749, 764, 532, 988 },
                                                    { 162, 11, 46, 114, 404, 951, 861, 590, 220, 3, 130, 937, 0, 196, 936, 148, 634, 807, 196, 49 },
                                                    { 630, 412, 161, 481, 743, 983, 669, 915, 563, 813, 758, 996, 15, 0, 836, 414, 995, 433, 74, 215 },
                                                    { 183, 982, 338, 300, 303, 3, 814, 648, 360, 99, 479, 942, 568, 647, 0, 749, 849, 564, 371, 420 },
                                                    { 282, 946, 26, 407, 244, 133, 928, 397, 360, 522, 153, 201, 732, 981, 647, 0, 74, 861, 178, 740 },
                                                    { 169, 20, 554, 464, 333, 816, 428, 526, 657, 87, 564, 430, 62, 654, 932, 518, 0, 930, 55, 169 },
                                                    { 75, 416, 288, 838, 945, 278, 209, 587, 617, 711, 674, 988, 386, 630, 251, 0, 392, 0, 74, 428 },
                                                    { 705, 401, 740, 273, 957, 722, 139, 78, 48, 628, 691, 143, 805, 8, 664, 752, 731, 518, 0, 55 },
                                                    { 607, 840, 909, 946, 412, 575, 44, 431, 402, 670, 844, 393, 856, 627, 464, 673, 675, 15, 223, 0 }};

        /***
         * Function: It fills up the matrix depending on the quantity of vertices that the user want, with the values they want.
         * Receive: nothing.
         * Return: a matrix with the result of the inputs.
         ***/
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

        /***
         * Function: It prints the matrix with the edges and the weight or paths. Depending on which matrix you send by parameter.
         * Receive: a matrix (or weighted graph) and the quantity of vertices.
         * Return: nothing.
         ***/
        private static void Print(int[,] distance, int verticesCount)
        {
            for (int a = 0; a < verticesCount; ++a)
                Console.Write("\t" + a.ToString());
            Console.WriteLine();
            for (int b = 0; b < 4; ++b)
                Console.Write(" ");
            for (int c = 0; c < (verticesCount*8); ++c)
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

        /***
         * Function: It calculates the short distance graph for any weighted graph. 
         *              Besides, it save the predecessors in another graph.
         * Receive: A graph.
         * Return: the resulting matrix (graph).
         ***/
        public static int[,] FloydWarshall(int[,] graph)
        {
            int[,] distance = new int[vertices, vertices];
            assignments++;

            for (int i = 0; i < vertices; ++i)
            {
                for (int j = 0; j < vertices; ++j)
                {
                    distance[i, j] = graph[i, j];
                    assignments++;
                    comparisons++;
                }
            }
            comparisons++;

            for (int k = 0; k < vertices; ++k)
            {
                for (int i = 0; i < vertices; ++i)
                {
                    for (int j = 0; j < vertices; ++j)
                    {
                        if (distance[i, k] + distance[k, j] < distance[i, j])
                        {
                            distance[i, j] = distance[i, k] + distance[k, j];
                            antecesors[i, j] = k;
                            assignments += 2;
                            comparisons++;
                        }
                    }
                }
            }
            comparisons++;
            return distance;
        }

        /***
         * Function: This is an adaption from the original FloydWarshall, it conditionates the short distance graph with an
         *              amount of jumps higher from the first run of the FloydWarshall.
         * Receive: the graph and the jumps wanted from the user.
         * Return: nothing.
         ***/
        public static void FloydWarshallWithJumps(int[,] graph, int jumps)
        {
            int[,] distance = new int[vertices, vertices];
            int cont = 0;
            assignments += 2;

            for (int i = 0; i < vertices; ++i)
            {
                for (int j = 0; j < vertices; ++j)
                {
                    distance[i, j] = graph[i, j];
                    assignments++;
                    comparisons++;
                }
            }
            comparisons++;

            aux = false;
            aux2 = true;
            int[,] auxiliarDistance = distance;
            int k = 0;
            assignments += 3;
            while ((aux == false) && (k < vertices))
            {
                for (; k < vertices; k++)
                {
                    for (int i = 0; i < vertices; i++)
                    {
                        for (int j = 0; j < vertices; j++)
                        {
                            if (distance[i, k] + distance[k, j] < distance[i, j])
                            {
                                if ((distance[i, k] + distance[k, j] != auxiliarDistance[i, j]) || (aux2 == true))
                                {
                                    distance[i, j] = distance[i, k] + distance[k, j];
                                    antecesors[i, j] = distance[i, j];
                                    assignments += 2;
                                    comparisons++;
                                }

                            }
                            cont++;
                        }
                    }
                    comparisons++;
                }
                comparisons++;
                auxiliarDistance = distance;
                if (cont <= jumps)
                {
                    aux = true;
                    assignments++;
                    comparisons++;
                }
                else
                { 
                    cont = 0;
                    assignments++;
                }
                aux2 = false;
                assignments++;
            }
            if (aux == true)
            {
                Print(distance, vertices);
                Print(antecesors, vertices);
            }
            else
                Console.WriteLine("No se encontro ruta con restriccion");
        }

        /***
         * Function: It modifies the distance from an edge and recalculate the short distance matrix.
         * Receive: the initial vertex, the final and the newDistance of the edge.
         * Return: the new short distance graph and the modified matrix of predecessors.
         ***/
        public static int[,] modify(int initial, int final, int newDistance)
        {

            if(distanceMatrix[initial,final] != newDistance)
            {
                distanceMatrix[initial, final] = newDistance;
                assignments++;
                comparisons++;
                
                for (int i = 0; i < vertices; i++)
                {
                    for (int j = 0; j < vertices; j++)
                    {
                        if (distanceMatrix[i, final] + distanceMatrix[final, j] < distanceMatrix[i, j])
                        {
                            distanceMatrix[i, j] = distanceMatrix[i, final] + distanceMatrix[final, j];
                            antecesors[i, j] = final;
                            assignments += 2;
                            comparisons++;
                        }
                    }
                }
                comparisons++;
            }
            return distanceMatrix;
        }

        /***
         * Function: This is the main menu.
         * Receive: nothing.
         * Return: nothing.
         ***/
        private static void dataRequest()
        {
            Console.WriteLine("\t\t*********************");
            Console.WriteLine("\t\t*    BIENVENIDO     *");
            Console.WriteLine("\t\t*********************");
            string op;
            do
            {
                comparisons = 0; assignments = 0;
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
                    int nd = Convert.ToInt32(Console.ReadLine());
                    int[,] temp = modify(i, e, nd);
                    Print(temp, vertices);
                    Print(antecesors, vertices);
                }
                if (op == "3")
                {
                    Console.Write("\n\tOriginal: \n");
                    Print(distanceMatrix, vertices);
                    int[,] temp = FloydWarshall(distanceMatrix);
                    Console.Write("\n\tFloydWarshall: \n");
                    Print(temp, vertices);
                    Console.Write("\n\tPredecesors: \n");
                    Print(antecesors, vertices);
                }
                if (op == "4")
                {
                    Console.Write("Type the amount of jumps you want: ");
                    jumps = Convert.ToInt32(Console.ReadLine());
                    FloydWarshallWithJumps(FloydWarshall(distanceMatrix), jumps);
                }
            } while (op != "0");
        }

        static void Main(string[] args)
        {
            /***
             * Here is a call for the menu with the options and callbacks needed.
             ***/
            dataRequest();
            
        }
    }
}
