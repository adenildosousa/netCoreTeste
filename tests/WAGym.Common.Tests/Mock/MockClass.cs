using System.ComponentModel.DataAnnotations;

namespace WAGym.Common.Tests.Mock
{
    public class MockClass
    {
        [Key]
        public int Id { get; set; }
        public string? Description { get; set; }
    }
}
