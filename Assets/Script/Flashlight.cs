using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Flashlight : NetworkBehaviour
{
    [SerializeField] GameObject FlashlightLight;

    [SyncVar]
    private bool FlashlightActive = false;

    Camera mainCam;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        FlashlightLight.gameObject.SetActive(false);
    }

    public override void OnStartLocalPlayer()
    {
        FlashlightLight.transform.SetParent(mainCam.transform);
    }

            // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer == true)
        {
            // Runs client command f flashlight
            Client();
        }
        else if (isLocalPlayer == false)
        {
            // Receives and updates the flashlight
            Notclient();
        }
    }
    
    void Client()
    {
        FlashlightLight.transform.SetParent(mainCam.transform);

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (FlashlightActive == false)
            {
                FlashlightLight.gameObject.SetActive(true);
                FlashlightActive = true;
                clienttoServer(true);
            }
            else
            {
                FlashlightLight.gameObject.SetActive(false);
                FlashlightActive = false;
                clienttoServer(false);
            }
        }
    }

    void Notclient()
    {
        if (FlashlightActive == false)
        {
            FlashlightLight.gameObject.SetActive(false);
        }
        else
        {
            FlashlightLight.gameObject.SetActive(true);
        }
    }

    [Command]
    void clienttoServer(bool flashlight) 
    {
        FlashlightActive = flashlight;
    }
}
