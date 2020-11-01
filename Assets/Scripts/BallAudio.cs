using UnityEngine;

public class BallAudio : MonoBehaviour {
    public float debounceTime = 0.1f;

    private AudioSource sfx;
    private float time = 0;

    public void Start() {
        sfx = GetComponent<AudioSource>();
    }

    public void Update() {
        time += Time.deltaTime;
    }

    public void OnCollisionEnter() {
        if ( time < debounceTime ) {
            return;
        }

        sfx.Play();
        time = 0;
    }
}
