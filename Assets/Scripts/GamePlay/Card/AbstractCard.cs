using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CardModel
{
    public abstract class AbstractCard : MonoBehaviour,Icard
    {
        public virtual void Flip()
        {
            
        }

        public virtual void Init(CardData cardData)
        {
            
        }

        public virtual void OnCardClicked()
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
