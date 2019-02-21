using System;
using System.Collections;


using UnityEngine;
using UnityEngine.SceneManagement;


using Photon.Pun;
using Photon.Realtime;


namespace Com.TowerDash.TowerDash
{
    /// <summary>
    /// Caméra suis une cible
    /// </summary>
    public class CameraWork : MonoBehaviour
    {


        #region Public Properties


        [Tooltip("La distance dans le plan local x-z à la cible")]
        public float distance = 7.0f;


        [Tooltip("La hauteur à laquelle nous voulons que la caméra soit au-dessus de la cible")]
        public float height = 3.0f;

    
        [Tooltip("Le décalage temporel pour la hauteur de la caméra.")]
        public float heightSmoothLag = 0.3f;


        [Tooltip("Autorisez la caméra à avoir une verticale par rapport à la cible, par exemple en donnant plus de visibilité sur le paysage et moins de terrain.")]
        public Vector3 centerOffset = Vector3.zero;


        [Tooltip("Définissez-la sur false si un composant d'un préfabriqué est instancié par Photon Network et appelez manuellement OnStartFollowing () lorsque et si nécessaire.")]
        public bool followOnStart = false;


        #endregion


        #region Private Properties


        // Transformation en cache de la cible
        Transform cameraTransform;


        // maintenir un drapeau interne pour se reconnecter si la cible est perdue ou si la caméra est commutée
        bool isFollowing;


        // Représente la vélocité actuelle, cette valeur est modifiée par SmoothDamp () chaque fois que vous l'appelez.
        private float heightVelocity = 0.0f;

        // Représente la position que nous essayons d'atteindre à l'aide de SmoothDamp ()
        private float targetHeight = 100000.0f;


        #endregion


        #region MonoBehaviour Messages


        /// <summary>
        /// Méthode MonoBehaviour appelée sur GameObject par Unity lors de la phase d'initialisation
        /// </summary>
        void Start()
        {
            // Commencez à suivre la cible
            if (followOnStart)
            {
                OnStartFollowing();
            }


        }


        /// <summary>
        /// Méthode MonoBehaviour appelée après que toutes les fonctions de mise à jour ont été appelées.
        /// Ceci est utile pour commander l'exécution du script. Par exemple, une caméra de suivi doit
        /// toujours être implémentée dans LateUpdate, car elle suit les objets susceptibles d'avoir été déplacés dans Update.
        /// </summary>
        void LateUpdate()
        {
            // La cible de transformation ne peut pas détruire à pleine charge,
            // // nous devons donc couvrir les cas où la caméra principale est différente à chaque fois que nous chargeons
            // une nouvelle scène et que nous nous reconnectons lorsque cela se produit.
            if (cameraTransform == null && isFollowing)
            {
                OnStartFollowing();
            }


            // Seulement suivre
            if (isFollowing)
            {
                Apply ();
            }
        }


        #endregion


        #region Public Methods


        /// <summary>
        /// Déclenche l'événement suivant.
        /// /// Utilisez ceci lorsque vous ne savez pas au moment de l'édition ce qu'il faut suivre, généralement des instances gérées par le réseau photon.
        /// </summary>
        public void OnStartFollowing()
        {
            cameraTransform = Camera.main.transform;
            isFollowing = true;
            // Nous ne lissons rien, nous allons directement au bon coup de caméra
            Cut();
        }


        #endregion


        #region Private Methods


        /// <summary>
        /// Suivez la cible en douceur
        /// </summary>
        void Apply()
        {
            Vector3 targetCenter = transform.position + centerOffset;


            // Calculer les angles de rotation actuels et cibles
            float originalTargetAngle = transform.eulerAngles.y;
            float currentAngle = cameraTransform.eulerAngles.y;


            // Ajustez l'angle réel de la cible lorsque la caméra est verrouillée
            float targetAngle = originalTargetAngle;


            currentAngle = targetAngle;


            targetHeight = targetCenter.y + height;


            // Amortir la hauteur
            float currentHeight = cameraTransform.position.y;
            currentHeight = Mathf.SmoothDamp( currentHeight, targetHeight, ref heightVelocity, heightSmoothLag );


            // Convertit l'angle en une rotation par laquelle on repositionne ensuite la caméra
            Quaternion currentRotation = Quaternion.Euler( 0, currentAngle, 0 );


            // Définit la position de la caméra sur le plan x-z ​​sur la
            // distance en mètres derrière la cible
            cameraTransform.position = targetCenter;
            cameraTransform.position += currentRotation * Vector3.back * distance;


            // Réglez la hauteur de la caméra
            cameraTransform.position = new Vector3( cameraTransform.position.x, currentHeight, cameraTransform.position.z );


            // Toujours regarder la cible
            SetUpRotation(targetCenter);
        }


        /// <summary>
        /// Positionnez directement la caméra sur la cible et le centre spécifiés.
        /// </summary>
        void Cut( )
        {
            float oldHeightSmooth = heightSmoothLag;
            heightSmoothLag = 0.001f;


            Apply();


            heightSmoothLag = oldHeightSmooth;
        }


        /// <summary>
        /// /// Configure la rotation de la caméra pour qu'elle soit toujours derrière la cible
        /// /// </summary>
        /// /// <param name = "centerPos"> Position centrale. </param>
        void SetUpRotation( Vector3 centerPos )
        {
            Vector3 cameraPos = cameraTransform.position;
            Vector3 offsetToCenter = centerPos - cameraPos;


            // Générer une rotation de base uniquement autour de l'axe des y
            Quaternion yRotation = Quaternion.LookRotation( new Vector3( offsetToCenter.x, 0, offsetToCenter.z ) );


            Vector3 relativeOffset = Vector3.forward * distance + Vector3.down * height;
            cameraTransform.rotation = yRotation * Quaternion.LookRotation( relativeOffset );


        }


        #endregion
    }
}
