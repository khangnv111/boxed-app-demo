namespace BoxedApp.ViewModels;

using System.ComponentModel.DataAnnotations;

public class SaveBook
{

    /// <summary>
    /// Gets or sets the make of the car.
    /// </summary>
    /// <example>Book name</example>
    [Required]
    public string NameBook { get; set; } = default!;

}
