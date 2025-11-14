using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{
    InputManager inputManager;
    
    
    public Transform targetTransform;
    public Transform cameraPivot;
    public Transform cameraTransform;
    public LayerMask collisionLayers;
    private float defaultPosition;
    private float defaultPositionY;
    private Vector3 cameraFollowVel = Vector3.zero;
    private Vector3 cameraVectorPos;

    public float cameraCollisionOffSet = 0.2f;
    public float minCollisionOffSet = 0.2f;
    public float cameraCollisionRadius = 0.2f;
    public float cameraFollowSpeed = 0.2f;
    public float cameraLookSpeed = 0.2f;
    public float cameraPivotSpeed  = 0.2f;

    public float lookAngle; // Look up & down
    public float pivotAngle; // Look left & right
    private float minimumPivotAngle = -25;
    private float maximumPivotAngle =  25;

    private void Awake()
    {
        inputManager = FindObjectOfType<InputManager>();
        targetTransform = FindObjectOfType<PlayerManager>().transform;
        cameraTransform = Camera.main.transform;
        defaultPosition = cameraTransform.localPosition.z;
        defaultPositionY = cameraTransform.localPosition.y;
        
        
    }

    public void HandleAllCameraMovment()
    {
        FollowPlayer();
        RotateCamera();
        HandleCameraCollision();
    }
    
    private void FollowPlayer()
    {
        Vector3 targetPosition = Vector3.SmoothDamp
            (transform.position, targetTransform.position, ref cameraFollowVel, cameraFollowSpeed);
        
        transform.position = targetPosition;
    }

    private void RotateCamera()
    {
        Vector3 rotation;
        Quaternion targetRotation;
        
        lookAngle = lookAngle + (inputManager.cameraInputX * cameraLookSpeed);
        pivotAngle = pivotAngle - (inputManager.cameraInputY * cameraPivotSpeed);
        pivotAngle = Mathf.Clamp(pivotAngle, minimumPivotAngle, maximumPivotAngle);
        
        rotation = Vector3.zero;
        rotation.y = lookAngle;
        
        targetRotation = Quaternion.Euler(rotation);
        transform.rotation = targetRotation;
        
        rotation = Vector3.zero;
        rotation.x = pivotAngle;
        targetRotation = Quaternion.Euler(rotation);
        cameraPivot.localRotation = targetRotation;
        
    }
    
    private void HandleCameraCollision()
    {
        float targetPosition = defaultPosition;
        RaycastHit hit;
        Vector3 direction = cameraTransform.position - cameraPivot.position;
        direction.Normalize();

        if (Physics.SphereCast
            (cameraPivot.transform.position, cameraCollisionRadius, direction, out hit, Mathf.Abs(targetPosition),
                collisionLayers))
        {
            float distance = Vector3.Distance(cameraPivot.position, hit.point);
            targetPosition = Mathf.Clamp(-(distance - cameraCollisionOffSet), -Mathf.Abs(defaultPosition), -minCollisionOffSet);
        }

        if (Mathf.Abs(targetPosition) < minCollisionOffSet)
        {
            targetPosition =- minCollisionOffSet;
        }
        cameraVectorPos.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPosition, 0.2f);
        cameraVectorPos.y = defaultPositionY;
        cameraTransform.localPosition = cameraVectorPos;
    }
}
