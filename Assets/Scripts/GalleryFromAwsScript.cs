#if NOTNOW
using Assets.Scripts.DataObjects;
using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine.Networking;
#endif
using System.Collections;
using UnityEngine;

public class GalleryFromAwsScript : MonoBehaviour
{
    private const string URL = "https://6bdrmwd3kc.execute-api.us-west-2.amazonaws.com/prod/";
#if NOTNOW
    private List<DynaDrawGalleryItem> dynaDrawGalleryItemsList;
#endif

    public void GenerateRequest()
    {
#if NOTNOW
        StartCoroutine(ProcessRequest(URL));
#endif
    }
#if NOTNOW

    private IEnumerator ProcessRequest(string uri)
    {
        UnityWebRequest request = UnityWebRequest.Get(uri);
        yield return request.SendWebRequest();

        if (request.result ==  UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(request.error);
            Debug.Log($"Trouble contacting the AWS API Gateway for GetAllPublishedGalleryItems.");

            dynaDrawGalleryItemsList = new List<DynaDrawGalleryItem>() { new DynaDrawGalleryItem("Rf6rf6rf6rf") };
        }
        else
        {
            var resultText = request.downloadHandler.text;
            Debug.Log(resultText);

            var awsApiResponse = JsonConvert.DeserializeObject<AwsApiGetAllPublishedGalleryItemsResponse>(resultText);
            dynaDrawGalleryItemsList = awsApiResponse.body;
        }

#endif

}
