using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clustering
{
    class Route
    {
        public List<Point> points { get; set; }
        public Point weight { get; set; }

        public Route()
        {
            points = new List<Point>();
        }

        public void AddPoint(Point point)
        {
            points.Add(point);
        }

        public void countWeight()
        {

        }
    }
}
