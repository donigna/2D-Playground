using UnityEngine;

namespace com.Kuwiku.Basic2D
{
    public class BasicManager2D : MonoBehaviour
    {
        public PlayerController playerPrefab;
        public InputManager inputManager;

        public void Start()
        {
            playerPrefab = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
            IInputReader inputReader = playerPrefab.GetComponent<IInputReader>();
            InputManager.Instance.RegisterPlayer(playerPrefab.playerId, inputReader);
        }

    }

}
