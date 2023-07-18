using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zust.Web.Abstract
{
    public interface IStaticService
    {
        string GetRandomCoverImage(string path);
        List<string> GetRandomStatusImagePaths(int count, string path);
    }
}
