using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyProject
{
    public class AuthException : Exception
    {
        public AuthException() { }
    }
    public class CorrectnessException : Exception
    {
        public CorrectnessException() { }
    }
    public class ResourseException : Exception
    {
        public ResourseException() { }
    }
    public class ClientException : Exception
    {
        public ClientException() { }
    }


}
