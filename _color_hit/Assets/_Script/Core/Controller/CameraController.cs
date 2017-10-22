using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Camera))]
public class CameraController : Controller 
{
	private Camera _camera;

	public override void OnNotification (string alias, Object target, params object[] data)
	{
		switch (alias)
		{
			case N.SetStyle__:
				{
					StyleData styleData = (StyleData)data [0];
					bool isInit = (bool)data [1];

					SetCameraColor (styleData.styleColor);
					break;
				}
		}
	}

	void Start()
	{
		_camera = GetComponent<Camera> ();

		SetCameraColor (core.styleModel.GetCurrentStyleData().styleColor);
	}

	private void SetCameraColor(Color styleColor)
	{
		if (_camera != null)
		{
			_camera.DOColor (styleColor, 0.3f);
		}
	}
		
}
