using Scripts.Systems;
using UnityEngine;

namespace Scripts.Player
{
    public class PlayerInputs: MonoBehaviour
    {
        public void Update()
        {
            float move = Input.GetAxis("Vertical");
            //float rotate = Input.GetAxis("Horizontal");

            for (int i = 0; i < PlayerRegistry.Count; i++)
            {
                PlayerRegistry.SetMoveIntent(i, move);
            }

        }
    }
}
