using System;
namespace Span.Culturio.Core.Models.Subscription
{
    public class CreateSubscriptionDto
    {
        public int UserId { get; set; }
        public int PackageId { get; set; }
        public string Name { get; set; }
    }
}

