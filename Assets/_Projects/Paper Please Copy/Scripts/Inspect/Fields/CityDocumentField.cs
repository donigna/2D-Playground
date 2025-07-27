namespace com.Kuwiku
{
    public class CityDocumentField : DocumentField
    {
        public City city;

        protected override void _LocalStart()
        {
            DisplayText($"{city}");
        }
    }
}
