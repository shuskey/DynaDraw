using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.DataObjects 
{
    [System.Serializable]
    public class AwsApiGetAllPublishedGalleryItemsResponse
    {
        public int statusCode { get; set; }
        public List<DynaDrawGalleryItem> body { get; set; }   
    }
    
}
