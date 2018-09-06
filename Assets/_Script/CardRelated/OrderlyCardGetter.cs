using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderlyCardGetter : CardGetter {
	public override List<Card> GetCard (CardPile cardPile, int num){
		int i;
		List<Card> SelectedCards= new List<Card>();
		for (i = 1; i <= num; i++) {
			SelectedCards.Add (cardPile.DrawCard(0));
		}
		return SelectedCards;
	}
}
