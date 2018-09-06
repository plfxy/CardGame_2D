using System;
using System.Collections;
using System.Collections.Generic;

//for sort
public struct SortUnit: IComparable<SortUnit>{
	public int index;
	public int priority;

	public SortUnit(int index,int priority){
		this.index = index;
		this.priority = priority;
	}

	public int CompareTo(SortUnit s){
		if (this.priority > s.priority) {
			return 1;
		} else if(this.priority==s.priority){
			return 0;
		}else{
			return -1;
		}
	}
}

public class CardPile {
	private List<Card> Cards {
		get{
			return _Cards;
		}
	}
	private List<Card> _Cards = new List<Card>();

	public int CardNumber { get; set; }

	public Card DrawCard(int index){
		Card card = Cards[index];
		Cards.RemoveAt (index);
		return card;
		//	a.Current
	}

	public void AddCard(Card card){
		Cards.Add (card);
	}

	public void AddCard(List<Card> cards){
		int i;
		for (i = 0; i < cards.Count; i++) {
			this.AddCard (cards [i]);
		}
	}

	public void Shuffle(){
		int i;
		Card[] c;
		List<SortUnit> Index=new List<SortUnit>();
		for (i=0; i < Cards.Count; i++) {
			Index.Add (new SortUnit (i, RandomNumberCreator.GetAInt (10000)));
		}
		Index.Sort ();
		c = new Card[Cards.Count];
		for (i = 0; i < c.Length; i++) {
			c [i] = Cards [Index [i].index];
		}
		for (i = 0; i < c.Length; i++) {
			Cards [i] = c [i];
		}
	}
	//private List<>
}
