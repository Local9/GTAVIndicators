using DynamicIndicators.Shared;
using System.Collections.Generic;

namespace DynamicIndicators.FiveM.Client
{
    public class Main : BaseScript
    {
        List<BlinkerParameters> _blinkerParameters = new();
        public static Log Log = new();

        public Main()
        {
            _blinkerParameters = Configuration.GetConfig().Vehicles;
            
        }
    }
}
