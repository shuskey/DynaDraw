using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.DataObjects
{
    [System.Serializable]
    public class SceneDefinitionPresets
    {
        public List<SceneDefinition> PresetSceneDefinitions { get; set; }
        int currentPresetIndex = 0;

        public SceneDefinitionPresets()
        {
            PresetSceneDefinitions = new List<SceneDefinition>();            
        }

        public void AddPreset(SceneDefinition newPreset)
        {
            PresetSceneDefinitions.Add(newPreset);
        }

        public SceneDefinition getSpecific(string sceneTitle)
        {
            currentPresetIndex = PresetSceneDefinitions.FindIndex(s => s.Title.ToLower() == sceneTitle.ToLower());
            if (currentPresetIndex == -1)
                currentPresetIndex = 0;
            return PresetSceneDefinitions[currentPresetIndex];        
        }

        public SceneDefinition getFirstPreset()
        {
            return PresetSceneDefinitions.FirstOrDefault();
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
