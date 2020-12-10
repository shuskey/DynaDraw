using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.DataObjects
{
    [System.Serializable]
    public class SceneDefinitionPresets
    {
        public List<SceneDefinition> PresetSceneDefinitions { get; set; }
        int currentPresetIndex = -1;

        public SceneDefinitionPresets()
        {
            PresetSceneDefinitions = new List<SceneDefinition>();            
        }

        public void AddPreset(SceneDefinition newPreset)
        {
            PresetSceneDefinitions.Add(newPreset);
        }

        public SceneDefinition sceneSet(string sceneTitle)
        {
            return  PresetSceneDefinitions.SingleOrDefault(s => s.Title.ToLower() == sceneTitle.ToLower());        
        }

        public SceneDefinition getNextPreset()
        {
            int numberOfPresets = PresetSceneDefinitions.Count;
            if (numberOfPresets == 0)
                return null;
            currentPresetIndex++;
            if (currentPresetIndex > numberOfPresets - 1)
                currentPresetIndex = 0;     
            return PresetSceneDefinitions[currentPresetIndex];
        }
    }
}
