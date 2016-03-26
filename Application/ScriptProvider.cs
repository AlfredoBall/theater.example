using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Theater;

namespace Application
{
    public static class ScriptProvider
    {
        public static Script Script;

        static ScriptProvider()
        {
            Script = new Script();

            Script["5a4ecace-8b73-437b-bd09-91179d6ebe06"] = new Event("Got Left Hand");
            Script["5a4ecace-8b73-437b-bd09-91179d6ebe07"] = new Event("Got Right Hand");
            Script["5a4ecace-8b73-437b-bd09-91179d6ebe08"] = new Event("Got Operator");

            Script.Plot = (e) =>
            {
                return true;
            };
        }
    }
}
