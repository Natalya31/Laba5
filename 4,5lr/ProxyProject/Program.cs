using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyProject
{
    class Program
    {
        static void Main(string[] args)
        {
            List <Client> listClient = SystemProxy.GetClients();
            Proxy proxy = new Proxy(new RealResourse());
            SystemProxy.Production(listClient, proxy);
            Console.ReadKey();
        }
    }
}
