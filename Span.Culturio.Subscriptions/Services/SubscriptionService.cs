using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Span.Culturio.Core.Models.Package;
using Span.Culturio.Core.Models.Subscription;
using Span.Culturio.Subscriptions.Data;
using Span.Culturio.Subscriptions.Data.Entities;

namespace Span.Culturio.Subscriptions.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public SubscriptionService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<string> Activate(ActivateDto activateDto, int validDays)
        {
            var subscription = await _context.Subscriptions.FindAsync(activateDto.SubscriptionId);
            if (subscription is null) return "SubscriptionNotFound";

            if (subscription.State == "active") return "SubscriptionAlreadyActive";

            subscription.State = "active";
            subscription.ActiveFrom = DateTime.Now;
            subscription.ActiveTo = DateTime.Now.AddDays(validDays);
            _context.Subscriptions.Update(subscription);
            await _context.SaveChangesAsync();

            return "SubscriptionActivated";
        }

        public Task<string> Activate(ActivateDto activateDto)
        {
            throw new NotImplementedException();
        }

        public async Task<SubscriptionDto> CreateAsync(CreateSubscriptionDto createSubscriptionDto)
        {
            var subscription = _mapper.Map<Subscription>(createSubscriptionDto);

            subscription.ActiveFrom = null;
            subscription.ActiveTo = null;
            subscription.State = "Inactive";
            subscription.RecordedVisits = 0;

            await _context.Subscriptions.AddAsync(subscription);
            await _context.SaveChangesAsync();

            var subscriptionDto = _mapper.Map<SubscriptionDto>(subscription);
            return subscriptionDto;
        }

        public async Task<string> CreateVisit(int subscriptionId, List<PackageItemDto> packageItemsDto)
        {
            packageItemsDto.ForEach(async x =>
            {
                var visit = new Visits
                {
                    SubscriptionId = subscriptionId,
                    PackageItemId = x.Id,
                    VisitsLeft = x.AvailableVisits,
                };

                await _context.AddAsync(visit);
            });
            await _context.SaveChangesAsync();

            return "Success";
        }

        public async Task<IEnumerable<SubscriptionDto>> GetAsync(int userId)
        {
            var subscriptions = await _context.Subscriptions.Where(x => x.UserId == userId).ToListAsync();
            var subscriptionsDto = _mapper.Map<List<SubscriptionDto>>(subscriptions);

            return subscriptionsDto;
        }

        public async Task<SubscriptionDto> GetById(int subscriptionId)
        {
            var subscription = await _context.Subscriptions.FindAsync(subscriptionId);
            var subscriptionDto = _mapper.Map<SubscriptionDto>(subscription);

            return subscriptionDto;
        }

        public async Task<string> TrackVisit(TrackVisitDto trackVisitDto)
        {
            var subscription = await _context.Subscriptions.FindAsync(trackVisitDto.SubscriptionId);
            if (subscription is null) return "SubscriptionNotFound";
            if (subscription.State != "active") return "SubscriptionNotActive";

            var visit = await _context.Visits.FirstAsync(x => x.SubscriptionId == trackVisitDto.SubscriptionId && x.PackageItemId == trackVisitDto.PackageItemId);
            if (visit is null) return "VisitNotFound";

            var visitsLeft = visit.VisitsLeft;
            if (visitsLeft == 0) return "NoVisitsLeft";

            visit.VisitsLeft = visitsLeft - 1;

            subscription.RecordedVisits += 1;
            _context.Subscriptions.Update(subscription);
            await _context.SaveChangesAsync();
            return "VisitTracked";
        }
    }
}

