using MediatR;

namespace Application.Common.ModificationSchedules.Commands;


public record DeactivateModificationScheduleCommand(Guid Id) : IRequest<Result>;