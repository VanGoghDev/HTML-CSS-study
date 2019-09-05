using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clusterLib
{
    public class Dot
    {
        public double distance { get; set; }
        public bool isChosen = false;

        public Dot(double v)
        {
            distance = v;
        }
    }
}
