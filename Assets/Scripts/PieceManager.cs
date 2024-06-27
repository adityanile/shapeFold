using System.Collections;
using UnityEngine;

public class PieceManager : MonoBehaviour
{
    // What will be the color of the painting after completion
    public static Color color = Color.white;

    public bool reached = false;
    private Rigidbody2D rb;

    private bool initailised;

    [SerializeField]
    private float soffset = 0.1f;
    private Vector2 checkPos;

    // For movement with the mouse
    [SerializeField]
    private Vector2 force = new Vector2(500, 500);
    private Vector3 screenPoint;
    private Vector3 offset;

    private void Start()
    {
        if (gameObject.CompareTag("Fixed"))
        {
            //If it's A fixed piece then,
            gameObject.GetComponent<SpriteRenderer>().color = color;
            
            var joint = gameObject.AddComponent<FixedJoint2D>();
            joint.enableCollision = true;
        }
        else
        {
            StartCoroutine(Init());
        }
    }
    IEnumerator Init()
    {
        initailised = false;

        yield return new WaitForSeconds(0.5f);

        rb = GetComponent<Rigidbody2D>();
        initailised = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!reached && initailised)
        {
            checkPos.x = (transform.position.x < 0) ? -transform.position.x : transform.position.x;
            checkPos.y = (transform.position.y < 0) ? -transform.position.y : transform.position.y;

            if (checkPos.x < soffset && checkPos.y < soffset)
            {
                PieceReached();
            }
        }
    }

    void PieceReached()
    {
        reached = true;

        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        gameObject.transform.position = new Vector3(0, 0, 0);
        gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);

        gameObject.GetComponent<SpriteRenderer>().color = color;
    }

    // For the movement of unfixed part only
    void OnMouseDown()
    {
        if (initailised)
        {
            screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

            rb.AddForce(force * Camera.main.ScreenToWorldPoint(Input.mousePosition) * Time.deltaTime, ForceMode2D.Force);
        }
    }

    void OnMouseDrag()
    {
        if (initailised)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;

            rb.AddForce(force * curPosition * Time.deltaTime, ForceMode2D.Force);
        }
    }
}
