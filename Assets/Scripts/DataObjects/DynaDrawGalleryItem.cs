using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.DataObjects 
{
    [System.Serializable]
    public class DynaDrawGalleryItem
    {
        enum itemType { Undefined = 0, Trash = 1, Review = 2, Pulished = 3, BodyShop = 4 }

        public string id { get; set; }
        public DateTime creationDateTime { get; set; }        
        public string dynaDrawSavedItem { get; set; }
        public int type { get; set; }

        public string authorId = "";
        public string description = "";
        public int viewCount = 0;
        public int upVoteCount = 0;
        public int downVoteCount = 0;
        public bool featuredCategory = true;
        public bool craziestCategory = false;

        public DynaDrawGalleryItem(string dynaDrawCommands)
        {
            dynaDrawSavedItem =  JsonConvert.SerializeObject(new DynaDrawSavedItem(dynaDrawCommands));
        }

    }  
}
