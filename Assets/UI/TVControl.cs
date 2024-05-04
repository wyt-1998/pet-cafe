using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TVControl : MonoBehaviour
{
    private VideoPlayer player;
    public GameObject tv;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<VideoPlayer>();
        tv =GameObject.Find("video");
        tv.SetActive(false);
    }

    public void VPlay()
    {
        if (!tv.activeInHierarchy)
        {
            tv.SetActive(true);   
        }
        player.Play();

    }

    public void VPause()
    {
        if (player.isPlaying)
        {
            player.Pause();
        }
    }

    public void VStop()
    {
     
        player.Stop();
        tv.SetActive(false);
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
