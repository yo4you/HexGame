using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private float MoveSpeed;
    [SerializeField] private Vector2 ZoomRange;
    private Camera _camera;
    [SerializeField] private float _fovZoomout;
    private float _baseFov;
    [SerializeField] private float _scrollSpeed;

    private void Start()
    {
        _camera = Camera.main;
        _baseFov = _camera.orthographicSize;
    }

    void Update()
    {
        var scroll = Input.GetAxis("Mouse ScrollWheel");
        {
            _baseFov = Mathf.Clamp(_baseFov + (scroll * _scrollSpeed), ZoomRange.x, ZoomRange.y);
        }

        var input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        {
            var move_zoomout = Mathf.Clamp01(10f * input.magnitude);
            var target_fov = _baseFov + (move_zoomout * _fovZoomout) * _baseFov;
            _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, target_fov, .05f);
        }

        transform.position += Time.deltaTime * input * MoveSpeed;

    }
}
