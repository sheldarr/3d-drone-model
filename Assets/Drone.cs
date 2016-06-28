using System;
using UnityEngine;

public class Drone : MonoBehaviour
{
    private const float PropellerMaxForce = 12.8f;
    private const int PropellerMaxRpm = 28000;
    private const int PropellerMinRpm = 0;
    private const int PropellerRpmStep = 50;
    private const float GravityForce = 5;

    private Rigidbody _rigidbody;

    private int _frontLeftPropellerActualRpm = GetGravityEquivalentRpm();
    private int _frontRightPropellerActualRpm = GetGravityEquivalentRpm();
    private int _backLeftPropellerActualRpm = GetGravityEquivalentRpm();
    private int _backRightFrontPropellerActualRpm = GetGravityEquivalentRpm();

    private static int GetGravityEquivalentRpm()
    {
        return (int)Math.Floor((GravityForce * PropellerMaxRpm) / PropellerMaxForce);
    }

    private float CalculateActualForce(float rpm)
    {
        return rpm / PropellerMaxRpm * PropellerMaxForce;
    }

    // Use this for initialization
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _rigidbody.AddForce(transform.up * CalculateActualForce(_frontLeftPropellerActualRpm), ForceMode.Force);

        if (Input.GetKey(KeyCode.S))
        {
            var gravityEquivalentRpm = GetGravityEquivalentRpm();

            _frontLeftPropellerActualRpm = gravityEquivalentRpm;
            _frontRightPropellerActualRpm = gravityEquivalentRpm;
            _backLeftPropellerActualRpm = gravityEquivalentRpm;
            _backRightFrontPropellerActualRpm = gravityEquivalentRpm;
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
            if (_backRightFrontPropellerActualRpm < PropellerMaxRpm)
            {
                _backRightFrontPropellerActualRpm += PropellerRpmStep;
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
            if (_backRightFrontPropellerActualRpm > PropellerMinRpm)
            {
                _backRightFrontPropellerActualRpm -= PropellerRpmStep;
            }
        }

        Debug.Log(transform.up * CalculateActualForce(_frontLeftPropellerActualRpm));
    }
}
