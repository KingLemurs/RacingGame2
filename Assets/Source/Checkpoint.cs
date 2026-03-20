using System;
using UnityEngine;

namespace Source
{
    public class Checkpoint : MonoBehaviour
    {
        public int CheckpointOrder;

        private void OnTriggerEnter(Collider other)
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<Gamemode>().TryUpdateCheckpoint(CheckpointOrder);
        }
    }
}