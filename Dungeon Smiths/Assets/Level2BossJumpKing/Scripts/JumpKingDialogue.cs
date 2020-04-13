using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpKingDialogue : MonoBehaviour
{
    public GameObject player;
    private GameObject text1;
    private GameObject text2;
    private GameObject text3;
    private bool firstTime1 = true;
    private bool firstTime2 = true;
    private bool firstTime3 = true;
    private bool fallBaitLine = false;
    private int change = 2;


    // Start is called before the first frame update
    void Start()
    {
        text1 = transform.GetChild(0).gameObject;
        text2 = transform.GetChild(1).gameObject;
        text3 = transform.GetChild(2).gameObject;
    }

    // Update is called once per frame
    void Update()
    {

        if (player.transform.position.y < 11)
        {
            if (change == 2)
            {
                change = 1;
                text1.SetActive(true);
                text2.SetActive(false);
                text3.SetActive(false);
                if (firstTime1)
                {
                    firstTime1 = false;
                    text1.GetComponent<Text>().text = "Hmm.. someone interrupting me in my dungeon. How rude, perhaps you can use some manners.";
                }
                else
                {
                    text1.GetComponent<Text>().text = randomText();
                }
            }
        }
        else if ((player.transform.position.y >= 11) && (player.transform.position.y < 32.5))
        {
            if (change == 1 || change == 3)
            {
                change = 2;
                text1.SetActive(false);
                text2.SetActive(true);
                text3.SetActive(false);
                if (firstTime2)
                {
                    firstTime2 = false;
                    text2.GetComponent<Text>().text = "Bet you can't catch what I say right now because you're too busy falling.";
                }
                else if (fallBaitLine)
                {
                    text2.GetComponent<Text>().text = "Fool me once, shame on you, fool me twice, you can't get fooled again.";
                    fallBaitLine = false;
                }
                else
                {
                    text2.GetComponent<Text>().text = randomText();
                }
            }
        }
        else if (player.transform.position.y >= 32.5)
        {
            if (change == 2)
            {
                change = 3;
                text1.SetActive(false);
                text2.SetActive(false);
                text3.SetActive(true);
                if (firstTime3)
                {
                    firstTime3 = false;
                    fallBaitLine = true;
                    text3.GetComponent<Text>().text = "Jump up to me, I need some company on this rock here.";
                }
                else
                {
                    text3.GetComponent<Text>().text = randomText();
                }
            }
        }
    }

    private string randomText()
    {
        string[] dialogue = new string[10] {"I will find the artifact before any- wait what artifact? I didn't say a word.",
            "I pity you, poor young one having to jump when I can just teleport with the snap of my fingers.",
            "They see me falling, they hating, patrolling trying to catch me falling down.",
            "Break a leg up there, you can interpret it however you would like to. Trust me I'm a nice guy.",
            "Did you know that more than 95% of hip fractures are caused by falling?",
            "Ah handsome, maybe we should go on a date, after all, you keep falling for me.",
            "Teleportation, what a wonderful tool. It's an ancient technique taught by my ancestors.",
            "I am everywhere and nowhere at once, perhaps we will meet again if you find a way out...",
            "The path to the journey is more important than the destination. But the destination may not exist.",
            "Falling is a state of being, perhaps you should get used to it, since jumping isn't your thing."};
        float random = Random.Range(0.0f, 10.0f);
        return dialogue[(int)random];
    }

}
