using Source;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private Gamemode _mode;
    private TMP_Text _text;
    public void Play()
    {
        SceneManager.LoadScene(1);
    }
}
