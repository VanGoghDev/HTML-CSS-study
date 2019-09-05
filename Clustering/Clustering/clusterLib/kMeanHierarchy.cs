using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clusterLib
{
    public class kMeanHierarchy
    {
        public List<Point> points { get; set; }
        public Route[] routes = new Route[] { new Route(), new Route() };
        public const double EPSILON = 0.0002;
        public const double MAXROUTELENGTH = 7;
        public const double MINROUTELENGTH = 3;
        public List<Cluster> clusterList = new List<Cluster>();
        List<double> distance = new List<double>();
        List<Dot> dist = new List<Dot>();
        List<Point> dots = new List<Point>();
        Cluster generalCluster;
        public int count = 0;
        public bool doClustering = true;  // продолжать кластеризацию или нет
        //public void setPoint(List<Point> points)
        //{
        //    this.points = points;
        //}

        public kMeanHierarchy(List<Point> points)
        {
            this.points = points;
            generalCluster = new Cluster(points);
        }

        public void countDistanceList(Point point)
        {
            for (int i = 0; i < distance.Count; i++)
            {
                distance[i] = MyMath.EuclidDistance(points[i], point);
                dist[i].distance = MyMath.EuclidDistance(points[i], point);
            }
        }


        public void findCenters(int centersCount)
        {
            for(int i = 0; i < points.Count; i++)
            {
                distance.Add(MyMath.EuclidDistance(points[i], generalCluster.weight));
                dist.Add(new Dot(MyMath.EuclidDistance(points[i], generalCluster.weight)));
            }
            dist.Where(i => i.isChosen == false).Max(x=>x.distance);
            int ind = distance.FindIndex(y => y == dist.Where(f => !f.isChosen).Max(x => x.distance));
            //int index = distance.FindIndex(x => x == distance.Max());
            dist[ind].isChosen = true;
            dots.Add(points[ind]);
            //dots.Add(points[index]);
            if (centersCount > 1)  // Если кластеров больше одного, то входим в цикл
            {
                for (int i = 0; i < centersCount - 1; i++)
                {
                    if (i == 0)  // Только в первый раз ищем точку которая максимально далеко от первого класса
                                 // То есть по максимальному значению
                    {
                        countDistanceList(dots[i]);
                        ind = distance.FindIndex(y => y == dist.Where(f => !f.isChosen).Max(x => x.distance));
                        //index = distance.FindIndex(x => x == distance.Max());
                        dist[ind].isChosen = true;
                        dots.Add(points[ind]);
                    }
                    else
                    {
                        for (int j = 0; j < distance.Count; j++)
                        {
                            distance[j] = Math.Abs(distance[j] - MyMath.EuclidDistance(points[j], dots[i]));
                            dist[j].distance = Math.Abs(dist[j].distance - MyMath.EuclidDistance(points[j], dots[i]));
                        }
                        //index = distance.FindIndex(x => x == distance.Min());
                        ind = distance.FindIndex(y => y == dist.Where(f => f.isChosen == false).Min(x => x.distance));
                        dist[ind].isChosen = true;
                        dots.Add(points[ind]);
                        //dots.Add(points[index]);
                    }
                }
            }
        }

        // найдем n точек которые максимально удалены от точки point в основном кластере
        public void findMaxDistance(Point point, int n)
        {
            List<double> distance = new List<double>();
            for(int i = 0; i < points.Count; i++)
            {
                distance.Add(MyMath.EuclidDistance(points[i], generalCluster.weight));
            }
            distance.Sort();
            int test = 34;
        }


        public void clustering(int clusterNum)
        {
            cluster(clusterNum);
            while (doClustering)
            {
                isIncrease(clusterNum);
                break;
            }
        }
        public void cluster(int clusterNum)
        {
            findCenters(clusterNum);
            // найдем самые дальние точки от центра общего кластера до остальных точек, в количестве clusterNum.
            findMaxDistance(generalCluster.weight, clusterNum);
            Random rnd = new Random();
            //Cluster[] clusters = new Cluster[] { new Cluster(points[rnd.Next(0, 19)]), new Cluster(points[rnd.Next(20, 39)]) };
            for(int i = 0; i < dots.Count; i++)
            {
                clusterList.Add(new Cluster(dots[i], i));
            }
            //clusterList = clusters.OfType<Cluster>().ToList();
            while (true)
            {
                count++;
                for (int i = 0; i < points.Count; i++)
                {
                    for (int j = 0; j < clusterList.Count(); j++)
                    {
                        clusterList[j].distance = MyMath.EuclidDistance(points[i], clusterList[j].weight);
                        if (j == clusterList.Count() - 1)  // заходим в этот блок кода на последнем шаге, чтобы проверить расстояния до всех кластеров
                        {
                            // находим минимальное расстояние из всех (смотрим расстояния от точки до всех кластеров)
                            double minDist = clusterList.Min(a => a.distance);
                            // получаем индекс кластера до которого расстояние минимальное
                            int index = clusterList.FindIndex(r => r.distance.Equals(minDist));
                            // запишем в поле точки к какому маршруту она относится (номер маршрута = номеру кластера в списке
                            points[i].pathNum = index;
                            // добавим в нужный кластер текущую точку
                            clusterList[index].addPoint(points[i]);
                        }
                    }
                }
                // Пересчитываем веса 
                for (int i = 0; i < clusterList.Count(); i++)
                {
                    clusterList[i].countWeight();
                }
                if (clusterList.All(x => x.distanceBetweenCM < EPSILON) || (count > 99))
                {
                    break;
                }
                else
                {
                    clusterList.ForEach(x => x.points.Clear());
                }
            }
            for (int i = 0; i < clusterList.Count; i++)
            {
                clusterList[i].GenetateCircle(360, clusterList[i].radius);
            }
        }

        public void isIncrease(int clusterNum)
        {
            int newCount = 0;
            for (int i = 0; i < clusterList.Count; i++)
            {
                clusterList[i].countRouteLength();
                if (clusterList[i].routeLength > MAXROUTELENGTH)
                {
                    newCount++;
                }
                else if (clusterList[i].routeLength < MINROUTELENGTH)
                {
                    newCount--;
                }
            }
            if (newCount != 0)
            {
                clusterList = new List<Cluster>();
                distance = new List<double>();
                dist = new List<Dot>();
                dots = new List<Point>();
                if (newCount + clusterNum < 1)
                {
                    cluster(2);
                } else
                {
                    cluster(clusterNum + newCount);
                }
                
                //doClustering = false;
            } else
            {
                doClustering = false;
            }
            
        }
    }


    
}
