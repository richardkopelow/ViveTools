using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OnPlayClicked()
    {
        VRPhysicsPlayer.Height = GameObject.Find("Camera (eye)").GetComponent<Transform>().localPosition.y;
        SceneManager.LoadScene("Sandbox");
    }
}
