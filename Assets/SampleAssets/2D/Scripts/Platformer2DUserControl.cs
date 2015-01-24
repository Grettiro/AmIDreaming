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

        private void Awake()
        {
            character = GetComponent<PlatformerCharacter2D>();
        }

        private void Update()
        {
			if (Input.GetButtonDown ("Jump")) 
					jump = true;
			if (Input.GetKeyDown("d"))
			{
					gravity = true;
			}	
        }

        private void FixedUpdate()
        {
            // Read the inputs.
			float h = Input.GetAxis ("Horizontal");
            // Pass all parameters to the character control script.
			character.Move (h, jump, gravity);
			gravity = false;
            jump = false;
        }
    }
}