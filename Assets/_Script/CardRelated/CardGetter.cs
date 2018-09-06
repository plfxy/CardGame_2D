using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardGetter {
	public abstract List<Card> GetCard (CardPile cardPile, int num); 
}
