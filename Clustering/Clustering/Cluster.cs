using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clustering
{
    class Cluster
    {
        public Point weight { get; set; }

        public List<Point> points { get; set; }

        public double distanceBetweenCM { get; set; }

        // расстояние от центра масс до самой дальней точки
        public double radius { get; set; }

        public double maxRadius { get; set; }
       
        // для того чтобы знать нужно ли пересчитывать центры масс кластеров или нет
        public bool isChange { get; set; }

        // расстояние от класса до i-ой точки
        // меняется на каждом шаге
        public double distance { get; set; }

        public Cluster(Point point)
        {
            this.weight = point;
            points = new List<Point>();
            this.isChange = true;
            maxRadius = 120;
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

        public void countWeight()
        {
            Point weightOld = new Point(weight.x, weight.y);
            // суммируем все координаты точек и делим на их количество
            weight.x = points.Sum(x => x.x) / points.Count();
            weight.y = points.Sum(x => x.y) / points.Count();

            // расстояние между центрами на текущей итерации и предыдущей
            distanceBetweenCM = MyMath.EuclidDistance(weightOld, weight);
        }
        
        public void checkMaxRadius()
        {
            if (points.Max(x => MyMath.EuclidDistance(x, weight)) > maxRadius)
            {

            }
        }
    }
}
