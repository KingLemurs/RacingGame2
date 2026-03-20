using Source;
using TMPro;
using UnityEngine;

public class CheckpointUI : MonoBehaviour
{
    private Gamemode _mode;

    private TMP_Text _text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _mode = GameObject.FindGameObjectWithTag("GameController").GetComponent<Gamemode>();
        _text = GetComponent<TMP_Text>();
        
        _mode.OnCheckpointReached.AddListener((() => _text.text = $"Current Checkpoint: {_mode.CurrentCheckpoint}\nCurrent Lap: {_mode.CurrentLap}"));
    }
}
