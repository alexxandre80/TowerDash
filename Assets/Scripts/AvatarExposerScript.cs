using Photon.Pun;
using UnityEngine;

namespace MyPhotonProject.Scripts
{
    public class AvatarExposerScript : MonoBehaviour
    {
        public Rigidbody AvatarRigidBody;
        public PhotonRigidbodyView AvatarRigidBodyView;
        public Camera AvatarCamera;
        public Transform AvatarRootTransform;
        public GameObject AvatarRootGameObject;
    }
}
