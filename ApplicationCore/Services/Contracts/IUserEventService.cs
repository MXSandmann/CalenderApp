﻿using ApplicationCore.Models;

namespace ApplicationCore.Services.Contracts
{
    public interface IUserEventService
    {
        Task<IEnumerable<UserEvent>> AddNewUserEvent(UserEvent userEvent);
        Task RemoveUserEvent(Guid id);
        Task<UserEvent> UpdateUserEvent(UserEvent userEvent);
        Task<IEnumerable<UserEvent>> GetUserEvents(string sortBy);
        Task<UserEvent> GetUserEventById(Guid id);
    }
}
