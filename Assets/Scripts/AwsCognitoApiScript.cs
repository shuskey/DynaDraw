using Amazon;
using Amazon.CognitoIdentity;
using Amazon.Runtime;
using Amazon.CognitoIdentityProvider;
using Amazon.Extensions.CognitoAuthentication;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.IdentityStore;
using Assets.Scripts.DataObjects;
using Newtonsoft.Json;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

public class AwsCognitoApiScript : MonoBehaviour
{
    public static AwsCognitoApiScript _Instance;
    public AwsCognitoTokensResponse aws_tokens;

    public AWSSECRETS AwsSecrets = new AWSSECRETS();

    void Awake()
    {
        Debug.Log("Starting AwsCognitoApiScript Awake function");
        DontDestroyOnLoad(this);
        if (_Instance == null)
        {
            _Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        aws_tokens = new AwsCognitoTokensResponse();
        Debug.Log("Calling GetTokensFromJason in AwsCognitoApiScript Awake function");
        GetTokensFromJson();
    }

    public AwsCognitoTokensResponse GetTokensFromJson()
    {
        aws_tokens.GetFromJson();
        return aws_tokens;
    }

    public AwsCognitoTokensResponse CognitoLogout()
    {
        aws_tokens.id_token =
            aws_tokens.access_token =
            aws_tokens.refresh_token =
            aws_tokens.cognito_code = "";
        aws_tokens.SaveIntoJson();
        return aws_tokens;
    }

    public async Task<AwsCognitoTokensResponse> CognitoLoginAsync_forStandAlone(string baseUrl)
    {
        Debug.Log("Now in CognitoLoginAsync_forWebGL function");
        if (await AuthenticateWithSrpAsync())
            Debug.Log("Login successful");

        return aws_tokens;
    }

    public AwsCognitoTokensResponse CognitoLogin_forWebGL(string baseUrl)
    {
        var urlToOpen = string.Format(AwsSecrets.AwsCognitoLoginURL, AwsSecrets.ClientId, baseUrl);
        Debug.Log("Using this URL to Login: " + urlToOpen);
        Application.OpenURL(urlToOpen);
        // External cognito web page will display the login form, then re-launch our page with parameters passed
        // indicating the result of the login
        // See MainMenuController.cs Start() function which calls back to GetCognitoTokensFromCode() in this file

        // I am pretty sure this function never get this far because of the above OpenURL
        // We need to Exit here
        return aws_tokens;
    }

    public async Task<bool> AuthenticateWithSrpAsync()
    {
        // Initialize the Amazon Cognito credentials provider
        //    CognitoAWSCredentials credentials = new CognitoAWSCredentials(
        //        AwsSecrets.AwsIdentityPoolId, RegionEndpoint.USWest2);
        //var identityId = credentials.GetIdentityId();


        AmazonCognitoIdentityProviderClient provider = new AmazonCognitoIdentityProviderClient(new AnonymousAWSCredentials(), RegionEndpoint.USWest2);
        CognitoUserPool userPool = new CognitoUserPool(AwsSecrets.POOL_ID, AwsSecrets.CLIENTAPP_ID, provider);

        CognitoUser user = new CognitoUser(AwsSecrets.TEST_USERNAME, AwsSecrets.CLIENTAPP_ID, userPool, provider);
        InitiateSrpAuthRequest authRequest = new InitiateSrpAuthRequest()
        {
            Password = AwsSecrets.TEST_PASSWORD
        };

        AuthFlowResponse authResponse = await user.StartWithSrpAuthAsync(authRequest).ConfigureAwait(false);
        if (authResponse.AuthenticationResult != null)
        {
            var usersAccessToken = authResponse.AuthenticationResult.AccessToken;
            aws_tokens.id_token = authResponse.AuthenticationResult.IdToken;
            aws_tokens.access_token = authResponse.AuthenticationResult.AccessToken;
            aws_tokens.refresh_token = authResponse.AuthenticationResult.RefreshToken;
            aws_tokens.cognito_code = user.Username;    
            return true;
        }
        else return false;  
    }

    private string Base64ClientIdClientSecret() => "Basic " + System.Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", AwsSecrets.ClientId, AwsSecrets.ClientSecret)));
     
    public IEnumerator GetCognitoTokensFromCode(string code, string baseUrl)
    {
        // We may have received a browser refresh and allready have
        // asked for these tokens from aws
        if (aws_tokens.cognito_code != code)
        {
            aws_tokens.cognito_code = code;

            Debug.Log("Now in GetCognitoTokensFromCode code = " + code + ", baseUrl= " + baseUrl);
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
                Debug.Log("Did not get a Connection Error, nor a Protocol Error from AWS call");
                var resultText = request.downloadHandler.text;
                Debug.Log("resultText = " + resultText);
                var awsCognitoTokens = JsonConvert.DeserializeObject<AwsCognitoTokensResponse>(resultText);
                aws_tokens.id_token = awsCognitoTokens.id_token;
                aws_tokens.access_token = awsCognitoTokens.access_token;
                aws_tokens.refresh_token = awsCognitoTokens.refresh_token;
            }
            Debug.Log("Calling SaveIntoJson in AwsCognitoApiScript GetCognitoTokensFromCode function");
            aws_tokens.SaveIntoJson();
        }
    }
}