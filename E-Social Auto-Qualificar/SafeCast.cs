using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace E_Social_Auto_Qualificar
{
    class SafeCast
    {
        public static bool? castbool(object _vlr)
        {
            if (_vlr.GetType() == typeof(bool))
            {

                return (bool)_vlr;

            }
                       

            return null;
        }

        public static int? castint(object _vlr)
        {
            if (_vlr.GetType() == typeof(string) || _vlr.GetType() == typeof(int) || _vlr.GetType() == typeof(long))
            {
                int o;
                if (int.TryParse(_vlr.ToString(), out o))
                {
                    return o;
                }               
            }

            return null;
        }

        
    }
}
