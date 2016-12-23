using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyProject
{
    public class CacheProd
    {
        public static List<RequestResponse> listUsedRequest = new List<RequestResponse>();
        public static List<RequestResponse> listUsedResponse = new List<RequestResponse>();
        public void AddUsedData(RequestResponse request, RequestResponse response)//добавить в кэш
        {
            listUsedRequest.Add(request);
            listUsedResponse.Add(response);
        }
        public int IsExistRequest(RequestResponse request)
        {
            int positionCache = -1;//возможно в кеше нет ответа
            bool flag = false;
            for (int i = 0; (i < (listUsedRequest.Count)) && (flag == false); i++)
            {
                flag = request.Body == listUsedRequest[i].Body;
                if (flag)
                {
                    positionCache = i;
                }
            }
            return positionCache;
        }
        public static RequestResponse GetResponseCache(RequestResponse request, CacheProd cache)//в кеш
        {
            RequestResponse response;
            int positionCache = cache.IsExistRequest(request);
            if (positionCache > 0)
            {
                response = listUsedRequest[positionCache];
            }
            else
            {
                response = null;
            }
            return response;
        }
    }
}
