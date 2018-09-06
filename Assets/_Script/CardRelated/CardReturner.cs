using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardReturner {
	private bool isPutBack,isShuffle;

	public CardReturner (bool isPutBack,bool isShuffle){
		this.isPutBack = isPutBack;
		this.isShuffle = isShuffle;
	}

	public void ReturnCards(CardPile cp,List<Card> cards){
		if (isPutBack){
			cp.AddCard (cards);
		}
		if (isShuffle) {
			cp.Shuffle ();
		}
	}
}
