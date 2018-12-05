using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Finite_State_Machine : MonoBehaviour {
	public Random r = new Random();
	public AI_State aiState;
	public int timer; 
	GameObject npc;
	public GameObject target;
	
	public enum AI_State {
        foodTable,
        bar,
        bulletin,
        dance,
        converse,
		eat,
		drink,
		onQuest
	}

	void updateState() {
		switch (aiState) {
			// things to do from foodTable
            case AI_State.foodTable:
				int s = Random.Range(0,7);
				switch (s) {
					case 0:
						aiState = AI_State.bar;
						break;
					case 1:
						aiState = AI_State.bulletin;
						break;
					case 2:
						aiState = AI_State.dance;
						break;
					case 3:
						aiState = AI_State.converse;
						break;
					case 4:
						aiState = AI_State.eat;
						break;
					case 5:
						aiState = AI_State.drink;
						break;
					case 6: 
						aiState = AI_State.onQuest;
						break;
				}
                break;
			// things to do from the bar
            case AI_State.bar:
				int s1 = Random.Range(0,7);
				switch(s1) {
					case 0:
						aiState = AI_State.foodTable;
						break;
					case 1:
						aiState = AI_State.bulletin;
						break;
					case 2:
						aiState = AI_State.dance;
						break;
					case 3:
						aiState = AI_State.converse;
						break;
					case 4:
						aiState = AI_State.eat;
						break;
					case 5:
						aiState = AI_State.drink;
						break;
					case 6: 
						aiState = AI_State.onQuest;
						break;		
				}
                break;
			// things to do from the bulletin
            case AI_State.bulletin:
				int s2 = Random.Range(0,4);
				switch(s2) {
					case 0:
						aiState = AI_State.foodTable;
						break;
					case 1:
						aiState = AI_State.bar;
						break;
					case 2:
						aiState = AI_State.converse;
						break;
					case 3:
						aiState = AI_State.onQuest;
						break;
				}
                break;
			// things to do from dancing
            case AI_State.dance:
				int s3 = Random.Range(0,4);
				switch(s3) {
					case 0:
						aiState = AI_State.foodTable;
						break;
					case 1:
						aiState = AI_State.bar;
						break;
					case 2:
						aiState = AI_State.bulletin;
						break;
					case 3:
						aiState = AI_State.converse;
						break;
				}
                break;
			// things to do from conversating
            case AI_State.converse:
				int s4 = Random.Range(0,4);
				switch(s4) {
					case 0:
						aiState = AI_State.foodTable;
						break;
					case 1:
						aiState = AI_State.bar;
						break;
					case 2:
						aiState = AI_State.bulletin;
						break;
					case 3:
						aiState = AI_State.dance;
						break;
				}
                break;
			// things to do from eating
			case AI_State.eat:
				int s5 = Random.Range(0,7);
				switch(s5) {
					case 0:
						aiState = AI_State.foodTable;
						break;
					case 1:
						aiState = AI_State.bar;
						break;
					case 2:
						aiState = AI_State.bulletin;
						break;
					case 3:
						aiState = AI_State.dance;
						break;
					case 4:
						aiState = AI_State.converse;
						break;
					case 5:
						aiState = AI_State.eat;
						break;
					case 6:
						aiState = AI_State.drink;
						break;
				}
				break;
			// things to do from drinking
			case AI_State.drink:
				int s6 = Random.Range(0,7);
				switch(s6) {
					case 0:
						aiState = AI_State.foodTable;
						break;
					case 1:
						aiState = AI_State.bar;
						break;
					case 2:
						aiState = AI_State.bulletin;
						break;
					case 3:
						aiState = AI_State.dance;
						break;
					case 4:
						aiState = AI_State.converse;
						break;
					case 5:
						aiState = AI_State.eat;
						break;
					case 6:
						aiState = AI_State.drink;
						break;
				}
				break;
			case AI_State.onQuest:
				
				break;
		}
	}

	// Use this for initialization
	void Start () {
		aiState = AI_State.bulletin;
		timer = 0;
		
	}
	
	// Update is called once per frame
	void Update () {
		// make an interval to call updateState in a certain amount of time
		timer += 1;
		if(timer%3000 == 0) {
			updateState();
			print(aiState);
		}

		switch (aiState) {
			case AI_State.foodTable:
				// go to the foodtable
				target = GameObject.Find("SM_Prop_Chair_04");
				this.transform.parent = target.transform;
				this.GetComponent<AICharacterControl>().target = target.transform;
				break;
			case AI_State.bar:
				// go to the bar and sit/stand
				int ss = Random.Range(0,2);
				target = GameObject.Find("SM_Prop_Bar_Counter_01");
				this.transform.parent = target.transform;
				this.GetComponent<AICharacterControl>().target = target.transform;
				if(ss == 0) {
					// walk to the bar stool
					//npc.transform.Translate();
					// set animation to sit on bar stool

				}else {
					//stand at bar
					//npc.transform.Translate();
				}
				break;
			case AI_State.bulletin:
				// go read the bulletin
				target = GameObject.Find("SM_Prop_Bookshelf_02");
				this.transform.parent = target.transform;
				this.GetComponent<AICharacterControl>().target = target.transform;
				break;
			case AI_State.dance:
				// go to the dance floor and dance
				target = GameObject.Find("SM_Prop_Rug_Small_02");
				this.transform.parent = target.transform;
				this.GetComponent<AICharacterControl>().target = target.transform;
				break;
			case AI_State.converse:
				// follow player and use dialogue to player
				// target = GameObject.FindWithTag();
				target = GameObject.Find("Player");
				this.transform.parent = target.transform;
				this.GetComponent<AICharacterControl>().target = target.transform;
				break;
			case AI_State.eat:
				// set animation to eat 
				break;
			case AI_State.drink:
				// set animation to drink
				break;
			case AI_State.onQuest:
				target = GameObject.Find("SM_Env_Door_01");
				this.transform.parent = target.transform;
				this.GetComponent<AICharacterControl>().target = target.transform;
				break;						
		}
	}
}
