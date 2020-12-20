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
    public class DynaDrawSavedCreations
    {
        public string UserSaveComments { get; set; }
        public List<DynaDrawSavedItem> UserSaveCreationsList { get; set; }

        private readonly string saveFileName = "dynadrawsaves.json";
        private const string playerPreferenceKey = "DynaDrawCreations";

        public void Add(string title, string commands, string sceneName, float fieldOfView, float timeScale)
        {
            UserSaveCreationsList.Add(new DynaDrawSavedItem(title, commands, sceneName, fieldOfView.ToString(), timeScale.ToString()));
            SaveIntoJson();
        }

        public void Remove(int index)
        {
            var itemToRemove = UserSaveCreationsList[index];
            UserSaveCreationsList.Remove(itemToRemove);
            SaveIntoJson();
        }
        public void Update(int index, string title, string commands, string sceneName, string fieldOfView, string timeScale)
        {
            UserSaveCreationsList[index].Title = title;
            UserSaveCreationsList[index].DynaDrawCommands = commands;
            UserSaveCreationsList[index].SceneName = sceneName;
            UserSaveCreationsList[index].FieldOfView = fieldOfView;
            UserSaveCreationsList[index].TimeScale = timeScale;
            SaveIntoJson();
        }

        public List<string> JustTitles()
        {
            var returnThisList = new List<string>();
            foreach (var userCreationItem in UserSaveCreationsList)
            {
                returnThisList.Add(userCreationItem.Title);
            }
            return returnThisList;
        }

        public void SaveIntoJson()
        {
            string saveDataJson = JsonConvert.SerializeObject(this);
            PlayerPrefs.SetString(playerPreferenceKey, saveDataJson);
            //System.IO.File.WriteAllText(Application.persistentDataPath + $"/{saveFileName}", saveDataJson);
        }

        public void GetFromJson()
        {
            try
            {
                //var saveDataJson = System.IO.File.ReadAllText(Application.persistentDataPath + $"/{saveFileName}");
                var saveDataJson = PlayerPrefs.GetString(playerPreferenceKey);
                var stuffFromFile = JsonConvert.DeserializeObject<DynaDrawSavedCreations>(saveDataJson);
                if (stuffFromFile.UserSaveComments == null || stuffFromFile.UserSaveCreationsList == null)
                {
                    Debug.Log($"{saveFileName}, did not contain any data.");
                    throw new Exception($"Save file named {saveFileName} had no data.");
                }
                UserSaveComments = stuffFromFile.UserSaveComments;
                UserSaveCreationsList = stuffFromFile.UserSaveCreationsList;
        }
            catch
            {
                Debug.Log($"Could not find or process file at {Application.persistentDataPath}/{saveFileName}, will start with notfound item.");
                UserSaveComments = "Your own creation";
                UserSaveCreationsList = new List<DynaDrawSavedItem>() { new DynaDrawSavedItem("Sample", "Rfrrrrrrfrrrrrrfrrrrrrf", "", "", "") };
}
        }
    }
}
