﻿using System;
using TMPro;
using UnityEngine;
using Zenject;

public class CardView : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _numText;

	private static float _singleNumFontSize = 0.0f;
	
	private IGameStateService _gameStateService;
	private ICardGeneratorService _cardGeneratorService;

	private int _num;
	public int Num
	{
		get { return _num; }
		set
		{
			_num = value;
			_numText.text = _num.ToString();
		}
	}

	public int Index;

	[Inject]
	void Init(IGameStateService gameStateService, ICardGeneratorService cardGeneratorService)
	{
		_gameStateService = gameStateService;
		_cardGeneratorService = cardGeneratorService;
	}

	public void PlayAboveTo(CardView boardCard)
	{
		Index = boardCard.Index;
		_gameStateService.Board.Cards[boardCard.Index] = this;
				
		MoveTo(_gameStateService.Board.Slots[boardCard.Index].position, 
			() => Destroy(boardCard.gameObject));
	}

	public bool CanIPlayAbove(out CardView boardCardDestination)
	{
		for (int i = 0; i < _gameStateService.Board.Cards.Length; ++i)
		{
			if (IsACompatibleMove(Num, _gameStateService.Board.Cards[i].Num))
			{
				boardCardDestination = _gameStateService.Board.Cards[i];
				return true;
			}
		}
		boardCardDestination = null;
		return false;
	}

	private bool IsACompatibleMove(int originNum, int destinationNum)
	{
		return originNum == destinationNum + 1 
		       || originNum == destinationNum - 1
		       || originNum == _cardGeneratorService.GetMaxRange 
		       && destinationNum == _cardGeneratorService.GetMinRange
		       || originNum == _cardGeneratorService.GetMinRange 
		       && destinationNum == _cardGeneratorService.GetMaxRange;
	}
	
	private void MoveTo(Vector2 destination, Action onComplete)
	{
		transform.SetAsLastSibling();
		LeanTween.move(gameObject,
			destination, 0.2f).setOnComplete(() => onComplete?.Invoke());
	}
}
