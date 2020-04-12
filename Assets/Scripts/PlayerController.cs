using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Layers which mouse clic will take in count to move the player")]
    private LayerMask mask;

    // NavMeshAgent component attached to this gameObject
    NavMeshAgent agent;
    // Main Camera of the scene
    Camera cam;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        cam = Camera.main;
    }

    private void Update()
    {
        // On mouseclic
        if ( Input.GetMouseButtonDown( 0 ) )
        {
            Vector3 mousePosition = Input.mousePosition;
            
            Vector3 targetPosition = cam.ScreenToWorldPoint( mousePosition );

            // If a Ray from the position in the direction of the camera normal hits a collider that has apropiate layer
            if ( Physics.Raycast( targetPosition, cam.transform.forward, out RaycastHit hit, 100f,  mask ) )
            {
                targetPosition = hit.point;
                // Update the navmesh target destination
                UpdateTargetPosition( targetPosition );
            }

        }
    }

    // Update NavMeshAgent target destination
    private void UpdateTargetPosition( Vector3 targetPosition )
    {
        agent.SetDestination( targetPosition );
    }
}
