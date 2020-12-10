using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.DataObjects 
{
    [System.Serializable]
    public class SceneDefinition
    {
        public string Title { get; set; }
        public Material SkyBoxMaterial { get; set; }
        public float DirectionalLightIntensity { get; set; }

        public SceneDefinition(string title, Material skyBoxMaterial, float directionalLightIntensity = 1.0f)
        {
            Title = title;
            SkyBoxMaterial = skyBoxMaterial;
            DirectionalLightIntensity = directionalLightIntensity;
        }
        
    }
    
}
