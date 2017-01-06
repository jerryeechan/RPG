using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.SerializableAttribute]
public class Attribute : BaseAttribute {

		private List<RawBonus>  _rawBonuses;
        private List<FinalBonus> _finalBonuses;
        protected int _finalValue;
         
        public Attribute(int startingValue):base(startingValue) 
        {    
            isCalculated = false;
            _rawBonuses = new List<RawBonus>();
            _finalBonuses = new List<FinalBonus>();    
            _finalValue = baseValue;
        }

        public void OneTurn()
        {
            foreach(var fb in _finalBonuses)
            {
                if(!fb.OneTurn())
                {
                    removeFinalBonus(fb);
                }
            }
        }
         
        public void addRawBonus(RawBonus bonus)
        {
            isCalculated = false;
            _rawBonuses.Add(bonus);
        }
         
        public void addFinalBonus(FinalBonus bonus)
        {
            isCalculated = false;
            _finalBonuses.Add(bonus);
        }
         
        public void removeRawBonus(RawBonus bonus)
        {
			isCalculated = false;
            if (_rawBonuses.IndexOf(bonus) >= 0)
            {
				_rawBonuses.Remove(bonus);
            }
        }
         
        public void removeFinalBonus(FinalBonus bonus)
        {
            isCalculated = false;
            if (_finalBonuses.IndexOf(bonus) >= 0)
            {
                _finalBonuses.Remove(bonus);
            }
        }

		protected void applyRawBonuses()
        {
            // Adding value from raw
            int rawBonusValue = 0;
            float rawBonusMultiplier = 0;
             
            foreach (var bonus in _rawBonuses)
            {
                rawBonusValue += bonus.baseValue;
                rawBonusMultiplier += bonus.baseMultiplier;
            }
             
            _finalValue += rawBonusValue;
			_finalValue = (int)((1 + rawBonusMultiplier)*_finalValue);
        }
         
        protected void applyFinalBonuses()
        {
            // Adding value from final
            int finalBonusValue = 0;
            float finalBonusMultiplier = 0;
             
            foreach (var bonus in _finalBonuses)
            {
                finalBonusValue += bonus.baseValue;
                finalBonusMultiplier += bonus.baseMultiplier;
            }
             
            _finalValue += finalBonusValue;
			_finalValue = (int)((1 + finalBonusMultiplier)*_finalValue);
        }
         
        public virtual int calculateValue()
        {
            _finalValue = baseValue;
             
            // Adding value from raw
            int rawBonusValue = 0;
			float rawBonusMultiplier = 0;
             
            foreach (var bonus in _rawBonuses)
            {
                rawBonusValue += bonus.baseValue;
                rawBonusMultiplier += bonus.baseMultiplier;
            }
             
            _finalValue += rawBonusValue;
			_finalValue = (int)((1 + rawBonusMultiplier)*_finalValue);
             
            // Adding value from final
            int finalBonusValue = 0;
            float finalBonusMultiplier = 0;
             
            foreach (var bonus in _finalBonuses)
            {
                finalBonusValue += bonus.baseValue;
                finalBonusMultiplier += bonus.baseMultiplier;
            }
             
            _finalValue += finalBonusValue;
			_finalValue = (int)((1 + finalBonusMultiplier)*_finalValue);
            isCalculated = true;
            return _finalValue;
        }
        
        bool isCalculated = false;
        public int finalValue
        {
            get{
                if(isCalculated)
                    return _finalValue;
                else
				    return calculateValue();
			}
        }
        public static int operator +(Attribute a1, Attribute a2)  
        {  
            return a1.finalValue+a2.finalValue;
        }
        public static int operator +(int val, Attribute a)  
        {  
            return val+a.finalValue;
        }
        public static int operator +(Attribute a, int val)  
        {  
            return val+a.finalValue;
        }
        public static float operator /(Attribute a, float val)  
        {  
            return (float)a.finalValue/val;
        }
        public static float operator *(Attribute a, float val)  
        {
            return (float)a.finalValue*val;
        }
}

