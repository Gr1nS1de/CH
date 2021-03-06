﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class PlayerDataController : Controller
{
	private PlayerData 				_playerData	{ get { return core.playerData;}}

	public override void OnNotification( string alias, Object target, params object[] data )
	{
		switch ( alias )
		{
			case N.GameStart:
				{
					_playerData.currentScore = 0;
					break;
				}

		}

	}

	void Awake()
	{
		core.playerData = new PlayerData ()
		{	
			playerRecord = Prefs.PlayerData.GetRecord (),
			starsCount = Prefs.PlayerData.GetStarsCount (),
			isDoubleCoin = Prefs.PlayerData.GetDoubleCoin () == 1,
			playedGamesCount = Prefs.PlayerData.GetPlayedGamesCount(),
		};
	}

	/*
	#region public methods
	public void UpdatePlayerItemCount(ItemType itemType, int count, bool isNotify = true)
	{
		int absCount = Mathf.Abs (count);

		switch(itemType)
		{
			case ItemType.Coin:
				{
					if (count > 0)
						Prefs.PlayerData.CreditCoins (absCount);
					else
						Prefs.PlayerData.DebitCoins (absCount);

					_playerDataModel.coinsCount += count;
					break;
				}

			case ItemType.Crystal:
				{
					if (count > 0)
						Prefs.PlayerData.CreditCrystals (absCount);
					else
						Prefs.PlayerData.DebitCrystals (absCount);

					_playerDataModel.crystalsCount += count;
					break;
				}
					
		}

		if(isNotify)
			Notify (N.PlayerItemCountChange__, NotifyType.ALL, itemType, count);
	}

	public void OnPlayerBuySkin(int skinId)
	{
		int skinPrice = ui.view.GetPlayerSkinElement (skinId).SkinPrice;

		UpdatePlayerItemCount (ItemType.Coin, -skinPrice);
	}

	public void ActivateDoubleCoin()
	{
		Prefs.PlayerData.SetDoubleCoin ();
		_playerDataModel.isDoubleCoin = true;
	}
	#endregion

	private void OnClickDailyGiftElement(DailyGiftElementId elementId, int giftCoinsCount)
	{
		switch (elementId)
		{
			case DailyGiftElementId.GiftHour_00:
				{
					UpdatePlayerItemCount (ItemType.Coin, giftCoinsCount);
					break;
				}

			default:
				{
					UpdatePlayerItemCount (ItemType.Coin, giftCoinsCount);

					break;
				}

		}

	}

	private void OnAddScore()
	{
		_playerDataModel.currentScore++;

		if (_playerDataModel.currentScore > _playerDataModel.playerRecord)
		{
			OnNewRecord (_playerDataModel.currentScore);
		}

		Notify (N.GameAddScore);
	}

	private void OnNewRecord(int score)
	{
		Prefs.PlayerData.SetRecord (score);
		_playerDataModel.playerRecord = score;
		Notify (N.OnPlayerNewRecord_, NotifyType.ALL, score);
	}

	private void OnPlayerImpactItem(ItemView itemView)
	{
		ItemType itemType = itemView.ItemType;

		switch (itemType)
		{
			case ItemType.Coin:
				{
					int coinsCount = (_playerDataModel.isDoubleCoin ? 2 : 1);

					UpdatePlayerItemCount(ItemType.Coin, coinsCount, false);
					break;
				}

			case ItemType.Crystal:
				{
					int crystalsCount = itemView.CrystalFractureCount;

					UpdatePlayerItemCount (ItemType.Crystal, crystalsCount, false);
					break;
				}
		}
	}

	private void IncreasePlayedGamesCount()
	{
		int currentPlayedGamesCount = 1;

		if (!PlayerPrefs.HasKey (Prefs.PlayerData.GamesPlayedCount))
		{
			PlayerPrefs.SetInt (Prefs.PlayerData.GamesPlayedCount, currentPlayedGamesCount);
		}
		else
		{
			Prefs.PlayerData.IncreasePlayedGamesCount ();

			currentPlayedGamesCount = Prefs.PlayerData.GetPlayedGamesCount();
		}

		Prefs.PlayerData.IncreaseSkinPlayedGamesStatistics (game.model.playerModel.currentSkinId);

		_playerDataModel.playedGamesCount = currentPlayedGamesCount;
	}*/
		
}

