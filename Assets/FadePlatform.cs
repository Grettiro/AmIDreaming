using UnityEngine;
using System.Collections;

public class FadePlatform : MonoBehaviour {

	public float fadeTime = 0.0f;

	// Use this for initialization
	void Start () {
		DeathTracker difficulty = GameObject.Find ("DeathTracker").GetComponent<DeathTracker> ();
		fadeTime = 2 - (float)difficulty.Difficulty / 10 * 2;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player") 
		{
			StartCoroutine(FadeOut (other));
		}
	}
	private IEnumerator FadeOut(Collider2D other)
	{
		yield return new WaitForSeconds(fadeTime);
		float alpha = this.GetComponent<SpriteRenderer>().color.a;
		for(float t = 0.0f; t < 1.0f; t +=Time.deltaTime / 0.3f)
		{
			Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha,0f, t));
			this.GetComponent<SpriteRenderer>().color = newColor;
			yield return null;
		}
		Destroy (this.gameObject);
	}
}
		               