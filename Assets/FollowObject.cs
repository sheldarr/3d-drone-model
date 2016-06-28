using UnityEngine;

public class FollowObject : MonoBehaviour
{
    private Vector3 _offset;

    public GameObject ObjectToFollow;

    // Use this for initialization
    void Start()
    {
        _offset = transform.position - ObjectToFollow.transform.position;
    }

    void Update()
    {
        transform.position = ObjectToFollow.transform.position + _offset;
        Debug.Log(transform.position);
    }
}
