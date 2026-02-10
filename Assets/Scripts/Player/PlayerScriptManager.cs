
using UnityEngine;
namespace Scripts.Player
{
    public class PlayerScriptManager : MonoBehaviour
    {
        //private Rigidbody rbPlayer;
        [Header("Player Settings")]
        //[SerializeField] private float defaultForwardSpeed = 5;
        //[SerializeField] private int defaultHealth = 100;
        //[SerializeField] private GameObject focalPoint;

        private PlayerController playerController;
        private PlayerColliderAndTrigger playerColTrig;
        private PlayerEffects playerEffects;
        private PlayerStats playerStats;


        private void Awake()
        {
            playerController =  gameObject.AddComponent<PlayerController>();
            playerColTrig =     gameObject.AddComponent<PlayerColliderAndTrigger>();
            playerEffects =     gameObject.AddComponent<PlayerEffects>();
            playerStats =       gameObject.AddComponent<PlayerStats>();
            //rbPlayer =          this.gameObject.GetComponent<Rigidbody>();
            //focalPoint =        GameObject.Find("FocalPoint");

            //playerController.Initialize(rbPlayer, focalPoint, defaultForwardSpeed);
            //playerStats.Initialize(defaultHealth);
            //playerColTrig.Initialize(playerEffects);
        }

    }
}