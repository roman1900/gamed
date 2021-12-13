using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using gamed.Models;
namespace gamed
{
public interface IIGDBService
{
	Task<List<PlatformModel>> GetPlatforms();
}
}
