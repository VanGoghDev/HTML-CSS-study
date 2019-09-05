using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clusterLib
{
    public class kMeanCluster
    {
        public List<Point> points { get; set; }
        public Route[] routes = new Route[] { new Route(), new Route() };
        public const double EPSILON = 0.0002;
        List<Cluster> clusterList;
        public int count = 0;
        public void setPoint(List<Point> points)
        {
            this.points = points;
        }

        public void clustering()
        {
            Random rnd = new Random();
            //Cluster[] clusters = new Cluster[] { new Cluster(points[rnd.Next(0, 19)]), new Cluster(points[rnd.Next(20, 39)])};
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
        }
    }
}
