using UnityEngine;

namespace com.Kuwiku
{
    public class GenderDocumentField : DocumentField
    {
        public Gender gender;

        protected override void _LocalStart()
        {
            DisplayText(gender.ToString());
        }
    }
}
