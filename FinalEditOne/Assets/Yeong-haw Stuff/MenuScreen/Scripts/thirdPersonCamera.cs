using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thirdPersonCamera : MonoBehaviour
{

    public Transform target;

    [System.Serializable]
    public class PositionSettings
    {
        //offset on where it looks at target
        public Vector3 targetPostOffset = new Vector3(0, 0, 0);

        public float lookSmooth = 120f;
        public float distanceFromTarget = -9;
        public float zoomSmooth = 120;
        public float maxZoom = -2;
        public float minZoom = -15;
        public bool smoothFollow = true; //new 
        public float smooth = .50f; //new

        [HideInInspector]
        public float newDistance = -9;
        [HideInInspector] //new
        public float adjustmentDistance = -9;
    }

    [System.Serializable]
    public class OrbitSettings
    {
        //input sent to the computer
        public float xRotation = -15f;
        public float yRotation = -180f;

        //how above or below we can see the player
        public float maxXRotation = 0f;
        public float minXRotation = -85f;

        //rotation on the axies left and right
        public float vOrbitSmooth = 200;
        public float hOrbitSmooth = 200;
    }

    [System.Serializable]
    public class InputSettings
    {
        //custom input for the usering in the game allow to look around the player. Edit > Project setting > input.
        public string ORBIT_HORIZONTAL_SNAP = "OrbitHorizontalSnap";
        public string ORBIT_HORIZONTAL = "OrbitHorizontal";
        public string ORBIT_VERTICAL = "OrbitVertical";
        public string ZOOM = "Mouse ScrollWheel";
    }

    [System.Serializable] //new
    public class DebugSettings
    {
        public bool drawDesiredCollisionLines = true;
        public bool drawAdjustedCollisionLines = true;
    }

    public PositionSettings position = new PositionSettings();
    public OrbitSettings orbit = new OrbitSettings();
    public InputSettings input = new InputSettings();
    public DebugSettings debug = new DebugSettings();  //new
    public CollisionHandler collision = new CollisionHandler(); //new

    Vector3 targetPos = Vector3.zero;
    Vector3 destination = Vector3.zero;
    Vector3 adjustedDestination = Vector3.zero;  //new
    Vector3 camVel = Vector3.zero;  //new

    float vOrbitInput, hOrbitInput, zoomInput, hOrbitSnapInput;




    void Start()
    {
        SetCameraTarget(target);
        MoveToTarget();
         collision.Initialize(Camera.main);
         collision.UpdateCameraClipPoints(transform.position, transform.rotation, ref collision.adjustedCameraClipPoints);
         collision.UpdateCameraClipPoints(destination, transform.rotation, ref collision.desiredCameraClipPoints);

        //camera mouse?????
    }

    public void SetCameraTarget(Transform t)
    {
        target = t;

        if (target == null)
        {
            Debug.LogError("Camera has no target to look at!");
        }

    }

    void getInput()
    {
        vOrbitInput = Input.GetAxisRaw(input.ORBIT_VERTICAL);
        hOrbitInput = Input.GetAxisRaw(input.ORBIT_HORIZONTAL);
        hOrbitSnapInput = Input.GetAxisRaw(input.ORBIT_HORIZONTAL_SNAP);
        zoomInput = Input.GetAxisRaw(input.ZOOM);
    }

    void FixedUpdate()
    {
        collision.UpdateCameraClipPoints(transform.position, transform.rotation, ref collision.adjustedCameraClipPoints);
        collision.UpdateCameraClipPoints(destination, transform.rotation, ref collision.desiredCameraClipPoints);

        //draw debug lines
        for (int i = 0; i < 5; i++)
        {
            if (debug.drawDesiredCollisionLines)
            {
                Debug.DrawLine(targetPos, collision.desiredCameraClipPoints[i], Color.white);
            }
            if (debug.drawAdjustedCollisionLines)
            {
                Debug.DrawLine(targetPos, collision.desiredCameraClipPoints[i], Color.green);
            }
        }

        collision.CheckColliding(targetPos);
        position.adjustmentDistance = collision.GetAdjustedDistanceWithRayFrom(targetPos);

    }


    void LateUpdate()
    {
        //moving
        MoveToTarget();
        //rotating
        LookAtTarget();


        ///this changed
        getInput();
        OrbitTarget();
        ZoomInOnTarget();
        ///
    }

    void MoveToTarget()
    {
        targetPos = target.position + position.targetPostOffset;
        destination = Quaternion.Euler(orbit.xRotation, orbit.yRotation + target.eulerAngles.y, 0) * -Vector3.forward * position.distanceFromTarget;
        destination += targetPos;

          if(collision.colliding){
              adjustedDestination = Quaternion.Euler(orbit.xRotation, orbit.yRotation + target.eulerAngles.y, 0) * Vector3.forward * position.adjustmentDistance;
              adjustedDestination += targetPos;

              if(position.smoothFollow){
                  //use smooth
                  transform.position = Vector3.SmoothDamp(transform.position, adjustedDestination, ref camVel, position.smooth);
              }
              else{
                  transform.position = adjustedDestination;
              }
          }
          else
          {
            if (position.smoothFollow){
                transform.position = Vector3.SmoothDamp(transform.position, destination, ref camVel, position.smooth);
            }
            else{
                transform.position = destination;
            }

        }
    }

    void LookAtTarget()
    {
        Quaternion targetRotation = Quaternion.LookRotation(targetPos - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, position.lookSmooth * Time.deltaTime);
    }

    void OrbitTarget()
    {
        if (hOrbitSnapInput > 0)
        {
            orbit.yRotation = -180;
            orbit.xRotation = -15f;
        }

        orbit.xRotation += -vOrbitInput * orbit.vOrbitSmooth * Time.deltaTime;
        orbit.yRotation += -hOrbitInput * orbit.hOrbitSmooth * Time.deltaTime;

        if (orbit.xRotation > orbit.maxXRotation)
        {
            orbit.xRotation = orbit.maxXRotation;
        }
        if (orbit.xRotation < orbit.minXRotation)
        {
            orbit.xRotation = orbit.minXRotation;
        }
    }

    void ZoomInOnTarget()
    {
        position.distanceFromTarget += zoomInput * position.zoomSmooth * Time.deltaTime;

        if (position.distanceFromTarget > position.maxZoom)
        {
            position.distanceFromTarget = position.maxZoom;
        }
        if (position.distanceFromTarget < position.maxZoom)
        {
            position.distanceFromTarget = position.minZoom;
        }
    }

    
    [System.Serializable]
    public class CollisionHandler
    {
        public LayerMask collisionLayer = ~0;

        [HideInInspector]
        public bool colliding = false;
        [HideInInspector]
        public Vector3[] adjustedCameraClipPoints;
        [HideInInspector]
        public Vector3[] desiredCameraClipPoints;

        Camera camera; 

        public void Initialize(Camera cam)
        {
            camera = cam;
            adjustedCameraClipPoints = new Vector3[5];
            desiredCameraClipPoints = new Vector3[5];
        }

        public void UpdateCameraClipPoints(Vector3 cameraPosition, Quaternion atRotation, ref Vector3[] intoArray)
        {
            if (!camera)
                return;

            // clear the contents of intoArray
            intoArray = new Vector3[5];

            float z = camera.nearClipPlane;
            float x = Mathf.Tan(camera.fieldOfView / 2f) * z;
            float y = x / camera.aspect;

            //top left (-x y z)
            intoArray[0] = (atRotation * new Vector3(-x, y, z)) + cameraPosition; // added and rotated the point relative to camera

            //top right (x y z)
            intoArray[1] = (atRotation * new Vector3(x, y, z)) + cameraPosition;

            //bottom left (-x -y z)
            intoArray[2] = (atRotation * new Vector3(-x, -y, z)) + cameraPosition;
            
            //bottom right (x -y z))
            intoArray[3] = (atRotation * new Vector3(x, -y, z)) + cameraPosition;

            //camera's position
            intoArray[4] = cameraPosition - camera.transform.forward;
        }

        bool CollisionDetectedAtClipPoints(Vector3[] clipPoints, Vector3 fromPosition)
        {
            for(int i =0; i < clipPoints.Length; i++)
            {
                Ray ray = new Ray(fromPosition, clipPoints[i] - fromPosition);
                float distance = Vector3.Distance(clipPoints[i], fromPosition);

                if(Physics.Raycast(ray, distance, collisionLayer))
                {
                    return true;
                }
            }

            return false;
        }


        public float GetAdjustedDistanceWithRayFrom(Vector3 from)
        {
            float distance = -1;

            for(int i = 0; i < desiredCameraClipPoints.Length; i++)
            {
                Ray ray = new Ray(from, desiredCameraClipPoints[i] - from);
                RaycastHit hit;

                if(Physics.Raycast(ray, out hit))
                {
                    if(distance == -1)
                    {
                        distance = hit.distance;
                    }
                    else
                    {
                        if (hit.distance < distance)
                            distance = hit.distance;
                    }
                }
            }

            if (distance == -1)
                return 0;
            else
                return distance;
        }

        public void CheckColliding(Vector3 targetPosition)
        {
            if(CollisionDetectedAtClipPoints(desiredCameraClipPoints,targetPosition))
            {
                colliding = true;
            }
            else
            {
                colliding = false;
            }
        }
    }

}