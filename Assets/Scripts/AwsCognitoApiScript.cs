using Assets.Scripts.DataObjects;
using Newtonsoft.Json;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class AwsCognitoApiScript : MonoBehaviour
{
    public AwsCognitoTokensResponse aws_tokens;
    public GameObject helloGuestOrLoggedInMember;
    public GameObject loginLogoutButton;

    public AWSSECRETS AwsSecrets = new AWSSECRETS();

    private bool everyOther = true;

    void Awake()
    {
        DontDestroyOnLoad(this);
        aws_tokens = new AwsCognitoTokensResponse();
        aws_tokens.GetFromJson();
    }

    public void cognitoLogout(string baseUrl, bool restartApp = true)
    {
        aws_tokens.id_token =
            aws_tokens.access_token =
            aws_tokens.refresh_token =
            aws_tokens.cognito_code = "logged out";
        aws_tokens.SaveIntoJson();
        helloGuestOrLoggedInMember.GetComponentInChildren<Text>().text = "Hello Guest";
        loginLogoutButton.GetComponentInChildren<Text>().text = "Login";
        if (restartApp)
            Application.OpenURL(baseUrl);
    }

    public void cognitoLogin(string baseUrl)
    {
        everyOther = !everyOther;
        if (everyOther)
        {
            Application.OpenURL(string.Format(AwsSecrets.AwsCognitoLoginURL, AwsSecrets.ClientId, baseUrl));
        }
        else
        {
            Debug.Log($"AwsCognitoURL: {AwsSecrets.AwsCognitoLoginURL}, ClientId: {AwsSecrets.ClientId}, baseUrl: {baseUrl}");
            Debug.Log($"Login will try to launch this URL: {string.Format(AwsSecrets.AwsCognitoLoginURL, AwsSecrets.ClientId, baseUrl)}");
        }
    }

    public void showLoggedInMember()
    {
        helloGuestOrLoggedInMember.GetComponentInChildren<Text>().text = "Hello Member " +
            aws_tokens.cognito_code.Substring(0, 10);
        loginLogoutButton.GetComponentInChildren<Text>().text = "Logout";
    }

    private string Base64ClientIdClientSecret() => "Basic " + System.Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", AwsSecrets.ClientId, AwsSecrets.ClientSecret)));
     
    public IEnumerator getCognitoTokensFromCode(string code, string baseUrl)
    {
        // We may have received a browser refresh and allready have
        // asked for these tokens from aws
        if (aws_tokens.cognito_code != code)
        {
            aws_tokens.cognito_code = code;
            var uri = string.Format(AwsSecrets.AwsCognitoTokensURL, code, AwsSecrets.ClientId, baseUrl);
            string body = "";
            UnityWebRequest request = UnityWebRequest.Post(uri, body);
            request.SetRequestHeader("Authorization", Base64ClientIdClientSecret());
            request.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(request.error);
                Debug.Log($"Trouble contacting the AWS Cognito for getting tokens.");

                aws_tokens.id_token = aws_tokens.access_token = aws_tokens.refresh_token = "";
            }
            else
            {
                var resultText = request.downloadHandler.text;
                var awsCognitoTokens = JsonConvert.DeserializeObject<AwsCognitoTokensResponse>(resultText);
                aws_tokens.id_token = awsCognitoTokens.id_token;
                aws_tokens.access_token = awsCognitoTokens.access_token;
                aws_tokens.refresh_token = awsCognitoTokens.refresh_token;
            }
            aws_tokens.SaveIntoJson();
        }
        showLoggedInMember();
    }
}