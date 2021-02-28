using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerExplode : MonoBehaviour
{
    enum TagsICareAbout { Projectile }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagsICareAbout.Projectile.ToString()))
        {
            StartCoroutine(DoExplosion(other));
        }
    }

    private IEnumerator DoExplosion(Collider other)
    {
        var myColor = GetComponent<Renderer>().material.color;
        var otherColor = other.GetComponent<Renderer>().material.color;
        GetComponent<Renderer>().material.color = Color.yellow;
        other.GetComponent<Renderer>().material.color = Color.yellow;
        yield return new WaitForSecondsRealtime(0.1f);
        GetComponent<Renderer>().material.color = myColor;
        other.GetComponent<Renderer>().material.color = otherColor;
    }
}
