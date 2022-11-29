using UnityEngine;

public class LaserSource : MonoBehaviour
{
    [SerializeField] Transform laserStartPoint;

    private Vector3 _direction;
    private LineRenderer _lineRenderer;
    GameObject tempReflector;
    private void Start()
    {
        _lineRenderer = gameObject.GetComponent<LineRenderer>();
        _lineRenderer.enabled = false;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        { _lineRenderer.enabled = true; }
        if (Input.GetKeyDown(KeyCode.E))
        { _lineRenderer.enabled = false; }

        float h = 100 * Time.deltaTime * Input.GetAxis("Mouse X");
        gameObject.transform.Rotate(0, h, 0);

        if (_lineRenderer.enabled)
        {
            _direction = laserStartPoint.forward;
            _lineRenderer.positionCount = 2;
            _lineRenderer.SetPosition(0, laserStartPoint.position);


            if (Physics.Raycast(laserStartPoint.position, _direction, out RaycastHit hit, Mathf.Infinity))
            {
                if (hit.transform.CompareTag("Reflactor"))
                {
                    tempReflector = hit.collider.gameObject;
                    Vector3 temp = Vector3.Reflect(_direction, hit.normal);
                    hit.collider.gameObject.GetComponent<LaserReflector>().OpenRay(hit.point, temp);
                }
                _lineRenderer.SetPosition(1, hit.point);
            }
            else
            {
                if (tempReflector)
                {
                    tempReflector.GetComponent<LaserReflector>().CloseRay();
                }
                _lineRenderer.SetPosition(1, _direction * 100);
            }
        }
    }


}
