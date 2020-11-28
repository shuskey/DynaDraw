using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.DataObjects 
{
    [System.Serializable]
    public class DynaDrawSavedItem
    {
        public string Title { get; set; }
        public string DynaDrawCommands { get; set; }

        public DynaDrawSavedItem(string title, string dynadrawcommands)
        {
            Title = title;
            DynaDrawCommands = dynadrawcommands;
        }
        
    }
    
}
