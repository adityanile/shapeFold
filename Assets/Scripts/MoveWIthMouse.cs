using UnityEngine;

public class MoveWithMouse : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;

    public Vector2 force = new Vector2(500, 500);
    private Rigidbody2D rb;

    private Transform hinge;
    public float angle = 10f;

    private void Start()
    {
        hinge = transform.GetChild(0);

        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            this.gameObject.transform.RotateAround(hinge.position, Vector3.forward, angle);
        }
        if (Input.GetMouseButton(1))
        {
            this.gameObject.transform.RotateAround(hinge.position, Vector3.forward, -angle);
        }
    }


    //void OnMouseDown()
    //{
    //    screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

    //    offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

    //    rb.AddForce(force * Camera.main.ScreenToWorldPoint(Input.mousePosition) * Time.deltaTime, ForceMode2D.Force);
    //}

    //void OnMouseDrag()
    //{
    //    Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
    //    Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;

    //    rb.AddForce(force * curPosition * Time.deltaTime, ForceMode2D.Force);
    //}


}
