using System.Collections.Generic;
using UnityEngine;

namespace com.Kuwiku
{

    public class CityRule : RuleField
    {
        [SerializeField] List<City> legalCities;
        [SerializeField] List<City> illegalCities;

        public override bool RuleValidity(GameObject obj)
        {
            if (obj.TryGetComponent(out CityDocumentField objCity))
            {
                if (objCity.fieldType == FieldType.City)
                {
                    if (legalCities.Contains(objCity.city))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}