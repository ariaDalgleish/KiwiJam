using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpVinyl : MonoBehaviour
{
    public GameObject PickUpText;
    public GameObject VinylOnPlayer;

      void Start()
      {
        VinylOnPlayer.SetActive(false);
        PickUpText.SetActive(false);
    }

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {

            PickUpText.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                this.gameObject.SetActive(false);
                VinylOnPlayer.SetActive(true);
                PickUpText.SetActive(false);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        PickUpText.SetActive(false);
    }
}
