using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float distance = 3;
    // макимальная дистанция, на которой мы можем взять мяч
    public float maxDistance = 5;
    public float scaler = 2000;
    public float angle = 40;
    Rigidbody rb;
    bool takeIt = false;
    public Vector3 force = new Vector3(0,1,0);
    Vector3 prevMousePosition = Vector3.zero;
    int goalresult = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "firstBasket" || other.name == "seckondBasket")
            goalresult++;

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "firstBasket" || other.name == "seckondBasket")
            goalresult++;
        if(goalresult == 4)
        {

        }
    }
    void OnMouseDown()
    {
        if (gameObject.transform.position.z < maxDistance)
        {
            Debug.Log($"Take It! in {Camera.main.ScreenToWorldPoint(Input.mousePosition)}");
            rb.isKinematic = true;
            takeIt = true;
        }
    }

    void OnMouseUp()
    {
        takeIt = false;
        if (gameObject.transform.position.z < maxDistance)
        {
            rb.isKinematic = false;
            rb.AddForce(Quaternion.Euler(angle, 0, 0) * force, ForceMode.Impulse);
            rb.AddTorque(Quaternion.Euler(angle, 0, 0) * Vector3.Normalize(force)/10, ForceMode.Impulse);
            Debug.Log($"Throw It! {Quaternion.Euler(angle, 0, 0) * force} ");
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void OnMouseDrag()
    {
        if (takeIt)
        {
            var mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
            gameObject.transform.position = Camera.main.ScreenToWorldPoint(mousePosition);

            force = (Input.mousePosition - prevMousePosition) / (Time.deltaTime* scaler);
            prevMousePosition = Input.mousePosition;
        }
    }
}
