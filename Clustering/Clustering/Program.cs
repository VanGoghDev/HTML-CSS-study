using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Clustering
{
    class Program
    {
        static void Main(string[] args)
        {
            Routes routes = new Routes();
            routes.UploadPoints(ConfigurationSettings.AppSettings["Path"]);
            kMeanHierarchy kmeanCluster = new kMeanHierarchy(routes.allPoints);
            //kmeanCluster.setPoint(routes.allPoints);
            kmeanCluster.clustering(12);

            hierarchyCluster hier = new hierarchyCluster();
            hier.setClusters(routes.allPoints);
            hier.clustering();
            int tast = 23;
        }
    }
}
