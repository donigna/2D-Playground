using System;
using System.Collections.Generic;
using UnityEngine;

namespace com.Kuwiku
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance { get; private set; }

        /// <summary>
        ///  Dictionary to hold input readers for each player.
        ///  Key: Player ID, Value: IInputReader instance for that player.
        /// </summary>
        private Dictionary<int, IInputReader> _inputReaders;

        /// <summary>
        /// This one is  used to register a player with their input reader.
        /// It allows the InputManager to manage multiple players' inputs.
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="inputReader"></param>
        public void RegisterPlayer(int playerId, IInputReader inputReader)
        {
            if (_inputReaders == null)
            {
                _inputReaders = new Dictionary<int, IInputReader>();
            }

            if (!_inputReaders.ContainsKey(playerId))
            {
                _inputReaders.Add(playerId, inputReader);
            }
            else
            {
                Debug.LogWarning($"Player ID {playerId} is already registered.");
            }
        }

        /// <summary>
        ///  Unregisters a player by their ID.
        /// This is useful for cleaning up when a player leaves or is removed from the game.
        /// </summary>
        /// <param name="playerId"></param>
        public void UnregisterPlayer(int playerId)
        {
            if (_inputReaders != null && _inputReaders.ContainsKey(playerId))
            {
                _inputReaders.Remove(playerId);
            }
            else
            {
                Debug.LogWarning($"Player ID {playerId} is not registered.");
            }
        }

        /// <summary>
        ///  Retrieves the input reader for a specific player ID.
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        public IInputReader GetInputReader(int playerId)
        {
            if (_inputReaders != null && _inputReaders.TryGetValue(playerId, out var inputReader))
            {
                return inputReader;
            }
            else
            {
                Debug.LogWarning($"No input reader found for Player ID {playerId}.");
                return null;
            }
        }

        void Awake()
        {
            // Ensure this script is only executed once
            // Singleton pattern to ensure only one instance of InputManager exists
            if (Instance == null) Instance = this;
            else Destroy(gameObject);

        }
    }
}
