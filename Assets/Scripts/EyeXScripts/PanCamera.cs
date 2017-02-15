using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(GazeAwareComponent))]
public class PanCamera : MonoBehaviour {
    private GazePointDataComponent _gazePointDataComponent;
    private UserPresenceComponent _userPresenceComponent;
    private Camera _camera;

    [Range(0.1f, 1.0f)]
    public float alpha = 0.3f;
    private Vector2 _historicPoint;
    private bool _hasHistoricPoint;
    public float Speed = 10;
    // Use this for initialization
    void Start () {

        _gazePointDataComponent = GetComponent<GazePointDataComponent>();
        _userPresenceComponent = GetComponent<UserPresenceComponent>();
        _camera = Camera.main;

    }
	
	// Update is called once per frame
	void Update ()
    {
        var lastGazePoint = _gazePointDataComponent.LastGazePoint;

        if (_userPresenceComponent.IsValid && _userPresenceComponent.IsUserPresent && lastGazePoint.IsValid)
        {
            var gazePointInScreenSpace = lastGazePoint.Screen;
            var smoothedGazePoint = Smoothify(gazePointInScreenSpace);

            //Pan Left
            if (smoothedGazePoint.x <= GetComponent<RectTransform>().sizeDelta.x * 0.10f)
            {
                _camera.transform.RotateAround(_camera.transform.position, Vector3.up, -Speed * Time.deltaTime);
            }
            //Pan Right1
            else if (smoothedGazePoint.x >= GetComponent<RectTransform>().sizeDelta.x * .90f)
            {
                _camera.transform.RotateAround(_camera.transform.position, Vector3.up, Speed * Time.deltaTime);
            }
        }
        else
            _hasHistoricPoint = false;

    }

    private Vector2 Smoothify(Vector2 point)
    {
        if (!_hasHistoricPoint)
        {
            _historicPoint = point;
            _hasHistoricPoint = true;
        }

        var smoothedPoint = new Vector2(point.x * alpha + _historicPoint.x * (1.0f - alpha),
            point.y * alpha + _historicPoint.y * (1.0f - alpha));

        _historicPoint = smoothedPoint;

        return smoothedPoint;
    }
}
