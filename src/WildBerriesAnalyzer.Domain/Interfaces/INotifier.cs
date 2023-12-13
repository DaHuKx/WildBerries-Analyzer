namespace WildBerriesAnalyzer.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс оповещений.
    /// </summary>
    public interface INotifier
    {
        /// <summary>
        /// Оповещение об успешном выполнении функции.
        /// </summary>
        /// <param name="message">Сообщение об успешном выполнении.</param>
        void Ok(string message);

        /// <summary>
        /// Незначительная ошибка во время выполнения функции.
        /// </summary>
        /// <param name="message">Предупредительное сообщение.</param>
        void Warning(string message);

        /// <summary>
        /// Оповещение об ошибке во время выполнения функции.
        /// </summary>
        /// <param name="message">Сообщение об ошибке.</param>
        void Error(string message);
    }
}
