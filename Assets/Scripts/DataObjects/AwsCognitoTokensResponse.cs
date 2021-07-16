using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.DataObjects
{
    [System.Serializable]
    public class AwsCognitoTokensResponse
    {
        public string id_token { get; set; }
        public string access_token { get; set; }
        public string refresh_token { get; set; }
        public int expires_in { get; set; }
        public string token_type { get; set; }
        public string cognito_code { get; set; }

        private const string playerPreferenceKey = "DynaDrawCredentials";

        public void SaveIntoJson()
        {
            string saveDataJson = JsonConvert.SerializeObject(this);
            Debug.Log("SaveIntoJson now will save this saveDataJson = " + saveDataJson);
            PlayerPrefs.SetString(playerPreferenceKey, saveDataJson);
            Debug.Log($"Aws Credentials for code={cognito_code}, saved to {Application.persistentDataPath}/{playerPreferenceKey}");
        }

        public void GetFromJson()
        {
            Debug.Log("Now inside GetFromJson");

            try
            {
                var saveDataJson = PlayerPrefs.GetString(playerPreferenceKey);
                var stuffFromFile = JsonConvert.DeserializeObject<AwsCognitoTokensResponse>(saveDataJson);
                if (string.IsNullOrEmpty(stuffFromFile.cognito_code) || string.IsNullOrEmpty(stuffFromFile.id_token))
                {
                    Debug.Log($"{playerPreferenceKey}, did not contain any data.");
                    throw new Exception($"Save file named {Application.persistentDataPath}/{playerPreferenceKey} had no data.");
                }
                id_token = stuffFromFile.id_token;
                access_token = stuffFromFile.access_token;
                refresh_token = stuffFromFile.refresh_token;
                expires_in = stuffFromFile.expires_in;
                token_type = stuffFromFile.token_type;
                cognito_code = stuffFromFile.cognito_code;
                Debug.Log($"Aws Credentials for code={cognito_code}, retrieved from {Application.persistentDataPath}/{playerPreferenceKey}");
            }
            catch
            {
                Debug.Log($"Could not find or process file at {Application.persistentDataPath}/{playerPreferenceKey}, will start with notfound item.");
            }
        }
    }
}
