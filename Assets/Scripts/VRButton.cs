using UnityEngine;
using System.Collections;

public class VRButton : MonoBehaviour
{
    public GameObject Receiver;
    public string MethodName;

	public void OnTriggerEnter(Collider other)
    {
    	Debug.Log("coll");
        Receiver.SendMessage(MethodName);
    }
}
