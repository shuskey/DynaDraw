using Assets.Scripts.DataObjects;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GalleryCardScript : MonoBehaviour
{
    public GameObject titleObject;
    public GameObject description;
    public Sprite thumbnail;

    private string id;
    private string authorMemberId;
    private string dynaDrawCommand;
    private DynaDrawGalleryItem dynaDrawGalleryItem;
    private System.DateTime creationDateTime;

    public void Initialize(string dynaDrawSavedItemString)  //Mock up a new one
    {
        dynaDrawGalleryItem = new DynaDrawGalleryItem(dynaDrawSavedItemString);
        var dynaDrawSavedItem = JsonConvert.DeserializeObject<DynaDrawSavedItem>(dynaDrawGalleryItem.dynaDrawSavedItem);
        titleObject.GetComponentInChildren<Text>().text = dynaDrawSavedItem.Title;
        description.GetComponentInChildren<Text>().text = dynaDrawGalleryItem.description;

    }

    public void Initialize(DynaDrawGalleryItem newDynaDrawGalleryItem)  //Init from Dynamo DB data
    {
        dynaDrawGalleryItem = newDynaDrawGalleryItem;
        var dynaDrawSavedItem = JsonConvert.DeserializeObject<DynaDrawSavedItem>(dynaDrawGalleryItem.dynaDrawSavedItem);
        titleObject.GetComponentInChildren<Text>().text = dynaDrawSavedItem.Title;
        description.GetComponentInChildren<Text>().text = dynaDrawGalleryItem.description;
        id = dynaDrawGalleryItem.id;
        authorMemberId = dynaDrawGalleryItem.authorId;
        dynaDrawCommand = dynaDrawSavedItem.DynaDrawCommands;
        creationDateTime = dynaDrawGalleryItem.creationDateTime;
        //TODO thumbnail = GetThumbnailFromS3(id);
    }
}
