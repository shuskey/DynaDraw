﻿using Newtonsoft.Json;
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

        public void Add(string title, string commands)
        {
            UserSaveCreationsList.Add(new DynaDrawSavedItem(title, commands));
            SaveIntoJson();
        }

        public void Remove(int index)
        {
            var itemToRemove = UserSaveCreationsList[index];
            UserSaveCreationsList.Remove(itemToRemove);
            SaveIntoJson();
        }
        public void Update(int index, string title, string commands)
        {
            UserSaveCreationsList[index].Title = title;
            UserSaveCreationsList[index].DynaDrawCommands = commands;
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
            System.IO.File.WriteAllText(Application.persistentDataPath + $"/{saveFileName}", saveDataJson);
        }

        public void GetFromJson()
        {
            try
            {
                var saveDataJson = System.IO.File.ReadAllText(Application.persistentDataPath + $"/{saveFileName}");
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
                UserSaveComments = "No File Found";
                UserSaveCreationsList = new List<DynaDrawSavedItem>() { new DynaDrawSavedItem("notfound", "") };
            }
        }

    }
}
