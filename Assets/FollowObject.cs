using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public GameObject ObjectToFollow;

    void Update()
    {
        transform.position = ObjectToFollow.transform.position 
            + ObjectToFollow.transform.up*3
            - ObjectToFollow.transform.forward*4;
        transform.LookAt(ObjectToFollow.transform);
    }
}
