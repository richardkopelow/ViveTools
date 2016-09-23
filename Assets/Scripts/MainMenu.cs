using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Transform PlayerHead;

    public void OnPlayClicked()
    {
        VRPhysicsPlayer.Height = PlayerHead.localPosition.y;
        SceneManager.LoadScene("PlayScene");
    }
}
