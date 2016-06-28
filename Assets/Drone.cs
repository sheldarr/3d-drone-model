using System;
using UnityEditor;
using UnityEngine;

public class Drone : MonoBehaviour
{
    public GameObject FrontLeftPropeller;
    public GameObject FrontRightPropeller;
    public GameObject BackLeftPropeller;
    public GameObject BackRightPropeller;

    public GameObject DroneCenter;
    public GameObject FrontLeftPropellerCenter;
    public GameObject FrontRightPropellerCenter;
    public GameObject BackLeftPropellerCenter;
    public GameObject BackRightPropellerCenter;

    private const float PropellerMaxForce = 4.4f;
    private const int PropellerMaxRpm = 28000;
    private const int PropellerMinRpm = 0;
    private const int PropellerRpmStep = 100;
    private const float GravityForce = 9.8f;
    private const float RotationSpeed = 1.0f;
    private const float MaxBackForwardSpeed = 3f;
    private const float BackForwardSpeedStep = 0.1f;
    private const float TiltSpeed = 0.2f;
    private const float MaxTilt = 14.2f;

    private bool _dKeyPressed, _aKeyPressed;

    private Rigidbody _rigidbody;

    private int _frontLeftPropellerActualRpm = GetGravityEquivalentRpm();
    private int _frontRightPropellerActualRpm = GetGravityEquivalentRpm();
    private int _backLeftPropellerActualRpm = GetGravityEquivalentRpm();
    private int _backRightPropellerActualRpm = GetGravityEquivalentRpm();

    private float _backForwardSpeed;

    private float _eulerZ;

