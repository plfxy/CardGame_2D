using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDrawer {
	private CardGetter cardGetter;
	private CardReturner cardReturner;
	private bool isPutBack; 
	//private

	public CardDrawer(CardGetter cardGetter,CardReturner cardRetuener){
		this.cardGetter = cardGetter;
		this.cardReturner = cardRetuener;
	}

	public List<Card> DrawCard (CardPile cardPile, int num){
		List<Card> selectedCard;
		selectedCard = cardGetter.GetCard (cardPile, num);
		cardReturner.ReturnCards (cardPile,selectedCard);
		return selectedCard;
	}
}
