using Assets.Scripts.DataObjects;
using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine;

public class GalleryFromAwsScript : MonoBehaviour
{
    public GameObject galleryCardPrefab;
    public GameObject galleryCardPanel;
    private const string URL = "https://6bdrmwd3kc.execute-api.us-west-2.amazonaws.com/prod/";
    private List<DynaDrawGalleryItem> dynaDrawGalleryItemsList;

    public void GenerateRequest()
    {
        StartCoroutine(ProcessRequest(URL));
    }

    private IEnumerator ProcessRequest(string uri)
    {
        UnityWebRequest request = UnityWebRequest.Get(uri);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
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

            foreach (var item in dynaDrawGalleryItemsList)
            {
                var gallerycard = Instantiate(galleryCardPrefab, galleryCardPanel.transform);
                gallerycard.transform.SetParent(galleryCardPanel.transform);
                gallerycard.GetComponentInChildren<GalleryCardScript>().Initialize(item);
            }
        }
    }
}
