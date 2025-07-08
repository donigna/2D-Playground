using UnityEngine;

namespace com.Kuwiku
{
    public class GenderRule : RuleField
    {
        [SerializeField] private Gender gender;

        public override bool RuleValidity(GameObject obj)
        {
            if (obj.TryGetComponent(out GenderDocumentField genderDoc))
            {
                if (genderDoc.gender == gender)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
