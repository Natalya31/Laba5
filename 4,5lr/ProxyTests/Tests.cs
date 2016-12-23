using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ProxyTests
{
    public class AuthTests
    {
        [Test]
        public void AuthTestTrue()
        {
            int possibleCountPassword = 50;
            ProxyProject.AuthService.RetainClients(possibleCountPassword);
            ProxyProject.Client client = new ProxyProject.Client(25);
            Assert.That(() => ProxyProject.AuthService.AuthClient(client), Throws.Nothing);
        }
        [Test]
        [ExpectedException(typeof(ProxyProject.AuthException))]
        public void AuthTestFalse()
        {
            int possibleCountPassword = 50;
            ProxyProject.AuthService.RetainClients(possibleCountPassword);
            ProxyProject.Client client = new ProxyProject.Client(52);
            ProxyProject.AuthService.AuthClient(client);
        }
    }
    public class CorrectnessTests
    {
        [Test] 
        public void CorrectnessTestTrue()
        {
            ProxyProject.Rules rl1 = new ProxyProject.Corectness();
            ProxyProject.RequestResponse request = ProxyProject.RequestResponse.GetPacket();
            Assert.That(() => rl1.TypeRule(request), Throws.Nothing);
        }
        [Test]
        [ExpectedException(typeof(ProxyProject.CorrectnessException))]
        public void CorrectnessTestFalse()
        {
            ProxyProject.Rules rl1 = new ProxyProject.Corectness();
            ProxyProject.RequestResponse request = ProxyProject.RequestResponse.GetPacket();
            request.Body = null;
            request.Header = null;
            rl1.TypeRule(request);
        }
    }
    public class ResourseRuleTests
    {
        [Test]
        public void ResourseTestTrue()
        {
            ProxyProject.Rules rl1 = new ProxyProject.RuleResourse();
            ProxyProject.RequestResponse request = ProxyProject.RequestResponse.GetPacket();
            request.Destination = 3;
            request.Time = 14;
            Assert.That(() => rl1.TypeRule(request), Throws.Nothing);
        }
        [Test]
        [ExpectedException(typeof(ProxyProject.ResourseException))]
        public void ResourseTestFalse()
        {
            ProxyProject.Rules rl1 = new ProxyProject.RuleResourse();
            ProxyProject.RequestResponse request = ProxyProject.RequestResponse.GetPacket();
            request.Destination = 3;
            request.Time = 20;
            rl1.TypeRule(request);
        }
    }
    public class ClientRuleTests
    {
        [Test]
        public void ClientTestTrue()
        {
            ProxyProject.Rules rl1 = new ProxyProject.RuleClient();
            ProxyProject.RequestResponse request = ProxyProject.RequestResponse.GetPacket();
            request.Destination = 13;//нет в запрещенных
            Assert.That(() => rl1.TypeRule(request), Throws.Nothing);
            request.Sourse = 21;
            request.Destination = 6;
            Assert.That(() => rl1.TypeRule(request), Throws.Nothing);
        }
        [Test]
        [ExpectedException(typeof(ProxyProject.ClientException))]
        public void ClientTestFalse()
        {
            ProxyProject.Rules rl1 = new ProxyProject.RuleClient();
            ProxyProject.RequestResponse request = ProxyProject.RequestResponse.GetPacket();
            request.Sourse = 21;
            request.Destination = 56;
            rl1.TypeRule(request);
        }
    }
    public class ProcessTests
    {
        [Test]
        public void ProcessTestTrue()
        {
            ProxyProject.Proxy proxy = new ProxyProject.Proxy(new ProxyProject.RealResourse());
            ProxyProject.RequestResponse request = ProxyProject.RequestResponse.GetPacket();
            request.Sourse = 1;
            request.Destination = 6;
            Assert.That(() => proxy.Process(request), Throws.Nothing);
        }
    }
    public class ProductionTests
    {
        public ProxyProject.Proxy proxy;
        public ProxyProject.Client client;
        [SetUp]
        public void ProductionPrepare()
        {
            proxy = new ProxyProject.Proxy(new ProxyProject.RealResourse());
            int possibleCountPassword = 50;
            ProxyProject.AuthService.RetainClients(possibleCountPassword);
            client = new ProxyProject.Client(30);
        }
        [Test]
        public void ProductionTest()
        {
            Assert.IsNotNull(proxy);
            Assert.IsNotNull(client);
            ProxyProject.RequestResponse request = ProxyProject.RequestResponse.GetPacket();
            Assert.IsNotNull(request);
            Assert.That(() => ProxyProject.AuthService.AuthClient(client), Throws.Nothing);
            Assert.That(() => proxy.Process(request), Throws.Nothing);
            ProxyProject.RequestResponse response = client.GetResponse(proxy, request, client);
            Assert.IsNotNull(response);
        }
    }
    public class CacheTests
    {
        public List<ProxyProject.RequestResponse> listUsedRequest = new List<ProxyProject.RequestResponse>();
        public List<ProxyProject.RequestResponse> listUsedResponse = new List<ProxyProject.RequestResponse>();
        public ProxyProject.RequestResponse request;
        public ProxyProject.RequestResponse response;
        public ProxyProject.Proxy proxy;
        public ProxyProject.Client client;
        public ProxyProject.CacheProd cache;
        [SetUp]
        public void CacheTestPrepare()
        {
            proxy = new ProxyProject.Proxy(new ProxyProject.RealResourse());
            client = new ProxyProject.Client(1);
            cache = new ProxyProject.CacheProd();
            request = ProxyProject.RequestResponse.GetPacket();
            response = client.GetResponse(proxy, request, client);
            cache.AddUsedData(request, response);
        }
        [Test]
        public void IsExistCacheTest()
        {
            Assert.Greater(cache.IsExistRequest(request), -1);
        }
    }
}
