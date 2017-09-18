using UnityEngine;
using System.Collections;

public abstract class MenuApplication<M, /*V,*/ C> : MenuApplication
	where M : Element
	//where V : Element
	where C : Element
{
	public M model			{ get { return (M)(object)base.model; } }
	//public V view			{ get { return (V)(object)base.view; } }
	public C controller		{ get { return (C)(object)base.controller; } }
}

public class MenuApplication : BaseApplication
{
	public MenuModel			model				{ get { return _model 			= SearchLocal<MenuModel>(_model, typeof(MenuModel).ToString()); } }
	//public MenuView			view				{ get { return _view			= SearchLocal<MenuView>(_view, typeof(MenuView).ToString()); } }
	public MenuController		controller			{ get { return _controller		= SearchLocal<MenuController>(_controller, typeof(MenuController).ToString()); } }

	private MenuModel			_model;
	//private MenuView        	_view;
	private MenuController  	_controller;

	private void Awake()
	{

	}

	private void Start()
	{

	}
}

