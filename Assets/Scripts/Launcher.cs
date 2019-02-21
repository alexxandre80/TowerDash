using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


namespace Com.TowerDash.TowerDash
{

    public class Launcher : MonoBehaviourPunCallbacks
    {
        #region Private Serializable Fields
            
        /// <summary>
        /// Le nombre maximum de joueurs par salle. Quand une salle est pleine, elle ne peut
        /// pas être rejointe par de nouveaux joueurs. Ainsi, une nouvelle salle sera créée..
        /// </summary>
        [Tooltip("Le nombre maximum de joueurs par salle. Quand une salle est pleine, elle ne peut pas être rejointe par de nouveaux joueurs. Ainsi, une nouvelle salle sera créée.")]
        [SerializeField]
        private byte maxPlayersPerRoom = 4;

        #endregion


        #region Private Fields


        /// <summary>
        /// Le numéro de version de ce client. Les utilisateurs sont séparés les uns des autres par gameVersion (ce qui vous permet d'apporter des modifications radicales).
        /// </summary>
        string gameVersion = "1";
        
        [Tooltip("Le panneau de l'interface utilisateur pour laisser l'utilisateur entrer le nom, se connecter et jouer")]
        [SerializeField]
        private GameObject controlPanel;
        [Tooltip("L'étiquette de l'interface utilisateur pour informer l'utilisateur que la connexion est en cours")]
        [SerializeField]
        private GameObject progressLabel;

        /// <summary>
        /// Gardez une trace du processus en cours. Comme la connexion est asynchrone et repose sur plusieurs rappels de Photon,
        /// nous devons garder cela à jour pour ajuster correctement le comportement lorsque nous recevons un rappel de Photon.
        /// Généralement, cela est utilisé pour le rappel OnConnectedToMaster ().
        /// </summary>
        bool isConnecting;

        #endregion
    
        public GameObject PlayerPrefab, SpawnPoint;

        #region MonoBehaviour CallBacks


        /// <summary>
        /// Méthode MonoBehaviour appelée sur GameObject par Unity au cours de la première phase d'initialisation.
        /// </summary>
        void Awake()
        {

            // cela garantit que nous pouvons utiliser PhotonNetwork.LoadLevel ()
            // sur le client maître et que tous les clients de la même room
            // synchronisent automatiquement leur niveau.
            PhotonNetwork.AutomaticallySyncScene = true;
        }


        /// <summary>
        /// Méthode MonoBehaviour appelée sur GameObject par Unity pendant la phase d’initialisation.
        /// </summary>
        void Start()
        {
            progressLabel.SetActive(false);
            controlPanel.SetActive(true);
        }


        #endregion


        #region Public Methods


        /// <summary>
        /// Démarrer le processus de connexion.
        /// - Si déjà connecté, nous essayons de rejoindre une salle au hasard
        /// - si pas encore connecté, connectez cette instance d'application au réseau Photon Cloud
        /// </summary>
        public void Connect()
        {
            // garde la volonté de rejoindre une salle, car lorsque nous reviendrons du jeu,
            // nous aurons un rappel que nous sommes connectés, nous devons donc savoir quoi faire ensuite.
            isConnecting = true;
            
            progressLabel.SetActive(true);
            controlPanel.SetActive(false);
            // nous vérifions si nous sommes connectés ou non, nous rejoignons si nous sommes, sinon nous établissons la connexion au serveur.
            if (PhotonNetwork.IsConnected)
            {
                // #Nous avons besoin à ce stade d'essayer de rejoindre une salle aléatoire.
                // Si cela échoue, nous en serons informés dans OnJoinRandomFailed () et nous en créerons un.
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                // #Nous devons d’abord et avant tout nous connecter à Photon Online Server.
                PhotonNetwork.GameVersion = gameVersion;
                PhotonNetwork.ConnectUsingSettings();
            }
        }


    #endregion
    
    
    #region MonoBehaviourPunCallbacks Callbacks


    public override void OnConnectedToMaster()
    {
        
        // nous ne voulons rien faire si nous n'essayons pas de rejoindre une salle.
        // ce cas où isConnecting est false est généralement le cas où vous avez perdu ou quitté le jeu,
        // quand ce niveau est chargé, OnConnectedToMaster sera appelé, dans ce cas
        // nous ne voulons rien faire.
        if (isConnecting){
        // #Le premier que nous essayons de faire est de rejoindre une salle existante potentielle.
        // S'il y en a, bien, sinon, nous serons rappelés avec OnJoinRandomFailed ()
        PhotonNetwork.JoinRandomRoom();
        }
    

    Debug.Log("PUN Basics Tutorial/Launcher:  OnConnectedToMaster () a été appelé par le PUN");
    }


    public override void OnDisconnected(DisconnectCause cause)
    {
        progressLabel.SetActive(false);
        controlPanel.SetActive(true);
        Debug.LogWarningFormat("PUN Basics Tutorial/Launcher: OnDisconnected() a été appelé par PUN avec raison {0}", cause);
    }
    
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("PUN Basics Tutorial/Launcher:OnJoinRandomFailed() a été appelé par PUN. Aucune salle aléatoire disponible, nous en créons une. \nAppel: PhotonNetwork.CreateRoom");

        // #Nous n'avons pas réussi à rejoindre une salle au hasard, peut-être qu'il n'en existe aucune
        // ou qu'elles sont toutes pleines. Pas de soucis, nous créons une nouvelle salle.
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
    }

    public override void OnJoinedRoom()
    {
        // #Nous ne chargeons que si nous sommes le premier joueur, sinon nous comptons sur
        // `PhotonNetwork.AutomaticallySyncScene` pour synchroniser notre scène d'instance.
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            Debug.Log("Nous chargeons le monde 'NewMap' ");

            // Charger le niveau de la pièce.
            PhotonNetwork.LoadLevel("NewMap");
        }
  
        Debug.Log("PUN Basics Tutorial/Launcher: OnJoinedRoom() appelé par PUN. Maintenant ce client est dans une room.");
    }


    #endregion


    }
}