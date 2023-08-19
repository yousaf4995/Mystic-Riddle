using System;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay
{
    public class Card:MonoBehaviour, Icard
    {
        public void Init()
        {
            throw new System.NotImplementedException();
        }

        public void Flip()
        {
            throw new System.NotImplementedException();
        }

        public void OnCardClicked()
        {
            throw new System.NotImplementedException();
        }
    }
}

[Serializable]
public struct CardData
{
    public int CardType;
    public Image cardFaceImage;
}