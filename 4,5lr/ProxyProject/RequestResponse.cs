using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyProject
{
    public class RequestResponse
    {
        private static byte[] header;
        private static byte[] body;
        private static int sourse;
        private static int destination;
        private static int time;
        private static int headerLengthByte;
        private static int maxBodyLenght;
        public byte[] Header { get; set; }
        public byte[] Body { get; set; }
        public int Sourse { get; set; }
        public int Destination { get; set; }
        public int Time { get; set; }
        private static int HeaderLengthByte
        {
            get { return headerLengthByte = 32; }
        }
        private static int BodyLengthByte { get; set; }
        private static int MaxBodyLenght
        {
            get { return maxBodyLenght = 10000; }
        }
        public static RequestResponse GetPacket() 
        {
            Random rand = new Random();
            RequestResponse packet = new RequestResponse();
            packet.Header = new byte[HeaderLengthByte];
            rand.NextBytes(packet.Header);
            BodyLengthByte = rand.Next(MaxBodyLenght);              //исключение о длине
            packet.Body = new byte [BodyLengthByte];
            rand.NextBytes (packet.Body);
            return packet;
        }

    }
}
