using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Clustering;
using System.Configuration;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Routes routes = new Clustering.Routes();
            routes.UploadPoints(ConfigurationSettings.AppSettings["Path"]);
            kMeanCluster kmeanCluster = new kMeanCluster();
            kmeanCluster.setPoint(routes.allPoints);
            kmeanCluster.clustering();

            this.chart1.ChartAreas[0].AxisY.IsStartedFromZero = false;
            int count = 0;
            foreach(Cluster cluster in kmeanCluster.clusterList)
            {
                this.chart1.Series.Add(new Series(cluster.ToString()+count));
                this.chart1.Series[cluster.ToString() + count].IsValueShownAsLabel = false;
                this.chart1.Series[cluster.ToString() + count].ChartType = SeriesChartType.Point;
                this.chart1.Series[cluster.ToString() + count].Points.DataBindXY(cluster.getAllX(), cluster.getAllY());
                count++;
            }
        }
    }
}
