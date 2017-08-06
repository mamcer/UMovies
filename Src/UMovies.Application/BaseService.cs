using CrossCutting.Core.Logging;
using System;
using UMovies.Data;

namespace UMovies.Application
{
    public abstract class BaseService
    {
        protected readonly ILogService _logService;

        protected readonly IUnitOfWork _unitOfWork;

        protected BaseService(IUnitOfWork unitOfWork, ILogService logService)
        {
            _logService = logService;
            _unitOfWork = unitOfWork;
        }

        protected void LogException(Exception ex)
        {
            if (ex != null)
            {
                _logService.Error(ex.Message);
                _logService.Error(ex.StackTrace);

                if (ex.InnerException != null)
                {
                    _logService.Error(ex.InnerException.Message);
                    _logService.Error(ex.InnerException.StackTrace);
                }
            }
        }
    }
}