using UnityEngine;
using System.Collections;

namespace framework
{

    public class utils_t
    {

        public static string to_str_ip(uint ip)
        {
            string str = "";

            uint a = (ip >> 24) & 255;
            uint b = (ip >> 16) & 255;
            uint c = (ip >> 8) & 255;
            uint d = ip & 255;

            str += a.ToString() + ".";
            str += b.ToString() + ".";
            str += c.ToString() + ".";
            str += d.ToString();

            return str;
        }
        
    }

}