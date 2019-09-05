using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clusterLib
{
    public class GeneralCluster
    {
        public List<Cluster> clusterList { get; set; }

        public GeneralCluster(List<Cluster> clusters)
        {
            this.clusterList = clusters;
        }

        public GeneralCluster(Cluster[] clusters)
        {
            this.clusterList = clusters.OfType<Cluster>().ToList();
        }
    }
}
