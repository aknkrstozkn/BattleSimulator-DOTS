using UnityEngine;
 
/// <summary>
/// To visualize FPS at the corner of screen for debugging purposes
/// </summary>
public class FPSCounter : MonoBehaviour
{
	private float _deltaTime = 0.0f;
	private Color _color;
	private GUIStyle _style;
	private int _width, _height;
	private Rect _rect;
	
	private void Awake()
	{
		_color = Color.yellow;
		_width = Screen.width; 
		_height = Screen.height;
		
		_style = new GUIStyle();
		_rect = new Rect(0, 0, _width, _height * 2 / 100);
		
		_style.alignment = TextAnchor.UpperLeft;
		_style.fontSize = _height * 2 / 100;
		_style.normal.textColor = _color;
	}

	private void Start()
	{
		// Make the game run as fast as possible
		Application.targetFrameRate = 300;
	}
 
	private void Update()
	{
		_deltaTime += (Time.unscaledDeltaTime - _deltaTime) * 0.1f;
	}
 
	private void OnGUI()
	{
		float msec = _deltaTime * 1000.0f;
		float fps = 1.0f / _deltaTime;
		string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
		GUI.Label(_rect, text, _style);
	}
}