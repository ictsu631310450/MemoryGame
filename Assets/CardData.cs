using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SecondTry
{
    public class CardData : MonoBehaviour
    {
        public Data _data;
        public Image bgImage;
        public Image numberImage;

        public int numberValue;
        public ManagerCard.ColorType colorType;

        [Header("ColorCard Player 1")]
        public string colorCard;
        [Obsolete("ไม่ใช้แล้วโว้ย")]private string[] color = { "red", "blue", "purple", "brown", "gold", "silver", "balck", "green" };


        [Header("NumberCard Infomation")]
        public string numberCard;
        private int rangeNumber;

        [SerializeField] private GameObject card;
        [SerializeField] private GameObject number;

        private Animator animator;
        public bool open;

        void Start()
        {
            rangeNumber = 16;
            animator = GetComponent<Animator>();

            Action randomeCard = () =>
            {
                //Gen Number
                int rdnumber = UnityEngine.Random.Range(1, rangeNumber);
                numberCard = "Number " + "(" + rdnumber.ToString() + ")";
                //Gen ColorCard
                int rdcolor = UnityEngine.Random.Range(0, color.Length);
                colorCard = color[rdcolor];
            };

            //Action
            randomeCard();
            //XrandomeCard(50);

            //Method
            //GenCard();
            _data.GetData();
            //SetColorCard();
            SetNumber();


        }


        public void NumberSet(string number)
        {
            numberCard = number;
        }
        /*
        public void XrandomeCard(int abc)
        {
            //Gen Number
            int rdnumber = UnityEngine.Random.Range(1, abc);
            numberCard = "Number " + "(" + rdnumber.ToString() + ")";
            //Gen ColorCard
            int rdcolor = UnityEngine.Random.Range(0, color.Length);
            colorCard = color[rdcolor];
        }
        */

        public void GenCard()
        {
            //Gen Number
            int rdnumber = UnityEngine.Random.Range(0, rangeNumber);
            numberCard = rdnumber.ToString();
            //Gen ColorCard
            int rdcolor = UnityEngine.Random.Range(0, color.Length);
            colorCard = color[rdcolor];
        }

        public void SetColorCard()
        {
            for (int i = 0; i < _data.cardcolor.Count; i++)
            {
                if (colorCard == _data.cardcolor[i].name)
                {
                    //Debug.Log("Match Color " + _data.cardcolor[i].name);
                    card.GetComponent<Image>().sprite = _data.cardcolor[i].GetComponent<Image>().sprite;

                }
            }
            Debug.Log("Match Color Succeed");
        }
        public void SetNumber()
        {
            //Debug.Log("numberCard : " + numberCard);
            //Debug.Log("cardnumber" + _data.cardnumber[0].name);

            for (int i = 0; i < _data.cardnumber.Count; i++)
            {
                if (numberCard == _data.cardnumber[i].name)
                {
                    //Debug.Log("Match " + _data.cardnumber[i].name);
                    number.GetComponent<Image>().sprite = _data.cardnumber[i].GetComponent<Image>().sprite;
                }
            }
            //Debug.Log("Match Number Succeed");
        }


        public void OpenCard2()
        {
            ManagerCard.onCardFlip.AddListener(OpenCard);
        }

        public void OpenCard()
        {
            if (open == false)
            {
                StartCoroutine(CardOpenAnimation());
            }
        }

        public bool CheckOpencard()
        {
            if (open == true)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private IEnumerator CardOpenAnimation()
        {
            open = true;
            animator.SetBool("Open", true);
            yield return new WaitForSeconds(0.5f);
            card.SetActive(true);
            number.SetActive(true);
        }


        public void CloserCard()
        {
            StartCoroutine(CloserCardAnimation());
        }

        private IEnumerator CloserCardAnimation()
        {
            open = false;
            animator.SetBool("Open", false);
            yield return new WaitForSeconds(0.5f);
            card.SetActive(false);
            number.SetActive(false);
        }


        [System.Serializable]
        public class Data
        {
            public List<GameObject> cardnumber = new List<GameObject>();
            public GameObject parentnumber;

            public List<GameObject> cardcolor = new List<GameObject>();
            public GameObject parentcolor;



            public void GetData()
            {
                Updatecardnumber();
                UpdatecardColor();
            }

            private void Updatecardnumber()
            {
                cardnumber.Clear();
                foreach (var tr in parentnumber.GetComponentsInChildren<Transform>()) cardnumber.Add(tr.gameObject);
                cardnumber.RemoveAt(0);
            }

            private void UpdatecardColor()
            {
                cardcolor.Clear();
                foreach (var tr in parentcolor.GetComponentsInChildren<Transform>()) cardcolor.Add(tr.gameObject);
                cardcolor.RemoveAt(0);
            }


        }


    }
}