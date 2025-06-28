using UnityEngine;

namespace com.Kuwiku
{
    public class LevelManager : MonoBehaviour
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
