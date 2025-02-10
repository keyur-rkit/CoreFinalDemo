using System.Reflection;

namespace CoreFinalDemo.Extensions
{
    /// <summary>
    /// Extension methods for converting DTO objects to POCO objects.
    /// </summary>
    public static class DTOtoPOCO
    {
        /// <summary>
        /// Converts a DTO object to a POCO object.
        /// </summary>
        /// <typeparam name="POCO">The type of the POCO object.</typeparam>
        /// <param name="dto">The DTO object.</param>
        /// <returns>The converted POCO object.</returns>
        /// <exception cref="Exception">Thrown when the conversion fails.</exception>
        public static POCO Convert<POCO>(this object dto)
        {
            Type? pocoType = typeof(POCO) ?? throw new Exception();
            POCO? pocoInstance = (POCO)Activator.CreateInstance(type: pocoType);

            // Get properties
            PropertyInfo[] dtoProperties = dto.GetType().GetProperties();
            PropertyInfo[] pocoProperties = pocoType.GetProperties();

            foreach (PropertyInfo dtoProperty in dtoProperties)
            {
                PropertyInfo? pocoProperty = Array.Find(array: pocoProperties, p => p.Name == dtoProperty.Name);

                if (pocoProperty != null && dtoProperty.PropertyType == pocoProperty.PropertyType)
                {
                    object? value = dtoProperty.GetValue(dto);
                    pocoProperty.SetValue(pocoInstance, value);
                }
            }

            return pocoInstance != null ? pocoInstance : throw new Exception();
        }
    }
}