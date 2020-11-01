using UnityEngine;

public class BallShooter : MonoBehaviour {
    private const float POWER_METER_Z_OFFSET = 0.05f;
    
    public int mouseButton = 0;
    public float forceMultiplier = 200.0f;
    public float holdTimeMax = 2.0f; // in seconds
    public float powerMeterHeightMax = 2.5f;

    private Rigidbody myRigidbody;
    private AudioSource sfx;
    private Camera cam;
    private GameObject powerMeter;
    private Material powerMeterMaterial;
    private AudioSource powerSfx;
    private float holdTime = 0;

    public void Start() {
        cam = Camera.main;
        myRigidbody = GetComponent<Rigidbody>();
        sfx = GetComponent<AudioSource>();
        powerMeter = GameObject.FindGameObjectWithTag( "PowerMeter" );
        powerMeterMaterial = powerMeter.GetComponent<Renderer>().material;
        powerSfx = Camera.main.GetComponent<AudioSource>();
    }

    private void setPowerMeterProperties( float fillFactor ) {
        Vector3 newScale = powerMeter.transform.localScale;
        Vector3 newPos = powerMeter.transform.position;
        newScale.y = powerMeterHeightMax * fillFactor;
        newPos.y = POWER_METER_Z_OFFSET + 0.5f * newScale.y;
        powerMeter.transform.localScale = newScale;
        powerMeter.transform.position = newPos;
        powerMeterMaterial.color = new Color( fillFactor, 1.0f - fillFactor, 0 ) * 2;
    }

    public void Update() {
        // following RB code has to be done in Update() [not FixedUpdate()] due to having to handle input events

        Transform camT = cam.transform;

        if ( Input.GetMouseButtonDown( mouseButton ) ) {
            myRigidbody.isKinematic = true;
            myRigidbody.detectCollisions = false;
            myRigidbody.velocity = Vector3.zero;
            myRigidbody.angularVelocity = Vector3.zero;
            sfx.Stop();
            powerSfx.Play();
        }

        if ( Input.GetMouseButton( mouseButton ) ) {
            transform.position = camT.position + camT.forward;
            holdTime += Time.deltaTime;
            holdTime = Mathf.Clamp( holdTime, 0, holdTimeMax );
            setPowerMeterProperties( holdTime / holdTimeMax );
        }

        if ( Input.GetMouseButtonUp( mouseButton ) ) {
            myRigidbody.isKinematic = false;
            myRigidbody.detectCollisions = true;

            float forceFactor = holdTime * forceMultiplier;
            Vector3 force = camT.forward * forceFactor;
            myRigidbody.AddForce( force );
            myRigidbody.AddRelativeTorque( 0.5f, 1.0f, 0.5f );
            holdTime = 0;
            setPowerMeterProperties( 0 );
            powerSfx.Stop();
        }
    }
}
