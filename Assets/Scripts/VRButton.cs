using UnityEngine;
using System.Collections;

public class VRButton : MonoBehaviour
{
    public GameObject Receiver;
    public string MethodName;

    public void OnCollisionEnter(Collision collision)
    {
        Receiver.SendMessage(MethodName);
    }
}
