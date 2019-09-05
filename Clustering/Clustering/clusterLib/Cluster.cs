using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clusterLib
{
    public class Cluster
    {
        public int id { get; set; }

        // расстояния от центра кластера до всех точек
        public List<double> distancePoint = new List<double>(); 
        public Point weight { get; set; }

        public List<Point> radiusPoints = new List<Point>();
        public List<Point> points { get; set; }

        Point startPoint = new Point();

        // расстояние маршрута
        public double routeLength = 0;

        public double distanceBetweenCM { get; set; }

        // расстояние от центра масс до самой дальней точки
        public float radius { get; set; }

        public double maxRadius { get; set; }
       
        // для того чтобы знать нужно ли пересчитывать центры масс кластеров или нет
        public bool isChange { get; set; }

        // расстояние от класса до i-ой точки
        // меняется на каждом шаге
        public double distance { get; set; }

        public Cluster(Point point, int id)
        {
            this.weight = point;
            points = new List<Point>();
            this.isChange = true;
            maxRadius = 120;
            this.id = id;
        }

        public Cluster()
        {
            weight = new Point(0, 0);
            points = new List<Point>();
            this.isChange = true;
        }

        public Cluster(List<Point> points)
        {
            weight = new Point(0, 0);
            this.points = points;
            countWeight();
            this.isChange = true;
        }

        public void addPoint(Point point)
        {
            this.points.Add(point);
        }

        // находим длину маршрута
        public void countRouteLength()
        {
            Random rnd = new Random();
            startPoint = points[rnd.Next(0, points.Count)];
            routeLength += getMinDistance(startPoint);
            for (int i = 0; i < points.Count; i++)
            {
                routeLength += getMinDistance(startPoint);
            }
            int test = 4;
        }

        // считаем матрицу расстояний между всеми точками в кластере
        public void countDistanceMaxtrix()
        {

        }

        public void countWeight()
        {
            Point weightOld = new Point(weight.x, weight.y);
            // суммируем все координаты точек и делим на их количество
            weight.x = points.Sum(x => x.x) / points.Count();
            weight.y = points.Sum(x => x.y) / points.Count();
            distancePoint = new List<double>();
            getDistanceFromCenter(this.points);
            int ind = distancePoint.FindIndex(x => x == distancePoint.Max());
            Point max = points[ind];
            //радиус
            radius = (float)MyMath.EuclidDistance(max, weight);

            // расстояние между центрами на текущей итерации и предыдущей
            distanceBetweenCM = MyMath.EuclidDistance(weightOld, weight);
        }

        public double DEGTORAD(int deg)
        {
            return Math.PI * deg / 180;
        }

        //находим точки для окружности
        public void GenetateCircle(int degree, float radius)
        {
            for(int i = 1; i < degree; i++)
            {
                if (i == 1)
                {
                    Point p1 = new Point();
                    Point p2 = new Point();
                    p1.x = weight.x + ((float)Math.Cos(DEGTORAD(360)) * radius);
                    p1.y = weight.y + ((float)Math.Sin(DEGTORAD(360)) * radius);
                    p2.x = weight.x + ((float)Math.Cos(DEGTORAD(1)) * radius);
                    p2.y = weight.y + ((float)Math.Sin(DEGTORAD(1)) * radius);
                    radiusPoints.Add(p1);
                    //radiusPoints.Add(p2);
                } else
                {
                    Point p1 = new Point();
                    Point p2 = new Point();
                    p1.x = weight.x + ((float)Math.Cos(DEGTORAD(i - 1)) * radius);
                    p1.y = weight.y + ((float)Math.Sin(DEGTORAD(i - 1)) * radius);
                    p2.x = weight.x + ((float)Math.Cos(DEGTORAD(i)) * radius);
                    p2.y = weight.y + ((float)Math.Sin(DEGTORAD(i)) * radius);
                    // g.DrawLine(p, p1, p2);
                    radiusPoints.Add(p1);
                    //radiusPoints.Add(p2);
                }

            }
        }

        public void getDistanceFromCenter(List<Point> ppoints)
        {
            for (int i = 0; i < ppoints.Count; i++)
            {
                distancePoint.Add(MyMath.EuclidDistance(points[i], weight));
            }
        }

        public double getMinDistance(Point point)
        {
            List<double> distance = new List<double>();
            double min;
            for(int i = 0; i < points.Count; i++)
            {
                distance.Add(MyMath.EuclidDistance(points[i], point));
                if (distance[i] == 0)
                {
                    distance[i] = 1000;
                }
            }
            int ind = distance.FindIndex(x => x == distance.Min());
            startPoint = points[ind];
            min = distance.Min();
            return min;
        }
        
        public void checkMaxRadius()
        {
            if (points.Max(x => MyMath.EuclidDistance(x, weight)) > maxRadius)
            {

            }
        }
    }
}
