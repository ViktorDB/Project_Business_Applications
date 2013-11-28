using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMProjectKlas.Model
{
    class LineUp
    {
        private string id;

        public string ID
        {
            get { return id; }
            set { id = value; }
        }

        private DateTime date;

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }
        private string from;

        public string From
        {
            get { return from; }
            set { from = value; }
        }

        private string until;

        public string Until
        {
            get { return until; }
            set { until = value; }
        }

        private Stage stage;

        public Stage Stage
        {
            get { return stage; }
            set { stage = value; }
        }

        private Band band;

        public Band Band
        {
            get { return band; }
            set { band = value; }
        }
        


        
        
    }
}
