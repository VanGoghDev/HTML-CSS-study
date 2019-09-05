using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clustering
{
    class hierarchyCluster
    {
        // все точки в начале - кластеры
        public List<Point> initialClusters { get; set; }

        // кластеры на каждом последующем шаге
        public List<Point> clusters { get; set; }

        // исходная матрица расстояний
        public double[,] initialProximityMatrix;
        // матрица расстояний, которая меняется с каждой итерацией
        // (удаляются столбцы и вставляются новые, т.е. меняются кластеры)
        public double[,] proximityMatrix;

        // изначально все точки - кластеры
        public void setClusters(List<Point> points)
        {
            this.initialClusters = points;
            int size = initialClusters.Count;
            initialProximityMatrix = new double[initialClusters.Count, initialClusters.Count];
        }

        // считаем исходную матрицу близости
        public void setInitialProximityMatrix()
        {
            for (int i = 0; i < (initialClusters.Count); i++)
            {
                for (int j = 0; j < (initialClusters.Count); j++)
                {
                    if (i != j)
                    {
                        initialProximityMatrix[i, j] = MyMath.EuclidDistance(initialClusters[i], initialClusters[j]);
                    } else
                    {
                        initialProximityMatrix[i, j] = 0;
                    }
                }
            }
        }

        // перестраиваем матрицу близости
        public void setProximityMatrix(double[,] arr, int rowIndx1, int colIndx1, int rowIndx2, int colIndx2)
        {
            proximityMatrix = new double[arr.GetLength(0) - 1, arr.GetLength(0) - 1];
            int inI, inJ;
            inI = inJ = 0;
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                if (inI == proximityMatrix.GetLength(0))
                {
                    break;
                }
                for (int j = 0; j < arr.GetLength(0); j++)
                {
                    if (inJ == proximityMatrix.GetLength(0))
                    {
                        break;
                    }
                    if (i == rowIndx1 || i == rowIndx2)
                    {
                        continue;
                    } else
                    {
                        proximityMatrix[inI, inJ] = arr[i, j];
                        
                        
                        if (inI == proximityMatrix.GetLength(0) - 1 || inJ == proximityMatrix.GetLength(0) - 1)
                        {
                            proximityMatrix[inI, inJ] = 1000;
                        }
                    }
                    inJ++;
                }
                inJ = 0;
                inI++;
            }
            int test = 0; 
        }

        public void test(double[,] arr, int dot1, int dot2)
        {
            proximityMatrix = new double[arr.GetLength(0) - 1, arr.GetLength(0) - 1];

            int ii, jj;
            ii = 0;
            
            for(int i = 0; i < arr.GetLength(0); i++)
            {
                if (i == dot1 || i == dot2)
                {
                    continue;
                }
                jj = 0;
                for(int j = 0; j < arr.GetLength(0); j++)
                {
                    if (j == dot2 || j == dot1)
                    {
                        continue;
                    } else
                    {
                        proximityMatrix[ii, jj] = arr[i, j];
                        
                        jj++;
                    }
                }
                if (ii == 39)
                {
                    break;
                }
                ii++;
            }
            int test = 0;
        }

        public void getMinElements(double[,] arr)
        {
            var maxarr = (from double v in arr select v).Min();
            double first, second = arr.Length;
            int firstRow, firstColumn, secondRow, secondColumn;
            firstRow = firstColumn = secondRow = secondColumn = 0;
            first = second = double.MaxValue;
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(0); j++)
                {
                    if (arr[i, j] < first && arr[i, j] != 0)
                    {
                        second = first;
                        secondRow = firstRow;
                        secondColumn = firstColumn;

                        first = arr[i, j];
                        firstRow = i;
                        firstColumn = j;
                    }
                    // если элемент между первым и вторым, то обновляем второй
                    else if (arr[i, j] < second && arr[i, j] != first && arr[i, j] != 0)
                    {
                        second = arr[i, j];
                        secondRow = i;
                        secondColumn = j;
                    }
                }
            }
            Cluster cluster1 = new Cluster();
            cluster1.addPoint(initialClusters.ElementAt(firstRow));
            cluster1.addPoint(initialClusters.ElementAt(firstRow));

            Cluster cluster2 = new Cluster();
            cluster1.addPoint(initialClusters.ElementAt(secondRow));
            cluster1.addPoint(initialClusters.ElementAt(secondColumn));

            //setProximityMatrix(arr, firstRow, firstColumn, secondRow, secondColumn);
            test(arr, firstRow, firstColumn);
        }

        public void clustering()
        {
            setInitialProximityMatrix();
            getMinElements(initialProximityMatrix);
        }
    }
}
