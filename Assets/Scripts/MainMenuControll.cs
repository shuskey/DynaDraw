using Assets.Scripts.DataObjects;
using Newtonsoft.Json;
using System.Collections;
using System.Runtime.InteropServices;
using System.Web;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuControll : MonoBehaviour
{
    public GameObject loginLogoutButton;
    public GameObject quitButton;
    public GameObject helloGuestOrLoggedInMember;

    private AwsCognitoApiScript awsCognitoApiScript;

    private string baseUrl = "";

#if UNITY_WEBGL
    [DllImport("__Internal")]
    private static extern void CopyToClipboard(string str);

    [DllImport("__Internal")]
    private static extern void OpenNewTab(string url);

    [DllImport("__Internal")]
    private static extern void CloseWindow();
#endif
    public void Home()
    {
        SceneManager.LoadScene(0);
    }

    public void Classic()
    {
        SceneManager.LoadScene(1);
    }

    public void ShootEmUp()
    {
        SceneManager.LoadScene(2);
    }

    public void ArtGallery()
    {
        SceneManager.LoadScene(3);
    }

    public void Credits()
    {
        SceneManager.LoadScene(4);
    }

    public void Quit()
    {
        Application.Quit();
    }

#if UNITY_WEBGL
    public void Login()
    {
        AwsCognitoTokensResponse aws_tokens;

        if (loginLogoutButton.GetComponentInChildren<Text>().text == "Logout")
        {
            Debug.Log("Logout pressed by user");
            aws_tokens = awsCognitoApiScript.CognitoLogout();
            ShowLoggedIn(aws_tokens);
            if (baseUrl != "")
            {
                Application.OpenURL(baseUrl);
                CloseWindow();  //so long and good bye to this window
            }
        }
        else
        {
            Debug.Log("Login pressed by user");
            aws_tokens = awsCognitoApiScript.CognitoLogin_forWebGL(baseUrl);
            CloseWindow();  //so long and good bye to this window - the New instance will start in another tab with the Cognito Code being 
            // passed in the URL
            ShowLoggedIn(aws_tokens);
        }
        Debug.Log("Calling SaveIntoJson in MainMenuControll Login function");
        aws_tokens.SaveIntoJson();

        // What happens next, in a WebGL App, is that the Congito App Client Settings for the DynaDraw client will call back to us with a code parameter passed in
        // We sould catch with in the below Awake function 
    }

#else

    public async void Login()
    {
        AwsCognitoTokensResponse aws_tokens;

        if (loginLogoutButton.GetComponentInChildren<Text>().text == "Logout")
        {
            aws_tokens = awsCognitoApiScript.CognitoLogout();
            ShowLoggedIn(aws_tokens);
            if (baseUrl != "")
                Application.OpenURL(baseUrl);
        }
        else
        {
            aws_tokens = await awsCognitoApiScript.CognitoLoginAsync_forStandAlone(baseUrl);
            ShowLoggedIn(aws_tokens);
        }        
        aws_tokens.SaveIntoJson();
        
        // What happens next, in a WebGL App, is that the Congito App Client Settings for the DynaDraw client will call back to us with a code parameter passed in
        // We sould catch with in the below Awake function 
    }
#endif

    private void ShowLoggedIn(AwsCognitoTokensResponse aws_tokens)
    {
        var loggedin = false;
        Debug.Log("Inside ShowLoggedIn function");
        if (aws_tokens == null)
        {
            Debug.Log("aws_tokens is null");

        } else
        {
            loggedin = !string.IsNullOrEmpty(aws_tokens.cognito_code);
        }

        helloGuestOrLoggedInMember.GetComponentInChildren<Text>().text = loggedin ?
                "Hello Member " + aws_tokens.cognito_code.Substring(0, 10)
                : "Hello Guest";
        loginLogoutButton.GetComponentInChildren<Text>().text = loggedin ? "Logout" : "Login";
    }

    private void GetBaseUrl(string webglUrl)
    {
        var myUri = new System.Uri(webglUrl);
        var portUrlInt = myUri.Port;
        var portUrlString = "";
        if (portUrlInt != 80 && portUrlInt != 443)
            portUrlString = $":{portUrlInt}";
        var folderString = "";
        if (myUri.Host == "photoloom.com")
            folderString = "dynadraw/";

        baseUrl = $"{myUri.Scheme}://{myUri.Host}{portUrlString}/{folderString}index.html";
        Debug.Log($"MainMenuController.baseUrl={baseUrl}");
    }

    private void Awake()
    {
        Debug.Log("Starting MainMenuController Awake function");

        awsCognitoApiScript = AwsCognitoApiScript._Instance;

        //AwsCognitoTokensResponse aws_tokens = new AwsCognitoTokensResponse();
        //aws_tokens.GetFromJson();
        //ShowLoggedIn(aws_tokens);
       
        var webglUrl = Application.absoluteURL;

        //Check for paramerters passed on URL for Webgl versions
        if (string.IsNullOrEmpty(webglUrl))
            return;

        GetBaseUrl(webglUrl);

        if (SceneManager.GetActiveScene().buildIndex != 0)
            return;

        quitButton.SetActive(false);  // no Quit button in WebGL version

        var myUri = new System.Uri(webglUrl);

        var dynastring = "";
#if UNITY_WEBGL
        dynastring = HttpUtility.ParseQueryString(myUri.Query).Get("dynastring");
#endif
        //If we have a sharing style URL here, then start in Classic 3D Dyna Draw
        if (string.IsNullOrEmpty(dynastring))
            return;

        if (!RunTimeGlobals.ThisIsTheInitialLoad)
            return;

        RunTimeGlobals.ThisIsTheInitialLoad = false;
        // Launch into the Classic Dyna Draw Scene
        Classic();        
    }

    private void Start()
    {
        Debug.Log("Starting MainMenuController Start function");
        Debug.Log("Active Scene buildIndex=" + SceneManager.GetActiveScene().buildIndex);

#if UNITY_WEBGL
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {

            //Check for paramerters passed on URL for Webgl versions
            var webglUrl = Application.absoluteURL;
            if (string.IsNullOrEmpty(webglUrl))
            {
                awsCognitoApiScript.CognitoLogout();
                return;
            }

            var myUri = new System.Uri(webglUrl);

            // If MainMenuController.Login() is called - like if the Login button is pressed, an aws congito api is called, which in return
            // does a call back to us with the code parameter set with the authenticated users congitoCode
            var cognitoCode = HttpUtility.ParseQueryString(myUri.Query).Get("code");

            if (!string.IsNullOrEmpty(cognitoCode))
            {
                Debug.Log("URL contails cognitoCode=" + cognitoCode + "  baseUrl=" + baseUrl);
                StartCoroutine(awsCognitoApiScript.GetCognitoTokensFromCode(cognitoCode, baseUrl));
            }
            else
            {
                Debug.Log("No cognitoCode passed in the URL");
                awsCognitoApiScript.CognitoLogout();
            }
        }
#endif
        // May not need the following GetTokensFromJson because it is done in AwsCognitoApiScript Awake()
        Debug.Log("Calling GetTokensFromJson in MainMenuControll Start function");
        var aws_tokens = awsCognitoApiScript.GetTokensFromJson();
        Debug.Log("aws_tokens=" + JsonConvert.SerializeObject(aws_tokens));
        ShowLoggedIn(aws_tokens);
    }
}
