using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReporterSayk
{
    public class PathStats
    {
        public long TotalSize { get; set; }
        public int NumberItems { get; set; }

        public PathStats(long totalSize,int numberItems)
        {
            this.TotalSize = totalSize;
            this.NumberItems = numberItems;
        }
    }  
}
