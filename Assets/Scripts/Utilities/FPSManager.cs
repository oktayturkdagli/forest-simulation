using UnityEngine;

namespace Utilities
{
	public class FPSManager : Singleton<FPSManager>
	{
		private const float UpdateInterval = 0.5f; // How often should the number update
		private float _accumulator = 0.0f;
		private int _frames = 0;
		private float _timeLeft;
		private float _fps;
		private readonly GUIStyle _textStyle = new GUIStyle();
		
		[field: SerializeField] public float FrameRate { get; set; } = 144;

		private void Start()
		{
			_timeLeft = UpdateInterval;
			_textStyle.fontStyle = FontStyle.Bold;
			_textStyle.normal.textColor = Color.white;
		}

		private void Update()
		{
			CalculateFPS();
		}

		private void OnGUI()
		{
			DisplayFPS();
		}

		private void OnValidate()
		{
			SetFrameRate();
		}

		private void CalculateFPS()
		{
			_timeLeft -= Time.deltaTime;
			_accumulator += Time.timeScale / Time.deltaTime;
			++_frames;

			// Interval ended - update GUI text and start new interval
			if (_timeLeft <= 0.0)
			{
				// Display two fractional digits (f2 format)
				_fps = (_accumulator / _frames);
				_timeLeft = UpdateInterval;
				_accumulator = 0.0f;
				_frames = 0;
			}
		}

		private void DisplayFPS()
		{
			// Display the fps and round to 2 decimals
			GUI.Label(new Rect(5, 5, 100, 25), _fps.ToString("F2") + "FPS", _textStyle);
		}

		private void SetFrameRate()
		{
			Application.targetFrameRate = (int)FrameRate;
		}
	}
}