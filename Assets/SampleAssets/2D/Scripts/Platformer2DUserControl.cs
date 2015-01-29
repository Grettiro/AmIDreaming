using UnityEngine;

namespace UnitySampleAssets._2D
{

    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D character;
        private bool jump;
		private int doubleJump;
		private bool gravity;
		private bool teleport;

        private void Awake()
        {
            character = GetComponent<PlatformerCharacter2D>();
        }

        private void Update()
        {
			if (Input.GetButtonDown("Jump")) 
			{
				jump = true;
			}
			if (Input.GetKeyDown("d"))
			{
				gravity = true;
			}
			if (Input.GetKeyDown("e"))
			{
				teleport = true;
			}
        }

        private void FixedUpdate()
        {
            // Read the inputs.
			float h = Input.GetAxis ("Horizontal");
            // Pass all parameters to the character control script.
			character.Move(h, jump, gravity, teleport);
			gravity = false;
            jump = false;
			teleport = false;
        }
    }
}