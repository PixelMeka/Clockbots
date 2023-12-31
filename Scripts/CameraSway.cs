using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSway : MonoBehaviour
{
    //For camera sway:
    public float swayStrength;  //How far will the camera sway
    public float swaySpeed;     //How fast will the camera sway
    Vector3 initialPos;

    //For camera shake:
    public GameObject player;
    bool damaged;
    bool shake = false;
    bool invokeOnce = false;

    float max = 20f;
    float min = 0f;
    float curAngle = 0;
    int speed1 = 90;
    int speed2 = 80;

    // Start is called before the first frame update
    void Start()
    {
        //Local position - to transform the child object's location
        //Camera will start here and come back here
        initialPos = transform.localPosition;  //For camera sway.
    }

    // Update is called once per frame
    void Update()
    {
        //For camera shake:
        bool damaged = player.GetComponent<PlayerStats>().damaged;
        if(damaged && !shake)
        {
            curAngle += speed1 * Time.deltaTime; //Slowly increases the angle value according to the time.
            curAngle = Mathf.Clamp(curAngle, min, max); //Clamps the angle value between the min and max value.
            this.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, curAngle); //Rotates the camera.
            if(curAngle >= max)
            {
                shake = true;
            }
        }
        if(shake && transform.rotation.eulerAngles.z != 0)
        {
            curAngle -= speed2 * Time.deltaTime; //Slowly decreases the angle value according to the time.
            curAngle = Mathf.Clamp(curAngle, min, max);
            this.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, curAngle);
            if (curAngle <= min)
            {
                if(!invokeOnce)
                {
                    Invoke("Shake_Pause", 0.5f); //Initiates a pause.
                    invokeOnce = true;
                }
            }
        }
        else if(!shake && !damaged && transform.rotation.eulerAngles.z != 0) //This fixes a problem related to the camera's Z-axis not rotating back to 0.
        {
            curAngle -= speed2 * Time.deltaTime;
            curAngle = Mathf.Clamp(curAngle, min, max);
            this.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, curAngle);
        }

        //For camera sway:
        float mouseX = -Input.GetAxis("Mouse X") * swayStrength;
        float mouseY = -Input.GetAxis("Mouse Y") * swayStrength;

        Vector3 finalPos = new Vector3(mouseX, mouseY, 0);
        //Lerp - moves from one position to another position based on time value (initialPos --> finalPos)
        transform.localPosition = Vector3.Lerp(transform.localPosition, finalPos + initialPos, Time.deltaTime * swaySpeed);
    }

    //To prevent the camera from constantly shaking.
    void Shake_Pause()
    {
        shake = false;
        invokeOnce = false;
    }
}
