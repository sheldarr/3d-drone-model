using System;
using UnityEngine;

public class Drone : MonoBehaviour
{
    public GameObject FrontLeftPropeller;
    public GameObject FrontRightPropeller;
    public GameObject BackLeftPropeller;
    public GameObject BackRightPropeller;

    public GameObject FrontLeftPropellerCenter;
    public GameObject FrontRightPropellerCenter;
    public GameObject BackLeftPropellerCenter;
    public GameObject BackRightPropellerCenter;

    private const float PropellerMaxForce = 64;
    private const int PropellerMaxRpm = 28000;
    private const int PropellerMinRpm = 0;
    private const int PropellerRpmStep = 100;
    private const float GravityForce = 9.8f;

    private Rigidbody _rigidbody;

    private int _frontLeftPropellerActualRpm = GetGravityEquivalentRpm();
    private int _frontRightPropellerActualRpm = 100;
    private int _backLeftPropellerActualRpm = 1;
    private int _backRightPropellerActualRpm = 400;

    private static int GetGravityEquivalentRpm()
    {
        return (int)Math.Floor((GravityForce * PropellerMaxRpm) / PropellerMaxForce);
    }

    private static float CalculateActualForce(float rpm)
    {
        return rpm / PropellerMaxRpm * PropellerMaxForce;
    }

    private static float CalculateRotationAngle(float rpm)
    {
        return rpm * 60 * Time.deltaTime;
    }

    // Use this for initialization
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var actualPropellerForce = CalculateActualForce(_frontLeftPropellerActualRpm);
        var currentForce = transform.up*(actualPropellerForce - GravityForce);
        Debug.Log(string.Format("Power: {0}%", actualPropellerForce/PropellerMaxForce*100));
        _rigidbody.AddForce(currentForce, ForceMode.Force);

        if (_frontLeftPropellerActualRpm > PropellerMaxRpm)
        {
            _frontLeftPropellerActualRpm = PropellerMaxRpm;
        }

        if (_frontRightPropellerActualRpm > PropellerMaxRpm)
        {
            _frontRightPropellerActualRpm = PropellerMaxRpm;
        }

        if (_backLeftPropellerActualRpm > PropellerMaxRpm)
        {
            _backLeftPropellerActualRpm = PropellerMaxRpm;
        }

        if (_backRightPropellerActualRpm > PropellerMaxRpm)
        {
            _backRightPropellerActualRpm = PropellerMaxRpm;
        }

        if (_frontLeftPropellerActualRpm < 0)
        {
            _frontLeftPropellerActualRpm = 0;
        }

        if (_frontRightPropellerActualRpm < 0)
        {
            _frontRightPropellerActualRpm = 0;
        }

        if (_backLeftPropellerActualRpm < 0)
        {
            _backLeftPropellerActualRpm = 0;
        }

        if (_backRightPropellerActualRpm < 0)
        {
            _backRightPropellerActualRpm = 0;
        }

        if (Input.GetKey(KeyCode.S))
        {
            var gravityEquivalentRpm = GetGravityEquivalentRpm();

            _frontLeftPropellerActualRpm = gravityEquivalentRpm;
            _frontRightPropellerActualRpm = gravityEquivalentRpm;
            _backLeftPropellerActualRpm = gravityEquivalentRpm;
            _backRightPropellerActualRpm = gravityEquivalentRpm;
        }

        if (Input.GetKey(KeyCode.T))
        {
            if (_frontLeftPropellerActualRpm < PropellerMaxRpm)
            {
                _frontLeftPropellerActualRpm += PropellerRpmStep;
            }
        }
        if (Input.GetKey(KeyCode.Y))
        {
            if (_frontRightPropellerActualRpm < PropellerMaxRpm)
            {
                _frontRightPropellerActualRpm += PropellerRpmStep;
            }
        }
        if (Input.GetKey(KeyCode.G))
        {
            if (_backLeftPropellerActualRpm < PropellerMaxRpm)
            {
                _backLeftPropellerActualRpm += PropellerRpmStep;
            }
        }
        if (Input.GetKey(KeyCode.H))
        {
            if (_backRightPropellerActualRpm < PropellerMaxRpm)
            {
                _backRightPropellerActualRpm += PropellerRpmStep;
            }
        }

        if (Input.GetKey(KeyCode.I))
        {
            if (_frontLeftPropellerActualRpm > PropellerMinRpm)
            {
                _frontLeftPropellerActualRpm -= PropellerRpmStep;
            }
        }
        if (Input.GetKey(KeyCode.O))
        {
            if (_frontRightPropellerActualRpm > PropellerMinRpm)
            {
                _frontRightPropellerActualRpm -= PropellerRpmStep;
            }
        }
        if (Input.GetKey(KeyCode.K))
        {
            if (_backLeftPropellerActualRpm > PropellerMinRpm)
            {
                _backLeftPropellerActualRpm -= PropellerRpmStep;
            }
        }
        if (Input.GetKey(KeyCode.L))
        {
            if (_backRightPropellerActualRpm > PropellerMinRpm)
            {
                _backRightPropellerActualRpm -= PropellerRpmStep;
            }
        }

        FrontLeftPropeller.transform.RotateAround(FrontLeftPropellerCenter.transform.position,
            Vector3.up, CalculateRotationAngle(_frontLeftPropellerActualRpm));
        FrontRightPropeller.transform.RotateAround(FrontRightPropellerCenter.transform.position,
            Vector3.up, CalculateRotationAngle(_frontRightPropellerActualRpm));
        BackLeftPropeller.transform.RotateAround(BackLeftPropellerCenter.transform.position,
            Vector3.up, CalculateRotationAngle(_backLeftPropellerActualRpm));
        BackRightPropeller.transform.RotateAround(BackRightPropellerCenter.transform.position,
            Vector3.up, CalculateRotationAngle(_backRightPropellerActualRpm));
    }
}
