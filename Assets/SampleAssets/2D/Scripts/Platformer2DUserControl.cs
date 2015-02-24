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
		private bool slowTime;
		private Animator anim;

        private void Awake()
        {
            character = GetComponent<PlatformerCharacter2D>();
			anim = GetComponent<Animator>();
        }

        private void Update()
        {
			if (Input.GetKey("escape"))
				Application.Quit();

			if (Input.GetKeyDown ("m")) 
			{
				anim.SetTrigger ("Die");
			}
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
			if (Input.GetKeyDown("s"))
			{
				slowTime = true;
				var enemySlow = GameObject.Find ("Enemies");
				Animator slow;
				if(enemySlow != null)
				{
					/*var slowEnemy = (EnemyBehavior)enemySlow.GetComponent("EnemyBehavior");
					slowEnemy.speed /= 2;
					if(enemySlow.rigidbody2D.velocity.x > 0.0f || enemySlow.rigidbody2D.velocity.y > 0.0f)
						enemySlow.rigidbody2D.velocity = slowEnemy.speed;
					else
						enemySlow.rigidbody2D.velocity = -slowEnemy.speed;
					*/
					foreach(Transform enemies in enemySlow.transform)
					{
						slow = GameObject.Find(enemies.name).GetComponent<Animator>();
						slow.speed /= 2.5f;
					}
				}
				
			}
			if (Input.GetKeyUp("s"))
			{
				slowTime = false;
				var enemySlow = GameObject.Find ("Enemies");
				Animator slow;
				
				if(enemySlow != null)
				{
					/*var slowEnemy = (EnemyBehavior)enemySlow.GetComponent("EnemyBehavior");
					slowEnemy.speed *= 2;
					if(enemySlow.rigidbody2D.velocity.x > 0.0f || enemySlow.rigidbody2D.velocity.y > 0.0f)
						enemySlow.rigidbody2D.velocity = slowEnemy.speed;
					else
						enemySlow.rigidbody2D.velocity = -slowEnemy.speed;
					*/
					foreach(Transform enemies in enemySlow.transform)
					{
						slow = GameObject.Find(enemies.name).GetComponent<Animator>();
						slow.speed *= 2.5f;
					}
				}
			}
        }

        private void FixedUpdate()
        {
            // Read the inputs.
			float h = Input.GetAxis ("Horizontal");
            // Pass all parameters to the character control script.
			character.Move(h, jump, gravity, teleport, slowTime);
			gravity = false;
            jump = false;
			teleport = false;
        }
    }
}