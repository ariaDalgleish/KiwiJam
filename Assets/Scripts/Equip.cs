using UnityEngine;

public class Equip : MonoBehaviour
{
    public GameObject[] vinyls; // Array of vinyl objects to be picked up
    public Transform holdPosition; // The position where the vinyl will be held
    public VinylPlayerController vinylPlayer; // Reference to the vinyl player

    private bool isVinylHeld = false; // Flag to track if a vinyl is currently held
    private GameObject heldVinyl; // Reference to the currently held vinyl

    void Start()
    {
        foreach (GameObject vinyl in vinyls)
        {
            Rigidbody vinylRigidbody = vinyl.GetComponent<Rigidbody>();
            vinylRigidbody.isKinematic = true;
        }
    }

    void Update()
    {
        // Check for pickup/drop input
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isVinylHeld)
            {
                TryPickUpVinyl();
            }
            else
            {
                TryPlaceOrDropVinyl();
            }
        }
    }

    void TryPickUpVinyl()
    {
        foreach (GameObject vinyl in vinyls)
        {
            if (Vector3.Distance(transform.position, vinyl.transform.position) < 2.0f)
            {
                PickupVinyl(vinyl);
                break;
            }
        }
    }

    void TryPlaceOrDropVinyl()
    {
        if (vinylPlayer.HasVinyl())
        {
            if (vinylPlayer.GetCurrentVinyl() == heldVinyl)
            {
                // Remove vinyl from player
                vinylPlayer.RemoveVinyl();
                DropVinyl();
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, vinylPlayer.transform.position) < 2.0f)
            {
                // Place vinyl on player
                vinylPlayer.PlaceVinyl(heldVinyl);
                isVinylHeld = false;
                heldVinyl = null;
            }
            else
            {
                // Drop vinyl normally
                DropVinyl();
            }
        }
    }

    void PickupVinyl(GameObject vinyl)
    {
        // Make vinyl kinematic to stop physics simulation
        Rigidbody vinylRigidbody = vinyl.GetComponent<Rigidbody>();
        vinylRigidbody.isKinematic = true;

        // Set vinyl's position and rotation to the holdPosition
        vinyl.transform.position = holdPosition.position;
        vinyl.transform.rotation = holdPosition.rotation;

        // Parent the vinyl to holdPosition
        vinyl.transform.SetParent(holdPosition);

        // Update state
        isVinylHeld = true;
        heldVinyl = vinyl;

        // Disable the collider to prevent unwanted collisions
        Collider vinylCollider = vinyl.GetComponent<Collider>();
        if (vinylCollider != null)
        {
            vinylCollider.enabled = false;
        }
    }

    void DropVinyl()
    {
        if (heldVinyl == null) return;

        // Unparent the vinyl from holdPosition
        heldVinyl.transform.SetParent(null);

        // Enable physics interactions
        Rigidbody vinylRigidbody = heldVinyl.GetComponent<Rigidbody>();
        vinylRigidbody.isKinematic = false;

        // Enable the collider to allow collisions
        Collider vinylCollider = heldVinyl.GetComponent<Collider>();
        if (vinylCollider != null)
        {
            vinylCollider.enabled = true;
        }

        // Update state
        isVinylHeld = false;
        heldVinyl = null;
    }
}
