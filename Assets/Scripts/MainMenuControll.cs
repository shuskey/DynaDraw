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

    private void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
            return;
        
        //Check for paramerters passed on URL for Webgl versions
        var webglUrl = Application.absoluteURL;
        if (string.IsNullOrEmpty(webglUrl))
            return;
        
        var myUri = new System.Uri(webglUrl);
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
}
