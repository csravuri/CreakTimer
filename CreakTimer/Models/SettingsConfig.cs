using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreakTimer.Models
{
    class SettingsConfig
    {
        public DateTime TargetDate { get; set; }
        public int Days { get; set; }
        public bool EnableDarkMode { get; set; }
    }
}