    private static int GetGravityEquivalentRpm()
    {
        return (int)Math.Floor((GravityForce / 4 * PropellerMaxRpm) / PropellerMaxForce);
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
        _eulerZ = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _aKeyPressed = _dKeyPressed = false;

        var actualFrontLeftPropellerForce = CalculateActualForce(_frontLeftPropellerActualRpm);
        var actualFrontRightPropellerForce = CalculateActualForce(_frontRightPropellerActualRpm);
        var actualBackLeftPropellerForce = CalculateActualForce(_backLeftPropellerActualRpm);
        var actualBackRightPropellerForce = CalculateActualForce(_backRightPropellerActualRpm);

        Debug.Log(string.Format("Power: FL{0}% FR{1}% BL{2}% BR{3}%", actualFrontLeftPropellerForce / PropellerMaxForce * 100,
            actualFrontRightPropellerForce / PropellerMaxForce * 100, actualBackLeftPropellerForce / PropellerMaxForce * 100,
            actualBackRightPropellerForce / PropellerMaxForce * 100));

        var summaryForce = actualFrontLeftPropellerForce + actualFrontRightPropellerForce
            + actualBackLeftPropellerForce + actualBackRightPropellerForce;

        var velocity = transform.forward * _backForwardSpeed;
        velocity.y = summaryForce - GravityForce;

        _rigidbody.velocity = velocity;

        if (_backForwardSpeed > MaxBackForwardSpeed)
        {
            _backForwardSpeed = MaxBackForwardSpeed;
        }

        if (_backForwardSpeed < -MaxBackForwardSpeed)
        {
            _backForwardSpeed = -MaxBackForwardSpeed;
        }

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

        if (Input.GetKey(KeyCode.Q))
        {
            _frontLeftPropellerActualRpm = GetGravityEquivalentRpm();
            _frontRightPropellerActualRpm = GetGravityEquivalentRpm();
            _backLeftPropellerActualRpm = GetGravityEquivalentRpm();
            _backRightPropellerActualRpm = GetGravityEquivalentRpm();
        }

        if (Input.GetKey(KeyCode.R))
        {
            if (_frontLeftPropellerActualRpm < PropellerMaxRpm)
            {
                _frontLeftPropellerActualRpm += PropellerRpmStep;
            }
            if (_frontRightPropellerActualRpm < PropellerMaxRpm)
            {
                _frontRightPropellerActualRpm += PropellerRpmStep;
            }
            if (_backLeftPropellerActualRpm < PropellerMaxRpm)
            {
                _backLeftPropellerActualRpm += PropellerRpmStep;
            }
            if (_backRightPropellerActualRpm < PropellerMaxRpm)
            {
                _backRightPropellerActualRpm += PropellerRpmStep;
            }
        }

        if (Input.GetKey(KeyCode.F))
        {
            if (_frontLeftPropellerActualRpm > PropellerMinRpm)
            {
                _frontLeftPropellerActualRpm -= PropellerRpmStep;
            }
            if (_frontRightPropellerActualRpm > PropellerMinRpm)
            {
                _frontRightPropellerActualRpm -= PropellerRpmStep;
            }
            if (_backLeftPropellerActualRpm > PropellerMinRpm)
            {
                _backLeftPropellerActualRpm -= PropellerRpmStep;
            }
            if (_backRightPropellerActualRpm > PropellerMinRpm)
            {
                _backRightPropellerActualRpm -= PropellerRpmStep;
            }
        }

        if (Input.GetKey(KeyCode.U))
        {
            if (_frontLeftPropellerActualRpm < PropellerMaxRpm)
            {
                _frontLeftPropellerActualRpm += PropellerRpmStep;
            }
        }
        if (Input.GetKey(KeyCode.I))
        {
            if (_frontRightPropellerActualRpm < PropellerMaxRpm)
            {
                _frontRightPropellerActualRpm += PropellerRpmStep;
            }
        }
        if (Input.GetKey(KeyCode.J))
        {
            if (_backLeftPropellerActualRpm < PropellerMaxRpm)
            {
                _backLeftPropellerActualRpm += PropellerRpmStep;
            }
        }
        if (Input.GetKey(KeyCode.K))
        {
            if (_backRightPropellerActualRpm < PropellerMaxRpm)
            {
                _backRightPropellerActualRpm += PropellerRpmStep;
            }
        }

        if (Input.GetKey(KeyCode.O))
        {
            if (_frontLeftPropellerActualRpm > PropellerMinRpm)
            {
                _frontLeftPropellerActualRpm -= PropellerRpmStep;
            }
        }
        if (Input.GetKey(KeyCode.P))
        {
            if (_frontRightPropellerActualRpm > PropellerMinRpm)
            {
                _frontRightPropellerActualRpm -= PropellerRpmStep;
            }
        }
        if (Input.GetKey(KeyCode.L))
        {
            if (_backLeftPropellerActualRpm > PropellerMinRpm)
            {
                _backLeftPropellerActualRpm -= PropellerRpmStep;
            }
        }
        if (Input.GetKey(KeyCode.Semicolon))
        {
            if (_backRightPropellerActualRpm > PropellerMinRpm)
            {
                _backRightPropellerActualRpm -= PropellerRpmStep;
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            _aKeyPressed = true;

            transform.RotateAround(DroneCenter.transform.position, Vector3.up, -RotationSpeed);

            if (_eulerZ < MaxTilt)
            {
                _eulerZ += TiltSpeed;
            }

            UpdateEulerAngle();
        }

        if (Input.GetKey(KeyCode.D))
        {
            _dKeyPressed = true;
            transform.RotateAround(DroneCenter.transform.position, Vector3.up, RotationSpeed);

            if (_eulerZ > -MaxTilt)
            {
                _eulerZ -= TiltSpeed;
            }

            UpdateEulerAngle();
        }

        if (Input.GetKey(KeyCode.W))
        {
            _backForwardSpeed += BackForwardSpeedStep;
        }

        if (Input.GetKey(KeyCode.S))
        {
            _backForwardSpeed -= BackForwardSpeedStep;
        }

        FrontLeftPropeller.transform.RotateAround(FrontLeftPropellerCenter.transform.position,
            transform.up, CalculateRotationAngle(_frontLeftPropellerActualRpm));
        FrontRightPropeller.transform.RotateAround(FrontRightPropellerCenter.transform.position,
            transform.up, CalculateRotationAngle(_frontRightPropellerActualRpm));
        BackLeftPropeller.transform.RotateAround(BackLeftPropellerCenter.transform.position,
            transform.up, CalculateRotationAngle(_backLeftPropellerActualRpm));
        BackRightPropeller.transform.RotateAround(BackRightPropellerCenter.transform.position,
            transform.up, CalculateRotationAngle(_backRightPropellerActualRpm));

        StraigtenShipUp();
    }

    private void UpdateEulerAngle()
    {
        Vector3 euler = transform.localEulerAngles;
        euler.z = _eulerZ;
        transform.localEulerAngles = euler;
    }

    private void StraigtenShipUp()
    {
        if (!_aKeyPressed && !_dKeyPressed)
        {
            if (_eulerZ < 0)
            {
                _eulerZ += TiltSpeed;
            }
            else
            {
                _eulerZ -= TiltSpeed;
            }

            UpdateEulerAngle();
        }
    }
}
