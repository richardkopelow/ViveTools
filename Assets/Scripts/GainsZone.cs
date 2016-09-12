using UnityEngine;
using System.Collections;

public class GainsZone : MonoBehaviour
{
    public Vector3 Size
    {
        get
        {
            return Trans.lossyScale;
        }
    }


    public Transform Trans;
    void Start()
    {
        Trans = GetComponent<Transform>();
    }

    void Update()
    {

    }
    
    public void OnTriggerEnter(Collider other)
    {
        VRGains player = other.GetComponent<VRGains>();
        if (player != null)
        {
            player.Zone = this;
        }
    }
    
    public void OnTriggerExit(Collider other)
    {
        VRGains player = other.GetComponent<VRGains>();
        if (player != null)
        {
            player.Zone = null;
        }
    }
}
