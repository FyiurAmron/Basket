using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameTarget : MonoBehaviour {
    public MainGameData mainGameData;

    private Collider[] gameTargets;
    private AudioSource winAudioSource;

    public void Start() {
        gameTargets = GameObject.FindGameObjectsWithTag( "GameTarget" )
                                .Select( go => go.GetComponent<Collider>() )
                                .ToArray();
        winAudioSource = GameObject.FindGameObjectWithTag( "PowerMeter" ).GetComponent<AudioSource>();
    }

    public void OnTriggerEnter( Collider other ) {
        HashSet<GameTarget> ht = mainGameData.hitTargets;
        ht.Add( this );
        if ( ht.Count == gameTargets.Length ) {
            winAudioSource.Play(); // TODO call some actual win routine from here
        }
    }

    public void OnTriggerExit( Collider other ) {
        HashSet<GameTarget> ht = mainGameData.hitTargets;
        ht.Remove( this );
    }
}
