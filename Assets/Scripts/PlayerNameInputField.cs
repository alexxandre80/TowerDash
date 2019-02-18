using UnityEngine;
using UnityEngine.UI;


using Photon.Pun;
using Photon.Realtime;


using System.Collections;


namespace Com.MyCompany.MyGame
{
    /// <summary>
    /// Champ de saisie du nom du joueur. Laissez l’utilisateur entrer son nom,
    /// apparaîtra au-dessus du joueur dans le jeu.
    /// </summary>
    [RequireComponent(typeof(InputField))]
    public class PlayerNameInputField : MonoBehaviour
    {
        #region Private Constants


        // Store the PlayerPref Key to avoid typos
        const string playerNamePrefKey = "PlayerName";


        #endregion


        #region MonoBehaviour CallBacks


        /// <summary>
        /// Méthode MonoBehaviour appelée sur GameObject par Unity pendant la phase d’initialisation.
        /// </summary>
        void Start () {


            string defaultName = string.Empty;
            InputField _inputField = this.GetComponent<InputField>();
            if (_inputField!=null)
            {
                if (PlayerPrefs.HasKey(playerNamePrefKey))
                {
                    defaultName = PlayerPrefs.GetString(playerNamePrefKey);
                    _inputField.text = defaultName;
                }
            }


            PhotonNetwork.NickName =  defaultName;
        }


        #endregion


        #region Public Methods


        /// <summary>
        /// Définit le nom du joueur et l’enregistre dans PlayerPrefs pour les sessions futures.
        /// </summary>
        /// <param name="value">Le nom du joueur</param>
        public void SetPlayerName(string value)
        {
            // #Important
            if (string.IsNullOrEmpty(value))
            {
                Debug.LogError("Le nom du joueur est nul ou vide");
                return;
            }
            PhotonNetwork.NickName = value;


            PlayerPrefs.SetString(playerNamePrefKey,value);
        }


        #endregion
    }
}