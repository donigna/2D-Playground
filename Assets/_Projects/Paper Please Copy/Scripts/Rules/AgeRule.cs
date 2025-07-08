
using System;
using UnityEngine;

namespace com.Kuwiku
{
    public class AgeRule : RuleField
    {
        [SerializeField] int minAge;

        public override bool RuleValidity(GameObject obj)
        {
            if (obj.TryGetComponent(out BirthDayDocumentField objAge))
            {
                if (objAge.fieldType == FieldType.BirthDay)
                {
                    if (objAge.GetAge() > minAge)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}