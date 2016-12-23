using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyProject
{
    abstract public class Resourse
    {
       public abstract RequestResponse Request(Client client);
       public abstract RequestResponse Response(RequestResponse request, Client client);
    }
    public class RealResourse : Resourse
    {
        public RequestResponse GetResponse(Client client)
        {
            RequestResponse packet = new RequestResponse();
            int countHours = 24;
            Random rand = new Random();
            packet = RequestResponse.GetPacket();
            packet.Destination = client.Id;
            packet.Time = rand.Next(countHours);
            return packet;
        }
        public override RequestResponse Request(Client client)
        {
            return client.GetRequest(client);
        }
        public override RequestResponse Response(RequestResponse request, Client client)
        {
            return GetResponse(client);
        }
    }
    public class Proxy : Resourse 
    {
        RealResourse resourse;
        CacheProd cache = new CacheProd();
        public Proxy(RealResourse resourse)
        {
            this.resourse = resourse;
        }
        public void Process (RequestResponse request)
        {
            Rules rl1 = new Corectness();
            Rules rl2 = new RuleResourse();
            Rules rl3 = new RuleClient();
            rl1.Successor = rl2;
            rl2.Successor = rl3;
            rl1.TypeRule(request);
        }
        public override RequestResponse Request(Client client)
        {
            return resourse.Request(client);
        }
        public override RequestResponse Response(RequestResponse request, Client client)
        {
            RequestResponse response = CacheProd.GetResponseCache(request, cache);
            if (response == null)
            {
                response = resourse.Response(request, client);
                cache.AddUsedData(request, response);
            }
            return response;
        }
    }
}
