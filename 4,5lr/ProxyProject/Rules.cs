using System;
using System.Text;
using System.IO;

namespace ProxyProject
{
    abstract public class Rules
    {
        public static bool isFalse = true;
        public Rules Successor { get; set; }
        public abstract void TypeRule(RequestResponse request);
    }
    public class Corectness : Rules
    {
        public override void TypeRule(RequestResponse request)
        {
            if ((request.Header == null) && (request.Body == null))
            {
                throw new CorrectnessException();
            }
            else if (Successor != null)
            {
                Successor.TypeRule(request);
            }
        }
    }
    public class RuleResourse : Rules
    {
        private bool IsAllowResourse(RequestResponse request)
        {
            bool flag = true;
            string[] ruleResourse = File.ReadAllLines("RuleResourse.txt");
            string[] s = new string[] { " ", "-" };
            for (int i = 0; (i < ruleResourse.Length) && (flag); i++)
            {
                string[] value = ruleResourse[i].Split(s, StringSplitOptions.RemoveEmptyEntries);
                if (request.Destination == Convert.ToInt32(value[0]))
                {
                    flag = (request.Time >= Convert.ToInt32(value[1])) && (request.Time <= Convert.ToInt32(value[2]));
                }
            }
            return flag;
        }
        public override void TypeRule(RequestResponse request)
        {
            if (!IsAllowResourse(request))
            {
                throw new ResourseException();
            }
            else if (Successor != null)
            {
                Successor.TypeRule(request);
            }
        }
    }
    public class RuleClient : Rules
    {
        private bool IsAllowClient(RequestResponse request)
        {
            bool flag = false;
            string[] ruleResourse = File.ReadAllLines("RuleClient.txt");
            string[] s = new string[] { " " };
            for (int i = 0; i < ruleResourse.Length; i++)
            {
                string[] value = ruleResourse[i].Split(s, StringSplitOptions.RemoveEmptyEntries);
                if (request.Sourse == Convert.ToInt32(value[0]))
                {
                    for (int j = 1; (j < value.Length) && (!flag); j++)
                    {
                        flag = (request.Destination == Convert.ToInt32(value[j]));
                    }
                }
            }
            return flag;
        }
        public override void TypeRule(RequestResponse request)
        {
            if (IsAllowClient(request))
            {
                throw new ClientException();
            }
            else if (Successor != null)
            {
                Successor.TypeRule(request);
            }
        }
    }
}
