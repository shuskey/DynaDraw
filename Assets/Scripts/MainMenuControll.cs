using System.Web;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuControll : MonoBehaviour
{

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

    bool toggleEveryOther = true;

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
            var LoginURL = "https://dynadraw.auth.us-west-2.amazoncognito.com/login?client_id=582fu0d2ul1dj8osvtlirvd9rd&response_type=code&scope=email+openid&redirect_uri=" + $"{baseUrl}";
            Debug.Log("Login Button will launch:");
            Debug.Log(LoginURL);
            toggleEveryOther  = !toggleEveryOther;
            if (toggleEveryOther)
                Application.OpenURL(LoginURL);
        }
    }

    private void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
            return;
        
        //Check for paramerters passed on URL for Webgl versions
        var webglUrl = Application.absoluteURL;
        if (string.IsNullOrEmpty(webglUrl))
            return;
        
        var myUri = new System.Uri(webglUrl);
        var dynastring = "";

#if !UNITY_EDITOR && UNITY_WEBGL
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
}
