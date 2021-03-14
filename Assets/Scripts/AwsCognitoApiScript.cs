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

    private const string AwsCognitoTokensURL = "https://dynadraw.auth.us-west-2.amazoncognito.com/oauth2/token?grant_type=authorization_code&code={0}&client_id=582fu0d2ul1dj8osvtlirvd9rd&scope=email+openid&redirect_uri={1}";
    void Awake()
    {
        DontDestroyOnLoad(this);
        aws_tokens = new AwsCognitoTokensResponse();
        aws_tokens.GetFromJson();
    }
    public void cognitoLogout()
    {
        aws_tokens.id_token = 
            aws_tokens.access_token = 
            aws_tokens.refresh_token = 
            aws_tokens.cognito_code = "logged out";
        aws_tokens.SaveIntoJson();
        helloGuestOrLoggedInMember.GetComponentInChildren<Text>().text = "Hello Guest";
        loginLogoutButton.GetComponentInChildren<Text>().text = "Login";
    }

    public void showLoggedInMember()
    {
        helloGuestOrLoggedInMember.GetComponentInChildren<Text>().text = "Hello Member " +
            aws_tokens.cognito_code.Substring(0,10);
        loginLogoutButton.GetComponentInChildren<Text>().text = "Logout";
    }

    public IEnumerator getCognitoTokensFromCode(string code, string baseUrl)    
    {
        // We may have received a browser refresh and allready have
        // asked for these tokens from aws
        if (aws_tokens.cognito_code != code)
        {
            aws_tokens.cognito_code = code;
            var uri = string.Format(AwsCognitoTokensURL, code, baseUrl);
            string body = "";            
            UnityWebRequest request = UnityWebRequest.Post(uri, body);            
            request.SetRequestHeader("Authorization", "Basic NTgyZnUwZDJ1bDFkajhvc3Z0bGlydmQ5cmQ6MXYzZjRqOXBsZTVyc245c20zYjYyZjEwamFjaDVlcDE4dnAyZWZpMXZxaTMyZHJrZDM0OQ==");
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