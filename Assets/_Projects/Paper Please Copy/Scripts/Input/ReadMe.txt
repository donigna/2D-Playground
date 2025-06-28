Code Input disini merupakan code modular yang aku ciptakan untuk keperluan pribadi

================================
Struktur dari kode ini adalah : 

IInputReader : Sebagai interface, code ini akan menjadi interface bagi beberapa reader input seperti local input dan networkInput

InputManager : Input Manager bertugas untuk mengatur jalannya input baik satu input atau lebih, masalahnya ia juga bertugas untuk mengelola jenis inputan dari player

LocalInputReader : Seperti yang aku katakan sebelumnya, code ini bertugas untuk mengatur aksi dari IInputReader untuk local player

NetworkInputReader : Sama seperti LocalInputReader, code ini bertugas untuk mengatur aksi dari IInputReader namun untuk player dari network

=================
Penggunaan Code
=================

Untuk mendaftarkan Player, maka Player perlu di instantiasi dan data IInputReader yang dimiliki player dimasukkan didaftarkan ke dalam InputManager. 

Contohnya : 
 	playerPrefab = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
 	IInputReader inputReader = playerPrefab.GetComponent<IInputReader>();
 	InputManager.Instance.RegisterPlayer(playerPrefab.playerId, inputReader);

Untuk menggunakan Input Action, berikut ini adalah contohnya di PlayerController :
	public int playerId;
        private IInputReader _input;

        void Awake()
        {
            _input = GetComponent<IInputReader>();
        }

        void Update()
        {
            if (_input == null)
            {
                Debug.LogError("Input is not assigned or missing on PlayerController.");
                return;
            }

            // Read movement input
            Vector2 move = _input.ReadMovement();
            if (move != Vector2.zero)
            {
                Debug.Log($"Player {playerId} is moving: {move}");
            }

            // Check for jump input
            if (_input.IsJumpPerformed())
            {
                Debug.Log($"Player {playerId} jumped!");
            }
        }