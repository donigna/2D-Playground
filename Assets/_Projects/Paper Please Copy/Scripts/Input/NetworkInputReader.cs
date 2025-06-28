using UnityEngine;

namespace com.Kuwiku
{
    /// <summary>
    /// Represents the input data structure used for network input reading.
    /// This struct contains all data that sent over the network for player input.
    /// It includes movement direction and jump action. 
    [System.Serializable]
    public struct InputData
    {
        public Vector2 Move;
        public bool JumpPressed;
    }


    /// <summary>
    /// This script is on progress i dont know it is working or not.
    /// It is used to read input data from the network.
    /// and then update the latest input data.
    /// This is useful for multiplayer games where input data is sent over the network.
    /// </summary>
    public class NetworkInputReader : MonoBehaviour, IInputReader
    {
        private InputData latestInputData;

        public void UpdateFromNetwork(InputData inputData)
        {
            latestInputData = inputData;
        }

        #region Action Callbacks
        public Vector2 ReadMovement()
        {
            return latestInputData.Move;
        }

        public bool IsJumpPerformed()
        {
            return latestInputData.JumpPressed;
        }

        public IInputReader.ActionButtonState GetActionButtonState()
        {
            throw new System.NotImplementedException();
        }
        #endregion

    }
}
