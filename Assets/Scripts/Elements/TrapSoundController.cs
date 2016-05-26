using UnityEngine;
using System.Collections;

public class TrapSoundController : MonoBehaviour {

    AudioSource audioSource;
    public Transform player;
    public float maxDistance;
    public float distance;
    public bool playing;

	void Start () {
        audioSource = GetComponent<AudioSource>();
        audioSource.Stop();
        playing = false;

        player = GameObject.Find("Carl").transform;
	}
	
	void Update () {
        distance = Mathf.Abs(Vector2.Distance(player.position, transform.position));

        if (distance < maxDistance && !playing)
        {
            playing = true;
            audioSource.Play();
        }
     
        else if (distance > maxDistance)
        {
            playing = false;
            audioSource.Stop();
        }
	}
}
