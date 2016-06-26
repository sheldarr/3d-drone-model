using UnityEngine;

public class WingsRotation : MonoBehaviour
{
	public const int SpeedMultiplier = 1400;

	public GameObject Wing1;
	public GameObject Wing2;
	public GameObject Wing3;
	public GameObject Wing4;

	public GameObject Wing1Center;
	public GameObject Wing2Center;
	public GameObject Wing3Center;
	public GameObject Wing4Center;

	void Start () {
		
	}
	
	void Update () {
		Wing1.transform.RotateAround(Wing1Center.gameObject.transform.position, Vector3.up, SpeedMultiplier * Time.deltaTime);
		Wing2.transform.RotateAround(Wing2Center.gameObject.transform.position, Vector3.up, SpeedMultiplier * Time.deltaTime);
		Wing3.transform.RotateAround(Wing3Center.gameObject.transform.position, Vector3.up, SpeedMultiplier * Time.deltaTime);
		Wing4.transform.RotateAround(Wing4Center.gameObject.transform.position, Vector3.up, SpeedMultiplier * Time.deltaTime);
	}
}
