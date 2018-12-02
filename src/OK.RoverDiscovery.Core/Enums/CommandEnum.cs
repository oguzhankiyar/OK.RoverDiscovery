using System.ComponentModel;

namespace OK.RoverDiscovery.Core.Enums
{
    public enum CommandEnum
    {
        [Description("Left")]
        L = 1,
        [Description("Right")]
        R = 2,
        [Description("Move")]
        M = 3
    }
}