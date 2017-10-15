using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class MenuController : Controller
{
	/*
	#region Declare controllers reference
	public InGamePanelController		InGamePanelController			{ get { return _inGamePanelController 		= SearchLocal<InGamePanelController>(		_inGamePanelController,		typeof(InGamePanelController).Name);	} }
	public MainMenuPanelController		MainMenuPanelController			{ get { return _mainMenuPanelController		= SearchLocal<MainMenuPanelController>(		_mainMenuPanelController,	typeof(MainMenuPanelController).Name);	} }
	public CenterElementsController		CenterElementsController		{ get { return _centerElementsController	= SearchLocal<CenterElementsController>(	_centerElementsController,	typeof(CenterElementsController).Name);	} }
	public RightElementsController		RightElementsController			{ get { return _rightElementsController		= SearchLocal<RightElementsController>(		_rightElementsController,	typeof(RightElementsController).Name);	} }
	public RewardVideoController		RewardVideoController			{ get { return _rewardVideoController		= SearchLocal<RewardVideoController>(		_rewardVideoController,		typeof(RewardVideoController).Name);	} }
	public PlayerSkinWindowController	PlayerSkinWindowController		{ get { return _playerSkinWindowController	= SearchLocal<PlayerSkinWindowController>(	_playerSkinWindowController,typeof(PlayerSkinWindowController).Name);	} }

	private PlayerSkinWindowController	_playerSkinWindowController;
	private RewardVideoController		_rewardVideoController;
	private RightElementsController		_rightElementsController;
	private CenterElementsController	_centerElementsController;
	private InGamePanelController		_inGamePanelController;
	private MainMenuPanelController		_mainMenuPanelController;
	#endregion*/

	//private MainMenuPanelModel	mainMenuPanelModel	{ get { return ui.model.mainMenuPanelModel; } }
	//private InGamePanelModel 	inGamePanelModel	{ get { return ui.model.inGamePanelModel; } }

	public override void OnNotification (string alias, Object target, params object[] data)
	{
		switch (alias)
		{
			case N.GameStart:
				{
					break;
				}

			case N.GameOver_:
				{

					break;
				}
		}
	}

	void Start()
	{
	}

	public void OnGameOver(System.Action complete)
	{
		//UpdateText();

		//UIMenuModel.canvasGroup.gameObject.SetActive(true);
		/*
		UIGameModel.canvasGroupInGame.DOFade(0,0.2f).SetEase(Ease.Linear);

		UIMenuModel.canvasGroupStart.DOFade(1f,1).SetEase(Ease.Linear).OnComplete(() => {

			UIMenuModel.canvasGroupStart.alpha = 1;

			//DOTween.KillAll();

			if(complete!=null)
				complete();
		});*/
	}
		
}
