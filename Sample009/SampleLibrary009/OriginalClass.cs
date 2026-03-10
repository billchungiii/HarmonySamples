namespace SampleLibrary009
{
    public class OriginalClass
    {
        /// <summary>
        /// This method intentionally throws an exception after displaying the provided message.
        /// </summary>
        /// <param name="message">The message to be displayed before the exception is thrown.</param>
        /// <exception cref="InvalidOperationException">Thrown to demonstrate exception handling.</exception>
        public void DisplayMessage(string message)
        {
            Console.WriteLine($"Original Message : {message}");
            throw new InvalidOperationException("An error occurred in DisplayMessage.");
        }
    }
}
