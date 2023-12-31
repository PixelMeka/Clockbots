using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    //To be able to edit in the inspector but keeping it private
    [SerializeField] 
    float mouseSensitivity;

    float xAxisClamp = 0;

    [SerializeField]
    Transform player, playerArms;

    void Start()
    {
        // To keep the cursor locked
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        RotateCamera();
    }

    void RotateCamera()
    {
        //Mouse inputs
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        float rotAmountX = mouseX * mouseSensitivity;
        float rotAmountY = mouseY * mouseSensitivity;

        //Prevents the camera from going way too up or down
        xAxisClamp -= rotAmountY;  

        Vector3 rotPlayerArms = playerArms.transform.rotation.eulerAngles;
        Vector3 rotPlayer = player.transform.rotation.eulerAngles;

        //Moving camera and the player + arms at the same time
        rotPlayerArms.x -= rotAmountY;  
        rotPlayerArms.z = 0;
        rotPlayer.y += rotAmountX;

        //Prevents the camera from going way too up or down
        if (xAxisClamp > 90)     
        {
            xAxisClamp = 90;
            rotPlayerArms.x = 90;
        }
        else if(xAxisClamp < -90) 
        {
            xAxisClamp = -90;
            rotPlayerArms.x = 270;
        }

        playerArms.rotation = Quaternion.Euler(rotPlayerArms);
        player.rotation = Quaternion.Euler(rotPlayer);
    }
}
