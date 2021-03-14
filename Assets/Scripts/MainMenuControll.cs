using System.Collections;
using System.Web;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuControll : MonoBehaviour
{
    public GameObject aws_controller_object;
    public GameObject loginLogoutButton;
    public GameObject quitButton;

    private AwsCognitoApiScript awsCognitoApiScript;
   
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

    public void Login()
    {
        //#if !UNITY_EDITOR && UNITY_WEBGL
        var webglUrl = Application.absoluteURL;
        if (!string.IsNullOrEmpty(webglUrl))
        {
            var myUri = new System.Uri(webglUrl);
            var portUrlInt = myUri.Port;
            var portUrlString = "";
            if (portUrlInt != 80 && portUrlInt != 443)
                portUrlString = $":{portUrlInt}";
            var folderString = "";
            if (myUri.Host == "photoloom.com")
                folderString = "dynadraw/";
            var baseUrl = $"{myUri.Scheme}://{myUri.Host}{portUrlString}/{folderString}index.html";

            if (loginLogoutButton.GetComponentInChildren<Text>().text == "Logout")
            {
                awsCognitoApiScript.cognitoLogout();
                Application.OpenURL(baseUrl);
            }
            else
            {
                var loginUrl = "https://dynadraw.auth.us-west-2.amazoncognito.com/login?client_id=582fu0d2ul1dj8osvtlirvd9rd&response_type=code&scope=email+openid&redirect_uri=" + $"{baseUrl}";
                Application.OpenURL(loginUrl);
            }

            // What happens next is that the Congito App Client Settings for the DynaDraw client will call back to us with a code parameter passed in
            // We sould catch with in the below Awake function
        }
        
        //#endif
    }

    private void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
            return;
        
        //Check for paramerters passed on URL for Webgl versions
        var webglUrl = Application.absoluteURL;
        if (string.IsNullOrEmpty(webglUrl))
            return;
        quitButton.SetActive(false);  // no Quit button in WebGL version

        var myUri = new System.Uri(webglUrl);

        //#if !UNITY_EDITOR && UNITY_WEBGL
        var dynastring = HttpUtility.ParseQueryString(myUri.Query).Get("dynastring");

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
        //#if !UNITY_EDITOR && UNITY_WEBGL        
        if (SceneManager.GetActiveScene().buildIndex != 0)
            return;

        awsCognitoApiScript = aws_controller_object.GetComponentInChildren<AwsCognitoApiScript>();

        //Check for paramerters passed on URL for Webgl versions
        var webglUrl = Application.absoluteURL;
        if (string.IsNullOrEmpty(webglUrl))
        {
            awsCognitoApiScript.cognitoLogout();
            return;
        }
            
        //Debug.Log("webglUrl=" + webglUrl);

        var myUri = new System.Uri(webglUrl);

        //Debug.Log("myUri=" + myUri);

        // If MainMenuController.Login() is called - like if the Login button is pressed, an aws congito api is called, which in return
        // does a call back to us with the code parameter set with the authenticated users congitoCode
        var cognitoCode = HttpUtility.ParseQueryString(myUri.Query).Get("code");
        //Debug.Log("congitoCode=" + cognitoCode);

        if (!string.IsNullOrEmpty(cognitoCode))
        {
            var portUrlInt = myUri.Port;
            var portUrlString = "";
            if (portUrlInt != 80)
                portUrlString = $":{portUrlInt}";
            var baseUrl = $"{myUri.Scheme}://{myUri.Host}{portUrlString}{myUri.LocalPath}";
            
            StartCoroutine(awsCognitoApiScript.getCognitoTokensFromCode(cognitoCode, baseUrl));
        }
        else
        {
            awsCognitoApiScript.cognitoLogout();
        }

        //#endif
    }
}
