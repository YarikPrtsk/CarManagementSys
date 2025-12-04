using Domain.Enums;

namespace Domain.Entities;

public class CarSale
    {
        public Guid Id { get; private set; }
        public string SaleNumber { get; private set; } = string.Empty;
        public Guid CarId { get; private set; }
        public string CustomerName { get; private set; } = string.Empty;
        public string CustomerContact { get; private set; } = string.Empty;
        public string Title { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public SalePriority Priority { get; private set; }
        public SaleStatus Status { get; private set; }
        public DateTime ScheduledDate { get; private set; }
        public DateTime? CompletedAt { get; private set; }
        public string? CompletionNotes { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public Guid? CustomerId { get; private set; }

        private CarSale() { }

        public static CarSale New(
    Guid id,
    string saleNumber,
    Guid carId,
    Guid? customerId,
    string customerName,
    string customerContact,
    string title,
    string description,
    SalePriority priority,
    DateTime scheduledDate)
        {
            return new CarSale
            {
                Id = id,
                SaleNumber = saleNumber,
                CarId = carId,
                CustomerName = customerName,
                CustomerContact = customerContact,
                Title = title,
                Description = description,
                Priority = priority,
                Status = SaleStatus.Pending,
                ScheduledDate = scheduledDate,
                CreatedAt = DateTime.UtcNow,
                CustomerId = customerId
            };
        }

        public void UpdateDetails(string title, string description, SalePriority priority, DateTime scheduledDate)
        {
            Title = title;
            Description = description;
            Priority = priority;
            ScheduledDate = scheduledDate;
            UpdatedAt = DateTime.UtcNow;
        }

        public void StartSale()
        {
            Status = SaleStatus.InProgress;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Complete(string completionNotes)
        {
            Status = SaleStatus.Completed;
            CompletedAt = DateTime.UtcNow;
            CompletionNotes = completionNotes;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Cancel()
        {
            Status = SaleStatus.Cancelled;
            UpdatedAt = DateTime.UtcNow;
        }
    }
