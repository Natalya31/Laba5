using System;
using System.Text;

namespace ProxyProject
{
    public class Client : RequestResponse
    {
        private int id;
        public int Id { get; set; }
        public Client (int id)
        {
            Id = id;
        }
        public RequestResponse GetRequest (Client client)
        {
            int countHours = 24;
            int CountResourse = 100;
            Random rand = new Random();
            RequestResponse packet = GetPacket();
            packet.Sourse = client.Id;
            packet.Destination = rand.Next(CountResourse);//убрать 
            packet.Time = rand.Next(countHours);
            return packet;
        }
        public RequestResponse GetResponse (Resourse proxy, RequestResponse request, Client client)
        {
            return proxy.Response(request, client);
        }
    }

}
