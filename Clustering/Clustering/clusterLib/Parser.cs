using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace clusterLib
{
    public class Parser
    {
        public void Parse(String filePath, Routes inObj) 
        {
            string line;
            List<string> xCoord = new List<string>();
            List<string> yCoord = new List<string>();
            using (StreamReader inStream = new StreamReader(filePath))
            {
                while((line = inStream.ReadLine()) != null)
                {
                    string[] foo = Regex.Split(line, "\t");
                    Point point = new Point(float.Parse(foo[0]), float.Parse(foo[1]));
                    inObj.addPoint(point);
                }
            }

        }
    }
}
