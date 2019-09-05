using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clustering
{
    class Routes
    {
        public List<Route> routes { get; set; }
        public List<Point> allPoints { get; set; }

        public Routes()
        {
            this.routes = new List<Route>();
            this.allPoints = new List<Point>();
        }
        public void UploadPoints(string filePath) 
        {
            Parser parser = new Parser();
            parser.Parse(filePath, this);
        }

        public void AddRoute(Route route)
        {
            routes.Add(route);
        }

        public void addPoint(Point point)
        {
            allPoints.Add(point);
        }
    }
}
