using TMPro;
using UnityEngine;

public class OnClickEvents : MonoBehaviour
{
    public TextMeshProUGUI soundText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleMute()
    {
        if(GameManager.mute)
        {
            GameManager.mute = false;
            soundText.text = "";
        }
        else
        {
            GameManager.mute = true;
            soundText.text = "/";
        }
    }

    public void QuitGame()
    {
        PlayerPrefs.DeleteKey ("CurrentLevelIndex"); 
        Application.Quit();
    }
}
