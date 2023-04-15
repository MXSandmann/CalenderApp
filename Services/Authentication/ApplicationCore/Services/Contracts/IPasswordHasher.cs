namespace WebUI.Services.Contracts
{
    public interface IPasswordHasher
    {
        /// <summary>
        /// Compute hash value from provided password in clear text.
        /// </summary>
        /// <param name="password">Provided password in clear text.</param>
        /// <returns>A tuple containing the computed hash value from the provided password and the auto-generated salt as a string.</returns>        
        (string hashedPassword, string salt) HashPassword(string password);
        /// <summary>
        /// Verifies provided password in clear text with stored hash value from database.
        /// </summary>
        /// <param name="password">Provided password in clear text.</param>
        /// <param name="hashedPassword">Hashed password from database</param>
        /// <param name="salt">Salt from database, needed for equality check</param>
        /// <returns>True if provided password matches stored hashed password, otherway false.</returns>
        bool VerifyPassword(string password, string hashedPassword, string salt);
    }
}
