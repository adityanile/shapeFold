using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithMouse : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;

    public Vector2 force = new Vector2(500, 500);
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
       
        rb.AddForce(force * Camera.main.ScreenToWorldPoint(Input.mousePosition) * Time.deltaTime, ForceMode2D.Force);
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;

        rb.AddForce(force * curPosition * Time.deltaTime, ForceMode2D.Force);
    }


}
