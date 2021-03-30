using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendenUndEmpfangen
{
    class Program
    {
        

        static void Main(string[] args)
        {
           
            Client c = new Client();
            Server s = new Server();
           

            s.starteServer();
            //c.starteClient();



        }       
    }  
}
