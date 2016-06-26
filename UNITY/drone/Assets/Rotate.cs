using UnityEngine;

public class Rotate : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.RotateAround(new Vector3(105.095f, 7.1181f, 55.4007f), Vector3.up, 1000 * Time.deltaTime);

	}
}
