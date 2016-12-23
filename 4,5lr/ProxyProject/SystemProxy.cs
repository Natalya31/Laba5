using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ProxyProject
{
    public class SystemProxy
    {
        public static Random rand = new Random();
        public static Proxy proxy;
        private static int minCountClient;
        private static int maxCountClient;
        public static List<Client> listClient;
        public static List<RealResourse> listResourses;
        public static int FactCountClient { get; set; }
        public static int MinCountClient 
        {
            get { return minCountClient = 20; }
        }
        public static int MaxCountClient
        {
            get { return maxCountClient = 50; }
        }
        public static List<Client> GetClients()
        {
            listClient = new List<Client>();
            FactCountClient = rand.Next(MinCountClient, MaxCountClient);
            for (int i = 0; i < FactCountClient; i++)
            {
                listClient.Add (new Client (i));
            }
            return listClient;
        }
        public static void Production (List <Client> listClient, Proxy proxy)
        {
            AuthService.RetainClients(MaxCountClient);
            foreach (Client client in listClient)
            {
                RequestResponse request = proxy.Request(client); //клиент отправил запрос
                try
                {
                    AuthService.CheckClient(client);
                    proxy.Process(request);
                    RequestResponse response = client.GetResponse(proxy, request, client);//получить ответ
                    Console.WriteLine("Клиент {0} получил ответ на свой запрос успешно! Ресурс - {1}.", client.Id + 1, request.Destination);
                }
                catch (AuthException)
                {
                    Console.WriteLine("Клиент {0} - недопустимый клиент!", request.Sourse);
                }
                catch (CorrectnessException)
                {
                    Console.WriteLine("Попытка клиента {0} отправить некоректный запрос ресурсу {1}. Запрос отклонен!", request.Sourse, request.Destination);
                }
                catch (ResourseException)
                {
                    Console.WriteLine("Попытка клиента {0} получить доступ к ресурсу {1}. Ресурс временно недоступен! Запрос отклонен!", request.Sourse, request.Destination);
                }
                catch (ClientException)
                {
                    Console.WriteLine("Клиент {0} не имеет прав для доступа к ресурсу {1}! Запрос отклонен!", request.Sourse, request.Destination);
                }
            }
        }
        
    }
}
