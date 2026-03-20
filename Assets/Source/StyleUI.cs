using System;
using TMPro;
using UnityEngine;

namespace Source
{
    public class StyleUI : MonoBehaviour
    {
        public int styleScale = 10;
        public int styleDecay = 10;
        
        private Gamemode _mode;
        private TMP_Text _text;
        private Rigidbody _playerRB;
        
        private int _style = 0;
        private string _rank = "F";

        private void Start()
        {
            _mode = GameObject.FindGameObjectWithTag("GameController").GetComponent<Gamemode>();
            _text = GetComponent<TMP_Text>();
            _playerRB = _mode.player.GetComponent<Rigidbody>();
            _mode.OnPlayerDeath.AddListener(() => { _style /= 2;});
        }

        private void FixedUpdate()
        {
            _style = Mathf.Max(0, _style);
            
            _text.text = $"Style Rank: {_rank}\nScore: {_style}";
            
            if (_playerRB.linearVelocity != Vector3.zero)
            {
                _style += (int)_playerRB.linearVelocity.magnitude * styleScale;
            }

            if (_playerRB.angularVelocity.magnitude > 1)
            {
                _style += (int)_playerRB.angularVelocity.magnitude * styleScale;
            }

            _style -= styleDecay;

            if (_style < 1000)
            {
                _rank = "F";
                _text.color = Color.brown;
            }
            else if (_style > 1000 && _style < 2500)
            {
                _rank = "D";
                _text.color = Color.cyan;
            }
            else if (_style > 2500 && _style < 5000)
            {
                _rank = "C";
                _text.color = Color.green;
            }
            else if (_style > 5000 && _style < 7500)
            {
                _rank = "B";
                _text.color = Color.yellow;
            }
            else if (_style > 7500 && _style < 10000)
            {
                _rank = "A";
                _text.color = Color.orange;
            }
            else if (_style > 10000 && _style < 15000)
            {
                _rank = "S";
                _text.color = Color.red;
            }
            else if (_style > 15000 && _style < 20000)
            {
                _rank = "SS";
                _text.color = Color.violetRed;
            }
            else
            {
                _rank = "SSS";
                _text.color = Color.violet;
            }
        }
    }
}