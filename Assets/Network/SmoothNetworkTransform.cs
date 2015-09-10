using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SmoothNetworkTransform : NetworkBehaviour {
    [SyncVar]
    private Vector3 truePosition;
    [SyncVar]
    private Quaternion trueRotation;
    [Range(0.1f, 10f)]
    public float lerpFactor = 5f;

    void Update()
    {
        if (isServer)
        {
            truePosition = transform.position;
            trueRotation = transform.rotation;
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, truePosition, lerpFactor * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, trueRotation, lerpFactor * Time.deltaTime);
        }
    }
    
}
