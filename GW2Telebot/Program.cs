using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GW2Telebot
{
    class Program
    {
        BackgroundWorker bw;
        public void Init()
        {
            this.bw = new BackgroundWorker();
            this.bw.DoWork += this.bw_DoWork;
        }

        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;
        }

        static void Main(string[] args)
        {
            
        }
    }
}
