using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void LoadJrMenu()
    {
        SceneManager.LoadScene(4);
    }
    
    public void LoadCrMenu()
    {
        SceneManager.LoadScene(3);
    }
    
    public void LoadSolo()
    {
        SceneManager.LoadScene(2);
    }
    
    public void LoadRoomSystemMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadRandomRoom()
    {
        GameObject.Find("AMBIANCE").transform.GetChild(1).GetChild(0).GetComponent<AudioSource>().Play();
        SceneManager.LoadScene(6);
    }

    public void LoadMainMenu()//Loading main menu and disconnecting the player so it can be reconnected when joining it
    {
        SceneManager.LoadScene(0);
        PhotonNetwork.Disconnect();
    }

    public void QuitGame()//Quit Game
    {
        PhotonNetwork.Disconnect();
        Application.Quit();
    }
}
