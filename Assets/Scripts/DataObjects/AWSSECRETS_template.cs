using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.DataObjects
{
    // To use rename the file and class to
    // AWSSECRETS.cs & AWSSECRETS
    // Put your secrets in below
    public class AWSSECRETS_template           
    {
        // Client App secretes Template
        public string ClientId
        {
            get { return "yourclientidgoeshere"; }
            set { }
        }
        public string ClientSecret
        {
            get { return "yourclientsecretgoeshere"; }
            set { }
        }
        public string AwsCognitoTokensURL
        {
            get { return "https://YourDomain.auth.YourRegion.amazoncognito.com/oauth2/token?grant_type=authorization_code&code={0}&client_id={1}&scope=email+openid&redirect_uri={2}"; ; }
            set { }
        }
        public string AwsCognitoLoginURL
        {
            get { return "https://YourDomin.auth.YourRegion.amazoncognito.com/login?client_id={0}&response_type=code&scope=email+openid&redirect_uri={1}"; }
            set { }
        }
    }
}
