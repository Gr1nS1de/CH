using UnityEngine;
using System.Collections;

public class N : MonoBehaviour
{
	#region Core notifications
	public const string DragInput____				= "drag.input";
	public const string OnDoubleTapInput			= "on.double.tap.input";
	public const string SetStyle__					= "set.style";
	public const string StartLevel__ 				= "start.level";
	public const string FinishStep_ 					= "finish.step";
	public const string RetryLevel 					= "retry.level";

	#region Services notifications
	public const string OnGameServicesConnected_	= "on.game.services.connected";

	#region Purchase notifications
	public const string PurchaseProductsLoaded_		= "purchase.products.loaded";
	public const string PurchaseDoubleCoin			= "purchase.double.coin";
	public const string PurchaseCoinsPack_00		= "purchase.coins.pack.00";
	public const string PurchaseCoinsPack_01		= "purchase.coins.pack.01";
	public const string OnPurchasedDoubleCoin		= "on.purchased.double.coin";
	public const string OnPurchasedCoinsPack_00		= "on.purchased.coins.pack.00";
	public const string OnPurchasedCoinsPack_01		= "on.purchased.coins.pack.01";
	#endregion
	#endregion
	#endregion

	#region Game notifications
	public const string GameStart					= "game.start";
	public const string GamePause					= "game.pause";
	public const string GameContinue				= "game.continue";
	public const string GameOver					= "game.over";
	public const string GameAddScore				= "game.add.score";

	#region player notifications
	//public const string DestructibleBreakEntity___ 	= "destructible.break.entity";
	public const string ImpactObstacle___ 			= "impact.obstacle";
	public const string PlayerItemCountChange__		= "game.coins.change";
	public const string PlayerNewRecord_			= "on.player.new.record";
	#endregion
	#endregion

	#region Menu notifications
	public const string SelectStyle_				= "select.style";
	public const string SelectLevel__ 				= "select.level";

	public const string UIThemeChanged_				= "ui.theme.changed";
	public const string UIWindowStateChanged_		= "ui.window.state.changed";
	public const string OnCenterButtonPressed_ 		= "on.center.button.pressed";
	public const string OnRightButtonPressed_ 		= "on.right.button.pressed";
	public const string UIShowRewardVideoAd			= "ui.show.reward.video.ad";
	public const string OnStartShowAdVideo			= "on.start.show.ad.video";
	public const string OnEndShowAdVideo_			= "on.end.show.ad.video";
	public const string OnPlayerGetDailyGift__		= "on.player.get.daily.gift";
	public const string OnPlayerBuySkin_			= "on.player.buy.skin";
	public const string OnPlayerSelectSkin___		= "on.player.select.skin";
	public const string OnDailyGiftAvailable_		= "on.daily.gift.available";
	#endregion

}
