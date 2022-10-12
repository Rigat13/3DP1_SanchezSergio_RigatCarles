using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    [Header("Rotation")]
    float yaw = 0;
    float pitch = 0;
    [SerializeField] float yawSpeed = 5.0f;
    [SerializeField] float pitchSpeed = 5.0f;
    [SerializeField] Transform pitchController;

    [SerializeField] bool invertYaw;
    [SerializeField] bool invertPitch;
    [SerializeField] float minPitch;
    [SerializeField] float maxPitch;


    [Header("Planar Movement")]
    [SerializeField] CharacterController characterController;
    [SerializeField] float walkSpeed = 5.0f;
    [SerializeField] float runSpeed = 10.0f;
    float vx_currentSpeed;
    Vector3 direction;

    [SerializeField] KeyCode runKey = KeyCode.LeftShift;
    [SerializeField] KeyCode forwardKey = KeyCode.W;
    [SerializeField] KeyCode backwardKey = KeyCode.S;
    [SerializeField] KeyCode rightKey = KeyCode.D;
    [SerializeField] KeyCode leftKey = KeyCode.A;


    [Header("Jump / Vertical Movement")]
    [SerializeField] float h_maxHeight = 4.0f;
    [SerializeField] float released_jump_gravity_multiplier = 2.0f;
    
    [SerializeField] float th1_timeToMaxHeight = 1f;
    [SerializeField] float th2_timeToMinHeight = 0.5f;

    float th_current_timeToMaxHeight;

    [SerializeField] int numberOfJumps = 2;
    int jumpsDone;

    float a_gravity;
    float v0_jumpSpeed;
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    
    float vy_verticalSpeed;
    bool onGround;
    bool onCeiling;
    bool peakReached;
    
    [Header("Mouse Debug")]
    [SerializeField] KeyCode angleLockKey = KeyCode.I;
    [SerializeField] KeyCode mouseLockKey = KeyCode.O;
    private bool angleLocked = false;


    void Awake()
    {
        yaw = transform.rotation.eulerAngles.y;
        pitch = pitchController.rotation.eulerAngles.x;

        th_current_timeToMaxHeight = th1_timeToMaxHeight;
        updateJumpValues();
    }

    void Update() 
    {
        inputUpdate();
        updateLockKeyState();
    }

    void FixedUpdate()
    {
        if (!angleLocked)
            rotate();
        
        move();
    }

    void rotate()
    {
        float xMouse = Input.GetAxis("Mouse X");
        float yMouse = Input.GetAxis("Mouse Y");
        updateYaw(xMouse);
        updatePitch(yMouse);
        
        transform.rotation = Quaternion.Euler(0, yaw, 0);
        pitchController.localRotation = Quaternion.Euler(pitch, 0, 0);
    }

    void updateYaw(float xMouse) {
        yaw += xMouse * yawSpeed * (invertYaw ? -1 : 1);
    }

    void updatePitch(float yMouse) {
        pitch -= yMouse * pitchSpeed * (invertPitch ? -1 : 1);
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
    }

    void updateLockKeyState()
    {
        if (Input.GetKeyDown(angleLockKey))
            angleLocked = !angleLocked;

        if (Input.GetKeyDown(mouseLockKey))
            Cursor.lockState = (Cursor.lockState == CursorLockMode.Locked) ? CursorLockMode.None : CursorLockMode.Locked;
    }

    void inputUpdate()
    {
        Vector3 forward = getForward();
        Vector3 right = getRight();
        updateDirectionByInput(forward, right);
        updateHorizontalSpeedByInput();
        updateVerticalSpeedByInput();
    }

    void updateDirectionByInput(Vector3 forward, Vector3 right){
        direction = Vector3.zero;

        if (Input.GetKey(forwardKey)) direction += forward;
        if (Input.GetKey(backwardKey)) direction -= forward;
        if (Input.GetKey(rightKey)) direction += right;
        if (Input.GetKey(leftKey)) direction -= right;
    }

    void updateHorizontalSpeedByInput()
    {
        if (Input.GetKey(runKey) && onGround) vx_currentSpeed = runSpeed;
        else vx_currentSpeed = walkSpeed;
    }

    void updateVerticalSpeedByInput()
    {
        if (Input.GetKeyDown(jumpKey) && jumpsDone < numberOfJumps) 
        {
            if (jumpsDone > 0) land(false);
            jump();
            jumpsDone++;
        }
        if (Input.GetKeyUp(jumpKey) && !onGround && !peakReached)
        {
            a_gravity *= released_jump_gravity_multiplier;
        }
    }

    void jump()
    {
        updateJumpValues();
        
        vy_verticalSpeed = v0_jumpSpeed;
        
    }

    void updateJumpValues()
    {
        v0_jumpSpeed = (2 * h_maxHeight) / th_current_timeToMaxHeight;
        a_gravity = (-2 * h_maxHeight) / (th_current_timeToMaxHeight * th_current_timeToMaxHeight);
    }

    void move()
    {
        Vector3 movement = calculateMovement();

        CollisionFlags flags = characterController.Move(movement);
        updateFlags(flags);
        if (onCeiling) vy_verticalSpeed = a_gravity * Time.fixedDeltaTime;
        
    }

    Vector3 calculateMovement()
    {
        Vector3 pos_movement = direction.normalized * Time.fixedDeltaTime * vx_currentSpeed;

        pos_movement.y += vy_verticalSpeed * Time.fixedDeltaTime + 0.5f * a_gravity * Time.fixedDeltaTime * Time.fixedDeltaTime;
        vy_verticalSpeed += a_gravity * Time.fixedDeltaTime;

        return pos_movement;
    }

    void updateFlags(CollisionFlags flags)
    {
        bool previousOnGround = onGround;

        onGround = (flags & CollisionFlags.Below) != 0;
        onCeiling = (flags & CollisionFlags.Above) != 0;


        if (!previousOnGround && onGround) land(onGround);
        if (!onGround && !peakReached && vy_verticalSpeed < 0) reachPeak();
    }

    void land(bool onRealGround)
    {
        jumpsDone = onRealGround ? 0 : jumpsDone; 
        peakReached = false; 

        th_current_timeToMaxHeight = th1_timeToMaxHeight;
    }

    void reachPeak()
    {
        peakReached = true; 
        th_current_timeToMaxHeight = th2_timeToMinHeight;
        updateJumpValues();
    }


    Vector3 getForward()
    {
        return new Vector3(Mathf.Sin(yaw * Mathf.Deg2Rad), 0.0f, Mathf.Cos(yaw * Mathf.Deg2Rad));
    }
    
    Vector3 getRight()
    {
        return new Vector3(Mathf.Sin((yaw+90) * Mathf.Deg2Rad), 0.0f, Mathf.Cos((yaw+90) * Mathf.Deg2Rad));
    }

}
