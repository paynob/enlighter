using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PickableObject : MonoBehaviour
{

    Collider coll;
    PlayerController player;
    Quaternion rotation;

    void Awake()
    {
        coll = GetComponent<Collider>();
        coll.isTrigger = true;
        rotation = transform.rotation;
    }

    private void Update()
    {
        if ( player != null)
        {
            if( Input.GetButtonDown( "Jump" ) )
                Rotate();
            
            transform.position = player.transform.position;
            transform.rotation = rotation;

            if( Input.GetMouseButtonDown( 1 ) )
                Release();
        }
    }
    public void Rotate()
    {
        transform.Rotate( Vector3.up, 90, Space.World );
        rotation = transform.rotation;
    }

    public void Release()
    {
        if ( player != null )
            player = null;
    }

    private void OnTriggerEnter( Collider other )
    {
        if ( other.CompareTag("Player"))
        {
            player = other.GetComponent<PlayerController>();
        }
    }

    private void OnTriggerExit( Collider other )
    {
        if( other.CompareTag( "Player" ) )
        {
            player = null;
        }
    }
}
