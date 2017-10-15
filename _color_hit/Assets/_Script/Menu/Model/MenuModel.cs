using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public enum UIPanelState
{
	MainMenu,
	InGame
}

public enum UIWindowState
{
	MainMenu,
	Pause,
	GameOver,
	Store,
	Settings,
	PlayerSkin,
	Like,
	DailyGift,
	Achievements,
	GS_Achievements,
	GS_Leaderboard
}

[System.Serializable]
public struct UITheme
{
	public StyleId StyleId;
	public Color IconsColor;
	public Color BackgroundColor;
	public Sprite PlayButtonBGSprite;
	public Sprite CenterButtonsBGSprite;
	public Sprite RightButtonsBGSPrite;
	public Sprite CenterButtonHalfBGSPrite;
}

public class MenuModel : Model
{

	#region UI Model
	public UIPanelState			uiMainState				{ get { return _uiPanelState; } 	set { _uiPanelState 	= value;} }
	public UITheme		 		menuTheme				{ get { return _menuTheme; } 		set { _menuTheme 		= value;} }
	public UIWindowState		uiWindowState			{ get { return _uiWindowState; } 	set { _uiWindowState 	= value; } }

	[SerializeField]
	private UIWindowState		_uiWindowState 			= UIWindowState.MainMenu;
	[SerializeField]
	private UITheme				_menuTheme;
	[SerializeField]
	private UIPanelState		_uiPanelState			= UIPanelState.MainMenu;
	#endregion

	/*
	#region Declare models reference
	public InGamePanelModel			inGamePanelModel			{ get { return _inGamePanelModel 		= SearchLocal<InGamePanelModel>(		_inGamePanelModel,		typeof(InGamePanelModel).Name);	} }
	public MainMenuPanelModel		mainMenuPanelModel			{ get { return _mainMenuPanelModel		= SearchLocal<MainMenuPanelModel>(		_mainMenuPanelModel,	typeof(MainMenuPanelModel).Name);	} }
	public NoticeModel				noticeModel					{ get { return _noticeModel				= SearchLocal<NoticeModel>(				_noticeModel,			typeof(NoticeModel).Name);	} }

	private NoticeModel				_noticeModel;
	private MainMenuPanelModel		_mainMenuPanelModel;
	private InGamePanelModel		_inGamePanelModel;
	#endregion
	*/
}

