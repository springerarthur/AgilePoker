namespace AgilePoker.WebUI.Components.Card
{
    public class CardModel
    {
        private const string DefaultCardValue = "?";

        public CardModel(string value = DefaultCardValue)
        {
            Value = value;
        }

        public string Value { get; set; }

        public bool Selected { get; set; }
    }
}
