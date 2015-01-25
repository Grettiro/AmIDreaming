using UnityEngine;

namespace UnitySampleAssets._2D
{

    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D character;
        private bool jump;
		private bool gravity;
		private bool dash;

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
				dash = true;
			}
        }

        private void FixedUpdate()
        {
            // Read the inputs.
			float h = Input.GetAxis ("Horizontal");
            // Pass all parameters to the character control script.
            character.Move(h, jump, gravity, dash);
			gravity = false;
            jump = false;
			dash = false;
        }
    }
}