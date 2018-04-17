namespace CardsPocker.Common
{
    public interface IMenuOption
    {
        IMenuOption Parent { get; set; }
        bool HasChildOptions { get; }
        bool HasNoInput { get; }

        string InputRequest { get; }
        string Title { get; }

        void RenderWithChildren(dynamic data);

        void RenderOptionOnly(dynamic optionNumber);

        void HandleInput(string input);

    }
}
