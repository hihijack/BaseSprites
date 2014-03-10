using UnityEngine;
using System.Collections;

public class SwapRect : MonoBehaviour {

	public float width;
	public float height;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0f));
	}

	public Vector3 GetRandomPos(){
		Vector3 v3R = Vector3.zero;
		Vector3 posOri = transform.position;
		v3R = new Vector3(posOri.x + UnityEngine.Random.Range(-1 * width / 2, width / 2), posOri.y + UnityEngine.Random.Range(-1 * height / 2 , height /2), 0f);
		return v3R;
	}
}
