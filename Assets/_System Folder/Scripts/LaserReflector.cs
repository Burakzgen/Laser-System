using Unity.Burst.CompilerServices;
using UnityEditor.PackageManager;
using UnityEngine;

public class LaserReflector : MonoBehaviour
{
    private Vector3 _startPosition;
    private Vector3 _direction;
    private LineRenderer _lineRenderer;
    bool _isOpen;
    GameObject tempReflector;
    private void Start()
    {
        _isOpen = false;
        _lineRenderer = gameObject.GetComponent<LineRenderer>();
    }
    private void Update()
    {
        if (_isOpen)
        {
            _lineRenderer.positionCount = 2;
            _lineRenderer.SetPosition(0, _startPosition);
            if (Physics.Raycast(_startPosition, _direction, out RaycastHit hitInfo, Mathf.Infinity))
            {
                if (hitInfo.collider.CompareTag("Reflactor"))
                {
                    tempReflector = hitInfo.collider.gameObject;
                    Vector3 temp = Vector3.Reflect(_direction, hitInfo.normal);
                    hitInfo.collider.gameObject.GetComponent<LaserReflector>().OpenRay(hitInfo.point, temp);
                }
                _lineRenderer.SetPosition(1, hitInfo.point);
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
        else
        {
            _lineRenderer.positionCount = 0;
        }
    }
    public void OpenRay(Vector3 pos, Vector3 dir)
    {
        _isOpen = true;
        _startPosition = pos;
        _direction = dir;
    }
    public void CloseRay()
    {
        _isOpen = false;
    }
}
