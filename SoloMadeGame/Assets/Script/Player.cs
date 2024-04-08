using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    private Rigidbody rbody;
    [SerializeField] float speedZ;
    [SerializeField] GameObject camera;
    public bool revarse = false;
    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Transform cameraRotation = camera.transform;
        Vector3 worldAngle = cameraRotation.eulerAngles;

        if (Input.GetKey("w"))
        {
            rbody.AddForce(Vector3.forward * speedZ, ForceMode.Force);
        }
        if (Input.GetKey("a"))
        {
            if (revarse == false)
            {
                rbody.AddForce(Vector3.left * speedZ, ForceMode.Force);
            }
            if (revarse == true)
            {
                rbody.AddForce(Vector3.right * speedZ, ForceMode.Force);
            }
        }
        if (Input.GetKey("s"))
        {
            rbody.AddForce(Vector3.back * speedZ, ForceMode.Force);
        }
        if (Input.GetKey("d"))
        {
            if (revarse == false)
            {
                rbody.AddForce(Vector3.right * speedZ, ForceMode.Force);
            }
            if (revarse == true)
            {
                rbody.AddForce(Vector3.left * speedZ, ForceMode.Force);
            }
        }
        if (Input.GetKeyDown("space"))
        {
            revarse = true;
        }
        if (Input.GetKeyDown("c"))
        {
            revarse = false;
        }
        if (revarse == true)
        {
            Physics.gravity = new Vector3(0, 10.0F, 0);
            worldAngle.z = 180f;
        }
        if (revarse == false)
        {
            Physics.gravity = new Vector3(0, -10.0F, 0);
            worldAngle.z = 0f;
        }
        cameraRotation.eulerAngles = worldAngle; // âÒì]äpìxÇê›íË
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("revarse"))
        {
            if (revarse == true)
            {
                revarse = false;
            }
            if (revarse == false)
            {
                revarse = true;
            }
        }
    }
}
