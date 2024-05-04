using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAudio : MonoBehaviour
{
    public AudioClip clip;
    public float volumeScale = 1f;
    private void OnCollisionEnter(Collision collision)
    {
        if (clip == null) return;
        // Interaction
        if (collision.gameObject.layer == 16 && collision.relativeVelocity.magnitude > 0.001f)
        {
            Debug.Log(collision.gameObject.name);
            Mesh mesh = collision.gameObject.GetComponent<MeshFilter>().mesh;
            Bounds bounds = mesh.bounds;
            float diameter = bounds.extents.x * 2;
            //Debug.Log(diameter * collision.gameObject.transform.localScale.x);
            //Debug.Log(0.2f * Mathf.Lerp(1, 6, diameter * collision.gameObject.transform.localScale.x));
            Debug.Log(collision.relativeVelocity.magnitude);
            AudioSource.PlayClipAtPoint(clip, collision.contacts[0].point, 0.1f * Mathf.Lerp(1, 4, diameter * collision.gameObject.transform.localScale.x * collision.relativeVelocity.magnitude) * volumeScale);
        }
        // Slime
        else if (collision.gameObject.layer == 3)
        {
            AudioSource.PlayClipAtPoint(clip, collision.contacts[0].point, 0.05f * volumeScale);
        }
    }
}
