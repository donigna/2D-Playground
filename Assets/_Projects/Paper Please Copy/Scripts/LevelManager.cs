using UnityEngine;

namespace com.Kuwiku
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance;

        public PlayerController playerPrefab;
        public InputManager inputManager;
        public CustomerManager customerManager;

        public bool openStore;


        public void Start()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }
            Instance = this;

            playerPrefab = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
            IInputReader inputReader = playerPrefab.GetComponent<IInputReader>();
            InputManager.Instance.RegisterPlayer(playerPrefab.playerId, inputReader);

            // Initial Customer
            customerManager.CreateNewCustomer();
        }

        public bool GetStoreOpenStatus()
        {
            if (openStore)
                return true;
            return false;
        }
    }
}
