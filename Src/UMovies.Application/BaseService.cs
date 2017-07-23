using System;
using CrossCutting.Core.Logging;

namespace UMovies.Application
{
    public abstract class BaseService
    {
        protected readonly ILogService _logService;

        protected BaseService(ILogService logService)
        {
            _logService = logService;
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