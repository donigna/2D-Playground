namespace com.Kuwiku
{
    public class NameDocumentField : DocumentField
    {
        public string firstName;
        public string lastName;

        protected override void _LocalStart()
        {
            DisplayText($"{firstName} {lastName}");
        }
    }
}
