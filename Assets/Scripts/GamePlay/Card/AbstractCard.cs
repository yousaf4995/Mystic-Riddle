using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CardModel
{
    public abstract class AbstractCard : MonoBehaviour,Icard
    {
        [SerializeField]internal bool isFliped = false;
        public virtual void Flip()
        {
            
        }

        public virtual void Init(CardData cardData)
        {
            
        }

        public virtual void CardClicked()
        {
           
        }

        public virtual void FlipNormalFace()
        { 

        }
        public virtual void FlipSpecificFace()
        {

        }
        public abstract void CalculateFlip();

    }
}
