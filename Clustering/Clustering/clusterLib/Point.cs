using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clusterLib
{
    public class Point
    {
        public float x { get; set; }
        public float y { get; set; }
        public bool isChosen = false;
        public int pathNum { get; set; }

        public Point()
        { }
        public Point(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public Point(float x, float y, int pathNum, int pointNum)
        {
            this.x = x;
            this.y = y;
        }
    }
}
