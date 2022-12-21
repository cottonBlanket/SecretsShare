using System;

namespace SecretsShare.DTO
{
    /// <summary>
    /// base entity class
    /// </summary>
    public class BaseDto
    {
        /// <summary>
        /// the unique identifier of the entity
        /// </summary>
        public Guid Id { get; set; }
    }
}