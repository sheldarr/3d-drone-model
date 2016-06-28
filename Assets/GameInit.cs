using UnityEngine;
using System.Collections;

public class GameInit : MonoBehaviour
{
	public GameObject DroneModel;
	public GameObject Wing1;
	public GameObject Wing2;
	public GameObject Wing3;
	public GameObject Wing4;

	// Use this for initialization
	void Start () {
		DroneModel.GetComponent<Renderer>().material.color = Color.black;
		Wing1.GetComponent<Renderer>().material.color = Color.red;
		Wing2.GetComponent<Renderer>().material.color = Color.red;
		Wing3.GetComponent<Renderer>().material.color = Color.red;
		Wing4.GetComponent<Renderer>().material.color = Color.red;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
