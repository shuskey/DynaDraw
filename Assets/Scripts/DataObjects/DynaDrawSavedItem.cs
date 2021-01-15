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
        public string Subtitle { get; set; }
        public string DynaDrawCommands { get; set; }
        public string SceneName { get; set; }
        public string FieldOfView { get; set; }
        public string TimeScale { get; set; }


        public DynaDrawSavedItem(string title, string subtitle, string dynadrawcommands, string sceneName, string fieldOfView, string timeScale)
        {
            Title = title;
            Subtitle = subtitle;
            DynaDrawCommands = dynadrawcommands;
            SceneName = sceneName;
            FieldOfView = fieldOfView;
            TimeScale = timeScale;
        }
    }
    
}
