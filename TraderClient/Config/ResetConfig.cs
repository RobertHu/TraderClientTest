using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace TraderClient.Config
{
    internal sealed class ResetConfig : ConfigurationSection
    {
        internal static readonly ResetConfig Default = (ResetConfig)ConfigurationManager.GetSection("resetConfig");

        [ConfigurationProperty("Hour")]
        internal int Hour
        {
            get
            {
                return (int)base["Hour"];
            }
            set
            {
                base["Hour"] = value;
            }
        }

        [ConfigurationProperty("Minute")]
        internal int Minute
        {
            get
            {
                return (int)base["Minute"];
            }
            set
            {
                base["Minute"] = value;
            }
        }

        [ConfigurationProperty("Seconds")]
        internal int Seconds
        {
            get
            {
                return (int)base["Seconds"];
            }
            set
            {
                base["Seconds"] = value;
            }
        }

    }
}
