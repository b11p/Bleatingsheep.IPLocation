using System;
using System.Collections.Generic;
using System.Text;

namespace Bleatingsheep.IPLocation
{
    internal abstract class IPLocation : IIPLocation
    {
        public abstract string Provider { get; }
        public abstract string Location { get; }

        public abstract override string ToString();
    }
}
