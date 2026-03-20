using System;
using UnityEngine;
using UnityEngine.Events;

namespace Source
{
    public class Gamemode : MonoBehaviour
    {
        public int LastCheckpoint = 8;
        public int CurrentCheckpoint = 0;
        public int CurrentLap = 1;

        public UnityEvent OnCheckpointReached = new();


        private void Start()
        {
            StartRound();
        }

        void StartRound()
        {
            CurrentCheckpoint = 0;
        }

        public void TryUpdateCheckpoint(int newRank)
        {
            if (newRank == 1 && CurrentCheckpoint == LastCheckpoint)
            {
                CurrentLap++;
            }
            
            if (newRank == CurrentCheckpoint + 1)
            {
                CurrentCheckpoint++;
                OnCheckpointReached.Invoke();
                print("AYOOOOOOOO");
            }
        }
    }
}