using AutoMapper;
using Microsoft.Extensions.Configuration;

namespace Orchestrator.Application.Common.Convinience
{
public class MappingProfile:Profile
    {
        public MappingProfile(IConfiguration configuration)
        {
        }
    }
}