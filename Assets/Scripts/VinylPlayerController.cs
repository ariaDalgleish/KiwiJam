using UnityEngine;

public class VinylPlayerController : MonoBehaviour
{
    public AudioSource audioSource; // The audio source component
    private GameObject currentVinyl; // The currently placed vinyl

    public void PlaceVinyl(GameObject vinyl)
    {
        if (currentVinyl != null)
        {
            Debug.LogError("Vinyl Player already has a vinyl placed!");
            return;
        }

        currentVinyl = vinyl;
        currentVinyl.transform.position = transform.position;
        currentVinyl.transform.rotation = transform.rotation;
        currentVinyl.transform.SetParent(transform);
        currentVinyl.GetComponent<Rigidbody>().isKinematic = true;
        currentVinyl.GetComponent<Collider>().enabled = false;

        // Play audio and start animation
        Vinyl vinylScript = currentVinyl.GetComponent<Vinyl>();
        audioSource.clip = vinylScript.audioClip;
        audioSource.Play();
        vinylScript.PlayAnimation();
    }

    public void RemoveVinyl()
    {
        if (currentVinyl == null)
        {
            Debug.LogError("No vinyl to remove!");
            return;
        }

        // Stop audio and animation
        audioSource.Stop();
        currentVinyl.GetComponent<Vinyl>().StopAnimation();

        // Reset vinyl state
        currentVinyl.GetComponent<Rigidbody>().isKinematic = false;
        currentVinyl.GetComponent<Collider>().enabled = true;
        currentVinyl.transform.SetParent(null);
        currentVinyl = null;
    }

    public bool HasVinyl()
    {
        return currentVinyl != null;
    }

    public GameObject GetCurrentVinyl()
    {
        return currentVinyl;
    }
}
