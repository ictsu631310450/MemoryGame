using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SecondTry
{
    public class ManagerCard : MonoBehaviour
    {
        public Player player;

        [SerializeField] private bool useUnityEvent;
        public static UnityEvent onCardFlip = new UnityEvent();

        public List<CardData> cardAll = new List<CardData>();
        public GameObject parentCardAll;

        [SerializeField] private string num1;
        [SerializeField] private string num2;

        [SerializeField] private RectTransform refdefal;

        public List<CardPrefab> cardPrefabs = new List<CardPrefab>();
        public Sprite[] numberSprites = new Sprite[16];

        [Serializable]
        public class CardPrefab
        {
            public ColorType colorType;
            public Sprite imageSprite;
        }


        public enum ColorType
        {
            RED, //0
            BLUE,
            PURPLE,
            BROWN,
            GOLD,
            SILVER,
            BLACK,
            GREEN, //7
        }

        void Start()
        {
            Updatecard();
            SetNumber();
            RandomPosition();
            Debug.Log("Can Play");

            //CardPrefab cardPrefab = cardPrefabs.Find(x => x.colorType == ColorType.GREEN);

            //cardPrefab.colorBGPrefab
            //Instantiate(cardPrefab.colorBGPrefab, null, false);
        }


        private void RandomPosition()
        {
            for (int i = 0; i < cardAll.Count; i++)
            {
                int rd = UnityEngine.Random.Range(0, cardAll.Count);
                refdefal.transform.position = cardAll[i].transform.position;
                cardAll[i].transform.position = cardAll[rd].transform.position;
                cardAll[rd].transform.position = refdefal.transform.position;
            }
            Debug.Log("Random Postion Succeed");
        }


        private void SetNumber()
        {
            for (int i = 0; i < cardAll.Count; i++)
            {
                CardData cardData = cardAll[i].GetComponent<CardData>();
                //CardData cardData2 = cardAll[(cardAll.Count - (i + 1))].GetComponent<CardData>();

                //cardData.NumberSet(cardData2.numberCard);
                //cardData.SetNumber();

                int hhh = UnityEngine.Random.Range(0,7); //random color
                ColorType colorType = (ColorType)hhh;
                cardData.SetColor(hhh);
                cardData.bgImage.sprite = cardPrefabs.Find(x => x.colorType == colorType).imageSprite;


                int numerRand = UnityEngine.Random.Range(1, 16);//random number
                cardData.numberImage.sprite = numberSprites[numerRand];
                cardData.SetNumber(numerRand);


            }
        }

        public void ClickGetdata(GameObject carddata)
        {
            CardData cardManager = carddata.GetComponent<CardData>();
            Action ProcessGetData = () =>
            {
                if (num1 == "")
                {
                    num1 = cardManager.numberCard;
                }
                else
                {
                    num2 = cardManager.numberCard;
                }
            };

            if (cardManager.CheckOpencard())
            {
                //Debug.Log("Check : " + cardManager.CheckOpencard());
                if (useUnityEvent)
                {
                    cardManager.OpenCard2();
                    onCardFlip.Invoke();
                }
                else
                {
                    cardManager.OpenCard();
                }
                ProcessGetData();
                CheckAction();
            }
            else
            {
                Debug.Log("Choose the original card.");
            }
        }

        private void Updatecard()
        {
            cardAll.Clear();
            foreach (var tr in parentCardAll.GetComponentsInChildren<CardData>()) cardAll.Add(tr);
            //cardAll.RemoveAt(0);
        }

        private void CheckAction()
        {
            if (num1 != "" && num2 != "")
            {
                if (useUnityEvent)
                {
                    onCardFlip.RemoveAllListeners();
                    Debug.Log("Remove AllListeners");
                }

                if (num1 != num2)
                {
                    StartCoroutine(CardCooldown("Closer"));
                    player.ChangePlayer();
                }
                else
                {
                    StartCoroutine(CardCooldown("Destory"));
                }
            }
        }


        private IEnumerator CardCooldown(string want)
        {
            switch (want)
            {
                case "Closer":
                    yield return new WaitForSeconds(0.5f);
                    CloserCard();
                    break;

                case "Destory":
                    yield return new WaitForSeconds(1f);
                    DestroyCard();
                    yield return new WaitForSeconds(0.5f);
                    Updatecard();
                    yield return new WaitForSeconds(0.5f);
                    CloserCard();
                    break;
            }
        }

        private void CloserCard()
        {
            for (int i = 0; i < cardAll.Count; i++)
            {
                cardAll[i].GetComponent<CardData>().CloserCard();
            }
            num1 = "";
            num2 = "";
        }

        private void DestroyCard()
        {
            for (int i = 0; i < cardAll.Count; i++)
            {
                if (cardAll[i].GetComponent<CardData>().open)
                {
                    DestroyCard(cardAll[i].gameObject);
                }
            }
        }

        private void DestroyCard(GameObject obj)
        {
            Destroy(obj);
        }


        [Serializable]
        public class Player
        {
            [SerializeField] private GameObject player1;
            [SerializeField] private GameObject player2;

            private bool activeplayer1;

            public void ChangePlayer()
            {
                if (activeplayer1)
                {
                    player1.SetActive(false);
                    player2.SetActive(true);
                    activeplayer1 = false;
                }
                else
                {
                    player1.SetActive(true);
                    player2.SetActive(false);
                    activeplayer1 = true;
                }
            }
        }

    }
}