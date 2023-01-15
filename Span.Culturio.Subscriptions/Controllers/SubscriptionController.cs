using System;
using Microsoft.AspNetCore.Mvc;
using Span.Culturio.Core.Models.Subscription;
using Span.Culturio.Subscriptions.Services;

namespace Span.Culturio.Subscriptions.Controllers
{
    [Route("api/subcriptions")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;
        private readonly IPackageService _packageService;

        public SubscriptionController(ISubscriptionService subscriptionService, IPackageService packageService)
        {
            _subscriptionService = subscriptionService;
            _packageService = packageService;
        }

        [HttpPost]
        public async Task<ActionResult<SubscriptionDto>> CreateAsync(CreateSubscriptionDto createSubscriptionDto)
        {
            var subscriptionDto = await _subscriptionService.CreateAsync(createSubscriptionDto);
            if (subscriptionDto is null) return BadRequest("Subscription could not be created");
            var packageDto = await _packageService.GetPackage(subscriptionDto.PackageId);
            if (packageDto is null) return BadRequest("Package could not be found");
            var packageItemsDto = packageDto.CultureObjects.ToList();
            var _ = await _subscriptionService.CreateVisit(subscriptionDto.Id, packageItemsDto);

            return Ok(subscriptionDto);
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<SubscriptionDto>> GetAsync(int userId)
        {
            var subscription = await _subscriptionService.GetAsync(userId);
            if (subscription is null) return BadRequest("Subscription could not be found");
            return Ok(subscription);
        }

        [HttpPost("track-visit")]
        public async Task<ActionResult<string>> TrackVisit(TrackVisitDto trackVisitDto)
        {
            var result = await _subscriptionService.TrackVisit(trackVisitDto);
            switch (result)
            {
                case "SubscriptionNotFound":
                    return BadRequest("Subscription could not be found");
                case "SubscriptionNotActive":
                    return BadRequest("Subscription is inactive");
                case "VisitNotFound":
                    return BadRequest("Subscription with that culture object does not exist");
                case "NoVisitsLeft":
                    return BadRequest("No visits left");
                case "VisitTracked":
                    return Ok("Visit has been tracked");
                default:
                    return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("activate")]
        public async Task<ActionResult<string>> Activate(ActivateDto activateDto)
        {
            var subscriptionDto = await _subscriptionService.GetById(activateDto.SubscriptionId);
            var package = await _packageService.GetPackage(subscriptionDto.PackageId);
            int validDays = package.ValidDays;
            var result = await _subscriptionService.Activate(activateDto, validDays);
            switch (result)
            {
                case "SubscriptionNotFound":
                    return BadRequest("Subscription could not be found");
                case "SubscriptionAlreadyActive":
                    return BadRequest("Subscription is already active");
                case "SubscriptionActivated":
                    return Ok("Subscription has been activated");
                default:
                    return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}

