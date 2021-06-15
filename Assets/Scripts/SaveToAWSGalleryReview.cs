using Assets.Scripts.DataObjects;
using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine;

public class SaveToAWSGalleryReview : MonoBehaviour
{
    public AwsCognitoTokensResponse aws_tokens;
    public GameObject AwsController;
    private const string AwsApiGalleryReviewURL = "https://photoloom.com/api/gallery/review";
    private AwsCognitoApiScript awsCognitoApiScript;

    public void GenerateRequest(DynaDrawSavedItem saveAsNewReviewItem)
    {
        var payload = JsonConvert.SerializeObject(saveAsNewReviewItem);
        StartCoroutine(ProcessRequest(AwsApiGalleryReviewURL, payload));
    }

    private IEnumerator ProcessRequest(string uri, string payload)
    {
        aws_tokens = awsCognitoApiScript.GetTokensFromJson();

        UnityWebRequest request = UnityWebRequest.Put(uri, payload);

        request.SetRequestHeader("Authorization", "Bearer " + aws_tokens.access_token);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            Debug.Log($"Trouble contacting the AWS API Gateway for PutReviewGalleryItems.");
        }

    }

    private void Awake()
    {
        awsCognitoApiScript = AwsCognitoApiScript._Instance;
    }
}
